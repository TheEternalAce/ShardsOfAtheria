using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Buffs.PlayerBuff.OnHitBuffs;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee.Baton
{
    public class AreusTonfa : ModProjectile
    {
        public int FadeInDuration = 7;
        public int FadeOutDuration = 2;

        public int TotalDuration = 12;

        public float CollisionWidth => 10f * Projectile.scale;

        public int Timer
        {
            get => (int)Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        public override void SetStaticDefaults()
        {
            Projectile.MakeTrueMelee();
            Projectile.AddAreus();
        }

        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(18);
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.scale = 1f;
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            Projectile.ownerHitCheck = true;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 60;
            Projectile.hide = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 60;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            Timer += 1;
            if (Timer >= TotalDuration)
            {
                Projectile.Kill();
                return;
            }
            else
            {
                player.heldProj = Projectile.whoAmI;
            }

            Projectile.Opacity = Utils.GetLerpValue(0f, FadeInDuration, Timer, clamped: true) * Utils.GetLerpValue(TotalDuration, TotalDuration - FadeOutDuration, Timer, clamped: true);

            Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: false, addGfxOffY: false);
            Projectile.Center = playerCenter + Projectile.velocity * (Timer - 1f);

            Projectile.spriteDirection = (Vector2.Dot(Projectile.velocity, Vector2.UnitX) >= 0f).ToDirectionInt();

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4 * Projectile.spriteDirection;

            Projectile.SetVisualOffsets(50);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.damage = (int)(Projectile.damage * 0.85f);
            Projectile.GetPlayerOwner().AddBuff<ThousandStrikes>(4);
            if (Projectile.ai[1] == 0)
            {
                Projectile.ai[1] = 1;
                var position = Main.rand.NextVector2FromRectangle(target.Hitbox);
                for (int i = 0; i++ < 4;)
                {
                    Dust dust = Dust.NewDustPerfect(position, DustID.Electric);
                    dust.velocity = Projectile.velocity * (1 + 0.5f * i);
                    dust.noGravity = true;
                }
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Projectile.GetPlayerOwner().AddBuff<ThousandStrikes>(4);
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override void CutTiles()
        {
            DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
            Vector2 start = Projectile.Center;
            Vector2 end = start + Projectile.velocity.SafeNormalize(-Vector2.UnitY) * 10f;
            Utils.PlotTileLine(start, end, CollisionWidth, DelegateMethods.CutTiles);
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Vector2 start = Projectile.Center;
            Vector2 end = start + Projectile.velocity * 70f;
            float collisionPoint = 0f;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, end, CollisionWidth, ref collisionPoint);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            int r = lightColor.R + SoA.ElectricColor.R;
            int g = lightColor.G + SoA.ElectricColor.G;
            int b = lightColor.B + SoA.ElectricColor.B;
            if (r > 255) r = 255;
            if (g > 255) g = 255;
            if (b > 255) b = 255;
            lightColor = new(r, g, b);
            var texture = TextureAssets.Projectile[927];
            var offset = Vector2.Normalize(Projectile.velocity).RotatedBy(MathHelper.PiOver2 * Projectile.direction) * 8f;
            var position = Projectile.Center + Projectile.velocity * 30f + offset - Main.screenPosition;
            Main.EntitySpriteDraw(texture.Value, position, null, SoA.ElectricColorA, Projectile.rotation + MathHelper.PiOver4 * Projectile.direction, texture.Size() / 2f, new Vector2(0.2f, 5f), SpriteEffects.None);
            position = Projectile.Center + Projectile.velocity * 15f + offset - Main.screenPosition;
            Main.EntitySpriteDraw(texture.Value, position, null, SoA.ElectricColorA, Projectile.rotation + MathHelper.PiOver4 * Projectile.direction, texture.Size() / 2f, new Vector2(0.2f, 5f), SpriteEffects.None);
            return base.PreDraw(ref lightColor);
        }
    }
}
