using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
//using WebCom.Effects.ScreenShaking;

namespace ShardsOfAtheria.Projectiles.Other
{
    public class DiamondBarrier : ModProjectile
    {
        private float rotation;

        public override string Texture => "Terraria/Images/Item_" + ItemID.LargeDiamond;

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;

            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 10;

            DrawOriginOffsetY = -5;
        }

        public override void AI()
        {
            var player = Projectile.GetPlayerOwner();
            if (!CheckActive(player))
            {
                Projectile.Kill();
                return;
            }
            float toRotate = rotation + MathHelper.Pi * Projectile.ai[0];
            Projectile.Center = player.Center + Vector2.One.RotatedBy(toRotate) * 45f;
            rotation += MathHelper.ToRadians(2.5f);
            Projectile.rotation = (Projectile.Center - player.Center).ToRotation() + MathHelper.PiOver2;

            BlockHostileProjectiles();
        }

        public void BlockHostileProjectiles()
        {
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile reflProjectile = Main.projectile[i];
                if (Projectile.Hitbox.Intersects(reflProjectile.Hitbox))
                {
                    if (SoAGlobalProjectile.ReflectAiList.Contains(reflProjectile.aiStyle))
                    {
                        if (reflProjectile.active && reflProjectile.hostile &&
                            reflProjectile.velocity != Vector2.Zero &&
                            !Main.projPet[reflProjectile.type])
                        {
                            reflProjectile.Kill();
                            for (int j = 0; j < 3; j++)
                            {
                                Dust.NewDust(reflProjectile.position, reflProjectile.width,
                                    reflProjectile.height, DustID.GemDiamond);
                            }
                        }
                    }
                }
            }
        }

        bool CheckActive(Player player)
        {
            if (player.dead || !player.active || !player.Gem().greaterDiamondCore)
                return false;
            else Projectile.timeLeft = 2;
            return true;
        }

        public override bool? CanCutTiles()
        {
            return false;
        }
    }
}