using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.PlayerBuff.AreusArmor;
using ShardsOfAtheria.Projectiles.Magic;
using ShardsOfAtheria.Projectiles.Melee;
using ShardsOfAtheria.Projectiles.Ranged;
using ShardsOfAtheria.Projectiles.Summon.Minions;
using ShardsOfAtheria.Projectiles.Throwing;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Players
{
    public partial class AreusArmorPlayer
    {
        public bool imperialSet;
        public int imperialVoid;
        public const int VOID_MAX = 99;
        private int voidStarTimer;
        private const int VOID_STAR_TIMER_MAX = 120;

        private void ImperialActive_Melee()
        {
            var spawnpos = Main.MouseWorld + Vector2.One.RotatedByRandom(MathHelper.TwoPi) * 130f;
            var vector = Vector2.Normalize(Main.MouseWorld - spawnpos) * 32;
            Projectile.NewProjectile(Player.GetSource_FromThis(), spawnpos, vector, ModContent.ProjectileType<VoidSlash>(),
                (int)ClassDamage.ApplyTo(120 + imperialVoid), 8f);
        }
        private void ImperialActive_Ranged()
        {
            SoundEngine.PlaySound(SoundID.NPCDeath52, Player.Center);
            var vector = Main.MouseWorld - Player.Center;
            vector.Normalize();
            vector *= 8;
            Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, vector, ModContent.ProjectileType<AreusVoidBlast>(),
                (int)ClassDamage.ApplyTo(120 + imperialVoid), 8f);
        }
        private void ImperialActive_Thrown()
        {
            SoundEngine.PlaySound(SoundID.NPCDeath52, Player.Center);
            var vector = Main.MouseWorld - Player.Center;
            vector.Normalize();
            vector *= 8;
            Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, vector, ModContent.ProjectileType<VoidBlade>(),
                (int)ClassDamage.ApplyTo(120 + imperialVoid), 8f);
        }
        private void ImperialActive_Magic()
        {
            SoundEngine.PlaySound(SoundID.NPCDeath52, Player.Center);
            Vector2 vector = new(0, -16);
            for (int i = 0; i < 5; i++)
            {
                float speedModifier = 1f - Main.rand.NextFloat(0.3f);
                var perturbedSpeed = vector.RotatedByRandom(MathHelper.PiOver4);

                Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, perturbedSpeed * speedModifier,
                    ModContent.ProjectileType<AreusShriek>(), (int)ClassDamage.ApplyTo(120 + imperialVoid), 8f);
            }
            float numProjectiles = 3f;
            float rotation = MathHelper.PiOver4;
            for (int i = 0; i < numProjectiles; i++)
            {
                var perturbedSpeed = vector.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numProjectiles - 1)));
                float speedModifier = 1f - Main.rand.NextFloat(0.3f);

                Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, perturbedSpeed * speedModifier,
                    ModContent.ProjectileType<AreusShriek>(), (int)ClassDamage.ApplyTo(120 + imperialVoid), 8f);
            }
        }
        private void ImperialActive_Summon()
        {
            foreach (var projectile in Main.projectile)
            {
                if (projectile.active && projectile.type == ModContent.ProjectileType<BlackStar>() &&
                    projectile.owner == Player.whoAmI)
                {
                    projectile.Kill();
                }
            }
        }

        public void ConsumeImperialVoid()
        {
            imperialVoid -= 33;
            Player.AddBuff<ShadeState>(180);
        }

        public void ImperialVoidStar()
        {
            if (CommanderSetChip)
            {
                if (imperialVoid >= 33)
                {
                    if (++voidStarTimer >= VOID_STAR_TIMER_MAX)
                    {
                        int damage = (int)ClassDamage.ApplyTo(imperialVoid);
                        Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero,
                            ModContent.ProjectileType<BlackStar>(), damage / 2, 6, Player.whoAmI);
                        ConsumeImperialVoid();
                        voidStarTimer = 0;
                    }
                }
            }
        }

        private void ImperialOnHitEffect(NPC target, NPC.HitInfo hit)
        {
            if (hit.DamageType.CountsAsClass(DamageClass.Melee) && Player.statMana != Player.statManaMax2)
                Player.statMana += 5;
            if (!Player.HasBuff<ShadeState>() || CommanderSetChip)
            {
                imperialVoid += 3;
                if (imperialVoid > VOID_MAX) imperialVoid = VOID_MAX;
            }
            if (Player.HasBuff<ShadeState>())
            {
                if (MageSetChip)
                {
                    int type = ModContent.ProjectileType<VoidDagger>();
                    if (Player.ownedProjectileCounts[type] == 0)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, type, (int)ClassDamage.ApplyTo(65), 0f, Player.whoAmI, i, target.whoAmI);
                        }
                    }
                }
            }
        }
    }
}
