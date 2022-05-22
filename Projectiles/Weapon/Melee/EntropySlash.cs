using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee
{
    public class EntropySlash : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Entropy Slash");
            Main.projFrames[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;

            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.usesLocalNPCImmunity = true;

            Projectile.timeLeft = 25;
        }

        public override void AI()
        {
			if (Projectile.ai[0] == 0)
			{
				Projectile.spriteDirection = Main.rand.NextBool(2) ? 1 : -1;
                Projectile.ai[0] = 1;
			}
            if (++Projectile.frameCounter == 3)
            {
                if (++Projectile.frame > 2)
                {
                    Projectile.frame = 2;
                }
                Projectile.frameCounter = 0;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<LoomingEntropy>(), 600);
        }
    }
}
