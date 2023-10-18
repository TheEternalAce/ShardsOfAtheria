using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Elizabeth
{
    public class BloodSickleSummon : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;

            Projectile.timeLeft = 120;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.penetrate = 2;
        }

        int InitialDamage;
        Player Target;

        public override void OnSpawn(IEntitySource source)
        {
            InitialDamage = Projectile.damage;
            Projectile.damage = 0;
            Target = Main.player[(int)Projectile.ai[2]];
            SoundEngine.PlaySound(SoundID.Item17, Projectile.Center);
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, TorchID.Crimson);

            Dust d = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Blood, newColor: Color.White);
            d.velocity *= 0;
            d.fadeIn = 1.3f;
            d.noGravity = true;

            Dust d2 = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Blood, newColor: Color.White);
            d2.fadeIn = 1.3f;
            d2.noGravity = true;

            Vector2 movePos = new(Projectile.ai[0] + Target.Center.X, Projectile.ai[1] + Target.Center.Y);
            Projectile.Track(movePos, -1, 32, 8);
        }

        public override void OnKill(int timeLeft)
        {
            Vector2 vector = Target.Center - Projectile.Center;
            vector.Normalize();
            vector *= 4f * Main.rand.NextFloat(0.66f, 1f);
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center,
                vector, ModContent.ProjectileType<BloodSickleHostile>(), InitialDamage,
                0, Main.myPlayer);
        }
    }
}