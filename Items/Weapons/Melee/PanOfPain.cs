using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Tiles;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class PanOfPain : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Lowers enemy defense and sets enemies a blaze\n" +
                "'BONK!'");
        }

        public override void SetDefaults()
        {
            Item.damage = 72;
            Item.DamageType = DamageClass.Melee;
            Item.width = 42;
            Item.height = 42;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 7;
            Item.value = Item.sellPrice(0,  10);
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.crit = 21;
            Item.shootSpeed = 15;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(SoARecipes.Cobalt, 15)
                .AddIngredient(ItemID.HellstoneBar, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            // Add the Onfire buff to the NPC for 1 second when the weapon hits an NPC
            // 60 frames = 1 second
            target.AddBuff(BuffID.OnFire, 10*60);
            target.AddBuff(ModContent.BuffType<Bonked>(), 10*60);
        }
    }
}