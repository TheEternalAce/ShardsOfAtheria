using BattleNetworkElements;
using BattleNetworkElements.Utilities;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Globals
{
    public class SoAGlobalProjectile : GlobalProjectile
    {
        public bool tempAreus = false;
        public bool explosion = false;
        public bool areusNullField = false;

        public static readonly Dictionary<int, bool> AreusProj = [];
        public static readonly List<int> Eraser = [];
        public static readonly Dictionary<int, float> Metalic = new()
        {
            #region Melee
            {ProjectileID.Anchor, 1f},
            {ProjectileID.BallOHurt, 1f},
            {ProjectileID.BloodyMachete, 1f},
            {ProjectileID.ChainGuillotine, 1f },
            {ProjectileID.ChainKnife, 1f },
            {ProjectileID.ChlorophyteOrb, 1f},
            {ProjectileID.FlamingMace, 1f},
            {ProjectileID.Mace, 1f},
            {ProjectileID.TheMeatball, 1f},
            #endregion
            #region Ranged
            {ProjectileID.Bullet, 1f},
            {ProjectileID.BulletDeadeye, 1f},
            {ProjectileID.BulletHighVelocity, 1f},
            {ProjectileID.BulletSnowman, 1f},
            {ProjectileID.ChlorophyteBullet, 1f},
            {ProjectileID.MoonlordBullet, 1f},
            {ProjectileID.SilverBullet, 1f},
            {ProjectileID.SniperBullet, 1f},

            {ProjectileID.ChlorophyteArrow, 1f},
            {ProjectileID.MoonlordArrow, 1f},

            {ProjectileID.Nail, 0.33f},
            {ProjectileID.NailFriendly, 0.1f},

            {ProjectileID.RocketI, 3f },
            {ProjectileID.RocketII, 3f },
            {ProjectileID.RocketIII, 3f },
            {ProjectileID.RocketIV, 3f },
            {ProjectileID.MiniNukeRocketI, 3f },
            {ProjectileID.MiniNukeRocketII, 3f },
            {ProjectileID.ClusterRocketI, 3f },
            {ProjectileID.ClusterRocketII, 3f },

            {ProjectileID.DD2JavelinHostile, 1f },
            {ProjectileID.DD2JavelinHostileT3, 1f },
            {ProjectileID.JavelinFriendly, 1f },
            {ProjectileID.JavelinHostile, 1f },
            {ProjectileID.PoisonedKnife, 1f },
            {ProjectileID.Shuriken, 1f },
            {ProjectileID.SpikyBall, 1f },
            {ProjectileID.ThrowingKnife, 1f },

            {ProjectileID.PoisonDartBlowgun, 1f },

            {ProjectileID.CopperCoin, 1f },
            {ProjectileID.GoldCoin, 1f },
            {ProjectileID.PlatinumCoin, 1f },
            {ProjectileID.SilverCoin, 1f },
            {ProjectileID.MechanicalPiranha, 1f },
            {ProjectileID.Harpoon, 1f },
            #endregion
            #region Summon
            {ProjectileID.DD2BallistraProj, 1f },
            #endregion
        };
        #region Reflectable AI Styles
        public static readonly List<int> ReflectAiList =
        [
            0,
            1,
            2,
            3,
            8,
            18,
            16,
            21,
            23,
            24,
            25,
            27,
            28,
            29,
            32,
            33,
            34,
            36,
            40,
            41,
            43,
            44,
            45,
            46,
            47,
            48,
            49,
            50,
            51,
            55,
            56,
            57,
            58,
            60,
            65,
            68,
            70,
            71,
            72,
            74,
            75,
            80,
            81,
            82,
            83,
            88,
            91,
            92,
            93,
            95,
            96,
            102,
            105,
            106,
            107,
            108,
            109,
            112,
            113,
            115,
            116,
            118,
            119,
            126,
            129,
            131,
            143,
            144,
            147,
            151,
            179,
            181
        ];
        #endregion

        public override bool InstancePerEntity => true;

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (SoA.BNEEnabled)
            {
                ChangeElements(projectile, source);
            }
            base.OnSpawn(projectile, source);
        }

        [JITWhenModsEnabled("BattleNetworkElements")]
        private static void ChangeElements(Projectile projectile, IEntitySource source)
        {
            foreach (Player player in Main.player)
            {
                if (player.active && !player.dead)
                {
                    if (projectile.owner == player.whoAmI && projectile.friendly)
                    {
                        var projectileElement = projectile.Elements();
                        if (player.Shards().areusProcessor)
                        {
                            projectileElement.isFire = false;
                            projectileElement.isAqua = false;
                            projectileElement.isElec = false;
                            projectileElement.isWood = false;

                            switch (player.Shards().processorElement)
                            {
                                case Element.Fire:
                                    projectileElement.isFire = true;
                                    break;
                                case Element.Aqua:
                                    projectileElement.isAqua = true;
                                    break;
                                case Element.Elec:
                                    projectileElement.isElec = true;
                                    break;
                                case Element.Wood:
                                    projectileElement.isWood = true;
                                    break;
                            }
                        }
                    }
                }
            }
        }

        public override bool PreAI(Projectile projectile)
        {
            areusNullField = false;
            return base.PreAI(projectile);
        }

        public override void PostAI(Projectile projectile)
        {
            int type = projectile.type;
            if (Eraser.Contains(type))
            {
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile proj = Main.projectile[i];
                    if (ReflectAiList.Contains(proj.aiStyle))
                    {
                        if (projectile.Hitbox.Intersects(proj.Hitbox) &&
                            proj.whoAmI != projectile.whoAmI &&
                            proj.hostile && proj.active)
                        {
                            for (var d = 0; d < 28; d++)
                            {
                                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                                Dust dust = Dust.NewDustPerfect(proj.Center,
                                    DustID.YellowTorch, speed * 2.4f);
                                dust.fadeIn = 1.3f;
                                dust.noGravity = true;
                            }
                            proj.Kill();
                        }
                    }
                }
            }
            if (Metalic.ContainsKey(type))
            {
                var npc = ShardsHelpers.FindClosestNPC(projectile.Center, (npc) => npc.HasBuff<Magnetic>(), 200f);
                if (npc != null)
                {
                    float speed = projectile.velocity.Length();
                    var vectorToTarget = npc.Center - projectile.Center;
                    ShardsHelpers.AdjustMagnitude(ref vectorToTarget, speed);
                    projectile.velocity = (4 * projectile.velocity + vectorToTarget) / 2f;
                    ShardsHelpers.AdjustMagnitude(ref projectile.velocity, speed);
                }
            }
            base.PostAI(projectile);
        }

        public override bool? CanHitNPC(Projectile projectile, NPC target)
        {
            if (target.GetGlobalNPC<SoAGlobalNPC>().areusNullField || areusNullField) return false;
            return base.CanHitNPC(projectile, target);
        }

        public override bool CanHitPlayer(Projectile projectile, Player target)
        {
            if (target.Shards().areusNullField || areusNullField) return false;
            return base.CanHitPlayer(projectile, target);
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            var player = projectile.GetPlayerOwner();
            var shards = player.Shards();
            if (projectile.IsAreus(false) || tempAreus)
            {
                int buffType = ModContent.BuffType<ElectricShock>();
                int buffTime = 600;
                if (shards.conductive)
                {
                    buffTime *= 2;
                }
                if (NPC.downedPlantBoss)
                {
                    buffType = BuffID.Electrified;
                }
                target.AddBuff(buffType, buffTime);
            }
        }
    }
}
