using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Areus
{
    public class AreusRailgun : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            SoAGlobalItem.AreusWeapon.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 26;

            Item.damage = 100;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 20f;
            Item.crit = 6;

            Item.useTime = 48;
            Item.useAnimation = 48;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item38;
            Item.noMelee = true;

            Item.shootSpeed = 32f;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(0, 2, 25);
            Item.shoot = ItemID.PurificationPowder;
            Item.useAmmo = AmmoID.Bullet;
            Item.ArmorPenetration = 20;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 20)
                .AddIngredient(ItemID.SoulofMight, 7)
                .AddIngredient(ItemID.SoulofFright, 7)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, 0);
        }

        public override bool? UseItem(Player player)
        {
            EffectsSystem.Shake.Set(8f);
            return null;
        }
    }
}