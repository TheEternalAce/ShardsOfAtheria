using MMZeroElements;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Projectiles.Weapon.Throwing;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Throwing
{
    public class HardlightKnife : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 999;

            WeaponElements.Metal.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 50;
            Item.consumable = true;
            Item.maxStack = 9999;

            Item.damage = 15;
            Item.DamageType = DamageClass.Throwing;
            Item.knockBack = 1;
            Item.crit = 6;

            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 16f;
            Item.rare = ItemRarityID.Green;
            Item.value = 6800;
            Item.shoot = ModContent.ProjectileType<HardlightKnifeProj>();
        }

        public override void AddRecipes()
        {
            CreateRecipe(300)
                .AddIngredient(ModContent.ItemType<ChargedFeather>(), 5)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}