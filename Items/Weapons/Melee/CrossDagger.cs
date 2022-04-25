using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class CrossDagger : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A blade that heals 100 Life after striking an enemy\n" +
                "'SOUL STEAL!'");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.damage = 26;
            Item.DamageType = DamageClass.Melee;
            Item.crit = 6;
            Item.knockBack = 6;
            Item.value = Item.sellPrice(0,  10);
            Item.rare = ItemRarityID.Red;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(SoARecipes.Gold, 7)
                .AddIngredient(ItemID.LifeCrystal, 5)
                .AddIngredient(ItemID.ManaCrystal, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override int ChoosePrefix(UnifiedRandom rand)
        {
            return base.ChoosePrefix(rand);
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            if (!player.GetModPlayer<SoAPlayer>().heartBreak)
            {
                player.statLife += 100;
                CombatText.NewText(player.Hitbox, Color.Green, 100);
            }
        }

        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<SoAPlayer>().sMHealingItem = true;
        }
    }
}