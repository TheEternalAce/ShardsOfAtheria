using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Projectiles.NPCProj;
using ShardsOfAtheria.Projectiles.Weapon.Melee.GenesisRagnarok.IceStuff;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee.GenesisRagnarok
{
    public class Genesis_Sword : ModProjectile
    {
        public float rotation = 45;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Genesis Sword");
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Projectile.owner];

            if (player.HeldItem.type == ModContent.ItemType<GenesisAndRagnarok>())
            {
                if ((player.HeldItem.ModItem as GenesisAndRagnarok).upgrades < 5 && (player.HeldItem.ModItem as GenesisAndRagnarok).upgrades >= 3)
                    target.AddBuff(BuffID.OnFire, 600);
                else if ((player.HeldItem.ModItem as GenesisAndRagnarok).upgrades == 5)
                {
                    target.AddBuff(BuffID.Frostburn, 600);
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<IceExplosion>(), Projectile.damage, Projectile.knockBack, player.whoAmI);
                }
            }
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.aiStyle = 75;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.light = 1f;
            Projectile.penetrate = -1;
            Projectile.extraUpdates = 1;
            DrawOffsetX = -31;
            DrawOriginOffsetY = -6;
            Projectile.DamageType = DamageClass.Melee;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            player.itemAnimation = 10;
            player.itemTime = 10;

            // Centering
            Vector2 value21 = Main.OffsetsPlayerOnhand[player.bodyFrame.Y / 56] * 2f;
            if (player.direction != 1)
            {
                value21.X = (float)player.bodyFrame.Width - value21.X;
            }
            if (player.gravDir != 1f)
            {
                value21.Y = (float)player.bodyFrame.Height - value21.Y;
            }
            value21 -= new Vector2((float)(player.bodyFrame.Width - player.width), (float)(player.bodyFrame.Height - 42)) / 2f;
            Projectile.Center = player.RotatedRelativePoint(player.MountedCenter - new Vector2(20f, 42f) / 2f + value21, reverseRotation: false, addGfxOffY: false) - Projectile.velocity;

            // Behavior
            if (player.whoAmI == Projectile.owner)
            {
                Projectile.ai[0] += 1f;
                if (Projectile.ai[0] > 60f)
                {
                    Projectile.Kill();
                }
                else player.itemAnimation = 10;

                int newDirection = Projectile.Center.X > player.Center.X ? -1 : 1;
                player.ChangeDir(newDirection);
                Projectile.direction = newDirection;

                if (Projectile.ai[0] == 30f)
                    SoundEngine.PlaySound(SoundID.DD2_MonkStaffSwing);
                if (Projectile.ai[0] > 30f)
                    rotation -= 6;
                else
                    rotation += 6;
                if (Projectile.direction == 1)
                    Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(rotation + 135);
                else Projectile.rotation = Projectile.velocity.ToRotation() - MathHelper.ToRadians(rotation - 45);

                if (player.HeldItem.type == ModContent.ItemType<GenesisAndRagnarok>())
                {
                    if ((Projectile.ai[0] == 1f || Projectile.ai[0] == 30f) && (player.HeldItem.ModItem as GenesisAndRagnarok).upgrades == 5)
                    {
                        float numberProjectiles = 3;
                        float shardRotation = MathHelper.ToRadians(15);
                        Vector2 position = Projectile.position;
                        Vector2 velocity = Vector2.Normalize(Main.MouseWorld - player.Center) * 16;
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-shardRotation, shardRotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                            Projectile.NewProjectile(Projectile.GetSource_FromThis(), position, perturbedSpeed, ModContent.ProjectileType<IceShard>(), Projectile.damage, Projectile.knockBack, player.whoAmI);
                        }
                    }

                    // Dust stuff
                    if ((player.HeldItem.ModItem as GenesisAndRagnarok).upgrades >= 3)
                    {
                        if (Projectile.alpha == 0)
                        {
                            if (Projectile.direction == 1)
                            {
                                float ro = Projectile.rotation + MathHelper.ToRadians(90);
                                for (int num72 = 0; num72 < 2; num72++)
                                {
                                    Dust obj4 = Main.dust[Dust.NewDust(Projectile.Hitbox.Center.ToVector2() + Projectile.velocity + ro.ToRotationVector2().SafeNormalize(Vector2.Zero) * 130f, Projectile.width, Projectile.height,
                                        (player.HeldItem.ModItem as GenesisAndRagnarok).upgrades < 5 ? DustID.Torch : DustID.Frost, 0f, 0f, 100, default, (player.HeldItem.ModItem as GenesisAndRagnarok).upgrades < 5 ? 2f : .5f)];
                                    obj4.noGravity = true;
                                    obj4.velocity *= 2f;
                                    obj4.velocity += Projectile.localAI[0].ToRotationVector2();
                                    obj4.fadeIn = 1.5f;
                                }
                            }
                            else
                            {
                                float ro = Projectile.rotation + MathHelper.ToRadians(90);
                                for (int num72 = 0; num72 < 2; num72++)
                                {
                                    Dust obj4 = Main.dust[Dust.NewDust(Projectile.Hitbox.Center.ToVector2() + Projectile.velocity + ro.ToRotationVector2().SafeNormalize(Vector2.Zero) * 170f, Projectile.width, Projectile.height,
                                        (player.HeldItem.ModItem as GenesisAndRagnarok).upgrades < 5 ? DustID.Torch : DustID.Frost, 0f, 0f, 100, default, (player.HeldItem.ModItem as GenesisAndRagnarok).upgrades < 5 ? 2f : .5f)];
                                    obj4.noGravity = true;
                                    obj4.velocity *= 2f;
                                    obj4.velocity += Projectile.localAI[0].ToRotationVector2();
                                    obj4.fadeIn = 1.5f;
                                }
                            }
                        }
                    }
                }
            }
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float ro = Projectile.rotation + MathHelper.ToRadians(90);
            float collisionPoint4 = 0f;
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Hitbox.Center.ToVector2(), Projectile.Center + Projectile.velocity + ro.ToRotationVector2().SafeNormalize(Vector2.Zero) * 130f, 16f * Projectile.scale, ref collisionPoint4))
            {
                return true;
            }
            return false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            var player = Main.player[Projectile.owner];

            Vector2 mountedCenter = player.MountedCenter;

            var drawPosition = Main.MouseWorld;
            var remainingVectorToPlayer = mountedCenter - drawPosition;

            if (Projectile.alpha == 0 && player.whoAmI == Projectile.owner)
            {
                int direction = -1;

                if (Projectile.Center.X < mountedCenter.X)
                    direction = 1;

                player.itemRotation = (float)Math.Atan2(remainingVectorToPlayer.Y * direction, remainingVectorToPlayer.X * direction);
            }
            return true;
        }
    }
}