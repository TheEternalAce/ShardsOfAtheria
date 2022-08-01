using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using Terraria.ID;

namespace ShardsOfAtheria.Projectiles.NPCProj.Variant
{
    public class VileFeather : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile refProj = new Projectile();
            refProj.SetDefaults(ProjectileID.HarpyFeather);

            Projectile.width = refProj.width;
            Projectile.height = refProj.height;

            Projectile.timeLeft = 5 * 60;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.expertMode || Main.hardMode)
            {
                target.AddBuff(BuffID.Weak, 60);
            }
        }
    }
}