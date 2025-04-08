using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.ShardsUI;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.AreusChips
{
    public class AreusArmorChip : ModItem
    {
        public const int SlotAny = -1;
        public const int SlotHead = 0;
        public const int SlotChest = 1;
        public const int SlotLegs = 2;

        public int slotType { get; internal set; } = SlotAny;

        public override string Texture => "ShardsOfAtheria/Items/AreusChips/AreusArmorChip";
        static readonly Texture2D BaseChip = ModContent.Request<Texture2D>("ShardsOfAtheria/Items/AreusChips/AreusArmorChip").Value;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 25;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;

            Item.maxStack = 9999;
            Item.rare = ItemDefaults.RarityDungeon;
            Item.value = ItemDefaults.ValueEarlyHardmode;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            string chipType = "";
            switch (slotType)
            {
                case SlotAny:
                    chipType = "Any slot";
                    break;
                case SlotHead:
                    chipType = "Head slot";
                    break;
                case SlotChest:
                    chipType = "Chest slot";
                    break;
                case SlotLegs:
                    chipType = "Leg slot";
                    break;
            }
            TooltipLine line = new(Mod, "ChipType", chipType);
            tooltips.Insert(tooltips.GetIndex("OneDropLogo"), line);
        }

        public override bool CanRightClick()
        {
            var player = Main.LocalPlayer;
            var areus = player.Areus();
            return Type != ModContent.ItemType<AreusArmorChip>() && areus.AreusArmorPiece;
        }

        //public override bool ConsumeItem(Player player)
        //{
        //    return Type != ModContent.ItemType<AreusArmorChip>();
        //}

        public override void RightClick(Player player)
        {
            var areusPlayer = player.Areus();
            if (areusPlayer.AreusArmorPiece)
            {
                for (int i = 0; i < 3; i++)
                {
                    string chipName = areusPlayer.chipNames[i];
                    if (slotType == i)
                    {
                        if (chipName != "")
                        {
                            var modItem = SoA.Instance.Find<ModItem>(chipName);
                            int inventoryIndex = player.FindItem(Type);
                            player.inventory[inventoryIndex] = new(modItem.Type);
                        }
                        areusPlayer.chipNames[i] = Name;
                        ModContent.GetInstance<ChipsUISystem>().SetSlotItem(i, new(Type));
                        break;
                    }
                    else if (slotType == SlotAny)
                    {
                        int slotIndex = 0;
                        if (areusPlayer.chipNames[0] == "")
                        {
                            slotIndex = 0;
                        }
                        else if (areusPlayer.chipNames[1] == "")
                        {
                            slotIndex = 1;
                        }
                        else if (areusPlayer.chipNames[2] == "")
                        {
                            slotIndex = 2;
                        }
                        else
                        {
                            var modItem = SoA.Instance.Find<ModItem>(chipName);
                            int inventoryIndex = player.FindItem(Type);
                            player.inventory[inventoryIndex] = new(modItem.Type);
                        }
                        areusPlayer.chipNames[slotIndex] = Name;
                        ModContent.GetInstance<ChipsUISystem>().SetSlotItem(slotIndex, new(Type));
                        break;
                    }
                }
            }
        }

        public virtual void UpdateChip(Player player)
        {
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            spriteBatch.Draw(BaseChip, position, frame, drawColor, 0f, origin, scale, SpriteEffects.None, 0f);
            return base.PreDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            var frame = ShardsHelpers.Frame(BaseChip.Bounds, 0, 0);
            var origin = BaseChip.Size() / 2f;
            spriteBatch.Draw(BaseChip, Item.Center - Main.screenPosition, frame, lightColor, 0f, origin, scale, SpriteEffects.None, 0f);
            return base.PreDrawInWorld(spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
        }
    }
}