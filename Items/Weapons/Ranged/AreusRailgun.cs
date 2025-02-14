using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Projectiles.Ranged;
using ShardsOfAtheria.Tiles.Crafting;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class AreusRailgun : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.IsRangedSpecialistWeapon[Type] = true;
            Item.AddAreus();
        }

        public override void SetDefaults()
        {
            Item.width = 76;
            Item.height = 26;

            Item.damage = 100;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 7f;
            Item.crit = 6;

            Item.useTime = 48;
            Item.useAnimation = 48;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item61;
            Item.channel = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 0f;
            Item.rare = ItemDefaults.RarityMechs;
            Item.value = Item.sellPrice(0, 2, 25);
            Item.shoot = ModContent.ProjectileType<AreusRailgunProj>();
            Item.useAmmo = AmmoID.Dart;
            Item.ArmorPenetration = 20;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 20)
                .AddIngredient(ItemID.GoldBar, 6)
                .AddIngredient(ItemID.SoulofMight, 7)
                .AddTile<AreusFabricator>()
                .Register();
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return false;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            type = Item.shoot;
        }
    }
}