using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SinfulSouls
{
    public class WrathSoul : SinfulSouls
    {
        public override int SoulBuffType => ModContent.BuffType<WrathBuff>();
    }

    public class WrathPlayer : ModPlayer
    {
        public bool soulActive;
        int lastDamageTaken;
        public int retainFuryTime;

        public override void ResetEffects()
        {
            soulActive = false;
            if (retainFuryTime > 0) retainFuryTime--;
        }

        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            CritWrath(ref modifiers);
        }

        public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
        {
            CritWrath(ref modifiers);
        }

        public override void PostHurt(Player.HurtInfo info)
        {
            lastDamageTaken = info.Damage;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.FlatBonusDamage += lastDamageTaken;
            lastDamageTaken = 0;
            if (Player.HasBuff<Fury>() && target.CanBeChasedBy()) retainFuryTime = 60;
        }

        public void CritWrath(ref Player.HurtModifiers modifiers)
        {
            if (soulActive && Main.rand.NextFloat() < 0.04f)
            {
                Player.AddBuff<Fury>(600);
                modifiers.FinalDamage *= 2f;
                SoundEngine.PlaySound(SoundID.ScaryScream, Player.Center);
            }
        }
    }

    public class WrathBuff : SinfulSoulBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.Wrath().soulActive = true;
            base.Update(player, ref buffIndex);
        }
    }

    public class Fury : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.buffTime[buffIndex] == 0)
            {
                string gender = player.Male ? "his" : "her";
                PlayerDeathReason death = new()
                {
                    SourceCustomReason = $"{player.name}'s rage burned {gender} soul away.",
                };
                player.KillMe(death, player.statLifeMax2 * 2, 1);
            }
            if (player.Wrath().retainFuryTime > 0) player.buffTime[buffIndex]++;

            player.GetDamage(DamageClass.Generic) += 0.5f;
            player.GetAttackSpeed(DamageClass.Generic) += 0.08f;
            player.moveSpeed += 0.1f;
        }
    }
}
