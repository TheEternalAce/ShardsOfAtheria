using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Utilities
{
    public partial class ShardsHelpers
    {
        public static bool HasBuff<T>(this Entity entity) where T : ModBuff
        {
            return HasBuff(entity, ModContent.BuffType<T>());
        }
        public static bool HasBuff(this Entity entity, int type)
        {
            if (entity is NPC npc) return npc.HasBuff(type);
            else if (entity is Player player) return player.HasBuff(type);
            return false;
        }

        public static void AddBuff<T>(this Entity entity, int buffTime, bool quiet = false) where T : ModBuff
        {
            AddBuff(entity, ModContent.BuffType<T>(), buffTime, quiet);
        }
        public static void AddBuff(this Entity entity, int buffType, int buffTime, bool quiet = false)
        {
            if (entity is NPC npc) npc.AddBuff(buffType, buffTime, quiet);
            else if (entity is Player player) player.AddBuff(buffType, buffTime, quiet);
        }

        public static void ClearBuff<T>(this Entity entity) where T : ModBuff
        {
            ClearBuff(entity, ModContent.BuffType<T>());
        }
        public static void ClearBuff(this Entity entity, int buffType)
        {
            if (entity.HasBuff(buffType))
            {
                if (entity is NPC npc) npc.DelBuff(npc.FindBuffIndex(buffType));
                else if (entity is Player player) player.ClearBuff(buffType);
            }
        }
    }
}
