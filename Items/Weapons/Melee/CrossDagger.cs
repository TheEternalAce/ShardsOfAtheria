using MMZeroElements;
using ShardsOfAtheria.Utilities;
using ShardsOfAtheria.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class CrossDagger : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            WeaponElements.Metal.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;

            Item.damage = 26;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6;
            Item.crit = 6;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;

            Item.value = Item.sellPrice(0, 15);
            Item.rare = ItemRarityID.Red;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(ShardsRecipes.Gold, 7)
                .AddIngredient(ItemID.LifeCrystal, 5)
                .AddIngredient(ItemID.ManaCrystal, 5)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override bool AllowPrefix(int pre)
        {
            return false;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            if (!player.ShardsOfAtheria().heartBreak)
            {
                player.Heal(100);
            }
        }

        public override void UpdateInventory(Player player)
        {
            player.ShardsOfAtheria().healingItem = true;
        }
    }
}