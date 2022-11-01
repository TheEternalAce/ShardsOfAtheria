using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.Pets;
using ShardsOfAtheria.Players;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Pets
{
    public class SmolNova : ModProjectile
	{
		public override void SetStaticDefaults()
        {
            Main.projPet[Projectile.type] = true;
			Main.projFrames[Type] = 10;
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.LilHarpy); // Copy the stats of the Lil' Harpy

            AIType = ProjectileID.LilHarpy; // Copy the AI of the Lil' Harpy.
        }

		public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];
			player.petFlagLilHarpy = false; // Relic from aiType
			return true;
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];

			if (!player.dead && player.HasBuff(ModContent.BuffType<NovaPetBuff>()))
			{
				Projectile.timeLeft = 2;
			}
		}
	}
}
