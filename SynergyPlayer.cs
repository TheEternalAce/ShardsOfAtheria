using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Items.SlayerItems;

using ShardsOfAtheria.NPCs;
using ShardsOfAtheria.Projectiles.Minions;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Projectiles.Tools;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;

namespace ShardsOfAtheria
{
    public class SynergyPlayer : ModPlayer
    {
        public int probeSpawnTimer;
        public int slimeSpawnTimer;

        public bool brainEyeSynergy;
        public bool brainLordSynergy;
        public bool eyeLordSynergy;
        public bool eyeTwinSynergy;
        public bool kingQueenSynergy;
        public bool mechaMayhemSynergy;
        public bool lunaticLordSynergy;

        public override void ResetEffects()
        {
            brainEyeSynergy = false;
            brainLordSynergy = false;
            eyeLordSynergy = false;
            eyeTwinSynergy = false;
            kingQueenSynergy = false;
            mechaMayhemSynergy = false;
            lunaticLordSynergy = false;
        }

        public override void UpdateEquips()
        {
            if (!ModContent.GetInstance<SoAWorld>().slayerMode || !Main.hardMode)
                return;
            if (brainEyeSynergy)
            {
                Player.AddBuff(BuffID.Shine, 2);
                Player.AddBuff(BuffID.NightOwl, 2);
                Player.AddBuff(BuffID.Hunter, 2);
                Player.AddBuff(BuffID.Dangersense, 2);
                Player.AddBuff(BuffID.Spelunker, 2);
            }
            if (brainLordSynergy)
            {
                if (!Player.HasBuff(ModContent.BuffType<TrueCreeperShield>()))
                    Player.GetModPlayer<SlayerPlayer>().creeperSpawnTimer++;
                else Player.GetModPlayer<SlayerPlayer>().creeperSpawnTimer = 0;
                if (Player.GetModPlayer<SlayerPlayer>().creeperSpawnTimer >= 360)
                {
                    if (Player.HasBuff(ModContent.BuffType<TrueCreeperShield>()))
                        return;
                    else
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            NPC.NewNPC(NPC.GetBossSpawnSource(Player.whoAmI), (int)Player.Center.X, (int)Player.Center.Y, ModContent.NPCType<TrueCreeper>());
                        }
                    }
                    Player.GetModPlayer<SlayerPlayer>().creeperSpawnTimer = 0;
                }
            }
            if (eyeTwinSynergy)
            {
                Player.statLifeMax2 *= (int)1.5f;
                if (Player.statLife < Math.Round(Player.statLifeMax2 * .5))
                {
                    Player.GetDamage(DamageClass.Generic) += 1f;
                    Player.statDefense *= (int).5f;
                }
            }
            if (mechaMayhemSynergy)
            {
                if (Player.GetModPlayer<SoAPlayer>().inCombatTimer > 0)
                {
                    Player.GetModPlayer<SlayerPlayer>().spinningTimer++;
                    if (Player.GetModPlayer<SlayerPlayer>().spinningTimer == 1200)
                    {
                        Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<SpinPrime>(), 200, 9f, Player.whoAmI);
                        SoundEngine.PlaySound(SoundID.Roar, Player.position);
                    }
                    if (Player.GetModPlayer<SlayerPlayer>().spinningTimer >= 1200)
                    {
                        Player.statDefense *= 2;
                        Player.GetDamage(DamageClass.Generic) += 1f;
                    }
                    if (Player.GetModPlayer<SlayerPlayer>().spinningTimer == 1500)
                        Player.GetModPlayer<SlayerPlayer>().spinningTimer = 0;
                }

                if (++probeSpawnTimer == 600 && Player.ownedProjectileCounts[ModContent.ProjectileType<TheDestroyersProbeAttack>()] < 5)
                {
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<TheDestroyersProbeAttack>(), 50, 0f, Player.whoAmI);
                    probeSpawnTimer = 0;
                }
            }
            else probeSpawnTimer = 0;
        }

        public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            if (!ModContent.GetInstance<SoAWorld>().slayerMode || !Main.hardMode)
                return;
            if (lunaticLordSynergy)
            {
                SoundEngine.PlaySound(SoundID.Item103);
                Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<ShadowTendrils>(), 50, 0f, Player.whoAmI);
            }
        }
    }
}
