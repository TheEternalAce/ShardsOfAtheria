using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Items.Tools.ToggleItems;
using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Projectiles.Magic.EntropyCatalyst;
using ShardsOfAtheria.Projectiles.Ranged;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic.Gambit
{
    public class ElecCoin : ModProjectile
    {
        int gravityTimer = 0;

        public override void SetStaticDefaults()
        {
            Projectile.AddAreus(true);
            Projectile.AddDamageType(1);

            SoAGlobalProjectile.Metalic.Add(Type, 0.5f);
        }

        public override void SetDefaults()
        {
            Projectile.width = 10; // The width of projectile hitbox
            Projectile.height = 6; // The height of projectile hitbox
            Projectile.aiStyle = -1; // The ai style of the projectile, please reference the source code of Terraria
            Projectile.friendly = false;
            Projectile.DamageType = DamageClass.Magic; // Is the projectile shoot by a ranged weapon?
            Projectile.tileCollide = false; // Can the projectile collide with tiles?
        }

        public override void AI()
        {
            var player = Main.player[Projectile.owner];
            Projectile.rotation += MathHelper.ToRadians(32) * Projectile.direction;
            gravityTimer++;
            if (gravityTimer >= 16)
            {
                var devCard = ToggleableTool.GetInstance<DevelopersKeyCard>(player);
                bool cardActive = devCard != null && devCard.Active;
                if (cardActive) Projectile.velocity *= 0.8f;
                else Projectile.ApplyGravity(Projectile.ai[0] == 1 ? 0.2f : 1f);
                gravityTimer = 16;
                Projectile.friendly = true;
                if (Projectile.Center.Y > player.Center.Y)
                {
                    Projectile.tileCollide = true;
                }
                if (Projectile.Hitbox.Intersects(player.Hitbox))
                {
                    Projectile.Kill();
                    var coin = player.HeldItem.ModItem as AreusGambit;
                    coin?.SetAttack(player);
                }
                if (player.Distance(Projectile.Center) < 120) Projectile.Track(player.position, 16f, 4f);
            }
            foreach (var proj in Main.projectile)
            {
                if (proj.friendly &&
                    proj.owner == Projectile.owner &&
                    (proj.aiStyle == 1 || proj.aiStyle == 0) &&
                    proj.damage > 0 &&
                    proj.active)
                {
                    if (proj.Distance(Projectile.Center) <= 15)
                    {
                        if (ShardsHelpers.TryGetModContent("TerrariaMayQuake", "FeedbackerPunch", out ModProjectile arm) && proj.type == arm.Type)
                        {
                            gravityTimer = 0;
                            SoundEngine.PlaySound(SoundID.Tink, Projectile.Center);
                            break;
                        }
                        if (ShardsHelpers.TryGetModContent("Terrakill", "Feedbacker", out arm) && proj.type == arm.Type)
                        {
                            Projectile.velocity = Projectile.Center.DirectionTo(Main.MouseWorld) * 16f;
                            gravityTimer = 0;
                            SoundEngine.PlaySound(SoundID.Tink, Projectile.Center);
                            break;
                        }
                        if (proj.ModProjectile is not HitscanBullet)
                        {
                            float speed = proj.velocity.Length();
                            Vector2 velocity = proj.velocity;
                            float maxDistance = Main.maxTilesX * 16f;

                            Entity closestTarget = ShardsHelpers.FindClosestProjectile(Projectile.Center, maxDistance, proj => ValidTarget(proj, ModContent.ProjectileType<ElecCoin>()));
                            if (closestTarget != null) velocity = Projectile.Center.DirectionTo(closestTarget.Center) * speed;
                            else
                            {
                                closestTarget = ShardsHelpers.FindClosestProjectile(Projectile.Center, maxDistance, proj => ValidTarget(proj, ModContent.ProjectileType<CatalystBomb>()));
                                if (closestTarget != null && proj.ModProjectile is not CatalystBomb) velocity = Projectile.Center.DirectionTo(closestTarget.Center) * speed;
                                else
                                {
                                    closestTarget = ShardsHelpers.FindClosestNPC(Projectile, null, maxDistance);
                                    if (closestTarget != null) velocity = Projectile.Center.DirectionTo(closestTarget.Center) * speed;
                                }
                            }
                            Projectile.Kill();
                            proj.Kill();
                            if (Main.netMode == NetmodeID.MultiplayerClient)
                            {
                                Projectile.netUpdate = true;
                                proj.netUpdate = true;
                            }

                            int type = ModContent.ProjectileType<HitscanBullet>();
                            if (proj.DamageType.CountsAsClass(DamageClass.Melee)) type = ModContent.ProjectileType<HitscanBullet_Melee>();
                            if (proj.DamageType.CountsAsClass(DamageClass.Magic)) type = ModContent.ProjectileType<HitscanBullet_Magic>();
                            if (proj.DamageType.CountsAsClass(DamageClass.Summon)) type = ModContent.ProjectileType<HitscanBullet_Summon>();
                            if (proj.ModProjectile is CatalystBomb) type = ModContent.ProjectileType<HitscanBullet_Explosive>();

                            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, velocity, type, (int)(proj.damage * 1.1f), 3f);
                            player.Sinner().PrideCancelAttack();
                        }
                        break;
                    }
                }
            }
        }

        public bool ValidCoin(Projectile projectile)
        {
            if (!projectile.active) return false;
            if (projectile.type != ModContent.ProjectileType<ElecCoin>()) return false;
            if (projectile.whoAmI == Projectile.whoAmI) return false;
            if (projectile.owner != Projectile.owner) return false;
            return true;
        }

        public bool ValidTarget(Projectile projectile, int targetType)
        {
            if (!projectile.active) return false;
            if (projectile.type != targetType) return false;
            if (projectile.whoAmI == Projectile.whoAmI) return false;
            if (projectile.owner != Projectile.owner) return false;
            return true;
        }

        public override void OnKill(int timeLeft)
        {
            for (var i = 0; i < 28; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.Electric, speed * 2.4f);
                d.fadeIn = 1.3f;
                d.noGravity = true;
            }
        }
    }
}
