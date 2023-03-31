using Microsoft.Xna.Framework;
using MMZeroElements;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Weapon.Areus.AreusSaber;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Areus
{
    public class AreusSaber : OverchargeWeapon
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            WeaponElements.Electric.Add(Type);
            SoAGlobalItem.AreusWeapon.Add(Type);
            SoAGlobalItem.Eraser.Add(Type);
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 54;

            Item.damage = 246;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 4;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 4f;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(0, 4, 57);
            Item.shoot = ModContent.ProjectileType<AreusSlash1>();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            Item.useTime = Item.useAnimation = 20;
            if (player.altFunctionUse == 2 || player.GetModPlayer<OverchargePlayer>().overcharged)
            {
                if (player.GetModPlayer<OverchargePlayer>().overcharged)
                {
                    Item.useTime = Item.useAnimation = 40;
                }
                Item.shoot = ModContent.ProjectileType<AreusSaberProj>();
            }
            else
            {
                Item.shoot = ModContent.ProjectileType<AreusSlash1>();
            }
            return base.CanUseItem(player);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddCondition(NetworkText.FromKey("Mods.ShardsOfAtheria.RecipeConditions.Upgrade"), r => false)
                .AddIngredient(ModContent.ItemType<AreusDagger>())
                .AddIngredient(ModContent.ItemType<AreusSword>())
                .AddIngredient(ItemID.LunarBar, 14)
                .Register();
        }

        public override void Overcharge(Player player, int projType, float damageMultiplier, Vector2 velocity, float ai1 = 0)
        {
            ConsumeOvercharge(player);
        }
    }
}