using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Weapons.Melee;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee.GenesisRagnarok
{
    public class RagnarokProj : ModProjectile
    {
        public int airTime = 0;
        public int airTimeMax = 20;
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
            if (player.HeldItem.type == ModContent.ItemType<GenesisAndRagnarok>())
                (player.HeldItem.ModItem as GenesisAndRagnarok).combo = 0;

            int newDirection = Projectile.Center.X > player.Center.X ? 1 : -1;
            player.ChangeDir(newDirection);
            Projectile.direction = newDirection;

            airTime++;
            if (airTime >= airTimeMax)
                Projectile.velocity = Vector2.Normalize(player.Center - Projectile.Center) *30;

            for (int num72 = 0; num72 < 2; num72++)
            {
                Dust obj4 = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 2f)];
                obj4.noGravity = true;
                obj4.velocity *= 2f;
                obj4.velocity += Projectile.localAI[0].ToRotationVector2();
                obj4.fadeIn = 1.5f;
            }

            if (Projectile.getRect().Intersects(player.getRect()) && airTime > airTimeMax || player.dead || Main.mouseLeft)
                Projectile.Kill();
        }

        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            if (!player.dead && Main.mouseLeft && Main.myPlayer == Projectile.owner && player.ownedProjectileCounts[ModContent.ProjectileType<RagnarokProj2>()] == 0)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<RagnarokProj2>(), player.HeldItem.damage, player.HeldItem.knockBack, player.whoAmI);
                SoundEngine.PlaySound(SoundID.Item71);
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 600);
        }
    }
}
