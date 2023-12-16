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

        public static readonly Dictionary<int, bool> AreusProj = new();
        public static readonly List<int> Eraser = new();

        public static readonly List<int> ReflectAiList = new()
        {
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
        };

        public override bool InstancePerEntity => true;

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (SoA.ElementModEnabled)
            {
                ChangeElements(projectile, source);
            }
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
