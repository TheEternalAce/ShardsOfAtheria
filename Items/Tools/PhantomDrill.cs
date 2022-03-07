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
using ShardsOfAtheria.Utilities;

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
			Item.damage = 20;
			Item.DamageType = DamageClass.Melee;
			Item.width = 64;
			Item.height = 30;
			Item.useTime = 5; //Actual Break 1 = FAST 50 = SUPER SLOW
			Item.useAnimation = 10;
			Item.pick = 150;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 0;
			Item.value = Item.sellPrice(gold: 10);
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item23;
			Item.autoReuse = true;
			Item.useTurn = true;
			Item.noMelee = true;
			Item.noUseGraphic = true;
			Item.channel = true;
			Item.shoot = ModContent.ProjectileType<PhantomDrillProj>();
			Item.shootSpeed = 32;
		}
	}
}