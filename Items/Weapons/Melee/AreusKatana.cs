using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Projectiles.Melee.ElecKatana;
using ShardsOfAtheria.Tiles.Crafting;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class AreusKatana : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddAreus();
            Item.AddUpgradable();
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 42;

            Item.damage = 60;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6;
            Item.crit = 4;

            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;

            Item.shootSpeed = 6;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(0, 1, 50);
            Item.shoot = ModContent.ProjectileType<ElecKunai>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 17)
                .AddIngredient(ItemID.GoldBar, 5)
                .AddIngredient<Jade>(2)
                .AddIngredient(ItemID.SoulofFlight, 10)
                .AddTile<AreusFabricator>()
                .Register();
        }

        public override bool CanUseItem(Player player)
        {
            if (player.Shards().Overdrive)
            {
                Item.noUseGraphic = true;
                Item.noMelee = true;
            }
            else
            {
                Item.noUseGraphic = false;
                Item.noMelee = false;
            }
            return base.CanUseItem(player);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.Shards().Overdrive)
            {
                type = ModContent.ProjectileType<ElecKatana>();
                velocity.Normalize();
            }
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }

        public override bool? UseItem(Player player)
        {
            if (player.Shards().Overdrive)
            {
                Item.FixSwing(player);
                return true;
            }
            return base.UseItem(player);
        }
    }
}