using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Ammo
{
    public class AreusArrowProj : ModProjectile
    {
        Vector2 point;

        public override void SetStaticDefaults()
        {
            Projectile.AddAreus();
            Projectile.AddDamageType(7);
        }

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 120;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 5;

            DrawOffsetX = 2;
        }

        public override void OnSpawn(IEntitySource source)
        {
            if (Main.myPlayer == Projectile.owner)
            {
                Player player = Main.player[Projectile.owner];
                SoundEngine.PlaySound(SoundID.Item91, Projectile.Center);
                point = (Projectile.Center + Vector2.Normalize(Projectile.velocity) * Vector2.Distance(player.Center, Main.MouseWorld));
            }
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            if (point != Vector2.Zero)
            {
                Dust dust = Dust.NewDustPerfect(point, DustID.Electric, Vector2.Zero);
                dust.noGravity = true;
                if (Projectile.Distance(point) < 16f)
                    Projectile.Kill();
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.Center);
            Projectile.ai[0] = 1;
            return base.OnTileCollide(oldVelocity);
        }

        public override void OnKill(int timeLeft)
        {
            if (Main.myPlayer == Projectile.owner && Projectile.ai[0] == 0)
            {
                Player player = Main.player[Projectile.owner];
                SoundEngine.PlaySound(SoundID.Item74, Projectile.Center);

                int damage = Projectile.damage;
                damage /= 3;
                float offset = MathHelper.ToRadians(Main.rand.NextFloat(60f));
                for (int i = 0; i < 6; i++)
                {
                    var velocity = new Vector2(1, 0).RotatedBy(MathHelper.ToRadians(60f * i) + offset) * 16f;
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, velocity,
                        ModContent.ProjectileType<ElectricArrow>(), damage, Projectile.knockBack, player.whoAmI);
                    Projectile.netUpdate = true;
                }
            }
        }
    }
}
