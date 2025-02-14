using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic
{
    public class ShockKnife : ModProjectile
    {
        bool Overdrive
        {
            get => Projectile.ai[2] == 1;
            set => Projectile.ai[2] = value ? 1 : 0;
        }

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            Projectile.AddElement(1);
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
            Projectile.AddRedemptionElement(12);
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 18;
            Projectile.timeLeft = 40;
            Projectile.extraUpdates = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.aiStyle = 0;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.stopsDealingDamageAfterPenetrateHits = true;
        }

        public override void OnSpawn(IEntitySource source)
        {
            Overdrive = Projectile.GetPlayerOwner().Overdrive();
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.spriteDirection = (int)Projectile.ai[0];

            if (++Projectile.ai[1] >= 20)
            {
                var vector = Vector2.Normalize(Projectile.velocity).RotatedBy(MathHelper.PiOver2 * Projectile.ai[0]);
                Projectile.velocity += vector * 2;

                var proj = ShardsHelpers.FindClosestProjectile(Projectile.Center, 50f, CheckProjectile);
                if (proj != null && Projectile.Hitbox.Intersects(proj.Hitbox)) { Projectile.Kill(); proj.Kill(); }
            }
            if (Overdrive && Main.rand.NextBool(8)) Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, DustID.Electric);
        }

        bool CheckProjectile(Projectile projectile)
        {
            if (projectile.type != Type) return false;
            if (projectile.owner != Projectile.owner) return false;
            if (projectile.whoAmI == Projectile.whoAmI) return false;
            return true;
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.Resize(60);
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (Projectile.ai[1] >= 20) modifiers.ScalingBonusDamage += 0.5f;
            if (!Projectile.Hitbox.Intersects(target.Hitbox)) modifiers.FinalDamage /= 3;
            if (Overdrive && target.life < 50) modifiers.SetInstantKill();
        }

        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            if (Projectile.ai[1] >= 20) modifiers.FinalDamage *= 1.5f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            var vector = Vector2.Normalize(Projectile.velocity) * 2f;
            for (int i = 0; i < Main.rand.Next(4, 7); i++) Dust.NewDust(target.position, target.width, target.height, DustID.Blood, -vector.X * 0.5f, -vector.Y);
            SoundEngine.PlaySound(SoA.HeavyCut.WithVolumeScale(0.25f), Projectile.Center);

            if (target.CanBeChasedBy())
            {
                var player = Projectile.GetPlayerOwner();
                int heal = Main.rand.Next(2) + 1;
                int multiplier = 1;
                if (target.life <= 0) multiplier = 2;
                if (Overdrive)
                {
                    target.AddBuff<ElectricShock>(300);
                    player.RestoreMana(14 * multiplier);
                    heal += 4;
                }
                player.Heal(heal * multiplier);
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            var vector = Vector2.Normalize(Projectile.velocity) * 2f;
            for (int i = 0; i < Main.rand.Next(4, 7); i++) Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Blood, -vector.X * 0.5f, -vector.Y);

            var player = Projectile.GetPlayerOwner();
            int heal = Main.rand.Next(2) + 1;
            int multiplier = 1;
            if (target.statLife <= 0) multiplier = 2;
            if (Overdrive)
            {
                target.AddBuff<ElectricShock>(300);
                player.RestoreMana(14 * multiplier);
                heal += 4;
            }
            player.Heal(heal * multiplier);
        }

        public override void OnKill(int timeLeft)
        {
            int dustAmount = Main.rand.Next(4, 7);
            for (int i = 0; i < dustAmount; i++) Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Blood);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.DrawAfterImage(lightColor);
            return true;
        }
    }
}
