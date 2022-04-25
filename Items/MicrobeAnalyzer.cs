using ShardsOfAtheria.Projectiles.Weapon.Ammo;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ShardsOfAtheria.Items
{
    public class MicrobeAnalyzer : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Use to analyze Microbes in inventory\n" +
                "'You found this on your way here and picked it up, thinking it could be useful'");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 26;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Blue;
            Item.autoReuse = true;
            Item.useTurn = true;
        }

        public override bool CanUseItem(Player player)
        {
            return player.HasItem(ModContent.ItemType<UnanalyzedMicrobe>());
        }

        public override bool? UseItem(Player player)
        {
            var dropChooser = new WeightedRandom<int>();
            dropChooser.Add(ModContent.ItemType<Bacteria>());
            dropChooser.Add(ModContent.ItemType<DNA>());
            dropChooser.Add(ModContent.ItemType<Virus>());

            int microbe = Main.LocalPlayer.FindItem(ModContent.ItemType<UnanalyzedMicrobe>());
            if (player.inventory[microbe].stack > 0)
            {
                player.inventory[microbe].stack--;
                Item.NewItem(player.GetItemSource_Misc(ModContent.ItemType<MicrobeAnalyzer>()), player.getRect(), dropChooser);
            }
            return true;
        }
    }
}