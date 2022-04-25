using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Items.SlayerItems.SoulCrystals;
using ShardsOfAtheria.Projectiles.NPCProj;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Minions
{
    public class TheDestroyersProbeAttack : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Destroyer's Probe");
        }

        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.aiStyle = 66;    
            AIType = ProjectileID.Retanimini;
            Projectile.timeLeft = 18000;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (!CheckActive(player))
            {
                Projectile.Kill();
            }

            Projectile.rotation += MathHelper.ToRadians(180);
        }

        // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
        private bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active || owner.GetModPlayer<SlayerPlayer>().DestroyerSoul == SoulCrystalStatus.None)
                return false;
            return true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
    }
}
