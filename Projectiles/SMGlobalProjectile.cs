using SagesMania.NPCs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Projectiles
{
    class SMGlobalProjectile : GlobalProjectile
    {
        public override void OnHitPlayer(Projectile projectile, Player target, int damage, bool crit)
        {
            if (projectile.type == ProjectileID.HarpyFeather)
                target.AddBuff(BuffID.Electrified, 10 * 60);
        }
    }
}
