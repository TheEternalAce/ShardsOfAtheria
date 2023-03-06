using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MMZeroElements;
using ReLogic.Content;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Projectiles.Tools;
using ShardsOfAtheria.Systems;
using ShardsOfAtheria.Tiles.Crafting;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Tools.Mining
{
    public class RedAreusDrill : ModItem
    {
        public static Asset<Texture2D> glowmask;

        public override void Unload()
        {
            glowmask = null;
        }

        public override void SetStaticDefaults()
        {
            ItemID.Sets.IsDrill[Type] = true;

            if (!Main.dedServ)
            {
                glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
            }

            SacrificeTotal = 1;
            SoAGlobalItem.AreusWeapon.Add(Type);
            WeaponElements.Fire.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 30;

            Item.pick = 225;

            Item.damage = 50;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 0;
            Item.tileBoost += 3;

            Item.useTime = 2; //Actual Break 1 = FAST 50 = SUPER SLOW
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item23;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;

            Item.shootSpeed = 32;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(0, 4, 75);
            Item.shoot = ModContent.ProjectileType<RedAreusDrillProj>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 20)
                .AddRecipeGroup(ShardsRecipes.Gold, 6)
                .AddIngredient(ItemID.LunarBar, 10)
                .AddIngredient(ItemID.Wire, 10)
                .AddTile(ModContent.TileType<AreusFabricator>())
                .Register();
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Item.BasicInWorldGlowmask(spriteBatch, glowmask.Value, new Color(255, 255, 255, 50) * 0.7f, rotation, scale);
        }
    }
}