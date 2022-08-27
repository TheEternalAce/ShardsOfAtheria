using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using ShardsOfAtheria.Projectiles.Weapon.Ranged;
using Terraria.DataStructures;
using ShardsOfAtheria.Projectiles.Other;
using Terraria.GameContent.Creative;
using ShardsOfAtheria.Items.Potions;

namespace ShardsOfAtheria.Items.Weapons.Areus
{
    public class AreusBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Brilliant light show'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            SoAGlobalItem.AreusWeapon.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 54;

            Item.damage = 20;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 4f;
            Item.crit = 5;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item91;
            Item.autoReuse = true;
            Item.noMelee = true;

            Item.shootSpeed = 16f;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(0, 5);
            Item.shoot = ProjectileID.PurificationPowder;
            Item.useAmmo = AmmoID.Arrow;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 5)
                .AddIngredient(ModContent.ItemType<SoulOfDaylight>(), 7)
                .AddIngredient(ModContent.ItemType<ChargedFeather>(), 10)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                type = ModContent.ProjectileType<AreusArrow>();
            }
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 5);
        }
    }
}