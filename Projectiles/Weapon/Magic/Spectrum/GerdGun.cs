using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Magic.Spectrum
{
    public class GerdGun : ModProjectile
    {
        public override string Texture => "ShardsOfAtheria/Items/DedicatedItems/MrGerd26/AlphaSpectrum";

        Player Owner => Main.player[Projectile.owner];

        float AttackSpeed => Owner.GetAttackSpeed(DamageClass.Generic) +
            Owner.GetAttackSpeed(DamageClass.Magic) - 2;

        float ChargeTimer
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        int AimDir => aimNormal.X > 0 ? 1 : -1;

        public bool BeingHeld => Main.player[Projectile.owner].channel && !Main.player[Projectile.owner].noItems && !Main.player[Projectile.owner].CCed;

        public Vector2 aimNormal;

        public float flashAlpha = 0;
        public float recoilAmount = 0;

        public int chargeLevel = 0;

        public bool charging = true;

        int laserNum = 0;

        public override void SetDefaults()
        {
            Projectile.width = 100;
            Projectile.height = 42;
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
            ChargeTimer = 20 - (int)(20 * AttackSpeed);
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
                (aimNormal * -2).RotatedBy(-(recoilAmount * 0.2f * AimDir));
            //set projectile rotation to point towards the aim normal, with adjustments for the recoil visual effect
            Projectile.rotation = aimNormal.ToRotation() - recoilAmount * 0.4f * AimDir;

            //set fancy player arm rotation
            Owner.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (aimNormal * 20).RotatedBy(-(recoilAmount * 0.2f * AimDir)).ToRotation() - MathHelper.PiOver2 + 0.3f * AimDir);

            if (BeingHeld)
            {
                Projectile.timeLeft = 10; //constantly set timeLeft to greater than zero to allow projectile to remain infinitely as long as player channels weapon
                Owner.manaRegenDelay = 10;

                if (charging)
                {
                    ChargeTimer--;

                    int manaCost = (int)(20 * Owner.manaCost);
                    if (ChargeTimer <= 0) //increment charge level and play charge increase visual effects (white flash + loading click sound)
                    {
                        if (Owner.statMana >= manaCost)
                        {
                            Owner.statMana -= manaCost;
                            chargeLevel++;

                            SoundEngine.PlaySound(SoundID.Item61, Projectile.Center);
                            flashAlpha = 1;
                            ChargeTimer = 15 - (int)(15 * AttackSpeed);
                            CombatText.NewText(Owner.getRect(), Color.Cyan, chargeLevel + 1);
                        }
                        else if (Owner.manaFlower)
                        {
                            Owner.QuickMana();
                        }
                    }
                }
            }
            else
            {
                int framesToShoot = 5 - (int)(5 * AttackSpeed);
                if (charging) //run for a single frame when player stops channeling weapon
                {
                    if (chargeLevel < 1) //fire normal projectile if gun is completely uncharged (basic tap-fire)
                    {
                        Fire();
                    }
                    else
                    {
                        //set timeLeft to a high enough value to allow entire burst fire to happen
                        Projectile.timeLeft = chargeLevel * framesToShoot + 14;
                        //ChargeTimer has use changed from delay between charge increments to a timer to control burst fire. this is done solely to use less variables
                        ChargeTimer = chargeLevel * framesToShoot + 8;
                    }
                }
                else //runs every frame from when the player stops channeling until projectile despawns
                {
                    if (chargeLevel >= 1)
                    {
                        ChargeTimer--;

                        if (ChargeTimer % framesToShoot == 0 && Projectile.timeLeft >= 10) //fire plasma ball every 3 frames until ChargeTimer reaches 0
                        {
                            Fire();
                        }
                    }
                }

                charging = false;
            }
        }

        private void Fire() //method to fire regular projectile
        {
            //increase recoil value, make gun appear like it's actually firing with some force
            recoilAmount += Main.rand.NextFloat(0.5f, 0.8f);
            SoundEngine.PlaySound(SoundID.Item12, Projectile.Center);

            //where the projectile should spawn, modified so the projectile actually looks like it's coming out of the barrel
            Vector2 shootOrigin = Projectile.Center + aimNormal * 40;

            if (Main.myPlayer == Projectile.owner)
            {
                float speed = 16;
                int damage = Projectile.originalDamage;
                float numberProjectiles = 3; // 3 shots
                float rotation = MathHelper.ToRadians(10);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = aimNormal.RotatedBy(MathHelper.Lerp(-rotation, rotation,
                        i / (numberProjectiles - 1))) * speed;
                    Projectile projectile = Projectile.NewProjectileDirect(
                        Projectile.GetSource_FromThis(), shootOrigin, perturbedSpeed,
                        ModContent.ProjectileType<SpectrumLaser>(), damage, 6, Projectile.owner);
                    if (projectile.ModProjectile is SpectrumLaser laser)
                    {
                        laser.laserColor = GetLaserColor();
                    }
                }
                if (++laserNum > 5)
                {
                    laserNum = 0;
                }
            }
        }

        Color GetLaserColor()
        {
            Color color = laserNum switch
            {
                0 => Color.Purple,
                1 => Color.Blue,
                2 => Color.Green,
                3 => Color.Yellow,
                4 => Color.Orange,
                5 => Color.Red,
                _ => Color.White,
            };
            return color;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }

        public override void PostDraw(Color lightColor)
        {
            Texture2D main = TextureAssets.Projectile[Type].Value;

            Vector2 position = Projectile.Center - Main.screenPosition;
            Vector2 origin = Projectile.Size / 2;
            origin.X -= 25;
            SpriteEffects flip = (AimDir == 1) ? SpriteEffects.None : SpriteEffects.FlipVertically;

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
