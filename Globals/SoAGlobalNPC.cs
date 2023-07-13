using AlchemistNPCLite.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Buffs.NPCDebuff;
using ShardsOfAtheria.Items.Accessories;
using ShardsOfAtheria.Items.GrabBags;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Items.SinfulSouls;
using ShardsOfAtheria.Items.SinfulSouls.Extras;
using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Items.Weapons.Ranged;
using ShardsOfAtheria.Items.Weapons.Summon;
using ShardsOfAtheria.Projectiles.Weapon.Melee.GenesisRagnarok;
using ShardsOfAtheria.ShardsConditions;
using ShardsOfAtheria.ShardsConditions.ItemDrop;
using ShardsOfAtheria.Systems;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Globals
{
    public class SoAGlobalNPC : GlobalNPC
    {
        public bool flawless = true;
        Asset<Texture2D> skyHarpy = ModContent.Request<Texture2D>("ShardsOfAtheria/NPCs/Variant/Harpy/SkyHarpy");

        public override bool InstancePerEntity => true;

        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            base.OnSpawn(npc, source);
            if (npc.type == NPCID.Harpy)
            {
                npc.GivenName = Language.GetTextValue("Mods.ShardsOfAtheria.NPCs.SkyHarpy.DisplayName");
            }
        }

        public override void ModifyShop(NPCShop shop)
        {
            if (shop.NpcType == NPCID.Wizard)
            {
                shop.Add(new Item(ModContent.ItemType<SinfulSoul>())
                {
                    shopCustomPrice = 250000
                }, SoAConditions.SlayerMode);
                shop.Add(new Item(ModContent.ItemType<SinfulArmament>())
                {
                    shopCustomPrice = 250000
                }, SoAConditions.SlayerMode);
            }
            else if (ModLoader.TryGetMod("AlchemistNPCLite", out Mod alchemistNPCLite))
            {
                if (shop.NpcType == alchemistNPCLite.Find<ModNPC>("Operator").Type)
                {
                    shop.Add<SoulOfDaylight>(new Condition("Mods.ShardsOfAtheria.Conditions.OperatorMaterials",
                        () => OperatorMaterialsShop));
                    shop.Add<SoulOfTwilight>(new Condition("Mods.ShardsOfAtheria.Conditions.OperatorMaterials",
                        () => OperatorMaterialsShop));
                    shop.Add<SoulOfSpite>(new Condition("Mods.ShardsOfAtheria.Conditions.OperatorMaterials",
                        () => OperatorMaterialsShop));
                    shop.Add<HardlightPrism>(new Condition("Mods.ShardsOfAtheria.Conditions.DefeatNova",
                        () => OperatorMaterialsShop && ShardsDownedSystem.downedValkyrie));
                    shop.Add<NovaBossBag>(new Condition("Mods.ShardsOfAtheria.Conditions.OperatorBags3",
                        () => OperatorBagsShop3));
                }
            }
        }

        [JITWhenModsEnabled("AlchemistNPCLite")]
        bool OperatorMaterialsShop
        {
            get { return Operator.Shop2; }
        }

        [JITWhenModsEnabled("AlchemistNPCLite")]
        bool OperatorBagsShop3
        {
            get { return Operator.Shop6; }
        }

        public override void SetupTravelShop(int[] shop, ref int nextSlot)
        {
            // Sells during a Full Moon
            if (Main.moonPhase == 0)
            {
                shop[nextSlot] = ModContent.ItemType<AreusShard>();
                nextSlot++;
            }
            // Sells during a New Moon
            if (Main.moonPhase == 4)
            {
                shop[nextSlot] = ModContent.ItemType<BionicOreItem>();
                nextSlot++;
            }
            base.SetupTravelShop(shop, ref nextSlot);
        }

        public override void OnKill(NPC npc)
        {
            if (npc.lifeMax > 5)
            {
                if (Main.dayTime && Main.rand.NextFloat() < .2f && Main.LocalPlayer.ZoneOverworldHeight)
                {
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<SoulOfDaylight>());
                }
                if ((Main.eclipse || !Main.dayTime) && Main.rand.NextFloat() < .2f && Main.LocalPlayer.ZoneOverworldHeight)
                {
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<SoulOfTwilight>());
                }
                if (Main.rand.NextFloat() < .2f && Main.LocalPlayer.ZoneUnderworldHeight)
                {
                    Item.NewItem(npc.GetSource_Loot(), npc.getRect(), ModContent.ItemType<SoulOfSpite>());
                }
            }
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            LeadingConditionRule notHardmode = new(new Conditions.IsPreHardmode());
            LeadingConditionRule master = new(new Conditions.IsMasterMode());
            LeadingConditionRule firstTimeKillingPlantera = new(new Conditions.FirstTimeKillingPlantera());
            LeadingConditionRule downedGolem = new(new DownedGolem());
            LeadingConditionRule downedCultist = new(new DownedLunaticCultist());
            LeadingConditionRule downedMoonLord = new(new DownedMoonLord());

            if (npc.type == NPCID.Mothron)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BrokenHeroGun>(), 4));
            }
            if (npc.type == NPCID.MartianSaucerCore)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ReactorMeltdown>(), 4));
            }
            if (npc.type == NPCID.GoblinShark)
            {
                npcLoot.RemoveWhere(
                    rule => rule is CommonDrop drop
                    && drop.itemId == ItemID.SharpTears
                );
            }
            if (npc.type == NPCID.BlackRecluse ||
                npc.type == NPCID.BlackRecluseWall ||
                npc.type == NPCID.JungleCreeper ||
                npc.type == NPCID.JungleCreeperWall)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AcidTrip>(), 8));
            }

            if (npc.type == NPCID.KingSlime)
            {
                master.OnSuccess(ItemDropRule.Common(ModContent.ItemType<KingsKusarigama>()));
                npcLoot.Add(master);
            }
            if (npc.type == NPCID.EyeofCthulhu)
            {
                master.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Cataracnia>()));
                npcLoot.Add(master);
            }
            if (npc.type == NPCID.BrainofCthulhu)
            {
                master.OnSuccess(ItemDropRule.Common(ModContent.ItemType<TomeOfOmniscience>()));
                npcLoot.Add(master);
            }
            if (npc.type == NPCID.EaterofWorldsHead || npc.type == NPCID.EaterofWorldsBody || npc.type == NPCID.EaterofWorldsTail)
            {
                LeadingConditionRule leadingConditionRule = new(new Conditions.LegacyHack_IsABoss());
                master.OnSuccess(ItemDropRule.Common(ModContent.ItemType<WormTench>()));
                leadingConditionRule.OnSuccess(master);
                npcLoot.Add(leadingConditionRule);
            }
            if (npc.type == NPCID.Deerclops)
            {
                master.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ScreamLantern>()));
                npcLoot.Add(master);
            }
            if (npc.type == NPCID.WallofFlesh)
            {
                notHardmode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MemoryFragment>()));
                npcLoot.Add(notHardmode);
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SinfulSoul>()));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SinfulArmament>()));

                master.OnSuccess(ItemDropRule.Common(ModContent.ItemType<FlailOfFlesh>()));
                npcLoot.Add(master);
            }
            if (npc.type == NPCID.TheDestroyer || npc.type == NPCID.TheDestroyerBody || npc.type == NPCID.TheDestroyerTail)
            {
                master.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Coilgun>()));
                npcLoot.Add(master);
            }
            if (npc.type == NPCID.SkeletronPrime)
            {
                master.OnSuccess(ItemDropRule.Common(ModContent.ItemType<HandCanon>()));
                npcLoot.Add(master);
            }
            if (npc.type == NPCID.Retinazer || npc.type == NPCID.Spazmatism)
            {
                LeadingConditionRule leadingConditionRule = new(new Conditions.MissingTwin());
                master.OnSuccess(ItemDropRule.Common(ModContent.ItemType<DoubleBow>()));
                leadingConditionRule.OnSuccess(master);
                npcLoot.Add(leadingConditionRule);
            }
            if (npc.type == NPCID.Plantera)
            {
                firstTimeKillingPlantera.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MemoryFragment>()));
                npcLoot.Add(firstTimeKillingPlantera);
            }
            if (npc.type == NPCID.Golem)
            {
                downedGolem.OnFailedConditions(ItemDropRule.Common(ModContent.ItemType<MemoryFragment>()));
                npcLoot.Add(downedGolem);
            }
            if (npc.type == NPCID.CultistBoss)
            {
                downedCultist.OnFailedConditions(ItemDropRule.Common(ModContent.ItemType<MemoryFragment>()));
                npcLoot.Add(downedCultist);
            }
            if (npc.type == NPCID.MoonLordCore)
            {
                downedMoonLord.OnFailedConditions(ItemDropRule.Common(ModContent.ItemType<MemoryFragment>()));
                master.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Zenova>()));
                npcLoot.Add(downedMoonLord);
                npcLoot.Add(master);
            }
        }

        public override void AI(NPC npc)
        {
            if (npc.type == NPCID.Harpy)
            {
                if (npc.ai[0] == 30f || npc.ai[0] == 60f || npc.ai[0] == 90f)
                {
                    if (Main.rand.NextBool(3))
                    {
                        int dir = Main.rand.NextBool(2) ? 1 : -1;
                        Vector2 position = Main.player[npc.target].Center;
                        Projectile proj = Projectile.NewProjectileDirect(npc.GetSource_FromThis(), position, Vector2.Zero,
                            ProjectileID.WaterGun, 0, 0f, Main.myPlayer);
                        proj.CallStorm(3);
                        proj.Kill();
                        npc.ai[0] = 91;
                    }
                }
            }
            base.AI(npc);
        }

        public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
        {
            if (target.ownedProjectileCounts[ModContent.ProjectileType<Ragnarok_Shield>()] > 0)
                npc.AddBuff(BuffID.OnFire, 1200);
            if (target.Slayer().NovaSoul)
            {
                npc.AddBuff(ModContent.BuffType<ElectricShock>(), 300);
            }
        }

        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if (npc.HasBuff(ModContent.BuffType<Marked>()))
                modifiers.FinalDamage *= 1.1f;
            base.ModifyIncomingHit(npc, ref modifiers);
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (npc.HasBuff(ModContent.BuffType<Marked>()))
            {
                drawColor = Color.MediumPurple;
            }
            if (npc.HasBuff(BuffID.Electrified) && Main.rand.NextBool(4))
            {
                int dust = Dust.NewDust(npc.position, npc.width + 4, npc.height + 4, DustID.Electric, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default, 1f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 1.8f;
                Main.dust[dust].velocity.Y -= 0.5f;
            }
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (npc.HasBuff(BuffID.Electrified))
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 40;
                if (damage < 20)
                {
                    damage = 20;
                }
            }
        }

        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (npc.type == NPCID.Harpy)
            {
                SpriteEffects effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
                npc.BasicInWorldGlowmask(spriteBatch, skyHarpy.Value, Color.White, screenPos, effects);
                return false;
            }
            return base.PreDraw(npc, spriteBatch, screenPos, drawColor);
        }
    }
}