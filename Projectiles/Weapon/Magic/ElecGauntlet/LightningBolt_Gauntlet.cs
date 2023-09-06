using ShardsOfAtheria.Items.Weapons.Magic;
using Terraria;

namespace ShardsOfAtheria.Projectiles.Weapon.Magic.ElecGauntlet
{
    public class LightningBolt_Gauntlet : LightningBoltFriendly
    {
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
