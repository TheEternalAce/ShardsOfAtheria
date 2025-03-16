using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Weapons.Magic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic.Sigil
{
    public class LightningSigil : ModProjectile
    {
        float ShootTimer
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }
        const float SHOOT_TIMER_MAX = 60;

        public bool BeingHeld => Main.player[Projectile.owner].channel && !Main.player[Projectile.owner].noItems && !Main.player[Projectile.owner].CCed;

        public override string Texture => ModContent.GetInstance<AreusSigil>().Texture;

        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(36);
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.scale = 1f;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ownerHitCheck = true;
            Projectile.extraUpdates = 1;
            Projectile.hide = true;
        }

        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];
            player.heldProj = Projectile.whoAmI;
            player.itemAnimation = 2;
            player.itemTime = 2;

            return base.PreAI();
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.netUpdate = true;

            if (BeingHeld)
            {
                Projectile.timeLeft = 10;
                player.manaRegenDelay = 120;
                player.manaRegen = 0;

                player.heldProj = Projectile.whoAmI;
                if (player.statMana >= 24)
                {
                    if (++ShootTimer >= SHOOT_TIMER_MAX)
                    {
                        ShootTimer = 0;
                        var source = Projectile.GetSource_FromThis();
                        var position = Projectile.Center;
                        var velocity = Main.MouseWorld - Projectile.Center;
                        velocity.Normalize();
                        int type = ModContent.ProjectileType<SigilLightning>();
                        int damage = Projectile.damage;
                        float knockback = Projectile.knockBack;
                        Projectile.NewProjectile(source, position, velocity, type, damage, knockback);
                        for (int i = 0; i < 4; i++)
                        {
                            velocity = velocity.RotatedByRandom(MathHelper.TwoPi);
                            Projectile.NewProjectile(source, position, velocity, type, damage, knockback);
                        }
                        SoundEngine.PlaySound(SoundID.Thunder, Projectile.Center);
                        player.statMana -= 24;
                    }
                }
            }

            int direction = Main.MouseWorld.X > player.Center.X ? 1 : -1;
            player.ChangeDir(direction);
            Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: false, addGfxOffY: false);
            Projectile.Center = playerCenter + Projectile.velocity * player.direction;
        }

        public override bool ShouldUpdatePosition()
        {
            return false;
        }
    }
}
