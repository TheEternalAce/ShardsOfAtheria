using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Melee
{
    public class ReactorMeltdownProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // The following sets are only applicable to yoyo that use aiStyle 99.
            // YoyosLifeTimeMultiplier is how long in seconds the yoyo will stay out before automatically returning to the player. 
            // Vanilla values range from 3f(Wood) to 16f(Chik), and defaults to -1f. Leaving as -1 will make the time infinite.
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = -1f;
            // YoyosMaximumRange is the maximum distance the yoyo sleep away from the player. 
            // Vanilla values range from 130f(Wood) to 400f(Terrarian), and defaults to 200f
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 300;
            // YoyosTopSpeed is top speed of the yoyo Projectile. 
            // Vanilla values range from 9f(Wood) to 17.5f(Terrarian), and defaults to 10f
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 14f;
            Main.projFrames[Projectile.type] = 2;
            Projectile.AddDamageType(5, 10);
            Projectile.AddElement(2);
            Projectile.AddElement(3);
            Projectile.AddRedemptionElement(7);
            Projectile.AddRedemptionElement(11);
        }

        public override void SetDefaults()
        {
            Projectile.extraUpdates = 0;
            Projectile.width = 16;
            Projectile.height = 16;
            // aiStyle 99 is used for all yoyos, and is Extremely suggested, as yoyo are extremely difficult without them
            Projectile.aiStyle = 99;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.scale = 1f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Electrified, 600);
            var explosion = Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero,
                ModContent.ProjectileType<ElectricExplosion>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
            explosion.DamageType = Projectile.DamageType;
        }

        public override void AI()
        {
            base.AI();

            if (++Projectile.frameCounter >= 60)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame == 1)
                {
                    if (SoA.ClientConfig.reactorBeep)
                    {
                        SoundEngine.PlaySound(SoA.ReactorAlarm, Projectile.Center);
                    }
                }
                if (Projectile.frame >= Main.projFrames[Type])
                {
                    Projectile.frame = 0;
                }
            }
            if (Projectile.frame == 1)
            {
                RadioactiveAura();
            }
        }

        int radiationRadius = 100;
        void RadioactiveAura()
        {
            for (var i = 0; i < 20; i++)
            {
                Vector2 spawnPos = Projectile.Center + Main.rand.NextVector2CircularEdge(radiationRadius, radiationRadius);
                Vector2 offset = spawnPos - Main.LocalPlayer.Center;
                if (Math.Abs(offset.X) > Main.screenWidth * 0.6f || Math.Abs(offset.Y) > Main.screenHeight * 0.6f) //dont spawn dust if its pointless
                    continue;
                Dust dust = Dust.NewDustDirect(spawnPos, 0, 0, DustID.Stone, 0, 0, 100, Color.GreenYellow);
                dust.velocity = Projectile.velocity;
                if (Main.rand.NextBool(3))
                {
                    dust.velocity += Vector2.Normalize(Projectile.Center - dust.position) * Main.rand.NextFloat(5f);
                    dust.position += dust.velocity * 5f;
                }
                dust.noGravity = true;
            }
            Radiation();
        }

        void Radiation()
        {
            foreach (NPC npc in Main.npc)
            {
                if (npc.active)
                {
                    var distToNPC = Vector2.Distance(npc.Center, Projectile.Center);
                    if (distToNPC <= radiationRadius)
                    {
                        npc.AddBuff(BuffID.Venom, 600);
                    }
                }
            }
            foreach (Player player in Main.player)
            {
                if (player.active && !player.dead)
                {
                    var distToNPC = Vector2.Distance(player.Center, Projectile.Center);
                    if (distToNPC <= radiationRadius)
                    {
                        player.AddBuff(BuffID.Venom, 600);
                    }
                }
            }
        }

        public override void PostAI()
        {
            if (Main.rand.NextBool())
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Electric);
                dust.noGravity = true;
                dust.scale = .6f;
            }
        }
    }
}
