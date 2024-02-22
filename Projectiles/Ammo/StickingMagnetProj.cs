using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Items.Weapons.Ammo;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Ammo
{
    public class StickingMagnetProj : ModProjectile
    {
        public override string Texture => ModContent.GetInstance<StickingMagnet>().Texture;

        public override void SetStaticDefaults()
        {
            Projectile.AddRedemptionElement(5);
            Projectile.AddRedemptionElement(13);

            SoAGlobalProjectile.Metalic.Add(Type, 0f);
        }

        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.aiStyle = -1;
            AIType = ProjectileID.BoneJavelin;
            Projectile.ignoreWater = true;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 120;
            Projectile.usesLocalNPCImmunity = true;
        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            Projectile.hide = true;
            behindNPCsAndTiles.Add(index);
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
            {
                targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
            }
            return projHitbox.Intersects(targetHitbox);
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.Center);
            Vector2 usePos = Projectile.position;

            Vector2 rotVector = (Projectile.rotation - MathHelper.PiOver2).ToRotationVector2();
            usePos += rotVector * 16f;

            const int NUM_DUSTS = 10;

            for (int i = 0; i < NUM_DUSTS; i++)
            {
                Dust dust = Dust.NewDustDirect(usePos, Projectile.width, Projectile.height, DustID.Tin);
                dust.position = (dust.position + Projectile.Center) / 2f;
                dust.velocity += rotVector * 2f;
                dust.velocity *= 0.5f;
                dust.noGravity = true;
                usePos -= rotVector * 8f;
            }
        }

        public bool IsStickingToTarget
        {
            get => Projectile.ai[0] == 1f;
            set => Projectile.ai[0] = value ? 1f : 0f;
        }

        public int TargetWhoAmI
        {
            get => (int)Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }

        private NPC Target => Main.npc[TargetWhoAmI];

        private const int MAX_STICKY_JAVELINS = 3;
        private readonly Point[] _stickingJavelins = new Point[MAX_STICKY_JAVELINS];

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            IsStickingToTarget = true;
            TargetWhoAmI = target.whoAmI;
            Projectile.velocity =
                (target.Center - Projectile.Center) *
                0.75f;
            Projectile.netUpdate = true;
            Projectile.timeLeft = 3600;
            target.AddBuff<Magnetic>(4);
            target.GetGlobalNPC<MagnetizedNPC>().magnetLife += 3f;

            Projectile.damage = 0;

            UpdateStickyJavelins(target);
        }

        private void UpdateStickyJavelins(NPC target)
        {
            int currentJavelinIndex = 0;

            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile currentProjectile = Main.projectile[i];
                if (i != Projectile.whoAmI
                    && currentProjectile.active
                    && currentProjectile.owner == Main.myPlayer
                    && currentProjectile.type == Projectile.type
                    && currentProjectile.ModProjectile is StickingMagnetProj javelinProjectile
                    && javelinProjectile.IsStickingToTarget
                    && javelinProjectile.TargetWhoAmI == target.whoAmI)
                {

                    _stickingJavelins[currentJavelinIndex++] = new Point(i, currentProjectile.timeLeft);
                    if (currentJavelinIndex >= _stickingJavelins.Length)
                        break;
                }
            }

            if (currentJavelinIndex >= MAX_STICKY_JAVELINS)
            {
                int oldJavelinIndex = 0;
                for (int i = 1; i < MAX_STICKY_JAVELINS; i++)
                {
                    if (_stickingJavelins[i].Y < _stickingJavelins[oldJavelinIndex].Y)
                    {
                        oldJavelinIndex = i;
                    }
                }
                Main.projectile[_stickingJavelins[oldJavelinIndex].X].Kill();
                target.GetGlobalNPC<MagnetizedNPC>().magnetLife -= 3f;
            }
        }

        private const int ALPHA_REDUCTION = 25;

        public override void AI()
        {
            UpdateAlpha();
            if (IsStickingToTarget) StickyAI();
            else NormalAI();
        }

        private void UpdateAlpha()
        {
            if (Projectile.alpha > 0)
            {
                Projectile.alpha -= ALPHA_REDUCTION;
            }

            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
        }

        int gravityTimer = 0;
        private void NormalAI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            if (++gravityTimer >= 15)
            {
                Projectile.velocity.Y = Projectile.velocity.Y + 0.1f;
            }
            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }
        }

        private void StickyAI()
        {
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            const int aiFactor = 15;
            Projectile.localAI[0] += 1f;

            int projTargetIndex = TargetWhoAmI;
            if (Projectile.localAI[0] >= 60 * aiFactor || projTargetIndex < 0 || projTargetIndex >= 200)
            {
                Projectile.Kill();
            }
            else if (Target.active && !Target.dontTakeDamage && Target.GetGlobalNPC<MagnetizedNPC>().magnetLife > 0f)
            {
                Projectile.Center = Target.Center - Projectile.velocity * 2f;
                Projectile.gfxOffY = Target.gfxOffY;
                Target.AddBuff<Magnetic>(2);
            }
            else
            {
                Projectile.Kill();
            }
        }
    }
}
