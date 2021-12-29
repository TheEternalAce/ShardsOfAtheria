using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs
{
    public class BoneStrength : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bone Strength");
            Description.SetDefault("10% increased damage and recuced damage taken by 20%\n" +
                "Your bones are stronger");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) += .1f;
            player.endurance += .2f;
        }
    }
}
