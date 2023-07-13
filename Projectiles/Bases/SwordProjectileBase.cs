using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Bases
{
    public abstract class SwordProjectileBase : ModProjectile
    {
        public static Asset<Texture2D> SwishTexture => ModContent.Request<Texture2D>(typeof(SwordProjectileBase).Namespace.Replace('.', '/') + "/Swish", AssetRequestMode.ImmediateLoad);
        public static Asset<Texture2D> Swish2Texture => ModContent.Request<Texture2D>(typeof(SwordProjectileBase).Namespace.Replace('.', '/') + "/Swish2", AssetRequestMode.ImmediateLoad);

        public static SoundStyle HeavySwing => SoundID.DD2_MonkStaffSwing;

        private bool _init;
        public int swingDirection;
        public int swordReach;
        public int swordSize;
        public int visualOutwards;
        public float rotationOffset;
        public bool forced50;
        public float scale;

        public bool playedSound;

        public bool damaging;
        public int damageTime;

        public int combo;

        public int freezeFrame;

        private float armRotation;
        private Vector2 angleVector;
        public Vector2 AngleVector { get => angleVector; set => angleVector = Vector2.Normalize(value); }
        public Vector2 BaseAngleVector => Vector2.Normalize(Projectile.velocity);
        public virtual float AnimProgress => 1f - (Main.player[Projectile.owner].itemAnimation * (Projectile.extraUpdates + 1) + Projectile.numUpdates + 1) / (float)(Main.player[Projectile.owner].itemAnimationMax * (Projectile.extraUpdates + 1));
        public float lastAnimProgress;

        public int amountAllowedToHit;

        public virtual bool SwingSwitchDir => AnimProgress > 0.4f && AnimProgress < 0.6f;

        public override void SetDefaults()
        {
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.localNPCHitCooldown = 500;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true;
            Projectile.aiStyle = ProjAIStyleID.Spear;
            amountAllowedToHit = 2;
            swordReach = 100;
            swordSize = 30;
        }

        public override bool? CanDamage()
        {
            return (AnimProgress > 0.4f && AnimProgress < 0.6f && freezeFrame <= 0) ? null : false;
        }

        public override void AI()
        {
            var player = Main.player[Projectile.owner];
            Projectile.aiStyle = -1;

            if (Projectile.numUpdates == -1 && freezeFrame > 0)
            {
                freezeFrame--;
                if (freezeFrame != 1)
                {
                    player.itemAnimation++;
                    player.itemTime++;
                }
            }
            if (player.ownedProjectileCounts[Type] > 1 || player.itemTime < 2 || player.dead)
            {
                Projectile.Kill();
                player.ownedProjectileCounts[Type]--;
            }

            var shards = player.Shards();

            player.heldProj = Projectile.whoAmI;
            if (!_init)
            {
                Projectile.scale = 1f;
                Initialize(player, shards);
                scale = Projectile.scale;
                Projectile.netUpdate = true;
                _init = true;
            }

            if (SwingSwitchDir)
            {
                UpdateDirection(player);
            }

            if (!player.frozen && !player.stoned)
            {
                var arm = Main.GetPlayerArmPosition(Projectile);
                float progress = AnimProgress;
                lastAnimProgress = progress;
                if (!forced50 && progress >= 0.5f)
                {
                    progress = 0.5f;
                    forced50 = true;
                }
                InterpolateSword(progress, out var angleVector, out float swingProgress, out float scale, out float outer);
                if (freezeFrame <= 0)
                {
                    AngleVector = angleVector;
                }
                Projectile.position = arm + AngleVector * swordReach;
                Projectile.position.X -= Projectile.width / 2f;
                Projectile.position.Y -= Projectile.height / 2f;
                Projectile.rotation = (arm - Projectile.Center).ToRotation() + rotationOffset;
                if (freezeFrame <= 0)
                {
                    UpdateSwing(progress, swingProgress);
                }
                if (Main.netMode != NetmodeID.Server)
                {
                    SetArmRotation(player, progress, swingProgress);
                }
                Projectile.scale = scale;
                visualOutwards = (int)outer;
            }
        }

        public virtual void UpdateSwing(float progress, float interpolatedSwingProgress)
        {
        }

        public virtual void FireProjectile(float progress, int type, int damage, float knockback, float velocity = 16f)
        {
            if (progress == 0.5f && Main.myPlayer == Projectile.owner)
            {
                Vector2 position = Projectile.Center;
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), position, AngleVector * Projectile.velocity.Length() * velocity,
                            type, damage, knockback, Projectile.owner);
            }
        }

        public virtual float SwingProgress(float progress)
        {
            return progress;
        }
        public static float GenericSwing3(float progress)
        {
            return progress >= 0.5f ? 0.5f + (0.5f - MathF.Pow(2f, 20f * (0.5f - (progress - 0.5f)) - 10f) / 2f) : MathF.Pow(2f, 20f * (progress) - 10f) / 2f;
        }
        public static float GenericSwing2(float progress, float pow = 2f)
        {
            if (progress > 0.5f)
            {
                return 0.5f - GenericSwing2(0.5f - (progress - 0.5f), pow) + 0.5f;
            }
            return ((float)Math.Sin(Math.Pow(progress, pow) * MathHelper.TwoPi - MathHelper.PiOver2) + 1f) / 2f;
        }
        public static float GenericSwing1(float progress, float pow = 2f, float startSwishing = 0.15f)
        {
            float oldProg = progress;
            float max = 1f - startSwishing;
            if (progress < startSwishing)
            {
                progress *= (float)Math.Pow(progress / startSwishing, pow);
            }
            else if (progress > max)
            {
                progress -= max;
                progress = startSwishing - progress;
                progress *= (float)Math.Pow(progress / startSwishing, pow);
                progress = startSwishing - progress;
                progress += max;
            }
            return MathHelper.Clamp((float)Math.Sin(progress * MathHelper.Pi - MathHelper.PiOver2) / 2f + 0.5f, 0f, oldProg);
        }
        public virtual Vector2 GetOffsetVector(float progress)
        {
            return BaseAngleVector.RotatedBy((progress * MathHelper.Pi - MathHelper.PiOver2) * -swingDirection);
        }
        public virtual float GetScale(float progress)
        {
            return scale;
        }
        public virtual float GetVisualOuter(float progress, float swingProgress)
        {
            return visualOutwards;
        }
        public void InterpolateSword(float progress, out Vector2 offsetVector, out float swingProgress, out float scale, out float outer)
        {
            swingProgress = SwingProgress(progress);
            offsetVector = GetOffsetVector(swingProgress);
            scale = GetScale(swingProgress);
            outer = (int)GetVisualOuter(progress, swingProgress);
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            var center = Main.player[Projectile.owner].Center;
            return ShardsHelpers.DeathrayHitbox(center, center + AngleVector * (swordReach * Projectile.scale * scale), targetHitbox, swordSize * Projectile.scale * scale);
        }

        public void UpdateDirection(Player player)
        {
            if (angleVector.X < 0f)
            {
                //player.direction = -1;
                Projectile.direction = -1;
            }
            else if (angleVector.X > 0f)
            {
                //player.direction = 1;
                Projectile.direction = 1;
            }
        }

        protected virtual void Initialize(Player player, ShardsPlayer shards)
        {
            AngleVector = Projectile.velocity;
            combo = shards.itemCombo;
            if (player.whoAmI == Projectile.owner)
                ShardsHelpers.CappedMeleeScale(Projectile);
            swingDirection = 1;
            UpdateDirection(player);
            swingDirection *= Projectile.direction;
        }

        protected virtual void SetArmRotation(Player player, float progress, float swingProgress)
        {
            var diff = Main.player[Projectile.owner].MountedCenter - Projectile.Center;
            if (Math.Sign(diff.X) == -player.direction)
            {
                var v = diff;
                v.X = Math.Abs(diff.X);
                armRotation = v.ToRotation();
            }
            else if (progress < 0.1f)
            {
                if (swingDirection * (progress >= 0.5f ? -1 : 1) * -player.direction == -1)
                {
                    armRotation = -1.11f;
                }
                else
                {
                    armRotation = 1.11f;
                }
            }

            if (armRotation > 1.1f)
            {
                player.bodyFrame.Y = 56;
            }
            else if (armRotation > 0.5f)
            {
                player.bodyFrame.Y = 56 * 2;
            }
            else if (armRotation < -0.5f)
            {
                player.bodyFrame.Y = 56 * 4;
            }
            else
            {
                player.bodyFrame.Y = 56 * 3;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            amountAllowedToHit--;
        }

        public override bool? CanHitNPC(NPC target)
        {
            return amountAllowedToHit > 0 ? null : false;
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((byte)freezeFrame);
            writer.Write(swingDirection == -1);
            writer.Write(combo);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            freezeFrame = reader.ReadByte();
            swingDirection = reader.ReadBoolean() ? -1 : 1;
            combo = reader.ReadInt32();
        }

        public override void PostDraw(Color lightColor)
        {
            //DrawDebug();
        }

        public bool GenericSwordDraw(Color lightColor)
        {
            var texture = TextureAssets.Projectile[Type].Value;
            var center = Main.player[Projectile.owner].Center;
            var handPosition = Main.GetPlayerArmPosition(Projectile) + AngleVector * visualOutwards;
            var drawColor = Projectile.GetAlpha(lightColor) * Projectile.Opacity;
            var drawCoords = handPosition - Main.screenPosition;
            float size = texture.Size().Length();
            var effects = SpriteEffects.None;
            var origin = new Vector2(0f, texture.Height);
            bool flip = Main.player[Projectile.owner].direction == 1 ? combo > 0 : combo == 0;

            Main.EntitySpriteDraw(texture, handPosition - Main.screenPosition, null, drawColor, Projectile.rotation, origin, Projectile.scale, effects, 0);

            if (AnimProgress > 0.35f && AnimProgress < 0.75f)
            {
                float intensity = (float)Math.Sin((AnimProgress - 0.35f) / 0.4f * MathHelper.Pi);
                Main.EntitySpriteDraw(texture, handPosition - Main.screenPosition, null, drawColor.UseA(0) * intensity * 0.5f, Projectile.rotation, origin, Projectile.scale, effects, 0);

                var swish = SwishTexture.Value;
                var swishOrigin = swish.Size() / 2f;
                var swishColor = new Color(100, 120, 140, 80) * intensity * intensity * Projectile.Opacity * 0.5f;
                float r = BaseAngleVector.ToRotation() + ((AnimProgress - 0.45f) / 0.2f * 2f - 1f) * -swingDirection * 0.6f;
                var swishLocation = Main.player[Projectile.owner].Center - Main.screenPosition + r.ToRotationVector2() * (size - 20f) * scale;
                Main.EntitySpriteDraw(swish, swishLocation, null, swishColor.UseA(0), r + MathHelper.PiOver2, swishOrigin, 1f, effects, 0);
            }
            return false;
        }

        public bool SingleEdgeSwordDraw<T>(Color lightColor) where T : ModItem
        {
            var texture = TextureAssets.Projectile[Type].Value;
            var center = Main.player[Projectile.owner].Center;
            var handPosition = Main.GetPlayerArmPosition(Projectile) + AngleVector * visualOutwards;
            var drawColor = Projectile.GetAlpha(lightColor) * Projectile.Opacity;
            var drawCoords = handPosition - Main.screenPosition;
            float size = texture.Size().Length();
            var effects = SpriteEffects.None;
            var origin = new Vector2(0f, texture.Height);
            bool flip = Main.player[Projectile.owner].direction == 1 ? combo > 0 : combo == 0;
            if (flip)
            {
                Main.instance.LoadItem(ModContent.ItemType<T>());
                texture = TextureAssets.Item[ModContent.ItemType<T>()].Value;
            }

            Main.EntitySpriteDraw(texture, handPosition - Main.screenPosition, null, drawColor, Projectile.rotation, origin, Projectile.scale, effects, 0);

            if (AnimProgress > 0.35f && AnimProgress < 0.75f)
            {
                float intensity = (float)Math.Sin((AnimProgress - 0.35f) / 0.4f * MathHelper.Pi);
                Main.EntitySpriteDraw(texture, handPosition - Main.screenPosition, null, drawColor.UseA(0) * intensity * 0.5f, Projectile.rotation, origin, Projectile.scale, effects, 0);

                var swish = SwishTexture.Value;
                var swishOrigin = swish.Size() / 2f;
                var swishColor = new Color(100, 120, 140, 80) * intensity * intensity * Projectile.Opacity * 0.5f;
                float r = BaseAngleVector.ToRotation() + ((AnimProgress - 0.45f) / 0.2f * 2f - 1f) * -swingDirection * 0.6f;
                var swishLocation = Main.player[Projectile.owner].Center - Main.screenPosition + r.ToRotationVector2() * (size - 20f) * scale;
                Main.EntitySpriteDraw(swish, swishLocation, null, swishColor.UseA(0), r + MathHelper.PiOver2, swishOrigin, 1f, effects, 0);
            }
            return false;
        }

        public bool SingleEdgeSwordDraw(Color lightColor)
        {
            return SingleEdgeSwordDraw(lightColor, Texture);
        }

        public bool SingleEdgeSwordDraw(Color lightColor, string path)
        {
            var texture = ModContent.Request<Texture2D>(path).Value;
            var center = Main.player[Projectile.owner].Center;
            var handPosition = Main.GetPlayerArmPosition(Projectile) + AngleVector * visualOutwards;
            var drawColor = Projectile.GetAlpha(lightColor) * Projectile.Opacity;
            var drawCoords = handPosition - Main.screenPosition;
            float size = texture.Size().Length();
            var effects = SpriteEffects.None;
            var origin = new Vector2(0f, texture.Height);
            bool flip = Main.player[Projectile.owner].direction == 1 ? combo > 0 : combo == 0;
            if (flip)
            {
                texture = ModContent.Request<Texture2D>(path + "_Flip").Value;
            }

            Main.EntitySpriteDraw(texture, handPosition - Main.screenPosition, null, drawColor, Projectile.rotation, origin, Projectile.scale, effects, 0);

            if (AnimProgress > 0.35f && AnimProgress < 0.75f)
            {
                float intensity = (float)Math.Sin((AnimProgress - 0.35f) / 0.4f * MathHelper.Pi);
                Main.EntitySpriteDraw(texture, handPosition - Main.screenPosition, null, drawColor.UseA(0) * intensity * 0.5f, Projectile.rotation, origin, Projectile.scale, effects, 0);

                var swish = SwishTexture.Value;
                var swishOrigin = swish.Size() / 2f;
                var swishColor = new Color(100, 120, 140, 80) * intensity * intensity * Projectile.Opacity * 0.5f;
                float r = BaseAngleVector.ToRotation() + ((AnimProgress - 0.45f) / 0.2f * 2f - 1f) * -swingDirection * 0.6f;
                var swishLocation = Main.player[Projectile.owner].Center - Main.screenPosition + r.ToRotationVector2() * (size - 20f) * scale;
                Main.EntitySpriteDraw(swish, swishLocation, null, swishColor.UseA(0), r + MathHelper.PiOver2, swishOrigin, 1f, effects, 0);
            }
            return false;
        }
    }
}