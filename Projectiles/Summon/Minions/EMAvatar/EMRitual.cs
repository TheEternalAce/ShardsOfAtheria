using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Summon.Minions.EMAvatar
{
    public class EMRitual : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 180;
            Projectile.height = 180;
            Projectile.timeLeft = 360000;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];

            if (Projectile.localAI[0] == 0)
            {
                var target = ShardsHelpers.FindClosestProjectile(Projectile.Center, 3000, ModContent.ProjectileType<EMAvatar>(), Projectile.owner);
                Projectile.localAI[0] = target.whoAmI;
            }
            var avatar = Main.projectile[(int)Projectile.localAI[0]];

            if (!CheckActive(owner) || avatar == null)
                Projectile.Kill();

            Projectile.Center = avatar.Center;
            Projectile.netUpdate = true;
        }

        // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
        private bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active || !owner.Slayer().CultistSoul)
                return false;
            else Projectile.timeLeft = 2;
            return true;
        }
    }
}