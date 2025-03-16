using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.NPCProj.Nova
{
    public class HardlightSlashHostile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Type] = 30;
            ProjectileID.Sets.TrailingMode[Type] = 6;
            Projectile.AddDamageType(11);
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Projectile.width = 92;
            Projectile.height = 92;

            Projectile.aiStyle = 0;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.light = 0.5f;
            Projectile.timeLeft = 300;

            DrawOriginOffsetY = -39;
        }

        public override void AI()
        {
            Projectile.spriteDirection = Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
            Projectile.rotation = Projectile.velocity.ToRotation() + (Projectile.spriteDirection == 1 ? 0f : MathHelper.Pi);
            if (Main.rand.NextBool(2))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Electric);
            }
            if (SoA.Eternity())
            {
                if (++Projectile.ai[0] >= 4)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<Photosphere>(), Projectile.damage, 0f);
                    Projectile.ai[0] = 0;
                }
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff<ElectricShock>(600);
            if (SoA.Eternity() && Main.rand.NextBool(5))
            {
                target.AddBuff(ModContent.Find<ModBuff>("FargowiltasSouls", "ClippedWingsBuff").Type, 600);
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = SoA.HardlightColorA;
            return base.PreDraw(ref lightColor);
        }
    }
}
