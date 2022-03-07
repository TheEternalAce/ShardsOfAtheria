using ShardsOfAtheria.Items;
using Terraria;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ShardsOfAtheria.Buffs
{
    public class MildRadiationPoisoning : ModBuff
    {
        public override string Texture => "ShardsOfAtheria/Buffs/ViralInfection";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mild Radiation Poisoning");
            DisplayName.SetDefault("Lost 25% max life and losing life");
            Main.debuff[Type] = true;
        }
    }

    public class ModerateRadiationPoisoning : ModBuff
    {
        public override string Texture => "ShardsOfAtheria/Buffs/ViralInfection";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Moderate Radiation Poisoning");
            DisplayName.SetDefault("Lost 50% max life and losing life");
            Main.debuff[Type] = true;
        }
    }

    public class SevereRadiationPoisoning : ModBuff
    {
        public override string Texture => "ShardsOfAtheria/Buffs/ViralInfection";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Severe Radiation Poisoning");
            DisplayName.SetDefault("Lost 75% max life and losing life");
            Main.debuff[Type] = true;
        }
    }

    public class RadiationPoisoningPlayer : ModPlayer
    {
        public override void PostUpdate()
        {
            if (Player.HasBuff<MildRadiationPoisoning>())
                Player.statLifeMax2 -= Player.statLifeMax2 * 1/4;
            if (Player.HasBuff<ModerateRadiationPoisoning>())
                Player.statLifeMax2 -= Player.statLifeMax2 * 1/2;
            if (Player.HasBuff<SevereRadiationPoisoning>())
                Player.statLifeMax2 -= Player.statLifeMax2 * 3/4;
            if (Player.statLife > Player.statLifeMax2)
                Player.statLife = Player.statLifeMax2;
        }

        public override void UpdateBadLifeRegen()
        {
            if (Player.HasBuff<MildRadiationPoisoning>())
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes .5 life lost per second.
                Player.lifeRegen -= 4;
            }
            if (Player.HasBuff<ModerateRadiationPoisoning>())
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes .5 life lost per second.
                Player.lifeRegen -= 8;
            }
            if (Player.HasBuff<SevereRadiationPoisoning>())
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes .5 life lost per second.
                Player.lifeRegen -= 16;
            }
        }
    }
}
