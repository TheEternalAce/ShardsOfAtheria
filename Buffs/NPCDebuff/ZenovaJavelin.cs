using ShardsOfAtheria.Projectiles.Melee;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.AnyDebuff
{
    public class ZenovaJavelin : ModBuff
    {
        public override string Texture => SoA.DebuffTemplate;

        public static readonly int DefenseReduction = 26;

        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense -= DefenseReduction;
        }
    }

    public class ZenJavelinNPC : GlobalNPC
    {
        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
        {
            if (npc.HasBuff<ZenovaJavelin>())
            {
                modifiers.Defense.Flat -= ZenovaJavelin.DefenseReduction;
            }
            base.ModifyIncomingHit(npc, ref modifiers);
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (npc.HasBuff(ModContent.BuffType<ZenovaJavelin>()))
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                int exampleJavelinCount = 0;
                for (int i = 0; i < 1000; i++)
                {
                    Projectile p = Main.projectile[i];
                    if (p.active && p.type == ModContent.ProjectileType<ZenovaProjectile>() && p.ai[0] == 1f && p.ai[1] == npc.whoAmI)
                    {
                        exampleJavelinCount++;
                    }
                }
                npc.lifeRegen -= exampleJavelinCount * 2 * 50;
                if (damage < exampleJavelinCount * 50)
                {
                    damage = exampleJavelinCount * 50;
                }
            }
        }
    }
}
