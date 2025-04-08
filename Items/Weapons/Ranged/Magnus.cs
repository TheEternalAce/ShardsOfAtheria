using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Items.SinfulSouls;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Ranged;
using ShardsOfAtheria.ShardsConditions;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class Magnus : SinfulItem
    {
        public override int RequiredSin => SinfulPlayer.Pride;

        public override void SetStaticDefaults()
        {
            Item.AddDamageType(5, 11);
            Item.AddElement(2);
            Item.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 28;
            Item.scale = .85f;

            Item.damage = 26;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 4f;
            Item.crit = 5;

            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item41;
            Item.noMelee = true;

            Item.shootSpeed = 0f;
            Item.rare = ItemDefaults.RaritySinful;
            Item.value = Item.sellPrice(0, 10, 25);
            Item.shoot = ModContent.ProjectileType<AmbassadorBeam>();
            Item.useAmmo = AmmoID.Bullet;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            type = Item.shoot;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<PrideSoul>()
                .AddCondition(SoAConditions.TransformArmament)
                .Register();
        }
    }
}