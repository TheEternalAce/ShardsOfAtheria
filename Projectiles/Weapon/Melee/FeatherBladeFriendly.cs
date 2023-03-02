using Microsoft.Xna.Framework;
using MMZeroElements;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Dusts;
using ShardsOfAtheria.Projectiles.NPCProj.Nova;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee
{
    public class FeatherBladeFriendly : ModProjectile
    {
        public override string Texture => "ShardsOfAtheria/Projectiles/NPCProj/Nova/FeatherBlade";

        public override void SetStaticDefaults()
        {
            ProjectileElements.Electric.Add(Type);
            ProjectileElements.Metal.Add(Type);
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ModContent.ProjectileType<FeatherBlade>());
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.timeLeft = 120;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 0;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            if (++Projectile.ai[0] >= 60 && !Projectile.tileCollide)
            {
                Projectile.tileCollide = true;
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 2; i++)
            {
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<HardlightDust_Blue>(), Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<HardlightDust_Pink>(), Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(ModContent.BuffType<ElectricShock>(), 60);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<ElectricShock>(), 60);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Color color = new(227, 182, 245, 80);
            Projectile.DrawProjectilePrims(color, ProjectileHelper.DiamondX1);
            lightColor = Color.White;
            return true;
        }
    }
}
