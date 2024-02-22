using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.AnyDebuff
{
    public class Magnetic : ModBuff
    {
        public override string Texture => SoA.DebuffTemplate;

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
        }
    }

    public class MagnetizedNPC : GlobalNPC
    {
        public float magnetLife = 0;

        public override bool InstancePerEntity => true;

        public override void PostAI(NPC npc)
        {
            if (!npc.HasBuff<Magnetic>())
            {
                magnetLife = 0;
            }
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            if (npc.HasBuff<Magnetic>())
            {
                if (SoAGlobalProjectile.Metalic.ContainsKey(projectile.type))
                {
                    magnetLife -= SoAGlobalProjectile.Metalic[projectile.type];
                    if (magnetLife <= 0f)
                    {
                        npc.ClearBuff<Magnetic>();
                    }
                }
            }
        }
    }
}