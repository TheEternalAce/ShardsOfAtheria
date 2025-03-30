using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Utilities;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Common.Projectiles
{
    public abstract class ChargeGun : ModProjectile
    {
        public override string Texture => "ShardsOfAtheria/Items/Weapons/Ranged/AreusRailgun";

        Player Owner => Main.player[Projectile.owner];

        float ChargeTimer { get => Projectile.ai[0]; set => Projectile.ai[0] = value; }

        int AimDir => aimNormal.X > 0 ? 1 : -1;

        public bool BeingHeld => Owner.channel && !Owner.noItems && !Owner.CCed;

        public Vector2 aimNormal;

        public float flashAlpha = 0;
        public float recoilAmount = 0;

        public int chargeLevel = 0;

        public bool charging = true;

        public abstract string FlashPath { get; }

        public override void SetDefaults()
        {
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
            Projectile.velocity = Vector2.Zero;
            SoundEngine.PlaySound(SoA.MagnetChargeUp, Projectile.Center);
        }

        public override bool PreAI()
        {
            Owner.heldProj = Projectile.whoAmI;
            Owner.itemAnimation = 2;
            Owner.itemTime = 2;

            if (Main.myPlayer == Projectile.owner)
            {
                //aimNormal is a normal vector pointing from the player at the mouse cursor
                aimNormal = Vector2.Normalize(Main.MouseWorld - Owner.MountedCenter + new Vector2(Owner.direction * -3, -1));
            }

            Owner.ChangeDir(AimDir);
            return true;
        }

        public override void AI()
        {
            Projectile.netUpdate = true;

            flashAlpha *= 0.85f; //constantly decrease alpha of the charge increase visual effect
            recoilAmount *= 0.85f; //constantly decrease the value of the variable that controls the recoil-esque visual effect of the gun's position

            //set projectile center to be at the player, slightly offset towards the aim normal, with adjustments for the recoil visual effect
            Projectile.Center = Owner.MountedCenter + new Vector2(Owner.direction * -3, -1) +
                (aimNormal * 10).RotatedBy(-(recoilAmount * 0.2f * AimDir));
            //set projectile rotation to point towards the aim normal, with adjustments for the recoil visual effect
            Projectile.rotation = aimNormal.ToRotation() - recoilAmount * 0.4f * AimDir;

            //set fancy player arm rotation
            Owner.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (aimNormal * 20).RotatedBy(-(recoilAmount * 0.2f * AimDir)).ToRotation() - MathHelper.PiOver2 + 0.3f * AimDir);

            if (BeingHeld)
            {
                Projectile.timeLeft = Owner.ApplyAttackSpeed(60, DamageClass.Ranged, 2); //constantly set timeLeft to greater than zero to allow projectile to remain infinitely as long as player channels weapon

                if (charging && chargeLevel < 3)
                {
                    ChargeTimer++;
                    float ChargeTimerMax = Owner.ApplyAttackSpeed(60, DamageClass.Ranged, 0, 0.5f) * (chargeLevel * 0.1f + 1);
                    if (ChargeTimer >= ChargeTimerMax)
                    {
                        if (chargeLevel >= 0) //increment charge level and play charge increase visual effects (white flash + loading click sound)
                        {
                            chargeLevel++;
                            flashAlpha = 1;
                            SoundEngine.PlaySound(SoundID.Unlock.WithPitchOffset(chargeLevel).WithVolumeScale(2f), Projectile.Center);
                        }
                        ChargeTimer = 0;
                    }
                }
            }
            else
            {
                if (charging) Fire(); //run for a single frame when player stops channeling weapon
                charging = false;
            }
        }

        private void Fire() //method to fire regular projectile
        {
            var soundType = SoA.MagnetWeakShot;

            //where the projectile should spawn, modified so the projectile actually looks like it's coming out of the barrel
            Vector2 shootOrigin = Projectile.Center;
            if (Collision.CanHit(shootOrigin, 0, 0, Projectile.GetPlayerOwner().Center, 0, 0)) shootOrigin = Projectile.GetPlayerOwner().Center;

            bool shoot = Owner.PickAmmo(Owner.HeldItem, out int dart, out float _,
                out int _, out float knockback, out int _);
            float speed = 12f;
            int damage = Projectile.originalDamage;
            if (chargeLevel >= 1) speed += 4f;
            if (chargeLevel > 1)
            {
                //increase recoil value, make gun appear like it's actually firing with some force
                soundType = SoA.MagnetShot;
                recoilAmount += 2f;
                speed *= 1.5f;
                damage += 50;
                if (chargeLevel == 3)
                {
                    recoilAmount += 3f;
                    damage += 50;
                }
            }
            if (shoot)
            {
                SoundEngine.StopTrackedSounds();
                SoundEngine.PlaySound(soundType, Projectile.Center);
                Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), shootOrigin,
                    aimNormal * speed, dart, damage, knockback, Projectile.owner);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }

        public override void PostDraw(Color lightColor)
        {
            Texture2D main = TextureAssets.Projectile[Type].Value;
            Texture2D flash = ModContent.Request<Texture2D>(FlashPath).Value;

            Vector2 position = Projectile.Center - Main.screenPosition;
            Vector2 origin = new(18, Projectile.Size.Y / 2);
            SpriteEffects flip = AimDir == 1 ? SpriteEffects.None : SpriteEffects.FlipVertically;

            Main.EntitySpriteDraw(main, position, null, lightColor, Projectile.rotation, origin, 1, flip, 0);
            Main.EntitySpriteDraw(flash, position, null, Color.White * flashAlpha, Projectile.rotation, origin, 1, flip, 0);
        }

        public override void SendExtraAI(BinaryWriter writer) //important because mouse cursor logic is really unstable in multiplayer if done wrong
        {
            writer.WriteVector2(aimNormal);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            aimNormal = reader.ReadVector2();
        }
    }
}
