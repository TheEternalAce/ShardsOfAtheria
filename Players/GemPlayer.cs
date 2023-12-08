using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.PlayerBuff;
using ShardsOfAtheria.Buffs.Summons;
using ShardsOfAtheria.Items.Accessories.GemCores;
using ShardsOfAtheria.Items.Accessories.GemCores.Greater;
using ShardsOfAtheria.Items.Accessories.GemCores.Regular;
using ShardsOfAtheria.Items.Accessories.GemCores.Super;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Projectiles.Summon.Minions;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Chat;
using Terraria.DataStructures;
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
        public bool amberCape;

        public bool amethystCore;
        public bool greaterAmethystCore;
        public bool amethystBomb;
        public bool amethystMask;

        public bool diamondCore;
        public bool greaterDiamondCore;
        public bool diamondShield;

        public bool lesserEmeraldCore;
        public bool emeraldCore;
        public bool superEmeraldCore;
        public bool emeraldBoots;

        public bool rubyCore;
        public bool greaterRubyCore;
        public bool superRubyCore;
        public bool rubyGauntlet;

        public float sapphireDodgeChance;
        public bool sapphireCore;
        public bool greaterSapphireCore;
        public bool superSapphireCore;
        public bool sapphireSpiritPrevious;
        public bool sapphireSpirit;

        public bool topazCore;
        public bool greaterTopazCore;
        public bool topazNecklace;

        public bool megaGemCore;
        public bool[,] megaGemCoreToggles = {
            { true, true, true, true, true, true, true },
            { true, true, true, true, true, true, true },
            { true, true, true, true, true, true, true }
        };


        public override void ResetEffects()
        {
            amberCore = false;
            amberCape = false;

            amethystCore = false;
            greaterAmethystCore = false;
            //superAmethystCore = false;
            amethystMask = false;

            diamondCore = false;
            greaterDiamondCore = false;
            diamondShield = false;

            lesserEmeraldCore = false;
            superEmeraldCore = false;
            emeraldBoots = false;

            rubyCore = false;
            greaterRubyCore = false;
            superRubyCore = false;
            rubyGauntlet = false;

            sapphireDodgeChance = 0f;
            sapphireCore = false;
            greaterSapphireCore = false;
            superSapphireCore = false;
            sapphireSpiritPrevious = sapphireSpirit;
            sapphireSpirit = false;

            topazNecklace = false;

            megaGemCore = false;
        }

        public override void SaveData(TagCompound tag)
        {
            tag[nameof(megaGemCoreToggles)] = megaGemCoreToggles;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey(nameof(megaGemCoreToggles)))
            {
                megaGemCoreToggles = tag.Get<bool[,]>(nameof(megaGemCoreToggles));
            }
        }

        public override void UpdateEquips()
        {
            if (sapphireSpirit)
            {
                int type = ModContent.ProjectileType<SapphireSpirit>();
                if (Player.ownedProjectileCounts[type] == 0)
                {
                    Item core = ModContent.GetInstance<SapphireCore>().Item;
                    int damage = 0;
                    if (superSapphireCore)
                    {
                        damage = 50;
                    }
                    Projectile.NewProjectile(Player.GetSource_Accessory(core), Player.Center,
                        Vector2.Zero, type, damage, 0, Player.whoAmI);
                }
            }
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
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (SoA.AmethystBombToggle.JustPressed)
            {
                amethystBomb = !amethystBomb;
                string key = "Mods.ShardsOfAtheria.Items.AmethystCore_Greater.Toggle";
                if (amethystBomb)
                {
                    key += "On";
                }
                else
                {
                    key += "Off";
                }
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    Main.NewText(Language.GetOrRegister(key));
                }
                else
                {
                    ChatHelper.SendChatMessageToClient(NetworkText.FromKey(key), Color.Wheat, Player.whoAmI);
                }
            }
            if (SoA.EmeraldTeleportKey.JustPressed)
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
                            NetMessage.SendData(MessageID.TeleportEntity, -1, -1, null, 0, Player.whoAmI, vector21.X, vector21.Y, 1);
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
                    amberCape = megaGemCoreToggles[loadout, 0];
                    amethystMask = megaGemCoreToggles[loadout, 1];
                    diamondShield = megaGemCoreToggles[loadout, 2];
                    emeraldBoots = megaGemCoreToggles[loadout, 3];
                    rubyGauntlet = megaGemCoreToggles[loadout, 4];
                    sapphireSpirit = megaGemCoreToggles[loadout, 5];
                    topazNecklace = megaGemCoreToggles[loadout, 6];
                }
                else
                {
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
                //Player.head = EquipLoader.GetEquipSlot(Mod, "Amber", EquipType.);
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
                TrySapphireDodge(sapphireDodgeChance);
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
            int normalBuffTime = 600;
            if (megaGemCore)
            {
                normalBuffTime += 300;
            }
            if (amberCore)
            {
                Player.AddBuff<SwarmingAmber>(normalBuffTime * 3);
                int type = ModContent.ProjectileType<AmberFly>();
                if (Player.ownedProjectileCounts[type] == 0)
                {
                    int amount = 2;
                    if (megaGemCore)
                    {
                        amount = 4;
                    }
                    for (int i = 0; i < amount; i++)
                    {
                        Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.One, type, 16, 0f);
                    }
                }
            }
            if (amethystCore)
            {
                Player.AddBuff<RallyingAmethyst>(normalBuffTime);
            }
            if (diamondCore)
            {
                Player.AddBuff<TenaciousDiamond>(normalBuffTime);
            }
            if (emeraldCore)
            {
                Player.AddBuff<FleetingEmerald>(normalBuffTime);
            }
            if (rubyCore)
            {
                Player.AddBuff<VengefulRuby>(normalBuffTime);
            }
            if (sapphireCore)
            {
                Player.AddBuff<CunningSapphire>(normalBuffTime);
            }
            if (topazCore)
            {
                Player.AddBuff<MendingTopaz>(normalBuffTime);
            }
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
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Player.HasBuff<CunningSapphire>())
            {
                target.AddBuff(BuffID.Confused, 300);
            }
            if (target.life <= 0)
            {
                if (greaterTopazCore)
                {
                    Projectile.NewProjectile(target.GetSource_Death(), target.Center, new Vector2(0, -10),
                        ModContent.ProjectileType<TopazOrb>(), 0, 0f, Player.whoAmI);
                }
                if (greaterAmberCore)
                {
                    int type = ModContent.ProjectileType<AmberBanner>();
                    float ai0 = 0f;
                    if (megaGemCore)
                    {
                        if (Player.ownedProjectileCounts[type] == 0)
                        {
                            ai0 = 1f;
                        }
                        else
                        {
                            int firstBanner = AmberBanner.FindOldestBanner(Player);
                            if (firstBanner > -1)
                            {
                                Main.projectile[firstBanner].ai[0] = 1f;
                            }
                        }
                    }
                    Projectile.NewProjectile(target.GetSource_Death(), target.Center, Vector2.Zero, type, 0, 0f, Player.whoAmI, ai0);
                }
            }
        }
    }
}
