using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Projectiles.Magic.WandAreus;
using ShardsOfAtheria.Tiles.Crafting;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
    public class AreusWand : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
            Item.AddAreus();
        }

        public override void SetDefaults()
        {
            Item.width = 52;
            Item.height = 56;

            Item.damage = 66;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 3;
            Item.crit = 29;
            Item.mana = 12;

            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item43;
            Item.autoReuse = true;
            Item.staff[Type] = true;
            Item.noMelee = true;

            Item.shootSpeed = 16;
            Item.rare = ItemDefaults.RarityPlantera;
            Item.value = 150000;
            Item.shoot = ModContent.ProjectileType<ElectricHook>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 20)
                .AddIngredient(ItemID.GoldBar, 5)
                .AddIngredient(ItemID.ThornHook)
                .AddTile<AreusFabricator>()
                .Register();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useStyle = ItemUseStyleID.Swing;
                Item.UseSound = SoundID.Item1;
                Item.noUseGraphic = true;
                Item.useTime = 16;
                Item.useAnimation = 16;
            }
            else
            {
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.UseSound = SoundID.Item43;
                Item.noUseGraphic = false;
                Item.useTime = 26;
                Item.useAnimation = 26;
            }
            return base.CanUseItem(player);
        }

        public override bool? UseItem(Player player)
        {
            Item.FixSwing(player);
            return true;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                velocity.Normalize();
                type = ModContent.ProjectileType<AreusWandBlade>();
            }
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
    }
}