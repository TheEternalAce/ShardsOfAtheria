using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Items.Potions;
using ShardsOfAtheria.Utilities;
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

        public override bool InstancePerEntity => true;

        public override void SetDefaults(Projectile proj)
        {
            int type = proj.type;

            if (ModContent.GetInstance<ShardsConfigServerSide>().experimental)
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
                    // Rocket
                    // Magic

                    // Melee
                    case ProjectileID.IceBolt:
                        IceProj.Add(type);
                        break;

                    // Arrow
                    case ProjectileID.MoonlordArrow:

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
                    case ProjectileID.BulletHighVelocity:

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
            if (ModContent.GetInstance<ShardsConfigServerSide>().experimental && Main.rand.NextBool(3))
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
                        if (Main.player[projectile.owner].HasBuff(ModContent.BuffType<Conductive>()))
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
                        if (Main.player[projectile.owner].HasBuff(ModContent.BuffType<Conductive>()))
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
            if (ModContent.GetInstance<ShardsConfigServerSide>().experimental)
            {
                if (source is EntitySource_Parent parentSource && parentSource.Entity is NPC npc)
                {
                    int sourceEnemy = npc.whoAmI;
                    if (SoAGlobalNPC.FireNPC.Contains(sourceEnemy))
                    {
                        if (!FireProj.Contains(type))
                        {
                            FireProj.Add(type);
                        }
                        tempFireProj = true;
                    }
                    if (SoAGlobalNPC.IceNPC.Contains(sourceEnemy))
                    {
                        if (!IceProj.Contains(type))
                        {
                            IceProj.Add(type);
                        }
                        tempIceProj = true;
                    }
                    if (SoAGlobalNPC.ElectricNPC.Contains(sourceEnemy))
                    {
                        if (!ElectricProj.Contains(type))
                        {
                            ElectricProj.Add(type);
                        }
                        tempElectricProj = true;
                    }
                    if (SoAGlobalNPC.MetalNPC.Contains(sourceEnemy))
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
                    int sourceProj = proj.whoAmI;
                    if (FireProj.Contains(sourceProj))
                    {
                        if (!FireProj.Contains(type))
                        {
                            FireProj.Add(type);
                        }
                        tempFireProj = true;
                    }
                    if (IceProj.Contains(sourceProj))
                    {
                        if (!IceProj.Contains(type))
                        {
                            IceProj.Add(type);
                        }
                        tempIceProj = true;
                    }
                    if (ElectricProj.Contains(sourceProj))
                    {
                        if (!ElectricProj.Contains(type))
                        {
                            ElectricProj.Add(type);
                        }
                        tempElectricProj = true;
                    }
                    if (MetalProj.Contains(sourceProj))
                    {
                        if (!MetalProj.Contains(type))
                        {
                            MetalProj.Add(type);
                        }
                        tempMetalProj = true;
                    }
                }
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
