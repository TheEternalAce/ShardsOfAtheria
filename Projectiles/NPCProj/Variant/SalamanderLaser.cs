using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace ShardsOfAtheria.Projectiles.NPCProj.Variant
{
    public class SalamanderLaser : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;

            Projectile.AddDamageType(5);
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
            Projectile.AddRedemptionElement(15);
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;

            Projectile.timeLeft = 300;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.extraUpdates = 2;
            Projectile.ignoreWater = true;
        }

        public override void OnSpawn(IEntitySource source)
        {
            SoundEngine.PlaySound(SoundID.Item33, Projectile.Center);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Projectile.Kill();
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();
            if (SoA.Massochist())
            {
                var player = ShardsHelpers.FindClosestPlayer(Projectile.Center, 200);
                if (player != null)
                {
                    float speed = Projectile.velocity.Length();
                    var vectorToTarget = player.Center - Projectile.Center;
                    ShardsHelpers.AdjustMagnitude(ref vectorToTarget, speed);
                    Projectile.velocity = (3 * Projectile.velocity + vectorToTarget) / 2f;
                    ShardsHelpers.AdjustMagnitude(ref Projectile.velocity, speed);
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            if (SoA.Eternity() && Main.LocalPlayer.IsLocal())
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<ElectricExplosion_Hostile>(),
                    Projectile.damage, 0);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.DrawBloomTrail(Color.White.UseA(50), SoA.LineBloom);
            return base.PreDraw(ref lightColor);
        }
    }
}