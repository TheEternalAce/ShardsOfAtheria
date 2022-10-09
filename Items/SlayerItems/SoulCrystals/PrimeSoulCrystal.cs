using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.NPCs;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Other;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems.SoulCrystals
{
    public class PrimeSoulCrystal : SoulCrystal
    {
        public static readonly string tip = "While in combat enter a \"spin phase\" for 5 seconds every 30 seconds\n" +
                "This \"spin phase\" inceases defense and damage by 100% and damages nearby enemies\n" +
                "Attacks fire either a laser, rocket or grenade";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Crystal (Skeletron Prime)");
            Tooltip.SetDefault(tip);

            base.SetStaticDefaults();
        }
    }
}
