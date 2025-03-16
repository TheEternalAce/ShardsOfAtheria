using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Summon.Minions.Star
{
    public class RedStarShot : ModProjectile
    {
        public override string Texture => SoA.BlankTexture;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
            Projectile.AddDamageType(5);
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
            if (Main.hardMode) Projectile.tileCollide = false;
        }

        public override void OnSpawn(IEntitySource source)
        {
            SoundEngine.PlaySound(SoundID.Item72, Projectile.Center);
        }

        public override void AI()
        {
            Projectile.velocity.Normalize();
            Projectile.velocity *= 8;

            int type = DustID.GemAmethyst;
            if (Main.rand.NextBool(3))
            {
                type = DustID.GemRuby;
            }
            Dust d = Dust.NewDustDirect(Projectile.Center, 0, 0, type);
            d.velocity *= 0;
            d.noGravity = true;

            float maxDetectRange = 400;
            int npcWhoAmI = Projectile.FindTargetWithLineOfSight(maxDetectRange);
            var player = Projectile.GetPlayerOwner();
            if (player.HasMinionAttackTargetNPC) npcWhoAmI = player.MinionAttackTargetNPC;
            if (npcWhoAmI != -1)
            {
                Projectile.Track(Main.npc[npcWhoAmI], inertia: 8f);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.owner == Main.myPlayer && Main.hardMode)
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, Type + 1, (int)(Projectile.damage * 0.75f), Projectile.knockBack);
        }
    }
}
