using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Items.Accessories;

namespace ShardsOfAtheria.Buffs.Cooldowns
{
    public class YamikoDashCooldown : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dash Cooldown");
            Description.SetDefault("You cannot use Yamiko's Dash ability for now");
            Main.debuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }
    }
}
