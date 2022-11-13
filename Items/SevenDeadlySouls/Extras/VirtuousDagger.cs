using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Materials;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SevenDeadlySouls.Extras
{
    public class VirtuousDagger : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Cleanses your soul of sin\n" +
                "Shatters on use");

            SacrificeTotal = 5;
        }

        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 52;
            Item.consumable = true;
            Item.maxStack = 9999;
            Item.scale = .75f;

            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item27;
        }

        public override bool CanUseItem(Player player)
        {
            if (!(player.HasBuff(SevenSoulPlayer.SinfulBuffs[0]) || player.HasBuff(SevenSoulPlayer.SinfulBuffs[1]) || player.HasBuff(SevenSoulPlayer.SinfulBuffs[2])
                || player.HasBuff(SevenSoulPlayer.SinfulBuffs[3]) || player.HasBuff(SevenSoulPlayer.SinfulBuffs[4]) || player.HasBuff(SevenSoulPlayer.SinfulBuffs[5])
                || player.HasBuff(SevenSoulPlayer.SinfulBuffs[6])))
            {
                return false;
            }
            return !player.immune && player.immuneTime == 0;
        }

        public override bool? UseItem(Player player)
        {
            player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " was purified into nothing."), player.statLifeMax2 / 5, -player.direction);
            for (int i = 0; i < SevenSoulPlayer.SinfulBuffs.Length; i++)
            {
                if (player.HasBuff(SevenSoulPlayer.SinfulBuffs[i]))
                {
                    player.ClearBuff(SevenSoulPlayer.SinfulBuffs[i]);
                    player.AddBuff(ModContent.BuffType<VirtuousSoul>(), 1800);
                }
            }
            player.GetModPlayer<SevenSoulPlayer>().SevenSoulUsed = 0;

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
