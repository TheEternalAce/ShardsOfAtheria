using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Items.Weapons;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles.Tools;
using System.Collections.Generic;
using Terraria.Audio;
using ReLogic.Content;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Creative;

namespace ShardsOfAtheria.Items.Tools
{
	public class PhantomDrill : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("");
			ItemID.Sets.IsDrill[Type] = true;

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

        public override void SetDefaults()
		{
			Item.width = 64;
			Item.height = 30;

			Item.pick = 150;

			Item.damage = 20;
			Item.DamageType = DamageClass.Melee;
			Item.knockBack = 0;

			Item.useTime = 5; //Actual Break 1 = FAST 50 = SUPER SLOW
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.UseSound = SoundID.Item23;
			Item.autoReuse = true;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.channel = true;

			Item.shootSpeed = 32;
			Item.rare = ItemRarityID.Green;
			Item.value = Item.sellPrice(0, 10);
			Item.shoot = ModContent.ProjectileType<PhantomDrillProj>();
		}
	}
}