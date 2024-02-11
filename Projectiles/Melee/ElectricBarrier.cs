using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.PlayerDebuff.Cooldowns;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Projectiles.Magic;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee
{
    public class ElectricBarrier : ModProjectile
    {
        public override string Texture => ModContent.GetInstance<ElectricWave>().Texture;

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 50;

            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 300;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (!CheckActive(player))
            {
                Projectile.Kill();
            }

            //Idle
            Projectile.Center = player.Center + new Vector2(1, 0).RotatedBy(Projectile.ai[0] * MathHelper.Pi) * 40f;
            Projectile.rotation = Vector2.Normalize(Projectile.Center - player.Center).ToRotation() + MathHelper.ToRadians(90f);

            int direction = player.Center.X < Projectile.Center.X ? 1 : -1;
            Projectile.spriteDirection = Projectile.direction = direction;
            if (Projectile.spriteDirection == 1)
            {
                DrawOffsetX = -26;
            }
            else
            {
                DrawOffsetX = 0;
            }

            Parry();
        }

        public void Parry()
        {
            Player player = Main.player[Projectile.owner];

            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile reflProjectile = Main.projectile[i];
                if (Projectile.Hitbox.Intersects(reflProjectile.getRect()))
                {
                    if (SoAGlobalProjectile.ReflectAiList.Contains(reflProjectile.aiStyle))
                    {
                        if (reflProjectile.active && reflProjectile.hostile &&
                            reflProjectile.velocity != Vector2.Zero)
                        {
                            if (Projectile.timeLeft > 270)
                            {
                                SoundEngine.PlaySound(SoundID.DD2_JavelinThrowersAttack, Projectile.Center);
                                SoundEngine.PlaySound(SoundID.DD2_DarkMageAttack, Projectile.Center);

                                Vector2 velocity = Main.MouseWorld - reflProjectile.position;
                                velocity.Normalize();
                                float speed = Math.Abs(reflProjectile.velocity.X) + Math.Abs(reflProjectile.velocity.Y);
                                velocity *= speed * 2;

                                reflProjectile.hostile = false;
                                reflProjectile.friendly = true;
                                reflProjectile.velocity = velocity;

                                player.AddBuff(BuffID.ParryDamageBuff, 600);
                                player.immune = true;
                                player.immuneTime = 60;
                            }
                            else
                            {
                                reflProjectile.Kill();
                            }
                            for (int j = 0; j < 3; j++)
                            {
                                Dust.NewDust(reflProjectile.position, reflProjectile.width,
                                    reflProjectile.height, DustID.Electric);
                            }
                        }
                    }
                }
            }

            if (Projectile.timeLeft > 270)
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC parryNPC = Main.npc[i];
                    if (Projectile.Hitbox.Intersects(parryNPC.getRect()) && !player.HasBuff(ModContent.BuffType<ParryCooldown>()) && parryNPC.CanBeChasedBy() && parryNPC.damage > 0)
                    {
                        player.immune = true;
                        player.immuneTime = 60;
                        player.AddBuff(ModContent.BuffType<ParryCooldown>(), 300);
                        player.AddBuff(BuffID.ParryDamageBuff, 300);
                    }
                }
            }
        }

        static bool CheckActive(Player player)
        {
            if (player == null || player.dead || !player.active ||
                !player.Areus().soldierSet || player.Areus().classChip != DamageClass.Melee)
            {
                return false;
            }
            return true;
        }

        public override bool? CanCutTiles()
        {
            return false;
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Electric, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = SoA.ElectricColorA;
            return base.PreDraw(ref lightColor);
        }
    }
}
