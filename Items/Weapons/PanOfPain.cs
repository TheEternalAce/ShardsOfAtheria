using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Buffs;
using SagesMania.Projectiles;
using SagesMania.Items.Placeable;

namespace SagesMania.Items.Weapons
{
    public class PanOfPain : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Lowers enemy defense and sets enemies a blaze\n" +
                "''BONK!''");
        }

        public override void SetDefaults()
        {
            item.damage = 72;
            item.melee = true;
            item.width = 42;
            item.height = 42;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 7;
            item.value = Item.sellPrice(gold: 10);
            item.rare = ItemRarityID.LightRed;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            item.crit = 21;
            item.shootSpeed = 15;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("SM:CobaltBars", 15);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
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