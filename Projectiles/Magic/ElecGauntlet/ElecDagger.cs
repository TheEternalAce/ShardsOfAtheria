using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic.ElecGauntlet
{
    public class ElecDagger : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(10); // This sets width and height to the same value (important when projectiles can rotate)
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.extraUpdates = 1; // Update 1+extraUpdates times per tick
            Projectile.timeLeft = 360; // This value does not matter since we manually kill it earlier, it just has to be higher than the duration we use in AI
        }

        public override void AI()
        {
            Projectile.spriteDirection = Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
            Projectile.rotation = Projectile.velocity.ToRotation() +
                (Projectile.spriteDirection == 1 ? MathHelper.ToRadians(45f) : MathHelper.ToRadians(135f));
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            if (Projectile.spriteDirection == 1)
            {
                DrawOffsetX = -12;
                DrawOriginOffsetX = 6;
            }
            else
            {
                DrawOffsetX = 0;
                DrawOriginOffsetX = -6;
            }
        }

        private ElecGauntletPlayer gplayer => Main.player[Projectile.owner].GetModPlayer<ElecGauntletPlayer>();
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff<ElectricShock>(600);
            gplayer.AddType(Type);
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            gplayer.ModifyGauntletHit(ref modifiers, Type);
        }
    }
}
