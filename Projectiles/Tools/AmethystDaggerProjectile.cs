using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.PlayerDebuff.GemCurse;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
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
            Projectile.timeLeft = 15;
            Projectile.hostile = true;
        }

        public override void AI()
        {
            Player player = Projectile.GetPlayerOwner();

            int newDirection = Projectile.Center.X > player.Center.X ? 1 : -1;
            Projectile.direction = newDirection;
            Projectile.spriteDirection = -newDirection;

            Projectile.Center = player.Center + -Projectile.velocity * timer;
            timer--;

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;
            if (Projectile.direction == 1) Projectile.rotation += MathHelper.PiOver2;
            Projectile.SetVisualOffsets(46);
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }

        public override bool CanHitPlayer(Player target)
        {
            return target.whoAmI == Projectile.owner && !target.immune && !target.creativeGodMode;
        }

        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            // Cut damage in half since hostile projectile damage is doubled for some reason.
            modifiers.IncomingDamageMultiplier *= 0.5f;
            var gem = target.Gem();
            // Reduce damage with gem cores.
            if (gem.amethystCore)
                modifiers.IncomingDamageMultiplier *= 0.8f;
            if (gem.greaterAmethystCore)
                modifiers.IncomingDamageMultiplier *= 0.8f;
            if (gem.superAmethystCore)
                modifiers.IncomingDamageMultiplier *= 0.8f;
            modifiers.ScalingArmorPenetration += 1f;
            modifiers.Knockback *= 0f;
            modifiers = modifiers with { Dodgeable = false };
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff<WoundedAmethyst>(60);
            Projectile.Kill();
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
