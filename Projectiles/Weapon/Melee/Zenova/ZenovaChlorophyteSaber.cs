using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee.Zenova
{
    public class ZenovaChlorophyteSaber : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zenova Chlorophyte Saber");
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;

            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.light = 0.5f;
            Projectile.extraUpdates = 1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;

            DrawOffsetX = -28;
            DrawOriginOffsetX = 14;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
            if (Projectile.spriteDirection == 1)
            {
                DrawOffsetX = -28;
                DrawOriginOffsetX = 14;
            }
            else
            {
                DrawOffsetX = 0;
                DrawOriginOffsetX = -14;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Ichor, 60);
            target.AddBuff(BuffID.Blackout, 3600);
        }
    }
}
