using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
            Projectile.AddElement(2);
            Projectile.AddRedemptionElement(7);
            Projectile.AddRedemptionElement(2);
            Projectile.AddRedemptionElement(9);
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
            if (player.HasItem<AreusGhostLantern>())
            {
                if (player.Overdrive() && Projectile.Distance(player.Center) < 200)
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
                    for (int i = 0; i < 10; i++)
                    {
                        Vector2 spawnPos = player.Center + Main.rand.NextVector2CircularEdge(50, 50);
                        Dust dust = Dust.NewDustDirect(spawnPos, 0, 0, DustID.Shadowflame, 0, 0, 100);
                        dust.velocity = player.velocity;
                        if (Main.rand.NextBool(3))
                        {
                            dust.velocity += Vector2.Normalize(player.Center - dust.position) * Main.rand.NextFloat(5f);
                            dust.position += dust.velocity * 5f;
                        }
                        dust.noGravity = true;
                    }
                    for (int i = 0; i < 10; i++)
                    {
                        Vector2 spawnPos = player.Center + Main.rand.NextVector2CircularEdge(50, 50);
                        Dust dust = Dust.NewDustDirect(spawnPos, 0, 0, DustID.Electric, 0, 0, 100);
                        dust.velocity = player.velocity;
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
            lightColor = SoA.ElectricColorA;
            var texture = ModContent.Request<Texture2D>("ShardsOfAtheria/Assets/BlurTrails/OrbBlur").Value;
            var frame = texture.Frame();
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, frame, lightColor, 0, frame.Size() / 2, 0.5f, SpriteEffects.None);
            return base.PreDraw(ref lightColor);
        }
    }
}
