using SagesMania.Items.Potions;
using SagesMania.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Weapons
{
	public class DracoPellet : ModItem
	{
		public override void SetStaticDefaults() 
		{
			//Tooltip.SetDefault("");
		}

		public override void SetDefaults() 
		{
			item.damage = 1012;
			item.melee = true;
			item.width = 30;
			item.height = 26;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 2;
			item.value = Item.sellPrice(gold: 80);
			item.rare = ItemRarityID.Red;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/PacBlasterShoot");
			item.crit = 14;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.shoot = ModContent.ProjectileType<DracoPelletProj>();
			item.shootSpeed = 16f;
			item.channel = true;
		}

		public override void AddRecipes() 
		{
			Mod calamity = ModLoader.GetMod("Calamity");
			if (calamity != null)
			{
				ModRecipe recipe = new ModRecipe(mod);
				recipe.AddIngredient(ModContent.ItemType<RootBeerCan>(), 30);
				recipe.AddIngredient(calamity.ItemType("SoulFramgent"));
				recipe.AddTile(calamity.TileType("DreadonForge"));
				recipe.SetResult(this);
				recipe.AddRecipe();
			}
		}
	}
}