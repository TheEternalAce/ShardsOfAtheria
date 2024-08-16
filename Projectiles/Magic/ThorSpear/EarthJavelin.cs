using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic.ThorSpear
{
    public class EarthJavelin : ModProjectile
    {
        public float Timer
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }
        public bool FullyCharged
        {
            get => Projectile.ai[1] == 1;
            set => Projectile.ai[1] = value ? 1 : 0;
        }

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
            float progress = (float)Timer / maxCharge;

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

            if (BeingHeld || Timer < minChargeRequired + (player.Overdrive() ? 40 : 0))
            {
                Projectile.timeLeft = 10;
                if (Timer < maxCharge) Timer += 0.125f * attackSpeed * (player.Overdrive() ? 1.2f : 1f);
                if (Timer == maxCharge && !FullyCharged)
                {
                    SoundEngine.PlaySound(SoundID.MaxMana, Projectile.Center);
                    FullyCharged = true;
                    ShardsHelpers.DustRing(player.Center, 2, DustID.Electric);
                    if (player.controlUseTile) Projectile.Kill();
                }
            }
            Projectile.velocity = player.MountedCenter.DirectionTo(Main.MouseWorld);
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
            if (Timer < minChargeRequired) return;
            int type = ModContent.ProjectileType<ElectricJavelin>();
            var player = Projectile.GetPlayerOwner();
            if (player.Overdrive()) type = ModContent.ProjectileType<EarthmoverBeam>();
            if (FullyCharged)
            {
                type = ModContent.ProjectileType<EarthmoverBeam>();
                Projectile.damage = (int)(Projectile.damage * 2.5f);
            }
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity * 16f, type, Projectile.damage, Projectile.knockBack);
            SoundEngine.PlaySound(SoundID.Item1, Projectile.Center);
        }
    }
}
