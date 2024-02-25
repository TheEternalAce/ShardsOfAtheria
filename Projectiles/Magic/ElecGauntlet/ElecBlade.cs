using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Melee;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic.ElecGauntlet
{
    public class ElecBlade : CoolSword
    {
        public override void SetStaticDefaults()
        {
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Projectile.width = Projectile.height = 30;
            swordReach = 160;
            rotationOffset = -MathHelper.PiOver4 * 3f;
            amountAllowedToHit = 3;
            Projectile.DamageType = DamageClass.Magic;
        }

        protected override void Initialize(Player player, ShardsPlayer shards)
        {
            base.Initialize(player, shards);
            if (shards.itemCombo > 0)
            {
                swingDirection *= -1;
            }
        }

        private ElecGauntletPlayer gplayer => Main.player[Projectile.owner].GetModPlayer<ElecGauntletPlayer>();
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
            var player = Main.player[Projectile.owner];
            int mana = 36;
            player.ManaEffect(mana);
            player.statMana += mana;
            if (player.statMana > player.statManaMax2)
            {
                player.statMana = player.statManaMax2;
            }
            freezeFrame = 6;
            target.AddBuff(BuffID.Electrified, 600);
            gplayer.AddType(Type);
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            gplayer.ModifyGauntletHit(ref modifiers, Type);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return SoA.ElectricColorA;
        }

        public override void AI()
        {
            base.AI();
            if (Main.player[Projectile.owner].itemAnimation <= 1)
            {
                Main.player[Projectile.owner].Shards().itemCombo = (ushort)(combo == 0 ? 20 : 0);
            }
            if (!playedSound && AnimProgress > 0.4f)
            {
                playedSound = true;
                SoundEngine.PlaySound(SoundID.Item1.WithPitchOffset(-1f), Projectile.Center);
            }
        }

        public override Vector2 GetOffsetVector(float progress)
        {
            return BaseAngleVector.RotatedBy((progress * (MathHelper.Pi * 1.5f) - MathHelper.PiOver2 * 1.5f) * -swingDirection * 1.1f);
        }

        public override float SwingProgress(float progress)
        {
            return GenericSwing2(progress);
        }

        public override float GetVisualOuter(float progress, float swingProgress)
        {
            if (progress > 0.8f)
            {
                float p = 1f - (1f - progress) / 0.2f;
                Projectile.alpha = (int)(p * 255);
                return -20f * p;
            }
            if (progress < 0.35f)
            {
                float p = 1f - progress / 0.35f;
                Projectile.alpha = (int)(p * 255);
                return -20f * p;
            }
            return 0f;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            DrawSwish();
            return SingleEdgeSwordDraw(lightColor);
        }
    }
}