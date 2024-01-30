using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.PlayerDebuff.GemCurse;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Tools
{
    public class AmethystDaggerProjectile : ModProjectile
    {
        int timer = 6;

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 60;
            Projectile.hostile = true;
        }

        public override void AI()
        {
            Player player = Projectile.GetPlayerOwner();

            int newDirection = Projectile.Center.X > player.Center.X ? 1 : -1;
            player.ChangeDir(newDirection);
            Projectile.direction = newDirection;
            Projectile.spriteDirection = -newDirection;

            Projectile.Center = player.Center + -Projectile.velocity * timer;
            timer--;

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;
            if (Projectile.direction == 1)
            {
                Projectile.rotation += MathHelper.PiOver2;
            }
            Projectile.SetVisualOffsets(46);

            if (Projectile.Colliding(Projectile.Hitbox, player.Hitbox))
            {
                int damage = player.statLifeMax2 / 10;
                var gem = player.Gem();
                if (gem.amethystCore)
                {
                    damage = (int)(damage * 0.8f);
                }
                if (gem.greaterAmethystCore)
                {
                    damage = (int)(damage * 0.8f);
                }
                if (gem.superAmethystCore)
                {
                    damage = (int)(damage * 0.8f);
                }
                Player.HurtInfo info = new()
                {
                    Damage = damage,
                    Knockback = 0f,
                    Dodgeable = false,
                    DamageSource = PlayerDeathReason.ByProjectile(player.whoAmI, Projectile.whoAmI)
                };
                player.Hurt(info);
                player.AddBuff<AmethystCurse>(60);
                Projectile.Kill();
            }
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override bool CanHitPlayer(Player target)
        {
            return target.whoAmI == Projectile.owner && !target.immune && !target.creativeGodMode;
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Tink, Projectile.Center);
            var vector = -Projectile.velocity;
            vector.Normalize();
            vector *= 3f;
            for (int i = 0; i < 6; i++)
            {
                var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GemAmethyst,
                    vector.X, vector.Y);
                dust.noGravity = true;
            }
        }
    }
}
