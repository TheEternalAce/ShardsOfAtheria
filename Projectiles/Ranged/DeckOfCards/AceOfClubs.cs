using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Ranged.DeckOfCards
{
    public class AceOfClubs : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddElement(3);
            Projectile.AddRedemptionElement(10);
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.DamageType = DamageClass.Ranged;
            if (SoA.ServerConfig.throwingWeapons) Projectile.DamageType = DamageClass.Throwing;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.timeLeft = 300;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];
            for (int i = 0; i < player.CountBuffs(); i++)
            {
                if (!Main.debuff[player.buffType[i]] && !BuffID.Sets.TimeLeftDoesNotDecrease[player.buffType[i]])
                {
                    player.buffTime[i] += 60;
                }
            }
            base.OnHitNPC(target, hit, damageDone);
        }
    }

    class ClubsPlayer : ModPlayer
    {
        public int clubsCardCooldown;
        public static int ClubsCardCooldownMax = 300;

        public override void ResetEffects()
        {
            if (clubsCardCooldown > 0)
            {
                clubsCardCooldown--;
            }
        }
    }
}
