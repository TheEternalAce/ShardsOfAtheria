using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace SagesMania.Items.Weapons.Melee
{
    public class CrossDagger : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A blade that heals 100 Life after striking an enemy\n" +
                "''SOUL STEAL!''");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.damage = 26;
            item.melee = true;
            item.crit = 6;
            item.knockBack = 6;
            item.value = Item.sellPrice(gold: 10);
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("SM:GoldBars", 7);
            recipe.AddIngredient(ItemID.LifeCrystal, 5);
            recipe.AddIngredient(ItemID.ManaCrystal, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override int ChoosePrefix(UnifiedRandom rand)
        {
            return base.ChoosePrefix(rand);
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            if (!player.GetModPlayer<SMPlayer>().heartBreak)
            {
                player.statLife += 100;
                CombatText.NewText(player.Hitbox, Color.Green, 100);
            }
        }

        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<SMPlayer>().sMHealingItem = true;
        }
    }
}