using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Globals.Elements;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Projectiles.Weapon.Areus.AreusGlaive;
using ShardsOfAtheria.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Areus
{
    public class AreusGlaive : ModItem
    {
        public int combo = 0;
        public int comboTimer;

        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            SoAGlobalItem.AreusWeapon.Add(Type);
            WeaponElements.ElectricWeapon.Add(Type);
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
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(0, 1);
            Item.shoot = ModContent.ProjectileType<AreusGlaive_Swing>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 16)
                .AddRecipeGroup(ShardsRecipes.Gold, 6)
                .AddIngredient(ModContent.ItemType<SoulOfDaylight>(), 10)
                .AddTile(TileID.Hellforge)
                .Register();
        }

        public override void HoldItem(Player player)
        {
            int comboTimerMax = 60;
            // If the item is not being used, continue
            if (player.itemAnimation == 0)
            {
                // If combo is not the spear, decrement the timer
                if (combo > 0)
                {
                    comboTimer--;
                }

                // If the timer is 0, reset the timer and combo
                if (comboTimer == 0)
                {
                    comboTimer = comboTimerMax;
                    combo = 0;
                }
            }
            else comboTimer = comboTimerMax;
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
                    Item.shootSpeed = 4.5f;
                    Item.UseSound = SoundID.DD2_MonkStaffSwing;
                    break;
                case 3:
                    Item.shoot = ModContent.ProjectileType<AreusGlaive_Thrust2>();
                    Item.shootSpeed = 4.5f;
                    Item.UseSound = SoundID.DD2_MonkStaffSwing;
                    break;
                case 4:
                    Item.shoot = ModContent.ProjectileType<AreusGlaive_Throw>();
                    Item.shootSpeed = 30f;
                    Item.UseSound = SoundID.Item71;
                    break;
            }
            return player.ownedProjectileCounts[ModContent.ProjectileType<AreusGlaive_Swing>()] < 1 && player.ownedProjectileCounts[ModContent.ProjectileType<AreusGlaive_Thrust>()] < 1
                    && player.ownedProjectileCounts[ModContent.ProjectileType<AreusGlaive_Thrust2>()] < 1 && player.ownedProjectileCounts[ModContent.ProjectileType<AreusGlaive_Throw>()] < 1;
        }

        public override bool? UseItem(Player player)
        {
            if (combo >= 4)
                combo = 0;
            else combo++;
            return true;
        }
    }
}