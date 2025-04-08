using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Projectiles.Melee.EnergyScythe;
using ShardsOfAtheria.Projectiles.Ranged;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons
{
    public class Prometheus : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
            ItemID.Sets.IsRangedSpecialistWeapon[Type] = true;
            Item.AddDamageType(3, 4);
            Item.AddElement(0);
            Item.AddElement(2);
            Item.AddRedemptionElement(2);
            Item.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 52;
            Item.scale = 1.4f;

            Item.damage = 112;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 13;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shoot = ModContent.ProjectileType<EnergyScythe>();
            Item.shootSpeed = 1;
            Item.value = Item.sellPrice(0, 3, 25);
            Item.rare = ItemDefaults.RarityHardmodeDungeon;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<BionicBarItem>(), 20)
                .AddIngredient(ItemID.Ectoplasm, 5)
                .AddIngredient(ItemID.HellstoneBar, 10)
                .AddIngredient(ModContent.ItemType<SoulOfSpite>(), 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override bool AltFunctionUse(Player player) => true;

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useTime = 17;
                Item.useAnimation = 17;
                Item.damage = 97;
                Item.DamageType = DamageClass.Ranged;
                Item.knockBack = 6;
                Item.UseSound = SoundID.Item20;
                Item.shoot = ModContent.ProjectileType<PrometheusFire>();
                Item.shootSpeed = 13f;
            }
            else
            {
                Item.useTime = 24;
                Item.useAnimation = 24;
                Item.damage = 112;
                Item.DamageType = DamageClass.Melee;
                Item.knockBack = 13;
                Item.UseSound = SoundID.Item71;
                Item.shoot = ModContent.ProjectileType<EnergyScythe>();
                Item.shootSpeed = 1;
            }
            return base.CanUseItem(player);
        }
    }
}