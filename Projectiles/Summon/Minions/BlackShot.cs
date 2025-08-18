using ShardsOfAtheria.Buffs.PlayerBuff.AreusArmor;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Summon.Minions
{
    public class BlackShot : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(5, 6);
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
            Projectile.AddRedemptionElement(9);
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 4;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 20;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.alpha = 255;
            Projectile.aiStyle = 0;
            Projectile.ignoreWater = true;
            Projectile.arrow = true;
        }

        public override void OnSpawn(IEntitySource source)
        {
            SoundEngine.PlaySound(SoundID.Item72, Projectile.Center);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            var player = Projectile.GetPlayerOwner();
            if (!player.HasBuff<ShadeState>())
            {
                var areus = player.Areus();
                areus.imperialVoid -= 3;
            }
            target.AddBuff(BuffID.Electrified, 600);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Electrified, 600);
        }

        public override void AI()
        {
            Projectile.velocity.Normalize();
            Projectile.velocity *= 8;
            if (++Projectile.ai[0] >= 5)
            {
                int type = DustID.ShadowbeamStaff;
                if (Main.rand.NextBool(3))
                {
                    type = DustID.Electric;
                }
                Dust d = Dust.NewDustDirect(Projectile.Center, 0, 0, type);
                d.velocity *= 0;
                d.fadeIn = 1.3f;
                d.noGravity = true;
            }

            float maxDetectRange = 400;
            int npcWhoAmI = Projectile.FindTargetWithLineOfSight(maxDetectRange);
            if (npcWhoAmI != -1)
            {
                Projectile.Track(Main.npc[npcWhoAmI], inertia: 8f);
            }
        }
    }
}
