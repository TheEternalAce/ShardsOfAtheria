using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic
{
    public class ElectricFlame : ModProjectile
    {
        public int dustType = DustID.Electric;
        public Color flameColor = Color.Purple;

        private bool Spin
        {
            get => Projectile.ai[2] == 1;
            set => Projectile.ai[2] = value ? 1 : 0;
        }

        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.Flames;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 7;
            Projectile.AddDamageType(3, 5);
            Projectile.AddElement(0);
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(1);
            Projectile.AddRedemptionElement(2);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Flames);
            Projectile.aiStyle = 0;
            Projectile.scale = 0.1f;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= 50;
            }
            Projectile.SetVisualOffsets(98, true);

            if (Projectile.scale < 1)
            {
                Projectile.scale += 0.02f;
            }

            if (Spin)
            {
                Projectile.rotation += MathHelper.ToRadians(-2.5f);
            }
            if (Projectile.ai[1] > 8)
            {
                var vector = Projectile.velocity;
                vector.Normalize();
                vector *= 0.2f;
                Projectile.velocity -= vector;
            }

            float countdown = 10f;
            if (Spin)
            {
                countdown /= 2f;
            }
            if (++Projectile.ai[0] >= countdown)
            {
                if (++Projectile.ai[1] == 10)
                {
                    Projectile.Kill();
                }
                Projectile.ai[0] = 0;
            }
            int dustDenominator = 3;
            if (Main.rand.NextBool(dustDenominator))
            {
                var color = Color.White;
                Color fadeTo = new(60, 60, 60, 220);
                float lightMultiplier = 1 - (0.1f * Projectile.ai[1]);
                var position = Projectile.position;
                position.X -= 22;
                position.Y -= 22;
                var dust = Dust.NewDustDirect(position, 50, 50, dustType, 0, 0, 0, Color.Lerp(fadeTo, color, lightMultiplier));
                dust.noGravity = true;
                dust.fadeIn = 1;
                dust.velocity = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(15));
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.damage = (int)(Projectile.damage * 0.85f);
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.X -= 22;
            hitbox.Y -= 22;
            hitbox.Width = 50;
            hitbox.Height = 50;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = flameColor;
            Color fadeTo = new(60, 60, 60, 220);
            float lightMultiplier = 1 - (0.1f * Projectile.ai[1]);

            var texture = ModContent.Request<Texture2D>(Texture).Value;
            for (int i = 0; i < 7; i++)
            {
                var drawPosition = Projectile.position - Main.screenPosition;
                Rectangle sourceRect = new(0, 98 * i, 98, 98);
                Main.spriteBatch.Draw(texture,
                    drawPosition,
                    sourceRect,
                    Color.Lerp(fadeTo, lightColor, lightMultiplier),
                    Projectile.rotation,
                    new Vector2(49),
                    Projectile.scale,
                    SpriteEffects.None,
                    0);
                lightColor.A += 20;
            }
            return false;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Spin = true;
            Projectile.velocity *= 0;
            return false;
        }
    }
}
