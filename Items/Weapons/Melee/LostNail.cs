using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Projectiles;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class LostNail : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'The nail wielded by a certain lost vessel'");
        }

        public override void SetDefaults()
        {
            Item.damage = 125;
            Item.DamageType = DamageClass.Melee;
            Item.width = 52;
            Item.height = 52;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = Item.sellPrice(gold: 15);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.crit = 30;
            Item.shoot = ModContent.ProjectileType<InfectionBlob>();
            Item.shootSpeed = 15;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<BrokenNail>())
                .AddIngredient(ItemID.BrokenHeroSword)
                .AddIngredient(ItemID.FragmentSolar, 9)
                .AddTile(TileID.LunarCraftingStation)
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