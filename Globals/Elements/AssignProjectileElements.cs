using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Globals.Elements
{
    public class AssignProjectileElements : GlobalProjectile
    {
        List<int> FireProj = SoAGlobalProjectile.FireProj;
        List<int> MetalProj = SoAGlobalProjectile.MetalProj;

        public override bool InstancePerEntity => true;

        public override void SetDefaults(Projectile proj)
        {
            int type = proj.type;
            switch (type)
            {
                case ProjectileID.Spark:
                    FireProj.Add(type);
                    break;
                case ProjectileID.WoodenArrowFriendly:
                case ProjectileID.Bullet:
                case ProjectileID.BulletHighVelocity:
                    MetalProj.Add(type);
                    break;
            }
        }
    }
}
