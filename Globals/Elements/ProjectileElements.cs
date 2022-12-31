using ShardsOfAtheria.Config;
using ShardsOfAtheria.Items.Potions;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Globals.Elements
{
    public class ProjectileElements : GlobalProjectile
    {
        public static List<int> Fire = new();
        public static List<int> Ice = new();
        public static List<int> Electric = new();
        public static List<int> Metal = new();
        public static List<int> AreusProj = new();

        public bool tempFire = false;
        public bool tempIce = false;
        public bool tempElectric = false;
        public bool tempMetal = false;

        public bool conductive = false;

        public override bool InstancePerEntity => true;

        public override void SetDefaults(Projectile proj)
        {
            int type = proj.type;

            if (ModContent.GetInstance<ShardsServerConfig>().experimental)
            {
                switch (type)
                {
                    // Rocket
                    case ProjectileID.RocketI:
                    case ProjectileID.RocketII:
                    case ProjectileID.RocketIII:
                    case ProjectileID.RocketIV:
                    case ProjectileID.ClusterRocketI:
                    case ProjectileID.ClusterRocketII:
                    case ProjectileID.DryRocket:
                    case ProjectileID.LavaRocket:
                    case ProjectileID.MiniNukeRocketI:
                    case ProjectileID.MiniNukeRocketII:

                    case ProjectileID.RocketSnowmanI:
                    case ProjectileID.RocketSnowmanII:
                    case ProjectileID.RocketSnowmanIII:
                    case ProjectileID.RocketSnowmanIV:
                    case ProjectileID.ClusterSnowmanRocketI:
                    case ProjectileID.ClusterSnowmanRocketII:
                    case ProjectileID.DrySnowmanRocket:
                    case ProjectileID.LavaSnowmanRocket:
                    case ProjectileID.MiniNukeSnowmanRocketI:
                    case ProjectileID.MiniNukeSnowmanRocketII:

                    case ProjectileID.GrenadeI:
                    case ProjectileID.GrenadeII:
                    case ProjectileID.GrenadeIII:
                    case ProjectileID.GrenadeIV:
                    case ProjectileID.ClusterGrenadeI:
                    case ProjectileID.ClusterGrenadeII:
                    case ProjectileID.DryGrenade:
                    case ProjectileID.LavaGrenade:
                    case ProjectileID.MiniNukeGrenadeI:
                    case ProjectileID.MiniNukeGrenadeII:

                    case ProjectileID.ProximityMineI:
                    case ProjectileID.ProximityMineII:
                    case ProjectileID.ProximityMineIII:
                    case ProjectileID.ProximityMineIV:
                    case ProjectileID.ClusterMineI:
                    case ProjectileID.ClusterMineII:
                    case ProjectileID.DryMine:
                    case ProjectileID.LavaMine:
                    case ProjectileID.MiniNukeMineI:
                    case ProjectileID.MiniNukeMineII:

                    case ProjectileID.RocketFireworkRed:
                    case ProjectileID.RocketFireworkGreen:
                    case ProjectileID.RocketFireworkBlue:
                    case ProjectileID.RocketFireworkYellow:

                    case ProjectileID.Celeb2Rocket:
                    case ProjectileID.Celeb2RocketExplosive:
                    case ProjectileID.Celeb2RocketLarge:
                    case ProjectileID.Celeb2RocketExplosiveLarge:

                    // Magic
                    case ProjectileID.Spark:

                    // Melee
                    //case ProjectileID.BladeofGrassLeaf:
                    case ProjectileID.ThornChakram:
                        Fire.Add(type);
                        break;

                    // Rocket
                    case ProjectileID.WetRocket:
                    case ProjectileID.HoneyRocket:
                    case ProjectileID.WetSnowmanRocket:
                    case ProjectileID.HoneySnowmanRocket:
                    case ProjectileID.WetGrenade:
                    case ProjectileID.HoneyGrenade:
                    case ProjectileID.WetMine:
                    case ProjectileID.HoneyMine:

                    // Other ammo
                    case ProjectileID.PoisonDart:
                    case ProjectileID.IchorDart:

                    // Melee
                    case ProjectileID.IceBolt:

                    // Other projectile
                    case ProjectileID.PoisonDartBlowgun:
                    case ProjectileID.PoisonDartTrap:
                        Ice.Add(type);
                        break;

                    // Rocket
                    case ProjectileID.ElectrosphereMissile:
                    case ProjectileID.Electrosphere:

                    // Magic
                    case ProjectileID.ThunderSpear:
                    case ProjectileID.ThunderSpearShot:
                    case ProjectileID.ThunderStaffShot:
                        Electric.Add(type);
                        break;

                    // Melee
                    case ProjectileID.TheRottedFork:
                    case ProjectileID.TheMeatball:
                    case ProjectileID.BallOHurt:
                        Metal.Add(type);
                        break;
                }
            }
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            int type = projectile.type;
            if (ModContent.GetInstance<ShardsServerConfig>().experimental)
            {
                if (Main.player[projectile.owner].HasBuff(ModContent.BuffType<Conductive>()) || Main.npc[projectile.owner].HasBuff(ModContent.BuffType<Conductive>()))
                {
                    conductive = true;
                }
                if (source is EntitySource_Parent parentSource && parentSource.Entity is NPC npc)
                {
                    int sourceEnemy = npc.type;
                    if (NPCElements.FireNPC.Contains(sourceEnemy))
                    {
                        tempFire = true;
                    }
                    if (NPCElements.IceNPC.Contains(sourceEnemy))
                    {
                        tempIce = true;
                    }
                    if (NPCElements.ElectricNPC.Contains(sourceEnemy))
                    {
                        tempElectric = true;
                    }
                    if (NPCElements.MetalNPC.Contains(sourceEnemy))
                    {
                        tempMetal = true;
                    }
                }
                if (source is EntitySource_Parent parentSourceProj && parentSourceProj.Entity is Projectile proj)
                {
                    int sourceProjType = proj.type;
                    if (Fire.Contains(sourceProjType))
                    {
                        tempFire = true;
                    }
                    if (Ice.Contains(sourceProjType))
                    {
                        tempIce = true;
                    }
                    if (Electric.Contains(sourceProjType))
                    {
                        tempElectric = true;
                    }
                    if (Metal.Contains(sourceProjType))
                    {
                        tempMetal = true;
                    }
                }
                //if (FireProj.Contains(type))
                //{
                //    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Fire Projectile: " + projectile.Name), Color.White);
                //}
                //if (IceProj.Contains(type))
                //{
                //    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Ice Projectile: " + projectile.Name), Color.White);
                //}
                //if (ElectricProj.Contains(type))
                //{
                //    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Electric Projectile: " + projectile.Name), Color.White);
                //}
                //if (MetalProj.Contains(type))
                //{
                //    ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Metal Projectile: " + projectile.Name), Color.White);
                //}
            }
        }

        public override void Kill(Projectile projectile, int timeLeft)
        {
            if (tempFire)
            {
                Fire.Remove(projectile.type);
            }
            if (tempIce)
            {
                Ice.Remove(projectile.type);
            }
            if (tempElectric)
            {
                Electric.Remove(projectile.type);
            }
            if (tempMetal)
            {
                Metal.Remove(projectile.type);
            }
        }
    }
}
