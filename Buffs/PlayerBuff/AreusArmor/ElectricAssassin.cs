using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Other;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerBuff.AreusArmor
{
    public class ElectricAssassin : ModBuff
    {
        public override string Texture => SoA.BuffTemplate;

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetAttackSpeed(DamageClass.SummonMeleeSpeed) += 0.15f;
        }
    }

    public class AssassinProjectile : GlobalProjectile
    {
        bool sparked = false;

        public override bool InstancePerEntity => true;

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!sparked)
            {
                var player = Main.player[projectile.owner];
                if (player.HasBuff<ElectricAssassin>() && projectile.DamageType == DamageClass.Throwing)
                {
                    int amount = 5;
                    float rotation = MathHelper.ToRadians(360 / amount);
                    for (int i = 0; i < amount; i++)
                    {
                        float speed = 6f * Main.rand.NextFloat(0.33f, 1f);
                        Vector2 position = target.Center + Vector2.One.RotatedBy(rotation * i);
                        Vector2 velocity = Vector2.Normalize(target.Center - position) * speed;
                        Projectile.NewProjectile(projectile.GetSource_FromThis(), position, velocity, ModContent.ProjectileType<ElectricSpark>(),
                            22, 0, player.whoAmI);
                    }
                    SoundEngine.PlaySound(SoundID.NPCHit53, projectile.Center);
                    sparked = true;
                }
            }
        }
    }
}
