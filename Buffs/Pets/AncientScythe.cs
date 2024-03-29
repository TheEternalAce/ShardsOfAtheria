﻿using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Pets;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.Pets
{
    public class AncientScythe : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.vanityPet[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<Scythe>()] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Vector2 toOwner = Vector2.Normalize(player.Center + new Vector2(5, 0));
                Projectile.NewProjectile(player.GetSource_Buff(buffIndex), player.Center, toOwner * 5f, ModContent.ProjectileType<Scythe>(), 0, 0f, player.whoAmI);
            }
        }
    }
}
