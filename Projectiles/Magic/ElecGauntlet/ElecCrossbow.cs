using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic.ElecGauntlet
{
    public class ElecCrossbow : ModProjectile
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
            Projectile.width = 52;
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
            ChargeTimer = 20;
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
                (aimNormal * -2).RotatedBy(-(recoilAmount * 0.2f * aimDir));
            //set projectile rotation to point towards the aim normal, with adjustments for the recoil visual effect
            Projectile.rotation = aimNormal.ToRotation() - recoilAmount * 0.4f * aimDir;

            //set fancy player arm rotation
            owner.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, (aimNormal * 20).RotatedBy(-(recoilAmount * 0.2f * aimDir)).ToRotation() - MathHelper.PiOver2 + 0.3f * aimDir);

            if (charging) //run for a single frame when player stops channeling weapon
            {
                Projectile.timeLeft = 20;
                Fire();
                charging = false;
            }
        }

        private void Fire() //method to fire regular projectile
        {
            //where the projectile should spawn, modified so the projectile actually looks like it's coming out of the barrel
            Vector2 shootOrigin = Projectile.Center + aimNormal * 5;

            if (Main.myPlayer == Projectile.owner)
            {
                float speed = 16;
                int damage = Projectile.originalDamage;
                float numberProjectiles = 2; // 2 shots
                float rotation = MathHelper.ToRadians(10);
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = aimNormal.RotatedBy(MathHelper.Lerp(-rotation, rotation,
                        i / (numberProjectiles - 1))) * speed;
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), shootOrigin, perturbedSpeed,
                        ModContent.ProjectileType<AreusArrowProj_Gauntlet>(), damage, Projectile.knockBack,
                        Projectile.owner);
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
