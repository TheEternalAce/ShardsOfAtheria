using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Projectiles.Magic.Gambit;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace ShardsOfAtheria.Projectiles.Ranged
{
    public class HitscanBullet : ModProjectile
    {
        public virtual Color HitscanColor => Color.Yellow;
        public List<Vector2> recordedPositions = [];
        public int positionsIndex = 0;
        public int Bounces
        {
            get => (int)Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
            SoAGlobalProjectile.Metalic.Add(Type, 1f);
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 2;
            Projectile.extraUpdates = 199;
            Projectile.timeLeft *= 5;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = 0;
            Projectile.ignoreWater = true;
            Projectile.stopsDealingDamageAfterPenetrateHits = true;
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
            foreach (var coin in Main.projectile)
            {
                if (coin.type == ModContent.ProjectileType<ElecCoin>() && coin.active && coin.owner == Projectile.owner)
                {
                    if (Projectile.Distance(coin.Center) < 10)
                    {
                        HitCoin(coin);
                        break;
                    }
                }
            }
        }

        public virtual void HitCoin(Projectile coin)
        {
            coin.Kill();
            Projectile.Center = coin.Center;
            RecordPosition();
            var closestCoin = ShardsHelpers.FindClosestProjectile(Projectile.Center, 2000, ValidCoin);
            if (closestCoin != null) Projectile.velocity = Projectile.Center.DirectionTo(closestCoin.Center) * coin.velocity.Length();
            else
            {
                var closestEnemy = ShardsHelpers.FindClosestNPC(coin, null, 2000);
                if (closestEnemy != null) Projectile.velocity = Projectile.Center.DirectionTo(closestEnemy.Center) * coin.velocity.Length();
            }
            Projectile.damage = (int)(Projectile.damage * 1.1f);
        }

        public virtual void RecordPosition()
        {
            positionsIndex++;
            recordedPositions.Add(Projectile.Center);
        }

        public bool ValidCoin(Projectile projectile)
        {
            if (!projectile.active) return false;
            if (projectile.type != ModContent.ProjectileType<ElecCoin>()) return false;
            if (projectile.owner != Projectile.owner) return false;
            return true;
        }

        public void Stop()
        {
            Projectile.timeLeft = (Projectile.extraUpdates + 1) * 4;
            Projectile.velocity *= 0f;
            Projectile.tileCollide = false;
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
            int thickness = 4;
            for (int i = 0; i < recordedPositions.Count - 1; i++)
            {
                Main.spriteBatch.DrawLine(thickness, recordedPositions[i] - Main.screenPosition, recordedPositions[i + 1] - Main.screenPosition, lightColor);
            }
            Main.spriteBatch.DrawLine(thickness, recordedPositions[positionsIndex] - Main.screenPosition, Projectile.Center - Main.screenPosition, lightColor);
            return false;
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
}
