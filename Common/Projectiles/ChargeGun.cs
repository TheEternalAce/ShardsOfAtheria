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
        /// <summary>
        /// How long the projectile should last after stopping channel.
        /// </summary>
        public abstract int LingerDuration { get; }
        /// <summary>
        /// How long should it take to increase charge level.
        /// </summary>
        public abstract float ChargeLevelTime { get; }
        public abstract float BaseShootSpeed { get; }

        public virtual int MaxCharge => 3;

        public virtual SoundStyle ChargeLevelUpSound => SoundID.Unlock.WithPitchOffset(1).WithVolumeScale(2f);
        public virtual SoundStyle ShootSound => SoundID.Item10;

        public virtual string FlashPath => Texture + "_Flash";

        internal Player Owner => Main.player[Projectile.owner];

        internal float ChargeTimer { get => Projectile.ai[0]; set => Projectile.ai[0] = value; }

        internal int AimDir => aimNormal.X > 0 ? 1 : -1;

        public bool BeingHeld => Owner.channel && !Owner.noItems && !Owner.CCed;

        public Vector2 aimNormal;

        public float flashAlpha = 0;
        public float recoilAmount = 0;

        public int chargeLevel = 0;
        public int totalChargeTime;

        public bool charging = true;

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
            UpdateVisual();

            //set fancy player arm rotation
            Owner.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (aimNormal * 20).RotatedBy(-(recoilAmount * 0.2f * AimDir)).ToRotation() - MathHelper.PiOver2 + 0.3f * AimDir);

            if (BeingHeld)
            {
                Projectile.timeLeft = LingerDuration; //constantly set timeLeft to greater than zero to allow projectile to remain infinitely as long as player channels weapon

                if (charging && (chargeLevel < MaxCharge || MaxCharge == -1))
                {
                    ChargeTimer++;
                    if (CanCharge())
                    {
                        if (chargeLevel >= 0) //increment charge level and play charge increase visual effects (white flash + loading click sound)
                        {
                            chargeLevel++;
                            flashAlpha = 1;
                            SoundEngine.PlaySound(ChargeLevelUpSound, Projectile.Center);
                        }
                        totalChargeTime += (int)ChargeTimer;
                        ChargeTimer = 0;
                        OnChargeIncrement();
                    }
                }
            }
            else if (GetFireStats(BaseShootSpeed, out Vector2 position, out Vector2 velocity, out int type, out int damage, out float knockback, out float recoil))
                Fire(position, velocity, type, damage, knockback, recoil); //run for a single frame when player stops channeling weapon

        }

        internal virtual bool CanCharge()
        {
            return ChargeTimer >= ChargeLevelTime;
        }

        internal virtual void OnChargeIncrement()
        {

        }

        internal virtual bool GetFireStats(float speed, out Vector2 position, out Vector2 velocity, out int type, out int damage, out float knockback, out float recoil)
        {
            position = Projectile.Center;
            if (!Collision.CanHit(position, 0, 0, Owner.Center, 0, 0)) position = Owner.Center;
            bool shoot = Owner.PickAmmo(Owner.HeldItem, out type, out float _,
                out int _, out knockback, out int _);
            velocity = aimNormal * speed;
            damage = Projectile.originalDamage * (chargeLevel + 1);
            recoil = 1f;
            return shoot && charging;
        }

        internal virtual void Fire(Vector2 position, Vector2 velocity, int type, int damage, float knockback, float recoil)
        {
            recoilAmount += recoil;
            SoundEngine.PlaySound(ShootSound, Projectile.Center);
            charging = false;

            Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), position,
                velocity, type, damage, knockback, Projectile.owner);
        }

        internal virtual void UpdateVisual()
        {

        }

        internal virtual Vector2 HoldOffset()
        {
            return Vector2.Zero;
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
            Vector2 origin = Projectile.Size / 2 + HoldOffset();
            SpriteEffects flip = AimDir == 1 ? SpriteEffects.None : SpriteEffects.FlipVertically;

            Main.EntitySpriteDraw(main, position, Projectile.Frame(), lightColor, Projectile.rotation, origin, 1, flip, 0);
            Main.EntitySpriteDraw(flash, position, Projectile.Frame(), Color.White * flashAlpha, Projectile.rotation, origin, 1, flip, 0);
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
