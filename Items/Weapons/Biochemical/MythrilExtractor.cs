using ShardsOfAtheria.Projectiles.Weapon;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Biochemical
{
    public class MythrilExtractor : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Extract microbes from your enemies");
        }

        public override void SetDefaults()
        {
            Item.width = 8;
            Item.height = 20;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.damage = 50;
            Item.DamageType = ModContent.GetInstance<BiochemicalDamage>();
            Item.crit = 4;
            Item.knockBack = 6;
            Item.value = Item.sellPrice(silver: 5, copper: 27);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.MythrilBar, 16)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            Item.NewItem(target.GetSource_OnHurt(player), target.getRect(), ModContent.ItemType<UnanalyzedMicrobe>(), Main.rand.Next(20, 40));
        }

        public override void OnHitPvp(Player player, Player target, int damage, bool crit)
        {
            Item.NewItem(target.GetSource_OnHurt(player), target.getRect(), ModContent.ItemType<UnanalyzedMicrobe>(), Main.rand.Next(20, 40));
        }
    }
}