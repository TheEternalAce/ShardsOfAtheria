using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee.BreakerBlade
{
    public class ElecSlash : ModProjectile
    {
        public override string Texture => SoA.SwordSlashTexture;

        public override void SetDefaults()
        {
            Projectile.width = 200;
            Projectile.height = 200;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.localNPCHitCooldown = 30;
            Projectile.usesLocalNPCImmunity = true;
        }

        private float Angle;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            int duration = 60; // Define the duration the projectile will exist in frames

            // Reset projectile time left if necessary
            if (Projectile.timeLeft > duration)
            {
                Projectile.timeLeft = duration;
            }
            Angle += 0.007f * 7;

            // ONLY CHANGE THE 35, ANYTHING ELSE WILL BREAK THE ROTATION
            Projectile.Center = player.MountedCenter + Vector2.One.RotatedBy(player.direction * Angle + (player.direction == 1 ? MathHelper.ToRadians(140f) : MathHelper.ToRadians(-50f))) * 35f;

            Vector2 toPlayer = player.Center - Projectile.Center;
            Projectile.rotation = toPlayer.ToRotation() - MathHelper.ToRadians(90f);

            Projectile.scale += 0.01f;

            if (Projectile.timeLeft < duration * 0.3)
                Projectile.alpha += 8;

            if (player.dead || player.ItemAnimationActive && player.HeldItem.type != ItemID.BreakerBlade
                && player.ownedProjectileCounts[Projectile.type] == 1)
            {
                Projectile.Kill();
                return;
            }
            player.heldProj = Projectile.whoAmI;
            Projectile.direction = player.direction;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (target.life == target.lifeMax)
            {
                modifiers.ScalingBonusDamage += 1.5f;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Electrified, 600);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture2D13 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
            int num156 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]; //ypos of lower right corner of sprite to draw
            int y3 = num156 * Projectile.frame; //ypos of upper left corner of sprite to draw
            Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;
            var opacity = Projectile.Opacity;

            Color color26 = Color.Cyan * opacity;

            SpriteEffects spriteEffects = Projectile.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color26,
                Projectile.rotation, origin2, Projectile.scale, spriteEffects, 0);
            Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color26,
                Projectile.rotation, origin2, Projectile.scale * 1.25f, spriteEffects, 0);
            return false;
        }
    }
}