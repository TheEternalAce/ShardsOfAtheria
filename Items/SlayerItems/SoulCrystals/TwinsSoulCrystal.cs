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
    public class TwinsSoulCrystal : SoulCrystal
    {
        public static readonly string tip = "Projectiles inflict either Ichor or Cursed Inferno\n" +
                "Melee hits inflict both debuffs\n" +
                "Taking damage summons a shadow double of you to strike back with 100% of damage taken";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Crystal (The Twins)");
            Tooltip.SetDefault(tip);

            base.SetStaticDefaults();
        }
    }
}
