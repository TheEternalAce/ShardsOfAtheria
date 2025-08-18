using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Projectiles.Ammo;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic.ElecGauntlet
{
    public class ElectricArrow_Gauntlet : ElectricArrow
    {
        public override string Texture => "ShardsOfAtheria/Projectiles/Ammo/ElectricArrow";

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.DamageType = DamageClass.Magic;
        }

        public override void OnKill(int timeLeft)
        {
            if (Main.myPlayer == Projectile.owner && Projectile.ai[0] == 0)
            {
                Player player = Main.player[Projectile.owner];
                SoundEngine.PlaySound(SoundID.Item74, Projectile.Center);

                int damage = Projectile.damage;
                damage /= 3;
                float offset = MathHelper.ToRadians(Main.rand.NextFloat(60f));
                for (int i = 0; i < 6; i++)
                {
                    var velocity = new Vector2(1, 0).RotatedBy(MathHelper.ToRadians(60f * i) + offset) * 16f;
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, velocity,
                        ModContent.ProjectileType<ElectricArrow_Gauntlet>(), damage, Projectile.knockBack, player.whoAmI);
                    Projectile.netUpdate = true;
                }
            }
        }

        private ElecGauntletPlayer gplayer => Main.player[Projectile.owner].GetModPlayer<ElecGauntletPlayer>();
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
            gplayer.AddType(Type);
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            gplayer.ModifyGauntletHit(ref modifiers, Type);
        }
    }
}
