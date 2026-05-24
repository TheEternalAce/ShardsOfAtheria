using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.PlayerBuff;
using ShardsOfAtheria.Buffs.Summons;
using ShardsOfAtheria.Projectiles.NPCProj;
using ShardsOfAtheria.Utilities;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.NPCs.Misc
{
    public class Creeper : ModNPC
    {
        int lingerTimer;
        int speedUpTimer;
        int _behaviour;
        int Behaviour
        {
            get => _behaviour;
            set
            {
                _behaviour = value;
                speedUpTimer = 0;
            }
        }
        Vector2 halfSize = new(13, 13);
        Vector2 targetPosition = Vector2.Zero;
        int hitboxWhoAmI;

        const int AttackBehavior = 0;
        const int ReturnBehavior = 1;
        const int LingerBehavior = 2;
        const int LingerAttackBehavior = 3;

        public override void SetStaticDefaults()
        {
            NPC.AddElement(1);
            NPC.AddElement(3);
            NPC.AddRedemptionElement(10);
            NPC.AddRedemptionElementType("Blood");

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new()
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }

        public override void SetDefaults()
        {
            NPC.width = 26;
            NPC.height = 26;
            NPC.damage = 20;
            NPC.defense = 0;
            NPC.lifeMax = 100;
            NPC.friendly = true;
            NPC.HitSound = SoundID.NPCHit9;
            NPC.DeathSound = SoundID.NPCDeath11;
            NPC.aiStyle = -1;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            NPC.ElementMultipliers(ShardsHelpers.NPCMultipliersAqua);
        }

        public override void OnSpawn(IEntitySource source)
        {
            EntitySource_BossSpawn bossSource = source as EntitySource_BossSpawn;
            targetPosition = bossSource.Target.Center;
            CreateHitbox();
        }

        Projectile Hitbox => Main.projectile[hitboxWhoAmI];

        public override void AI()
        {
            float speed = 8f;
            float inertia = 30f;
            // This should almost always be the first code in AI() as it is responsible for finding the proper player target
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest();
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (!Hitbox.active) CreateHitbox();

                var player = Main.player[NPC.target];
                if (player.dead || !player.active || !player.Slayer().BoCSoul)
                    NPC.active = false;
                else NPC.active = true;

                player.AddBuff(ModContent.BuffType<CreeperShield>(), 5);

                if (Behaviour == LingerAttackBehavior || Behaviour == LingerBehavior)
                    lingerTimer++;
                if (lingerTimer >= 120)
                {
                    lingerTimer = 0;
                    if (Behaviour == LingerBehavior)
                    {
                        Behaviour = AttackBehavior;
                        targetPosition = Main.MouseWorld;
                    }
                    else if (Behaviour == LingerAttackBehavior)
                        Behaviour = ReturnBehavior;
                }

                if (Behaviour == LingerBehavior || Behaviour == ReturnBehavior)
                    targetPosition = player.Center;

                bool nearAttackPosition = NPC.Distance(player.Center) > 1000f ||
                    NPC.Distance(targetPosition) < 25f;
                bool tooFar = NPC.Distance(targetPosition) > 500f;
                bool wayTooFar = NPC.Distance(player.Center) > 1600f;

                if (wayTooFar) NPC.Center = player.Center;
                else if (tooFar && ++speedUpTimer > 120) speed += 20f;
                else if (nearAttackPosition && Behaviour == AttackBehavior)
                {
                    if (Main.rand.NextBool(3)) Behaviour = LingerAttackBehavior;
                    else
                    {
                        Behaviour = ReturnBehavior;
                        targetPosition = player.Center;
                    }
                }
                else if (NPC.Distance(targetPosition) < 50f && Behaviour == ReturnBehavior)
                {
                    if (Main.rand.NextBool(3)) Behaviour = LingerBehavior;
                    else
                    {
                        Behaviour = AttackBehavior;
                        targetPosition = Main.MouseWorld;
                    }
                }

                Vector2 direction = targetPosition - NPC.position - halfSize;
                direction.Normalize();
                direction *= speed;

                NPC.velocity = (NPC.velocity * (inertia - 1) + direction) / inertia;

                // If your minion is flying, you want to do this independently of any conditions
                float overlapVelocity = 0.04f;
                // Fix overlap with other minions
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC other = Main.npc[i];

                    if (i != NPC.whoAmI && other.active && other.type == ModContent.NPCType<Creeper>() && Math.Abs(NPC.position.X - other.position.X) + Math.Abs(NPC.position.Y - other.position.Y) < NPC.width)
                    {
                        if (NPC.position.X < other.position.X)
                            NPC.velocity.X -= overlapVelocity;
                        else
                            NPC.velocity.X += overlapVelocity;

                        if (NPC.position.Y < other.position.Y)
                            NPC.velocity.Y -= overlapVelocity;
                        else
                            NPC.velocity.Y += overlapVelocity;
                    }
                }
            }
        }

        public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            projectile.penetrate--;
        }

        public override void OnKill()
        {
            Main.player[NPC.target].AddBuff(ModContent.BuffType<CreeperRevenge>(), 600);
            Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(Main.rand.Next(-6, 7), Main.rand.Next(-6, 7)), 402);
        }

        void CreateHitbox()
        {
            hitboxWhoAmI = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, Vector2.Zero,
                ModContent.ProjectileType<CreeperHitbox>(), 40, 0f, NPC.target, NPC.whoAmI);
        }
    }
}