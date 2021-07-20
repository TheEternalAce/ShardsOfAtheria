using Microsoft.Xna.Framework;
using SagesMania.Buffs;
using SagesMania.Items;
using SagesMania.Projectiles;
using SagesMania.Projectiles.Ammo;
using SagesMania.Projectiles.Minions;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace SagesMania
{
    public class SMPlayer : ModPlayer
    {
        public bool areusBatteryElectrify;
        public bool areusWings;
        public bool BBBottle;
        public bool PhantomBulletBottle;
        public bool Co2Cartridge;
        public bool lesserSapphireCore;
        public bool sapphireCore;
        public bool superSapphireCore;
        public bool greaterSapphireCore;
        public bool greaterRubyCore;
        public bool superRubyCore;
        public bool OrangeMask;
        public bool livingMetal;
        public bool omnicientTome;
        public bool baseConservation;
        public bool sapphireMinion;
        public bool lesserEmeraldCore;
        public bool emeraldCore;
        public bool superEmeraldCore;
        public bool areusKey;
        public bool unshackledTome;
        public bool megaGemCore;
        public bool shadowBrand;
        public bool hallowedSeal;
        public bool zenovaJavelin;
        public bool heartBreak;
        public bool sMHealingItem;
        public bool phaseOffense;
        public bool rushDrive;
        public bool omegaDrive;
        public bool markOfAnastasia;
        public bool megaGemCoreGrav;
        public bool areusChargePack;
        public bool valkyrieCrown;


        //Slayer mode stuff
        public bool creeperPet;
        public bool honeyCrown;
        public bool honeybeeMinion;
        public bool vampiricJaw;
        public bool plantCells;
        public bool moonCore;
        public bool spiderClock;
        public Vector2 recentPos;
        public int recentLife;
        public int recentMana;
        public int recentCharge;
        public int saveTimer;

        public int TomeKnowledge;
        public int shadowBrandToggled;
        public int megamergedTimer;

        // Here we include a custom resource, similar to mana or health.
        // Creating some variables to define the current value of our example resource as well as the current maximum value. We also include a temporary max value, as well as some variables to handle the natural regeneration of this resource.
        public bool areusWeapon;
        public int areusResourceCurrent;
        public const int DefaultAreusResourceMax = 100;
        public int areusResourceMax;
        public int areusResourceMax2;
        internal int areusResourceRegenTimer = 0;
        public int areusResourceStartRegenTimer;
        public static readonly Color HealAreusResource = new Color(0, 255, 255); // We can use this for CombatText, if you create an item that replenishes exampleResourceCurrent.

        public bool naturalAreusRegen;
        public bool areusChargeMaxed;

        public int overdriveTimeCurrent;
        public const int DefaultOverdriveTimeMax = 300;
        public int overdriveTimeMax;
        public int overdriveTimeMax2;
        internal int overdriveTimeRegenTimer = 0;

        public override void ResetEffects()
        {
            areusBatteryElectrify = false;
            areusWings = false;
            BBBottle = false;
            PhantomBulletBottle = false;
            Co2Cartridge = false;
            lesserSapphireCore = false;
            sapphireCore = false;
            superSapphireCore = false;
            greaterSapphireCore = false;
            greaterRubyCore = false;
            superRubyCore = false;
            OrangeMask = false;
            livingMetal = false;
            omnicientTome = false;
            baseConservation = false;
            sapphireMinion = false;
            superEmeraldCore = false;
            areusKey = false;
            unshackledTome = false;
            megaGemCore = false;
            shadowBrand = false;
            hallowedSeal = false;
            zenovaJavelin = false;
            heartBreak = false;
            sMHealingItem = false;
            rushDrive = false;
            omegaDrive = false;
            valkyrieCrown = false;


            //Slayer mode stuff
            creeperPet = false;
            honeyCrown = false;
            honeybeeMinion = false;
            vampiricJaw = false;
            plantCells = false;
            moonCore = false;
            spiderClock = false;

            ResetVariables();
            naturalAreusRegen = false;
            areusWeapon = false;
        }

        public override void UpdateDead()
        {
            ResetVariables();
        }

        private void ResetVariables()
        {
            areusResourceMax2 = areusResourceMax;
            overdriveTimeMax2 = overdriveTimeMax;
        }

        public override void Initialize()
        {
            megamergedTimer = 54000;
            overdriveTimeCurrent = 300;
            areusResourceMax = DefaultAreusResourceMax;
            overdriveTimeMax = DefaultOverdriveTimeMax;
        }

        public override TagCompound Save()
        {
            return new TagCompound {
                {"megaGemCoreGrav", megaGemCoreGrav},
                {"TomeKnowledge", TomeKnowledge},
                {"shadowBrandToggled", shadowBrandToggled},
                { "areusResourceCurrent", areusResourceCurrent},
                { "overdriveTimeCurrent", overdriveTimeCurrent},
                { "phaseOffense", phaseOffense}
            };
        }

        public override void Load(TagCompound tag)
        {
            megaGemCoreGrav = tag.GetBool("megaGemCoreGrav");
            TomeKnowledge = tag.GetInt("TomeKnowledge");
            shadowBrandToggled = tag.GetInt("shadowBrandToggled");
            areusResourceCurrent = tag.GetInt("areusResourceCurrent");
            overdriveTimeCurrent = tag.GetInt("overdriveTimeCurrent");
            phaseOffense = tag.GetBool("phaseOffense");
        }

        public override void SetupStartInventory(IList<Item> items, bool mediumcoreDeath)
        {
            Item item = new Item();
            item.SetDefaults(ModContent.ItemType<SlayersEmblem>());
            item.stack = 1;
            items.Add(item);
        }

        public override void PreUpdate()
        {
            UpdateResource();
            if (OrangeMask)
            {
                player.statDefense += 7;
                player.rangedDamage += .1f;
                player.rangedCrit += 4;
            }
            if (omnicientTome)
            {
                if (TomeKnowledge == 0)
                {
                    player.AddBuff(ModContent.BuffType<BaseCombat>(), 2);
                }
                else if (TomeKnowledge == 1)
                {
                    player.AddBuff(ModContent.BuffType<BaseConservation>(), 2);
                }
                else if (TomeKnowledge == 2)
                {
                    player.AddBuff(ModContent.BuffType<BaseExploration>(), 2);
                    player.AddBuff(BuffID.Mining, 2);
                    player.AddBuff(BuffID.Builder, 2);
                    player.AddBuff(BuffID.Shine, 2);
                    player.AddBuff(BuffID.Hunter, 2);
                    player.AddBuff(BuffID.Spelunker, 2);
                }
            }
            if (unshackledTome)
            {
                if (!player.GetModPlayer<SMPlayer>().areusKey)
                {
                    player.AddBuff(BuffID.ChaosState, 10 * 60);
                    player.AddBuff(BuffID.Confused, 10 * 60);
                    player.AddBuff(BuffID.ManaSickness, 10 * 60);
                    player.AddBuff(BuffID.Poisoned, 10 * 60);
                    player.AddBuff(BuffID.Obstructed, 10 * 60);
                    player.AddBuff(BuffID.MoonLeech, 10 * 60);
                    player.AddBuff(BuffID.BrokenArmor, 10 * 60);
                    player.AddBuff(BuffID.Weak, 10 * 60);
                    player.AddBuff(164, 10 * 60);
                }
            }
            if (Config.sapphireMinion)
            {
                if (player.ownedProjectileCounts[ModContent.ProjectileType<SapphireSpiritMinion>()] <= 0 && greaterSapphireCore)
                {
                    Projectile.NewProjectile(player.position, player.velocity, ModContent.ProjectileType<SapphireSpiritMinion>(), 80, 5, player.whoAmI);
                }
                if (player.ownedProjectileCounts[ModContent.ProjectileType<SapphireSpiritMinion>()] <= 0 && superSapphireCore)
                {
                    Projectile.NewProjectile(player.position, player.velocity, ModContent.ProjectileType<SapphireSpiritMinion>(), 157, 5, player.whoAmI);
                }
                if (player.ownedProjectileCounts[ModContent.ProjectileType<SapphireSpiritMinion>()] <= 0 && megaGemCore)
                {
                    Projectile.NewProjectile(player.position, player.velocity, ModContent.ProjectileType<SapphireSpiritMinion>(), 267, 5, player.whoAmI);
                }
            }
            if (ModContent.GetInstance<SMWorld>().flightToggle && (megaGemCore || areusWings))
            {
                player.wingTime = 100;
                player.rocketTime = 100;
            }
            if (player.ownedProjectileCounts[ModContent.ProjectileType<HoneybeeMinion>()] <= 0 && honeyCrown)
            {
                Projectile.NewProjectile(player.position, player.velocity, ModContent.ProjectileType<HoneybeeMinion>(), 30, 5, player.whoAmI);
            }
            if (spiderClock)
            {
                saveTimer++;
                if (saveTimer == 300)
                {
                    recentPos = player.position;
                    recentLife = player.statLife;
                    recentMana = player.statMana;
                    recentCharge = areusResourceCurrent;
                    CombatText.NewText(player.Hitbox, Color.Gray, "Time shift ready");
                }
                if (saveTimer >= 302)
                    saveTimer = 302;
            }
            else saveTimer = 0;
        }

        public override void PostUpdate()
        {
            if (player.HasBuff(ModContent.BuffType<Overdrive>()) || omegaDrive)
            {
                player.armorEffectDrawShadow = true;
                player.armorEffectDrawOutlines = true;
                player.buffImmune[BuffID.Regeneration] = true;
                player.buffImmune[BuffID.Honey] = true;
                player.buffImmune[BuffID.Campfire] = true;
                player.buffImmune[BuffID.HeartLamp] = true;
                player.shinyStone = false;
            }
            if (!ModContent.GetInstance<SMWorld>().flightToggle)
            {
                player.wingTime = 0;
                if (Config.NoRocketFlightToggle)
                    player.rocketTime = 0;
            }
            if (!player.HasBuff(ModContent.BuffType<Megamerged>()))
            {
                megamergedTimer++;
                if (megamergedTimer >= 54000) megamergedTimer = 54000;
                player.ClearBuff(ModContent.BuffType<Overdrive>());
                player.ClearBuff(ModContent.BuffType<Overheat>());
                player.buffImmune[ModContent.BuffType<Overdrive>()] = true;
                player.buffImmune[ModContent.BuffType<Overheat>()] = true;
            }
            else
            {
                megamergedTimer--;
                if (megamergedTimer <= 0) player.AddBuff(ModContent.BuffType<Overheat>(), 2);
            }
            if (!livingMetal)
            {
                player.ClearBuff(ModContent.BuffType<Megamerged>());
                player.buffImmune[ModContent.BuffType<Megamerged>()] = true;
            }
            if (ModContent.GetInstance<SMWorld>().slayerMode)
            {
                player.statDefense /= 2;
                player.endurance /= 2;
            }
            if (omegaDrive || (player.statLife <= player.statLifeMax2 / 2 && rushDrive))
            {
                if (player.GetModPlayer<SMPlayer>().phaseOffense)
                {
                    player.statDefense /= 2;
                    player.allDamageMult += 2f;
                    player.meleeCrit += 20;
                    player.magicCrit += 20;
                    player.rangedCrit += 20;
                }
                else
                {
                    player.statDefense *= 2;
                    player.endurance += .2f;
                    player.allDamageMult *= .5f;
                }
            }
        }

        public override void PostUpdateRunSpeeds()
        {
            if (player.HasBuff(ModContent.BuffType<BaseExploration>()))
                player.moveSpeed += .1f;
            if (player.HasBuff(ModContent.BuffType<Infection>()))
                player.moveSpeed -= .5f;
            if (player.HasBuff(ModContent.BuffType<Megamerged>()))
                player.moveSpeed += 1;
            if (player.HasBuff(ModContent.BuffType<Overdrive>()))
                player.moveSpeed += .5f;
            if (player.HasBuff(ModContent.BuffType<Overheat>()))
                player.moveSpeed -= .5f;
            if (player.HasBuff(ModContent.BuffType<SoulInfused>()))
                player.moveSpeed += .5f;
            if (areusKey)
                player.moveSpeed += .5f;
            if (lesserEmeraldCore)
                player.moveSpeed += .05f;
            if (emeraldCore)
                player.moveSpeed += .1f;
            if (superEmeraldCore)
                player.moveSpeed += 1f;
            if (megaGemCore)
                player.moveSpeed += 1f;
            if (markOfAnastasia)
            {
                if (player.name == "Sophie" || player.name == "Lilly" || player.name == "Damien"
                || player.name == "Ariiannah" || player.name == "Arii" || player.name == "Peter"
                || player.name == "Shane")
                    player.moveSpeed += 1f;
            }
            else player.moveSpeed += .5f;
            if (omegaDrive)
                player.moveSpeed += 1f;
            if (omegaDrive || (player.statLife <= player.statLifeMax2 / 2 && rushDrive))
                player.moveSpeed += .2f;
        }

        public override void UpdateLifeRegen()
        {
            if (player.HasBuff(ModContent.BuffType<Megamerged>()) && !player.HasBuff(ModContent.BuffType<Overdrive>()))
                player.lifeRegen += 4;
            if (player.HasBuff(ModContent.BuffType<SoulInfused>()))
                player.lifeRegen += 4;
            if (areusKey)
                player.lifeRegen *= 2;
            if (omegaDrive)
                player.lifeRegen += 4;
            if (plantCells)
                if (player.ZoneOverworldHeight)
                    player.lifeRegen += 15;
                else player.lifeRegen += 10;
        }

        private void UpdateResource()
        {
            if (areusWeapon)
            {
                if (naturalAreusRegen && player.itemAnimation == 0)
                {
                    areusResourceStartRegenTimer++;
                    if (areusResourceStartRegenTimer > 180)
                    {
                        // For our resource lets make it regen slowly over time to keep it simple, let's use exampleResourceRegenTimer to count up to whatever value we want, then increase currentResource.
                        areusResourceRegenTimer++; //Increase it by 60 per second, or 1 per tick.

                        // A simple timer that goes up to 1/5 second, increases the exampleResourceCurrent by 1 and then resets back to 0.
                        if (areusResourceRegenTimer > 12)
                        {
                            areusResourceCurrent += 1;
                            areusResourceRegenTimer = 0;
                        }
                    }

                    // Limit exampleResourceCurrent from going over the limit imposed by exampleResourceMax.
                    areusResourceCurrent = Utils.Clamp(areusResourceCurrent, 0, areusResourceMax2);
                }
                else areusResourceStartRegenTimer = 0;
            }
            else if (naturalAreusRegen)
            {
                areusResourceStartRegenTimer++;
                if (areusResourceStartRegenTimer > 180)
                {
                    // For our resource lets make it regen slowly over time to keep it simple, let's use exampleResourceRegenTimer to count up to whatever value we want, then increase currentResource.
                    areusResourceRegenTimer++; //Increase it by 60 per second, or 1 per tick.

                    // A simple timer that goes up to 1/5 second, increases the exampleResourceCurrent by 1 and then resets back to 0.
                    if (areusResourceRegenTimer > 12)
                    {
                        areusResourceCurrent += 1;
                        areusResourceRegenTimer = 0;
                    }
                }

                // Limit exampleResourceCurrent from going over the limit imposed by exampleResourceMax.
                areusResourceCurrent = Utils.Clamp(areusResourceCurrent, 0, areusResourceMax2);
            }
            else areusResourceStartRegenTimer = 0;
            if (areusResourceCurrent >= areusResourceMax2 && ModLoader.GetMod("TerrariaOverhaul") == null)
            {
                areusResourceCurrent = areusResourceMax2;
                if (!areusChargeMaxed)
                {
                    Main.PlaySound(SoundID.NPCHit53, player.position);
                    CombatText.NewText(player.Hitbox, Color.Cyan, "Charged");
                    areusChargeMaxed = true;
                }
            }
            else areusChargeMaxed = false;

            if (ModLoader.GetMod("TerrariaOverhaul") != null)
            {
                areusResourceCurrent = areusResourceMax2;
            }

            if (player.HasBuff(ModContent.BuffType<Overdrive>()) && !omegaDrive)
            {
                // For our resource lets make it regen slowly over time to keep it simple, let's use exampleResourceRegenTimer to count up to whatever value we want, then increase currentResource.
                overdriveTimeRegenTimer++; //Increase it by 60 per second, or 1 per tick.

                // A simple timer that goes up to 3 seconds, increases the exampleResourceCurrent by 1 and then resets back to 0.
                if (overdriveTimeRegenTimer > 60)
                {
                    overdriveTimeCurrent -= 1;
                    overdriveTimeRegenTimer = 0;
                }
            }
        }

        public override bool ConsumeAmmo(Item weapon, Item ammo)
        {
            if (BBBottle)
                return Main.rand.NextFloat() >= .05f;
            if (PhantomBulletBottle)
                return Main.rand.NextFloat() >= .48f;
            if (baseConservation)
                return Main.rand.NextFloat() >= .15f;
            return true;
        }

        public override bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (PhantomBulletBottle && item.ranged && item.useAmmo == AmmoID.Bullet)
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<PhantomBullet>(), 300, knockBack, player.whoAmI);
            if (BBBottle && item.ranged && item.useAmmo == AmmoID.Bullet)
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<BBProjectile>(), 4, knockBack, player.whoAmI);
            if (Co2Cartridge)
            {
                if (type == ModContent.ProjectileType<BBProjectile>())
                {
                    type = ProjectileID.BulletHighVelocity;
                }
                return true;
            }
            return true;
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (SagesMania.OverdriveKey.JustPressed)
            {
                if (player.HasBuff(ModContent.BuffType<Megamerged>()) && !player.HasBuff(ModContent.BuffType<Overdrive>()) && overdriveTimeCurrent >= 0)
                {
                    if (!player.HasBuff(ModContent.BuffType<Overheat>()))
                    {
                        player.AddBuff(ModContent.BuffType<Overdrive>(), 2);
                        CombatText.NewText(player.Hitbox, Color.Green, "Overdrive: ON", true);
                        Main.PlaySound(SoundID.Item4, player.position);
                    }
                    else
                    {
                        CombatText.NewText(player.Hitbox, Color.Red, "OVERHEATED!");
                        Main.PlaySound(SoundID.NPCHit55, player.position);
                    }
                }
                else if (player.HasBuff(ModContent.BuffType<Megamerged>()))
                {
                    player.ClearBuff(ModContent.BuffType<Overdrive>());
                    CombatText.NewText(player.Hitbox, Color.Red, "Overdrive: OFF");
                    Main.PlaySound(SoundID.NPCDeath56, player.position);
                }
            }
            if (SagesMania.TomeKey.JustPressed)
            {
                if (omnicientTome)
                {
                    if (TomeKnowledge == 2)
                    {
                        TomeKnowledge = 0;
                    }
                    else TomeKnowledge += 1;
                    Main.PlaySound(SoundID.Item1, player.position);
                }
            }
            if (SagesMania.EmeraldTeleportKey.JustPressed)
            {
                if (superEmeraldCore)
                {
                    Vector2 vector21 = default(Vector2);
                    vector21.X = (float)Main.mouseX + Main.screenPosition.X;
                    if (player.gravDir == 1f)
                    {
                        vector21.Y = (float)Main.mouseY + Main.screenPosition.Y - (float)player.height;
                    }
                    else
                    {
                        vector21.Y = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY;
                    }
                    vector21.X -= player.width / 2;
                    if (vector21.X > 50f && vector21.X < (float)(Main.maxTilesX * 16 - 50) && vector21.Y > 50f && vector21.Y < (float)(Main.maxTilesY * 16 - 50))
                    {
                        int num181 = (int)(vector21.X / 16f);
                        int num182 = (int)(vector21.Y / 16f);
                        if ((Main.tile[num181, num182].wall != 87 || !((double)num182 > Main.worldSurface) || NPC.downedPlantBoss) && !Collision.SolidCollision(vector21, player.width, player.height))
                        {
                            player.Teleport(vector21, 1);
                            NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, player.whoAmI, vector21.X, vector21.Y, 1);
                            if (player.chaosState)
                            {
                                player.statLife -= player.statLifeMax2 / 7;
                                PlayerDeathReason damageSource = PlayerDeathReason.ByOther(13);
                                if (Main.rand.Next(2) == 0)
                                {
                                    damageSource = PlayerDeathReason.ByOther(player.Male ? 14 : 15);
                                }
                                if (player.statLife <= 0)
                                {
                                    player.KillMe(damageSource, 1.0, 0);
                                }
                                player.lifeRegenCount = 0;
                                player.lifeRegenTime = 0;
                            }
                            player.AddBuff(BuffID.ChaosState, 360);
                        }
                    }
                }
                if (megaGemCore)
                {
                    Vector2 vector21 = default(Vector2);
                    vector21.X = (float)Main.mouseX + Main.screenPosition.X;
                    if (player.gravDir == 1f)
                    {
                        vector21.Y = (float)Main.mouseY + Main.screenPosition.Y - (float)player.height;
                    }
                    else
                    {
                        vector21.Y = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY;
                    }
                    vector21.X -= player.width / 2;
                    if (vector21.X > 50f && vector21.X < (float)(Main.maxTilesX * 16 - 50) && vector21.Y > 50f && vector21.Y < (float)(Main.maxTilesY * 16 - 50))
                    {
                        int num181 = (int)(vector21.X / 16f);
                        int num182 = (int)(vector21.Y / 16f);
                        if ((Main.tile[num181, num182].wall != 87 || !((double)num182 > Main.worldSurface) || NPC.downedPlantBoss) && !Collision.SolidCollision(vector21, player.width, player.height))
                        {
                            player.Teleport(vector21, 1);
                            NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, player.whoAmI, vector21.X, vector21.Y, 1);
                            if (player.chaosState)
                            {
                                player.statLife -= player.statLifeMax2 / 7;
                                PlayerDeathReason damageSource = PlayerDeathReason.ByOther(13);
                                if (Main.rand.Next(2) == 0)
                                {
                                    damageSource = PlayerDeathReason.ByOther(player.Male ? 14 : 15);
                                }
                                if (player.statLife <= 0)
                                {
                                    player.KillMe(damageSource, 1.0, 0);
                                }
                                player.lifeRegenCount = 0;
                                player.lifeRegenTime = 0;
                            }
                            player.AddBuff(BuffID.ChaosState, 360);
                        }
                    }
                }
            }
            if (SagesMania.ShadowCloak.JustPressed)
            {
                if (shadowBrand)
                {
                    if (shadowBrandToggled == 1)
                    {
                        shadowBrandToggled = 0;
                    }
                    else shadowBrandToggled = 1;
                }
            }
            if (SagesMania.ShadowTeleport.JustPressed)
            {
                if (shadowBrand && player.HasBuff(ModContent.BuffType<ShadowTeleport>()))
                {
                    Vector2 vector21 = default(Vector2);
                    vector21.X = (float)Main.mouseX + Main.screenPosition.X;
                    if (player.gravDir == 1f)
                    {
                        vector21.Y = (float)Main.mouseY + Main.screenPosition.Y - (float)player.height;
                    }
                    else
                    {
                        vector21.Y = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY;
                    }
                    vector21.X -= player.width / 2;
                    if (vector21.X > 50f && vector21.X < (float)(Main.maxTilesX * 16 - 50) && vector21.Y > 50f && vector21.Y < (float)(Main.maxTilesY * 16 - 50))
                    {
                        int num181 = (int)(vector21.X / 16f);
                        int num182 = (int)(vector21.Y / 16f);
                        if ((Main.tile[num181, num182].wall != 87 || !((double)num182 > Main.worldSurface) || NPC.downedPlantBoss) && !Collision.SolidCollision(vector21, player.width, player.height))
                        {
                            player.Teleport(vector21, 1);
                            NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, player.whoAmI, vector21.X, vector21.Y, 1);
                            player.ClearBuff(ModContent.BuffType<ShadowTeleport>());
                        }
                    }
                }
            }
            if (SagesMania.PhaseSwitch.JustPressed)
            {
                if (player.statLife >= player.statLifeMax2 / 2)
                {
                    if (phaseOffense)
                    {
                        phaseOffense = false;
                        Main.NewText("Phase 2 Type: Defensive");
                    }
                    else
                    {
                        phaseOffense = true;
                        Main.NewText("Phase 2 Type: Offensive");
                    }
                }
            }
            if (SagesMania.QuickCharge.JustPressed && areusChargePack)
            {
                int areusChargePackIndex = Main.LocalPlayer.FindItem(ModContent.ItemType<AreusChargePack>());
                Main.LocalPlayer.inventory[areusChargePackIndex].stack--;
                Main.PlaySound(SoundID.NPCHit53);
                areusResourceCurrent += 50;
                CombatText.NewText(player.Hitbox, Color.Aqua, 50);
            }
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
         {
            if (valkyrieCrown)
                target.AddBuff(BuffID.Electrified, 60);
            if (areusBatteryElectrify)
                target.AddBuff(BuffID.Electrified, 10 * 60);
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
                player.AddBuff(BuffID.Ironskin, 10 * 60);
                player.AddBuff(BuffID.Endurance, 10 * 60);
            }
            if (hallowedSeal && item.melee)
                player.statMana += 15;
            if (vampiricJaw && item.melee && !item.noMelee)
            {
                player.HealEffect(item.damage / 5);
                player.statLife += item.damage / 5;
            }
            if (moonCore)
            {
                player.HealEffect(item.damage / 2);
                player.statLife += item.damage / 2;
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (proj.owner == player.whoAmI)
            {
                if (valkyrieCrown)
                    target.AddBuff(BuffID.Electrified, 60);
                if (areusBatteryElectrify)
                    target.AddBuff(BuffID.Electrified, 10 * 60);
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
                    player.AddBuff(BuffID.Ironskin, 10 * 60);
                    player.AddBuff(BuffID.Endurance, 10 * 60);
                }
                if (hallowedSeal && proj.melee)
                    player.statMana += 15;
                if (moonCore)
                {
                    player.HealEffect(proj.damage / 10);
                    player.statLife += proj.damage / 10;
                }
            }
        }

        public override bool? CanHitNPC(Item item, NPC target)
        {
            if (target.friendly && omegaDrive)
                return true;
            else return base.CanHitNPC(item, target);
        }

        public override bool? CanHitNPCWithProj(Projectile proj, NPC target)
        {
            if (target.friendly && omegaDrive)
                return true;
            else return base.CanHitNPCWithProj(proj, target);
        }

        public override void FrameEffects()
        {
            if (Config.MegamergeVisual)
            {
                if (player.HasBuff(ModContent.BuffType<Megamerged>()))
                {
                    player.handon = -1;
                    player.handoff = -1;
                    player.back = -1;
                    player.front = -1;
                    player.shoe = -1;
                    player.waist = -1;
                    player.shield = -1;
                    player.neck = -1;
                    player.face = -1;
                    player.balloon = -1;

                    player.head = mod.GetEquipSlot("LivingMetalHead", EquipType.Head);
                    player.body = mod.GetEquipSlot("LivingMetalBody", EquipType.Body);
                    player.legs = mod.GetEquipSlot("LivingMetalLegs", EquipType.Legs);

                }
                if (omegaDrive)
                {
                    player.handon = -1;
                    player.handoff = -1;
                    player.back = -1;
                    player.front = -1;
                    player.shoe = -1;
                    player.waist = -1;
                    player.shield = -1;
                    player.neck = -1;
                    player.face = -1;
                    player.balloon = -1;

                    player.head = mod.GetEquipSlot("OmegaMetalHead", EquipType.Head);
                    player.body = mod.GetEquipSlot("OmegaMetalBody", EquipType.Body);
                    player.legs = mod.GetEquipSlot("OmegaMetalLegs", EquipType.Legs);
                }
            }
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (lesserSapphireCore && Main.rand.NextFloat() < 0.05f)
            {
                CombatText.NewText(player.Hitbox, Color.RoyalBlue, "Dodge!", true);
                player.immune = true;
                player.immuneTime = 60;
                return false;
            }
            if (sapphireCore && Main.rand.NextFloat() < 0.1f)
            {
                CombatText.NewText(player.Hitbox, Color.RoyalBlue, "Dodge!", true);
                player.immune = true;
                player.immuneTime = 60;
                return false;
            }
            if (superSapphireCore && Main.rand.NextFloat() < 0.15f)
            {
                CombatText.NewText(player.Hitbox, Color.RoyalBlue, "Dodge!", true);
                player.immune = true;
                player.immuneTime = 60;
                return false;
            }
            if (megaGemCore && Main.rand.NextFloat() < 0.2f)
            {
                CombatText.NewText(player.Hitbox, Color.RoyalBlue, "Dodge!", true);
                player.immune = true;
                player.immuneTime = 60;
                return false;
            }
            if (shadowBrand && shadowBrandToggled == 0 && Main.rand.NextFloat() < .1f)
            {
                player.immune = true;
                player.immuneTime = 60;
                player.AddBuff(ModContent.BuffType<ShadowTeleport>(), 2);
                return false;
            }
            else return true;
        }

        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            if (player.HasBuff(ModContent.BuffType<Megamerged>()) || omegaDrive)
                Main.PlaySound(SoundID.NPCHit4, player.position);
        }

        public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            if (player.HasBuff(ModContent.BuffType<Overdrive>()))
            {
                player.ClearBuff(ModContent.BuffType<Overdrive>());
                player.AddBuff(ModContent.BuffType<Overheat>(), 10 * 60);
                CombatText.NewText(player.Hitbox, Color.Red, "Overdrive: BREAK", true);
                Main.PlaySound(SoundID.NPCDeath44, player.position);
            }
            if (megaGemCore)
            {
                player.AddBuff(BuffID.Rage, 10 * 60);
                player.AddBuff(BuffID.Wrath, 10 * 60);
            }
            if (sMHealingItem && !player.HasBuff(ModContent.BuffType<HeartBreak>()))
                player.AddBuff(ModContent.BuffType<HeartBreak>(), 20 * 60);
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (player.HasBuff(ModContent.BuffType<Overdrive>()))
            {
                damageSource = PlayerDeathReason.ByCustomReason(player.name + " pushed too far.");
            }
            if (player.HasBuff(ModContent.BuffType<Overheat>()))
            {
                damageSource = PlayerDeathReason.ByCustomReason(player.name + "'s Living Metal overheated.");
            }
            return true;
        }

        public override void UpdateBadLifeRegen()
        {
            if (player.HasBuff(ModContent.BuffType<InjectionShock>()))
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes .5 life lost per second.
                player.lifeRegen -= 1;
            }
            if (player.HasBuff(ModContent.BuffType<Overheat>()))
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 10 life lost per second.
                player.lifeRegen -= 20;
            }
            if (player.HasBuff(ModContent.BuffType<Infection>()))
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 5 life lost per second.
                player.lifeRegen -= 10;
            }
            if (player.HasBuff(ModContent.BuffType<ZenovaJavelin>()))
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 50 life lost per second.
                player.lifeRegen -= 100;
            }
        }

        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (player.HasBuff(ModContent.BuffType<Overheat>()))
            {
                if (Main.rand.NextBool(4) && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, DustID.Smoke, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, Color.Black, 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    Main.playerDrawDust.Add(dust);
                }
            }
            if (player.HasBuff(ModContent.BuffType<InjectionShock>()))
            {
                if (Main.rand.NextBool(4) && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, DustID.Blood, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    Main.playerDrawDust.Add(dust);
                }
            }
        }
    }
}