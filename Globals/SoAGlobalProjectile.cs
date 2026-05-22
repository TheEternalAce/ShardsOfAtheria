using BattleNetworkElements;
using BattleNetworkElements.Utilities;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.Sinner;
using ShardsOfAtheria.Items.Weapons.Ranged;
using ShardsOfAtheria.Projectiles.Ammo;
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
        public bool canHitCoin = false;

        [ReinitializeDuringResizeArrays]
        public static class Sets
        {
            public static readonly bool?[] Areus = new bool?[ProjectileLoader.ProjectileCount];
            public static readonly bool[] TrueMelee = new bool[ProjectileLoader.ProjectileCount];
            public static readonly float[] Metalic = new float[ProjectileLoader.ProjectileCount];
        }

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

        public static readonly int[] ConvertableProjectiles =
        [
            ProjectileID.WoodenArrowFriendly,
            ProjectileID.FlamingArrow,
            ProjectileID.FrostburnArrow,
            ProjectileID.UnholyArrow,

            ProjectileID.Bullet,
            ProjectileID.SilverBullet,
            ProjectileID.MeteorShot,
        ];

        public override bool InstancePerEntity => true;

        public override void SetStaticDefaults()
        {
            #region Magnetic Melee
            Sets.Metalic[ProjectileID.Anchor] = 1f;
            Sets.Metalic[ProjectileID.BallOHurt] = 1f;
            Sets.Metalic[ProjectileID.BloodyMachete] = 1f;
            Sets.Metalic[ProjectileID.ChainGuillotine] = 1f;
            Sets.Metalic[ProjectileID.ChainKnife] = 1f;
            Sets.Metalic[ProjectileID.ChlorophyteOrb] = 1f;
            Sets.Metalic[ProjectileID.FlamingMace] = 1f;
            Sets.Metalic[ProjectileID.Mace] = 1f;
            Sets.Metalic[ProjectileID.TheMeatball] = 1f;
            #endregion
            #region Magnetic Ranged
            Sets.Metalic[ProjectileID.Bullet] = 1f;
            Sets.Metalic[ProjectileID.BulletDeadeye] = 1f;
            Sets.Metalic[ProjectileID.BulletHighVelocity] = 1f;
            Sets.Metalic[ProjectileID.BulletSnowman] = 1f;
            Sets.Metalic[ProjectileID.MoonlordBullet] = 1f;
            Sets.Metalic[ProjectileID.SilverBullet] = 1f;
            Sets.Metalic[ProjectileID.SniperBullet] = 1f;

            Sets.Metalic[ProjectileID.ChlorophyteArrow] = 1f;
            Sets.Metalic[ProjectileID.MoonlordArrow] = 1f;

            Sets.Metalic[ProjectileID.NailFriendly] = 0.01f;
            Sets.Metalic[ProjectileID.Nail] = 0.01f;
            if (ShardsHelpers.TryGetModContent("GMR", "OvercooledBullet", out ModProjectile overcooledNail))
                Sets.Metalic[overcooledNail.Type] = 0.01f;

            Sets.Metalic[ProjectileID.RocketI] = 3f;
            Sets.Metalic[ProjectileID.RocketII] = 3f;
            Sets.Metalic[ProjectileID.RocketIII] = 3f;
            Sets.Metalic[ProjectileID.RocketIV] = 3f;
            Sets.Metalic[ProjectileID.MiniNukeRocketI] = 3f;
            Sets.Metalic[ProjectileID.MiniNukeRocketII] = 3f;
            Sets.Metalic[ProjectileID.ClusterRocketI] = 3f;
            Sets.Metalic[ProjectileID.ClusterRocketII] = 3f;

            Sets.Metalic[ProjectileID.DD2JavelinHostile] = 1f;
            Sets.Metalic[ProjectileID.DD2JavelinHostileT3] = 1f;
            Sets.Metalic[ProjectileID.JavelinFriendly] = 1f;
            Sets.Metalic[ProjectileID.JavelinHostile] = 1f;
            Sets.Metalic[ProjectileID.PoisonedKnife] = 1f;
            Sets.Metalic[ProjectileID.SpikyBall] = 1f;
            Sets.Metalic[ProjectileID.ThrowingKnife] = 1f;

            Sets.Metalic[ProjectileID.PoisonDartBlowgun] = 1f;

            Sets.Metalic[ProjectileID.CopperCoin] = 1f;
            Sets.Metalic[ProjectileID.GoldCoin] = 1f;
            Sets.Metalic[ProjectileID.PlatinumCoin] = 1f;
            Sets.Metalic[ProjectileID.SilverCoin] = 1f;
            Sets.Metalic[ProjectileID.MechanicalPiranha] = 1f;
            Sets.Metalic[ProjectileID.Harpoon] = 1f;
            #endregion
            #region Magnetic Magic
            #endregion
            #region Magnetic Summon
            Sets.Metalic[ProjectileID.DD2BallistraProj] = 1f;
            #endregion
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (SoA.BNEEnabled)
                ChangeElements(projectile, source);
            if (source is EntitySource_ItemUse_WithAmmo ammoWeaponSource)
                if (ammoWeaponSource.Item.type == ModContent.ItemType<NailPounder>()) nailPunch = true;
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
            if (Sets.Metalic[type] > 0f)
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
            if (projectile.aiStyle == ProjAIStyleID.Nail && nailPunch)
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
            if (searchingProj.aiStyle == ProjAIStyleID.Nail && searchingProj.ai[0] != 0) return false;
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
            if (target.HasBuff<Hatred>()) return true;
            return base.CanHitPlayer(projectile, target);
        }
    }
}
