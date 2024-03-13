using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Buffs.PlayerBuff;
using ShardsOfAtheria.Buffs.PlayerDebuff.Cooldowns;
using ShardsOfAtheria.Buffs.PlayerDebuff.GemCurse;
using ShardsOfAtheria.Buffs.Summons;
using ShardsOfAtheria.Items.Accessories.GemCores;
using ShardsOfAtheria.Items.Accessories.GemCores.Greater;
using ShardsOfAtheria.Items.Accessories.GemCores.Regular;
using ShardsOfAtheria.Items.Accessories.GemCores.Super;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Projectiles.Summon.Minions.GemCore;
using ShardsOfAtheria.Utilities;
using System;
using System.Linq;
using Terraria;
using Terraria.Chat;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ShardsOfAtheria.Players
{
    public class GemPlayer : ModPlayer
    {
        public bool amberCore;
        public bool greaterAmberCore;
        public bool superAmberCore;
        public bool amberCape;
        public int maxAmberBanners;

        public bool amethystCore;
        public bool greaterAmethystCore;
        public bool superAmethystCore;
        public bool amethystBomb;
        public bool amethystWallBomb;
        public bool amethystMask;

        public bool diamondCore;
        public bool greaterDiamondCore;
        public bool superDiamondCore;
        public bool diamondShield;

        public bool lesserEmeraldCore;
        public bool emeraldCore;
        public bool greaterEmeraldCore;
        public bool superEmeraldCore;
        public bool emeraldBoots;

        public bool rubyCore;
        public bool greaterRubyCore;
        public bool superRubyCore;
        public bool rubyGauntlet;
        private int rubyExplosiveCooldown = 0;

        public float sapphireDodgeChance;
        public bool sapphireCore;
        public bool greaterSapphireCore;
        public bool superSapphireCore;
        public bool sapphireSpiritPrevious;
        public bool sapphireSpirit;
        public bool sapphireSpiritUpgrade;

        public bool topazCore;
        public bool greaterTopazCore;
        public bool superTopazCore;
        public int topazHealTimer;
        public bool topazNecklace;

        public bool megaGemCorePrevious;
        public bool megaGemCore;
        public bool[,] masterGemCoreToggles = {
            { true, true, true, true, true, true, true },
            { true, true, true, true, true, true, true },
            { true, true, true, true, true, true, true }
        };
        public bool gemSoul;

        public readonly int[] Curses =
        [
            ModContent.BuffType<WoundedAmethyst>(),
            ModContent.BuffType<CrowdedAmber>(),
            ModContent.BuffType<FrailDiamond>(),
            ModContent.BuffType<TimidEmerald>(),
            ModContent.BuffType<SpitefulRuby>(),
            ModContent.BuffType<RecklessSapphire>(),
            ModContent.BuffType<FragileTopaz>(),
        ];
        public readonly int[] Blessings =
        [
            ModContent.BuffType<EfficientAmethyst>(),
            ModContent.BuffType<SwarmingAmber>(),
            ModContent.BuffType<TenaciousDiamond>(),
            ModContent.BuffType<FleetingEmerald>(),
            ModContent.BuffType<VengefulRuby>(),
            ModContent.BuffType<CunningSapphire>(),
            ModContent.BuffType<MendingTopaz>(),
        ];

        public override void ResetEffects()
        {
            amberCore = false;
            greaterAmberCore = false;
            superAmberCore = false;
            maxAmberBanners = 0;
            amberCape = false;

            amethystCore = false;
            greaterAmethystCore = false;
            superAmethystCore = false;
            amethystMask = false;

            diamondCore = false;
            greaterDiamondCore = false;
            superDiamondCore = false;
            diamondShield = false;

            lesserEmeraldCore = false;
            emeraldCore = false;
            greaterEmeraldCore = false;
            superEmeraldCore = false;
            emeraldBoots = false;

            rubyCore = false;
            greaterRubyCore = false;
            superRubyCore = false;
            rubyGauntlet = false;
            if (rubyExplosiveCooldown > 0)
            {
                rubyExplosiveCooldown--;
            }

            sapphireDodgeChance = 0f;
            sapphireCore = false;
            greaterSapphireCore = false;
            superSapphireCore = false;
            sapphireSpiritPrevious = sapphireSpirit;
            sapphireSpirit = false;
            sapphireSpiritUpgrade = false;

            topazCore = false;
            greaterTopazCore = false;
            superTopazCore = false;
            topazNecklace = false;

            megaGemCorePrevious = megaGemCore;
            megaGemCore = false;
            gemSoul = false;
        }

        public override void SaveData(TagCompound tag)
        {
            tag[nameof(masterGemCoreToggles)] = masterGemCoreToggles;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey(nameof(masterGemCoreToggles)))
            {
                masterGemCoreToggles = tag.Get<bool[,]>(nameof(masterGemCoreToggles));
            }
        }

        public override void UpdateEquips()
        {
            SpawnSapphireSpirit();
            if (greaterDiamondCore)
            {
                int type = ModContent.ProjectileType<DiamondBarrier>();
                if (Player.ownedProjectileCounts[type] == 0)
                {
                    Item core = ModContent.GetInstance<DiamondCore_Greater>().Item;
                    Projectile.NewProjectile(Player.GetSource_Accessory(core), Player.Center,
                        Vector2.Zero, type, 0, 0f, Player.whoAmI);
                    if (megaGemCore)
                    {
                        Projectile.NewProjectile(Player.GetSource_Accessory(core), Player.Center,
                            Vector2.Zero, type, 0, 0f, Player.whoAmI, 1f);
                    }
                }
            }
            if (superAmberCore)
            {
                foreach (Projectile projectile in Main.projectile)
                {
                    if (projectile.active && projectile.owner == Player.whoAmI && projectile.sentry)
                    {
                        foreach (var otherPlayer in Main.player)
                        {
                            if (otherPlayer.team == Player.team)
                            {
                                otherPlayer.statDefense += 4;
                            }
                        }
                    }
                }
            }
            if (superTopazCore && !Player.moonLeech)
            {
                if (++topazHealTimer >= 2400)
                {
                    Player.Heal((int)(Player.statLifeMax2 * 0.15f));
                    topazHealTimer = 0;
                }
            }
            if (greaterEmeraldCore)
            {
                float debuffSpeed = 0f;
                for (int i = 0; i < Player.CountBuffs(); i++)
                {
                    if (!BuffID.Sets.TimeLeftDoesNotDecrease[Player.buffType[i]])
                    {
                        if (Main.debuff[Player.buffType[i]])
                        {
                            debuffSpeed += 0.08f;
                        }
                    }
                }
                Player.moveSpeed += debuffSpeed;
            }
        }

        public override void UpdateVisibleAccessories()
        {
            if (!megaGemCore && !megaGemCorePrevious)
            {
                SpawnSapphireSpirit();
            }
        }

        private void SpawnSapphireSpirit()
        {
            if (sapphireSpirit || gemSoul)
            {
                int type = ModContent.ProjectileType<SapphireSpirit>();
                int damage = 50;
                if (gemSoul)
                {
                    type = ModContent.ProjectileType<GemSoul>();
                    if (sapphireSpiritUpgrade)
                    {
                        damage = 80;
                    }
                }
                if (Player.ownedProjectileCounts[type] == 0)
                {
                    Player.ownedProjectileCounts[type] = 1;
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center,
                        Vector2.Zero, type, damage, 0, Player.whoAmI);
                }
            }
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (SoA.AmethystBombToggle.JustPressed)
            {
                string key = "Mods.ShardsOfAtheria.Items.AmethystCore_Greater.Toggle";
                if (Main.keyState.IsKeyDown(Keys.LeftShift))
                {
                    key += "Wall";
                    amethystWallBomb = !amethystWallBomb;
                    if (amethystWallBomb)
                    {
                        key += "On";
                    }
                    else
                    {
                        key += "Off";
                    }
                }
                else
                {
                    amethystBomb = !amethystBomb;
                    if (amethystBomb)
                    {
                        key += "On";
                    }
                    else
                    {
                        key += "Off";
                    }
                }
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Main.NewText(Language.GetOrRegister(key));
                }
                else
                {
                    ChatHelper.SendChatMessageToClient(NetworkText.FromKey(key), Color.White, Player.whoAmI);
                }
            }
            if (SoA.EmeraldTeleportKey.JustPressed)
            {
                if (megaGemCore || superEmeraldCore)
                {
                    if (!Player.HasBuff<EmeraldTeleportCooldown>())
                    {
                        Vector2 teleportPosition;
                        teleportPosition.X = Main.mouseX + Main.screenPosition.X;
                        if (Player.gravDir == 1f)
                        {
                            teleportPosition.Y = Main.mouseY + Main.screenPosition.Y;
                        }
                        else
                        {
                            teleportPosition.Y = Main.screenPosition.Y + Main.screenHeight - Main.mouseY;
                        }
                        if (teleportPosition.X > 50f && teleportPosition.X < Main.maxTilesX * 16 - 50 && teleportPosition.Y > 50f && teleportPosition.Y < Main.maxTilesY * 16 - 50)
                        {
                            Player.SetImmuneTimeForAllTypes(Player.longInvince ? 100 : 60);
                            Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<EmeraldTeleport>(),
                                0, 0, Player.whoAmI, teleportPosition.X, teleportPosition.Y);
                        }
                    }
                }
            }
        }

        public override bool? CanAutoReuseItem(Item item)
        {
            if (megaGemCore || greaterRubyCore || superRubyCore)
            {
                if (item.damage > 0)
                {
                    return true;
                }
            }
            return base.CanAutoReuseItem(item);
        }

        public override void UpdateVisibleVanityAccessories()
        {
            for (int n = 13; n < 18 + Player.GetAmountOfExtraAccessorySlotsToShow(); n++)
            {
                Item item = Player.armor[n];
                if (item.type == ModContent.ItemType<MegaGemCore>())
                {
                    int loadout = Player.CurrentLoadoutIndex;
                    amberCape = masterGemCoreToggles[loadout, 0];
                    amethystMask = masterGemCoreToggles[loadout, 1];
                    diamondShield = masterGemCoreToggles[loadout, 2];
                    emeraldBoots = masterGemCoreToggles[loadout, 3];
                    rubyGauntlet = masterGemCoreToggles[loadout, 4];
                    sapphireSpirit = masterGemCoreToggles[loadout, 5];
                    topazNecklace = masterGemCoreToggles[loadout, 6];
                }
                else
                {
                    if (item.type == ModContent.ItemType<AmberCore>() ||
                        item.type == ModContent.ItemType<AmberCore_Greater>() ||
                        item.type == ModContent.ItemType<AmberCore_Super>())
                    {
                        amberCape = true;
                    }
                    if (item.type == ModContent.ItemType<AmethystCore>() ||
                        item.type == ModContent.ItemType<AmethystCore_Greater>() ||
                        item.type == ModContent.ItemType<AmethystCore_Super>())
                    {
                        amethystMask = true;
                    }
                    if (item.type == ModContent.ItemType<DiamondCore>() ||
                        item.type == ModContent.ItemType<DiamondCore_Greater>() ||
                        item.type == ModContent.ItemType<DiamondCore_Super>())
                    {
                        diamondShield = true;
                    }
                    if (item.type == ModContent.ItemType<EmeraldCore>() ||
                        item.type == ModContent.ItemType<DiamondCore_Greater>() ||
                        item.type == ModContent.ItemType<EmeraldCore_Super>())
                    {
                        emeraldBoots = true;
                    }
                    if (item.type == ModContent.ItemType<RubyCore>() ||
                        item.type == ModContent.ItemType<RubyCore_Greater>() ||
                        item.type == ModContent.ItemType<RubyCore_Super>())
                    {
                        rubyGauntlet = true;
                    }
                    if (item.type == ModContent.ItemType<SapphireCore>() ||
                        item.type == ModContent.ItemType<SapphireCore_Greater>() ||
                        item.type == ModContent.ItemType<SapphireCore_Super>())
                    {
                        sapphireSpirit = true;
                    }
                    if (item.type == ModContent.ItemType<TopazCore>() ||
                        item.type == ModContent.ItemType<TopazCore_Greater>() ||
                        item.type == ModContent.ItemType<TopazCore_Super>())
                    {
                        topazNecklace = true;
                    }
                }
            }
        }

        public override void FrameEffects()
        {
            if (amberCape)
            {
                Player.back = EquipLoader.GetEquipSlot(Mod, "AmberCape", EquipType.Back);
            }
            if (amethystMask)
            {
                Player.head = EquipLoader.GetEquipSlot(Mod, "AmethystMask", EquipType.Head);
            }
            if (diamondShield)
            {
                Player.shield = (sbyte)EquipLoader.GetEquipSlot(Mod, "DiamondShield", EquipType.Shield);
            }
            if (emeraldBoots)
            {
                Player.shoe = (sbyte)EquipLoader.GetEquipSlot(Mod, "EmeraldBoots", EquipType.Shoes);
            }
            if (rubyGauntlet)
            {
                Player.handon = (sbyte)EquipLoader.GetEquipSlot(Mod, "RubyGauntlet", EquipType.HandsOn);
                Player.handoff = (sbyte)EquipLoader.GetEquipSlot(Mod, "RubyGauntlet_Off", EquipType.HandsOff);
            }
            if (topazNecklace)
            {
                Player.neck = (sbyte)EquipLoader.GetEquipSlot(Mod, "TopazAmulet", EquipType.Neck);
            }
        }

        public override bool FreeDodge(Player.HurtInfo info)
        {
            if (sapphireDodgeChance > 0)
            {
                return TrySapphireDodge(sapphireDodgeChance);
            }
            return base.FreeDodge(info);
        }

        public bool TrySapphireDodge(float percentChance)
        {
            float roll = Main.rand.NextFloat();
            bool doDodge = roll < percentChance;
            Main.rand.NextBool();
            if (doDodge)
            {
                Player.SetImmuneTimeForAllTypes(Player.longInvince ? 100 : 60);
                Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero,
                    ModContent.ProjectileType<SapphireShield>(), 0, 0, Player.whoAmI);
            }
            return doDodge;
        }

        public override void PostHurt(Player.HurtInfo info)
        {
            int gemBlessingTime = 600;
            if (megaGemCore)
            {
                gemBlessingTime += 300;
            }
            if (amberCore)
            {
                Player.AddBuff<SwarmingAmber>(gemBlessingTime * 3);
                int type = ModContent.ProjectileType<AmberFly>();
                int amount = 2;
                if (megaGemCore)
                {
                    amount = 4;
                }
                if (Player.ownedProjectileCounts[type] <= amount)
                {
                    for (int i = 0; i < amount - Player.ownedProjectileCounts[type]; i++)
                    {
                        Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.One, type, 16, 0f);
                    }
                }
            }

            AddCurses();
            if (amethystCore) Player.AddBuff<EfficientAmethyst>(gemBlessingTime);
            if (diamondCore) Player.AddBuff<TenaciousDiamond>(gemBlessingTime);
            if (emeraldCore) Player.AddBuff<FleetingEmerald>(gemBlessingTime);
            if (rubyCore) Player.AddBuff<VengefulRuby>(gemBlessingTime);
            if (sapphireCore) Player.AddBuff<CunningSapphire>(gemBlessingTime);
            if (topazCore) Player.AddBuff<MendingTopaz>(gemBlessingTime);

            if (greaterSapphireCore)
            {
                Vector2 vector = new(0, -10);
                float rotation = MathHelper.PiOver2;
                for (int i = 0; i < 10; i++)
                {
                    var perturbedSpeed = vector.RotatedByRandom(rotation);
                    perturbedSpeed *= 1f - Main.rand.NextFloat(0.33f);
                    Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, perturbedSpeed,
                        ModContent.ProjectileType<SapphireSpike>(), 60, 0f);
                }
            }
            if (superSapphireCore)
            {
                for (int i = 0; i < Player.CountBuffs(); i++)
                {
                    int buffID = Player.buffType[i];
                    if (!BuffID.Sets.TimeLeftDoesNotDecrease[buffID] && !Curses.Contains(buffID))
                    {
                        if (Main.debuff[buffID])
                        {
                            Player.buffTime[i] -= 30;
                        }
                        else
                        {
                            Player.buffTime[i] += 60;
                        }
                    }
                }
            }
        }

        private void AddCurses()
        {
            if (Player.HasBuff<SwarmingAmber>() && Player.HasBuff<EfficientAmethyst>()) TryAddCurse<CrowdedAmber>();
            if (Player.HasBuff<EfficientAmethyst>() && Player.HasBuff<MendingTopaz>()) TryAddCurse<WoundedAmethyst>();
            if (Player.HasBuff<FleetingEmerald>() && Player.HasBuff<VengefulRuby>()) TryAddCurse<TimidEmerald>();
            if (Player.HasBuff<TenaciousDiamond>() && Player.HasBuff<VengefulRuby>()) TryAddCurse<FrailDiamond>();
            if (Player.HasBuff<VengefulRuby>() && Player.HasBuff<TenaciousDiamond>()) TryAddCurse<SpitefulRuby>();
            if (Player.HasBuff<CunningSapphire>() && Player.HasBuff<VengefulRuby>()) TryAddCurse<RecklessSapphire>();
            if (Player.HasBuff<MendingTopaz>() && Player.HasBuff<TenaciousDiamond>()) TryAddCurse<FragileTopaz>();
        }

        private void TryAddCurse<T>(int curseChanceDenominator = 5) where T : ModBuff
        {
            if (Main.rand.NextBool(curseChanceDenominator))
            {
                int curseDuration = 900;
                Player.AddBuff<T>(curseDuration);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Player.HasBuff<CunningSapphire>())
            {
                target.AddBuff(BuffID.Confused, 300);
            }
            if (greaterRubyCore && rubyExplosiveCooldown == 0)
            {
                if (hit.Crit)
                {
                    int type = ModContent.ProjectileType<RubyExplosive>();
                    Projectile.NewProjectile(target.GetSource_Death(), target.Center, Vector2.Zero, type, 60, 0f, Player.whoAmI);
                    rubyExplosiveCooldown = 180;
                }
            }
            if (superRubyCore)
            {
                target.AddBuff<SpitefulRuby>(480);
            }
            if (megaGemCore)
            {
                int lifestealDenominator = 6;
                if (hit.DamageType == DamageClass.Summon)
                {
                    lifestealDenominator *= 4;
                }
                if (Main.rand.NextBool(lifestealDenominator) && !Player.moonLeech)
                {
                    int type = ModContent.ProjectileType<LifeStealGem>();
                    Projectile.NewProjectile(target.GetSource_Death(), target.Center, Vector2.Zero, type, 3, 0f, Player.whoAmI);
                }
            }
            if (target.life <= 0)
            {
                if (greaterAmberCore)
                {
                    int type = ModContent.ProjectileType<AmberBanner>();
                    float ai0 = 0f;
                    int bannerAmount = Player.ownedProjectileCounts[type];
                    if (megaGemCore)
                    {
                        if (bannerAmount == 0)
                        {
                            ai0 = 1f;
                        }
                        else
                        {
                            AmberBanner.MakeOldestBannerFollowPlayer(Player);
                        }
                    }
                    var nearestBanner = ShardsHelpers.FindClosestProjectile(target.Center, 150, type);
                    if (nearestBanner == null)
                    {
                        Projectile.NewProjectile(target.GetSource_Death(), target.Center, Vector2.Zero, type, 0, 0f, Player.whoAmI, ai0);
                        if (bannerAmount >= maxAmberBanners)
                        {
                            int firstBannerWhoAmI = AmberBanner.FindOldestBanner(Player);
                            if (firstBannerWhoAmI > -1)
                            {
                                var firstBanner = Main.projectile[firstBannerWhoAmI];
                                firstBanner.Kill();
                            }
                            AmberBanner.MakeOldestBannerFollowPlayer(Player);
                        }
                    }
                }
                if (greaterTopazCore && !Player.moonLeech)
                {
                    Projectile.NewProjectile(target.GetSource_Death(), target.Center, new Vector2(0, -10),
                        ModContent.ProjectileType<TopazOrb>(), 0, 0f, Player.whoAmI);
                }
                if (superDiamondCore)
                {
                    int buffTime = (int)(damageDone * 0.05f);
                    if (Player.HasBuff<DiamondBarrierBuff>())
                    {
                        int buffIndex = Player.FindBuffIndex(ModContent.BuffType<DiamondBarrierBuff>());
                        Player.buffTime[buffIndex] += buffTime;
                    }
                    else
                    {
                        Player.AddBuff<DiamondBarrierBuff>(buffTime);
                    }
                }
            }
        }
    }
}
