using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Projectiles.Minions
{
	public class HoneybeeMinion : HoverShooter
	{
		public override void SetStaticDefaults()
		{
			Main.projPet[projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
			ProjectileID.Sets.Homing[projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true; //This is necessary for right-click targeting
			Main.projFrames[projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			projectile.netImportant = true;
			projectile.width = 16;
			projectile.height = 18;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 18000;
			projectile.tileCollide = true;
			projectile.ignoreWater = false;
			Lighting.AddLight(projectile.Center, Color.White.ToVector3() * 0.78f);
			inertia = 20f;
			shoot = ProjectileID.Bullet;
			shootSpeed = 16f;
		}

		public override void CheckActive()
		{
			Player player = Main.player[projectile.owner];
			SMPlayer modPlayer = player.GetModPlayer<SMPlayer>();
			if (player.dead)
			{
				modPlayer.honeybeeMinion = false;
			}
			if (modPlayer.honeybeeMinion)
			{ // Make sure you are resetting this bool in ModPlayer.ResetEffects. See ExamplePlayer.ResetEffects
				projectile.timeLeft = 2;
			}
		}
		
		public override void SelectFrame()
		{
			projectile.frameCounter++;
			if (projectile.frameCounter >= 8)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 4;
			}
		}
	}
}
