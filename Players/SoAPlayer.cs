using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Buffs.Cooldowns;
using ShardsOfAtheria.Items.Accessories;
using ShardsOfAtheria.Items.SevenDeadlySouls;
using ShardsOfAtheria.Items.Tools.Misc;
using ShardsOfAtheria.Items.Weapons.Areus;
using ShardsOfAtheria.Items.Weapons.Melee;
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
    public class SoAPlayer : ModPlayer
    {
        public bool areusBatteryElectrify;
        public bool areusWings;
        public bool lesserSapphireCore;
        public bool sapphireCore;
        public bool superSapphireCore;
        public bool greaterRubyCore;
        public bool superRubyCore;
        public bool baseConservation;
        public bool sapphireMinion;
        public bool lesserEmeraldCore;
        public bool emeraldCore;
        public bool superEmeraldCore;
        public bool areusKey;
        public bool megaGemCore;
        public bool shadowBrand;
        public bool shadowBrandToggled;
        public bool hallowedSeal;
        public bool zenovaJavelin;
        public bool heartBreak;
        public bool sMHealingItem;
        public bool phaseOffense;
        public bool rushDrive;
        public bool areusChargePack;

        public bool valkyrieCrown;
        public bool valkyrieCrownHideVanity;
        public bool valkyrieCrownForceVanity;

        public bool pearlwoodSet;
        public int pearlwoodBowShoot;

        public int inCombat;

        public bool naturalAreusRegen;
        public bool areusChargeMaxed;

        // These 5 relate to Biometal.
        public bool BiometalPrevious;
        public bool Biometal;
        public bool BiometalHideVanity;
        public bool BiometalForceVanity;

        public int overdriveTimeCurrent;
        public const int DefaultOverdriveTimeMax = 300;
        public int overdriveTimeMax;
        public int overdriveTimeMax2;
        internal int overdriveTimeRegenTimer = 0;

        public override void ResetEffects()
        {
            areusBatteryElectrify = false;
            areusWings = false;
            lesserSapphireCore = false;
            lesserEmeraldCore = false;
            sapphireCore = false;
            superSapphireCore = false;
            greaterRubyCore = false;
            superRubyCore = false;
            baseConservation = false;
            sapphireMinion = false;
            superEmeraldCore = false;
            areusKey = false;
            megaGemCore = false;
            shadowBrand = false;
            hallowedSeal = false;
            zenovaJavelin = false;
            heartBreak = false;
            sMHealingItem = false;
            rushDrive = false;
            valkyrieCrown = false;

            pearlwoodSet = false;

            ResetVariables();
            naturalAreusRegen = false;

            BiometalPrevious = Biometal;
            Biometal = BiometalHideVanity = BiometalForceVanity = false;
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

            shadowBrandToggled = false;
            phaseOffense = true;
        }

        public override void SaveData(TagCompound tag)
        {
            tag["shadowBrandToggled"] = shadowBrandToggled;
            tag["overdriveTimeCurrent"] = overdriveTimeCurrent;
            tag["phaseOffense"] = phaseOffense;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("shadowBrandToggled"))
                shadowBrandToggled = tag.GetBool("shadowBrandToggled");
            if (tag.ContainsKey("overdriveTimeCurrent"))
                overdriveTimeCurrent = (int)tag["overdriveTimeCurrent"];
            if (tag.ContainsKey("phaseOffense"))
                phaseOffense = tag.GetBool("phaseOffense");
        }

        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {
            if (!mediumCoreDeath && Player.GetModPlayer<SevenSoulPlayer>().SevenSoulUsed > 0)
                return new[] { new Item(ModContent.ItemType<Necronomicon>()) };

            if (Player.GetModPlayer<SevenSoulPlayer>().SevenSoulUsed == 0)
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

        public override void PreUpdate()
        {
            UpdateResource();

            if (inCombat > 0)
                inCombat--;
        }

        public override void PostUpdate()
        {
            if (!Player.HasBuff(ModContent.BuffType<Megamerged>()))
            {
                Player.ClearBuff(ModContent.BuffType<Overdrive>());
                Player.buffImmune[ModContent.BuffType<Overdrive>()] = true;
            }
            else Player.buffImmune[ModContent.BuffType<Overdrive>()] = false;
            if (Player.ownedProjectileCounts[ModContent.ProjectileType<Ragnarok_Shield>()] > 0)
            {
                Player.statDefense += 20;
                Player.endurance += .15f;
            }
        }

        public override void PostUpdateBuffs()
        {
            if (Player.HasBuff(ModContent.BuffType<Megamerged>()))
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
        }

        public override void UpdateEquips()
        {
            //if (Player.armor[10].type == ItemID.KingSlimeMask)
            //{
            //    Player.jumpBoost = true;
            //}
            //if (Player.armor[10].type == ItemID.EyeMask)
            //{
            //    Player.npcTypeNoAggro[NPCID.DemonEye] = true;
            //    Player.npcTypeNoAggro[NPCID.FromNetId(NPCID.DemonEye2)] = true;
            //    Player.npcTypeNoAggro[NPCID.DemonEyeOwl] = true;
            //    Player.npcTypeNoAggro[NPCID.DemonEyeSpaceship] = true;
            //}
            //if (Player.armor[10].type == ItemID.EaterMask)
            //{
            //    Player.calmed = true;
            //}
            //if (Player.armor[10].type == ItemID.BrainMask)
            //{
            //    Player.AddBuff(BuffID.Dangersense, 2);
            //    Player.AddBuff(BuffID.Hunter, 2);
            //    Player.AddBuff(BuffID.Spelunker, 2);
            //}
            //if (Player.armor[10].type == ItemID.BeeMask)
            //{
            //    Player.npcTypeNoAggro[NPCID.Bee] = true;
            //    Player.npcTypeNoAggro[NPCID.BeeSmall] = true;
            //    Player.npcTypeNoAggro[NPCID.Hornet] = true;
            //    Player.npcTypeNoAggro[NPCID.Hornet] = true;
            //    Player.npcTypeNoAggro[NPCID.HornetFatty] = true;
            //    Player.npcTypeNoAggro[NPCID.FromNetId(NPCID.LittleHornetFatty)] = true;
            //    Player.npcTypeNoAggro[NPCID.FromNetId(NPCID.BigHornetFatty)] = true;
            //    Player.npcTypeNoAggro[NPCID.HornetHoney] = true;
            //    Player.npcTypeNoAggro[NPCID.FromNetId(NPCID.LittleHornetHoney)] = true;
            //    Player.npcTypeNoAggro[NPCID.FromNetId(NPCID.BigHornetHoney)] = true;
            //    Player.npcTypeNoAggro[NPCID.HornetLeafy] = true;
            //    Player.npcTypeNoAggro[NPCID.FromNetId(NPCID.LittleHornetLeafy)] = true;
            //    Player.npcTypeNoAggro[NPCID.FromNetId(NPCID.BigHornetLeafy)] = true;
            //    Player.npcTypeNoAggro[NPCID.HornetSpikey] = true;
            //    Player.npcTypeNoAggro[NPCID.FromNetId(NPCID.LittleHornetSpikey)] = true;
            //    Player.npcTypeNoAggro[NPCID.FromNetId(NPCID.BigHornetSpikey)] = true;
            //    Player.npcTypeNoAggro[NPCID.HornetStingy] = true;
            //    Player.npcTypeNoAggro[NPCID.FromNetId(NPCID.LittleHornetStingy)] = true;
            //    Player.npcTypeNoAggro[NPCID.FromNetId(NPCID.BigHornetStingy)] = true;
            //}
            //if (Player.armor[10].type == ItemID.SkeletronMask)
            //{
            //    Player.npcTypeNoAggro[NPCID.Skeleton] = true;
            //    Player.npcTypeNoAggro[NPCID.SkeletonAlien] = true;
            //    Player.npcTypeNoAggro[NPCID.SkeletonAstonaut] = true;
            //    Player.npcTypeNoAggro[NPCID.SkeletonTopHat] = true;
            //    Player.npcTypeNoAggro[NPCID.FromNetId(NPCID.BigHeadacheSkeleton)] = true;
            //    Player.npcTypeNoAggro[NPCID.HeadacheSkeleton] = true;
            //}
            //if (Player.armor[10].type == ItemID.DeerclopsMask)
            //{
            //    Player.buffImmune[BuffID.Frostburn] = true;
            //    Player.buffImmune[BuffID.Frostburn2] = true;
            //    Player.buffImmune[BuffID.Frozen] = true;
            //    Player.buffImmune[BuffID.Chilled] = true;
            //}
            //if (Player.armor[10].type == ItemID.FleshMask)
            //{
            //    Player.defaultItemGrabRange *= (int)1.5f;
            //}
            //if (Player.armor[10].type == ItemID.QueenSlimeMask)
            //{
            //    Player.npcTypeNoAggro[NPCID.BlueSlime] = true;
            //    Player.npcTypeNoAggro[NPCID.SlimeSpiked] = true;
            //}
            //if (Player.armor[10].type == ItemID.DestroyerMask)
            //{
            //    Player.AddBuff(BuffID.Mining, 2);
            //}
            //if (Player.armor[10].type == ItemID.SkeletronPrimeMask)
            //{

            //}
            //if (Player.armor[10].type == ItemID.TwinMask)
            //{

            //}
            //if (Player.armor[10].type == ItemID.PlanteraMask)
            //{

            //}
            //if (Player.armor[10].type == ItemID.GolemMask)
            //{

            //}
            //if (Player.armor[10].type == ItemID.DukeFishronMask)
            //{

            //}
            //if (Player.armor[10].type == ItemID.FairyQueenMask)
            //{

            //}
            //if (Player.armor[10].type == ItemID.BossMaskCultist)
            //{

            //}
            //if (Player.armor[10].type == ItemID.BossMaskMoonlord)
            //{

            //}

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
            if (megaGemCore)
            {
                Player.moveSpeed += .2f;
            }
            if (Player.statLife <= Player.statLifeMax2 / 2 && rushDrive)
            {
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
            if (Player.HasBuff(ModContent.BuffType<Megamerged>()) && !Player.HasBuff(ModContent.BuffType<Overdrive>()))
                Player.lifeRegen += 4;
            if (Player.HasBuff(ModContent.BuffType<SoulInfused>()))
                Player.lifeRegen += 4;
            if (areusKey)
                Player.lifeRegen *= 2;
        }

        private void UpdateResource()
        {
            if (Player.HasBuff(ModContent.BuffType<Overdrive>()) && !Player.GetModPlayer<DecaPlayer>().modelDeca)
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
            if (ShardsOfAtheria.QuickTest.JustPressed /*&& (Player.name == "AceOfSpades2370" || Player.name == "The Eternal Ace")*/)
            {
                ModContent.GetInstance<ShardsDownedSystem>().slainTwins = false;
                ModContent.GetInstance<ShardsDownedSystem>().slainPrime = false;
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
                if (Player.HasBuff(ModContent.BuffType<Megamerged>()) && !Player.HasBuff(ModContent.BuffType<Overdrive>()) && overdriveTimeCurrent >= 0)
                {
                    Player.AddBuff(ModContent.BuffType<Overdrive>(), 2);
                    CombatText.NewText(Player.Hitbox, Color.Green, "Overdrive: ON", true);
                    SoundEngine.PlaySound(SoundID.Item4, Player.position);
                }
                else if (Player.HasBuff(ModContent.BuffType<Megamerged>()))
                {
                    Player.ClearBuff(ModContent.BuffType<Overdrive>());
                    CombatText.NewText(Player.Hitbox, Color.Red, "Overdrive: OFF");
                }
            }
            if (ShardsOfAtheria.EmeraldTeleportKey.JustPressed)
            {
                if (superEmeraldCore)
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
                if (megaGemCore)
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

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            inCombat = 300;
            if (valkyrieCrown)
                target.AddBuff(ModContent.BuffType<ElectricShock>(), 60);
            if (areusBatteryElectrify)
                target.AddBuff(ModContent.BuffType<ElectricShock>(), 10 * 60);
            if (greaterRubyCore)
                target.AddBuff(BuffID.OnFire, 10 * 60);
            if (superRubyCore)
            {
                target.AddBuff(BuffID.CursedInferno, 10 * 60);
                target.AddBuff(BuffID.Ichor, 10 * 60);
            }
            if (megaGemCore)
            {
                target.AddBuff(BuffID.Daybreak, 10 * 60);
                target.AddBuff(BuffID.BetsysCurse, 10 * 60);
                Player.AddBuff(BuffID.Ironskin, 10 * 60);
                Player.AddBuff(BuffID.Endurance, 10 * 60);
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            inCombat = 300;
            if (proj.owner == Player.whoAmI)
            {
                if (valkyrieCrown)
                    target.AddBuff(ModContent.BuffType<ElectricShock>(), 60);
                if (areusBatteryElectrify)
                    target.AddBuff(ModContent.BuffType<ElectricShock>(), 10 * 60);
                if (greaterRubyCore)
                    target.AddBuff(BuffID.OnFire, 10 * 60);
                if (superRubyCore)
                {
                    target.AddBuff(BuffID.CursedInferno, 10 * 60);
                    target.AddBuff(BuffID.Ichor, 10 * 60);
                }
                if (megaGemCore)
                {
                    target.AddBuff(BuffID.Daybreak, 10 * 60);
                    target.AddBuff(BuffID.BetsysCurse, 10 * 60);
                    Player.AddBuff(BuffID.Ironskin, 10 * 60);
                    Player.AddBuff(BuffID.Endurance, 10 * 60);
                }
                if (hallowedSeal && proj.DamageType == DamageClass.Melee)
                    Player.statMana += 15;
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
            }
        }

        public override void FrameEffects()
        {
            if ((Biometal || BiometalForceVanity) && !BiometalHideVanity)
            {
                var exampleCostume = ModContent.GetInstance<Biometal>();
                Player.head = EquipLoader.GetEquipSlot(Mod, exampleCostume.Name, EquipType.Head);
                Player.body = EquipLoader.GetEquipSlot(Mod, exampleCostume.Name, EquipType.Body);
                Player.legs = EquipLoader.GetEquipSlot(Mod, exampleCostume.Name, EquipType.Legs);

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
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource, ref int cooldownCounter)
        {
            if (!Player.immune)
            {
                if (Player.whoAmI == Main.myPlayer && lesserSapphireCore && Main.rand.NextFloat() < 0.05f)
                {
                    Player.immune = true;
                    Player.immuneTime = 60;
                    return false;
                }
                if (Player.whoAmI == Main.myPlayer && sapphireCore && Main.rand.NextFloat() < 0.1f)
                {
                    Player.immune = true;
                    Player.immuneTime = 60;
                    return false;
                }
                if (Player.whoAmI == Main.myPlayer && superSapphireCore && Main.rand.NextFloat() < 0.15f)
                {
                    Player.immune = true;
                    Player.immuneTime = 60;
                    return false;
                }
                if (Player.whoAmI == Main.myPlayer && megaGemCore && Main.rand.NextFloat() < 0.2f)
                {
                    Player.immune = true;
                    Player.immuneTime = 60;
                    return false;
                }
                if (Player.whoAmI == Main.myPlayer && shadowBrand && shadowBrandToggled && Main.rand.NextFloat() < .1f)
                {
                    Player.immune = true;
                    Player.immuneTime = 60;
                    return false;
                }
            }
            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource, ref cooldownCounter);
        }

        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit, int cooldownCounter)
        {
            if (Player.HasBuff(ModContent.BuffType<Megamerged>()))
                SoundEngine.PlaySound(SoundID.NPCHit4, Player.position);
        }

        public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit, int cooldownCounter)
        {
            inCombat = 300;
            if (Player.HasBuff(ModContent.BuffType<Overdrive>()))
                Player.ClearBuff(ModContent.BuffType<Overdrive>());
            if (megaGemCore)
            {
                Player.AddBuff(BuffID.Rage, 10 * 60);
                Player.AddBuff(BuffID.Wrath, 10 * 60);
                Player.AddBuff(BuffID.Inferno, 10 * 60);
            }
            if (superSapphireCore)
                Player.AddBuff(BuffID.Inferno, 10 * 60);
            if (Player.HasItem(ModContent.ItemType<WandOfHealing>()) && !Player.HasBuff(ModContent.BuffType<HeartBreak>()))
                Player.AddBuff(ModContent.BuffType<HeartBreak>(), 120);
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