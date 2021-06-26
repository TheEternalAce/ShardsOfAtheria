using SagesMania.Items.Potions;
using SagesMania.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Weapons
{
	public class PacBlasterCharles : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Ice Pac-Blaster");
			Tooltip.SetDefault("''This is the greatest plaaaaaan!''\n" +
				"'A certain Root Beer addict's friend'\n" +
				"Damage scales throughout progression\n" +
				"[c/FF6400:Special Item]");
		}

		public override void SetDefaults() 
		{
			item.damage = 40;
			item.magic = true;
			item.noMelee = true;
			item.mana = 10;
			item.width = 32;
			item.height = 32;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 0;
			item.rare = ItemRarityID.Blue;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/PacBlasterShoot");
			item.autoReuse = true;
			item.crit = 0;
			item.shoot = ModContent.ProjectileType<IcePacBlasterShot>();
			item.shootSpeed = 16f;
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<RootBeerCan>(), 30);
			recipe.AddIngredient(ItemID.IceBlock, 30);
			recipe.AddTile(TileID.Hellforge);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<PacBlasterEllie>());
			recipe.AddIngredient(ItemID.IceBlock, 30);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}

		public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
		{
			if (Main.hardMode)
			{
				add += .1f;
			}
			if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
			{
				add += .15f;
			}
			if (NPC.downedPlantBoss)
			{
				add += .15f;
			}
			if (NPC.downedGolemBoss)
			{
				add += .2f;
			}
			if (NPC.downedAncientCultist)
			{
				add += .5f;
			}
			if (NPC.downedMoonlord)
			{
				add += 1f;
			}
		}
	}
}