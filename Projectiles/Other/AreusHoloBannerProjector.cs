using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Buffs.NPCDebuff;
using ShardsOfAtheria.Buffs.PlayerBuff.AreusBannerBuffs;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Other
{
    public class AreusHoloBannerProjector : ModProjectile
    {
        private static Asset<Texture2D> bannerTexture;
        int mode = -1;

        public override void Load()
        {
            bannerTexture = ModContent.Request<Texture2D>("ShardsOfAtheria/Projectiles/Other/AreusHoloBanner");
        }

        public override void Unload()
        {
            bannerTexture = null;
        }

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 26;
            Projectile.timeLeft *= 5;
        }

        public override void OnSpawn(IEntitySource source)
        {
            mode = Main.rand.Next(3);
        }

        public override void AI()
        {
            if (++Projectile.ai[0] > 40)
            {
                if (Projectile.velocity != Vector2.Zero)
                {
                    Projectile.velocity *= 0.85f;
                }
            }
            UpdateVisuals();
            RepellBanners(1000);
            AreusAura(500);
        }

        private void UpdateVisuals()
        {
            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame > 3)
                {
                    Projectile.frame = 0;
                }
            }
        }

        private void AreusAura(int radius)
        {
            for (var i = 0; i < 20; i++)
            {
                Vector2 spawnPos = Projectile.Center + Main.rand.NextVector2CircularEdge(radius, radius);
                Vector2 offset = spawnPos - Main.LocalPlayer.Center;
                if (Math.Abs(offset.X) > Main.screenWidth * 0.6f || Math.Abs(offset.Y) > Main.screenHeight * 0.6f) //dont spawn dust if its pointless
                    continue;
                Dust dust = Dust.NewDustDirect(spawnPos, 0, 0, DustID.Electric, 0, 0, 100);
                dust.velocity = Projectile.velocity;
                if (Main.rand.NextBool(3))
                {
                    dust.velocity += Vector2.Normalize(Projectile.Center - dust.position) * Main.rand.NextFloat(5f);
                    dust.position += dust.velocity * 5f;
                }
                dust.noGravity = true;
            }
            AreusBuffs(radius);
        }

        private void RepellBanners(float maxDistance)
        {
            foreach (Projectile projectile in Main.projectile)
            {
                if (projectile.active && projectile.whoAmI != Projectile.whoAmI && projectile.type == Type)
                {
                    var distToPlayer = Vector2.Distance(projectile.Center, Projectile.Center);
                    if (distToPlayer <= maxDistance)
                    {
                        var vector = projectile.Center - Projectile.Center;
                        vector.Normalize();
                        Projectile.velocity = vector * -4;
                        projectile.velocity = vector * 4;
                    }
                }
            }
        }

        private void AreusBuffs(int maxDistance)
        {
            foreach (Player player in Main.player)
            {
                if (player.active && !player.dead)
                {
                    var distToPlayer = Vector2.Distance(player.Center, Projectile.Center);
                    if (distToPlayer <= maxDistance)
                    {
                        if (Projectile.DamageType == DamageClass.Melee) player.AddBuff<AreusBannerMeleeBuff>(300);
                        if (Projectile.DamageType == DamageClass.Ranged) player.AddBuff<AreusBannerRangedBuff>(300);
                        if (Projectile.DamageType == DamageClass.Magic) player.AddBuff<AreusBannerMagicBuff>(300);
                        if (Projectile.DamageType == DamageClass.Summon) player.AddBuff<AreusBannerSummonBuff>(300);
                    }
                }
            }
            foreach (NPC npc in Main.npc)
            {
                if (npc.CanBeChasedBy())
                {
                    var distToNPC = Vector2.Distance(npc.Center, Projectile.Center);
                    if (distToNPC <= maxDistance)
                    {
                        if (Projectile.DamageType == DamageClass.Melee) npc.AddBuff<AreusBannerMeleeDebuff>(300);
                        if (Projectile.DamageType == DamageClass.Ranged) npc.AddBuff<AreusBannerRangedDebuff>(300);
                        if (Projectile.DamageType == DamageClass.Magic)
                        {
                            foreach (Player player in Main.player)
                            {
                                if (player.active && !player.dead)
                                {
                                    var distToPlayer = Vector2.Distance(player.Center, Projectile.Center);
                                    if (distToPlayer <= maxDistance)
                                    {
                                        if (player.statMana < player.statManaMax2 && Main.rand.NextBool(10))
                                        {
                                            player.statMana++;
                                        }
                                    }
                                }
                            }
                        }
                        if (Projectile.DamageType == DamageClass.Summon) npc.AddBuff<AreusBannerSummonDebuff>(300);
                    }
                }
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.velocity *= 0f;
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            Vector2 position = Projectile.Center - new Vector2(15, 93) - Main.screenPosition;
            Main.spriteBatch.Draw(bannerTexture.Value, position, null, lightColor);
            return base.PreDraw(ref lightColor);
        }
    }
}