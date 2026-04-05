using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Projectiles.Ammo;
using ShardsOfAtheria.Projectiles.Magic.EntropyCatalyst;
using ShardsOfAtheria.Projectiles.Magic.Gambit;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Utilities;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace ShardsOfAtheria.Common.Projectiles
{
    public class HitscanBullet : ModProjectile
    {
        public readonly List<Vector2> recordedPositions = [];

        internal Color hitscanColor = Color.White;
        internal int thickness = 4;
        internal int bounces = 1;
        internal int coinsHit = 0;
        internal float penetrateDamageMultiplier = 0.8f;
        internal float bounceDamageMultiplier = 1.1f;
        internal float minimumDamagePercent = 0.05f;
        internal int normalStopTime = 10;

        internal int minimumDamage;
        internal int StopTime => (Projectile.extraUpdates + 1) * normalStopTime;

        public override string Texture => SoA.BlankTexture;

        /// <summary>
        /// Do not override, override StaticProperties instead.
        /// </summary>
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.DrawScreenCheckFluff[Type] = Main.maxTilesX * 16;
            StaticProperties();
        }

        public virtual void StaticProperties()
        {
            Projectile.AddDamageType(7);
            SoAGlobalProjectile.Metalic.Add(Type, 1f);
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 4;
            Projectile.extraUpdates = 199;
            Projectile.timeLeft = StopTime * 2;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = 0;
            Projectile.ignoreWater = true;
            Projectile.stopsDealingDamageAfterPenetrateHits = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 120;
        }

        public override void OnSpawn(IEntitySource source)
        {
            recordedPositions.Add(Projectile.Center);
            minimumDamage = (int)(Projectile.damage * minimumDamagePercent);
        }

        public override void AI()
        {
            if (Projectile.velocity != Vector2.Zero)
            {
                Projectile.velocity.Normalize();
                Projectile.velocity *= 2;
                int tileSize = 16;
                int tileMargin = 1 * tileSize;
                if (Projectile.Center.X > Main.maxTilesX * tileSize - tileMargin || Projectile.Center.Y > Main.maxTilesY * tileSize - tileMargin ||
                    Projectile.Center.X < tileMargin || Projectile.Center.Y < tileMargin)
                    Stop();
            }
            foreach (var proj in Main.ActiveProjectiles)
            {
                if (proj.owner == Projectile.owner && Projectile.Center.Distance(proj.Center) < 10)
                {
                    if (proj.type == ModContent.ProjectileType<ElecCoin>()) HitCoin(proj);
                    if (proj.type == ModContent.ProjectileType<PrideGold>() && proj.ai[0] >= 16f)
                    {
                        Stop();
                        (proj.ModProjectile as PrideGold).HitCoin();
                    }
                    if (proj.type == ModContent.ProjectileType<CatalystBomb>() && proj.active && proj.owner == Projectile.owner)
                    {
                        proj.damage = (int)(proj.damage * (1.1f + 0.1f * coinsHit));
                        proj.Kill();
                        proj.ModProjectile.OnKill(0);
                        Stop();
                    }
                }
            }
            if (Main.netMode == NetmodeID.MultiplayerClient) Projectile.netUpdate = true;
            if (Projectile.timeLeft == StopTime + 1) Stop();
        }

        public virtual void HitCoin(Projectile coin)
        {
            float maxDistance = Main.maxTilesX * 16f;
            coin.Kill();
            Projectile.Center = coin.Center;
            RecordPosition();
            Entity closestTarget = ShardsHelpers.FindClosestProjectile(Projectile.Center, maxDistance, proj => ValidTarget(proj, ModContent.ProjectileType<ElecCoin>()));
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
            coinsHit++;
            Projectile.GetPlayerOwner().Sinner().PrideCancelAttack();
            Projectile.timeLeft += StopTime;
        }

        public bool ValidTarget(Projectile projectile, int targetType)
        {
            if (!projectile.active) return false;
            if (projectile.type != targetType) return false;
            if (projectile.owner != Projectile.owner) return false;
            return true;
        }

        public virtual void RecordPosition()
        {
            //lastPositionIndex++;
            recordedPositions.Add(Projectile.Center);
        }

        public virtual void Stop()
        {
            Projectile.timeLeft = StopTime;
            Projectile.velocity *= 0f;
            Projectile.tileCollide = false;
            Projectile.netUpdate = true;
        }

        public override void CutTiles()
        {
            DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
            Vector2 start = recordedPositions[^1];
            Vector2 end = Projectile.Center + Projectile.velocity * 8f;
            Utils.PlotTileLine(start, end, thickness, DelegateMethods.CutTiles);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            RecordPosition();
            if (Projectile.damage > minimumDamage) Projectile.damage = (int)(Projectile.damage * penetrateDamageMultiplier);
            else if (Projectile.damage < minimumDamage) Projectile.damage = minimumDamage;
            if (Projectile.penetrate == 1) Stop();
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.Center, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.Center);
            RecordPosition();
            if (--bounces == 0) Stop();
            else
            {
                // If the projectile hits the left or right side of the tile, reverse the X velocity
                if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon)
                    Projectile.velocity.X = -oldVelocity.X * 1.05f;

                // If the projectile hits the top or bottom side of the tile, reverse the Y velocity
                if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
                    Projectile.velocity.Y = -oldVelocity.Y * 1.05f;

                float coneLength = 200f;
                var npc = Projectile.FindClosestNPC(ValidTarget, coneLength);
                bool ValidTarget(NPC target)
                {
                    float angle = MathHelper.ToRadians(45f);
                    return target.Hitbox.IntersectsConeFastInaccurate(Projectile.Center, coneLength, Projectile.velocity.ToRotation(), angle);
                }
                if (npc != null)
                {
                    var velocity = npc.Center - Projectile.Center;
                    velocity.Normalize();
                    Projectile.velocity = velocity *= 16f;
                }
                Projectile.timeLeft += StopTime;
                Projectile.damage = (int)(Projectile.damage * bounceDamageMultiplier);
            }
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            int thickness = this.thickness;
            Vector2 screenPos = Main.screenPosition;
            int lifeTime = Projectile.timeLeft;
            float percent = lifeTime / (float)StopTime;
            lightColor = hitscanColor;
            if (lifeTime < StopTime)
                thickness = (int)Math.Round(thickness * percent);
            for (int i = 0; i < recordedPositions.Count - 1; i++)
            {
                Main.spriteBatch.DrawLine(thickness, recordedPositions[i] - screenPos, recordedPositions[i + 1] - screenPos, lightColor);
            }
            if (recordedPositions.Count > 0) Main.spriteBatch.DrawLine(thickness, recordedPositions[^1] - screenPos, Projectile.Center - screenPos, lightColor);
            return false;
        }
    }

    public class HitscanBullet_Electric : HitscanBullet
    {
        public List<Vector2[]> electricPositions = [];

        public Vector2 DirectionNormal => Vector2.Normalize(Projectile.velocity);

        public Color arcColor = Color.Cyan;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.penetrate = -1;
            penetrateDamageMultiplier = 0.75f;
            normalStopTime = 30;
            thickness = 6;
        }

        public override void OnSpawn(IEntitySource source)
        {
            base.OnSpawn(source);
            electricPositions.Add([Projectile.Center, Projectile.Center, Projectile.Center]);
            SoundEngine.PlaySound(SoundID.Item72, Projectile.Center);
            SoundEngine.PlaySound(SoundID.Item43, Projectile.Center);
        }

        public override void AI()
        {
            base.AI();
            if (Projectile.velocity != Vector2.Zero && ++Projectile.ai[1] >= 16f - 16f / Projectile.extraUpdates)
            {
                Projectile.ai[1] = 0f;
                electricPositions.Add([
                    Projectile.Center + DirectionNormal.RotatedBy(MathHelper.PiOver2) * Main.rand.NextFloat(-20f, 20f),
                    Projectile.Center + DirectionNormal.RotatedBy(MathHelper.PiOver2) * Main.rand.NextFloat(-20f, 20f),
                    Projectile.Center + DirectionNormal.RotatedBy(MathHelper.PiOver2) * Main.rand.NextFloat(-20f, 20f),
                    ]);
            }
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            base.ModifyDamageHitbox(ref hitbox);
            int size = 30;
            hitbox.Inflate(size, size);
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            foreach (Projectile proj in Main.ActiveProjectiles)
            {
                if (proj.type == ProjectileID.NailFriendly && -proj.ai[0] - 1 == target.whoAmI)
                    modifiers.FlatBonusDamage += 10;
                if (proj.type == ModContent.ProjectileType<GoldenNail>() && -proj.ai[0] - 1 == target.whoAmI)
                    modifiers.FlatBonusDamage += 5;
                if (proj.type == ModContent.ProjectileType<StickingMagnetProj>() && proj.ai[1] == target.whoAmI)
                {
                    modifiers.FlatBonusDamage += 30;
                    proj.Kill();
                }
                if (proj.aiStyle == ProjAIStyleID.Nail)
                {
                    proj.ai[1] = 90;
                    proj.damage *= 0;
                    proj.friendly = false;
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = arcColor;
            int thickness = 2;
            int lifeTime = Projectile.timeLeft;
            float percent = lifeTime / (float)StopTime;
            if (lifeTime < StopTime)
                thickness = (int)Math.Round(thickness * percent);

            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < electricPositions.Count - 1; i++)
                {
                    Main.spriteBatch.DrawLine(thickness, electricPositions[i][j] - Main.screenPosition, electricPositions[i + 1][j] - Main.screenPosition, lightColor);
                }
                if (electricPositions.Count > 0) Main.spriteBatch.DrawLine(thickness, electricPositions[^1][j] - Main.screenPosition, Projectile.Center - Main.screenPosition, lightColor);
            }
            return base.PreDraw(ref lightColor);
        }
    }
    public class HitscanBullet_Bounce3 : HitscanBullet
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            bounces = 3;
            Projectile.penetrate = -1;
            hitscanColor = Color.IndianRed;
            penetrateDamageMultiplier = 0.9f;
            bounceDamageMultiplier = 1.2f;
        }
    }
    public class HitscanBullet_Melee : HitscanBullet
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.DamageType = DamageClass.Melee;
            hitscanColor = Color.Red;
        }
    }

    public class HitscanBullet_Magic : HitscanBullet
    {
        public override void StaticProperties()
        {
            Projectile.AddDamageType(4);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.DamageType = DamageClass.Magic;
            hitscanColor = Color.Cyan;
        }
    }

    public class HitscanBullet_Summon : HitscanBullet
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.DamageType = DamageClass.Summon;
            hitscanColor = Color.Pink;
        }
    }

    public class HitscanBullet_Explosive : HitscanBullet
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            hitscanColor = Color.Orange;
        }

        public override void Stop()
        {
            base.Stop();
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<FieryExplosion>(),
                (int)(Projectile.damage * 1.5f), Projectile.knockBack);
        }
    }
}
