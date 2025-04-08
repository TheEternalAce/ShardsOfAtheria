using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Buffs.NPCDebuff;
using ShardsOfAtheria.Common.Projectiles;
using ShardsOfAtheria.Dusts;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Ranged.PlagueRail
{
    public class PlagueBeam : BasicBeam
    {
        public override string Texture => SoA.BlankTexture;

        public override int DustType => ModContent.DustType<PlagueDust>();

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.PlayerHurtDamageIgnoresDifficultyScaling[Type] = true;
            Projectile.AddDamageType(6, 8);
            Projectile.AddElement(3);
            Projectile.AddRedemptionElement(11);
        }

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 4;
            Projectile.timeLeft = 600;
            Projectile.extraUpdates = 99;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.alpha = 255;
            Projectile.aiStyle = 0;
            Projectile.ignoreWater = true;
            dustFadeIn = 1.3f;
            dustDelay = 11;
        }

        public override void OnSpawn(IEntitySource source)
        {
            SoundEngine.PlaySound(SoundID.Item72, Projectile.Center);
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (target.HasBuff<PlagueMark>())
            {
                modifiers.Defense -= 0.1f;
                modifiers.ScalingBonusDamage += 0.1f;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff<Plague>(300);
            var player = Projectile.GetPlayerOwner();
            if (player.HasBuff<Plague>()) player.Heal(40);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff<Plague>(300);
            var player = Projectile.GetPlayerOwner();
            if (player.HasBuff<Plague>()) player.Heal(80);
        }

        public override void AI()
        {
            base.AI();
            if (dustTimer > dustDelay && !Projectile.hostile) Projectile.hostile = true;
        }
    }
}
