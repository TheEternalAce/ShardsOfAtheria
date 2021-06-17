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
        }

        public override void PostUpdate()
        {
            if (OrangeMask)
            {
                player.statDefense += 7;
                player.rangedDamage += .1f;
                player.rangedCrit += 4;
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
                    CombatText.NewText(player.Hitbox, Color.White, "Overdrive enabled", true);
                    Main.PlaySound(SoundID.Item4);
                    player.AddBuff(ModContent.BuffType<Overdrive>(), 10 * 60);
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
                CombatText.NewText(player.Hitbox, Color.RoyalBlue, "Dodge!");
                player.immune = true;
                player.immuneTime = 30;
                return false;
            }
            if (sapphireCore && Main.rand.NextFloat() < 0.1f)
            {
                CombatText.NewText(player.Hitbox, Color.RoyalBlue, "Dodge!");
                player.immune = true;
                player.immuneTime = 30;
                return false;
            }
            if (superSapphireCore && Main.rand.NextFloat() < 0.2f)
            {
                CombatText.NewText(player.Hitbox, Color.RoyalBlue, "Dodge!");
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
            }
        }
    }
}