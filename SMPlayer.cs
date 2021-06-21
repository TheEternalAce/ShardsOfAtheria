using Microsoft.Xna.Framework;
using SagesMania.Buffs;
using SagesMania.Projectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania
{
    public class SMPlayer : ModPlayer
    {
        public bool areusBatteryElectrify;
        public bool BBBottle;
        public bool PhantomBulletBottle;
        public bool Co2Cartridge;
        public bool naturalAreusRegen;
        public bool lesserSapphireCore;
        public bool sapphireCore;
        public bool superSapphireCore;
        public bool greaterRubyCore;
        public bool OrangeMask;
        public bool Overdrive;
        public bool livingMetal;
        public bool Infected;
        public bool omnicientTome;
        public bool baseConservation;
        public bool baseExploration;

        public int TomeKnowledge;

        public override void ResetEffects()
        {
            areusBatteryElectrify = false;
            BBBottle = false;
            PhantomBulletBottle = false;
            Co2Cartridge = false;
            naturalAreusRegen = false;
            lesserSapphireCore = false;
            sapphireCore = false;
            superSapphireCore = false;
            greaterRubyCore = false;
            OrangeMask = false;
            livingMetal = false;
            Overdrive = false;
            Infected = false;
            omnicientTome = false;
            baseConservation = false;
            baseExploration = false;
        }

        public override void PostUpdate()
        {
            if (OrangeMask)
            {
                player.statDefense += 7;
                player.rangedDamage += .1f;
                player.rangedCrit += 4;
            }
            if (Overdrive)
            {
                player.armorEffectDrawShadow = true;
                player.armorEffectDrawOutlines = true;
            }
            if (omnicientTome)
            {
                if (TomeKnowledge == 0)
                {
                    player.AddBuff(ModContent.BuffType<BaseCombat>(), 1800);
                    player.ClearBuff(ModContent.BuffType<BaseConservation>());
                    player.ClearBuff(ModContent.BuffType<BaseExploration>());
                }
                else if (TomeKnowledge == 1)
                {
                    player.AddBuff(ModContent.BuffType<BaseConservation>(), 1800);
                    player.ClearBuff(ModContent.BuffType<BaseCombat>());
                    player.ClearBuff(ModContent.BuffType<BaseExploration>());
                }
                else if (TomeKnowledge == 2)
                {
                    player.AddBuff(ModContent.BuffType<BaseExploration>(), 1800);
                    player.ClearBuff(ModContent.BuffType<BaseCombat>());
                    player.ClearBuff(ModContent.BuffType<BaseConservation>());
                }
            }
            if (baseExploration)
            {
                player.AddBuff(BuffID.Mining, 1);
                player.AddBuff(BuffID.Builder, 1);
                player.AddBuff(BuffID.Shine, 1);
                player.AddBuff(BuffID.Hunter, 1);
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
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<PhantomBullet>(), damage, knockBack, player.whoAmI);
            }
            if (BBBottle)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<BBProjectile>(), damage, knockBack, player.whoAmI);
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
                if (livingMetal && !player.HasBuff(ModContent.BuffType<Overdrive>()))
                {
                    CombatText.NewText(player.Hitbox, Color.Green, "Overdrive: ON", true);
                    Main.PlaySound(SoundID.Item4);
                    player.AddBuff(ModContent.BuffType<Overdrive>(), 600 * 60);
                }
                else
                {
                    player.ClearBuff(ModContent.BuffType<Overdrive>());
                    CombatText.NewText(player.Hitbox, Color.Red, "Overdrive: OFF");

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
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (areusBatteryElectrify)
            {
                target.AddBuff(BuffID.Electrified, 10 * 60);
            }
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (lesserSapphireCore && Main.rand.NextFloat() < 0.05f)
            {
                CombatText.NewText(player.Hitbox, Color.RoyalBlue, "Dodge!", true);
                player.immune = true;
                player.immuneTime = 30;
                return false;
            }
            if (sapphireCore && Main.rand.NextFloat() < 0.1f)
            {
                CombatText.NewText(player.Hitbox, Color.RoyalBlue, "Dodge!", true);
                player.immune = true;
                player.immuneTime = 30;
                return false;
            }
            if (superSapphireCore && Main.rand.NextFloat() < 0.2f)
            {
                CombatText.NewText(player.Hitbox, Color.RoyalBlue, "Dodge!", true);
                player.immune = true;
                player.immuneTime = 30;
                return false;
            }
            else return true;
        }

        public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            if (player.HasBuff(ModContent.BuffType<Overdrive>()))
            {
                player.ClearBuff(ModContent.BuffType<Overdrive>());
                CombatText.NewText(player.Hitbox, Color.Red, "Overdrive: BREAK", true);
            }
        }

        public override void UpdateBadLifeRegen()
        {
            if (Overdrive)
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second.
                player.lifeRegen -= 2;
            }
            if (Infected)
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (player.lifeRegen > 0)
                {
                    player.lifeRegen = 0;
                }
                player.lifeRegenTime = 0;
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second.
                player.lifeRegen -= 16;
            }
        }

        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (Overdrive)
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