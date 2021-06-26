using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Projectiles.Minions
{
	public class SapphireSpiritMinion : HoverShooter
	{
		public override void SetStaticDefaults()
		{
			Main.projPet[projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
			ProjectileID.Sets.Homing[projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true; //This is necessary for right-click targeting
		}

		public override void SetDefaults()
		{
			projectile.netImportant = true;
			projectile.width = 32;
			projectile.height = 32;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.minionSlots = 1;
			projectile.penetrate = -1;
			projectile.timeLeft = 18000;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			Lighting.AddLight(projectile.Center, Color.White.ToVector3() * 0.78f);
			inertia = 20f;
			shoot = ModContent.ProjectileType<SapphireBolt>();
			shootSpeed = 16f;
		}

		public override void CheckActive()
		{
			Player player = Main.player[projectile.owner];
			SMPlayer modPlayer = player.GetModPlayer<SMPlayer>();
			if (player.dead)
			{
				modPlayer.sapphireMinion = false;
			}
			if (modPlayer.sapphireMinion)
			{ // Make sure you are resetting this bool in ModPlayer.ResetEffects. See ExamplePlayer.ResetEffects
				projectile.timeLeft = 2;
			}
		}
		/*
		public override void SelectFrame()
		{
			projectile.frameCounter++;
			if (projectile.frameCounter >= 8)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 3;
			}
		}
		*/
	}
}
