using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SagesMania.Items.Weapons.Melee
{
    public class Shinai : SpecialItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Bonk, go to horny jail'");
        }

        public override void SetDefaults()
        {
            item.damage = 20;
            item.melee = true;
            item.width = 50;
            item.height = 50;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 6;
            item.rare = ItemRarityID.White;
            item.value = Item.sellPrice(silver: 6);
            item.UseSound = SoundID.Item1;
            item.crit = 4;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup(RecipeGroupID.Wood, 17);
            recipe.AddRecipeGroup("SM:CopperBars", 2);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            CombatText.NewText(target.Hitbox, Color.White, "Bonk!");
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Bonk"));
        }
    }
}