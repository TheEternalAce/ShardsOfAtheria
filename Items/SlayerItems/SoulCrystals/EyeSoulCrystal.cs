using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Tools;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems.SoulCrystals
{
    public class EyeSoulCrystal : SoulCrystal
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Crystal (Eye Of Cthulhu)");
            Tooltip.SetDefault("Summon 3 Servants when dashing, these Servants will chase down enemies\n" +
                "Also creates an All Seeing Eye that lights up the cursor and makes enemies take 10% more damage");
        }

        public override bool? UseItem(Player player)
        {
            absorbSoulTimer--;
            if (absorbSoulTimer == 0 || ModContent.GetInstance<ClientSideConfig>().instantAbsorb)
            {
                Main.LocalPlayer.GetModPlayer<SlayerPlayer>().EyeSoul = SoulCrystalStatus.Absorbed;
                Projectile.NewProjectile(player.GetProjectileSource_Item(Item), Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<AllSeeingEye>(), 0, 0f, player.whoAmI);
            }
            return base.UseItem(player);
        }
    }
}
