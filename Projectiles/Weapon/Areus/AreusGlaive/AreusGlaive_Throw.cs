using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Weapon.Magic;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Areus.AreusGlaive
{
    public class AreusGlaive_Throw : ModProjectile
    {
        public int airTime = 0;
        public int airTimeMax = 15;

        public override string Texture => "ShardsOfAtheria/Projectiles/Weapon/Areus/AreusGlaive/AreusGlaive_Thrust";

        public override void SetStaticDefaults()
        {
            SoAGlobalProjectile.AreusProj.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 84;
            Projectile.height = 84;
            Projectile.scale = 1.3f;

            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;

            SoAGlobalProjectile.AreusProj.Add(Type);
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            player.itemAnimation = 10;
            player.itemTime = 10;

            int newDirection = Projectile.Center.X > player.Center.X ? 1 : -1;
            player.ChangeDir(newDirection);
            Projectile.direction = newDirection;

            Projectile.rotation += 0.6f * Projectile.direction;

            Projectile.ai[1]++;
            if (Projectile.ai[1] == 10)
            {
                SoundEngine.PlaySound(SoundID.Item71);
                Projectile.ai[1] = 0;
                if (Projectile.GetGlobalProjectile<OverchargedProjectile>().overcharged)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Projectile proj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Normalize(Projectile.position + Projectile.velocity - Projectile.Center)
                            .RotatedBy(MathHelper.ToRadians(60 * i)) * 5, ModContent.ProjectileType<LightningBoltFriendly>(), Projectile.damage, Projectile.knockBack, player.whoAmI);
                        proj.DamageType = DamageClass.Melee;
                    }
                }
            }

            Projectile.ai[0]++;
            if (Projectile.ai[0] >= 15)
            {
                Projectile.velocity = Vector2.Normalize(player.Center - Projectile.Center) * 30;
            }

            if (Projectile.getRect().Intersects(player.getRect()) && Projectile.ai[0] >= 15)
                Projectile.Kill();
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            var player = Main.player[Projectile.owner];

            Vector2 mountedCenter = player.MountedCenter;

            var drawPosition = Main.MouseWorld;
            var remainingVectorToPlayer = mountedCenter - drawPosition;

            if (Projectile.alpha == 0)
            {
                int direction = -1;

                if (Main.MouseWorld.X < mountedCenter.X)
                    direction = 1;

                player.itemRotation = (float)Math.Atan2(remainingVectorToPlayer.Y * direction, remainingVectorToPlayer.X * direction);
            }
            lightColor = Color.White;
            return true;
        }
    }
}
