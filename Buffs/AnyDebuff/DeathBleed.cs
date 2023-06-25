using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.AnyDebuff
{
    public class DeathBleed : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<BleedPlayer>().deathBleed = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<BleedNPC>().deathBleed = true;
        }
    }

    public class BleedNPC : GlobalNPC
    {
        public bool deathBleed;

        public override bool InstancePerEntity => true;

        public override void ResetEffects(NPC npc)
        {
            deathBleed = false;
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (deathBleed)
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 20;
                if (damage < 5)
                {
                    damage = 10;
                }
            }
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (deathBleed && Main.rand.NextBool(8))
            {
                int dust = Dust.NewDust(npc.position, npc.width + 4, npc.height + 4, DustID.Blood, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default, 1f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 1.8f;
                Main.dust[dust].velocity.Y -= 0.5f;
            }
        }
    }

    public class BleedPlayer : ModPlayer
    {
        public bool deathBleed;

        public override void ResetEffects()
        {
            deathBleed = false;
        }

        public override void UpdateBadLifeRegen()
        {
            if (deathBleed)
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 10 life lost per second, if the player is holding their left or right movement keys.
                Player.lifeRegen -= 25;
            }
        }

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (deathBleed && Main.rand.NextBool(8))
            {
                int dust = Dust.NewDust(Player.position, Player.width + 4, Player.height + 4, DustID.Blood, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default, 1f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 1.8f;
                Main.dust[dust].velocity.Y -= 0.5f;
            }
        }
    }
}
