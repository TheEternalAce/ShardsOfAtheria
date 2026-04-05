using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.Summons;
using ShardsOfAtheria.Projectiles.Summon.Minions;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Summon.Active
{
    public class FrostsparkBeam : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(2, 5);
            Projectile.AddElement(1);
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(4);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;

            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 4;
            Projectile.light = 1;
            Projectile.DamageType = DamageClass.Summon;
            //Projectile.alpha = 255;
        }

        public override void OnSpawn(IEntitySource source)
        {
            SoundEngine.PlaySound(SoundID.Item28, Projectile.Center);
            SoundEngine.PlaySound(SoundID.Item72, Projectile.Center);
            Projectile.rotation = MathHelper.PiOver2;
            if (Projectile.ai[0] == 0) Projectile.alpha = 255;
        }

        public override void AI()
        {
            Projectile.velocity.Normalize();
            Projectile.velocity *= 4f;

            if (Projectile.alpha > 0) Projectile.alpha -= 15;
            else if (Projectile.alpha < 0) Projectile.alpha = 0;
            else
            {
                Projectile.rotation += MathHelper.ToRadians(5f);

                Vector2 offset = Vector2.Normalize(Projectile.velocity).RotatedBy(Projectile.rotation) * 4f;
                Dust dust = Dust.NewDustPerfect(Projectile.Center + offset, DustID.Ice);
                dust.velocity *= 0f;
                dust.noGravity = true;

                dust = Dust.NewDustPerfect(Projectile.Center - offset, DustID.Electric, Scale: 0.5f);
                dust.velocity *= 0f;
                dust.noGravity = true;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn2, 600);
            if (Projectile.ai[0] == 0)
            {
                var player = Main.player[Projectile.owner];
                player.MinionAttackTargetNPC = target.whoAmI;

                player.AddBuff<FrostsparkDroneBuff>(300);
                if (player.ownedProjectileCounts[ModContent.ProjectileType<FrostsparkDrone>()] == 0)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero,
                            ModContent.ProjectileType<FrostsparkDrone>(), Projectile.damage, Projectile.knockBack, player.whoAmI, i);
                    }
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Ice);
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Electric);
            }
            SoundEngine.PlaySound(SoundID.Item27, Projectile.position);
        }
    }
}
