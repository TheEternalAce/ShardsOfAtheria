using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Items.Tools.Misc;
using ShardsOfAtheria.NPCs;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Tools;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SoulCrystals
{
    public class EyeSoulCrystal : SoulCrystal
    {
        public static readonly string tip = "Summon 3 Servants after 5 seconds, these Servants will chase down enemies\n" +
                "Also creates an All Seeing Eye that lights up the cursor and marks enemies, making them take 10% more damage";

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault(tip);

            base.SetStaticDefaults();
        }

        public override bool? UseItem(Player player)
        {
            if (absorbSoulTimer == 0 || ModContent.GetInstance<ShardsConfigClientSide>().instantAbsorb)
            {
                if (player.ownedProjectileCounts[ModContent.ProjectileType<AllSeeingEye>()] <= 0)
                {
                    Projectile.NewProjectile(player.GetSource_FromThis(), Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<AllSeeingEye>(), 0, 0f, player.whoAmI);
                }
            }
            return base.UseItem(player);
        }
    }
}
