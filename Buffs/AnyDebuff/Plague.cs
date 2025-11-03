using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.NPCDebuff;
using ShardsOfAtheria.Dusts;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.AnyDebuff
{
    public class Plague : ModBuff
    {
        public const float SpeedReduction = 0.3f;
        public const int DefenseReduction = 20;

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            int severity = player.GetModPlayer<PlaguePlayer>().plagueSeverity;
            if (severity > 1)
            {
                player.moveSpeed -= SpeedReduction;
                player.accRunSpeed -= SpeedReduction;
                player.statDefense -= DefenseReduction;
                player.blind = true;
            }
            if (severity > 2)
            {
                player.moveSpeed -= SpeedReduction;
                player.accRunSpeed -= SpeedReduction;
                player.blackout = true;
            }
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            int severity = npc.GetGlobalNPC<PlagueNPC>().plagueSeverity;
            if (severity > 1)
            {
                npc.StatSpeed() -= SpeedReduction;
            }
            if (severity > 2)
            {
                npc.StatSpeed() -= SpeedReduction;
            }
        }
    }

    public class PlagueNPC : GlobalNPC
    {
        public int plagueSeverity;
        public int plagueSeverityTimer;

        public override bool InstancePerEntity => true;

        public override void PostAI(NPC npc)
        {
            if (npc.HasBuff<Plague>())
            {
                if (++plagueSeverityTimer >= 300)
                {
                    plagueSeverity++;
                    plagueSeverityTimer = 0;
                }
            }
            else
            {
                plagueSeverity = plagueSeverityTimer = 0;
            }
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (plagueSeverity > 0)
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                int dpt = 10 * plagueSeverity;
                if (npc.HasBuff<PlagueMark>()) dpt = (int)(dpt * 1.5f);
                npc.lifeRegen -= dpt;
                if (damage < dpt)
                {
                    damage = dpt;
                }
            }
        }

        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if (plagueSeverity > 1)
            {
                modifiers.Defense.Flat -= Plague.DefenseReduction;
            }
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (npc.HasBuff<Plague>() && Main.rand.NextBool(plagueSeverity > 6 ? 2 : 8 - plagueSeverity))
            {
                int type = ModContent.DustType<PlagueDust>();
                int dust = Dust.NewDust(npc.position, npc.width + 4, npc.height + 4, type, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default, 1f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 1.8f;
                Main.dust[dust].velocity.Y -= 0.5f;
            }
        }
    }

    public class PlaguePlayer : ModPlayer
    {
        public int plagueSeverity;
        public int plagueSeverityTimer;

        public override void PostUpdateBuffs()
        {
            if (Player.HasBuff<Plague>())
            {
                if (++plagueSeverityTimer >= 300)
                {
                    plagueSeverity++;
                    plagueSeverityTimer = 0;
                }
            }
            else
            {
                plagueSeverity = plagueSeverityTimer = 0;
            }
        }

        public override void UpdateBadLifeRegen()
        {
            if (plagueSeverity > 0)
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 10 life lost per second, if the player is holding their left or right movement keys.
                int dpt = 20 * plagueSeverity;
                if (Player.HasBuff<PlagueMark>()) dpt = (int)(dpt * 1.5f);
                Player.lifeRegen -= dpt;
            }
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genDust, ref PlayerDeathReason damageSource)
        {
            if (damageSource.SourceNPCIndex == -1 &&
                damageSource.SourceItem == null &&
                damageSource.SourceProjectileType == 0)
            {
                if (Player.HasBuff<Plague>())
                {
                    damageSource = PlayerDeathReason.ByCustomReason(NetworkText.FromKey("ShardsOfAtheria.DeathMessages.Plague", Player.name));
                }
            }
            return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genDust, ref damageSource);
        }

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (Player.HasBuff<Plague>() && Main.rand.NextBool(8 - plagueSeverity))
            {
                int type = ModContent.DustType<PlagueDust>();
                int dust = Dust.NewDust(Player.position, Player.width + 4, Player.height + 4, type, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default, 1f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 1.8f;
                Main.dust[dust].velocity.Y -= 0.5f;
            }
        }
    }
}
