using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.NPCs;
using ShardsOfAtheria.Projectiles.Other;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems.SoulCrystals
{
    public class PrimeSoulCrystal : SoulCrystal
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Crystal (Skeletron Prime)");
            Tooltip.SetDefault("While in combat enter a \"spin phase\" for 5 seconds every 30 seconds\n" +
                "This \"spin phase\" inceases defense and damage by 100% and damages nearby enemies\n" +
                "Attacks fire either a laser, rocket or grenade");
        }

        public override bool? UseItem(Player player)
        {
            absorbSoulTimer--;
            if (absorbSoulTimer == 0 || ModContent.GetInstance<ConfigClientSide>().instantAbsorb)
            {
                Main.LocalPlayer.GetModPlayer<SlayerPlayer>().PrimeSoul = true;
            }
            return base.UseItem(player);
        }
    }
}
