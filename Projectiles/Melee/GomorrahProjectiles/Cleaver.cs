using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee.GomorrahProjectiles
{
    public class Cleaver : ModProjectile
    {
        public override string Texture => ModContent.GetInstance<Gomorrah>().Texture;

        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(11);
            Projectile.AddElement(3);
            Projectile.AddRedemptionElement(12);

            Projectile.MakeMetalic();
        }

        public override void SetDefaults()
        {
            Projectile.width = 15;
            Projectile.height = 15;

            Projectile.friendly = true;
            Projectile.penetrate = 4;
            Projectile.DamageType = DamageClass.Melee;
        }

        int gravityDelay = 16;
        public override void AI()
        {
            Projectile.spriteDirection = Projectile.direction;
            Projectile.SetVisualOffsets(new Vector2(56, 48), true);
            Projectile.rotation += MathHelper.ToRadians(15) * Projectile.direction;
            Projectile.ApplyGravity(ref gravityDelay, 0.5f);
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.Resize(40, 40);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            return base.OnTileCollide(oldVelocity);
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Blood,
                    Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
            }
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Iron,
                    Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
            }
        }
    }
}
