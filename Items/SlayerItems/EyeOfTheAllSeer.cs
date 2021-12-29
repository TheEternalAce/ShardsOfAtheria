using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems
{
	public class EyeOfTheAllSeer: SlayerItem
	{
        public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Grants Shine, Night Owl, Dangersense, Hunter and Spelunker potion effects\n" +
				"<right> to zoom\n" +
				"2% increased damage\n" +
				"'I can see everything!'");
		}

        public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 22;
			Item.value = Item.sellPrice(silver: 15);
			Item.rare = ItemRarityID.Expert;
			Item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.AddBuff(BuffID.Shine, 2);
			player.AddBuff(BuffID.NightOwl, 2);
			player.AddBuff(BuffID.Dangersense, 2);
			player.AddBuff(BuffID.Hunter, 2);
			player.AddBuff(BuffID.Spelunker, 2);
			player.scope = true;
			player.GetDamage(DamageClass.Generic) += 0.02f;
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
				player.GetDamage(DamageClass.Generic) += 0.02f;
				Item.accessory = false;
			}
		}
    }
}