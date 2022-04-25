using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee
{
    public class RagnarokProj2 : ModProjectile
    {
		public double rotation;
        private const string ChainTexturePath = "ShardsOfAtheria/Projectiles/Weapon/Melee/RagnarokProj2_GenesisChain";
        public override void SetDefaults()
        {
            Projectile.width = 60;
            Projectile.height = 60;

            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.light = .4f;

            DrawOffsetX = -5;
            DrawOriginOffsetY = -5;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

			int mouseDirection = Main.MouseWorld.X > player.Center.X ? 1 : -1;
			if (mouseDirection == -1)
				rotation += .33;
			else rotation -= .33;
			Projectile.Center = player.Center + Vector2.One.RotatedBy(rotation) * 180;
			player.itemAnimation = 10;

			int newDirection = Projectile.Center.X > player.Center.X ? 1 : -1;
			player.ChangeDir(newDirection);
			Projectile.direction = newDirection;
			if (!Main.mouseLeft || player.dead || !player.active)
				Projectile.Kill();
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 600);
		}

		public override bool PreDraw(ref Color lightColor)
		{
			var player = Main.player[Projectile.owner];

			Vector2 mountedCenter = player.MountedCenter;
            Asset<Texture2D> chainTexture = ModContent.Request<Texture2D>(ChainTexturePath);

			var drawPosition = Projectile.Center;
			var remainingVectorToPlayer = mountedCenter - drawPosition;

			float rotation = remainingVectorToPlayer.ToRotation() - MathHelper.PiOver2;

			if (Projectile.alpha == 0)
			{
				int direction = -1;

				if (Projectile.Center.X < mountedCenter.X)
					direction = 1;

				player.itemRotation = (float)Math.Atan2(remainingVectorToPlayer.Y * direction, remainingVectorToPlayer.X * direction);
			}

			// This while loop draws the chain texture from the projectile to the player, looping to draw the chain texture along the path
			while (true)
			{
				float length = remainingVectorToPlayer.Length();

				// Once the remaining length is small enough, we terminate the loop
				if (length < 25f || float.IsNaN(length))
					break;

				// drawPosition is advanced along the vector back to the player by 12 pixels
				// 12 comes from the height of ExampleFlailProjectileChain.png and the spacing that we desired between links
				drawPosition += remainingVectorToPlayer * 14 / length;
				remainingVectorToPlayer = mountedCenter - drawPosition;

				// Finally, we draw the texture at the coordinates using the lighting information of the tile coordinates of the chain section
				Color color = Lighting.GetColor((int)drawPosition.X / 16, (int)(drawPosition.Y / 16f));
				Main.spriteBatch.Draw(chainTexture.Value, drawPosition - Main.screenPosition, null, color, rotation, chainTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
			}

			return true;
		}
	}
}
