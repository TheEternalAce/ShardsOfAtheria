using Microsoft.Xna.Framework;
using ShardsOfAtheria.NPCs.Misc;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Summon.Minions.GemCore
{
    public class EmeraldShot : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.MinionShot[Type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 4;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 99;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.alpha = 255;
            Projectile.aiStyle = 0;
            Projectile.ignoreWater = true;
        }

        public override void OnSpawn(IEntitySource source)
        {
            SoundEngine.PlaySound(SoundID.Item72);
        }

        public override void AI()
        {
            Projectile.velocity.Normalize();
            Projectile.velocity *= 8;
            if (++Projectile.ai[2] >= 5)
            {
                int type = DustID.GemEmerald;
                Dust d = Dust.NewDustDirect(Projectile.Center, 0, 0, type);
                d.velocity *= 0;
                d.fadeIn = 1.3f;
                d.noGravity = true;
            }

            Vector2 endPos = new(Projectile.ai[0], Projectile.ai[1]);
            if (Projectile.Distance(endPos) <= 5)
            {
                Projectile.Kill();
            }
        }

        public override void OnKill(int timeLeft)
        {
            NPC.NewNPC(Projectile.GetSource_FromThis(), (int)Projectile.Center.X, (int)Projectile.Center.Y, ModContent.NPCType<EmeraldPlatform>());
            ShardsHelpers.DustRing(Projectile.Center, 3f, DustID.GemEmerald);
        }
    }
}
