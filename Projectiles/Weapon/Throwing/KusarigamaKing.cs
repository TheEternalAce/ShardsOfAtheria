using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MMZeroElements;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Throwing
{
    public class KusarigamaKing : ModProjectile
    {
        float rotation;
        int gravityTimer;
        private const string ChainTexturePath = "ShardsOfAtheria/Projectiles/Weapon/Throwing/KingsKusarigama_Chain";

        public override void SetStaticDefaults()
        {
            ProjectileElements.Ice.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.ownerHitCheck = true;

            DrawOffsetX = -8;
            DrawOriginOffsetY = -4;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            player.itemAnimation = 10;
            player.itemTime = 10;

            var handPosition = Main.GetPlayerArmPosition(Projectile);
            Projectile.rotation = (Projectile.Center - handPosition).ToRotation() + MathHelper.ToRadians(90);
            if (Projectile.Distance(handPosition) >= 200)
            {
                Projectile.tileCollide = true;
            }
            if (returning)
            {
                DoReturnAI(player);
            }
            else if (Projectile.ai[1] == 0)
            {
                DoThrownAI(player);
            }
            else
            {
                DoSwingAI(player);
            }
        }

        void DoReturnAI(Player player)
        {
            if (Projectile.tileCollide)
            {
                Projectile.tileCollide = false;
            }
            Projectile.velocity = Vector2.Normalize(player.Center - Projectile.Center) * 32;
            Projectile.spriteDirection = Projectile.direction;
            if (Projectile.getRect().Intersects(player.getRect()))
            {
                Projectile.Kill();
            }
        }

        void DoSwingAI(Player player)
        {
            if (Projectile.velocity != Vector2.Zero)
            {
                Projectile.velocity *= 0;
            }
            var handPosition = Main.GetPlayerArmPosition(Projectile);

            int mouseDirection = Main.MouseWorld.X > player.Center.X ? 1 : -1;
            if (Projectile.ai[0] == 0)
            {
                if (rotation == 0)
                {
                    float startingDegrees;
                    startingDegrees = MathHelper.ToRadians(180);
                    if (mouseDirection == -1)
                    {
                        startingDegrees = 0;
                    }
                    rotation = startingDegrees;
                }
                Projectile.ai[0] = 1;
            }

            float rotationToAdd = MathHelper.ToRadians(15) * mouseDirection;
            rotation += rotationToAdd;
            player.itemAnimation = 10;

            Projectile.rotation += rotationToAdd;

            int newDirection = Main.MouseWorld.X > player.Center.X ? 1 : -1;
            player.ChangeDir(newDirection);
            Projectile.direction = newDirection;
            Projectile.spriteDirection = -mouseDirection;
            if (!Main.mouseLeft || player.dead || !player.active)
            {
                returning = true;
                Projectile.ai[1] = 0;
            }
            else
            {
                Projectile.Center = handPosition + new Vector2(1, 0).RotatedBy(rotation) * 180;
                Projectile.netUpdate = true;
            }

            if (Projectile.getRect().Intersects(player.getRect()))
            {
                Projectile.Kill();
            }
        }

        bool returning = false;
        void DoThrownAI(Player player)
        {
            var handPosition = Main.GetPlayerArmPosition(Projectile);

            Projectile.spriteDirection = -Projectile.direction;
            rotation = (Projectile.Center - handPosition).ToRotation();

            int gravityTimerMax = 26;
            if (++gravityTimer >= gravityTimerMax)
            {
                if (++Projectile.velocity.Y >= 16)
                {
                    Projectile.velocity.Y = 16;
                }
            }
            if (Main.myPlayer == player.whoAmI)
            {
                var dist = Vector2.Distance(handPosition, Projectile.Center);
                if (Main.mouseLeft && dist >= 180 && dist <= 200)
                {
                    Projectile.ai[1] = 1;
                }
            }
            if (Vector2.Distance(handPosition, Projectile.Center) >= 500 && !returning)
            {
                returning = true;
            }
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
            if (Projectile.ai[1] == 0 || returning)
            {
                return base.Colliding(projHitbox, targetHitbox);
            }
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), player.Center, Projectile.Center, 16f * Projectile.scale, ref collisionPoint4))
            {
                return true;
            }
            return base.Colliding(projHitbox, targetHitbox);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            var player = Main.player[Projectile.owner];

            Vector2 handPosition = Main.GetPlayerArmPosition(Projectile);
            Asset<Texture2D> chainTexture = ModContent.Request<Texture2D>(ChainTexturePath);

            var drawPosition = Projectile.Center;
            var remainingVectorToPlayer = handPosition - drawPosition;

            float rotation = remainingVectorToPlayer.ToRotation() - MathHelper.PiOver2;

            if (Projectile.alpha == 0)
            {
                player.itemRotation = 0;
            }

            // This while loop draws the chain texture from the projectile to the player, looping to draw the chain texture along the path
            while (true)
            {
                float length = remainingVectorToPlayer.Length();

                // Once the remaining length is small enough, we terminate the loop
                if (length < 25f || float.IsNaN(length))
                    break;

                // drawPosition is advanced along the vector back to the player by 12 pixels
                // 12 comes from the height of ExampleFlailProjectileChain.png and the spacing that we desired between links
                drawPosition += remainingVectorToPlayer * 12 / length;
                remainingVectorToPlayer = handPosition - drawPosition;

                // Finally, we draw the texture at the coordinates using the lighting information of the tile coordinates of the chain section
                Main.spriteBatch.Draw(chainTexture.Value, drawPosition - Main.screenPosition, null, Color.White, rotation, chainTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
            }
            lightColor = Color.White;

            return true;
        }
    }
}
