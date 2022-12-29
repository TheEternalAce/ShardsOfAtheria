using Microsoft.Xna.Framework;
using ShardsOfAtheria.Config;
using ShardsOfAtheria.NPCs.Boss.NovaStellar.LightningValkyrie;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.BossSummons
{
    public class ValkyrieCrest : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.SortingPriorityBossSpawns[Type] = 12; // This helps sort inventory know that this is a boss summoning Item.

            // This is set to true for all NPCs that can be summoned via an Item (calling NPC.SpawnOnPlayer). If this is for a modded boss,
            // write this in the bosses file instead
            NPCID.Sets.MPAllowedEnemies[ModContent.NPCType<NovaStellar>()] = true;

            SacrificeTotal = 3;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            if (!ModContent.GetInstance<ShardsServerConfig>().nonConsumeBoss)
            {
                Item.consumable = true;
                Item.maxStack = 9999;
            }

            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(0, 5);
        }

        // We use the CanUseItem hook to prevent a Player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            return player.ZoneOverworldHeight && !NPC.AnyNPCs(ModContent.NPCType<NovaStellar>());
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                if (!Main.dayTime)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Nova Stellar is asleep, try again in the morning."), Color.White);
                    }
                    else
                    {
                        Main.NewText("Nova Stellar is asleep, try again in the morning.");
                    }
                    return true;
                }

                // If the Player using the item is the client
                // (explicitely excluded serverside here)
                SoundEngine.PlaySound(SoundID.Roar, player.position);

                int type = ModContent.NPCType<NovaStellar>();

                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    // If the Player is not in multiPlayer, spawn directly
                    NPC.SpawnOnPlayer(player.whoAmI, type);
                }
                else
                {
                    // If the Player is in multiPlayer, request a spawn
                    // This will only work if NPCID.Sets.MPAllowedEnemies[type] is true, which we set in MinionBossBody
                    NetMessage.SendData(MessageID.SpawnBoss, number: player.whoAmI, number2: type);
                }
            }

            return true;
        }
    }
}