using ShardsOfAtheria.Projectiles.Magic;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee.AreusJoustingLance
{
    public class LightningBolt_Lance : LightningBoltFriendly
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.DamageType = DamageClass.Melee;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.Knockback *= Projectile.GetPlayerOwner().velocity.Length() / 7f;
            modifiers.SourceDamage *= 0.1f + Projectile.GetPlayerOwner().velocity.Length() / 7f * 0.9f;
        }
    }
}
