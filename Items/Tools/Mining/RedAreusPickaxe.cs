using Microsoft.Xna.Framework;
using MMZeroElements.Utilities;
using ShardsOfAtheria.Dusts;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Tools.Mining
{
    public class RedAreusPickaxe : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            SoAGlobalItem.AreusWeapon.Add(Type);
            Item.AddFire();
        }

        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 54;

            Item.pick = 150;

            Item.damage = 20;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6;
            Item.mana = 0;

            Item.useTime = 5; //Actual Break 1 = FAST 50 = SUPER SLOW
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item15;
            Item.autoReuse = true;
            Item.useTurn = true;

            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(0, 1, 25);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 20)
                .AddRecipeGroup(ShardsRecipes.Gold, 6)
                .AddIngredient(ItemID.Wire, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(5))
            {
                Dust.NewDust(hitbox.TopLeft(), hitbox.Width, hitbox.Height, ModContent.DustType<AreusDust_Red>());
            }
        }
    }
}