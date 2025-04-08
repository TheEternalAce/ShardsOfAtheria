using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Projectiles.Melee.EntropyCutter;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class EntropySlicer : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddDamageType(2);
            Item.AddElement(1);
            Item.AddRedemptionElement(3);
        }

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 54;

            Item.damage = 200;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 4;
            Item.crit = 8;

            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item71;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 15f;
            Item.rare = ItemDefaults.RaritySlayer;
            Item.shoot = ModContent.ProjectileType<EntropyBlade>();
        }

        public override bool MeleePrefix()
        {
            return true;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (player.timeSinceLastDashStarted < 30) damage += .5f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<FragmentEntropy>(), 64)
                .AddIngredient(ItemID.FragmentSolar, 32)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }

        public override bool? UseItem(Player player)
        {
            Item.FixSwing(player);
            return true;
        }
    }
}