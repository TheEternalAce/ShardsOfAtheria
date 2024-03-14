using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.Pets;
using ShardsOfAtheria.Items.PetItems;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Pets
{
    public class Scythe : ModProjectile
    {
        public override string Texture => ModContent.GetInstance<AncientDeathsScythe>().Texture;

        public override void SetStaticDefaults()
        {
            Main.projPet[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 58;
            Projectile.height = 60;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (!player.dead && player.HasBuff(ModContent.BuffType<AncientScythe>()))
            {
                Projectile.timeLeft = 2;
            }

            var idlePosition = player.Center + new Vector2(-50, 0) * player.direction;
            var vectorToIdlePosition = idlePosition - Projectile.Center;
            Projectile.velocity = vectorToIdlePosition * 0.055f;
            Projectile.spriteDirection = Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
            if (Projectile.velocity.Length() > 1f)
            {
                Projectile.rotation += MathHelper.ToRadians(Projectile.velocity.Length() * 1.5f) * player.direction;
            }
            else
            {
                float rotation = (player.Center - Projectile.Center).ToRotation();
                float direction = Projectile.rotation - rotation > 0 ? 1 : -1;
                if (Projectile.velocity.X < 0)
                {
                    rotation += MathHelper.Pi;
                }
                Projectile.rotation = MathHelper.Lerp(Projectile.rotation, rotation * direction, MathHelper.ToRadians(5) * direction);
                //Projectile.rotation = rotation;
            }
            if (Projectile.rotation > MathHelper.TwoPi)
            {
                Projectile.rotation -= MathHelper.TwoPi;
            }
            if (Projectile.rotation < -MathHelper.TwoPi)
            {
                Projectile.rotation += MathHelper.TwoPi;
            }
        }
    }
}
