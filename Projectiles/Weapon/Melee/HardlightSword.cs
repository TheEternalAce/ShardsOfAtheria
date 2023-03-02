using Microsoft.Xna.Framework;
using MMZeroElements;
using ShardsOfAtheria.Dusts;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee
{
    public class HardlightSword : ModProjectile
    {
        public override string Texture => "ShardsOfAtheria/Items/Weapons/Melee/ValkyrieBlade";

        public override void SetStaticDefaults()
        {
            ProjectileElements.Electric.Add(Type);
            ProjectileElements.Metal.Add(Type);
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 30;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.damage = 37;

            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ownerHitCheck = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 30;

            DrawOffsetX = -20;
            DrawOriginOffsetY = -20;
        }

        private double rotation;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (!CheckActive(player))
            {
                Projectile.Kill();
            }

            if (Projectile.ai[1] == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<HardlightDust_Blue>(), Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
                    Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<HardlightDust_Pink>(), Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
                }
                Projectile.ai[1] = 1;
            }
            //Idle
            rotation += MathHelper.ToRadians(4);
            Projectile.Center = player.Center + Vector2.One.RotatedBy(((Projectile.ai[0] / 2f) * MathHelper.TwoPi) + rotation) * 65;
            Projectile.timeLeft = 60;
            Projectile.rotation = Vector2.Normalize(Projectile.Center - player.Center).ToRotation() + MathHelper.ToRadians(45f);
        }

        bool CheckActive(Player player)
        {
            if (player == null || player.dead || !player.active || !player.ShardsOfAtheria().valkyrieCrown)
            {
                return false;
            }
            return true;
        }

        public override bool? CanCutTiles()
        {
            return false;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 2; i++)
            {
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<HardlightDust_Blue>(), Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<HardlightDust_Pink>(), Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
            }
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            // "Hit anything between the player and the tip of the sword"
            // shootSpeed is 2.1f for reference, so this is basically plotting 12 pixels ahead from the center
            Vector2 vector = Vector2.Normalize(Main.player[Projectile.owner].Center - Projectile.Center) * 24f;
            Vector2 start = Projectile.Center + vector;
            Vector2 end = Projectile.Center - vector;
            float collisionPoint = 0f; // Don't need that variable, but required as parameter
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, end, 10, ref collisionPoint);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Color color = new(227, 182, 245, 80);
            Projectile.DrawPrimsAfterImage(color);
            lightColor = Color.White;
            return true;
        }
    }
}
