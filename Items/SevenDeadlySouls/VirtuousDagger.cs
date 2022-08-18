using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SevenDeadlySouls
{
    public class VirtuousDagger : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Cleanses your soul of sin\n" +
                "Shatters on use");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 5;
        }

        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 52;
            Item.consumable = true;
            Item.maxStack = 30;
            Item.scale = .75f;

            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item27;
        }

        public override bool? UseItem(Player player)
        {
            player.Hurt(PlayerDeathReason.ByPlayer(player.whoAmI), player.statLifeMax2 / 5, -player.direction);
            player.ClearBuff(ModContent.BuffType<EnvyBuff>());
            player.ClearBuff(ModContent.BuffType<GluttonyBuff>());
            player.ClearBuff(ModContent.BuffType<GreedBuff>());
            player.ClearBuff(ModContent.BuffType<LustBuff>());
            player.ClearBuff(ModContent.BuffType<PrideBuff>());
            player.ClearBuff(ModContent.BuffType<SlothBuff>());
            player.ClearBuff(ModContent.BuffType<WrathBuff>());
            SevenSoulPlayer.SevenSoulUsed = 0;

            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(player.position, player.width, player.height, DustID.WhiteTorch, 0, 0, 0, Color.White, 2);
            }
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SoulOfDaylight>(), 15)
                .AddIngredient(ModContent.ItemType<SoulOfTwilight>(), 15)
                .AddIngredient(ItemID.PurificationPowder, 15)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
