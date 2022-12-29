using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Projectiles.Weapon.Areus.AreusTwinSabers;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Areus
{
    public class AreusTwinSabers : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            SoAGlobalItem.AreusWeapon.Add(Type);
            SoAGlobalItem.Eraser.Add(Type);
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 46;

            Item.SetWeaponValues(76, 5, 6);
            Item.DamageType = DamageClass.Melee;

            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shoot = ModContent.ProjectileType<AreusSaberScissors>();
            Item.shootSpeed = 1;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(1);
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.damage = 152;

                Item.useTime = 50;
                Item.useAnimation = 50;

                Item.shoot = ModContent.ProjectileType<AreusSaberScissors>();
            }
            else
            {
                Item.damage = 76;

                Item.useTime = 24;
                Item.useAnimation = 24;

                Item.shoot = ModContent.ProjectileType<AreusSaberTwin>();
            }
            return base.CanUseItem(player);
        }

        public override void AddRecipes()
        {

        }
    }
}
