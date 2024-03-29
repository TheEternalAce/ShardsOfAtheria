﻿using ShardsOfAtheria.Projectiles.Summon.Minions;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.Summons
{
    public class BloodySigil : ModBuff
    {
        public override string Texture => SoA.BuffTemplate;

        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
            Main.buffNoTimeDisplay[Type] = true; // The time remaining won't display on this buff
        }

        public override void Update(Player player, ref int buffIndex)
        {
            // If the minions exist reset the buff time, otherwise remove the buff from the player
            if (player.ownedProjectileCounts[ModContent.ProjectileType<BloodSigil>()] > 0 || player.ownedProjectileCounts[ModContent.ProjectileType<AreusMirrorShard>()] > 0)
            {
                player.buffTime[buffIndex] = 18000;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}
