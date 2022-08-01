using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Projectiles.Weapon.Melee.GenesisRagnarok.IceStuff;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee.GenesisRagnarok
{
    public class RagnarokProj : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;

            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.light = .4f;

            DrawOffsetX = -4;
            DrawOriginOffsetY = -4;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Projectile.owner];

            if (player.HeldItem.type == ModContent.ItemType<GenesisAndRagnarok>())
            {
                if ((player.HeldItem.ModItem as GenesisAndRagnarok).upgrades < 5 && (player.HeldItem.ModItem as GenesisAndRagnarok).upgrades >= 3)
                    target.AddBuff(BuffID.OnFire, 600);
                else if ((player.HeldItem.ModItem as GenesisAndRagnarok).upgrades == 5)
                {
                    target.AddBuff(BuffID.Frostburn, 600);
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<IceVortexShard>(), Projectile.damage, Projectile.knockBack, player.whoAmI);
                }
            }
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            player.itemAnimation = 10;
            player.itemTime = 10;

            int newDirection = Projectile.Center.X > player.Center.X ? 1 : -1;
            player.ChangeDir(newDirection);
            Projectile.direction = newDirection;

            Projectile.ai[1]++;
            if (Projectile.ai[1] >= 20)
                Projectile.velocity = Vector2.Normalize(player.Center - Projectile.Center) *30;

            if (player.HeldItem.type == ModContent.ItemType<GenesisAndRagnarok>())
            {
                (player.HeldItem.ModItem as GenesisAndRagnarok).combo = 0;

                for (int num72 = 0; num72 < 2; num72++)
                {
                    Dust obj4 = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, (player.HeldItem.ModItem as GenesisAndRagnarok).upgrades < 5 ? DustID.Torch : DustID.Frost, 0f, 0f, 100, default,
                        (player.HeldItem.ModItem as GenesisAndRagnarok).upgrades < 5 ? 2f : .5f)];
                    obj4.noGravity = true;
                    obj4.velocity *= 2f;
                    obj4.velocity += Projectile.localAI[0].ToRotationVector2();
                    obj4.fadeIn = 1.5f;
                }

                if (Projectile.getRect().Intersects(player.getRect()) && Projectile.ai[1] > 20 || player.dead || (Main.mouseLeft && (Main.LocalPlayer.HeldItem.ModItem as GenesisAndRagnarok).upgrades >= 2))
                    Projectile.Kill();
            }
        }

        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            if (player.HeldItem.type == ModContent.ItemType<GenesisAndRagnarok>())
            {
                if (!player.dead && Main.mouseLeft && Main.myPlayer == Projectile.owner && player.ownedProjectileCounts[ModContent.ProjectileType<RagnarokProj2>()] == 0 && (Main.LocalPlayer.HeldItem.ModItem as GenesisAndRagnarok).upgrades >= 2)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<RagnarokProj2>(), Projectile.damage, Projectile.knockBack, player.whoAmI);
                    SoundEngine.PlaySound(SoundID.Item71);
                }
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}
