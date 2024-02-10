using Microsoft.Xna.Framework;
using ShardsOfAtheria.Dusts;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Nova
{
    public class HardlightKnifeHostile : ModProjectile
    {
        public override string Texture => "ShardsOfAtheria/Projectiles/Ranged/HardlightKnifeProj";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Projectile.width = 10; // The width of projectile hitbox
            Projectile.height = 10; // The height of projectile hitbox
            Projectile.aiStyle = -1; // The ai style of the projectile, please reference the source code of Terraria
            Projectile.hostile = true; // Can the projectile deal damage to enemies?
            Projectile.timeLeft = 600; // The live time for the projectile (60 = 1 second, so 600 is 10 seconds)
            Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
            Projectile.extraUpdates = 1; // Set to above 0 if you want the projectile to update multiple time in a frame
            Projectile.penetrate = 3;
            Projectile.tileCollide = false;
            DrawOffsetX = -2;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (Projectile.penetrate > 1)
            {
                Vector2 position = target.Center + Vector2.One.RotatedByRandom(360) * 120;
                Projectile.position = position;
                for (int i = 0; i < 2; i++)
                {
                    Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<HardlightDust_Blue>());
                    dust.noGravity = true;
                    Dust dust2 = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<HardlightDust_Pink>());
                    dust2.noGravity = true;
                }
                Projectile.velocity = Vector2.Normalize(target.Center - position) * 16f;
                Projectile.netUpdate = true;
                Projectile.tileCollide = false;
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 2; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<HardlightDust_Blue>(), Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<HardlightDust_Pink>(), Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.DrawProjectilePrims(SoA.HardlightColor * 0.7f, ShardsHelpers.DiamondX1);
            lightColor = Color.White;
            return true;
        }
    }
}
