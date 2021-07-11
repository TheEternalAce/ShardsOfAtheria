using Terraria;
using Terraria.ModLoader;

namespace SagesMania.Buffs
{
    public class BoneStrength : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Bone Strength");
            Description.SetDefault("10% increased damage and recuced damage taken by 20%\n" +
                "Your bones are stronger");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.allDamage += .1f;
            player.endurance += .2f;
        }
    }
}
