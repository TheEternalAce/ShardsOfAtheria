using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Magic.EntropyCatalyst;
using ShardsOfAtheria.Projectiles.Magic.Gambit;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Utilities;
using System;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Common.Projectiles
{
    public abstract class BasicBeam : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public int bounces = 0;
        public float dustFadeIn = 0f;
        public int dustDelay = 6;
        public int dustTimer = 0;
        public abstract int DustType { get; }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 4;
            Projectile.extraUpdates = 199;
            Projectile.timeLeft *= Projectile.extraUpdates;
            Projectile.friendly = true;
            Projectile.alpha = 255;
            Projectile.aiStyle = 0;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            if (Projectile.velocity != Vector2.Zero)
            {
                Projectile.velocity.Normalize();
                Projectile.velocity *= 2;
            }

            Vector2 spawnPos = Projectile.Center;
            if ((Math.Abs(spawnPos.X) > Main.screenWidth * 0.6f || Math.Abs(spawnPos.Y) > Main.screenHeight * 0.6f) && ++dustTimer >= dustDelay)
            {
                Dust d = Dust.NewDustPerfect(spawnPos, DustType, Vector2.Zero);
                d.velocity *= 0;
                d.fadeIn = dustFadeIn;
                d.noGravity = true;
            }

            foreach (var proj in Main.ActiveProjectiles)
            {
                if (proj.owner == Projectile.owner && proj.whoAmI != Projectile.whoAmI)
                {
                    if (Projectile.Distance(proj.Center) < 10)
                    {
                        if (proj.type == ModContent.ProjectileType<ElecCoin>()) HitCoin(proj);
                        if (proj.type == ModContent.ProjectileType<PrideGold>() && proj.ai[0] >= 16f)
                        {
                            Projectile.Kill();
                            (proj.ModProjectile as PrideGold).HitCoin();
                        }
                        if (proj.type == ModContent.ProjectileType<CatalystBomb>() && proj.active && proj.owner == Projectile.owner)
                        {
                            proj.damage = (int)(proj.damage * 1.1f);
                            proj.Kill();
                            proj.ModProjectile.OnKill(0);
                        }
                    }
                }
            }
            if (Main.netMode == NetmodeID.MultiplayerClient) Projectile.netUpdate = true;
        }

        public virtual void HitCoin(Projectile coin)
        {
            float maxDistance = Main.maxTilesX * 16f;
            coin.Kill();
            Projectile.Center = coin.Center;
            Entity closestTarget = ShardsHelpers.FindClosestProjectile(Projectile.Center, maxDistance, ValidCoin);
            if (closestTarget != null) Projectile.velocity = Projectile.Center.DirectionTo(closestTarget.Center) * Projectile.velocity.Length();
            else
            {
                closestTarget = ShardsHelpers.FindClosestProjectile(Projectile.Center, maxDistance, proj => ValidTarget(proj, ModContent.ProjectileType<CatalystBomb>()));
                if (closestTarget != null) Projectile.velocity = Projectile.Center.DirectionTo(closestTarget.Center) * Projectile.velocity.Length();
                else
                {
                    closestTarget = ShardsHelpers.FindClosestNPC(coin, null, maxDistance);
                    if (closestTarget != null) Projectile.velocity = Projectile.Center.DirectionTo(closestTarget.Center) * Projectile.velocity.Length();
                    else Projectile.velocity = Projectile.velocity.RotatedByRandom(MathHelper.TwoPi);
                }
            }
            Projectile.damage = (int)(Projectile.damage * 1.1f);
            Projectile.GetPlayerOwner().Sinner().PrideCancelAttack();
        }

        public bool ValidCoin(Projectile projectile)
        {
            return ValidTarget(projectile, ModContent.ProjectileType<ElecCoin>(),
                ModContent.ProjectileType<PrideGold>());
        }

        public bool ValidTarget(Projectile projectile, params int[] targetType)
        {
            if (!projectile.active) return false;
            if (projectile.owner != Projectile.owner) return false;
            if (!targetType.Contains(projectile.type)) return false;
            return true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.Center);

            if (bounces-- != 0)
            {
                // If the projectile hits the left or right side of the tile, reverse the X velocity
                if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon)
                    Projectile.velocity.X = -oldVelocity.X;

                // If the projectile hits the top or bottom side of the tile, reverse the Y velocity
                if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
                    Projectile.velocity.Y = -oldVelocity.Y;

                return false;
            }
            return true;
        }
    }
}
