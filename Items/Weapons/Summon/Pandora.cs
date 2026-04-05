using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Projectiles.Summon.Active;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Summon
{
    public class Pandora : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddDamageType(2, 5);
            Item.AddElement(1);
            Item.AddElement(2);
            Item.AddRedemptionElement(4);
            Item.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;

            Item.damage = 77;
            Item.DamageType = DamageClass.Summon;
            Item.knockBack = 0f;
            Item.mana = 10;

            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item43;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.staff[Item.type] = true;

            Item.shootSpeed = 1f;
            Item.shoot = ModContent.ProjectileType<FrostsparkBeam>();
            Item.value = Item.sellPrice(0, 3, 25);
            Item.rare = ItemDefaults.RarityHardmodeDungeon;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<BionicBarItem>(), 20)
                .AddIngredient(ItemID.Ectoplasm, 5)
                .AddIngredient(ItemID.IceBlock, 10)
                .AddIngredient(ModContent.ItemType<SoulOfSpite>(), 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}