using Microsoft.Xna.Framework;
using SagesMania.Buffs;
using SagesMania.Items.Accessories;
using SagesMania.Projectiles;
using SagesMania.Projectiles.Minions;
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
        public bool superEmeraldCore;
        public bool areusKey;
        public bool unshackledTome;
        public bool megaGemCore;
        public bool shadowBrand;
        public bool hallowedSeal;
        public bool zenovaJavelin;
        public bool heartBreak;
        public bool sMHealingItem;
        public bool rushDrive;

        Vector2 recentPos;

        public int TomeKnowledge;
        public int shadowBrandToggled;
        public int gravToggle;
        public int flightToggle;
        public int megamergedTimer;
        public int phaseSwitch;

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
        }

        public override void Initialize()
        {
            megamergedTimer = 54000;
        }

        public override TagCompound Save()
        {
            return new TagCompound {
				// {"somethingelse", somethingelse}, // To save more data, add additional lines
				{"TomeKnowledge", TomeKnowledge},
                {"shadowBrandToggled", shadowBrandToggled},
                {"gravToggle", gravToggle},
				{"flightToggle", flightToggle},
				{"phaseSwitch", phaseSwitch},
            };
        }

        public override void Load(TagCompound tag)
        {
            TomeKnowledge = tag.GetInt("TomeKnowledge");
            shadowBrandToggled = tag.GetInt("shadowBrandToggled");
            gravToggle = tag.GetInt("gravToggle");
            flightToggle = tag.GetInt("flightToggle");
            phaseSwitch = tag.GetInt("phaseSwitch");
        }

        public override void PostUpdate()
        {
            if (OrangeMask)
            {
                player.statDefense += 7;
                player.rangedDamage += .1f;
                player.rangedCrit += 4;
            }
            if (player.HasBuff(ModContent.BuffType<Overdrive>()))
            {
                player.armorEffectDrawShadow = true;
                player.armorEffectDrawOutlines = true;
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
            if (player.ownedProjectileCounts[ModContent.ProjectileType<SapphireSpiritMinion>()] <= 0 && greaterSapphireCore)
            {
                Projectile.NewProjectile(player.position, player.velocity, ModContent.ProjectileType<SapphireSpiritMinion>(), 80, 5, player.whoAmI);
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
            if (player.ownedProjectileCounts[ModContent.ProjectileType<SapphireSpiritMinion>()] <= 0 && superSapphireCore)
            {
                Projectile.NewProjectile(player.position, player.velocity, ModContent.ProjectileType<SapphireSpiritMinion>(), 157, 5, player.whoAmI);
            }
            if (player.ownedProjectileCounts[ModContent.ProjectileType<SapphireSpiritMinion>()] <= 0 && megaGemCore)
            {
                Projectile.NewProjectile(player.position, player.velocity, ModContent.ProjectileType<SapphireSpiritMinion>(), 267, 5, player.whoAmI);
            }
            if (flightToggle == 1)
            {
                player.wingTime = 0;
                player.rocketTime = 0;
            }
            else if (megaGemCore || areusWings)
            {
                if (player.wingTime == 0)
                    player.wingTime = 100;
                if (player.rocketTime == 0)
                    player.rocketTime = 100;
            }
            if (!player.HasBuff(ModContent.BuffType<Megamerged>()))
            {
                megamergedTimer++;
                if (megamergedTimer >= 54000) megamergedTimer = 54000;
            }
            if (player.HasBuff(ModContent.BuffType<Megamerged>()))
            {
                megamergedTimer--;
                if (megamergedTimer <= 0) player.AddBuff(ModContent.BuffType<Overheat>(), 2);
            }
        }
        
        public override bool ConsumeAmmo(Item weapon, Item ammo)
        {
            if (BBBottle)
            {
                return Main.rand.NextFloat() >= .05f;
            }
            if (PhantomBulletBottle)
            {
                return Main.rand.NextFloat() >= .48f;
            }
            if (baseConservation)
            {
                return Main.rand.NextFloat() >= .15f;
            }
            return true;
        }

        public override bool Shoot(Item item, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (PhantomBulletBottle)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<PhantomBullet>(), 300, knockBack, player.whoAmI);
            }
            if (BBBottle)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<BBProjectile>(), 4, knockBack, player.whoAmI);
            }
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
                if (player.HasBuff(ModContent.BuffType<Megamerged>()) && !player.HasBuff(ModContent.BuffType<Overdrive>()))
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
            if (SagesMania.Megamerge.JustPressed)
            {
                if (livingMetal && !player.HasBuff(ModContent.BuffType<Megamerged>()))
                {
                    if (!player.HasBuff(ModContent.BuffType<MegamergeCooldown>()))
                    {
                        player.AddBuff(ModContent.BuffType<Megamerged>(), 2);
                        CombatText.NewText(player.Hitbox, Color.White, "Megamerge!", true);
                        if (player.Male)
                            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/MegamergeMale"), player.position);
                        else Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/MegamergeFemale"), player.position);
                    }
                    else
                    {
                        CombatText.NewText(player.Hitbox, Color.Red, "On Cooldown!");
                    }
                }
                else if (player.HasBuff(ModContent.BuffType<Megamerged>()))
                {
                    player.ClearBuff(ModContent.BuffType<Megamerged>());
                    Main.PlaySound(SoundID.Item4, player.position);
                }
            }
            if (SagesMania.PhaseSwitch.JustPressed)
            {
                if (player.statLife >= player.statLifeMax2/2)
                {
                    if (phaseSwitch == 1)
                    {
                        phaseSwitch = 0;
                        Main.NewText("Phase 2 Type: Offensive");
                    }
                    else
                    {
                        phaseSwitch += 1;
                        Main.NewText("Phase 2 Type: Defensive");
                    }
                }
            }
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (areusBatteryElectrify)
            {
                target.AddBuff(BuffID.Electrified, 10 * 60);
            }
            if (greaterRubyCore)
            {
                target.AddBuff(BuffID.OnFire, 10 * 60);
            }
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
            {
                player.statMana += 5;
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (areusBatteryElectrify)
            {
                target.AddBuff(BuffID.Electrified, 10 * 60);
            }
            if (greaterRubyCore)
            {
                target.AddBuff(BuffID.OnFire, 10 * 60);
            }
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
            {
                player.statMana += 5;
            }
        }

        public override void FrameEffects()
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

                if (player.name == "Inverted")
                {
                    player.head = mod.GetEquipSlot("InvertedLivingMetalHead", EquipType.Head);
                    player.body = mod.GetEquipSlot("InvertedLivingMetalBody", EquipType.Body);
                    player.legs = mod.GetEquipSlot("InvertedLivingMetalLegs", EquipType.Legs);

                }
                else
                {
                    player.head = mod.GetEquipSlot("LivingMetalHead", EquipType.Head);
                    player.body = mod.GetEquipSlot("LivingMetalBody", EquipType.Body);
                    player.legs = mod.GetEquipSlot("LivingMetalLegs", EquipType.Legs);
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
            if (player.HasBuff(ModContent.BuffType<Megamerged>())) Main.PlaySound(SoundID.NPCHit4, player.position);
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
                if (player.HasBuff(ModContent.BuffType<Megamerged>()))
                    damageSource = PlayerDeathReason.ByCustomReason(player.name + " was Megamerged for too long.");
                else damageSource = PlayerDeathReason.ByCustomReason(player.name + "'s systems overheated.");
            }
            return true;
        }

        public override void UpdateBadLifeRegen()
        {
            if (player.HasBuff(ModContent.BuffType<Overdrive>()))
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 6 life lost per second.
                player.lifeRegen -= 12;
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
            if (player.HasBuff(ModContent.BuffType<Overdrive>()))
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
            if ( player.HasBuff(ModContent.BuffType<Overheat>()))
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
        }
    }
}