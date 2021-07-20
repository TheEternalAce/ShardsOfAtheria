using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Buffs;
using SagesMania.Projectiles;

namespace SagesMania.Items.Weapons.Melee
{
    public class LostNail : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'The nail wielded by a certain lost vessel'");
        }

        public override void SetDefaults()
        {
            item.damage = 125;
            item.melee = true;
            item.width = 52;
            item.height = 52;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 6;
            item.value = Item.sellPrice(gold: 15);
            item.rare = ItemRarityID.Orange;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.crit = 30;
            item.shoot = ModContent.ProjectileType<InfectionBlob>();
            item.shootSpeed = 15;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<BrokenNail>());
            recipe.AddIngredient(ItemID.BrokenHeroSword);
            recipe.AddIngredient(ItemID.FragmentSolar, 9);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            // Add the Onfire buff to the NPC for 1 second when the weapon hits an NPC
            // 60 frames = 1 second
            target.AddBuff(ModContent.BuffType<Infection>(), 10*60);
        }
    }
}