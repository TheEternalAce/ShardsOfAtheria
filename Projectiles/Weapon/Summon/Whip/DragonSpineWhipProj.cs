using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Summon.Whip
{
    public class DragonSpineWhipProj : WhipProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Spine Whip");
            ProjectileID.Sets.IsAWhip[Type] = true;
        }
        public override void WhipDefaults()
        {
            originalColor = Color.Purple;
            whipRangeMultiplier = 1f;
            fallOff = 0.15f;
            tag = BuffID.SwordWhipPlayerBuff;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<LoomingEntropy>(), 60);
            base.OnHitNPC(target, damage, knockback, crit);
            Player player = Main.player[Projectile.owner];
            player.AddBuff(BuffID.SwordWhipPlayerBuff, 180);
        }
    }
}
