using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using ShardsOfAtheria.Projectiles.Weapon.Summon.Whip;

namespace ShardsOfAtheria.Items.SlayerItems.SlayersEquipment
{
    public class DragonSpineWhip : SlayerItem
    {
        public override void SetStaticDefaults()
        {

        }

        public override void SetDefaults()
        {
            Item.autoReuse = false;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = Item.useAnimation = 40;
            Item.width = 18;
            Item.height = 18;
            Item.shoot = ModContent.ProjectileType<DragonSpineWhipProj>();
            Item.UseSound = SoundID.Item152;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Summon;
            Item.noUseGraphic = true;
            Item.damage = 220;
            Item.knockBack = 2f;
            Item.shootSpeed = 5f;
            Item.rare = ModContent.RarityType<SlayerRarity>();
            Item.value = Item.sellPrice(0, 10, 0, 0);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<FragmentEntropy>(), 64)
                .AddIngredient(ItemID.FragmentStardust, 32)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}