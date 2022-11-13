using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.Summons
{
    public class CreeperShield : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Summon) -= .75f;

            if (player.HeldItem.pick > 0 || player.HeldItem.axe > 0 || player.HeldItem.hammer > 0)
                player.HeldItem.damage = 0;
        }
    }
}
