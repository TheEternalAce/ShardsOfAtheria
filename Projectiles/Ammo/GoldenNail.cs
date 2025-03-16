using ShardsOfAtheria.Items.Weapons.Ammo;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Ammo
{
    public class GoldenNail : ModProjectile
    {
        public override string Texture => ModContent.GetInstance<GoldNail>().Texture;

        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(4);
            Projectile.AddRedemptionElement(15);
        }

        public override void SetDefaults()
        {
            Projectile.width = 10; // The width of projectile hitbox
            Projectile.height = 10; // The height of projectile hitbox
            Projectile.aiStyle = 93; // The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = true; // Can the projectile deal damage to enemies?
            Projectile.DamageType = DamageClass.Ranged; // Is the projectile shoot by a ranged weapon?
            Projectile.penetrate = -1; // How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            Projectile.alpha = 255; // The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in) Make sure to delete this if you aren't using an aiStyle that fades in. You'll wonder why your projectile is invisible.
            Projectile.netImportant = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 5;

            AIType = ProjectileID.NailFriendly;
        }

        public override void AI()
        {
            base.AI();
            if (Projectile.ai[0] != 0) Projectile.ai[1] -= 0.75f;
        }
    }
}
