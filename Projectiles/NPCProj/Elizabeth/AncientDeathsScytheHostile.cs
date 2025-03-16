using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Items.PetItems;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Elizabeth
{
    public class AncientDeathsScytheHostile : ModProjectile
    {
        public override string Texture => ModContent.GetInstance<AncientDeathsScythe>().Texture;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            Projectile.AddDamageType(6);
            Projectile.AddElement(1);
            Projectile.AddElement(3);
            Projectile.AddRedemptionElement(1);
            Projectile.AddRedemptionElement(12);
        }

        public override void SetDefaults()
        {
            Projectile.width = 58;
            Projectile.height = 60;

            Projectile.timeLeft = 1800;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 1;
        }

        public override void OnSpawn(IEntitySource source)
        {
            SoundEngine.PlaySound(SoundID.Item71.WithPitchOffset(-1f), Projectile.Center);
        }

        public override void AI()
        {
            Player player = Projectile.FindClosestPlayer(2000);
            if (player != null)
            {
                Vector2 move = player.Center - Projectile.Center;
                move = move.RotatedByRandom(MathHelper.PiOver4);
                float speed = 8f;
                ShardsHelpers.AdjustMagnitude(ref move, speed);
                Projectile.velocity = 30 * Projectile.velocity + move;
                ShardsHelpers.AdjustMagnitude(ref Projectile.velocity, speed);
            }
            Projectile.rotation += MathHelper.ToRadians(5f) * (Projectile.velocity.X > 0).ToDirectionInt();
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff<DeathBleed>(300);
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Shadowflame, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            Projectile.DrawAfterImage(lightColor);
            return base.PreDraw(ref lightColor);
        }
    }
}