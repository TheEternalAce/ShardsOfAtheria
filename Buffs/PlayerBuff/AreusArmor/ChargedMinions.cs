using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerBuff.AreusArmor
{
    public class ChargedMinions : ModBuff
    {
        public override string Texture => SoA.BuffTemplate;

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetAttackSpeed(DamageClass.SummonMeleeSpeed) += 0.15f;
        }
    }

    public class ChargedMinion : GlobalProjectile
    {
        int sparkTimer;
        const int SPARK_TIMER_MAX = 60;

        public override bool InstancePerEntity => true;

        public override void AI(Projectile projectile)
        {
            base.AI(projectile);
            if (projectile.minion)
            {
                if (projectile.damage > 0 || projectile.friendly)
                {
                    var player = projectile.GetPlayerOwner();
                    var shardsPlayer = player.Shards();
                    var areusPlayer = player.Areus();

                    if (player.HasBuff<ChargedMinions>())
                    {
                        if (shardsPlayer.InCombat)
                        {
                            if (areusPlayer.guardSet)
                            {
                                if (++sparkTimer >= SPARK_TIMER_MAX)
                                {
                                    int amount = 5;
                                    float rotation = MathHelper.ToRadians(360 / amount);
                                    for (int i = 0; i < amount; i++)
                                    {
                                        float speed = 6f * Main.rand.NextFloat(0.33f, 1f);
                                        Vector2 position = projectile.Center + Vector2.One.RotatedBy(rotation * i);
                                        Vector2 velocity = Vector2.Normalize(projectile.Center - position) * speed;
                                        Projectile.NewProjectile(projectile.GetSource_FromThis(), position, velocity, ModContent.ProjectileType<ElectricSpark>(),
                                            22, 0, player.whoAmI);
                                    }
                                    SoundEngine.PlaySound(SoundID.NPCHit53, projectile.Center);
                                    sparkTimer = 0;
                                }
                            }
                        }
                    }
                    else sparkTimer = 0;
                }
            }
        }
    }
}
