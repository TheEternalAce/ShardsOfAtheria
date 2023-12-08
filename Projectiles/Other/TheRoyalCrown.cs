using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;
//using WebCom.Effects.ScreenShaking;

namespace ShardsOfAtheria.Projectiles.Other
{
    public class TheRoyalCrown : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 56;
            Projectile.height = 22;

            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            var player = Projectile.GetPlayerOwner();
            if (!CheckActive(player))
            {
                Projectile.Kill();
                return;
            }
            Projectile.Center = player.Center + new Vector2(0, -36);
            Projectile.spriteDirection = player.direction;
        }

        private bool CheckActive(Player player)
        {
            int headSlot = EquipLoader.GetEquipSlot(Mod, "RoyalCrown", EquipType.Head);
            if (player.dead || !player.active || !player.Areus().royalSet || player.head != headSlot)
                return false;
            else Projectile.timeLeft = 2;
            return true;
        }
    }
}