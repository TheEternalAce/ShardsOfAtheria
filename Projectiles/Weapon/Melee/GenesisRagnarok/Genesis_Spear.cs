﻿using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Projectiles.Weapon.Melee.GenesisRagnarok.IceStuff;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee.GenesisRagnarok
{
    public class Genesis_Spear : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Genesis");
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
            Projectile.width = 36;
            Projectile.height = 36;
            Projectile.aiStyle = 19;
            Projectile.penetrate = -1;
            Projectile.alpha = 0;

            Projectile.hide = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
        }

        // In here the AI uses this example, to make the code more organized and readable
        // Also showcased in ExampleJavelinProjectile.cs
        public float MovementFactor // Change this value to alter how fast the spear moves
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        // It appears that for this AI, only the ai0 field is used!
        public override void AI()
        {
            // Since we access the owner player instance so much, it's useful to create a helper local variable for this
            // Sadly, Projectile/ModProjectile does not have its own
            Player player = Main.player[Projectile.owner];

            // Here we set some of the projectile's owner properties, such as held item and itemtime, along with projectile direction and position based on the player
            Vector2 ownerMountedCenter = player.RotatedRelativePoint(player.MountedCenter, true);
            Projectile.direction = player.direction;
            player.heldProj = Projectile.whoAmI;
            player.itemTime = player.itemAnimation;
            Projectile.position.X = ownerMountedCenter.X - (float)(Projectile.width / 2);
            Projectile.position.Y = ownerMountedCenter.Y - (float)(Projectile.height / 2);
            // As long as the player isn't frozen, the spear can move
            if (!player.frozen)
            {
                if (MovementFactor == 0f) // When initially thrown out, the ai0 will be 0f
                {
                    MovementFactor = 3f; // Make sure the spear moves forward when initially thrown out
                    Projectile.netUpdate = true; // Make sure to netUpdate this spear
                }
                if (player.itemAnimation < player.itemAnimationMax / 3) // Somewhere along the item animation, make sure the spear moves back
                {
                    MovementFactor -= 2.4f;
                }
                else // Otherwise, increase the movement factor
                {
                    MovementFactor += 2.1f;
                }
            }
            // Change the spear position based off of the velocity and the movementFactor
            Projectile.position += Projectile.velocity * MovementFactor;
            // When we reach the end of the animation, we can kill the spear projectile
            if (player.itemAnimation == 0)
            {
                Projectile.Kill();
            }
            // Apply proper rotation, with an offset of 135 degrees due to the sprite's rotation, notice the usage of MathHelper, use this class!
            // MathHelper.ToRadians(xx degrees here)
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);
            // Offset by 90 degrees here
            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation -= MathHelper.ToRadians(90f);
            }

            if (player.HeldItem.type == ModContent.ItemType<GenesisAndRagnarok>())
            {
                if (Projectile.ai[1] == 0 && Main.myPlayer == player.whoAmI && (player.HeldItem.ModItem as GenesisAndRagnarok).upgrades == 5)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Normalize(Main.MouseWorld - Projectile.Center) * 16, ModContent.ProjectileType<IceShard>(), Projectile.damage,
                        Projectile.knockBack, player.whoAmI);
                    Projectile.ai[1] = 1;
                }

                if ((Main.LocalPlayer.HeldItem.ModItem as GenesisAndRagnarok).upgrades >= 3)
                {
                    for (int num72 = 0; num72 < 2; num72++)
                    {
                        Dust obj4 = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, (player.HeldItem.ModItem as GenesisAndRagnarok).upgrades < 5 ? DustID.Torch : DustID.Frost, 0f, 0f, 100, default,
                            (player.HeldItem.ModItem as GenesisAndRagnarok).upgrades < 5 ? 2f : .5f)];
                        obj4.noGravity = true;
                        obj4.velocity *= 2f;
                        obj4.velocity += Projectile.localAI[0].ToRotationVector2();
                        obj4.fadeIn = 1.5f;
                    }
                }
            }
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

            if (Projectile.alpha == 0)
            {
                int direction = -1;

                if (Main.MouseWorld.X < mountedCenter.X)
                    direction = 1;

                player.itemRotation = (float)Math.Atan2(remainingVectorToPlayer.Y * direction, remainingVectorToPlayer.X * direction);
            }
            return true;
        }
    }
}
