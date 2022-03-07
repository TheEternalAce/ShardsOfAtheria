using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.Audio;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class Shinai : SpecialItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Bonk, go to horny jail'");
        }

        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.DamageType = DamageClass.Melee;
            Item.width = 50;
            Item.height = 50;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.rare = ItemRarityID.White;
            Item.value = Item.sellPrice(silver: 6);
            Item.UseSound = SoundID.Item1;
            Item.crit = 4;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(RecipeGroupID.Wood, 17)
                .AddRecipeGroup(SoARecipes.Copper, 2)
                .AddTile(TileID.WorkBenches)
                .Register();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            CombatText.NewText(target.Hitbox, Color.White, "Bonk!");
            SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/Item/Bonk"));
        }
    }
}