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
        public override void SetStaticDefaults()
        {
            Projectile.AddDamageType(5);
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(10);
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 360;
        }

        public override void AI()
        {
            Projectile.SetVisualOffsets(new Vector2(22, 28));
            Projectile.spriteDirection = Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
            Projectile.rotation = Projectile.velocity.ToRotation() +
                (Projectile.spriteDirection == 1 ? MathHelper.ToRadians(45f) : MathHelper.ToRadians(135f));
        }

        private ElecGauntletPlayer gplayer => Main.player[Projectile.owner].GetModPlayer<ElecGauntletPlayer>();
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff<ElectricShock>(300);
            gplayer.AddType(Type);
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            gplayer.ModifyGauntletHit(ref modifiers, Type);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = SoA.ElectricColorA0;
            return base.PreDraw(ref lightColor);
        }
    }
}
