using BattleNetworkElements;
using BattleNetworkElements.Utilities;
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

        public static List<int> AreusProj = new List<int>();
        public static List<int> DarkAreusProj = new List<int>();
        public static List<int> Eraser = new List<int>();

        public static List<int> ReflectAiList = new List<int>
        {
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
        private void ChangeElements(Projectile projectile, IEntitySource source)
        {
            foreach (Player player in Main.player)
            {
                if (projectile.active)
                {
                    if (projectile.owner == player.whoAmI)
                    {
                        if (player.active && !player.dead && projectile.friendly)
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
                        if (projectile.Hitbox.Intersects(proj.Hitbox) && proj != projectile && proj.hostile)
                        {
                            proj.Kill();
                        }
                    }
                }
            }
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (AreusProj.Contains(projectile.type) || tempAreus)
            {
                int buffTime = 600;
                if (Main.player[projectile.owner].Shards().conductive)
                {
                    buffTime *= 2;
                }
                if (Main.hardMode)
                {
                    target.AddBuff(BuffID.Electrified, buffTime);
                }
                else
                {
                    target.AddBuff(ModContent.BuffType<ElectricShock>(), buffTime);
                }
            }
        }
    }
}
