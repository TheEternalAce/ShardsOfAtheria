using Microsoft.Xna.Framework;
using MMZeroElements.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Ranged.DeckOfCards
{
    public class AceOfDiamonds : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddElec();
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.penetrate = 5;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.SetCrit();
            base.ModifyHitNPC(target, ref modifiers);
        }
    }
}
