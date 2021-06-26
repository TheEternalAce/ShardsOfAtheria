using Microsoft.Xna.Framework;
using SagesMania.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace SagesMania.Items.Weapons
{
    public class Prometheus : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Left Click to swing an energy scythe, <right> to throw a phantom scythe\n" +
                "''It seems like you're worthy of playing his little game, his game of destiny!''");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.value = Item.sellPrice(gold: 10);
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("SM:GoldBars", 7);
            recipe.AddIngredient(ItemID.Ectoplasm, 5);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.noMelee = true;
                item.noUseGraphic = true;
                item.useTime = 17;
                item.useAnimation = 17;
                item.damage = 97;
                item.ranged = true;
                item.knockBack = 6;
                item.mana = 15;
                item.shoot = ModContent.ProjectileType<PhantomScythe>();
                item.shootSpeed = 13f;
                item.useTurn = false;
            }
            else
            {
                item.noMelee = false;
                item.noUseGraphic = false;
                item.useTime = 30;
                item.useAnimation = 30;
                item.damage = 112;
                item.melee = true;
                item.knockBack = 13;
                item.mana = 0;
                item.shoot = ProjectileID.None;
                item.useTurn = true;
            }
            return base.CanUseItem(player);
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 10 * 60);
        }
    }
}