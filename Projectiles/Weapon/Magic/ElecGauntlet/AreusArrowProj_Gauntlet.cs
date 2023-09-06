using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Projectiles.Weapon.Ammo;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Magic.ElecGauntlet
{
    public class AreusArrowProj_Gauntlet : AreusArrowProj
    {
        public override string Texture => "ShardsOfAtheria/Projectiles/Weapon/Ammo/AreusArrowProj";

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.DamageType = DamageClass.Magic;
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
