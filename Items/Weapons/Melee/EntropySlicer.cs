using MMZeroElements;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Projectiles.Weapon.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class EntropySlicer : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            WeaponElements.Ice.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 54;

            Item.damage = 200;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 4;
            Item.crit = 8;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item71;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 15f;
            Item.rare = ItemRarityID.Yellow;
            Item.shoot = ModContent.ProjectileType<EntropyBlade>();
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[ModContent.ProjectileType<EntropyBlade>()] < 1;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            base.ModifyWeaponDamage(player, ref damage);
            if (player.timeSinceLastDashStarted < 30)
            {
                damage += .5f;
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<FragmentEntropy>(), 64)
                .AddIngredient(ItemID.FragmentSolar, 32)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}