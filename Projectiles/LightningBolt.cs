using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles
{

    public class LightningBolt : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning Bolt");
        }

        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 200;

            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.tileCollide = true;
            Projectile.light = 1f;
            Projectile.extraUpdates = 19;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 60;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(ModContent.BuffType<ElectricShock>(), 10 * 60);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = Vector2.Zero;
            return false;
        }

        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Thunder, Projectile.position);
        }
    }
    public class LightningBoltFriendly : ModProjectile
    {
        public override string Texture => "ShardsOfAtheria/Projectiles/LightningBolt";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning Bolt");
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ModContent.ProjectileType<LightningBolt>());

            Projectile.DamageType = DamageClass.Magic;
            Projectile.friendly = true;
            Projectile.hostile = false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<ElectricShock>(), 10 * 60);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity = Vector2.Zero;
            return false;
        }

        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Thunder, Projectile.position);
        }
    }

    public class LightningBoltSpawner : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning Bolt");
        }

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;

            Projectile.aiStyle = -1;
            Projectile.tileCollide = true;
            Projectile.light = 1f;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 60;
        }

        public override void AI()
        {
            if (Main.rand.NextBool(3))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Electric,
                       Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 200, Scale: 1f);
                dust.velocity += Projectile.velocity * 0.3f;
                dust.velocity *= 0.2f;
            }
        }

        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), Projectile.position, new Vector2(0, 10), ModContent.ProjectileType<LightningBolt>(), Projectile.damage, Projectile.knockBack, Main.myPlayer);
        }
    }
}
