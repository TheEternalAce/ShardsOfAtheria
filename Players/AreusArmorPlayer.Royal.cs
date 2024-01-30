using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.PlayerBuff;
using ShardsOfAtheria.Projectiles.Magic;
using ShardsOfAtheria.Projectiles.Melee;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Projectiles.Ranged;
using ShardsOfAtheria.Projectiles.Summon.Minions;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Players
{
    public partial class AreusArmorPlayer
    {
        public bool royalSet;
        public int royalVoid;
        public const int ROYAL_VOID_MAX = 99;
        private int voidStarTimer;
        private const int VOID_STAR_TIMER_MAX = 120;

        private void RoyalActive_Melee()
        {
            var spawnpos = Main.MouseWorld + Vector2.One.RotatedByRandom(MathHelper.TwoPi) * 130f;
            var vector = Vector2.Normalize(Main.MouseWorld - spawnpos) * 32;
            Projectile.NewProjectile(Player.GetSource_FromThis(), spawnpos, vector, ModContent.ProjectileType<VoidSlash>(),
                (int)ClassDamage.ApplyTo(120 + royalVoid), 8f);
        }
        private void RoyalActive_Ranged()
        {
            SoundEngine.PlaySound(SoundID.NPCDeath52, Player.Center);
            var vector = Main.MouseWorld - Player.Center;
            vector.Normalize();
            vector *= 8;
            Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, vector, ModContent.ProjectileType<AreusVoidBlast>(),
                (int)ClassDamage.ApplyTo(120 + royalVoid), 8f);
        }
        private void RoyalActive_Magic()
        {
            SoundEngine.PlaySound(SoundID.NPCDeath52, Player.Center);
            Vector2 vector = new(0, -16);
            for (int i = 0; i < 5; i++)
            {
                float speedModifier = 1f - Main.rand.NextFloat(0.3f);
                var perturbedSpeed = vector.RotatedByRandom(MathHelper.PiOver4);

                Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, perturbedSpeed * speedModifier,
                    ModContent.ProjectileType<AreusShriek>(), (int)ClassDamage.ApplyTo(120 + royalVoid), 8f);
            }
            float numProjectiles = 3f;
            float rotation = MathHelper.PiOver4;
            for (int i = 0; i < numProjectiles; i++)
            {
                var perturbedSpeed = vector.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numProjectiles - 1)));
                float speedModifier = 1f - Main.rand.NextFloat(0.3f);

                Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, perturbedSpeed * speedModifier,
                    ModContent.ProjectileType<AreusShriek>(), (int)ClassDamage.ApplyTo(120 + royalVoid), 8f);
            }
        }
        private void RoyalActive_Summon()
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

        private void ConsumeRoyalVoid()
        {
            royalVoid -= 33;
            Player.AddBuff<ShadeState>(180);
        }

        private void RoyalVoidStar()
        {
            int type = ModContent.ProjectileType<TheRoyalCrown>();
            int headSlot = EquipLoader.GetEquipSlot(Mod, "RoyalCrown", EquipType.Head);
            if (Player.ownedProjectileCounts[type] == 0 && Player.head == headSlot)
            {
                Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, type, 0, 0);
            }
            if (CommanderSet)
            {
                if (royalVoid >= 33)
                {
                    if (++voidStarTimer >= VOID_STAR_TIMER_MAX)
                    {
                        int damage = (int)ClassDamage.ApplyTo(royalVoid);
                        Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero,
                            ModContent.ProjectileType<BlackStar>(), damage / 2, 6, Player.whoAmI);
                        ConsumeRoyalVoid();
                        voidStarTimer = 0;
                    }
                }
            }
        }

        private void RoyalOnHitEffect(NPC target, NPC.HitInfo hit)
        {
            if (hit.DamageType == DamageClass.Melee)
            {
                if (Player.statMana != Player.statManaMax2)
                {
                    Player.statMana += 5;
                }
            }
            if (!Player.HasBuff<ShadeState>() || CommanderSet)
            {
                royalVoid += 3;
                if (royalVoid > ROYAL_VOID_MAX)
                {
                    royalVoid = ROYAL_VOID_MAX;
                }
            }
            if (Player.HasBuff<ShadeState>())
            {
                if (MageSet)
                {
                    int type = ModContent.ProjectileType<VoidDagger>();
                    if (Player.ownedProjectileCounts[type] == 0)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, type,
                                (int)ClassDamage.ApplyTo(65), 0f, Player.whoAmI, i, target.whoAmI);
                        }
                    }
                }
            }
        }
    }
}
