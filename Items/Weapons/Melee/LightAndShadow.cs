using ShardsOfAtheria.Projectiles.Melee.BalanceSwords;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class LightAndShadow : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddDamageType(6, 10);
            Item.AddElement(0);
            Item.AddElement(2);
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 68;
            Item.scale = 1.5f;

            Item.damage = 72;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6;
            Item.crit = 4;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 6;
            Item.rare = ItemDefaults.RarityEarlyHardmode;
            Item.value = Item.sellPrice(0, 1, 50);
            Item.shoot = ModContent.ProjectileType<LightInShadow>();
        }

        public override bool MeleePrefix()
        {
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<LightWithinDarkness>())
                .AddIngredient(ModContent.ItemType<DarknessWithinLight>())
                .Register();
        }

        public override bool ConsumeItem(Player player)
        {
            return false;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            Item.shoot = ModContent.ProjectileType<LightInShadow>();
            if (player.altFunctionUse == 2)
            {
                Item.shoot = ModContent.ProjectileType<ShadowInLight>();
            }
            return base.CanUseItem(player);
        }

        public override bool? UseItem(Player player)
        {
            Item.FixSwing(player);
            return true;
        }
    }
}