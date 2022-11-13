using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Items.Potions;
using ShardsOfAtheria.Projectiles.Weapon.Areus;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Globals
{
    public class SoAGlobalProjectile : GlobalProjectile
    {
        #region Projectile Categories
        public static List<int> ThrowingProjectile = new();
        #endregion

        #region Projectile Elements (for 1.0)
        public static List<int> ElectricProjectile = new();
        public static List<int> FireProjectile = new();
        public static List<int> IceProjectile = new();
        public static List<int> MetalProjectile = new();

        #region Projectile Sub-Elements
        public static List<int> AreusProjectile = new();
        public static List<int> BloodProjectile = new();
        public static List<int> FrostfireProjectile = new();
        #endregion

        #endregion

        public override void SetDefaults(Projectile projectile)
        {
            if (ThrowingProjectile.Contains(projectile.type))
            {
                projectile.DamageType = DamageClass.Throwing;
            }

            #region Assign Sub-Element projectile to a Base-Elements
            if (BloodProjectile.Contains(projectile.type))
            {
                MetalProjectile.Add(projectile.type);
                IceProjectile.Add(projectile.type);
            }
            if (FrostfireProjectile.Contains(projectile.type))
            {
                FireProjectile.Add(projectile.type);
                IceProjectile.Add(projectile.type);
            }
            if (AreusProjectile.Contains(projectile.type))
            {
                MetalProjectile.Add(projectile.type);
                if (projectile.type != ModContent.ProjectileType<MourningStar>())
                {
                    ElectricProjectile.Add(projectile.type);
                }
            }
            #endregion
        }

        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[projectile.owner];

            if (ElectricProjectile.Contains(projectile.type))
            {
                target.AddBuff(ModContent.BuffType<ElectricShock>(), player.HasBuff(ModContent.BuffType<Conductive>()) ? 1200 : 600);
            }
        }
    }
}