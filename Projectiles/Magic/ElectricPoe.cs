using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Weapons.Magic;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Magic
{
    public class ElectricPoe : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
            Projectile.AddElement(0);
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(1);
            Projectile.AddRedemptionElement(2);
            Projectile.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1;
        }

        public override void OnSpawn(IEntitySource source)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath52, Projectile.Center);
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(Projectile.position, 10, 12, DustID.Electric);
                Dust.NewDust(Projectile.position, 10, 12, DustID.Shadowflame);
            }
        }

        public override void AI()
        {
            var player = Projectile.GetPlayerOwner();
            Projectile.velocity *= 0.85f;
            if (player.HasItem<AreusGhostLantern>())
            {
                float pullRange = 50;
                if (player.HeldItem.type == ModContent.ItemType<AreusGhostLantern>()) pullRange += 50;
                if (player.Overdrive()) pullRange += 150;
                if (Projectile.Distance(player.Center) < pullRange)
                {
                    var vectorToPlayer = player.Center - Projectile.Center;
                    ShardsHelpers.AdjustMagnitude(ref vectorToPlayer, 14f);
                    Projectile.velocity = (4 * Projectile.velocity + vectorToPlayer) / 2f;
                    ShardsHelpers.AdjustMagnitude(ref Projectile.velocity, 14f);
                }
                if (Projectile.Distance(player.Center) < 35)
                {
                    Projectile.Kill();
                    int inventoryIndex = player.FindItem(ModContent.ItemType<AreusGhostLantern>());
                    var lantern = player.inventory[inventoryIndex].ModItem as AreusGhostLantern;
                    lantern.poes++;
                    for (int i = 0; i < 20; i++)
                    {
                        Vector2 spawnPos = player.Center + Main.rand.NextVector2CircularEdge(50, 50);
                        Dust dust = Dust.NewDustDirect(spawnPos, 0, 0, DustID.Shadowflame, 0, 0, 100);
                        dust.velocity = Vector2.Zero;
                        if (Main.rand.NextBool(3))
                        {
                            dust.velocity += Vector2.Normalize(player.Center - dust.position) * Main.rand.NextFloat(5f);
                            dust.position += dust.velocity * 5f;
                        }
                        dust.noGravity = true;
                    }
                    for (int i = 0; i < 12; i++)
                    {
                        Vector2 spawnPos = player.Center + Main.rand.NextVector2CircularEdge(50, 50);
                        Dust dust = Dust.NewDustDirect(spawnPos, 0, 0, DustID.Electric, 0, 0, 100);
                        dust.velocity = Vector2.Zero;
                        if (Main.rand.NextBool(3))
                        {
                            dust.velocity += Vector2.Normalize(player.Center - dust.position) * Main.rand.NextFloat(5f);
                            dust.position += dust.velocity * 5f;
                        }
                        dust.noGravity = true;
                    }
                }
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath39, Projectile.Center);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = SoA.ElectricColor;
            Projectile.DrawBloomTrail(lightColor.UseA(50), SoA.OrbBloom, scale: 0.5f);
            return base.PreDraw(ref lightColor);
        }
    }
}
