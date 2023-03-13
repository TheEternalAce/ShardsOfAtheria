using ShardsOfAtheria.Buffs.AnyDebuff;
using MMZeroElements;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee
{
    public class ReactorMeltdownProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // The following sets are only applicable to yoyo that use aiStyle 99.
            // YoyosLifeTimeMultiplier is how long in seconds the yoyo will stay out before automatically returning to the player. 
            // Vanilla values range from 3f(Wood) to 16f(Chik), and defaults to -1f. Leaving as -1 will make the time infinite.
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = -1f;
            // YoyosMaximumRange is the maximum distance the yoyo sleep away from the player. 
            // Vanilla values range from 130f(Wood) to 400f(Terrarian), and defaults to 200f
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 279;
            // YoyosTopSpeed is top speed of the yoyo Projectile. 
            // Vanilla values range from 9f(Wood) to 17.5f(Terrarian), and defaults to 10f
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 10f;
            Main.projFrames[Projectile.type] = 2;

            ProjectileElements.Electric.Add(Type);
        }
        public override void SetDefaults()
        {
            Projectile.extraUpdates = 0;
            Projectile.width = 16;
            Projectile.height = 16;
            // aiStyle 99 is used for all yoyos, and is Extremely suggested, as yoyo are extremely difficult without them
            Projectile.aiStyle = 99;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.scale = 1f;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<ElectricShock>(), 600);
        }

        public override void AI()
        {
            if (++Projectile.frameCounter >= 60)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= 2)
                {
                    Projectile.frame = 0;
                    SoundEngine.PlaySound(new SoundStyle("ShardsOfAtheria/Sounds/Item/ReactorMeltdownAlarm")
                    {
                        Volume = 0.9f,
                        MaxInstances = 3,
                    });
                }
            }
            base.AI();
        }

        public override void PostAI()
        {
            if (Main.rand.NextBool())
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Electric);
                dust.noGravity = true;
                dust.scale = .6f;
            }
        }
    }

}
