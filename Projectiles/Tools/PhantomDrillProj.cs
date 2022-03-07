using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Tools
{
    public class PhantomDrillProj : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Phantom Drill");
        }

        public override void SetDefaults()
        {
			Projectile.width = 30;
			Projectile.height = 64;
			Projectile.aiStyle = 20;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.hide = true; //aiStyle 20 assigns heldProj
			Projectile.ownerHitCheck = true;
			Projectile.DamageType = DamageClass.Melee;
		}
	}
}