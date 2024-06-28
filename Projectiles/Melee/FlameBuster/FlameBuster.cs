using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee.FlameBuster
{
    public class FlameBuster : ModProjectile
    {
        public const int FadeInDuration = 7;
        public const int FadeOutDuration = 4;

        public const int TotalDuration = 10;

        public float CollisionWidth => 10f * Projectile.scale;

        public int Timer
        {
            get => (int)Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        public override string Texture => ModContent.GetInstance<FlameKnuckleBuster>().Texture;

        public override void SetStaticDefaults()
        {
            Projectile.AddElement(0);
            Projectile.AddRedemptionElement(2);
            Projectile.AddRedemptionElement(8);
        }

        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(18);
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.scale = 1f;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ownerHitCheck = true;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 60;
            Projectile.hide = true;
        }

        public override void OnSpawn(IEntitySource source)
        {
            var player = Projectile.GetPlayerOwner();
            if ((player.controlRight && Projectile.direction >= 0) || (player.controlLeft && Projectile.direction < 0))
                player.velocity.X *= 1.1f;
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

            if (Projectile.ai[1] == 1 && player.Overdrive() && Timer > 18) Timer--;

            Projectile.SetVisualOffsets(34);
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
            Vector2 end = start + Projectile.velocity * 6f;
            float collisionPoint = 0f;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, end, CollisionWidth, ref collisionPoint);
        }

        public override void OnKill(int timeLeft)
        {
            float numberProjectiles = 2;
            var source = Projectile.GetSource_FromThis();
            var position = Projectile.Center;
            var velocity = Projectile.velocity;
            velocity.Normalize();
            velocity *= 16f;
            int type = ModContent.ProjectileType<FlameBusterBullet>();
            int damage = Projectile.damage / 3;
            if (Projectile.GetPlayerOwner().ownedProjectileCounts[type] >= 6) return;

            if (Projectile.ai[1] == 1)
            {
                position.Y -= 50;
                velocity *= 0f;
                type++;
                Projectile.NewProjectile(source, position, velocity, type, damage, 2f);
                return;
            }
            var player = Projectile.GetPlayerOwner();
            for (int i = 0; i < numberProjectiles; i++)
            {
                var adjustment = Projectile.velocity.RotatedBy(MathHelper.PiOver2 + MathHelper.Pi * i) * 1f;
                var adjustedPosition = position + adjustment;
                if (!Collision.CanHit(Projectile.Center, 0, 0, player.Center, 0, 0)) adjustedPosition = player.Center + adjustment;
                Projectile.NewProjectile(source, adjustedPosition, velocity, type, damage, 2f);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire3, 300);
            var player = Projectile.GetPlayerOwner();
            player.SetImmuneTimeForAllTypes(15);
            player.velocity = Projectile.velocity;
            player.velocity.Normalize();
            player.velocity *= -4f;
        }
    }
}
