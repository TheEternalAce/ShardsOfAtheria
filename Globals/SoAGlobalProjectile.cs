using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Items.Potions;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Globals
{
    public class SoAGlobalProjectile : GlobalProjectile
    {
        #region Projectile Elements (for 1.0)
        public static List<int> ElectricProj = new();
        public static List<int> FireProj = new();
        public static List<int> IceProj = new();
        public static List<int> MetalProj = new();

        #region Projectile Sub-Elements
        public static List<int> AreusProj = new();
        public static List<int> BloodProj = new();
        public static List<int> FrostfireProj = new();
        public static List<int> HardlightProj = new();
        public static List<int> PlasmaProj = new();
        public static List<int> OrganicProj = new();
        #endregion

        #endregion

        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[projectile.owner];

            if (ElectricProj.Contains(projectile.type))
            {
                target.AddBuff(ModContent.BuffType<ElectricShock>(), player.HasBuff(ModContent.BuffType<Conductive>()) ? 1200 : 600);
            }
        }
    }
}