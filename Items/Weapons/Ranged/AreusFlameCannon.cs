using BattleNetworkElements.Utilities;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Weapon.Ranged.FireCannon;
using ShardsOfAtheria.ShardsConditions;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class AreusFlameCannon : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            Item.AddAreus();
            Item.AddFire();
        }

        public override void SetDefaults()
        {
            Item.width = 62;
            Item.height = 34;

            Item.damage = 100;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 5f;
            Item.crit = 4;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.channel = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 0f;
            Item.rare = ItemRarityID.Master;
            Item.value = Item.sellPrice(0, 2, 75);
            Item.shoot = ModContent.ProjectileType<FlameCannon>();
            Item.useAmmo = AmmoID.Gel;
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return false;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            type = ModContent.ProjectileType<FlameCannon>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusRailgun>())
                .AddIngredient(ItemID.Flamethrower)
                .AddIngredient(ItemID.BeetleHusk, 14)
                .AddCondition(SoAConditions.Upgrade)
                .Register();
        }
    }
}