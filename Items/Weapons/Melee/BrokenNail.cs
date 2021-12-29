using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Projectiles;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Tiles;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class BrokenNail : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infected Nail"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("'The nail wielded by a certain broken vessel'");
        }

        public override void SetDefaults()
        {
            Item.damage = 97;
            Item.DamageType = DamageClass.Melee;
            Item.width = 42;
            Item.height = 42;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.crit = 21;
            Item.shoot = ModContent.ProjectileType<InfectionBlob>();
            Item.shootSpeed = 15;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup("SM:Tier3Bars", 15)
                .AddIngredient(ModContent.ItemType<CrystalInfection>(), 15)
                .AddTile(ModContent.TileType<CobaltWorkbench>())
                .Register();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            // Add the Onfire buff to the NPC for 1 second when the weapon hits an NPC
            // 60 frames = 1 second
            target.AddBuff(ModContent.BuffType<Infection>(), 10*60);
        }
    }
}