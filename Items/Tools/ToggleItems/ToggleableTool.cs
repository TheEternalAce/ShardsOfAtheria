using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ShardsOfAtheria.Items.Tools.ToggleItems
{
    public abstract class ToggleableTool : ModItem
    {
        public int mode = 0;
        public int maxMode = 1;
        public bool Active => mode > 0;

        public override void SaveData(TagCompound tag)
        {
            tag["mode"] = mode;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("mode")) mode = tag.GetInt("mode");
        }

        public override bool CanRightClick()
        {
            return true;
        }
        public override bool ConsumeItem(Player player)
        {
            return false;
        }

        public override void RightClick(Player player)
        {
            if (++mode > maxMode) mode = 0;
        }

        public static ToggleableTool GetInstance<T>(Player player) where T : ModItem
        {
            ToggleableTool tool = null;
            int type = ModContent.ItemType<T>();
            foreach (Item item in player.inventory)
            {
                if (item.type == type) { tool = item.ModItem as ToggleableTool; break; }
            }
            foreach (Item item in player.bank.item)
            {
                if (item.type == type) { tool = item.ModItem as ToggleableTool; break; }
            }
            foreach (Item item in player.bank2.item)
            {
                if (item.type == type) { tool = item.ModItem as ToggleableTool; break; }
            }
            foreach (Item item in player.bank3.item)
            {
                if (item.type == type) { tool = item.ModItem as ToggleableTool; break; }
            }
            foreach (Item item in player.bank4.item)
            {
                if (item.type == type) { tool = item.ModItem as ToggleableTool; break; }
            }
            return tool;
        }
    }
}
