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
                Projectile.timeLeft = 2;

            var idlePosition = player.Center + new Vector2(-100 * player.direction, 0);
            var vectorToIdlePosition = idlePosition - Projectile.Center;
            Projectile.velocity = vectorToIdlePosition * 0.055f;
            Projectile.spriteDirection = Projectile.direction = (Projectile.Center.X < player.Center.X).ToDirectionInt();
            Projectile.rotation += MathHelper.ToRadians(Projectile.velocity.Length() * 1.5f * Projectile.direction);
        }
    }
}
