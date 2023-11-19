using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace ShardsOfAtheria.Projectiles.Melee.OmegaSword
{
    public class MessiahRanbu : ModProjectile
    {
        public int combo;
        public bool BeingHeld => Main.player[Projectile.owner].channel && !Main.player[Projectile.owner].noItems && !Main.player[Projectile.owner].CCed;

        public override void SetStaticDefaults()
        {
            Projectile.AddElementFire();
            Main.projFrames[Type] = 20;
            SoAGlobalProjectile.Eraser.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 384;
            Projectile.height = 384;
            Projectile.aiStyle = 75;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 12;
            Projectile.timeLeft = 30;

            DrawOffsetX = -160;
            DrawOriginOffsetX = 80;
        }

        // Ranbu pattern: 1, 2, 3, 2, 4, 3, 5
        bool launchPlayer = false;

        public override void AI()
        {
            UpdateVisuals();

            Player player = Main.player[Projectile.owner];
            if (combo == 7 && !launchPlayer)
            {
                launchPlayer = true;
                player.velocity += new Vector2(1 * player.direction, -2) * 6f;
            }
            var vector = Vector2.Zero;
            Projectile.velocity = vector;
            if (player.IsLocal())
            {
                vector = Main.MouseWorld - player.Center;
                vector.Normalize();
                vector *= 140f;
                Projectile.velocity = vector;
                if (Main.MouseWorld.X > player.Center.X)
                {
                    Projectile.direction = 1;
                }
                else
                {
                    Projectile.direction = -1;
                }
            }
            Projectile.spriteDirection = Projectile.direction;

            float angle = 0;
            if (Projectile.direction == 1)
            {
                DrawOffsetX = -160;
                DrawOriginOffsetX = 80;
            }
            else
            {
                DrawOffsetX = 0;
                DrawOriginOffsetX = -80;
                angle = MathHelper.Pi;
            }
            Projectile.rotation = vector.ToRotation() + angle;
            Projectile.netUpdate = true;
        }

        public void UpdateVisuals()
        {
            if (Projectile.frame == 0 && Projectile.frameCounter == 0)
            {
                Spark();
            }

            if (Projectile.frameCounter == 1)
            {
                bool slashEndFrame = (Projectile.frame + 1) % 4 == 0;
                if (slashEndFrame)
                {
                    if (!BeingHeld)
                    {
                        Projectile.Kill();
                    }
                    else
                    {
                        SoundEngine.PlaySound(SoundID.Item1);
                        Spark();
                    }
                }
            }
            if (++Projectile.frameCounter == 3)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame >= Main.projFrames[Type])
                {
                    Projectile.Kill();
                }

                if ((Projectile.frame + 1) % 4 == 0)
                {
                    combo++;
                    switch (combo)
                    {
                        case 4:
                            //Projectile.frame = 3;
                            break;
                        case 5:
                            Projectile.frame = 11;
                            break;
                        case 6:
                            Projectile.frame = 7;
                            break;
                        case 7:
                            Projectile.frame = 15;
                            break;
                    }
                }
            }
        }

        void Spark()
        {
            var player = Main.player[Projectile.owner];
            float numberProjectiles = Main.rand.Next(3, 6);
            float rotation = MathHelper.ToRadians(15);
            var position = player.Center + Vector2.Normalize(Projectile.velocity) * 10f;
            var velocity = Projectile.velocity.RotatedBy(MathHelper.ToRadians(15 * (Main.MouseWorld.X > player.Center.X ? -1 : 1)));
            velocity.Normalize();
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), position, perturbedSpeed * 10f * Main.rand.NextFloat(0.5f, 1f),
                    ProjectileID.Spark, Projectile.damage, Projectile.knockBack, player.whoAmI);
            }
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (combo == 6)
            {
                modifiers.SetCrit();
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire3, 600);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hit)
        {
            target.AddBuff(BuffID.OnFire3, 600);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            return base.PreDraw(ref lightColor);
        }
    }
}