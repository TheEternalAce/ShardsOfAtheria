using ShardsOfAtheria.Items.Placeable;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using ShardsOfAtheria.Items.Weapons.Areus;
using Terraria.GameContent.Creative;

namespace ShardsOfAtheria.Items.Tools
{
    public class RedAreusPickaxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Uses highly concentrated electricity to cut through stones and ores");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

        public override void SetDefaults()
		{
			Item.width = 60;
			Item.height = 54;

			Item.pick = 150;

			Item.damage = 20;
			Item.DamageType = DamageClass.Melee;
			Item.knockBack = 6;
			Item.mana = 0;

			Item.useTime = 5; //Actual Break 1 = FAST 50 = SUPER SLOW
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.UseSound = SoundID.Item15;
			Item.autoReuse = true;
			Item.useTurn = true;

			Item.rare = ItemRarityID.Cyan;
			Item.value = Item.sellPrice(0, 1, 25);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ModContent.ItemType<AreusShard>(), 20)
				.AddIngredient(ModContent.ItemType<SoulOfSpite>(), 13)
				.AddIngredient(ItemID.Wire, 10)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(10))
			{
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Electric);
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Torch);
			}
		}
	}
}