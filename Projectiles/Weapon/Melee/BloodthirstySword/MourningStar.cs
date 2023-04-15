using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Bases;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee.BloodthirstySword
{
    public class MourningStar : SwordProjectileBase
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 70;
            ProjectileID.Sets.TrailingMode[Type] = -1;
            SoAGlobalProjectile.DarkAreusProj.Add(Type);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Projectile.width = Projectile.height = 30;
            swordReach = 45;
            rotationOffset = -MathHelper.PiOver4 * 3f;
            amountAllowedToHit = 5;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            if (player.HeldItem.type == ModContent.ItemType<TheMourningStar>())
            {
                TheMourningStar mourningStar = Main.LocalPlayer.HeldItem.ModItem as TheMourningStar;
                mourningStar.blood += 20;
                if (target.life <= 0)
                {
                    mourningStar.blood += 40;
                }
            }
            base.OnHitNPC(target, damage, knockback, crit);
        }

        protected override void Initialize(Player player, ShardsPlayer shards)
        {
            base.Initialize(player, shards);
            if (shards.itemCombo > 0)
            {
                swingDirection *= -1;
            }
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
                Main.player[Projectile.owner].ShardsOfAtheria().itemCombo = (ushort)(combo == 0 ? 20 : 0);
            }
            if (!playedSound && AnimProgress > 0.4f)
            {
                playedSound = true;
                SoundEngine.PlaySound(SoundID.Item1.WithPitchOffset(-1f), Projectile.Center);
            }
        }

        public override Vector2 GetOffsetVector(float progress)
        {
            return BaseAngleVector.RotatedBy((progress * (MathHelper.Pi * 1.5f) - MathHelper.PiOver2 * 1.5f) * -swingDirection * 1.1f);
        }

        public override void UpdateSwing(float progress, float interpolatedSwingProgress)
        {
            if (progress > 0.85f)
            {
                Projectile.Opacity = 1f - (progress - 0.85f) / 0.15f;
            }

            Projectile.oldPos[0] = AngleVector * 60f * Projectile.scale;
            Projectile.oldRot[0] = Projectile.oldPos[0].ToRotation() + MathHelper.PiOver4;

            // Manually updating oldPos and oldRot 
            for (int i = Projectile.oldPos.Length - 1; i > 0; i--)
            {
                Projectile.oldPos[i] = Projectile.oldPos[i - 1];
                Projectile.oldRot[i] = Projectile.oldRot[i - 1];
            }

            if (progress == 0.5f && Main.myPlayer == Projectile.owner)
            {
                Player player = Main.player[Projectile.owner];

                if (player.HeldItem.type == ModContent.ItemType<TheMourningStar>())
                {
                    TheMourningStar mourningStar = Main.LocalPlayer.HeldItem.ModItem as TheMourningStar;

                    if (mourningStar.blood >= TheMourningStar.BloodProjCost)
                    {
                        Vector2 position = Projectile.Center;
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), position, AngleVector * Projectile.velocity.Length() * 16f,
                            ModContent.ProjectileType<BloodCutter>(), (int)(Projectile.damage * 0.75), (int)(Projectile.knockBack * 0.75), player.whoAmI);
                        mourningStar.blood -= TheMourningStar.BloodProjCost;
                    }
                }
            }
        }

        public override float SwingProgress(float progress)
        {
            return GenericSwing2(progress);
        }

        public override float GetVisualOuter(float progress, float swingProgress)
        {
            return 0f;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return SingleEdgeSwordDraw<TheMourningStar>(lightColor);
        }
    }
}