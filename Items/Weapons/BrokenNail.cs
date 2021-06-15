using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Buffs;
using SagesMania.Projectiles;
using SagesMania.Items.Placeable;

namespace SagesMania.Items.Weapons
{
    public class BrokenNail : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infected Nail"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("The nail wielded by a certain broken vessel.");
        }

        public override void SetDefaults()
        {
            item.damage = 97;
            item.melee = true;
            item.width = 42;
            item.height = 42;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 6;
            item.value = Item.sellPrice(gold: 10);
            item.rare = ItemRarityID.Orange;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            item.crit = 21;
            item.shoot = ModContent.ProjectileType<InfectionBlob>();
            item.shootSpeed = 15;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("SM:AdamantiteBars", 15);
            recipe.AddIngredient(ModContent.ItemType<CrystalInfection>(), 15);
            recipe.AddTile(TileID.MythrilAnvil);
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