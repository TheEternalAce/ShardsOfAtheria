using Microsoft.Xna.Framework;
using SagesMania.Items;
using SagesMania.Items.SlayerItems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.NPCs.Death
{
    [AutoloadBossHead]
    public class Death : ModNPC
    {
        public override string HeadTexture => "SagesMania/NPCs/Death/Death_Head_Boss";
        private int scytheSpawnTimer;

        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            npc.width = 100;
            npc.height = 90;
            npc.damage = 200000;
            npc.defense = 0;
            npc.lifeMax = 200000;
            npc.HitSound = SoundID.NPCHit54;
            npc.DeathSound = SoundID.NPCDeath52;
            npc.knockBackResist = 0f;
            npc.aiStyle = 22;
            npc.boss = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            music = MusicID.Boss1;
            npc.value = Item.buyPrice(0, 5, 0, 0);
            npc.npcSlots = 15f;
        }

        public override bool PreAI()
        {
            Player player = Main.LocalPlayer;
            if (player.dead)
                npc.active = false;
            if (ModContent.GetInstance<SMWorld>().slainDeath)
            {
                Main.NewText("Death was slain...");
                npc.active = false;
            }
            return base.PreAI();
        }

        public override void AI()
        {
            Player player = Main.LocalPlayer;
            if (npc.localAI[0] == 0f && npc.life >= 1 && !ModContent.GetInstance<SMWorld>().slainDeath)
            {
                Main.PlaySound(SoundID.Roar, npc.position, 0);
                if (ModContent.GetInstance<SMWorld>().slayerMode)
                {
                    Main.NewText("[c/323232:...]");
                    NPC.NewNPC((int)npc.position.X - 50, (int)npc.position.Y, ModContent.NPCType<DeathsScytheNPC>());
                    NPC.NewNPC((int)npc.position.X - 100, (int)npc.position.Y, ModContent.NPCType<DeathsScytheNPC>());
                    NPC.NewNPC((int)npc.position.X + 50, (int)npc.position.Y, ModContent.NPCType<DeathsScytheNPC>());
                    NPC.NewNPC((int)npc.position.X + 100, (int)npc.position.Y, ModContent.NPCType<DeathsScytheNPC>());
                }
                else
                {
                    Main.NewText("[c/323232:Well here we are, let's do this.]");
                    NPC.NewNPC((int)npc.position.X - 50, (int)npc.position.Y, ModContent.NPCType<DeathsScytheNPC>());
                    NPC.NewNPC((int)npc.position.X + 50, (int)npc.position.Y, ModContent.NPCType<DeathsScytheNPC>());
                }
                npc.localAI[0] = 1f;
            }
            npc.spriteDirection = player.Center.X > npc.Center.X ? 1 : -1;
            if (NPC.CountNPCS(ModContent.NPCType<DeathsScytheNPC>()) < 10)
                scytheSpawnTimer++;
            if (scytheSpawnTimer == 600)
            {
                Main.PlaySound(SoundID.Item71);
                if (Main.rand.Next(1) == 0)
                    NPC.NewNPC((int)npc.position.X - 50, (int)npc.position.Y, ModContent.NPCType<DeathsScytheNPC>());
                else NPC.NewNPC((int)npc.position.X + 50, (int)npc.position.Y, ModContent.NPCType<DeathsScytheNPC>());
                scytheSpawnTimer = 0;
            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * .5f);
            npc.damage = (int)(npc.damage * .5f);
        }

        public override void NPCLoot()
        {
            ModContent.GetInstance<SMWorld>().downedDeath = true;
            if (!ModContent.GetInstance<SMWorld>().slayerMode)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<SoulOfDaylight>(), 10);
                Item.NewItem(npc.getRect(), ItemID.SoulofFlight, 10);
                Item.NewItem(npc.getRect(), ItemID.SoulofFright, 10);
                Item.NewItem(npc.getRect(), ItemID.SoulofLight, 10);
                Item.NewItem(npc.getRect(), ItemID.SoulofMight, 10);
                Item.NewItem(npc.getRect(), ItemID.SoulofNight, 10);
                Item.NewItem(npc.getRect(), ItemID.SoulofSight, 10);
                Item.NewItem(npc.getRect(), ModContent.ItemType<SoulOfSpite>(), 10);
                Item.NewItem(npc.getRect(), ModContent.ItemType<SoulOfStarlight>(), 10);
                Item.NewItem(npc.getRect(), ModContent.ItemType<DeathEssence>(), Main.rand.Next(6, 8));
            }
            else
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<DeathEssence>(), 4000);
                Item.NewItem(npc.getRect(), ModContent.ItemType<SoulOfDaylight>(), 4000);
                Item.NewItem(npc.getRect(), ItemID.SoulofFlight, 4000);
                Item.NewItem(npc.getRect(), ItemID.SoulofFright, 4000);
                Item.NewItem(npc.getRect(), ItemID.SoulofLight, 4000);
                Item.NewItem(npc.getRect(), ItemID.SoulofMight, 4000);
                Item.NewItem(npc.getRect(), ItemID.SoulofNight, 4000);
                Item.NewItem(npc.getRect(), ItemID.SoulofSight, 4000);
                Item.NewItem(npc.getRect(), ModContent.ItemType<SoulOfSpite>(), 4000);
                Item.NewItem(npc.getRect(), ModContent.ItemType<SoulOfStarlight>(), 4000);
                Item.NewItem(npc.getRect(), ModContent.ItemType<DeathsScythe>());
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