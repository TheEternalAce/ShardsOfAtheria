using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee
{
    public class SilverRing : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(1);
            Projectile.AddRedemptionElement(1);
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;

            Projectile.timeLeft = 300;
            Projectile.aiStyle = ProjAIStyleID.ThrownProjectile;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // If the projectile hits the left or right side of the tile, reverse the X velocity
            if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon)
                Projectile.velocity.X = -oldVelocity.X * 0.25f;

            // If the projectile hits the top or bottom side of the tile, reverse the Y velocity
            if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
                Projectile.velocity.Y = -oldVelocity.Y * 0.25f;
            return false;
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (target.type == NPCID.Werewolf) modifiers.ScalingBonusDamage += 1f;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            return base.PreDraw(ref lightColor);
        }
    }
}
