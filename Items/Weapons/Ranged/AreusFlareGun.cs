using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Projectiles.Ranged;
using ShardsOfAtheria.Tiles.Crafting;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class AreusFlareGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.IsRangedSpecialistWeapon[Type] = true;
            Item.AddAreus();
            Item.AddDamageType(3, 5);
            Item.AddElement(0);
            Item.AddRedemptionElement(2);
        }

        public override void SetDefaults()
        {
            Item.width = 52;
            Item.height = 22;

            Item.damage = 40;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 0f;

            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.noMelee = true;

            Item.shootSpeed = 16f;
            Item.rare = ItemDefaults.RarityEarlyHardmode;
            Item.value = 180000;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.useAmmo = AmmoID.Flare;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 12)
                .AddIngredient(ItemID.GoldBar, 6)
                .AddIngredient(ItemID.SoulofNight, 8)
                .AddIngredient(ItemID.Ichor, 14)
                .AddTile<AreusFabricator>()
                .Register();
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 12)
                .AddIngredient(ItemID.GoldBar, 6)
                .AddIngredient(ItemID.SoulofNight, 8)
                .AddIngredient(ItemID.CursedFlame, 14)
                .AddTile<AreusFabricator>()
                .Register();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            int convertibleFlare = -1;
            if (ShardsHelpers.TryGetModContent("NotEnoughFlareGuns", "ConvertibleFlare", out ModItem flare)) convertibleFlare = flare.Type;
            if (type == ProjectileID.BlueFlare || type == convertibleFlare) type = ModContent.ProjectileType<AreusFlare>();
        }
    }
}