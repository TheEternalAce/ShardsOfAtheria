using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Projectiles.Pets
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
            Main.projPet[projectile.type] = true;

        }

        public override void SetDefaults()
        {
            projectile.width = 34;
            projectile.height = 34;
			projectile.CloneDefaults(ProjectileID.BabyHornet);
			aiType = ProjectileID.BabyHornet;
		}

		public override bool PreAI()
		{
			Player player = Main.player[projectile.owner];
			player.hornet = false; // Relic from aiType
			return true;
		}

		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			SMPlayer modPlayer = player.GetModPlayer<SMPlayer>();
			if (player.dead)
			{
				modPlayer.creeperPet = false;
			}
			if (modPlayer.creeperPet)
			{
				projectile.timeLeft = 2;
			}
		}
	}
}
