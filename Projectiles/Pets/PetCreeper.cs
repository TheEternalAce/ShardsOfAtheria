using Microsoft.Xna.Framework;
using ShardsOfAtheria.Players;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Pets
{
    public class PetCreeper : ModProjectile
	{
		public override void SetStaticDefaults()
        {
            Main.projPet[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 34;
			Projectile.aiStyle = -1;
			Projectile.tileCollide = false;
		}

		public override bool PreAI()
		{
			Player player = Main.player[Projectile.owner];
			player.hornet = false; // Relic from aiType
			return true;
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			SlayerPlayer slayer = player.GetModPlayer<SlayerPlayer>();

			if (++Projectile.ai[1] >= 30)
			{
				Projectile.velocity = Vector2.Normalize(player.Center - Projectile.Center) * 5f;
				Projectile.ai[1] = 0;
			}

			if (player.dead)
			{
				slayer.creeperPet = false;
			}
			if (slayer.creeperPet)
			{
				Projectile.timeLeft = 2;
			}
		}
	}
}
