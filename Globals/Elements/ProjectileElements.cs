using ShardsOfAtheria.Buffs.AnyDebuff;
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
        public static List<int> FireProj = new();
        public static List<int> IceProj = new();
        public static List<int> ElectricProj = new();
        public static List<int> MetalProj = new();
        public static List<int> AreusProj = new();

        public bool tempFireProj = false;
        public bool tempIceProj = false;
        public bool tempElectricProj = false;
        public bool tempMetalProj = false;
        public bool tempAreusProj = false;

        public bool conductive = false;

        public override bool InstancePerEntity => true;

        public override void SetDefaults(Projectile proj)
        {
            int type = proj.type;

            if (ModContent.GetInstance<ShardsServerSideConfig>().experimental)
            {
                switch (type)
                {
                    // Arrow
                    case ProjectileID.FlamingArrow:
                    case ProjectileID.JestersArrow:
                    case ProjectileID.HellfireArrow:
                    case ProjectileID.CursedArrow:
                    case ProjectileID.VenomArrow:

                    // Bullet
                    case ProjectileID.MeteorShot:
                    case ProjectileID.CursedBullet:
                    case ProjectileID.ExplosiveBullet:

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

                    // Other ammo
                    case ProjectileID.CursedDart:
                    case ProjectileID.JackOLantern:

                    // Magic
                    case ProjectileID.Spark:

                    // Melee
                    //case ProjectileID.BladeofGrassLeaf:
                    case ProjectileID.ThornChakram:
                        FireProj.Add(type);
                        break;

                    // Arrow
                    case ProjectileID.UnholyArrow:
                    case ProjectileID.HolyArrow:
                    case ProjectileID.IchorArrow:

                    // Bullet
                    case ProjectileID.IchorBullet:
                    case ProjectileID.VenomBullet:

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
                        IceProj.Add(type);
                        break;

                    // Arrow
                    case ProjectileID.MoonlordArrow:

                    // Bullet
                    case ProjectileID.NanoBullet:
                    case ProjectileID.MoonlordBullet:

                    // Rocket
                    case ProjectileID.ElectrosphereMissile:
                    case ProjectileID.Electrosphere:

                    // Magic
                    case ProjectileID.ThunderSpear:
                    case ProjectileID.ThunderSpearShot:
                    case ProjectileID.ThunderStaffShot:
                        ElectricProj.Add(type);
                        break;

                    // Arrow
                    case ProjectileID.WoodenArrowFriendly:
                    case ProjectileID.ChlorophyteArrow:
                    case ProjectileID.BoneArrow:
                    //case ProjectileID.ShimmerArrow:

                    // Bullet
                    case ProjectileID.Bullet:
                    case ProjectileID.CrystalBullet:
                    case ProjectileID.ChlorophyteBullet:
                    case ProjectileID.BulletHighVelocity:
                    case ProjectileID.GoldenBullet:

                    // Other ammo
                    case ProjectileID.CrystalDart:

                    // Melee
                    case ProjectileID.TheRottedFork:
                    case ProjectileID.TheMeatball:
                    case ProjectileID.BallOHurt:
                        MetalProj.Add(type);
                        break;

                    // Arrow
                    case ProjectileID.FrostburnArrow:
                        FireProj.Add(type);
                        IceProj.Add(type);
                        break;
                }
            }
        }

        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            if (ModContent.GetInstance<ShardsServerSideConfig>().experimental && Main.rand.NextBool(3))
            {
                int buffTime = 600;
                if (Main.hardMode)
                {
                    if (FireProj.Contains(projectile.type))
                    {
                        target.AddBuff(BuffID.OnFire3, buffTime);
                    }
                    if (IceProj.Contains(projectile.type))
                    {
                        target.AddBuff(BuffID.Frostburn2, buffTime);
                    }
                    if (ElectricProj.Contains(projectile.type))
                    {
                        if (conductive)
                        {
                            buffTime *= 2;
                        }
                        target.AddBuff(BuffID.Electrified, buffTime);
                    }
                }
                else
                {
                    if (FireProj.Contains(projectile.type))
                    {
                        target.AddBuff(BuffID.OnFire, buffTime);
                    }
                    if (IceProj.Contains(projectile.type))
                    {
                        target.AddBuff(BuffID.Frostburn, buffTime);
                    }
                    if (ElectricProj.Contains(projectile.type))
                    {
                        if (conductive)
                        {
                            buffTime *= 2;
                        }
                        target.AddBuff(ModContent.BuffType<ElectricShock>(), buffTime);
                    }
                }
            }
        }

        public override void OnHitPlayer(Projectile projectile, Player target, int damage, bool crit)
        {
            if (ModContent.GetInstance<ShardsServerSideConfig>().experimental && Main.rand.NextBool(3))
            {
                int buffTime = 600;
                if (Main.hardMode)
                {
                    if (FireProj.Contains(projectile.type))
                    {
                        target.AddBuff(BuffID.OnFire3, buffTime);
                    }
                    if (IceProj.Contains(projectile.type))
                    {
                        target.AddBuff(BuffID.Frostburn2, buffTime);
                    }
                    if (ElectricProj.Contains(projectile.type))
                    {
                        if (conductive)
                        {
                            buffTime *= 2;
                        }
                        target.AddBuff(BuffID.Electrified, buffTime);
                    }
                }
                else
                {
                    if (FireProj.Contains(projectile.type))
                    {
                        target.AddBuff(BuffID.OnFire, buffTime);
                    }
                    if (IceProj.Contains(projectile.type))
                    {
                        target.AddBuff(BuffID.Frostburn, buffTime);
                    }
                    if (ElectricProj.Contains(projectile.type))
                    {
                        if (conductive)
                        {
                            buffTime *= 2;
                        }
                        target.AddBuff(ModContent.BuffType<ElectricShock>(), buffTime);
                    }
                }
            }
        }

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            int type = projectile.type;
            if (ModContent.GetInstance<ShardsServerSideConfig>().experimental)
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
                        if (!FireProj.Contains(type))
                        {
                            FireProj.Add(type);
                        }
                        tempFireProj = true;
                    }
                    if (NPCElements.IceNPC.Contains(sourceEnemy))
                    {
                        if (!IceProj.Contains(type))
                        {
                            IceProj.Add(type);
                        }
                        tempIceProj = true;
                    }
                    if (NPCElements.ElectricNPC.Contains(sourceEnemy))
                    {
                        if (!ElectricProj.Contains(type))
                        {
                            ElectricProj.Add(type);
                        }
                        tempElectricProj = true;
                    }
                    if (NPCElements.MetalNPC.Contains(sourceEnemy))
                    {
                        if (!MetalProj.Contains(type))
                        {
                            MetalProj.Add(type);
                        }
                        tempMetalProj = true;
                    }
                }
                if (source is EntitySource_Parent parentSourceProj && parentSourceProj.Entity is Projectile proj)
                {
                    int sourceProjType = proj.type;
                    if (FireProj.Contains(sourceProjType))
                    {
                        if (!FireProj.Contains(type))
                        {
                            FireProj.Add(type);
                        }
                        tempFireProj = true;
                    }
                    if (IceProj.Contains(sourceProjType))
                    {
                        if (!IceProj.Contains(type))
                        {
                            IceProj.Add(type);
                        }
                        tempIceProj = true;
                    }
                    if (ElectricProj.Contains(sourceProjType))
                    {
                        if (!ElectricProj.Contains(type))
                        {
                            ElectricProj.Add(type);
                        }
                        tempElectricProj = true;
                    }
                    if (MetalProj.Contains(sourceProjType))
                    {
                        if (!MetalProj.Contains(type))
                        {
                            MetalProj.Add(type);
                        }
                        tempMetalProj = true;
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
            if (tempFireProj)
            {
                FireProj.Remove(projectile.type);
            }
            if (tempIceProj)
            {
                IceProj.Remove(projectile.type);
            }
            if (tempElectricProj)
            {
                ElectricProj.Remove(projectile.type);
            }
            if (tempMetalProj)
            {
                MetalProj.Remove(projectile.type);
            }
        }
    }
}
