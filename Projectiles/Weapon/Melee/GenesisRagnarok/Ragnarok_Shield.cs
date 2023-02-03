using Microsoft.Xna.Framework;
using MMZeroElements;
using ShardsOfAtheria.Buffs.Cooldowns;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Weapon.Melee.GenesisRagnarok
{
    public class Ragnarok_Shield : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileElements.Ice.Add(Type);
            ProjectileElements.Fire.Add(Type);
            ProjectileElements.Electric.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 64;
            Projectile.height = 56;

            Projectile.aiStyle = 75;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 1;
            Projectile.penetrate = -1;
            Projectile.light = .4f;
        }

        public override bool? CanCutTiles() => false;

        public override void AI()
        {
            var direction = Main.MouseWorld - Projectile.Center;
            Player player = Main.player[Projectile.owner];
            player.itemAnimation = 10;
            player.itemTime = 10;
            (player.HeldItem.ModItem as GenesisAndRagnarok).combo = 0;

            if (Main.myPlayer == Projectile.owner)
            {
                if (!Main.mouseRight || player.dead || Main.mouseLeft)
                    Projectile.Kill();
                Projectile.rotation = direction.ToRotation();

                int newDirection = Main.MouseWorld.X > player.Center.X ? 1 : -1;
                player.ChangeDir(newDirection);
                Projectile.direction = newDirection;
                Parry();
            }
        }

        public override void Kill(int timeLeft)
        {
            Player player = Main.player[Projectile.owner];
            Vector2 velocity = Vector2.Normalize(Main.MouseWorld - player.Center) * 30f;
            if (!player.dead && !Main.mouseLeft && Main.myPlayer == Projectile.owner)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), player.Center, velocity.RotatedByRandom(MathHelper.ToRadians(5)), ModContent.ProjectileType<RagnarokProj>(), Projectile.damage, Projectile.knockBack, player.whoAmI);
                SoundEngine.PlaySound(SoundID.Item1);
            }
        }

        public void Parry()
        {
            Rectangle hitbox = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height);
            Player player = Main.player[Projectile.owner];
            ShardsPlayer shardsPlayer = player.ShardsOfAtheria();
            int upgrades = shardsPlayer.genesisRagnarockUpgrades;

            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile reflProjectile = Main.projectile[i];
                if (hitbox.Intersects(reflProjectile.getRect()))
                {
                    if (SoAGlobalProjectile.ReflectAiList.Contains(reflProjectile.type))
                    {
                        if (reflProjectile.active && reflProjectile.velocity != Vector2.Zero && reflProjectile.hostile)
                        {
                            float damage = reflProjectile.damage;
                            int penetrate = reflProjectile.penetrate;
                            Vector2 velocity = -reflProjectile.velocity;
                            int extraUpdates = reflProjectile.extraUpdates;
                            float knockback = reflProjectile.knockBack;

                            Vector2 dir = Main.MouseWorld - reflProjectile.position;
                            dir.Normalize();
                            dir *= (Math.Abs(reflProjectile.velocity.X) + Math.Abs(reflProjectile.velocity.Y));
                            velocity = dir;
                            if (reflProjectile.hostile)
                            {
                                SoundEngine.PlaySound(SoundID.DD2_JavelinThrowersAttack, Projectile.Center);
                                SoundEngine.PlaySound(SoundID.DD2_DarkMageAttack, Projectile.Center);
                                reflProjectile.hostile = false;
                                reflProjectile.friendly = true;
                                reflProjectile.damage = (int)damage;
                                reflProjectile.penetrate = penetrate;
                                reflProjectile.velocity = velocity;
                                reflProjectile.extraUpdates = extraUpdates;
                                reflProjectile.knockBack = knockback;

                                player.immune = true;
                                player.immuneTime = 60;
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC parryNPC = Main.npc[i];
                if (hitbox.Intersects(parryNPC.getRect()) && !player.HasBuff(ModContent.BuffType<ParryCooldown>()) && !parryNPC.friendly && parryNPC.damage > 0)
                {
                    player.immune = true;
                    player.immuneTime = 60;
                    player.AddBuff(ModContent.BuffType<ParryCooldown>(), 300);
                    player.AddBuff(BuffID.ParryDamageBuff, 300);

                    if (upgrades < 5 && upgrades >= 3)
                    {
                        parryNPC.AddBuff(BuffID.OnFire, 600);
                    }
                    else if (upgrades == 5)
                    {
                        parryNPC.AddBuff(BuffID.Frostburn, 600);
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

            if (Projectile.alpha == 0 && Main.myPlayer == Projectile.owner)
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