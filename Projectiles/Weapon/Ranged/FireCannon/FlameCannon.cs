using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Utilities;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Effects.ScreenShaking;

namespace ShardsOfAtheria.Projectiles.Weapon.Ranged.FireCannon
{
    public class FlameCannon : ModProjectile
    {
        Player owner => Main.player[Projectile.owner];

        float ChargeTimer { get => Projectile.ai[0]; set => Projectile.ai[0] = value; }

        int aimDir => aimNormal.X > 0 ? 1 : -1;

        public bool BeingHeld => Main.player[Projectile.owner].channel && !Main.player[Projectile.owner].noItems && !Main.player[Projectile.owner].CCed;

        public Vector2 aimNormal;

        public float flashAlpha = 0;
        public float recoilAmount = 0;

        public int chargeLevel = 0;

        public bool charging = true;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 78;
            Projectile.height = 20;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 2;
        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.originalDamage = Projectile.damage;
            Projectile.damage = 0;
            ChargeTimer = 10;
        }

        public override bool PreAI()
        {
            owner.heldProj = Projectile.whoAmI;
            owner.itemAnimation = 2;
            owner.itemTime = 2;

            if (Main.myPlayer == Projectile.owner)
            {
                //aimNormal is a normal vector pointing from the player at the mouse cursor
                aimNormal = Vector2.Normalize(Main.MouseWorld - owner.MountedCenter + new Vector2(owner.direction * -3, -1));
            }

            owner.ChangeDir(aimDir);
            return true;
        }

        public override void AI()
        {
            Projectile.netUpdate = true;

            flashAlpha *= 0.85f; //constantly decrease alpha of the charge increase visual effect
            recoilAmount *= 0.85f; //constantly decrease the value of the variable that controls the recoil-esque visual effect of the gun's position

            //set projectile center to be at the player, slightly offset towards the aim normal, with adjustments for the recoil visual effect
            Projectile.Center = owner.MountedCenter + new Vector2(owner.direction * -3, -1) +
                (aimNormal * 10).RotatedBy(-(recoilAmount * 0.2f * aimDir));
            //set projectile rotation to point towards the aim normal, with adjustments for the recoil visual effect
            Projectile.rotation = aimNormal.ToRotation() - recoilAmount * 0.4f * aimDir;
            UpdateVisual();

            //set fancy player arm rotation
            owner.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (aimNormal * 20).RotatedBy(-(recoilAmount * 0.2f * aimDir)).ToRotation() - MathHelper.PiOver2 + 0.3f * aimDir);

            if (BeingHeld)
            {
                Projectile.timeLeft = 10; //constantly set timeLeft to greater than zero to allow projectile to remain infinitely as long as player channels weapon

                if (charging)
                {
                    ChargeTimer--;
                    if (ChargeTimer < 0)
                    {
                        if (chargeLevel < 3)
                        {
                            chargeLevel++;
                            flashAlpha = 1;
                            ChargeTimer = 60;
                            SoundEngine.PlaySound(SoundID.DD2_BetsyFireballImpact, Projectile.Center);
                        }
                    }
                }
            }
            else
            {
                if (charging) //run for a single frame when player stops channeling weapon
                {
                    Fire();
                }
                charging = false;
            }
        }

        private void Fire() //method to fire regular projectile
        {
            if (chargeLevel == 0)
            {
                return;
            }
            SoundEngine.PlaySound(SoundID.Item38, Projectile.Center);

            //where the projectile should spawn, modified so the projectile actually looks like it's coming out of the barrel
            Vector2 shootOrigin = Projectile.Center + aimNormal * 5;

            if (Main.myPlayer == Projectile.owner)
            {
                bool shoot = owner.PickAmmo(owner.HeldItem, out int _, out float _,
                    out int _, out float knockback, out int _);
                float speed = 20f;
                int damage = Projectile.originalDamage;
                int flame = ModContent.ProjectileType<FireCannon_Fire1>();
                if (chargeLevel >= 2)
                {
                    //increase recoil value, make gun appear like it's actually firing with some force
                    recoilAmount += 2f;
                    damage += 50;
                    flame = ModContent.ProjectileType<FireCannon_Fire2>();
                    if (chargeLevel == 3)
                    {
                        recoilAmount += 3f;
                        speed /= 2;
                        damage += 50;
                        flame = ModContent.ProjectileType<FireCannon_Fire3>();
                    }
                }
                if (shoot)
                {
                    if (chargeLevel == 3)
                    {
                        ScreenShake.ShakeScreen(6, 60);
                    }
                    float numberProjectiles = 2; // 2 shots
                    var source = Projectile.GetSource_FromThis();
                    var velocity = aimNormal * speed;

                    float num117 = (float)Math.PI / 10f;
                    float num90 = Main.mouseX + Main.screenPosition.X - shootOrigin.X;
                    float num101 = Main.mouseY + Main.screenPosition.Y - shootOrigin.Y;
                    Vector2 vector7 = new Vector2(num90, num101);
                    vector7.Normalize();
                    vector7 *= 50f;
                    bool flag3 = Collision.CanHit(shootOrigin, 0, 0, shootOrigin + vector7, 0, 0);
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        float num120 = i - (numberProjectiles - 1f) / 2f;
                        Vector2 vector8 = vector7.RotatedBy(num117 * num120);
                        if (!flag3)
                        {
                            vector8 -= vector7;
                        }
                        Projectile.NewProjectile(source, shootOrigin + vector8, velocity, flame, damage, knockback, owner.whoAmI);
                    }
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }

        public override void PostDraw(Color lightColor)
        {
            Texture2D main = TextureAssets.Projectile[Type].Value;

            Vector2 position = Projectile.Center - Main.screenPosition;
            Vector2 origin = new(16, 56 / 2);
            SpriteEffects flip = aimDir == 1 ? SpriteEffects.None : SpriteEffects.FlipVertically;

            Main.EntitySpriteDraw(main, position, Projectile.Frame(), lightColor, Projectile.rotation, origin, 1, flip, 0);
        }

        public override void SendExtraAI(BinaryWriter writer) //important because mouse cursor logic is really unstable in multiplayer if done wrong
        {
            writer.WriteVector2(aimNormal);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            aimNormal = reader.ReadVector2();
        }

        void UpdateVisual()
        {
            Projectile.frame = chargeLevel;
        }
    }
}
