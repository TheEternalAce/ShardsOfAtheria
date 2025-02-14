using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Summon.Minions.GemCore;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic.MagicalGems
{
    public class AmberFly2 : AmberFly
    {
        public override bool PowerBoosted => false;

        public override string Texture => ModContent.GetInstance<AmberFly>().Texture;

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.DamageType = DamageClass.Magic;
            Projectile.minion = false;
            Projectile.timeLeft = 120;
            Projectile.tileCollide = true;
            Projectile.penetrate = 3;
        }

        public override bool CheckActive(Player owner)
        {
            return true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
    }
}
