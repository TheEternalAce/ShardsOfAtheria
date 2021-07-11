using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Accessories
{
	public class EyeOfTheAllSeer: ModItem
	{
        public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Grants Shine, Night Owl, Dangersense and Hunter potion effects\n" +
				"<right> to zoom\n" +
				"+2% damage\n" +
				"''I can see everything!''");
		}

        public override void SetDefaults()
		{
			item.width = 22;
			item.height = 22;
			item.value = Item.sellPrice(silver: 15);
			item.rare = ItemRarityID.White;
			item.accessory = true;
			item.expert = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.AddBuff(BuffID.Shine, 2);
			player.AddBuff(BuffID.NightOwl, 2);
			player.AddBuff(BuffID.Dangersense, 2);
			player.AddBuff(BuffID.Hunter, 2);
			player.AddBuff(BuffID.Spelunker, 2);
			player.scope = true;
			player.allDamage += 0.02f;
		}

        public override void UpdateInventory(Player player)
		{
			if (NPC.downedMechBoss2)
			{
				player.AddBuff(BuffID.Shine, 2);
				player.AddBuff(BuffID.NightOwl, 2);
				player.AddBuff(BuffID.Dangersense, 2);
				player.AddBuff(BuffID.Hunter, 2);
				player.AddBuff(BuffID.Spelunker, 2);
				player.scope = true;
				player.allDamage += 0.02f;
			}
		}
    }
}