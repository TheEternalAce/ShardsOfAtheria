using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Dusts;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee.AreusJustitia
{
    public class ZAreusJustitia : ModProjectile
    {
        public override string Texture => "ShardsOfAtheria/Items/Weapons/Melee/ZAreusEdge";

        public override void SetStaticDefaults()
        {
            Projectile.MakeTrueMelee();
            Projectile.AddAreus();
            Projectile.AddDamageType(3, 5, 9);
            Projectile.AddElement(0);
            Projectile.AddRedemptionElement(2);
            Projectile.AddRedemptionElement(13);
        }

        public override void SetDefaults()
        {
            Projectile.width = 86;
            Projectile.height = 86;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.scale = 1.3f;
            Projectile.alpha = 0;
            Projectile.timeLeft = 120;
            Projectile.ownerHitCheck = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.hostile = false;
        }

        public override void OnSpawn(IEntitySource source)
        {
            if (Projectile.GetPlayerOwner().meleeScaleGlove)
                Projectile.scale *= 1.1f;
        }

        public override void AI()
        {
            Player projOwner = Main.player[Projectile.owner];
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter, reverseRotation: true);
            Projectile.spriteDirection = Math.Sign(Projectile.velocity.X);
            projOwner.heldProj = Projectile.whoAmI;
            float rot = Projectile.velocity.ToRotation();
            float progress = 1f - projOwner.itemAnimation / (float)projOwner.itemAnimationMax;
            if (progress < 0.33f)
            {
                if (Projectile.ai[1] < 1f)
                {
                    Projectile.ai[1] = 1f;
                    SoundStyle style = SoA.Judgement1;
                    SoundEngine.PlaySound(in style, Projectile.Center);
                    if (Main.myPlayer == Projectile.owner)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), ownerMountedCenter, Projectile.velocity * 32f, ModContent.ProjectileType<FlameSlashExtention>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 1f);
                    }
                }
                if (progress < 0.05f)
                {
                    rot += MathHelper.ToRadians(-160f) * Projectile.spriteDirection;
                }
                else if (progress < 0.2f)
                {
                    rot += MathHelper.ToRadians(MathHelper.Lerp(-160f, 45f, (progress - 0.05f) % 0.15f / 0.15f)) * Projectile.spriteDirection;
                }
                else if (progress < 0.25f)
                {
                    rot += MathHelper.ToRadians(45f) * Projectile.spriteDirection;
                }
                else if (progress < 0.3f)
                {
                    if (Main.myPlayer == projOwner.whoAmI)
                    {
                        Projectile.velocity = Vector2.Normalize(Main.MouseWorld - ownerMountedCenter);
                    }
                    rot += MathHelper.ToRadians(MathHelper.Lerp(45f, 0f, progress % 0.05f / 0.05f)) * Projectile.spriteDirection;
                    Projectile.ai[0] = MathHelper.Lerp(0f, -30f, progress % 0.05f / 0.05f);
                }
                Projectile.localAI[1] = Main.rand.Next(-10, 10);
            }
            else if (progress < 0.66f)
            {
                if (progress < 0.43f)
                {
                    Projectile.ai[0] = MathHelper.Lerp(-30f, 10f, (progress - 0.03f) % 0.1f / 0.1f);
                }
                else if (progress < 0.46f)
                {
                    if (Main.myPlayer == projOwner.whoAmI)
                    {
                        Projectile.velocity = Vector2.Normalize(Main.MouseWorld - ownerMountedCenter);
                    }
                    Projectile.ai[0] = MathHelper.Lerp(10f, -30f, (progress - 0.01f) % 0.03f / 0.03f);
                }
                else if (progress < 0.56f)
                {
                    rot += MathHelper.ToRadians(Projectile.localAI[1]) * Projectile.spriteDirection;
                    Projectile.ai[0] = MathHelper.Lerp(-30f, 10f, (progress - 0.06f) % 0.1f / 0.1f);
                }
                else if (progress < 0.6f)
                {
                    rot += MathHelper.ToRadians(Projectile.localAI[1]) * Projectile.spriteDirection;
                    Projectile.ai[0] = 10f;
                }
                else
                {
                    if (Main.myPlayer == projOwner.whoAmI)
                    {
                        Projectile.velocity = Vector2.Normalize(Main.MouseWorld - ownerMountedCenter);
                    }
                    rot += MathHelper.ToRadians(MathHelper.Lerp(Projectile.localAI[1], -160f, progress % 0.06f / 0.06f)) * Projectile.spriteDirection;
                    Projectile.ai[0] = MathHelper.Lerp(10f, 0f, progress % 0.06f / 0.06f);
                }
                if (Projectile.ai[1] < 2f)
                {
                    Projectile.ai[1] = 2f;
                    if (Main.myPlayer == Projectile.owner)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), ownerMountedCenter, new Vector2(30f, 0f).RotatedBy(rot), ModContent.ProjectileType<FlameStabExtention>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 1f, 3f);
                    }
                    SoundStyle style = SoA.Judgement2;
                    SoundEngine.PlaySound(in style, Projectile.Center);
                }
                if (Projectile.ai[1] < 3f && progress > 0.46f)
                {
                    Projectile.ai[1] = 3f;
                    if (Main.myPlayer == Projectile.owner)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), ownerMountedCenter, new Vector2(30f, 0f).RotatedBy(rot), ModContent.ProjectileType<FlameStabExtention>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 1f, 3f);
                    }
                    SoundStyle style = SoA.Judgement3;
                    SoundEngine.PlaySound(in style, Projectile.Center);
                }
            }
            else
            {
                if (Projectile.ai[1] < 4f)
                {
                    Projectile.ai[1] = 4f;
                    if (Main.myPlayer == Projectile.owner)
                    {
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), ownerMountedCenter, Projectile.velocity * 26f, ModContent.ProjectileType<FlameSlashExtention>(), Projectile.damage, Projectile.knockBack, Projectile.owner, 2f);
                    }
                    SoundStyle style = SoA.Judgement2;
                    SoundEngine.PlaySound(in style, Projectile.Center);
                }
                if (!(progress < 0.7f))
                {
                    rot = !(progress < 0.95f) ? rot + MathHelper.ToRadians(45f) * Projectile.spriteDirection : rot + MathHelper.ToRadians(MathHelper.Lerp(-160f, 45f, (float)Math.Sin((double)(1.57f * (progress - 0.7f) / 0.25f)))) * Projectile.spriteDirection;
                }
                else
                {
                    Projectile.ai[0] = 0f;
                    rot += MathHelper.ToRadians(-160f);
                }
            }
            Vector2 velRot = new Vector2(1f, 0f).RotatedBy(rot);
            projOwner.itemRotation = (float)Math.Atan2((double)(velRot.Y * Projectile.direction), (double)(velRot.X * Projectile.direction));
            projOwner.direction = Projectile.spriteDirection;
            Projectile.rotation = rot + MathHelper.ToRadians(Projectile.spriteDirection == 1 ? 45 : 135);
            if (0.05f < progress && progress < 0.25f || 0.66f < progress && progress < 0.95f)
            {
                for (int j = 0; j < 3; j++)
                {
                    Dust.NewDustPerfect(ownerMountedCenter + new Vector2(30 + Main.rand.Next(100), 0f).RotatedBy(rot), ModContent.DustType<AreusDust>()).noGravity = true;
                }
            }
            if (0.33f < progress && progress < 0.43f || 0.46f < progress && progress < 0.6f)
            {
                Dust.NewDustPerfect(ownerMountedCenter + new Vector2(30 + Main.rand.Next(100), Main.rand.Next(-5, 5)).RotatedBy(rot), ModContent.DustType<AreusDust>(), -Projectile.velocity * 4f).noGravity = true;
            }
            Projectile.Center = ownerMountedCenter + (80f + Projectile.ai[0]) * velRot;
            if (projOwner.itemAnimation == 1) Projectile.Kill();
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.ScalingArmorPenetration += 1f;
            modifiers.Knockback *= 0.1f;
        }

        public override bool? CanHitNPC(NPC target)
        {
            float progress = 1f - Main.player[Projectile.owner].itemAnimation / (float)Main.player[Projectile.owner].itemAnimationMax;
            if (!(0.25f < progress) || !(progress < 0.33f) || !(0.56f < progress) || !(progress < 0.7f))
            {
                return null;
            }
            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player projOwner = Main.player[Projectile.owner];
            float progress = 1f - projOwner.itemAnimation / (float)projOwner.itemAnimationMax;
            if (progress < 0.66f)
            {
                if (progress < 0.33f)
                {
                    target.immune[Projectile.owner] = projOwner.itemAnimation - (int)(projOwner.itemAnimationMax * 0.666f);
                }
                else
                {
                    target.immune[Projectile.owner] = 6;
                }
            }
            else
            {
                target.immune[Projectile.owner] = 5;
            }
        }

        public override void OnKill(int timeLeft)
        {
            Main.player[Projectile.owner].itemRotation = 0f;
            Main.player[Projectile.owner].heldProj = -1;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Player projOwner = Main.player[Projectile.owner];
            if (projOwner.itemAnimation == 3)
            {
                return false;
            }
            Vector2 position = projOwner.RotatedRelativePoint(projOwner.MountedCenter, reverseRotation: true) - Main.screenPosition;
            Vector2 originOffset = new Vector2(Projectile.ai[0] + 12f, 0f).RotatedBy(MathHelper.ToRadians(Projectile.direction == 1 ? 135 : 45));
            Vector2 origin = new Vector2(Projectile.spriteDirection == 1 ? 0 : 86, 86f) + originOffset;
            SpriteEffects spriteEffect = Projectile.spriteDirection != 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, position, new Rectangle(0, 0, TextureAssets.Projectile[Projectile.type].Width(), TextureAssets.Projectile[Projectile.type].Height()), lightColor * ((255 - Projectile.alpha) / 255f), Projectile.rotation, origin, Projectile.scale, spriteEffect);
            return false;
        }
    }
}
