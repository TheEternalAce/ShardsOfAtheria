using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Tiles;
using ShardsOfAtheria.Projectiles.Weapon;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class Ragnarok : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Holding left click brings up a shield that increases defense by 16\n" +
                "Releasing left click throws the shield which can go through tiles");
        }

        public override void SetDefaults()
        {
            Item.damage = 100;
            Item.DamageType = DamageClass.Melee;
            Item.width = 70;
            Item.height = 70;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 6;
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item15;
            Item.autoReuse = false;
            Item.crit = 6;

            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<Ragnarok_Shield>();
            Item.shootSpeed = 0;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[ModContent.ProjectileType<RagnarokProj>()] < 1 && player.ownedProjectileCounts[ModContent.ProjectileType<Ragnarok_Shield>()] < 1;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.FragmentSolar, 9)
                .AddIngredient(ItemID.FragmentVortex, 9)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}