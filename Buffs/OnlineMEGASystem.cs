using Terraria;
using Terraria.ModLoader;

namespace SagesMania.Buffs
{
    public class OnlineMEGASystem : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("M.E.G.A. System");
            Description.SetDefault("The power of the Living Metal is yours\n" +
                "''Biolink established! M.E.G.A. System online!''");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            SMPlayer p = player.GetModPlayer<SMPlayer>();

            // We use blockyAccessoryPrevious here instead of blockyAccessory because UpdateBuffs happens before UpdateEquips but after ResetEffects.
            if (p.livingMetalArmorPrevious)
            {
                p.livingMetalArmor = true;
                player.extraFall += 45;
            }
        }
    }
}
