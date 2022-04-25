using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs
{
    public class CreeperShield : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Creeper Shield");
            Description.SetDefault("Immune to damage, cannot attack and summons severely weakened");
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
