using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee.AreusDaggerProjs
{
    public class AreusDaggerCurrent : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 4;
            Projectile.timeLeft = 400;
            Projectile.extraUpdates = 22;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.alpha = 255;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 140;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<ElectricShock>(), 600);
        }

        public override void AI()
        {
            Projectile.velocity.Normalize();
            Projectile.velocity *= 4f;

            int daggerIndex = (int)Projectile.ai[0];
            Projectile dagger = Main.projectile[daggerIndex];

            Projectile.Track(dagger.Center);

            if (Projectile.Hitbox.Intersects(dagger.Hitbox))
            {
                Projectile.Kill();
                //if (!Projectile.GetPlayerOwner().Shards().Overdrive)
                //{
                dagger.Kill();
                //}
            }

            Dust d = Dust.NewDustDirect(Projectile.Center, 0, 0, DustID.Electric);
            d.velocity *= 0;
            d.fadeIn = 1.3f;
            d.noGravity = true;
        }

        public override void OnKill(int timeLeft)
        {
            int daggerIndex = AreusDagger.FindOldestDagger();
            if (daggerIndex != -1)
            {
                var dagger = Main.projectile[daggerIndex];

                var daggerCenter = dagger.Center;
                var vector = Vector2.Normalize(daggerCenter - Projectile.Center) * 16;

                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, vector,
                    ModContent.ProjectileType<AreusDaggerCurrent>(), Projectile.damage, Projectile.knockBack,
                    Projectile.owner, daggerIndex);
            }

            for (var i = 0; i < 28; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Electric, speed * 2.4f);
                d.fadeIn = 1.3f;
                d.noGravity = true;
            }
        }
    }
}
