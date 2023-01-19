using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.AnyDebuff
{
    public class ElectricShock : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<ShockedPlayer>().shocked = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<ShockedNPC>().shocked = true;
        }
    }

    public class ShockedNPC : GlobalNPC
    {
        public bool shocked;

        public override bool InstancePerEntity => true;

        public override void ResetEffects(NPC npc)
        {
            shocked = false;
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (shocked)
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 10;
                damage = 10;
            }
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (shocked && Main.rand.NextBool(8))
            {
                int dust = Dust.NewDust(npc.position, npc.width + 4, npc.height + 4, DustID.Electric, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default, 1f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 1.8f;
                Main.dust[dust].velocity.Y -= 0.5f;
            }
        }
    }

    public class ShockedPlayer : ModPlayer
    {
        public bool shocked;

        public override void ResetEffects()
        {
            shocked = false;
        }

        public override void UpdateBadLifeRegen()
        {
            if (shocked)
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;
                if (Main.keyState.IsKeyDown(Keys.Left) || Main.keyState.IsKeyDown(Keys.Right))
                {
                    // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 10 life lost per second, if the player is holding their left or right movement keys.
                    Player.lifeRegen -= 20;
                }
                else
                {
                    Player.lifeRegen -= 2; // This effect causes 1 life lost per second otherwise.
                }
            }
        }

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (shocked && Main.rand.NextBool(8))
            {
                int dust = Dust.NewDust(Player.position, Player.width + 4, Player.height + 4, DustID.Electric, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default, 1f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 1.8f;
                Main.dust[dust].velocity.Y -= 0.5f;
            }
        }
    }
}
