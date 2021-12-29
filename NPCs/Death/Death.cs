using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items;
using ShardsOfAtheria.Items.SlayerItems;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.NPCs.Death
{
    [AutoloadBossHead]
    public class Death : ModNPC
    {
        public override string HeadTexture => "ShardsOfAtheria/NPCs/Death/Death_Head_Boss";
        private int scytheSpawnTimer;

        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            NPC.width = 100;
            NPC.height = 90;
            NPC.damage = 200000;
            NPC.defense = 0;
            NPC.lifeMax = 200000;
            NPC.HitSound = SoundID.NPCHit54;
            NPC.DeathSound = SoundID.NPCDeath52;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 22;
            NPC.boss = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            Music = MusicID.Boss1;
            NPC.value = Item.buyPrice(0, 5, 0, 0);
            NPC.npcSlots = 15f;
        }

        public override bool PreAI()
        {
            Player player = Main.LocalPlayer;
            if (player.dead)
                NPC.active = false;
            if (ModContent.GetInstance<SMWorld>().slainDeath)
            {
                Main.NewText("Death was slain...");
                NPC.active = false;
            }
            return base.PreAI();
        }

        public override void AI()
        {
            Player player = Main.LocalPlayer;
            if (NPC.localAI[0] == 0f && NPC.life >= 1 && !ModContent.GetInstance<SMWorld>().slainDeath)
            {
                SoundEngine.PlaySound(SoundID.Roar, NPC.position, 0);
                if (ModContent.GetInstance<SMWorld>().slayerMode)
                {
                    Main.NewText("[c/323232:...]");
                    NPC.NewNPC((int)NPC.position.X - 50, (int)NPC.position.Y, ModContent.NPCType<DeathsScytheNPC>());
                    NPC.NewNPC((int)NPC.position.X - 100, (int)NPC.position.Y, ModContent.NPCType<DeathsScytheNPC>());
                    NPC.NewNPC((int)NPC.position.X + 50, (int)NPC.position.Y, ModContent.NPCType<DeathsScytheNPC>());
                    NPC.NewNPC((int)NPC.position.X + 100, (int)NPC.position.Y, ModContent.NPCType<DeathsScytheNPC>());
                }
                else
                {
                    Main.NewText("[c/323232:Well here we are, let's do this.]");
                    NPC.NewNPC((int)NPC.position.X - 50, (int)NPC.position.Y, ModContent.NPCType<DeathsScytheNPC>());
                    NPC.NewNPC((int)NPC.position.X + 50, (int)NPC.position.Y, ModContent.NPCType<DeathsScytheNPC>());
                }
                NPC.localAI[0] = 1f;
            }
            NPC.spriteDirection = player.Center.X > NPC.Center.X ? 1 : -1;
            if (NPC.CountNPCS(ModContent.NPCType<DeathsScytheNPC>()) < 10)
                scytheSpawnTimer++;
            if (scytheSpawnTimer == 600)
            {
                SoundEngine.PlaySound(SoundID.Item71);
                if (Main.rand.Next(1) == 0)
                    NPC.NewNPC((int)NPC.position.X - 50, (int)NPC.position.Y, ModContent.NPCType<DeathsScytheNPC>());
                else NPC.NewNPC((int)NPC.position.X + 50, (int)NPC.position.Y, ModContent.NPCType<DeathsScytheNPC>());
                scytheSpawnTimer = 0;
            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * .5f);
            NPC.damage = (int)(NPC.damage * .5f);
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            ModContent.GetInstance<SMWorld>().downedDeath = true;
            if (!ModContent.GetInstance<SMWorld>().slayerMode)
            {
                Item.NewItem(NPC.getRect(), ModContent.ItemType<SoulOfDaylight>(), 10);
                Item.NewItem(NPC.getRect(), ItemID.SoulofFlight, 10);
                Item.NewItem(NPC.getRect(), ItemID.SoulofFright, 10);
                Item.NewItem(NPC.getRect(), ItemID.SoulofLight, 10);
                Item.NewItem(NPC.getRect(), ItemID.SoulofMight, 10);
                Item.NewItem(NPC.getRect(), ItemID.SoulofNight, 10);
                Item.NewItem(NPC.getRect(), ItemID.SoulofSight, 10);
                Item.NewItem(NPC.getRect(), ModContent.ItemType<SoulOfSpite>(), 10);
                Item.NewItem(NPC.getRect(), ModContent.ItemType<SoulOfStarlight>(), 10);
                Item.NewItem(NPC.getRect(), ModContent.ItemType<DeathEssence>(), Main.rand.Next(6, 8));
            }
            else
            {
                Item.NewItem(NPC.getRect(), ModContent.ItemType<DeathEssence>(), 4000);
                Item.NewItem(NPC.getRect(), ModContent.ItemType<SoulOfDaylight>(), 4000);
                Item.NewItem(NPC.getRect(), ItemID.SoulofFlight, 4000);
                Item.NewItem(NPC.getRect(), ItemID.SoulofFright, 4000);
                Item.NewItem(NPC.getRect(), ItemID.SoulofLight, 4000);
                Item.NewItem(NPC.getRect(), ItemID.SoulofMight, 4000);
                Item.NewItem(NPC.getRect(), ItemID.SoulofNight, 4000);
                Item.NewItem(NPC.getRect(), ItemID.SoulofSight, 4000);
                Item.NewItem(NPC.getRect(), ModContent.ItemType<SoulOfSpite>(), 4000);
                Item.NewItem(NPC.getRect(), ModContent.ItemType<SoulOfStarlight>(), 4000);
                Item.NewItem(NPC.getRect(), ModContent.ItemType<DeathsScythe>());
                ModContent.GetInstance<SMWorld>().slainDeath = true;
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.LocalPlayer.statLife == 0)
                Main.NewText($"[c/FF0000:{Main.LocalPlayer.name} had their soul reaped]");
        }
    }
}