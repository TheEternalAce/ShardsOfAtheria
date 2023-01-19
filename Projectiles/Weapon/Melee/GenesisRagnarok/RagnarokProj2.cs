﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using MMZeroElements;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Weapon.Magic;
using ShardsOfAtheria.Projectiles.Weapon.Melee.GenesisRagnarok.IceStuff;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee.GenesisRagnarok
{
    public class RagnarokProj2 : ModProjectile
    {
        public double rotation;
        private const string ChainTexturePath = "ShardsOfAtheria/Projectiles/Weapon/Melee/GenesisRagnarok/RagnarokProj2_GenesisChain";
        private const string AltChainTexturePath = "ShardsOfAtheria/Projectiles/Weapon/Melee/GenesisRagnarok/RagnarokProj2_GenesisChain_Alt";

        public override string Texture => "ShardsOfAtheria/Projectiles/Weapon/Melee/GenesisRagnarok/RagnarokProj";

        public override void SetStaticDefaults()
        {
            ProjectileElements.Ice.Add(Type);
            ProjectileElements.Fire.Add(Type);
            ProjectileElements.Electric.Add(Type);
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
            Projectile.light = .4f;

            Projectile.usesLocalNPCImmunity = true;

            DrawOffsetX = -4;
            DrawOriginOffsetY = -4;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            SoAPlayer shardsPlayer = player.GetModPlayer<SoAPlayer>();
            int upgrades = shardsPlayer.genesisRagnarockUpgrades;
            player.itemAnimation = 10;
            player.itemTime = 10;

            if (player.HeldItem.type == ModContent.ItemType<GenesisAndRagnarok>())
            {
                (player.HeldItem.ModItem as GenesisAndRagnarok).combo = 0;

                for (int num72 = 0; num72 < 2; num72++)
                {
                    Dust obj4 = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, upgrades < 5 ? DustID.Torch : DustID.Frost, 0f, 0f, 100, default,
                        upgrades < 5 ? 2f : .5f)];
                    obj4.noGravity = true;
                    obj4.velocity *= 2f;
                    obj4.velocity += Projectile.localAI[0].ToRotationVector2();
                    obj4.fadeIn = 1.5f;
                }
                if (upgrades == 5 && Main.rand.NextBool(10))
                {
                    Projectile proj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Normalize(Main.MouseWorld - Projectile.Center) * 5,
                        ModContent.ProjectileType<LightningBoltFriendly>(), Projectile.damage, Projectile.knockBack, player.whoAmI);
                    proj.DamageType = DamageClass.Melee;
                }
            }

            int mouseDirection = Main.MouseWorld.X > player.Center.X ? 1 : -1;
            if (Projectile.ai[0] == 0)
            {
                if (mouseDirection == 1)
                    rotation = -0.5;
                else rotation = 2;
                Projectile.ai[0] = 1;
            }

            if (mouseDirection == 1)
                rotation -= .33;
            else rotation += .33;
            player.itemAnimation = 10;

            int newDirection = Main.MouseWorld.X > player.Center.X ? 1 : -1;
            player.ChangeDir(newDirection);
            Projectile.direction = newDirection;
            if (!Main.mouseLeft || player.dead || !player.active)
            {
                Projectile.velocity = Vector2.Normalize(player.Center - Projectile.Center) * 31;
            }
            else
            {
                Projectile.Center = Main.GetPlayerArmPosition(Projectile) + Vector2.One.RotatedBy(rotation) * 180;
                Projectile.netUpdate = true;
            }

            if (Projectile.getRect().Intersects(player.getRect()))
                Projectile.Kill();
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            SoAPlayer shardsPlayer = player.GetModPlayer<SoAPlayer>();
            int upgrades = shardsPlayer.genesisRagnarockUpgrades;

            if (player.HeldItem.type == ModContent.ItemType<GenesisAndRagnarok>())
            {
                if (upgrades < 5 && upgrades >= 3)
                {
                    target.AddBuff(BuffID.OnFire, 600);
                }
                else if (upgrades == 5)
                {
                    target.AddBuff(BuffID.Frostburn, 600);
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<IceVortexShard>(), Projectile.damage, Projectile.knockBack, player.whoAmI);
                }
            }
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Player player = Main.player[Projectile.owner];

            float collisionPoint4 = 0f;
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), player.Center, Projectile.Center, 16f * Projectile.scale, ref collisionPoint4))
            {
                return true;
            }
            else if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.position, Projectile.position + new Vector2(60, 0), 16f * Projectile.scale, ref collisionPoint4))
            {
                return true;
            }
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            var player = Main.player[Projectile.owner];
            SoAPlayer shardsPlayer = player.GetModPlayer<SoAPlayer>();
            int upgrades = shardsPlayer.genesisRagnarockUpgrades;

            Vector2 handPosition = Main.GetPlayerArmPosition(Projectile);
            Asset<Texture2D> chainTexture = ModContent.Request<Texture2D>(upgrades < 5 ? ChainTexturePath : AltChainTexturePath);

            var drawPosition = Projectile.Center;
            var remainingVectorToPlayer = handPosition - drawPosition;

            float rotation = remainingVectorToPlayer.ToRotation() - MathHelper.PiOver2;

            if (Projectile.alpha == 0)
            {
                int direction = -1;
                if (Main.MouseWorld.X < handPosition.X && Main.myPlayer == Projectile.owner)
                    direction = 1;

                if (direction == 1)
                    player.itemRotation = 0;
                else player.itemRotation = 0;
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
                drawPosition += remainingVectorToPlayer * 14 / length;
                remainingVectorToPlayer = handPosition - drawPosition;

                // Finally, we draw the texture at the coordinates using the lighting information of the tile coordinates of the chain section
                Main.spriteBatch.Draw(chainTexture.Value, drawPosition - Main.screenPosition, null, Color.White, rotation, chainTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
            }
            lightColor = Color.White;

            return true;
        }
    }
}
