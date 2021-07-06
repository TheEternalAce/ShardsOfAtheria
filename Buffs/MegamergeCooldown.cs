using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Items.Accessories;

namespace SagesMania.Buffs
{
    public class MegamergeCooldown : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Megamerge Cooldown");
            Description.SetDefault("You cnannot megamerge for now");
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
		{
            player.buffImmune[ModContent.BuffType<Megamerged>()] = true;
        }
    }
}
