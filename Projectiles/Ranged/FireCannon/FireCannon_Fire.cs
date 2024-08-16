using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Ranged.FireCannon
{
    public class FireCannon_Fire1 : ModProjectile
    {
        static Asset<Texture2D> glowmask;

        public override void Load()
        {
            if (!Main.dedServ) glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
        }

        public override void Unload()
        {
            glowmask = null;
        }

        public override void SetStaticDefaults()
        {
            Projectile.AddElement(0);
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(2);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;

            Projectile.friendly = true;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 120;
            Projectile.DamageType = DamageClass.Ranged;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            Main.EntitySpriteDraw(glowmask.Value, Projectile.Center, null, SoA.ElectricColorA, 0f, glowmask.Size() / 2, 1f, 0);
            return base.PreDraw(ref lightColor);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.GetPlayerOwner().Shards().Overdrive)
            {
                ShardsHelpers.CallStorm(Projectile.GetSource_FromThis(), Projectile.Center, 3,
                    (int)(Projectile.damage * 0.66f), Projectile.knockBack, DamageClass.Ranged, Projectile.owner);
                return base.OnTileCollide(oldVelocity);
            }
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center,
                Vector2.Zero, ModContent.ProjectileType<ElecFirePillar>(),
                Projectile.damage / 3, 0, Projectile.owner);
            return base.OnTileCollide(oldVelocity);
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 6; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height,
                    DustID.Torch);
            }
            for (int i = 0; i < 3; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height,
                    DustID.Electric);
            }
            if (Projectile.GetPlayerOwner().Shards().Overdrive)
            {
                ShardsHelpers.CallStorm(Projectile.GetSource_FromThis(), Projectile.Center, 3,
                    (int)(Projectile.damage * 0.66f), Projectile.knockBack, DamageClass.Ranged, Projectile.owner);
            }
        }
    }
    public class FireCannon_Fire2 : FireCannon_Fire1
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.penetrate = 5;
        }

        int timer = 0;
        int TimerMax = 1;
        public override void AI()
        {
            base.AI();
            if (++timer >= TimerMax)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero,
                    ModContent.ProjectileType<ChargedFireTrail>(), Projectile.damage / 2, 0, Projectile.owner);
                timer = 0;
            }
        }
    }
    public class FireCannon_Fire3 : FireCannon_Fire1
    {
        public override string Texture => SoA.BlankTexture;

        public override void Load()
        {
        }
        public override void Unload()
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = 30;
            Projectile.height = 30;

            Projectile.timeLeft *= 2;
            Projectile.penetrate = -1;
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.X -= 35;
            hitbox.Y -= 35;
            hitbox.Width += 70;
            hitbox.Height += 70;
        }

        int timer = 0;
        int TimerMax = 30;
        public override void AI()
        {
            base.AI();
            if (++timer >= TimerMax)
            {
                SoundEngine.PlaySound(SoundID.Item20, Projectile.Center);
                int numProjectiles = 6;
                for (int i = 0; i < numProjectiles; i++)
                {
                    float angle = 360 / numProjectiles;
                    float rotAngle = MathHelper.ToRadians(angle * i);
                    rotAngle += MathHelper.ToRadians(angle / 2);
                    Vector2 vector = Projectile.velocity.RotatedBy(rotAngle);
                    vector.Normalize();
                    vector *= 6;
                    Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(),
                        Projectile.Center, vector, ProjectileID.Flames, Projectile.damage / 2, 0,
                        Projectile.owner);
                }
                timer = 0;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<FieryExplosion>(), Projectile.damage, 1f);
            return true;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.EntitySpriteDraw(SoA.OrbBloom, Projectile.Center - Main.screenPosition, SoA.OrbBloom.Frame(), SoA.ElectricColorA, 0f, SoA.OrbBloom.Size() / 2, 2.5f, SpriteEffects.None);
            return false;
        }
    }
}
