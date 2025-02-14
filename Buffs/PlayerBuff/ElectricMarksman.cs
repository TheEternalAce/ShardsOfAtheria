using ShardsOfAtheria.Projectiles.Ammo;
using ShardsOfAtheria.Projectiles.Ranged;
using ShardsOfAtheria.Utilities;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerBuff
{
    public class ElectricMarksman : ModBuff
    {
        public override string Texture => SoA.BuffTemplate;

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetAttackSpeed(DamageClass.Ranged) += 0.15f;
        }
    }

    public class MarksmanProjectile : GlobalProjectile
    {
        public static readonly int[] ConvertableProjectiles = new int[]
        {
            ProjectileID.WoodenArrowFriendly,
            ProjectileID.FlamingArrow,
            ProjectileID.FrostburnArrow,
            ProjectileID.UnholyArrow,

            ProjectileID.Bullet,
            ProjectileID.SilverBullet,
            ProjectileID.MeteorShot,
            ModContent.ProjectileType<TungstenBullet>(),
        };

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            var player = projectile.GetPlayerOwner();
            if (player.Areus().guardSet && player.Areus().RangerSetChip)
            {
                if (player.HasBuff<ElectricMarksman>())
                {
                    if (ConvertableProjectiles.Contains(projectile.type))
                    {
                        Projectile.NewProjectile(source, projectile.Center, projectile.velocity,
                            ModContent.ProjectileType<ElectricMarksmanShot>(), projectile.damage,
                            projectile.knockBack, projectile.owner);
                        projectile.active = false;
                    }
                }
            }
        }
    }
}
