using Microsoft.Xna.Framework;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic
{
    public class VendettaBeam : ModProjectile
    {
        CardinalSoulPlayer Sinner => Projectile.GetPlayerOwner().CardinalSoul();

        public override string Texture => SoA.BlankTexture;

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;

            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 4;
            Projectile.light = 1;
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void AI()
        {
            Projectile.velocity.Normalize();
            Projectile.velocity *= 4f;

            if (++Projectile.ai[0] >= 20)
            {
                if (Projectile.ai[1] == 0) Projectile.ai[1] = MathHelper.PiOver2;
                Projectile.ai[1] += MathHelper.ToRadians(5f);
                Projectile.SetVisualOffsets(52);
                Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;

                Vector2 offset = Vector2.Normalize(Projectile.velocity).RotatedBy(MathHelper.PiOver2 + Projectile.ai[1]) * 4f;
                Dust dust = Dust.NewDustPerfect(Projectile.Center + offset, DustID.ShadowbeamStaff);
                dust.velocity *= 0f;
                dust.noGravity = true;

                dust = Dust.NewDustPerfect(Projectile.Center - offset, DustID.ShadowbeamStaff);
                dust.velocity *= 0f;
                dust.noGravity = true;
            }
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            int quarrel = Sinner.EnvyQuarrel;
            if (Sinner.envyQuarrelTarget == target.whoAmI && quarrel > 0)
                modifiers.FlatBonusDamage += quarrel * 5;
            else modifiers.FinalDamage /= 2;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Sinner.envyQuarrelTarget >= 0 && Sinner.envyQuarrelTarget != target.whoAmI)
                Sinner.EnvyQuarrel = 0;
            Sinner.envyQuarrelTarget = target.whoAmI;
            Sinner.EnvyQuarrel++;
            if (Sinner.EnvyQuarrel >= 10)
            {
                IEntitySource source = target.GetSource_OnHurt(Projectile);
                Vector2 position = target.Center;
                int type = ModContent.ProjectileType<VendettaTendril>();
                int damage = Projectile.damage * 2;
                float knockback = 0f;

                SoundEngine.PlaySound(SoundID.Item103, Projectile.Center);

                for (int i = 0; i < 10; i++)
                {
                    Vector2 velocity = Vector2.One.RotatedByRandom(MathHelper.TwoPi) * 8f * (1f - Main.rand.NextFloat(0.33f));
                    float ai0 = Main.rand.Next(10, 80) * 0.00075f;
                    if (Main.rand.NextBool(2)) ai0 *= -1f;
                    float ai1 = Main.rand.Next(10, 80) * 0.00075f;
                    if (Main.rand.NextBool(2)) ai1 *= -1f;
                    Projectile.NewProjectile(source, position, velocity * (2 - Main.rand.NextFloat(0.33f)),
                        type, damage, knockback, Projectile.owner, ai0, ai1);
                }
                Sinner.EnvyQuarrel = 0;
            }
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.ShadowbeamStaff);
            }
        }
    }
}
