using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.NPCDebuff;
using ShardsOfAtheria.Gores;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Summon
{
    public class StrikerRod : ModProjectile
    {
        public override string Texture => "ShardsOfAtheria/Projectiles/Weapon/Ammo/AreusBulletProj";

        public override void SetStaticDefaults()
        {
            Projectile.AddAreus();
        }

        public override void SetDefaults()
        {
            Projectile.width = 6;
            Projectile.height = 6;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.timeLeft = 600;
            Projectile.light = 0.5f;
            Projectile.ignoreWater = true;
            Projectile.extraUpdates = 1;
            AIType = ProjectileID.Bullet;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            if (Main.rand.NextBool(20))
            {
                Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Electric, Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 200, Scale: 1f);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            var vector = Projectile.velocity;
            vector.Normalize();
            vector *= -3;
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.Center, vector,
                ModContent.GoreType<AreusRodGore>());

            var player = Main.player[Projectile.owner];
            player.MinionAttackTargetNPC = target.whoAmI;

            target.AddBuff<StrikerTag>(1200);
            var struck = target.GetGlobalNPC<StrikerNPC>();
            if (struck.tagAmount < 3)
            {
                SoundEngine.PlaySound(SoundID.MaxMana);
                struck.tagAmount++;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.NPCHit4, Projectile.position);
            var vector = oldVelocity;
            vector.Normalize();
            vector *= -3;
            Gore.NewGore(Projectile.GetSource_FromThis(), Projectile.Center, vector,
                ModContent.GoreType<AreusRodGore>());
            return base.OnTileCollide(oldVelocity);
        }
    }
}
