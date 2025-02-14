using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerDebuff.SinDebuffs
{
    public class Embarassment : ModBuff
    {
        public override string Texture => SoA.DebuffTemplate;

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense -= 50;
            player.moveSpeed /= 2;
            player.GetDamage(DamageClass.Generic) -= 0.5f;
        }
    }
}
