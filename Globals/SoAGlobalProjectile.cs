using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Items.Potions;
using ShardsOfAtheria.Projectiles.Weapon.Areus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Globals
{
    public class SoAGlobalProjectile : GlobalProjectile
    {
        #region Projectile Categories
        public static bool[] ThrowingProjectile = new bool[ItemLoader.ItemCount];
        #endregion

        #region Projectile Elements (for 1.0)
        public static bool[] ElectricProjectile = new bool[ItemLoader.ItemCount];
        public static bool[] FireProjectile = new bool[ItemLoader.ItemCount];
        public static bool[] IceProjectile = new bool[ItemLoader.ItemCount];
        public static bool[] MetalProjectile = new bool[ItemLoader.ItemCount];

        #region Projectile Sub-Elements
        public static bool[] AreusProjectile = new bool[ItemLoader.ItemCount];
        public static bool[] BloodProjectile = new bool[ItemLoader.ItemCount];
        public static bool[] FrostfireProjectile = new bool[ItemLoader.ItemCount];
        #endregion

        #endregion

        public override void SetDefaults(Projectile projectile)
        {
            if (ThrowingProjectile[projectile.type])
            {
                projectile.DamageType = DamageClass.Throwing;
            }

            #region Assign Sub-Element projectile to a Base-Elements
            if (BloodProjectile[projectile.type])
            {
                MetalProjectile[projectile.type] = true;
                IceProjectile[projectile.type] = true;
            }
            if (FrostfireProjectile[projectile.type])
            {
                FireProjectile[projectile.type] = true;
                IceProjectile[projectile.type] = true;
            }
            if (AreusProjectile[projectile.type])
            {
                MetalProjectile[projectile.type] = true;
                if (projectile.type != ModContent.ProjectileType<MourningStar>())
                {
                    ElectricProjectile[projectile.type] = true;
                }
            }
            #endregion

            #region Assign element to projectile
            if (projectile.type == ProjectileID.Spark)
            {
                FireProjectile[projectile.type] = true;
            }
            #endregion
        }

        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[projectile.owner];

            if (ElectricProjectile[projectile.type])
            {
                target.AddBuff(ModContent.BuffType<ElectricShock>(), player.HasBuff(ModContent.BuffType<Conductive>()) ? 1200 : 600);
            }
        }
    }
}