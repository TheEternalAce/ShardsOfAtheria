using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Items.Potions;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
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
        #endregion

        #endregion

        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[projectile.owner];
            int type = projectile.type;

            if (ElectricProj.Contains(type))
            {
                target.AddBuff(ModContent.BuffType<ElectricShock>(), player.HasBuff(ModContent.BuffType<Conductive>()) ? 1200 : 600);
            }
            if (FireProj.Contains(type))
            {
                target.AddBuff(Main.hardMode ? BuffID.OnFire3 : BuffID.OnFire, 600);
            }
        }
    }
}