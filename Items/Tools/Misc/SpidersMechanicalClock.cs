using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.Cooldowns;
using ShardsOfAtheria.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Tools.Misc
{
    public class SpidersMechanicalClock : ModItem
    {
        int saveTimer = 0;
        public override bool IsLoadingEnabled(Mod mod)
        {
            return false;
        }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Yellow;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 45;
            Item.useAnimation = 45;
        }

        public override bool CanUseItem(Player player)
        {
            return !player.HasBuff(ModContent.BuffType<ClockCooldown>()) && player.HasItem(Type);
        }

        public override bool? UseItem(Player player)
        {
            player.Teleport(player.GetModPlayer<SlayerPlayer>().recentPos);
            player.statLife = player.GetModPlayer<SlayerPlayer>().recentLife;
            player.statMana = player.GetModPlayer<SlayerPlayer>().recentMana;
            player.AddBuff(ModContent.BuffType<ClockCooldown>(), 5 * 60);
            return true;
        }

        public override void UpdateInventory(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                saveTimer++;
                if (saveTimer == 300)
                {
                    player.GetModPlayer<SlayerPlayer>().recentPos = player.position;
                    player.GetModPlayer<SlayerPlayer>().recentLife = player.statLife;
                    player.GetModPlayer<SlayerPlayer>().recentMana = player.statMana;
                    CombatText.NewText(player.getRect(), Color.Gray, "Time shift ready");
                }
                if (saveTimer >= 302)
                    saveTimer = 302;
            }
        }
    }
}
