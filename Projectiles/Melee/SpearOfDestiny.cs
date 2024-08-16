using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Accessories;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee
{
    public class SpearOfDestiny : ModProjectile
    {
        public override string Texture => ModContent.GetInstance<DestinyLance>().Texture;

        public override void SetStaticDefaults()
        {
            Projectile.AddElement(1);
            Projectile.AddRedemptionElement(2);
        }

        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.width = Projectile.height = 20;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
        }

        public override void AI()
        {
            var player = Projectile.GetPlayerOwner();
            if (player.HasItemEquipped<DestinyLance>(out var _1)) Projectile.timeLeft = 2;
            if (player.ownedProjectileCounts[Type] > 1)
            {
                Projectile.Kill();
                player.ownedProjectileCounts[Type]--;
            }
            Projectile.spriteDirection = Projectile.direction;
            Projectile.SetVisualOffsets(70);
            if (Projectile.ai[0] == 0 && player.ItemAnimationActive && player.HeldItem.damage > 0)
            {
                Projectile.ai[0] = 50;
                SoundEngine.PlaySound(SoundID.DD2_MonkStaffSwing, Projectile.Center);
            }
            Projectile.velocity = player.Center.DirectionTo(Main.MouseWorld);
            Projectile.Center = player.MountedCenter + Projectile.velocity * (90f + Projectile.ai[0]);
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4 * Projectile.spriteDirection;
            Projectile.scale = 1f + Projectile.ai[0] * 0.005f;
            if (Projectile.ai[0] > 0f) Projectile.ai[0]--;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            var player = Projectile.GetPlayerOwner();
            if (projHitbox.Intersects(targetHitbox)) return true;
            return ShardsHelpers.DeathrayHitbox(player.Center + Projectile.velocity * 30, Projectile.Center, targetHitbox, 25f);
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (Projectile.ai[0] > 30f) modifiers.FlatBonusDamage += 20f;
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }
    }
}
