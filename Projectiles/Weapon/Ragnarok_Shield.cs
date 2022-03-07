using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon
{
    public class Ragnarok_Shield : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 56;

            Projectile.aiStyle = 75;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 1;
            Projectile.penetrate = -1;
            Projectile.light = .4f;
        }

        public override bool? CanCutTiles() => false;

        public override void AI()
        {
            var direction = Main.MouseWorld - Projectile.Center;
            Player owner = Main.player[Projectile.owner];
            if (!Main.mouseLeft)
                Projectile.Kill();
            Projectile.rotation = direction.ToRotation();
            owner.statDefense += 8;
        }

        public override void Kill(int timeLeft)
        {
            Player owner = Main.player[Projectile.owner];
            Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), owner.Center, Vector2.Normalize(Main.MouseWorld - Main.player[Projectile.owner].Center) * 15f, ModContent.ProjectileType<RagnarokProj>(), owner.HeldItem.damage, owner.HeldItem.knockBack, owner.whoAmI);
            SoundEngine.PlaySound(SoundID.Item1);
        }
    }
}
