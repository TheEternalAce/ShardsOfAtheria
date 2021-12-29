using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Pets
{
    public class PetCreeper : ModProjectile
	{
		protected float idleAccel = 0.05f;
		protected float spacingMult = 1f;
		protected float viewDist = 400f;
		protected float chaseDist = 200f;
		protected float chaseAccel = 6f;
		protected float inertia = 40f;

		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Creeper");
            Main.projPet[Projectile.type] = true;

        }

        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 34;
			Projectile.CloneDefaults(ProjectileID.BabyHornet);
			AIType = ProjectileID.BabyHornet;
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
			SMPlayer modPlayer = player.GetModPlayer<SMPlayer>();
			if (player.dead)
			{
				modPlayer.creeperPet = false;
			}
			if (modPlayer.creeperPet)
			{
				Projectile.timeLeft = 2;
			}
		}
	}
}
