using SagesMania.Items;
using SagesMania.Items.SlayerItems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.NPCs.Death
{
    public class DeathsScytheNPC : ModNPC
    {
        public override string Texture => "SagesMania/Items/SlayerItems/DeathsScythe";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Death's Scythe");
        }

        public override void SetDefaults()
        {
            npc.width = 60;
            npc.height = 60;
            npc.damage = 200000;
            npc.defense = 0;
            npc.lifeMax = 2000;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath52;
            npc.knockBackResist = 0f;
            npc.aiStyle = 23;
        }

        public override bool PreNPCLoot()
        {
            return false;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * .5f);
            npc.damage = (int)(npc.damage * .5f);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.LocalPlayer.statLife == 0)
                Main.NewText($"[c/FF0000:{ Main.LocalPlayer.name} had their soul reaped]");
        }

        public override bool PreAI()
        {
            if (!NPC.AnyNPCs(ModContent.NPCType<Death>()))
                npc.active = false;
            return base.PreAI();
        }
    }
}