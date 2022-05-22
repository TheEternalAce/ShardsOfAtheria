using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs
{
    public class TrueCreeperShield : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Creeper Shield");
            Description.SetDefault("Immune to damage and 50% reduced damage");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) -= .5f;
        }
    }
}
