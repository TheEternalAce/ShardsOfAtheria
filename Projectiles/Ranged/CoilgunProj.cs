using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Utilities;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Ranged
{
    public class CoilgunProj : ModProjectile
    {
        public override string Texture => "ShardsOfAtheria/Items/Weapons/Ranged/Coilgun";

        Player owner => Main.player[Projectile.owner];

        float ChargeTimer { get => Projectile.ai[0]; set => Projectile.ai[0] = value; }

        int aimDir => aimNormal.X > 0 ? 1 : -1;

        public bool BeingHeld => Main.player[Projectile.owner].channel && !Main.player[Projectile.owner].noItems && !Main.player[Projectile.owner].CCed;

        public Vector2 aimNormal;

        public float flashAlpha = 0;
        public float recoilAmount = 0;

        public int chargeLevel = 0;

        public bool charging = true;

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
            ChargeTimer = 60;
            Projectile.velocity = Vector2.Zero;
            SoundEngine.PlaySound(SoA.MagnetChargeUp, Projectile.Center);
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

            //set fancy player arm rotation
            owner.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (aimNormal * 20).RotatedBy(-(recoilAmount * 0.2f * aimDir)).ToRotation() - MathHelper.PiOver2 + 0.3f * aimDir);

            if (BeingHeld)
            {
                Projectile.timeLeft = 60 - (int)(60 * (owner.GetTotalAttackSpeed(DamageClass.Ranged) - 1f)); //constantly set timeLeft to greater than zero to allow projectile to remain infinitely as long as player channels weapon

                if (charging)
                {
                    ChargeTimer--;
                    if (ChargeTimer < 0)
                    {
                        if (chargeLevel >= 1 && chargeLevel < 3)
                        {
                            chargeLevel++;
                            flashAlpha = 1;
                        }
                        else if (chargeLevel < 1) //increment charge level and play charge increase visual effects (white flash + loading click sound)
                        {
                            chargeLevel++;
                            flashAlpha = 1;
                        }
                        ChargeTimer = 60;
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
            var soundType = SoA.MagnetWeakShot;

            //where the projectile should spawn, modified so the projectile actually looks like it's coming out of the barrel
            Vector2 shootOrigin = Projectile.Center + aimNormal * 40;

            bool shoot = owner.PickAmmo(owner.HeldItem, out int dart, out float _,
                out int _, out float knockback, out int _);
            float speed = 12f;
            int damage = Projectile.originalDamage;
            if (chargeLevel > 1)
            {
                soundType = SoA.MagnetShot;
                //increase recoil value, make gun appear like it's actually firing with some force
                recoilAmount += 2f;
                speed *= 2;
                damage += 50;
                if (chargeLevel == 3)
                {
                    recoilAmount += 3f;
                    damage += 50;
                    Projectile.Explode(owner.Center, damage / 2, hostile: true);
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

            Vector2 position = Projectile.Center - Main.screenPosition;
            Vector2 origin = new(18, Projectile.Size.Y / 2);
            SpriteEffects flip = aimDir == 1 ? SpriteEffects.None : SpriteEffects.FlipVertically;

            Main.EntitySpriteDraw(main, position, null, lightColor, Projectile.rotation, origin, 1, flip, 0);
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
