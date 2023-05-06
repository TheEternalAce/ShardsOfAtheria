using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.NPCDebuff
{
    public class LoomingEntropy : ModBuff
    {
        public static readonly int TagDamage = 26;

        public override void SetStaticDefaults()
        {
            BuffID.Sets.IsAnNPCWhipDebuff[Type] = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.defense -= 14;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense -= 14;
        }
    }

    public class EntropyPlayer : ModPlayer
    {
        public override void UpdateBadLifeRegen()
        {
            if (Player.HasBuff(ModContent.BuffType<LoomingEntropy>()))
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 50;
            }
        }
    }

    public class EntropyNPC : GlobalNPC
    {
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            // Only player attacks should benefit from this buff, hence the NPC and trap checks.
            if (projectile.npcProj || projectile.trap || !projectile.IsMinionOrSentryRelated)
                return;


            // SummonTagDamageMultiplier scales down tag damage for some specific minion and sentry projectiles for balance purposes.
            var projTagMultiplier = ProjectileID.Sets.SummonTagDamageMultiplier[projectile.type];
            if (npc.HasBuff<LoomingEntropy>())
            {
                // Apply a flat bonus to every hit
                modifiers.FlatBonusDamage += LoomingEntropy.TagDamage * projTagMultiplier;
            }
        }
    }
}
