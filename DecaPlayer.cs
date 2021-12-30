using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Items.DecaEquipment;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria
{
    public class DecaPlayer : ModPlayer
    {
        public bool modelDeca;

        public override void ResetEffects()
        {
            modelDeca = false;
        }

        public override void PostUpdateMiscEffects()
        {
            if (modelDeca)
            {
                Player.armorEffectDrawOutlines = true;
                Player.GetDamage(DamageClass.Generic) += 1f;
                Player.GetCritChance(DamageClass.Generic) += 20;
                Player.statDefense += 50;
                Player.endurance += 1f;
                Player.wingTimeMax += 160;
                Player.rocketTimeMax += 160;
                Player.noKnockback = true;
                Player.buffImmune[BuffID.Poisoned] = true;
                Player.buffImmune[BuffID.Bleeding] = true;
                Player.buffImmune[BuffID.Darkness] = true;
                Player.buffImmune[BuffID.Cursed] = true;
                Player.buffImmune[BuffID.Silenced] = true;
                Player.buffImmune[BuffID.Slow] = true;
                Player.buffImmune[BuffID.Confused] = true;
                Player.buffImmune[BuffID.BrokenArmor] = true;
                Player.buffImmune[BuffID.Weak] = true;
                Player.buffImmune[BuffID.Venom] = true;
                Player.buffImmune[BuffID.OnFire] = true;
                Player.buffImmune[BuffID.Frostburn] = true;
                Player.buffImmune[BuffID.Electrified] = true;
                Player.buffImmune[BuffID.Chilled] = true;
                Player.buffImmune[BuffID.Frozen] = true;
                Player.buffImmune[BuffID.WitheredArmor] = true;
                Player.buffImmune[BuffID.Ichor] = true;
                Player.buffImmune[BuffID.ChaosState] = true;
                Player.buffImmune[BuffID.MoonLeech] = true;
                Player.buffImmune[BuffID.PotionSickness] = true;
                Player.buffImmune[ModContent.BuffType<HeartBreak>()] = true;
                Player.lavaImmune = true;
                Player.fireWalk = true;
                Player.GetModPlayer<SMPlayer>().overdriveTimeCurrent = Player.GetModPlayer<SMPlayer>().overdriveTimeMax;
            }
        }

        public override void PreUpdateMovement()
        {
            if (modelDeca)
                Player.moveSpeed += 1f;
        }
    }
}
