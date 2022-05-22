using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.DecaEquipment;
using System;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Summon
{
    public class DecaShardProj : ModProjectile
    {
        public int aiTimer;
        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.DamageType = DamageClass.Summon;

            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 1;
            Projectile.penetrate = -1;

            DrawOffsetX = -12;
        }

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];

            if (!CheckActive(owner))
            {
                return;
            }

            Idle(owner);
            if (owner.GetModPlayer<DecaPlayer>().swarming)
                Swarming(owner);
        }

        // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
        private bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active || owner.ownedProjectileCounts[ModContent.ProjectileType<DecaShardProj>()] > owner.GetModPlayer<DecaPlayer>().decaShards
                || !owner.GetModPlayer<DecaPlayer>().modelDeca)
                return false;
            else Projectile.timeLeft = 2;
            return true;
        }

        private void Idle(Player owner)
        {
            Projectile.rotation = 0;
            Vector2 velocity = Vector2.Normalize(owner.Center - Projectile.Center);
            aiTimer++;
            if (aiTimer >= 60)
            {
                Projectile.velocity = velocity.RotatedByRandom(MathHelper.ToRadians(45)) * 2.5f;
                aiTimer = 0;
            }
            Projectile.friendly = false;
        }

        private void Swarming(Player owner)
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
            Vector2 velocity = Vector2.Normalize(Main.MouseWorld - Projectile.Center);
            aiTimer++;
            if (aiTimer >= 30)
            {
                Projectile.velocity = velocity.RotatedByRandom(MathHelper.ToRadians(15)) * 6f;
                aiTimer = 0;
            }
            Projectile.friendly = true;
        }
    }
}
