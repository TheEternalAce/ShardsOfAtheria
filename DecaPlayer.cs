using SagesMania.Buffs;
using SagesMania.Items.DecaEquipment;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania
{
    public class DecaPlayer : ModPlayer
    {
        public bool decaFragmentA;
        public bool decaFragmentB;
        public bool decaFragmentC;
        public bool decaFragmentD;
        public bool decaFragmentE;
        public bool fullDeca;
        public bool trueFullDeca;

        public override void ResetEffects()
        {
            decaFragmentA = false;
            decaFragmentB = false;
            decaFragmentC = false;
            decaFragmentD = false;
            decaFragmentE = false;
        }

        public override void PostUpdateMiscEffects()
        {
            if (fullDeca)
                player.armorEffectDrawOutlines = true;
            if (decaFragmentA && decaFragmentB && decaFragmentC && decaFragmentD && decaFragmentE)
                fullDeca = true;
            else fullDeca = false;
            if (fullDeca && player.HasItem(ModContent.ItemType<DecaClaw>()) && player.HasItem(ModContent.ItemType<DecaBlade>())
                && player.HasItem(ModContent.ItemType<DecaScythe>()) && player.HasItem(ModContent.ItemType<DecaSwarmerRemote>()))
                trueFullDeca = true;
            else trueFullDeca = false;
            if (decaFragmentA)
            {
                player.allDamage += 1f;
                player.magicCrit += 20;
                player.meleeCrit += 20;
                player.rangedCrit += 20;
            }
            if (decaFragmentB)
            {
                player.statDefense += 50;
                player.endurance += 1f;
            }
            if (decaFragmentC)
            {
                player.wingTimeMax += 160;
                player.rocketTimeMax += 160;
            }
            if (decaFragmentD)
            {
                player.noKnockback = true;
                player.buffImmune[BuffID.Poisoned] = true;
                player.buffImmune[BuffID.Bleeding] = true;
                player.buffImmune[BuffID.Darkness] = true;
                player.buffImmune[BuffID.Cursed] = true;
                player.buffImmune[BuffID.Silenced] = true;
                player.buffImmune[BuffID.Slow] = true;
                player.buffImmune[BuffID.Confused] = true;
                player.buffImmune[BuffID.BrokenArmor] = true;
                player.buffImmune[BuffID.Weak] = true;
                player.buffImmune[BuffID.Venom] = true;
                player.buffImmune[BuffID.OnFire] = true;
                player.buffImmune[BuffID.Frostburn] = true;
                player.buffImmune[BuffID.Electrified] = true;
                player.buffImmune[BuffID.Chilled] = true;
                player.buffImmune[BuffID.Frozen] = true;
                player.buffImmune[BuffID.WitheredArmor] = true;
                player.buffImmune[BuffID.Ichor] = true;
                player.buffImmune[BuffID.ChaosState] = true;
                player.buffImmune[BuffID.MoonLeech] = true;
                player.buffImmune[BuffID.PotionSickness] = true;
                player.buffImmune[ModContent.BuffType<HeartBreak>()] = true;
                player.lavaImmune = true;
                player.fireWalk = true;
            }
            if (decaFragmentE)
            {
                player.GetModPlayer<SMPlayer>().overdriveTimeCurrent = player.GetModPlayer<SMPlayer>().overdriveTimeMax;
                player.buffImmune[ModContent.BuffType<Overheat>()] = true;
                player.buffImmune[ModContent.BuffType<MegamergeCooldown>()] = true;
            }
        }

        public override void PreUpdateMovement()
        {
            if (decaFragmentC)
                player.moveSpeed += 1f;
        }

        public override bool? CanHitNPC(Item item, NPC target)
        {
            if (Main.LocalPlayer.HeldItem.modItem is DecaEquipment)
                return true;
            return base.CanHitNPC(item, target);
        }

        public override bool? CanHitNPCWithProj(Projectile proj, NPC target)
        {
            if (Main.LocalPlayer.HeldItem.modItem is DecaEquipment && proj.friendly)
                return true;
            return base.CanHitNPCWithProj(proj, target);
        }
    }
}
