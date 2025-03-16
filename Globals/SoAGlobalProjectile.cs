using BattleNetworkElements;
using BattleNetworkElements.Utilities;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Weapons.Ranged;
using ShardsOfAtheria.Projectiles.Ammo;
using ShardsOfAtheria.Projectiles.Magic.ThorSpear;
using ShardsOfAtheria.Utilities;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Globals
{
    public class SoAGlobalProjectile : GlobalProjectile
    {
        public bool explosion = false;
        public bool areusNullField = false;
        public bool nailPunch = false;

        int voidTrailCooldown = 0;

        public static readonly Dictionary<int, bool> AreusProj = [];
        public static readonly List<int> Eraser = [];
        public static readonly List<int> TrueMelee = [];
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

            {ModContent.ProjectileType<GoldenNail>(), 0.33f},
            {ModContent.ProjectileType<StickingMagnetProj>(), 0f},
            #endregion
            #region Magic
            { ModContent.ProjectileType<ElectricJavelin>(), 1f },
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

        public override void SetStaticDefaults()
        {
            if (ShardsHelpers.TryGetModContent("GMR", "OvercooledBullet", out ModProjectile overcooledNail)) Metalic.Add(overcooledNail.Type, 0.33f);
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (SoA.BNEEnabled)
                ChangeElements(projectile, source);
            if (source is EntitySource_ItemUse_WithAmmo weaponSource)
                if (weaponSource.Item.type == ModContent.ItemType<NailPounder>()) nailPunch = true;
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
                var magnetPos = ShardsHelpers.FindClosestProjectile(projectile.Center, 200f, magnet => Magnet(magnet, projectile));
                if (magnetPos != null)
                {
                    float speed = projectile.velocity.Length();
                    var vectorToTarget = magnetPos.Center - projectile.Center;
                    ShardsHelpers.AdjustMagnitude(ref vectorToTarget, speed);
                    projectile.velocity = (3 * projectile.velocity + vectorToTarget) / 2f;
                    ShardsHelpers.AdjustMagnitude(ref projectile.velocity, speed);
                }
            }
            if (projectile.aiStyle == 93 && nailPunch)
            {
                if (projectile.ai[0] == 0) // Tumble logic
                {
                    if (projectile.ai[1] > 20) projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f + MathHelper.ToRadians(10f) * (projectile.ai[1] - 20) * projectile.direction;
                    else if (projectile.ai[1] == 20) { projectile.damage = (int)(projectile.damage * 0.15f); projectile.ai[2] = 1; }
                    else if (projectile.velocity.Length() > 5f) projectile.velocity *= 0.85f;
                }
                else if (projectile.ai[1] == 81) projectile.damage = (int)(projectile.damage * 0.15f * 0.75f);
            }
            base.PostAI(projectile);
        }
        private static bool Magnet(Projectile magnet, Projectile searchingProj)
        {
            if (!magnet.active) return false;
            if (magnet.whoAmI == searchingProj.whoAmI) return false;
            if (magnet.type != ModContent.ProjectileType<StickingMagnetProj>()) return false;
            if (searchingProj.type == ModContent.ProjectileType<StickingMagnetProj>())
            {
                if ((searchingProj.ModProjectile as StickingMagnetProj).IsStickingToTarget) return false;
                if (!(magnet.ModProjectile as StickingMagnetProj).IsStickingToTarget) return false;
            }
            if (searchingProj.aiStyle == 93 && searchingProj.ai[0] != 0) return false;
            return true;
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
    }
}
