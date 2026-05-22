using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Projectiles.Melee.AreusGlaive;
using ShardsOfAtheria.Tiles.Crafting;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class AreusGlaive : ModItem
    {
        public int combo = 0;
        public int comboTimer;

        public override void SetStaticDefaults()
        {
            ItemID.Sets.Spears[Type] = true;
            Item.AddAreus();
            Item.AddDamageType(11);
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 54;

            Item.damage = 36;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 1f;
            Item.rare = ItemDefaults.RarityDungeon;
            Item.value = Item.sellPrice(0, 1);
            Item.shoot = ModContent.ProjectileType<AreusGlaive_Swing>();
        }

        public override bool MeleePrefix()
        {
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 10)
                .AddIngredient(ItemID.GoldBar, 5)
                .AddIngredient(ModContent.ItemType<SoulOfDaylight>(), 10)
                .AddTile<AreusFabricator>()
                .Register();
        }

        public override bool CanUseItem(Player player)
        {
            int swing = ModContent.ProjectileType<AreusGlaive_Swing>();
            int thrust = ModContent.ProjectileType<AreusGlaive_Thrust>();
            int thrustBack = ModContent.ProjectileType<AreusGlaive_ThrustBackwards>();
            int spearThrow = ModContent.ProjectileType<AreusGlaive_Throw>();
            if (player.altFunctionUse == 2) combo = 4;
            switch (combo)
            {
                case 0:
                case 1:
                    Item.shoot = swing;
                    Item.shootSpeed = 1f;
                    Item.UseSound = SoundID.Item1;
                    Item.useStyle = ItemUseStyleID.Swing;
                    break;
                case 2:
                    Item.shoot = thrust;
                    SetSpearDefaults();
                    break;
                case 3:
                    Item.shoot = thrustBack;
                    SetSpearDefaults();
                    break;
                case 4:
                    Item.shoot = spearThrow;
                    Item.shootSpeed = 16f;
                    Item.UseSound = SoundID.Item71;
                    Item.useStyle = ItemUseStyleID.Swing;
                    break;
            }
            void SetSpearDefaults()
            {
                Item.shootSpeed = 5.5f;
                Item.UseSound = SoundID.DD2_MonkStaffSwing;
                Item.useStyle = ItemUseStyleID.Shoot;
            }
            return player.ownedProjectileCounts[swing] < 1 && player.ownedProjectileCounts[thrust] < 1 &&
                player.ownedProjectileCounts[spearThrow] < 1;
        }

        public override bool? UseItem(Player player)
        {
            if (combo == 0 || combo == 1) Item.FixSwing(player);
            if (combo >= 4)
                combo = 0;
            else combo++;
            return true;
        }
    }
}