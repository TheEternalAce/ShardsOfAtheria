using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals.Elements;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Projectiles.Weapon.Melee.GenesisRagnarok.IceStuff;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee.GenesisRagnarok
{
    public class Genesis_Spear2 : ModProjectile
    {
        public int airTime = 0;
        public int airTimeMax = 15;

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
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<IceExplosion>(), Projectile.damage, Projectile.knockBack, player.whoAmI);
                }
            }
        }

        public override void SetStaticDefaults()
        {
            ProjectileElements.IceProj.Add(Type);
            ProjectileElements.FireProj.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 132;
            Projectile.height = 132;

            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.light = .4f;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            player.itemAnimation = 10;
            player.itemTime = 10;

            int newDirection = Projectile.Center.X > player.Center.X ? 1 : -1;
            player.ChangeDir(newDirection);
            Projectile.direction = newDirection;

            Projectile.rotation += 0.4f * (float)Projectile.direction;

            Projectile.ai[1]++;
            if (Projectile.ai[1] == 10)
            {
                SoundEngine.PlaySound(SoundID.Item71);
                Projectile.ai[1] = 0;
                if (player.HeldItem.type == ModContent.ItemType<GenesisAndRagnarok>())
                {
                    if ((player.HeldItem.ModItem as GenesisAndRagnarok).upgrades == 5)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Normalize((Projectile.position + Projectile.velocity) - Projectile.Center).RotatedBy(MathHelper.ToRadians(60 * i)) * 16,
                                ModContent.ProjectileType<IceShard>(), Projectile.damage, Projectile.knockBack, player.whoAmI);
                        }
                    }
                }
            }

            Projectile.ai[0]++;
            if (Projectile.ai[0] >= 15)
            {
                Projectile.velocity = Vector2.Normalize(player.Center - Projectile.Center) * 30;
            }

            if (Projectile.getRect().Intersects(player.getRect()) && Projectile.ai[0] >= 15)
                Projectile.Kill();

            if (player.HeldItem.type == ModContent.ItemType<GenesisAndRagnarok>())
            {
                if ((player.HeldItem.ModItem as GenesisAndRagnarok).upgrades >= 3)
                {
                    for (int num72 = 0; num72 < 2; num72++)
                    {
                        Dust obj4 = Main.dust[Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, (player.HeldItem.ModItem as GenesisAndRagnarok).upgrades < 5 ? DustID.Torch : DustID.Frost, 0f, 0f, 100, default,
                            (player.HeldItem.ModItem as GenesisAndRagnarok).upgrades < 5 ? 2f : .5f)];
                        obj4.noGravity = true;
                        obj4.velocity *= 2f;
                        obj4.velocity += Projectile.localAI[0].ToRotationVector2();
                        obj4.fadeIn = 1.5f;
                    }
                }
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            var player = Main.player[Projectile.owner];

            Vector2 mountedCenter = player.MountedCenter;

            var drawPosition = Main.MouseWorld;
            var remainingVectorToPlayer = mountedCenter - drawPosition;

            if (Projectile.alpha == 0)
            {
                int direction = -1;

                if (Main.MouseWorld.X < mountedCenter.X)
                    direction = 1;

                player.itemRotation = (float)Math.Atan2(remainingVectorToPlayer.Y * direction, remainingVectorToPlayer.X * direction);
            }
            lightColor = Color.White;
            return true;
        }
    }
}
