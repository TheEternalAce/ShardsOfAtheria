using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Projectiles.Ranged;
using ShardsOfAtheria.Tiles.Crafting;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class AreusGrenade : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 999;
            ItemID.Sets.IsRangedSpecialistWeapon[Type] = true;
            Item.AddAreus();
            Item.AddElement(0);
            Item.AddRedemptionElement(2);
            Item.AddRedemptionElement(15);
        }

        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 56;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.ammo = ItemID.Grenade;

            Item.damage = 50;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 7;
            Item.crit = 6;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;

            Item.shootSpeed = 16f;
            Item.value = 10000;
            Item.rare = ItemDefaults.RarityDungeon;
            Item.shoot = ModContent.ProjectileType<AreusGrenadeProj>();
        }

        public override void AddRecipes()
        {
            CreateRecipe(60)
                .AddIngredient(ModContent.ItemType<AreusShard>(), 3)
                .AddIngredient(ItemID.GoldBar)
                .AddIngredient(ModContent.ItemType<SoulOfSpite>(), 2)
                .AddTile<AreusFabricator>()
                .Register();
        }
    }
}