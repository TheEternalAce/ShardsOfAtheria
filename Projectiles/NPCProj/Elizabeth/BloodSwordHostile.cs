using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Elizabeth
{
    public class BloodSwordHostile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 3;
            SoAGlobalProjectile.Eraser.Add(Type);
            Projectile.AddElement(1);
            Projectile.AddElement(3);
            Projectile.AddRedemptionElement(12);
        }

        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 100;

            Projectile.aiStyle = -1;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 180;

            DrawOriginOffsetY = -50;
        }

        bool swordSwing = false;

        public override void AI()
        {
            var npc = Projectile.GetNPCOwner(1);
            var target = Main.player[(int)Projectile.ai[0]];
            Projectile.velocity = target.Center - npc.Center;
            Projectile.velocity.Normalize();

            Projectile.Center = npc.Center + Projectile.velocity * 50;
            Projectile.rotation = Projectile.velocity.ToRotation();
            Projectile.spriteDirection = Projectile.direction = -(Projectile.velocity.X > 0).ToDirectionInt();
            if (Projectile.spriteDirection == 1)
            {
                DrawOffsetX = 0;
                DrawOriginOffsetX = -50;
                Projectile.rotation += MathHelper.Pi;
            }
            else
            {
                DrawOffsetX = -100;
                DrawOriginOffsetX = 50;
            }

            if (Projectile.Hitbox.Intersects(target.Hitbox) && !swordSwing)
            {
                swordSwing = true;
            }

            if (swordSwing)
            {
                int frameTime = 10;
                if (++Projectile.frameCounter >= frameTime)
                {
                    if (++Projectile.frame >= 3)
                    {
                        Projectile.Kill();
                    }
                    else if (Projectile.frame == 1)
                    {
                        SoundEngine.PlaySound(SoundID.Item71.WithPitchOffset(1f), Projectile.Center);
                    }
                    Projectile.frameCounter = 0;
                }
            }
        }

        public override bool? CanDamage()
        {
            return Projectile.frame == 1;
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (SoA.Eternity())
            {
                info.DamageSource = PlayerDeathReason.ByCustomReason(target.name + " had their soul reaped.");
                target.KillMe(info.DamageSource, info.Damage, info.HitDirection);
            }
            target.AddBuff<DeathBleed>(1200);
        }
    }
}