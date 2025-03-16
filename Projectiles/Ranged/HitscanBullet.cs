using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Projectiles.Magic.EntropyCatalyst;
using ShardsOfAtheria.Projectiles.Magic.Gambit;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace ShardsOfAtheria.Projectiles.Ranged
{
    public class HitscanBullet : ModProjectile
    {
        public virtual Color HitscanColor => Color.White;
        public virtual int thickness => 4;

        public List<Vector2> recordedPositions = [];
        public int positionsIndex = 0;
        public int Bounces
        {
            get => (int)Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }
        public int coinsHit = 0;

        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(7);
            SoAGlobalProjectile.Metalic.Add(Type, 1f);
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 4;
            Projectile.extraUpdates = 199;
            Projectile.timeLeft *= 5;
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
        }

        public override void AI()
        {
            if (Projectile.velocity != Vector2.Zero)
            {
                Projectile.velocity.Normalize();
                Projectile.velocity *= 2;
            }
            foreach (var proj in Main.projectile)
            {
                if (proj.active && proj.owner == Projectile.owner)
                {
                    if (Projectile.Distance(proj.Center) < 10)
                    {
                        if (proj.type == ModContent.ProjectileType<ElecCoin>()) HitCoin(proj);
                        if (proj.type == ModContent.ProjectileType<CatalystBomb>() && proj.active && proj.owner == Projectile.owner)
                        {
                            proj.damage = (int)(proj.damage * (1.1f + 0.1f * coinsHit));
                            proj.Kill();
                            proj.ModProjectile.OnKill(0);
                            Stop();
                        }
                    }
                }
            }
            if (Main.netMode == NetmodeID.MultiplayerClient) Projectile.netUpdate = true;
            if (Projectile.timeLeft == (Projectile.extraUpdates + 1) * 4 + 1) Stop();
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
                }
            }
            Projectile.damage = (int)(Projectile.damage * 1.1f);
            coinsHit++;
        }

        public virtual void RecordPosition()
        {
            positionsIndex++;
            recordedPositions.Add(Projectile.Center);
        }

        public bool ValidTarget(Projectile projectile, int targetType)
        {
            if (!projectile.active) return false;
            if (projectile.type != targetType) return false;
            if (projectile.owner != Projectile.owner) return false;
            return true;
        }

        public virtual void Stop()
        {
            Projectile.timeLeft = (Projectile.extraUpdates + 1) * 4;
            Projectile.velocity *= 0f;
            Projectile.tileCollide = false;
            Projectile.netUpdate = true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            RecordPosition();
            if (Projectile.penetrate == 1) Stop();
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            RecordPosition();
            if (--Projectile.ai[0] <= 0) Stop();
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = HitscanColor;
            for (int i = 0; i < recordedPositions.Count - 1; i++)
            {
                Main.spriteBatch.DrawLine(thickness, recordedPositions[i] - Main.screenPosition, recordedPositions[i + 1] - Main.screenPosition, lightColor);
            }
            if (recordedPositions.Count > 0) Main.spriteBatch.DrawLine(thickness, recordedPositions[positionsIndex] - Main.screenPosition, Projectile.Center - Main.screenPosition, lightColor);
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            base.OnKill(timeLeft);
        }
    }

    public class HitscanBullet_Melee : HitscanBullet
    {
        public override Color HitscanColor => Color.Red;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.DamageType = DamageClass.Melee;
        }
    }

    public class HitscanBullet_Magic : HitscanBullet
    {
        public override Color HitscanColor => Color.Cyan;

        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(4);
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.DamageType = DamageClass.Magic;
        }
    }

    public class HitscanBullet_Summon : HitscanBullet
    {
        public override Color HitscanColor => Color.Pink;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.DamageType = DamageClass.Summon;
        }
    }

    public class HitscanBullet_Explosive : HitscanBullet
    {
        public override Color HitscanColor => Color.Orange;

        public override void Stop()
        {
            base.Stop();
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<FieryExplosion>(),
                (int)(Projectile.damage * 1.5f), Projectile.knockBack);
        }
    }
}
