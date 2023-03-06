using Microsoft.Xna.Framework;
using MMZeroElements;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Throwing.DeckOfCards
{
    public class AceOfClubs : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileElements.Electric.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            ProjectileElements.Fire.Remove(Type);
            ProjectileElements.Ice.Remove(Type);
            ProjectileElements.Metal.Remove(Type);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            for (int i = 0; i < player.CountBuffs(); i++)
            {
                if (!Main.debuff[player.buffType[i]] && !BuffID.Sets.TimeLeftDoesNotDecrease[player.buffType[i]])
                {
                    player.buffTime[i] += 60;
                }
            }
            base.OnHitNPC(target, damage, knockback, crit);
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
