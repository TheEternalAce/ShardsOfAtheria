using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Items.Weapons.Ammo;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Weapon.Ammo;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Projectiles.Minions
{
    public class YoungHarpy : ModProjectile
    {
        int shootTimer = 0;

        public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.LilHarpy;

        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = Main.projFrames[ProjectileID.LilHarpy];
            // This is necessary for right-click targeting
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

            Main.projPet[Projectile.type] = true; // Denotes that this projectile is a pet or minion

            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true; // This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true; // Make the cultist resistant to this projectile, as it's resistant to all homing projectiles.
        }

        public override void SetDefaults()
        {
            Projectile refProj = new Projectile();
            refProj.SetDefaults(ProjectileID.LilHarpy);

            Projectile.netImportant = true;
			Projectile.width = refProj.width;
			Projectile.height = refProj.height;
			Projectile.aiStyle = 62;
			Projectile.penetrate = -1;
			Projectile.timeLeft *= 5;
			Projectile.minion = true;
			Projectile.minionSlots = 1f;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];

            Projectile.netUpdate = true;
            if (!CheckActive(owner))
                Projectile.Kill();
            else
            {
                if (++Projectile.frameCounter >= 12)
                {
                    if (++Projectile.frame >= 5)
                    {
                        Projectile.frame = 0;
                    }
                    Projectile.frameCounter = 0;
                }
                SearchForTargets(owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
                if (foundTarget)
                {
                    if (++shootTimer >= 90)
                    {
                        float numberProjectiles = 3; // 3 shots
                        float rotation = MathHelper.ToRadians(5);
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 vel = Vector2.Normalize(targetCenter - Projectile.Center);
                            Vector2 perturbedSpeed = vel.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 7f; // Watch out for dividing by 0 if there is only 1 projectile.
                            Projectile feather = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.Center, perturbedSpeed, ProjectileID.HarpyFeather, Projectile.damage, 0, Projectile.owner);
                            feather.friendly = true;
                            feather.hostile = false;
                            feather.penetrate = 1;
                            feather.DamageType = DamageClass.Summon;
                        }
                        shootTimer = 0;
                    }
                }
                else
                {
                    shootTimer = 0;
                }
            }
            base.AI();
        }

        private void SearchForTargets(Player owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter)
        {
            // Starting search distance
            distanceFromTarget = 700f;
            targetCenter = Projectile.position;
            foundTarget = false;

            // This code is required if your minion weapon has the targeting feature
            if (owner.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[owner.MinionAttackTargetNPC];
                float between = Vector2.Distance(npc.Center, Projectile.Center);

                // Reasonable distance away so it doesn't target across multiple screens
                if (between < 2000f)
                {
                    distanceFromTarget = between;
                    targetCenter = npc.Center;
                    foundTarget = true;
                }
            }

            if (!foundTarget)
            {
                // This code is required either way, used for finding a target
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];

                    if (npc.CanBeChasedBy())
                    {
                        float between = Vector2.Distance(npc.Center, Projectile.Center);
                        bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
                        bool inRange = between < distanceFromTarget;
                        bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);
                        // Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
                        // The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
                        bool closeThroughWall = between < 100f;

                        if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall))
                        {
                            distanceFromTarget = between;
                            targetCenter = npc.Center;
                            foundTarget = true;
                        }
                    }
                }
            }

            // friendly needs to be set to true so the minion can deal contact damage
            // friendly needs to be set to false so it doesn't damage things like target dummies while idling
            // Both things depend on if it has a target or not, so it's just one assignment here
            // You don't need this assignment if your minion is shooting things instead of dealing contact damage
            Projectile.friendly = foundTarget;
        }

        // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
        private bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active || !owner.HasBuff(ModContent.BuffType<JuvenileHarpy>()))
                return false;
            else Projectile.timeLeft = 2;
            return true;
        }
    }
}
