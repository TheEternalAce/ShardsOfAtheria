using SagesMania.Items.Potions;
using SagesMania.Projectiles;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Weapons.Magic
{
	public class PacBlasterEllie : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Fire Pac-Blaster");
			Tooltip.SetDefault("'Genderbent version of a certain Root Beer addict's friend'\n" +
                "Damage scales throughout progression");
		}

		public override void SetDefaults() 
		{
			item.damage = 36;
			item.magic = true;
			item.noMelee = true;
			item.mana = 8;
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
			item.shoot = ModContent.ProjectileType<FirePacBlasterShot>();
			item.shootSpeed = 20;
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<RootBeerCan>(), 30);
			recipe.AddIngredient(ItemID.HellstoneBar, 10);
			recipe.AddTile(TileID.Hellforge);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<PacBlasterCharles>());
			recipe.AddIngredient(ItemID.HellstoneBar, 10);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			tooltips.Add(new TooltipLine(mod, "Special Item", "[c/FF6400:Special Item]"));
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