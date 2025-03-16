using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace ShardsOfAtheria.Projectiles.Ranged
{
    public class KusarigamaKing : ModProjectile
    {
        float rotation;
        bool returning = false;
        bool swinging;
        Vector2 initialVelocity;
        float initialSpeed;
        float AttackSpeed => Math.Min(Projectile.GetPlayerOwner().GetTotalAttackSpeed(DamageClass.Ranged.TryThrowing()), 1.5f);

        public bool BeingHeld => Main.player[Projectile.owner].channel && !Main.player[Projectile.owner].noItems && !Main.player[Projectile.owner].CCed;

        private const string ChainTexturePath = "ShardsOfAtheria/Projectiles/Ranged/KingsKusarigama_Chain";

        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(0, 11);
            Projectile.AddElement(1);
            Projectile.AddRedemptionElement(3);
        }

        public override void SetDefaults()
        {
            Projectile.width = 35;
            Projectile.height = 35;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged.TryThrowing();
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.ownerHitCheck = true;
            Projectile.extraUpdates = 2;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.netImportant = true;
        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.velocity *= AttackSpeed;
            initialVelocity = Projectile.velocity;
            initialSpeed = Projectile.velocity.Length();
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            float dist = Vector2.Distance(player.MountedCenter, Projectile.Center);
            float zone = 30;

            int direction = player.Center.X > Projectile.Center.X ? -1 : 1;
            Projectile.spriteDirection = direction;

            player.itemAnimation = 10;
            player.itemTime = 10;

            Projectile.SetVisualOffsets(new Vector2(66, 58), true);
            Projectile.rotation = (Projectile.Center - player.Center).ToRotation() + MathHelper.PiOver2;

            if (dist >= 200) Projectile.tileCollide = true;
            if (returning) DoReturnAI(player);
            else if ((BeingHeld || swinging) && dist >= 180 - zone && dist <= 180 + zone) DoSwingAI(player);
            else DoThrownAI(player);
        }

        void DoReturnAI(Player player)
        {
            if (Projectile.tileCollide) Projectile.tileCollide = false;
            Projectile.velocity = Projectile.Center.DirectionTo(player.Center) * 10f;
            if (player.Hitbox.Contains(Projectile.Center.ToPoint())) Projectile.Kill();
        }

        void DoSwingAI(Player player)
        {
            swinging = true;
            if (Projectile.tileCollide) Projectile.tileCollide = false;
            if (Projectile.velocity != Vector2.Zero) Projectile.velocity *= 0;
            var mountedCenter = player.MountedCenter;

            int mouseDirection = 1;

            if (player.IsLocal())
            {
                initialVelocity = mountedCenter.DirectionTo(Main.MouseWorld) * initialSpeed;
                mouseDirection = Main.MouseWorld.X > player.Center.X ? 1 : -1;
            }
            float mouseRotation = initialVelocity.ToRotation();
            if (Projectile.ai[0] == 0)
            {
                if (rotation == 0)
                {
                    float startingDegrees;
                    startingDegrees = MathHelper.Pi;
                    if (mouseDirection == -1)
                    {
                        startingDegrees = 0;
                    }
                    rotation = startingDegrees;
                }
                Projectile.ai[0] = 1;
            }

            float rotationToAdd = MathHelper.ToRadians(5f * AttackSpeed) * mouseDirection;
            rotation += rotationToAdd;
            Projectile.rotation += rotationToAdd;

            if (rotation > MathHelper.TwoPi) rotation -= MathHelper.TwoPi;
            if (rotation < 0) rotation += MathHelper.TwoPi;

            player.ChangeDir(mouseDirection);
            Projectile.direction = Projectile.Center.X > player.Center.X ? 1 : -1;
            Projectile.spriteDirection = mouseDirection;

            Projectile.Center = mountedCenter + Vector2.UnitX.RotatedBy(rotation) * 180;
            Projectile.netUpdate = true;
            float leniency = MathHelper.ToRadians(5f * AttackSpeed);
            float currentRotation = (Projectile.Center - mountedCenter).ToRotation();
            if (!BeingHeld && currentRotation >= mouseRotation - leniency && currentRotation <= mouseRotation + leniency)
            {
                swinging = false;
                Projectile.velocity = initialVelocity;
                //Console.WriteLine(currentRotation - startRotation);
            }
        }

        void DoThrownAI(Player player)
        {
            var mountedCenter = player.MountedCenter;
            rotation = (Projectile.Center - mountedCenter).ToRotation();
            if ((Projectile.velocity == Vector2.Zero || Vector2.Distance(mountedCenter, Projectile.Center) >= 500 * AttackSpeed) && !returning) returning = true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            returning = true;
            return false;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Player player = Main.player[Projectile.owner];

            float collisionPoint4 = 0f;
            if ((BeingHeld || swinging) && Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), player.Center, Projectile.Center, 16f * Projectile.scale, ref collisionPoint4))
                return true;
            return base.Colliding(projHitbox, targetHitbox);
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.Inflate(15, 15);
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            var hitbox = Projectile.Hitbox;
            hitbox.Inflate(15, 15);

            if (BeingHeld || swinging) modifiers.HitDirectionOverride = (Main.player[Projectile.owner].Center.X < target.Center.X) ? 1 : -1;
            if (returning) modifiers.ScalingBonusDamage += 0.5f;
            if (!hitbox.Intersects(target.Hitbox)) modifiers.FinalDamage -= 0.25f;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            var player = Main.player[Projectile.owner];

            Vector2 mountedCenter = player.MountedCenter;
            Asset<Texture2D> chainTexture = ModContent.Request<Texture2D>(ChainTexturePath);

            var drawPosition = Projectile.Center;
            var remainingVectorToPlayer = mountedCenter - drawPosition;

            float rotation = remainingVectorToPlayer.ToRotation() - MathHelper.PiOver2;

            if (Projectile.alpha == 0)
            {
                int direction = -1;

                if (Projectile.Center.X < mountedCenter.X)
                    direction = 1;

                if (BeingHeld || swinging) player.itemRotation = 0f;
                else player.itemRotation = (float)Math.Atan2(remainingVectorToPlayer.Y * direction, remainingVectorToPlayer.X * direction);
            }

            // This while loop draws the chain texture from the projectile to the player, looping to draw the chain texture along the path
            while (true)
            {
                float length = remainingVectorToPlayer.Length();

                // Once the remaining length is small enough, we terminate the loop
                if (length < 20f || float.IsNaN(length))
                    break;

                // drawPosition is advanced along the vector back to the player by 12 pixels
                // 12 comes from the height of ExampleFlailProjectileChain.png and the spacing that we desired between links
                drawPosition += remainingVectorToPlayer * 12 / length;
                remainingVectorToPlayer = mountedCenter - drawPosition;

                // Finally, we draw the texture at the coordinates using the lighting information of the tile coordinates of the chain section
                Main.spriteBatch.Draw(chainTexture.Value, drawPosition - Main.screenPosition, null, lightColor, rotation, chainTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
            }

            return true;
        }
    }
}
