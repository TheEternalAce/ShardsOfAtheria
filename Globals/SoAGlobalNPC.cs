using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Buffs.NPCDebuff;
using ShardsOfAtheria.Items.Accessories;
using ShardsOfAtheria.Items.BossSummons;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Items.SinfulSouls;
using ShardsOfAtheria.Items.SinfulSouls.Extras;
using ShardsOfAtheria.Items.Tools.ToggleItems;
using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Items.Weapons.Ranged;
using ShardsOfAtheria.Items.Weapons.Summon;
using ShardsOfAtheria.Projectiles.Melee.GenesisRagnarok;
using ShardsOfAtheria.ShardsConditions;
using ShardsOfAtheria.ShardsConditions.ItemDrop;
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
        readonly Asset<Texture2D> skyHarpy = ModContent.Request<Texture2D>("ShardsOfAtheria/NPCs/Variant/Harpy/SkyHarpy");

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
                shop.Add(new Item(ModContent.ItemType<SinfulSoul>()), SoAConditions.SlayerMode);
                shop.Add(new Item(ModContent.ItemType<SinfulArmament>()), SoAConditions.SlayerMode);
            }
            if (shop.NpcType == NPCID.Merchant)
            {
                shop.Add<RiggedCoin>(SoAConditions.AtherianPresent);
            }
        }

        public override void SetupTravelShop(int[] shop, ref int nextSlot)
        {
            shop[nextSlot] = ModContent.ItemType<Jade>();
            nextSlot++;
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
            if (npc.type == NPCID.Reaper)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AncientMedalion>(), 4));
            }
            if (npc.type == NPCID.BlackRecluse ||
                npc.type == NPCID.BlackRecluseWall ||
                npc.type == NPCID.JungleCreeper ||
                npc.type == NPCID.JungleCreeperWall)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<AcidTrip>(), 10));
            }

            // Master mode drops
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
            if (npc.type == NPCID.TheDestroyer)
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
            if (npc.type == NPCID.MartianSaucerCore)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ReactorMeltdown>(), 4));
                master.OnSuccess(ItemDropRule.Common(ModContent.ItemType<HansMachineGun>()));
                npcLoot.Add(master);
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
                        ShardsHelpers.CallStorm(npc.GetSource_FromAI(), Main.player[npc.target].Center,
                            3, 20, 0, DamageClass.Generic, Main.myPlayer, 1, true);
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
            if (npc.HasBuff(ModContent.BuffType<MarkedByAvatar>()))
                modifiers.ScalingBonusDamage += 1f;
            base.ModifyIncomingHit(npc, ref modifiers);
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (npc.HasBuff(ModContent.BuffType<Marked>()))
                drawColor = Color.MediumPurple;
            if (npc.HasBuff(ModContent.BuffType<MarkedByAvatar>()))
                drawColor = Color.GreenYellow;
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
                npc.BasicInWorldGlowmask(spriteBatch, skyHarpy.Value, drawColor, screenPos, effects);
                return false;
            }
            return base.PreDraw(npc, spriteBatch, screenPos, drawColor);
        }
    }
}