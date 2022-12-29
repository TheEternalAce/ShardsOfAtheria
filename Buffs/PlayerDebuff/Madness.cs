using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Players;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerDebuff
{
    public class Madness : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.confused = true;
            player.blind = true;
            player.blackout = true;
            player.yoraiz0rDarkness = true;
            player.statDefense -= 16;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.confused = true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, int buffIndex, ref BuffDrawParams drawParams)
        {
            Vector2 shake = new Vector2(Main.rand.Next(-2, 3), Main.rand.Next(-2, 3));

            drawParams.Position += shake;
            drawParams.TextPosition += shake;
            return base.PreDraw(spriteBatch, buffIndex, ref drawParams);
        }
    }

    public class MaddenedPlayer : ModPlayer
    {
        public override void UpdateBadLifeRegen()
        {
            if (Player.HasBuff(ModContent.BuffType<Madness>()))
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;
                Player.lifeRegen -= Player.GetModPlayer<SlayerPlayer>().soulCrystals.Count;
            }
        }
    }

    public class MaddenedNPC : GlobalNPC
    {
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (npc.HasBuff(ModContent.BuffType<Madness>()))
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 16;
            }
        }
    }
}
