using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Items.Potions;
using ShardsOfAtheria.NPCs;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Globals
{
    public class SoAGlobalProjectile : GlobalProjectile
    {
        public static List<int> AreusProjectile = new();

        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[projectile.owner];

            if (AreusProjectile.Contains(projectile.type))
            {
                target.AddBuff(ModContent.BuffType<ElectricShock>(), player.HasBuff(ModContent.BuffType<Conductive>()) ? 1200 : 600);
            }
        }
    }
}