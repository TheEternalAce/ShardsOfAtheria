﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic.ElecGauntlet
{
    public class ElecCannon : ModProjectile
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

        public override void SetDefaults()
        {
            Projectile.width = 38;
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
            ChargeTimer = 15;
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
                (aimNormal * 2).RotatedBy(-(recoilAmount * 0.2f * aimDir));
            //set projectile rotation to point towards the aim normal, with adjustments for the recoil visual effect
            Projectile.rotation = aimNormal.ToRotation() - recoilAmount * 0.4f * aimDir;

            //set fancy player arm rotation
            owner.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (aimNormal * 20).RotatedBy(-(recoilAmount * 0.2f * aimDir)).ToRotation() - MathHelper.PiOver2 + 0.3f * aimDir);

            if (charging) //run for a single frame when player stops channeling weapon
            {
                Projectile.timeLeft = 20; //constantly set timeLeft to greater than zero to allow projectile to remain infinitely as long as player channels weapon
                Fire();
                charging = false;
            }
        }

        private void Fire() //method to fire regular projectile
        {
            SoundEngine.PlaySound(SoundID.Item38, Projectile.Center);

            //where the projectile should spawn, modified so the projectile actually looks like it's coming out of the barrel
            Vector2 shootOrigin = Projectile.Center + aimNormal * 5;

            if (Main.myPlayer == Projectile.owner)
            {
                float speed = 16f;
                int damage = Projectile.originalDamage;
                int bullet = ModContent.ProjectileType<FireCannon_Fire_Gauntlet>();
                recoilAmount += 1f;
                Projectile proj = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), shootOrigin,
                    aimNormal * speed, bullet, damage, Projectile.knockBack, Projectile.owner);
                proj.DamageType = Projectile.DamageType;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = SoA.ElectricColorA0;
            return false;
        }

        public override void PostDraw(Color lightColor)
        {
            Texture2D main = TextureAssets.Projectile[Type].Value;

            Vector2 position = Projectile.Center - Main.screenPosition;
            Vector2 origin = new(0, Projectile.Size.Y / 2);
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
