using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Projectiles.Magic.Sigil;
using ShardsOfAtheria.Tiles.Crafting;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
    public class AreusSigil : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            Item.AddAreus();
            Item.AddElementElec();
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 36;

            Item.damage = 130;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 3.75f;
            Item.crit = 16;
            Item.mana = 24;

            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item43;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;

            Item.shootSpeed = 12f;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(0, 4, 25);
            Item.shoot = ModContent.ProjectileType<LightningSigil>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 10)
                .AddIngredient(ItemID.GoldBar, 3)
                .AddIngredient(ItemID.FragmentVortex, 7)
                .AddTile<AreusFabricator>()
                .Register();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = new Vector2(25, 0);
        }
    }
}