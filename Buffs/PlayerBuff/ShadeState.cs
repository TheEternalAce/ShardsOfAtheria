using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Ranged;
using ShardsOfAtheria.Projectiles.Summon;
using ShardsOfAtheria.Utilities;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
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
            if (player.Areus().imperialSet && player.Areus().RangerSet)
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
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (ProjectileID.Sets.IsAWhip[projectile.type])
            {
                var player = projectile.GetPlayerOwner();
                var areus = player.Areus();

                if (areus.imperialSet && areus.CommanderSet && player.HasBuff<ShadeState>())
                {
                    var source = projectile.GetSource_FromThis();
                    var position = target.Center;
                    var velocity = Vector2.One.RotatedByRandom(MathHelper.TwoPi);
                    velocity *= 12f - Main.rand.NextFloat(6f);

                    int type = ModContent.ProjectileType<VoidThorn>();
                    int damage = projectile.damage;
                    float knockback = projectile.knockBack;

                    for (int i = 0; i < 4; i++)
                    {
                        velocity = velocity.RotatedByRandom(MathHelper.TwoPi);
                        Projectile.NewProjectile(source, position, velocity, type, damage, knockback, -1, target.whoAmI);
                    }
                }
            }
        }
    }
}
