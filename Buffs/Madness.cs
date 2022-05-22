using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs
{
    public class Madness : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Madness");
            Description.SetDefault("Losing life and your vision is reduced\n" +
                "'You're losing yourself...'");
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
        public int madness;

        public override void PostUpdate()
        {
            if (Player.HasBuff(ModContent.BuffType<Madness>()))
            {
                if (++madness >= 1800)
                {
                    //CombatText.NewText(Player.getRect(), Color.Purple, $"{GetMadnessText()}");
                    madness = 0;
                }
            }
            else madness = 0;
        }

        public override void UpdateBadLifeRegen()
        {
            if (Player.HasBuff(ModContent.BuffType<Madness>()))
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;
                Player.lifeRegen -= Player.GetModPlayer<SlayerPlayer>().soulCrystals;
            }
        }

        private static string GetMadnessText()
        {
            return Main.rand.Next(3) switch
            {
                0 => "Keep going",
                1 => "I don't know what I've been told",
                _ => "Oh the misery"
            };
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
