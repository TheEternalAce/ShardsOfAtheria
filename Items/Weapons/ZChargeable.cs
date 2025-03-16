using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons
{
    public abstract class ZChargeable : ModItem
    {
        public abstract int ZChargedItem { get; }

        public static readonly int ModuleType = ModContent.ItemType<ZChargedModule>();

        public override bool ConsumeItem(Player player)
        {
            return false;
        }

        public override bool CanRightClick()
        {
            return Main.LocalPlayer.HasItem(ModuleType) && ZChargedItem > -1 && ZChargedItem != Type;
        }

        public override void RightClick(Player player)
        {
            if (ZChargedItem != Type && ZChargedItem > -1)
            {
                bool favorited = Item.favorited;
                int reforge = Item.prefix;
                Item.SetDefaults(ZChargedItem);
                Item.stack = 1;
                Item.favorited = favorited;
                Item.Prefix(reforge);
                int i = player.FindItem(ModuleType);
                Item module = player.inventory[i];
                module.stack--;
            }
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (ZChargedItem != Type && ZChargedItem > -1) tooltips.Insert(tooltips.GetIndex("OneDropLogo"), new TooltipLine(Mod, "Zchargeable", "There's a slot to fit some kind of module..."));
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            var player = Main.LocalPlayer;
            if (ZChargedItem != Type && player.HasItem(ModuleType))
                Main.EntitySpriteDraw(SoA.OrbBloom, position, null, SoA.ElectricColorA0, 0f, SoA.OrbBloom.Size() / 2, 1f, 0);
            return base.PreDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }
    }
}
