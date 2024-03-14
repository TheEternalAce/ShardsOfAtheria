using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace ShardsOfAtheria.Projectiles.Magic.AreusGamble
{
    public class AreusGambleGoodExplosion : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
            Projectile.AddRedemptionElement(15);
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ModContent.ProjectileType<ElementExplosion>());
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 20;
        }

        public override void AI()
        {
            var player = Projectile.GetPlayerOwner();
            if (Projectile.ai[0] == 0)
            {
                if (player.IsLocal())
                {
                    Projectile.width = Main.screenWidth;
                    Projectile.height = Main.screenHeight;
                }
                Projectile.ai[0] = 1;
            }
            Projectile.Center = player.Center;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Electrified, 300);
            var player = Projectile.GetPlayerOwner();
            int manaGain = player.statManaMax2 / 10;
            player.ManaEffect(manaGain);
            player.statMana += manaGain;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Electrified, 120);
            int manaLoss = -target.statManaMax2 / 10;
            if (target.statMana < Math.Abs(manaLoss))
            {
                manaLoss = -target.statMana;
            }
            target.ManaEffect(manaLoss);
            target.statMana += manaLoss;
        }
    }
}
