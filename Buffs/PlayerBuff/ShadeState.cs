using ShardsOfAtheria.Projectiles.Ranged;
using ShardsOfAtheria.Utilities;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerBuff
{
    public class ShadeState : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            var areusPlayer = player.Areus();
            areusPlayer.areusDamage += 0.15f;
            player.GetDamage(areusPlayer.classChip) += 0.15f;
            player.GetCritChance(areusPlayer.classChip) += 0.15f;
            player.GetAttackSpeed(areusPlayer.classChip) += 0.15f;
        }
    }

    public class ShadeProjectile : GlobalProjectile
    {
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            var player = projectile.GetPlayerOwner();
            if (player.Areus().royalSet && player.Areus().classChip == DamageClass.Ranged)
            {
                if (player.HasBuff<ShadeState>())
                {
                    if (MarksmanProjectile.ConvertableProjectiles.Contains(projectile.type))
                    {
                        Projectile.NewProjectile(source, projectile.Center, projectile.velocity,
                            ModContent.ProjectileType<ElectricShadeShot>(), projectile.damage,
                            projectile.knockBack, projectile.owner);
                        projectile.active = false;
                    }
                }
            }
        }
    }
}
