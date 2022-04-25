using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Items.SlayerItems.SoulCrystals;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Tools
{
    public class AllSeeingEye : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("All Seeing Eye");
        }

        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 26;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
			Projectile.alpha = 100;
        }

        public override void AI()
        {
            float maxDetectRadius = 24f;
            Player owner = Main.LocalPlayer;

			if (!CheckActive(owner))
			{
				return;
			}

			Projectile.Center = Main.MouseWorld;
			Lighting.AddLight(Projectile.Center, TorchID.White);

			// Trying to find NPC closest to the projectile
			NPC closestNPC = FindClosestNPC(maxDetectRadius);
			if (closestNPC == null)
				return;

			if (Projectile.getRect().Intersects(closestNPC.getRect()))
			{
				closestNPC.AddBuff(ModContent.BuffType<Marked>(), 600);
			}
		}

		// This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
		private bool CheckActive(Player owner)
		{
			if (owner.dead || !owner.active || owner.GetModPlayer<SlayerPlayer>().EyeSoul == SoulCrystalStatus.None)
				return false;
			else Projectile.timeLeft = 2;
			return true;
		}

		// Finding the closest NPC to attack within maxDetectDistance range
		// If not found then returns null
		public NPC FindClosestNPC(float maxDetectDistance)
		{
			NPC closestNPC = null;

			// Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
			float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

			// Loop through all NPCs(max always 200)
			for (int k = 0; k < Main.maxNPCs; k++)
			{
				NPC target = Main.npc[k];
				// Check if NPC able to be targeted. It means that NPC is
				// 1. active (alive)
				// 2. chaseable (e.g. not a cultist archer)
				// 3. max life bigger than 5 (e.g. not a critter)
				// 4. can take damage (e.g. moonlord core after all it's parts are downed)
				// 5. hostile (!friendly)
				// 6. not immortal (e.g. not a target dummy)
				if (target.CanBeChasedBy())
				{
					// The DistanceSquared function returns a squared distance between 2 points, skipping relatively expensive square root calculations
					float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);

					// Check if it is within the radius
					if (sqrDistanceToTarget < sqrMaxDetectDistance)
					{
						sqrMaxDetectDistance = sqrDistanceToTarget;
						closestNPC = target;
					}
				}
			}

			return closestNPC;
		}
	}
}
