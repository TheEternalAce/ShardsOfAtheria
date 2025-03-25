﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ShardsOfAtheria.Items.Tools.ToggleItems;
using ShardsOfAtheria.Projectiles.Magic;
using ShardsOfAtheria.Projectiles.NPCProj.Nova;
using ShardsOfAtheria.Projectiles.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;
using WebCom.Effects.ScreenShaking;

namespace ShardsOfAtheria.Utilities
{
    public static partial class ShardsHelpers
    {
        public static Projectile[] CallStorm(IEntitySource source, Vector2 position, int amount, int damage,
            float knockback, DamageClass damageClass, float ai0 = 0f, float ai1 = 0f, float ai2 = 0f, int owner = -1, int pierce = 1, bool hostile = false)
        {
            Projectile[] projectiles = new Projectile[amount];
            SoundEngine.PlaySound(SoundID.NPCDeath56, position);
            int projectileType = ModContent.ProjectileType<LightningBoltFriendly>();
            if (hostile)
            {
                projectileType = ModContent.ProjectileType<LightningBolt>();
            }
            for (var i = 0; i < amount - 1; i++)
            {
                projectiles[i] = Projectile.NewProjectileDirect(source,
                    new Vector2(position.X + Main.rand.Next(-60 * amount, 60 * amount), position.Y - 600),
                    new Vector2(0, 5), projectileType, damage, knockback, owner, ai0, ai1, ai2);
                projectiles[i].penetrate = pierce;
                projectiles[i].DamageType = damageClass;
            }
            projectiles[amount - 1] = Projectile.NewProjectileDirect(source,
                new Vector2(position.X, position.Y - 600),
                new Vector2(0, 5), projectileType, damage, knockback, owner, ai0, ai1, ai2);
            projectiles[amount - 1].penetrate = pierce;
            projectiles[amount - 1].DamageType = damageClass;
            return projectiles;
        }
        public static void Explode(IEntitySource source, Vector2 position, int damage, float knockback, DamageClass damageClass,
            int owner, bool hostile = false, int explosionSize = 120, bool dustParticles = true)
        {
            Projectile explosion = Projectile.NewProjectileDirect(source, position, Vector2.Zero,
                ModContent.ProjectileType<ElementExplosion>(), damage, knockback, owner);
            explosion.DamageType = damageClass;
            explosion.Size = new Vector2(explosionSize);
            explosion.hostile = hostile;
            if (dustParticles)
            {
                explosion.ai[1] = 1;
            }
            ScreenShake.ShakeScreen(6, 60);
        }

        public static Projectile[] ProjectileRing(IEntitySource source, Vector2 center, int amount,
            float radius, float speed, int type, int damage, float knockback, int owner = -1, float ai0 = 0f,
            float ai1 = 0f, float ai2 = 0f, float rotationAddition = 0f)
        {
            Projectile[] projectiles = new Projectile[amount];
            float rotation = MathHelper.ToRadians(360 / amount);
            for (int i = 0; i < amount; i++)
            {
                Vector2 position = center + Vector2.One.RotatedBy(rotation * i + rotationAddition) * radius;
                Vector2 velocity = Vector2.Normalize(center - position) * speed;
                projectiles[i] = Projectile.NewProjectileDirect(source, position, velocity, type,
                    damage, knockback, owner, ai0, ai1, ai2);
            }
            return projectiles;
        }

        public static Projectile[] ProjectileSpread(IEntitySource source, Vector2 position, Vector2 velocity, float amount,
            float rotation, bool evenSpread, int type, int damage, float knockback, int owner = -1, float ai0 = 0f,
            float ai1 = 0f, float ai2 = 0f, float speedVariation = 0f)
        {
            Projectile[] projectiles = new Projectile[(int)amount];
            if (amount <= 1)
            {
                projectiles = new Projectile[1];
                if (speedVariation > 0f) velocity *= 1f - Main.rand.NextFloat(speedVariation);
                projectiles[0] = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, owner, ai0, ai1, ai2);
                return projectiles;
            }
            for (int i = 0; i < amount; i++)
            {
                if (evenSpread)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (amount - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                    if (speedVariation > 0f) perturbedSpeed *= 1 - Main.rand.NextFloat(speedVariation);
                    projectiles[i] = Projectile.NewProjectileDirect(source, position, perturbedSpeed, type, damage, knockback, owner, ai0, ai1, ai2);
                }
                else
                {
                    Vector2 perturbedSpeed = velocity.RotatedByRandom(rotation);
                    if (speedVariation > 0f) perturbedSpeed *= 1 - Main.rand.NextFloat(speedVariation);
                    projectiles[i] = Projectile.NewProjectileDirect(source, position, perturbedSpeed, type, damage, knockback, owner, ai0, ai1, ai2);
                }
            }
            return projectiles;
        }

