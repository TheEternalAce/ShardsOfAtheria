using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Bases;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Effects.ScreenShaking;
//using WebCom.Effects.ScreenShaking;

namespace ShardsOfAtheria.Projectiles.Melee
{
    public class Warframe_Upgrade : SwordProjectileBase
    {
        public static Asset<Texture2D> glowmask;

        public override void Unload()
        {
            glowmask = null;
        }

        public override void SetStaticDefaults()
        {
            if (!Main.dedServ)
            {
                glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
            }

            ProjectileID.Sets.TrailingMode[Type] = 3;
            ProjectileID.Sets.TrailCacheLength[Type] = 13;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Projectile.width = Projectile.height = 30;
            swordReach = 180;
            rotationOffset = -MathHelper.PiOver4 * 3f;
            amountAllowedToHit = 4;
        }

        protected override void Initialize(Player player, ShardsPlayer shards)
        {
            base.Initialize(player, shards);
            if (shards.itemCombo > 0)
            {
                swingDirection *= -1;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
            target.AddBuff(BuffID.Electrified, 600);
            ScreenShake.ShakeScreen(6, 60);
            var player = Main.player[Projectile.owner];
            var vector = player.Center - target.Center;
            vector.Normalize();
            player.velocity = vector * 8;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            base.AI();
            if (Main.player[Projectile.owner].itemAnimation <= 1)
            {
                Main.player[Projectile.owner].Shards().itemCombo = (ushort)(combo == 0 ? 20 : 0);
            }
            if (!playedSound && AnimProgress > 0.4f)
            {
                playedSound = true;
                SoundEngine.PlaySound(SoundID.Item1.WithPitchOffset(-1f), Projectile.Center);
            }
            if (Main.rand.NextBool(2))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Electric);
                dust.velocity = AngleVector * Projectile.velocity.Length() * 4;
            }
        }

        public override void UpdateSwing(float progress, float interpolatedSwingProgress)
        {
            FireProjectile(progress, ModContent.ProjectileType<WarframeSlash>(),
                (int)(Projectile.damage * 0.75), (int)(Projectile.knockBack * 0.75));
        }

        public override float SwingProgress(float progress)
        {
            return GenericSwing2(progress);
        }

        public override float GetVisualOuter(float progress, float swingProgress)
        {
            if (progress > 0.8f)
            {
                float p = 1f - (1f - progress) / 0.2f;
                Projectile.alpha = (int)(p * 255);
                return -20f * p;
            }
            if (progress < 0.35f)
            {
                float p = 1f - progress / 0.35f;
                Projectile.alpha = (int)(p * 255);
                return -20f * p;
            }
            return 0f;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            var handPosition = Main.GetPlayerArmPosition(Projectile) + AngleVector * visualOutwards;
            var drawColor = Projectile.GetAlpha(lightColor) * Projectile.Opacity;
            var origin = new Vector2(0f, glowmask.Value.Height);
            var effects = SpriteEffects.None;

            Main.EntitySpriteDraw(glowmask.Value, handPosition - Main.screenPosition, null, drawColor, Projectile.rotation, origin, Projectile.scale, effects, 0);

            DrawSwish();

            return GenericSwordDraw(lightColor);
        }
    }
}