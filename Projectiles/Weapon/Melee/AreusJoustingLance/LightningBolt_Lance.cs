using ShardsOfAtheria.Projectiles.Weapon.Magic;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee.AreusJoustingLance
{
    public class LightningBolt_Lance : LightningBoltFriendly
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.DamageType = DamageClass.Melee;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            modifiers.Knockback *= Projectile.GetPlayer().velocity.Length() / 7f;
            modifiers.SourceDamage *= 0.1f + Projectile.GetPlayer().velocity.Length() / 7f * 0.9f;
        }
    }
}
