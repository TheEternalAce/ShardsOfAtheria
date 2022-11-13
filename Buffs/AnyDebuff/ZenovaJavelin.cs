using ShardsOfAtheria.Projectiles.Weapon.Melee;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.AnyDebuff
{
    public class ZenovaJavelin : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense -= 26;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.defense -= 26;
        }
    }

    public class ZenJavelinNPC : GlobalNPC
    {
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

    public class ZenJavelinPlayer : ModPlayer
    {
        public override void UpdateBadLifeRegen()
        {
            if (Player.HasBuff(ModContent.BuffType<ZenovaJavelin>()))
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 50 life lost per second.
                Player.lifeRegen -= 100;
            }
        }
    }
}
