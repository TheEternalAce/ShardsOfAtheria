using BattleNetworkElements.Elements;
using BattleNetworkElements.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Projectiles.Weapon.Magic;
using System;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
//using WebCom.Effects.ScreenShaking;

namespace ShardsOfAtheria.Utilities
{
    public static class ShardsProjectileHelper
    {
        public static void CallStorm(this Projectile projectile, int amount, int pierce = 1)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath56, projectile.Center);
            for (var i = 0; i < amount; i++)
            {
                Projectile p = Projectile.NewProjectileDirect(projectile.GetSource_FromThis(),
                    new Vector2(projectile.Center.X + Main.rand.Next(-60 * amount, 60 * amount), projectile.Center.Y - 600), new Vector2(0, 5),
                    ModContent.ProjectileType<LightningBoltFriendly>(), (int)(projectile.damage * 0.66f), projectile.knockBack, Main.player[projectile.owner].whoAmI);
                p.penetrate = pierce;
                p.DamageType = projectile.DamageType;
            }
        }

        public static void Explode(this Projectile proj, Vector2 position, int damage, bool hostile = false, int explosionSize = 120)
        {
            Projectile explosion = Projectile.NewProjectileDirect(proj.GetSource_FromThis(), position, Vector2.Zero,
                ModContent.ProjectileType<ElementExplosion>(), damage, proj.knockBack, proj.owner);
            explosion.DamageType = proj.DamageType;
            explosion.Size = new Vector2(explosionSize);
            explosion.hostile = hostile;
            ProjectileElements elementExplosion = explosion.GetGlobalProjectile<ProjectileElements>();
            int type = proj.type;
            if (proj.IsFire())
            {
                elementExplosion.isFire = true;
            }
            if (proj.IsAqua())
            {
                elementExplosion.isAqua = true;
            }
            if (proj.IsElec())
            {
                elementExplosion.isElec = true;
            }
            if (proj.IsWood())
            {
                elementExplosion.isWood = true;
            }
        }

        public static void Explode(this Projectile proj, int explosionSize = 120)
        {
            Vector2 newExplosionSize = new Vector2(explosionSize);
            proj.timeLeft = 10;
            proj.velocity *= 0f;
            proj.position += (proj.Size - newExplosionSize) / 2;
            proj.Size = newExplosionSize;
            proj.hide = true;
            proj.alpha = 255;
            proj.tileCollide = false;
            proj.ownerHitCheck = true;
            //ScreenShake.ShakeScreen(6, 60);
            SoundEngine.PlaySound(SoundID.Item14);
            for (int i = 0; i < 10; i++)
            {
                if (proj.IsFire())
                {
                    Dust dust = Dust.NewDustDirect(proj.position, proj.height, proj.width, DustID.Torch, Scale: 1.3f);
                    dust.velocity *= 4f;
                }
                if (proj.IsAqua())
                {
                    Dust dust = Dust.NewDustDirect(proj.position, proj.height, proj.width, DustID.Ice);
                    dust.velocity *= 4f;
                }
                if (proj.IsElec())
                {
                    Dust dust = Dust.NewDustDirect(proj.position, proj.height, proj.width, DustID.Electric);
                    dust.velocity *= 4f;
                }
                Dust dust2 = Dust.NewDustDirect(proj.position, proj.height, proj.width, DustID.Smoke, Scale: 1.5f);
                dust2.velocity *= 2f;
            }
        }

        /// <summary>
        /// Finding the closest NPC to attack within maxDetectDistance range.
        /// If not found then returns null.
        /// </summary>
        /// <param name="maxDetectDistance"></param>
        /// <returns></returns>
        public static NPC FindClosestNPC(this Projectile projectile, float maxDetectDistance, params int[] blaclkistedWhoAmI)
        {
            NPC closestNPC = null;

            // Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;
            if (maxDetectDistance < 0)
            {
                sqrMaxDetectDistance = float.PositiveInfinity;
            }

            // Loop through all NPCs(max always 200)
            for (int k = 0; k < Main.maxNPCs; k++)
            {
                NPC target = Main.npc[k];
                // Check if NPC able to be targeted. It means that NPC is
                // 1. active (alive)
                // 2. chaseable (e.g. not a cultist archer)
                // 3. max life bigger than 5 (e.g. not a critter)
                // 4. can take damage (e.g. moonlord core after all it's parts are downed)
                // 5. hostile (!friendly)
                // 6. not immortal (e.g. not a target dummy)
                if (target.CanBeChasedBy())
                {
                    // The DistanceSquared function returns a squared distance between 2 points, skipping relatively expensive square root calculations
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, projectile.Center);

                    // Check if it is within the radius
                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        // Check if NPC.whoAmI is not blacklisted
                        if (!blaclkistedWhoAmI.Contains(target.whoAmI))
                        {
                            sqrMaxDetectDistance = sqrDistanceToTarget;
                            closestNPC = target;
                        }
                    }
                }
            }

            return closestNPC;
        }

        public static void Track(this Projectile projectile, NPC npc, float maxDist, float speed = 16f, float inertia = 16f)
        {
            if (npc == null) return;
            projectile.Track(npc.Center, maxDist, speed, inertia);
        }
        public static void Track(this Projectile projectile, Vector2 position, float maxDist, float speed = 16f, float inertia = 16f)
        {
            bool shouldTrack = true;
            if (maxDist > 0)
            {
                if (Vector2.Distance(projectile.Center, position) > maxDist)
                {
                    shouldTrack = false;
                }
            }
            if (shouldTrack)
            {
                // The immediate range around the target (so it doesn't latch onto it when close)
                Vector2 direction = position - projectile.Center;
                direction.Normalize();
                direction *= speed;

                projectile.velocity = (projectile.velocity * (inertia - 1) + direction) / inertia;
            }
        }

        public const string DiamondX1 = "DiamondBlur1";
        public const string DiamondX2 = "DiamondBlur2";
        public const string OrbX1 = "OrbBlur1";
        public const string OrbX2 = "OrbBlur2";
        public const string LineX1 = "LineTrail1";
        public const string LineX2 = "LineTrail2";
        public static void DrawProjectilePrims(this Projectile projectile, Color color, string style, float angleAdd = 0f, float scale = 1f)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);

            Main.instance.LoadProjectile(projectile.type);
            Texture2D texture = ModContent.Request<Texture2D>("ShardsOfAtheria/BlurTrails/" + style).Value;
            float plusRot = 0;
            if (style == DiamondX1 || style == DiamondX2 || style == LineX1 || style == LineX2)
            {
                plusRot = MathHelper.ToRadians(90);
            }

            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                var offset = new Vector2(projectile.width / 2f, projectile.height / 2f);
                var frame = texture.Frame(1, 1, 0, 0);
                Vector2 drawPos = (projectile.oldPos[k] - Main.screenPosition) + offset;
                float sizec = scale * (projectile.oldPos.Length - k) / (projectile.oldPos.Length * 0.8f);
                Color drawColor = color * (1f - projectile.alpha) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, frame, drawColor, projectile.oldRot[k] + plusRot + angleAdd, frame.Size() / 2, sizec, SpriteEffects.None, 0);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Main.GameViewMatrix.ZoomMatrix);
        }
        //Credits to Aslysmic/Tewst Mod (so cool)

        public static void DrawPrimsAfterImage(this Projectile projectile, Color color, Texture2D texture)
        {
            Rectangle frame = new Rectangle(0, 0, texture.Width, texture.Height);
            Vector2 offset = new Vector2(projectile.width / 2, projectile.height / 2);
            var effects = projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            var origin = frame.Size() / 2f;
            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[projectile.type]; i++)
            {
                float progress = 1f / ProjectileID.Sets.TrailCacheLength[projectile.type] * i;
                Main.spriteBatch.Draw(texture, projectile.oldPos[i] + offset - Main.screenPosition, frame, color * (1f - progress), projectile.rotation, origin, Math.Max(projectile.scale * (1f - progress), 0.1f), effects, 0f);
            }

            Main.spriteBatch.Draw(texture, projectile.position + offset - Main.screenPosition, null, Color.White, projectile.rotation, origin, projectile.scale, SpriteEffects.None, 0f);
        }

        public static void DrawPrimsAfterImage(this Projectile projectile, Color color)
        {
            var texture = TextureAssets.Projectile[projectile.type].Value;
            projectile.DrawPrimsAfterImage(color, texture);
        }
        //Credits to Aequus Mod (Omega Starite my beloved)

        public static SoAGlobalProjectile ShardsOfAtheria(this Projectile projectile)
        {
            return projectile.GetGlobalProjectile<SoAGlobalProjectile>();
        }

        public static void AddAreus(this Projectile projectile, bool dark = false)
        {
            projectile.type.AddAreusProj(dark);
            projectile.AddElec();
        }
        public static void AddAreusProj(this int projID, bool dark)
        {
            if (dark)
            {
                SoAGlobalProjectile.DarkAreusProj.Add(projID);
            }
            else
            {
                SoAGlobalProjectile.AreusProj.Add(projID);
            }
        }
    }
}
