using MMZeroElements.Utilities;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.ModCondition;
using ShardsOfAtheria.Projectiles.Weapon.Melee.AreusSaber;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class AreusSaber : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            Item.AddElecDefault();
            SoAGlobalItem.AreusWeapon.Add(Type);
            SoAGlobalItem.Eraser.Add(Type);
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 54;

            Item.damage = 246;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 4;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 4f;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(0, 4, 57);
            Item.shoot = ModContent.ProjectileType<AreusSlash1>();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddCondition(SoAConditions.Upgrade)
                .AddIngredient(ModContent.ItemType<AreusDagger>())
                .AddIngredient(ModContent.ItemType<AreusSword>())
                .AddIngredient(ItemID.LunarBar, 14)
                .Register();
        }
    }
}