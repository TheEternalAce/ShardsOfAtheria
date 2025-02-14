using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace ShardsOfAtheria.Projectiles.Magic.ThorSpear
{
    public class EarthJavelin : ModProjectile
    {
        public bool fullyCharged = false;
        public float timer = 0;
        public int minChargeRequired = 10;
        public int maxCharge = 50;

        public bool BeingHeld => Main.player[Projectile.owner].channel && !Main.player[Projectile.owner].noItems && !Main.player[Projectile.owner].CCed;

        public override string Texture => ModContent.GetInstance<EarthmoverSpear>().Texture;

        public override void SetStaticDefaults()
        {
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(5);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 20;
            Projectile.penetrate = 5;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.extraUpdates = 2;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            var player = Projectile.GetPlayerOwner();
            float attackSpeed = player.GetTotalAttackSpeed(DamageClass.Magic);
            int endLagBase = 20;
            int endLag = endLagBase - (int)(endLagBase * (attackSpeed - 1));
            float progress = (float)timer / maxCharge;

            if (player.ownedProjectileCounts[Type] > 1)
            {
                Projectile.Kill();
                player.ownedProjectileCounts[Type]--;
            }

            Projectile.spriteDirection = Projectile.direction;
            Projectile.SetVisualOffsets(72);
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4 * Projectile.spriteDirection;

            player.heldProj = Projectile.whoAmI;
            player.SetDummyItemTime(endLag);
            player.manaRegenDelay = endLag * 2;
            player.manaRegenCount = 0;

            if (BeingHeld || timer < minChargeRequired + (player.Overdrive() ? 40 : 0))
            {
                Projectile.timeLeft = 10;
                if (timer < maxCharge) timer += 0.125f * attackSpeed * (player.Overdrive() ? 1.2f : 1f);
                if (timer >= maxCharge && !fullyCharged)
                {
                    fullyCharged = true;
                    if (player.controlUseTile) Projectile.Kill();
                    SoundEngine.PlaySound(SoundID.MaxMana, Projectile.Center);
                    ShardsHelpers.DustRing(player.Center, 2, DustID.Electric);
                }
            }
            if (player.IsLocal()) Projectile.velocity = player.MountedCenter.DirectionTo(Main.MouseWorld);
            Projectile.netUpdate = true;
            Projectile.Center = player.MountedCenter + Projectile.velocity * 30f + Projectile.velocity * 50f * (1f - progress);
            player.ChangeDir(Projectile.direction);
            player.itemRotation = Projectile.DirectionFrom(player.MountedCenter).ToRotation();
            if (Projectile.Center.X < player.MountedCenter.X)
            {
                player.itemRotation += (float)Math.PI;
            }
            player.itemRotation = MathHelper.WrapAngle(player.itemRotation);
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            if (timer < minChargeRequired) return;
            int type = ModContent.ProjectileType<ElectricJavelin>();
            var player = Projectile.GetPlayerOwner();
            if (!player.IsLocal()) return;
            if (player.Overdrive()) type = ModContent.ProjectileType<EarthmoverBeam>();
            if (fullyCharged)
            {
                type = ModContent.ProjectileType<EarthmoverBeam>();
                Projectile.damage = (int)(Projectile.damage * 2.5f);
            }
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity * 16f, type, Projectile.damage, Projectile.knockBack);
            SoundEngine.PlaySound(SoundID.Item1, Projectile.Center);
        }
    }
}
