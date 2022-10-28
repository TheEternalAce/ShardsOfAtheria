using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Projectiles.NPCProj;
using ShardsOfAtheria.Projectiles.Weapon.Areus;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Other
{
    public class ArrowSpliter : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;

            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            if (Main.myPlayer == Projectile.owner)
            {
                Projectile.timeLeft = 2;

                if (++Projectile.ai[0] == 10)
                {
                    Player player = Main.player[Projectile.owner];
                    if (player.ownedProjectileCounts[ModContent.ProjectileType<AreusArrow>()] == 0)
                    {
                        Projectile.Kill();
                    }
                }
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile proj = Main.projectile[i];
                    if (proj.type == ModContent.ProjectileType<AreusArrow>())
                    {
                        if (Projectile.Hitbox.Intersects(proj.getRect()) && proj.ai[0] == 1 && proj.active)
                        {
                            Projectile.Kill();
                            proj.Kill();
                            Projectile.netUpdate = true;
                        }
                    }
                }
            }

            base.AI();
        }
    }
}
