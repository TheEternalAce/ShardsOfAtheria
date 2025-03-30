using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Projectiles;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Melee.GenesisRagnarok.IceStuff;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee.GenesisRagnarok
{
    public class Genesis_Sword : CoolSword
    {
        public override void SetStaticDefaults()
        {
            Projectile.MakeTrueMelee();
            SoAGlobalProjectile.Eraser.Add(Type);
            Projectile.AddDamageType(2, 5);
            Projectile.AddElement(1);
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(4);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Projectile.width = Projectile.height = 30;
            swordReach = 190;
            rotationOffset = -MathHelper.PiOver4 * 3f;
            hitsLeft = 5;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];
            ShardsPlayer shardsPlayer = player.Shards();
            int upgrades = shardsPlayer.genesisRagnarockUpgrades;

            if (player.HeldItem.type == ModContent.ItemType<GenesisAndRagnarok>())
            {
                if (upgrades < 5 && upgrades >= 3)
                {
                    target.AddBuff(BuffID.OnFire, 600);
                }
                else if (upgrades == 5)
                {
                    target.AddBuff(BuffID.Frostburn, 600);
                    ShardsHelpers.CallStorm(Projectile.GetSource_FromThis(), Projectile.Center, 3,
                        (int)(Projectile.damage * 0.66f), Projectile.knockBack, DamageClass.Melee, Projectile.owner);
                }
            }
            base.OnHitNPC(target, hit, damageDone);
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
                Main.player[Projectile.owner].Shards().itemCombo = (ushort)(combo == 0 ? 20 : 0);
            }
            if (!playedSound && AnimProgress > 0.4f)
            {
                playedSound = true;
                SoundEngine.PlaySound(SoundID.Item1.WithPitchOffset(-1f), Projectile.Center);
            }
        }

        public override void UpdateSwing(float progress, float interpolatedSwingProgress)
        {
            if (progress == 0.5f && Main.myPlayer == Projectile.owner)
            {
                Player player = Main.player[Projectile.owner];
                ShardsPlayer shardsPlayer = player.Shards();
                int upgrades = shardsPlayer.genesisRagnarockUpgrades;

                if (player.HeldItem.type == ModContent.ItemType<GenesisAndRagnarok>())
                {
                    if (upgrades == 5)
                    {
                        float numberProjectiles = 3;
                        float shardRotation = MathHelper.ToRadians(15);
                        Vector2 position = Projectile.Center;
                        Vector2 velocity = AngleVector * Projectile.velocity.Length() * 16f;
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-shardRotation, shardRotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                            Projectile.NewProjectile(Projectile.GetSource_FromThis(), position, perturbedSpeed, ModContent.ProjectileType<IceShard>(), Projectile.damage, Projectile.knockBack, player.whoAmI);
                        }
                    }
                }
            }
        }

        public override Vector2 GetOffsetVector(float progress)
        {
            return BaseAngleVector.RotatedBy((progress * (MathHelper.Pi * 1.5f) - MathHelper.PiOver2 * 1.5f) * -swingDirection * 1.1f);
        }

        public override float SwingProgress(float progress)
        {
            return SwingProgressAequus(progress);
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
            DrawSwish();
            return GenericSwordDraw(lightColor);
        }
    }
}