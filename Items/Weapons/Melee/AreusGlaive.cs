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
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
            Item.channel = true;
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
            switch (combo)
            {
                case 0:
                case 1:
                    Item.shoot = ModContent.ProjectileType<AreusGlaive_Swing>();
                    Item.shootSpeed = 1f;
                    Item.UseSound = SoundID.Item1;
                    break;
                case 2:
                    Item.shoot = ModContent.ProjectileType<AreusGlaive_Thrust>();
                    Item.shootSpeed = 5.5f;
                    Item.UseSound = SoundID.DD2_MonkStaffSwing;
                    break;
                case 3:
                    Item.shoot = ModContent.ProjectileType<AreusGlaive_Thrust2>();
                    Item.shootSpeed = 5f;
                    Item.UseSound = SoundID.DD2_MonkStaffSwing;
                    break;
                case 4:
                    Item.shoot = ModContent.ProjectileType<AreusGlaive_Throw>();
                    Item.shootSpeed = 16f;
                    Item.UseSound = SoundID.Item71;
                    break;
            }
            return player.ownedProjectileCounts[ModContent.ProjectileType<AreusGlaive_Swing>()] < 1 && player.ownedProjectileCounts[ModContent.ProjectileType<AreusGlaive_Thrust>()] < 1
                    && player.ownedProjectileCounts[ModContent.ProjectileType<AreusGlaive_Thrust2>()] < 1 && player.ownedProjectileCounts[ModContent.ProjectileType<AreusGlaive_Throw>()] < 1;
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