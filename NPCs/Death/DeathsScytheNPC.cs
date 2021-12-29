using ShardsOfAtheria.Items;
using ShardsOfAtheria.Items.SlayerItems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.NPCs.Death
{
    public class DeathsScytheNPC : ModNPC
    {
        public override string Texture => "ShardsOfAtheria/Items/SlayerItems/DeathsScythe";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Death's Scythe");
        }

        public override void SetDefaults()
        {
            NPC.width = 60;
            NPC.height = 60;
            NPC.damage = 200000;
            NPC.defense = 0;
            NPC.lifeMax = 2000;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath52;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 23;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = (int)(NPC.lifeMax * .5f);
            NPC.damage = (int)(NPC.damage * .5f);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.LocalPlayer.statLife == 0)
                Main.NewText($"[c/FF0000:{ Main.LocalPlayer.name} had their soul reaped]");
        }

        public override bool PreAI()
        {
            if (!NPC.AnyNPCs(ModContent.NPCType<Death>()))
                NPC.active = false;
            return base.PreAI();
        }
    }
}