        public static DamageClass TryThrowing(this DamageClass damage)
        {
            if (SoA.ServerConfig.throwingWeapons) return DamageClass.Throwing;
            return damage;
        }

        public static bool TryGetModContent(string modName, string entityName, out ModItem entity)
        {
            entity = null;
            if (ModLoader.TryGetMod(modName, out Mod mod) && mod.TryFind(entityName, out entity)) return true;
            return false;
        }
        public static bool TryGetModContent(string modName, string entityName, out ModNPC entity)
        {
            entity = null;
            if (ModLoader.TryGetMod(modName, out Mod mod) && mod.TryFind(entityName, out entity)) return true;
            return false;
        }
        public static bool TryGetModContent(string modName, string entityName, out ModBuff entity)
        {
            entity = null;
            if (ModLoader.TryGetMod(modName, out Mod mod) && mod.TryFind(entityName, out entity)) return true;
            return false;
        }
        public static bool TryGetModContent(string modName, string entityName, out ModProjectile entity)
        {
            entity = null;
            if (ModLoader.TryGetMod(modName, out Mod mod) && mod.TryFind(entityName, out entity)) return true;
            return false;
        }

        public static void AdjustMagnitude(ref Vector2 vector, float num1, float num2 = 6f)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > num2)
            {
                vector *= num1 / magnitude;
            }
        }

        public static Color UseA(this Color color, int alpha) => new(color.R, color.G, color.B, alpha);

        public static Color UseA(this Color color, float alpha) => new(color.R, color.G, color.B, (int)(alpha * 255));

        public static void CappedMeleeScale(Projectile proj)
        {
            float scale = Main.player[proj.owner].CappedMeleeScale();
            if (scale != 1f)
            {
                proj.scale *= scale;
                proj.width = (int)(proj.width * proj.scale);
                proj.height = (int)(proj.height * proj.scale);
            }
        }

        public static int ProgressionMultiplier(Player player)
        {
            var tierLock = ToggleableTool.GetInstance<TierLock>(player);
            if (tierLock != null && tierLock.mode > 0) return tierLock.mode;
            if (NPC.downedMoonlord) return 5;
            else if (NPC.downedGolemBoss) return 4;
            else if (Main.hardMode) return 3;
            else if (NPC.downedBoss3) return 2;
            return 1;
        }
        public static int ScaleByProggression(Player player, int baseAmount = 1)
        {
            return baseAmount * ProgressionMultiplier(player);
        }
        public static StatModifier ScaleByProggression(Player player, StatModifier baseAmount)
        {
            return baseAmount * ProgressionMultiplier(player);
        }

        public static float Wave(float time, float minimum, float maximum)
        {
            return minimum + ((float)Math.Sin(time) + 1f) / 2f * (maximum - minimum);
        }

        public static bool DeathrayHitbox(Vector2 center, Rectangle targetHitbox, float rotation, float length, float size, float startLength = 0f)
        {
            return DeathrayHitbox(center, targetHitbox, rotation.ToRotationVector2(), length, size, startLength);
        }
        public static bool DeathrayHitbox(Vector2 center, Rectangle targetHitbox, Vector2 normal, float length, float size, float startLength = 0f)
        {
            return DeathrayHitbox(center + normal * startLength, center + normal * startLength + normal * length, targetHitbox, size);
        }
        public static bool DeathrayHitbox(Vector2 from, Vector2 to, Rectangle targetHitbox, float size)
        {
            float _ = float.NaN;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), from, to, size, ref _);
        }

        public static bool[] GetBoolArray(this TagCompound tagCompound, string key)
        {
            return tagCompound.Get<bool[]>(key);
        }

        public static Color MaxRGBA(this Color color, byte amt)
        {
            return color.MaxRGBA(amt, amt);
        }
        public static Color MaxRGBA(this Color color, byte amt, byte a)
        {
            return color.MaxRGBA(amt, amt, amt, a);
        }
        public static Color MaxRGBA(this Color color, byte r, byte g, byte b, byte a)
        {
            color.R = Math.Max(color.R, r);
            color.G = Math.Max(color.G, g);
            color.B = Math.Max(color.B, b);
            color.A = Math.Max(color.A, a);
            return color;
        }

        public static Vector2 RotateTowards(Vector2 currentPosition, Vector2 currentVelocity, Vector2 targetPosition, float maxChange)
        {
            float scaleFactor = currentVelocity.Length();
            float targetAngle = currentPosition.AngleTo(targetPosition);
            return currentVelocity.ToRotation().AngleTowards(targetAngle, maxChange).ToRotationVector2() * scaleFactor;
        }

        public static Rectangle Frame(this Rectangle rectangle, int frameX, int frameY, int sizeOffsetX = 0, int sizeOffsetY = 0)
        {
            return new Rectangle(rectangle.X + (rectangle.Width - sizeOffsetX) * frameX, rectangle.Y + (rectangle.Width - sizeOffsetY) * frameY, rectangle.Width, rectangle.Height);
        }
        public static Rectangle Frame(this Projectile projectile)
        {
            return TextureAssets.Projectile[projectile.type].Value.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame);
        }

        public static ref Rectangle Resize(this ref Rectangle rectangle, int size)
        {
            return ref rectangle.Resize(size, size);
        }
        public static ref Rectangle Resize(this ref Rectangle rectangle, int width, int height)
        {
            Point center = rectangle.Center;
            rectangle.Width = width;
            rectangle.Height = height;
            rectangle.X = center.X - width / 2;
            rectangle.Y = center.Y - height / 2;
            return ref rectangle;
        }


        public static SpriteEffects GetSpriteEffect(this Projectile projectile)
        {
            return (-projectile.spriteDirection).ToSpriteEffect();
        }

        public static SpriteEffects ToSpriteEffect(this int value)
        {
            return value == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        }

        public static void AddKey(this WeightedRandom<string> random, string key, params object[] args)
        {
            random.Add(Language.GetTextValue(key, args));
        }

        public static bool ContainsAny<T>(this IEnumerable<T> en, Predicate<T> predicate)
        {
            foreach (var t in en)
            {
                if (predicate(t))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool ContainsAny<T>(this IEnumerable<T> en, T en2)
        {
            return ContainsAny(en, (t) => t.Equals(en2));
        }

        public static bool CheckTileCollision(Vector2 pos, Rectangle hitbox)
        {
            bool valid = false;
            if (pos.X > 50f && pos.X < Main.maxTilesX * 16 - 50 &&
                pos.Y > 50f && pos.Y < Main.maxTilesY * 16 - 50)
            {
                if (!Collision.SolidCollision(pos, hitbox.Width, hitbox.Height))
                {
                    valid = true;
                }
            }
            return valid;
        }

        public static int FindClosestNPCIndex(Vector2 pos, Func<NPC, bool> additionalChecks = null, float maxDist = 2000f, params int[] blacklistedWhoAmI)
        {
            NPC npc = FindClosestNPC(pos, additionalChecks, maxDist, blacklistedWhoAmI);
            return npc is null ? -1 : npc.whoAmI;
        }
        public static NPC FindClosestNPC(Vector2 pos, Func<NPC, bool> additionalChecks = null, float maxDist = 2000f, params int[] blacklistedWhoAmI)
        {
            NPC closestNPC = null;

            // Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
            float sqrMaxDetectDistance = maxDist * maxDist;
            if (maxDist == -1) sqrMaxDetectDistance = float.PositiveInfinity;

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
                if (target.CanBeChasedBy() && (additionalChecks == null || additionalChecks(target)))
                {
                    // The DistanceSquared function returns a squared distance between 2 points, skipping relatively expensive square root calculations
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, pos);

                    // Check if it is within the radius
                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        // Check if NPC.whoAmI is not blacklisted
                        if (!blacklistedWhoAmI.Contains(target.whoAmI))
                        {
                            sqrMaxDetectDistance = sqrDistanceToTarget;
                            closestNPC = target;
                        }
                    }
                }
            }

            return closestNPC;
        }
        public static Player FindClosestPlayer(this Vector2 position, float maxDetectDistance = 2000f, params int[] blaclkistedWhoAmI)
        {
            Player closestPlayer = null;

            // Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

            // Loop through all Players(max always 255)
            for (int k = 0; k < Main.maxPlayers; k++)
            {
                Player target = Main.player[k];
                // Check if Player able to be targeted. It means that Player is
                // 1. active and alive
                if (target.active && !target.dead)
                {
                    // The DistanceSquared function returns a squared distance between 2 points, skipping relatively expensive square root calculations
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, position);

                    // Check if it is within the radius
                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        // Check if NPC.whoAmI is not blacklisted
                        if (!blaclkistedWhoAmI.Contains(target.whoAmI))
                        {
                            sqrMaxDetectDistance = sqrDistanceToTarget;
                            closestPlayer = target;
                        }
                    }
                }
            }

            return closestPlayer;
        }
        public static Projectile FindClosestProjectile(this Vector2 position, float maxDetectDistance = 2000f, int specificType = 0,
            int owner = -1, bool? hostile = null, bool? friendly = null, params int[] blaclkistedWhoAmI)
        {
            Projectile closestProjectile = null;

            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

            for (int k = 0; k < Main.maxProjectiles; k++)
            {
                Projectile target = Main.projectile[k];
                if (target.active)
                {
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, position);

                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        if (!blaclkistedWhoAmI.Contains(target.whoAmI))
                        {
                            if ((specificType == 0 || target.type == specificType) &&
                                (owner == -1 || target.owner == owner) &&
                                (hostile == null || target.hostile == hostile) &&
                                (friendly == null || target.friendly == friendly))
                            {
                                sqrMaxDetectDistance = sqrDistanceToTarget;
                                closestProjectile = target;
                            }
                        }
                    }
                }
            }

            return closestProjectile;
        }
        public static Projectile FindClosestProjectile(this Vector2 position, float maxDetectDistance = 2000f, Dictionary<string, object> args = null)
        {
            Projectile closestProjectile = null;
            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

            int specificType = 0;
            int owner = -1;
            bool? hostile = null;
            bool? friendly = null;
            List<int> blacklistedWhoAmI = new();

            if (args.ContainsKey("type"))
                specificType = (int)args["type"];
            if (args.ContainsKey("owner"))
                owner = (int)args["owner"];
            if (args.ContainsKey("hostile"))
                hostile = (bool)args["hostile"];
            if (args.ContainsKey("friendly"))
                friendly = (bool)args["friendly"];
            if (args.ContainsKey("blacklistWhoAmI"))
                blacklistedWhoAmI.AddRange((int[])args["blacklistWhoAmI"]);

            for (int k = 0; k < Main.maxProjectiles; k++)
            {
                Projectile target = Main.projectile[k];
                if (target.active)
                {
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, position);

                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        if (!blacklistedWhoAmI.Contains(target.whoAmI))
                        {
                            if ((specificType == 0 || target.type == specificType) &&
                                (owner == -1 || target.owner == owner) &&
                                (hostile == null || target.hostile == hostile) &&
                                (friendly == null || target.friendly == friendly))
                            {
                                sqrMaxDetectDistance = sqrDistanceToTarget;
                                closestProjectile = target;
                            }
                        }
                    }
                }
            }

            return closestProjectile;
        }
        public static Projectile FindClosestProjectile(this Vector2 position, float maxDetectDistance = 2000f, Func<Projectile, bool> validProjectileFunc = null)
        {
            Projectile closestProjectile = null;
            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

            for (int k = 0; k < Main.maxProjectiles; k++)
            {
                Projectile target = Main.projectile[k];
                if (target.active)
                {
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, position);

                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        if (validProjectileFunc == null || validProjectileFunc(target))
                        {
                            sqrMaxDetectDistance = sqrDistanceToTarget;
                            closestProjectile = target;
                        }
                    }
                }
            }
            return closestProjectile;
        }
        public static Projectile FindOldestProjectile(Func<Projectile, bool> validProjectileFunc = null)
        {
            Projectile closestProjectile = null;
            int timeLeft = 0;

            for (int k = 0; k < Main.maxProjectiles; k++)
            {
                Projectile target = Main.projectile[k];
                if (target.active)
                {
                    if (target.timeLeft > timeLeft)
                    {
                        if (validProjectileFunc == null || validProjectileFunc(target))
                        {
                            timeLeft = target.timeLeft;
                            closestProjectile = target;
                        }
                    }
                }
            }
            return closestProjectile;
        }

        public static void KillOldestSentry(Player player, int newTurretIndex = -1)
        {
            int currentTurretIndex = 0; // The javelin index
            Point[] turrets = new Point[player.maxTurrets];

            for (int i = 0; i < Main.maxProjectiles; i++) // Loop all projectiles
            {
                Projectile currentProjectile = Main.projectile[i];
                if (i != newTurretIndex // Make sure the looped projectile is not the current javelin
                    && currentProjectile.active // Make sure the projectile is active
                    && currentProjectile.owner == player.whoAmI // Make sure the projectile's owner is the client's player
                    && currentProjectile.sentry) // Make sure the projectile is a sentry
                {
                    turrets[currentTurretIndex++] = new Point(i, currentProjectile.timeLeft); // Add the current projectile's index and timeleft to the point array
                    if (currentTurretIndex >= turrets.Length)  // If the javelin's index is bigger than or equal to the point array's length, break
                        break;
                }
            }

            // Remove the oldest sticky javelin if we exceeded the maximum
            if (currentTurretIndex >= player.maxTurrets)
            {
                int oldTurretIndex = 0;
                // Loop our point array
                for (int i = 0; i < player.maxTurrets; i++)
                {
                    // Remove the already existing javelin if it's timeLeft value (which is the Y value in our point array) is smaller than the new javelin's timeLeft
                    if (turrets[i].Y < turrets[oldTurretIndex].Y)
                    {
                        oldTurretIndex = i; // Remember the index of the removed javelin
                    }
                }
                // Remember that the X value in our point array was equal to the index of that javelin, so it's used here to kill it.
                Main.projectile[turrets[oldTurretIndex].X].Kill();
            }
        }

        public static bool AnyProjectile(int type)
        {
            bool projectileFound = false;
            for (int k = 0; k < Main.maxProjectiles; k++)
            {
                Projectile target = Main.projectile[k];
                if (target.active && target.type == type)
                {
                    projectileFound = true;
                    break;
                }
            }
            return projectileFound;
        }
        public static bool AnyProjectile<T>() where T : ModProjectile
        {
            bool projectileFound = false;
            for (int k = 0; k < Main.maxProjectiles; k++)
            {
                Projectile target = Main.projectile[k];
                if (target.active && target.type == ModContent.ProjectileType<T>())
                {
                    projectileFound = true;
                    break;
                }
            }
            return projectileFound;
        }

        public static bool MovingTowardPoint(Vector2 targetPos, Vector2 velocity, Vector2 point, float checkDistance = 2000f)
        {
            velocity.Normalize();
            Rectangle hitbox = new((int)point.X, (int)point.Y, 1, 1);
            float maximumAngle = MathHelper.PiOver4;
            float coneRotation = velocity.ToRotation();
            if (Utils.IntersectsConeSlowMoreAccurate(hitbox, targetPos, checkDistance, coneRotation, maximumAngle)) return true;
            return false;
        }

        public static bool NoInvasionOfAnyKind(this NPCSpawnInfo spawnInfo)
        {
            return !(Main.eclipse || spawnInfo.Player.ZoneTowerNebula ||
                spawnInfo.Player.ZoneTowerVortex || spawnInfo.Player.ZoneTowerSolar ||
                spawnInfo.Player.ZoneTowerStardust || Main.pumpkinMoon || Main.snowMoon ||
                spawnInfo.Invasion);
        }

        public static Dust[] DustRing(Vector2 position, float radius, int dustType, int amount = 28, float fadeIn = 1.3f, bool noGravity = true)
        {
            Dust[] dusts = new Dust[amount];
            for (var i = 0; i < amount; i++)
            {
                var vector = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(position, dustType, vector * radius);
                d.fadeIn = fadeIn;
                d.noGravity = noGravity;
                dusts[i] = d;
            }
            return dusts;
        }

        public static TooltipLine ShiftTooltipCycle(this ModItem modItem, int maxIndex)
        {
            TooltipLine line;
            var shards = Main.LocalPlayer.Shards();
            float cycleSpeed = 1f;
            if (Main.keyState.IsKeyDown(Keys.Left)) cycleSpeed /= 2;
            if (Main.keyState.IsKeyDown(Keys.Right)) cycleSpeed *= 2;
            shards.shiftTooltipCycleTimer += cycleSpeed;
            if (Main.keyState.IsKeyDown(Keys.LeftControl) || Main.keyState.IsKeyDown(Keys.LeftControl)) shards.shiftTooltipCycleTimer = 0;
            if (shards.shiftTooltipCycleTimer >= 180)
            {
                if (++shards.shiftTooltipIndex > maxIndex) shards.shiftTooltipIndex = 0;
                shards.shiftTooltipCycleTimer = 0;
            }
            string shiftText = Language.GetTextValue(modItem.GetLocalizationKey("ShiftTooltip" + shards.shiftTooltipIndex));
            line = new(modItem.Mod, "ShiftTooltip", shiftText);
            return line;
        }
    }
}
