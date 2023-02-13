using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Buffs.Cooldowns;
using ShardsOfAtheria.Buffs.PlayerBuff;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Items.Accessories;
using ShardsOfAtheria.Items.Accessories.GemCores;
using ShardsOfAtheria.Items.Potions;
using ShardsOfAtheria.Items.SinfulSouls;
using ShardsOfAtheria.Items.Tools.Misc;
using ShardsOfAtheria.Items.Weapons.Areus;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Projectiles.Weapon.Magic;
using ShardsOfAtheria.Projectiles.Weapon.Melee.GenesisRagnarok;
using ShardsOfAtheria.Projectiles.Weapon.Summon;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ShardsOfAtheria.Players
{
    public class ShardsPlayer : ModPlayer
    {
        public bool lesserSapphireCore;
        public bool sapphireCore;
        public bool superSapphireCore;
        public bool greaterRubyCore;
        public bool superRubyCore;
        public bool baseConservation;
        public bool lesserEmeraldCore;
        public bool emeraldCore;
        public bool superEmeraldCore;
        public bool areusKey;
        public bool megaGemCore;
        public bool amethystMask;
        public bool emeraldWings;
        public bool diamanodShield;
        public bool rubyGauntlet;
        public bool sapphireSpirit;
        public bool topazNecklace;
        public bool heartBreak;
        public bool healingItem;
        public bool phaseOffense;
        public bool rushDrive;
        public bool areusChargePack;

        public bool valkyrieCrown;
        public bool valkyrieCrownHideVanity;
        public bool valkyrieCrownForceVanity;
        public bool hardlightBraces;
        public int hardlightBracesCooldown;
        public int hardlightBracesCooldownMax = 60;

        public bool pearlwoodSet;
        public int pearlwoodBowShoot;

        public int combatTimer;
        public bool inCombat;
        /// <summary>
        /// When above 0, you are in a combo. Ticks down by 1 every player update.
        /// <para>Item "combos" are used for determining what type of item action to use.</para>
        /// <para>A usage example would be a weapon with a 3 swing pattern. Each swing will increase the combo meter by 60, and when it becomes greater than 120, reset to 0.</para>
        /// </summary>
        public ushort itemCombo;

        // These 10 relate to Biometal.
        public bool Biometal;
        public bool BiometalSound;
        public bool BiometalHideVanity;
        public bool BiometalForceVanity;
        public int overdriveTimeCurrent;
        public const int DefaultOverdriveTimeMax = 300;
        public int overdriveTimeMax;
        public int overdriveTimeMax2;
        internal int overdriveTimeRegenTimer = 0;

        public double[] elementMultiplier = { 1.0, 1.0, 1.0, 1.0 };

        public int readingDisk = 0;

        public int aggression = 0;

        public bool conductive;
        public int genesisRagnarockUpgrades = 0;
        public bool showRagnarok;

        public override void ResetEffects()
        {
            lesserSapphireCore = false;
            lesserEmeraldCore = false;
            sapphireCore = false;
            superSapphireCore = false;
            greaterRubyCore = false;
            superRubyCore = false;
            baseConservation = false;
            superEmeraldCore = false;
            areusKey = false;
            megaGemCore = false;
            amethystMask = false;
            diamanodShield = false;
            emeraldWings = false;
            rubyGauntlet = false;
            sapphireSpirit = false;
            topazNecklace = false;
            heartBreak = false;
            healingItem = false;
            rushDrive = false;
            valkyrieCrown = false;
            showRagnarok = false;
            hardlightBraces = false;
            if (hardlightBracesCooldown > 0)
            {
                hardlightBracesCooldown--;
            }
            pearlwoodSet = false;

            ResetVariables();
            Biometal = BiometalSound = BiometalHideVanity = BiometalForceVanity = false;

            UpdateResource();

            inCombat = false;
            if (combatTimer > 0)
            {
                combatTimer--;
                inCombat = true;
            }
            else if (aggression > 0)
            {
                aggression--;
            }
            if (aggression >= 100)
            {
                aggression = 100;
            }
            else if (aggression < 0)
            {
                aggression = 0;
            }

            conductive = false;
        }

        public override void UpdateDead()
        {
            ResetVariables();
        }

        private void ResetVariables()
        {
            overdriveTimeMax2 = overdriveTimeMax;
        }

        public override void Initialize()
        {
            overdriveTimeCurrent = 300;
            overdriveTimeMax = DefaultOverdriveTimeMax;
            itemCombo = 0;

            phaseOffense = true;
        }

        public override void SaveData(TagCompound tag)
        {
            tag["overdriveTimeCurrent"] = overdriveTimeCurrent;
            tag["phaseOffense"] = phaseOffense;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("overdriveTimeCurrent"))
                overdriveTimeCurrent = (int)tag["overdriveTimeCurrent"];
            if (tag.ContainsKey("phaseOffense"))
                phaseOffense = tag.GetBool("phaseOffense");
        }

        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {
            if (!mediumCoreDeath && Player.GetModPlayer<SinfulPlayer>().SevenSoulUsed > 0)
                return new[] { new Item(ModContent.ItemType<Necronomicon>()) };

            if (Player.GetModPlayer<SinfulPlayer>().SevenSoulUsed == 0)
            {
                List<Item> list = new() {
                    new Item(ModContent.ItemType<SinfulSoul>())
                    //new Item(ModContent.ItemType<SinfulArmament>())
                };

                if (!mediumCoreDeath)
                    list.Add(new Item(ModContent.ItemType<Necronomicon>()));

                return list;
            }

            return base.AddStartingItems(mediumCoreDeath);
        }

        public override void PostUpdate()
        {
            if (!Biometal)
            {
                Player.ClearBuff(ModContent.BuffType<Overdrive>());
                Player.buffImmune[ModContent.BuffType<Overdrive>()] = true;
            }
            else
            {
                Player.buffImmune[ModContent.BuffType<Overdrive>()] = false;
            }
            if (Player.ownedProjectileCounts[ModContent.ProjectileType<Ragnarok_Shield>()] > 0)
            {
                Player.statDefense += 20;
                Player.endurance += .15f;
            }
        }

        public override void PostUpdateBuffs()
        {
            if (Biometal)
            {
                Player.moveSpeed += .1f;
            }
            if (Player.HasBuff(ModContent.BuffType<Overdrive>()))
            {
                Player.moveSpeed += .5f;
            }
            if (Player.HasBuff(ModContent.BuffType<SoulInfused>()))
            {
                Player.moveSpeed += .5f;
            }
            Player.GetDamage(DamageClass.Generic) += 0.01f * aggression;
            Player.moveSpeed += 0.01f * aggression;
            Player.aggro += 1 * aggression;
        }

        public void UpdateItemFields()
        {
            if (itemCombo > 0)
            {
                itemCombo--;
            }
        }

        public override void UpdateEquips()
        {
            if (areusKey)
            {
                Player.moveSpeed += .5f;
            }
            if (lesserEmeraldCore)
            {
                Player.moveSpeed += .05f;
            }
            if (emeraldCore)
            {
                Player.moveSpeed += .1f;
            }
            if (superEmeraldCore)
            {
                Player.moveSpeed += .15f;
            }
            if (Player.statLife < Player.statLifeMax2 / 2 && rushDrive)
            {
                if (phaseOffense)
                {
                    Player.GetDamage(DamageClass.Generic) += 1f;
                    Player.GetCritChance(DamageClass.Generic) += 0.05f;
                    Player.statDefense /= 2;
                }
                else
                {
                    Player.GetDamage(DamageClass.Generic) -= 0.5f;
                    Player.endurance += 0.2f;
                    Player.statDefense *= 2;
                }
                Player.moveSpeed += .2f;
            }
            if (Player.HeldItem.type == ModContent.ItemType<AreusKatana>())
            {
                Player.moveSpeed += .05f;
            }
            if (Player.HeldItem.type == ModContent.ItemType<TheMessiah>())
            {
                Player.autoReuseGlove = false;
            }
        }

        public override void UpdateLifeRegen()
        {
            if (Biometal && !Player.HasBuff(ModContent.BuffType<Overdrive>()))
                Player.lifeRegen += 4;
            if (Player.HasBuff(ModContent.BuffType<SoulInfused>()))
                Player.lifeRegen += 4;
            if (areusKey)
                Player.lifeRegen *= 2;
        }

        private void UpdateResource()
        {
            if (Player.HasBuff(ModContent.BuffType<Overdrive>()))
            {
                // For our resource lets make it regen slowly over time to keep it simple, let's use exampleResourceRegenTimer to count up to whatever value we want, then increase currentResource.
                overdriveTimeRegenTimer++; //Increase it by 60 per second, or 1 per tick.

                // A simple timer that goes up to 1 seconds, increases the overdriveTime by 1 and then resets back to 0.
                if (overdriveTimeRegenTimer > 60)
                {
                    overdriveTimeCurrent -= 1;
                    overdriveTimeRegenTimer = 0;
                }
            }
        }

        public override bool CanConsumeAmmo(Item weapon, Item ammo)
        {
            if (baseConservation)
                return Main.rand.NextFloat() >= .15f;
            return true;
        }

        public override void SetControls()
        {
            if (Player.HasBuff(ModContent.BuffType<StunLock>()))
            {
                Player.controlUp = false;
                Player.controlDown = false;
                Player.controlLeft = false;
                Player.controlRight = false;
                Player.controlJump = false;
                Player.controlUseItem = false;
            }
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (Main.keyState.IsKeyDown(Keys.Escape))
            {
                readingDisk = 0;
            }
            if (ShardsOfAtheria.ArmorSetBonusActive.JustReleased && !Player.HasBuff(ModContent.BuffType<SetBonusCooldown>()))
            {
                if (pearlwoodSet && !Player.mouseInterface)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<SoulDagger>(), 50, 0, Player.whoAmI, i);
                    }
                    Player.AddBuff(ModContent.BuffType<SetBonusCooldown>(), 120);
                }
            }
            if (ShardsOfAtheria.OverdriveKey.JustPressed)
            {
                if (Biometal)
                {
                    if (overdriveTimeCurrent > 0 && !Player.HasBuff(ModContent.BuffType<Overdrive>()))
                    {
                        Player.AddBuff(ModContent.BuffType<Overdrive>(), 2);
                        CombatText.NewText(Player.Hitbox, Color.Green, "Overdrive: ON", true);
                        SoundEngine.PlaySound(SoundID.Item4, Player.position);
                    }
                    else
                    {
                        Player.ClearBuff(ModContent.BuffType<Overdrive>());
                        CombatText.NewText(Player.Hitbox, Color.Red, "Overdrive: OFF");
                    }
                }
            }
            if (ShardsOfAtheria.EmeraldTeleportKey.JustPressed)
            {
                if (megaGemCore || superEmeraldCore)
                {
                    Vector2 vector21 = default;
                    vector21.X = Main.mouseX + Main.screenPosition.X;
                    if (Player.gravDir == 1f)
                    {
                        vector21.Y = Main.mouseY + Main.screenPosition.Y - Player.height;
                    }
                    else
                    {
                        vector21.Y = Main.screenPosition.Y + Main.screenHeight - Main.mouseY;
                    }
                    vector21.X -= Player.width / 2;
                    if (vector21.X > 50f && vector21.X < Main.maxTilesX * 16 - 50 && vector21.Y > 50f && vector21.Y < Main.maxTilesY * 16 - 50)
                    {
                        int num181 = (int)(vector21.X / 16f);
                        int num182 = (int)(vector21.Y / 16f);
                        if ((Main.tile[num181, num182].WallType != 87 || !(num182 > Main.worldSurface) || NPC.downedPlantBoss) && !Collision.SolidCollision(vector21, Player.width, Player.height))
                        {
                            Player.Teleport(vector21, 1);
                            NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, Player.whoAmI, vector21.X, vector21.Y, 1);
                            if (Player.chaosState)
                            {
                                Player.statLife -= Player.statLifeMax2 / 7;
                                PlayerDeathReason damageSource = PlayerDeathReason.ByOther(13);
                                if (Main.rand.NextBool(2))
                                {
                                    damageSource = PlayerDeathReason.ByOther(Player.Male ? 14 : 15);
                                }
                                if (Player.statLife <= 0)
                                {
                                    Player.KillMe(damageSource, 1.0, 0);
                                }
                                Player.lifeRegenCount = 0;
                                Player.lifeRegenTime = 0;
                            }
                            Player.AddBuff(BuffID.ChaosState, 360);
                        }
                    }
                }
            }
            if (ShardsOfAtheria.PhaseSwitch.JustPressed)
            {
                if (Player.statLife >= Player.statLifeMax2 / 2 && rushDrive)
                {
                    if (phaseOffense)
                    {
                        phaseOffense = false;
                        ChatHelper.SendChatMessageToClient(NetworkText.FromLiteral("Phase 2 Type: Defensive"), Color.White, Player.whoAmI);
                    }
                    else
                    {
                        phaseOffense = true;
                        ChatHelper.SendChatMessageToClient(NetworkText.FromLiteral("Phase 2 Type: Offensive"), Color.White, Player.whoAmI);
                    }
                }
            }
        }

        public override bool? CanAutoReuseItem(Item item)
        {
            if (megaGemCore || greaterRubyCore || superRubyCore)
            {
                return true;
            }
            return base.CanAutoReuseItem(item);
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            combatTimer = 300;
            if (valkyrieCrown)
            {
                target.AddBuff(ModContent.BuffType<ElectricShock>(), 60);
            }
            if (greaterRubyCore)
            {
                target.AddBuff(BuffID.OnFire, 600);
            }
            if (superRubyCore)
            {
                target.AddBuff(BuffID.CursedInferno, 600);
                target.AddBuff(BuffID.Ichor, 600);
            }
            if (megaGemCore)
            {
                target.AddBuff(BuffID.Daybreak, 600);
                target.AddBuff(BuffID.BetsysCurse, 600);
                Player.AddBuff(BuffID.Ironskin, 600);
                Player.AddBuff(BuffID.Endurance, 600);
            }
            PreformHardlightBracesEffect(target);
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            combatTimer = 300;
            if (proj.owner == Player.whoAmI)
            {
                if (valkyrieCrown)
                {
                    target.AddBuff(ModContent.BuffType<ElectricShock>(), 60);
                }
                if (greaterRubyCore)
                {
                    target.AddBuff(BuffID.OnFire, 600);
                }
                if (superRubyCore)
                {
                    target.AddBuff(BuffID.CursedInferno, 600);
                    target.AddBuff(BuffID.Ichor, 600);
                }
                if (megaGemCore)
                {
                    target.AddBuff(BuffID.Daybreak, 600);
                    target.AddBuff(BuffID.BetsysCurse, 600);
                    Player.AddBuff(BuffID.Ironskin, 600);
                    Player.AddBuff(BuffID.Endurance, 600);
                }
                if (proj.type != ModContent.ProjectileType<HardlightBlade>())
                {
                    PreformHardlightBracesEffect(target);
                }
            }
        }

        public void PreformHardlightBracesEffect(NPC target)
        {
            if (hardlightBraces && hardlightBracesCooldown == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    Item braces = ModContent.GetInstance<HardlightBraces>().Item;
                    Vector2 point = target.Center + Vector2.One.RotatedBy(MathHelper.ToRadians(90 * i)) * 120f;
                    Vector2 velocity = Vector2.Normalize(target.Center - point) * braces.shootSpeed;
                    Projectile blades = Projectile.NewProjectileDirect(Player.GetSource_Accessory(braces), point, velocity,
                        braces.shoot, braces.damage, braces.knockBack, Player.whoAmI);
                    blades.DamageType = DamageClass.Generic;
                    blades.penetrate = 1;
                }
                hardlightBracesCooldown = hardlightBracesCooldownMax;
            }
        }

        public override void UpdateVisibleVanityAccessories()
        {
            for (int n = 13; n < 18 + Player.GetAmountOfExtraAccessorySlotsToShow(); n++)
            {
                Item item = Player.armor[n];
                if (item.type == ModContent.ItemType<Biometal>())
                {
                    BiometalHideVanity = false;
                    BiometalForceVanity = true;
                }
                if (item.type == ModContent.ItemType<ValkyrieCrown>())
                {
                    valkyrieCrownHideVanity = false;
                    valkyrieCrownForceVanity = true;
                }
                if (item.type == ModContent.ItemType<MegaGemCore>())
                {
                    amethystMask = true;
                    diamanodShield = true;
                    emeraldWings = true;
                    rubyGauntlet = true;
                    sapphireSpirit = true;
                    topazNecklace = true;
                }
                else
                {
                    if (item.type == ModContent.ItemType<AmethystCore>() || item.type == ModContent.ItemType<AmethystCore_Greater>()
                        || item.type == ModContent.ItemType<AmethystCore_Super>())
                    {
                        amethystMask = true;
                    }
                    if (item.type == ModContent.ItemType<DiamondCore>() || item.type == ModContent.ItemType<DiamondCore_Greater>()
                        || item.type == ModContent.ItemType<DiamondCore_Super>())
                    {
                        diamanodShield = true;
                    }
                    if (item.type == ModContent.ItemType<EmeraldCore>() || item.type == ModContent.ItemType<EmeraldCore_Greater>()
                        || item.type == ModContent.ItemType<EmeraldCore_Super>())
                    {
                        emeraldWings = true;
                    }
                    if (item.type == ModContent.ItemType<RubyCore>() || item.type == ModContent.ItemType<RubyCore_Greater>()
                        || item.type == ModContent.ItemType<RubyCore_Super>())
                    {
                        rubyGauntlet = true;
                    }
                    if (item.type == ModContent.ItemType<SapphireCore>() || item.type == ModContent.ItemType<SapphireCore_Greater>()
                        || item.type == ModContent.ItemType<SapphireCore_Super>())
                    {
                        sapphireSpirit = true;
                    }
                    if (item.type == ModContent.ItemType<TopazCore>() || item.type == ModContent.ItemType<TopazCore_Greater>()
                        || item.type == ModContent.ItemType<TopazCore_Super>())
                    {
                        topazNecklace = true;
                    }
                }
            }
        }

        public override void FrameEffects()
        {
            if (amethystMask)
            {
                Player.head = EquipLoader.GetEquipSlot(Mod, "AmethystMask", EquipType.Head);
            }
            if (diamanodShield)
            {
                Player.shield = (sbyte)EquipLoader.GetEquipSlot(Mod, "DiamondShield", EquipType.Shield);
            }
            if (emeraldWings)
            {
                Player.wings = (sbyte)EquipLoader.GetEquipSlot(Mod, "EmeraldWings", EquipType.Wings);
            }
            if (rubyGauntlet)
            {
                Player.handon = (sbyte)EquipLoader.GetEquipSlot(Mod, "RubyGauntlet", EquipType.HandsOn);
                Player.handoff = (sbyte)EquipLoader.GetEquipSlot(Mod, "RubyGauntlet_Off", EquipType.HandsOff);
            }
            if (sapphireSpirit)
            {
                Player.balloon = (sbyte)EquipLoader.GetEquipSlot(Mod, "SapphireSpirit", EquipType.Balloon);
            }
            if (topazNecklace)
            {
                Player.neck = (sbyte)EquipLoader.GetEquipSlot(Mod, "TopazAmulet", EquipType.Neck);
            }
            if ((Biometal || BiometalForceVanity) && !BiometalHideVanity)
            {
                var biometal = ModContent.GetInstance<Biometal>();
                Player.head = EquipLoader.GetEquipSlot(Mod, biometal.Name, EquipType.Head);
                Player.body = EquipLoader.GetEquipSlot(Mod, biometal.Name, EquipType.Body);
                Player.legs = EquipLoader.GetEquipSlot(Mod, biometal.Name, EquipType.Legs);

                Player.handon = -1;
                Player.handoff = -1;
                Player.back = -1;
                Player.front = -1;
                Player.shoe = -1;
                Player.waist = -1;
                Player.neck = -1;
                Player.face = -1;
                Player.balloon = -1;
            }
            if (showRagnarok && Player.ownedProjectileCounts[ModContent.ProjectileType<Ragnarok_Shield>()] == 0
                && Player.ownedProjectileCounts[ModContent.ProjectileType<RagnarokProj>()] == 0
                && Player.ownedProjectileCounts[ModContent.ProjectileType<RagnarokProj2>()] == 0)
            {
                var ragnarok = ModContent.GetInstance<GenesisAndRagnarok>();
                Player.shield = (sbyte)EquipLoader.GetEquipSlot(Mod, ragnarok.Name, EquipType.Shield);
            }
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource, ref int cooldownCounter)
        {
            if (!Player.immune)
            {
                if (megaGemCore)
                {
                    return TrySapphireDodge(0.2f);
                }
                else if (superSapphireCore)
                {
                    return TrySapphireDodge(0.15f);
                }
                else if (sapphireCore)
                {
                    return TrySapphireDodge(0.1f);
                }
                else if (lesserSapphireCore)
                {
                    return TrySapphireDodge(0.05f);
                }
            }
            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource, ref cooldownCounter);
        }

        public bool TrySapphireDodge(float percentChance)
        {
            float roll = Main.rand.NextFloat();
            bool doDodge = roll < percentChance;
            if (doDodge)
            {
                Player.immune = true;
                Player.immuneTime = 60;
                Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<SapphireShield>(), 0, 0, Player.whoAmI);
            }
            return !doDodge;
        }

        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit, int cooldownCounter)
        {
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (Main.npc[i].active)
                {
                    Main.npc[i].GetGlobalNPC<SoAGlobalNPC>().flawless = false;
                }
            }
        }

        public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit, int cooldownCounter)
        {
            combatTimer = 300;
            if (Player.HasBuff(ModContent.BuffType<Overdrive>()))
                Player.ClearBuff(ModContent.BuffType<Overdrive>());
            if (megaGemCore)
            {
                Player.AddBuff(BuffID.Rage, 600);
                Player.AddBuff(BuffID.Wrath, 600);
                Player.AddBuff(BuffID.Inferno, 600);
            }
            if (superSapphireCore)
            {
                Player.AddBuff(BuffID.Inferno, 600);
            }
            if (Player.HasItem(ModContent.ItemType<WandOfHealing>()) && !Player.HasBuff(ModContent.BuffType<HeartBreak>()))
            {
                Player.AddBuff(ModContent.BuffType<HeartBreak>(), 900);
            }
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (Player.HasBuff(ModContent.BuffType<Overdrive>()))
            {
                damageSource = PlayerDeathReason.ByCustomReason(Player.name + " pushed too far.");
            }
            return true;
        }

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (Player.HasBuff(ModContent.BuffType<InjectionShock>()))
            {
                if (Main.rand.NextBool(4) && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(Player.position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, DustID.Blood, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default, 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                }
            }
        }
    }
}