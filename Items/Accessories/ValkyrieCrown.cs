using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Tiles;
using System.Collections.Generic;
using ShardsOfAtheria.Players;

namespace ShardsOfAtheria.Items.Accessories
{
    [AutoloadEquip(EquipType.Face)]
    public class ValkyrieCrown : ModItem
    {
        public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Attacks shock enemies briefly");
            ArmorIDs.Head.Sets.DrawHatHair[Item.faceSlot] = true;

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

		public override void SetDefaults()
		{
			Item.width = 32;
			Item.height = 32;
			Item.accessory = true;
            Item.canBePlacedInVanityRegardlessOfConditions = true;

            Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(0, 0, 50);
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			player.GetModPlayer<SoAPlayer>().valkyrieCrown = true;
        }
    }
}