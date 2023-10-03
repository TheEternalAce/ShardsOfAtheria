using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SinfulSouls
{
    public abstract class SinfulSouls : SinfulItem
    {
        public virtual int SoulType => 0;

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.consumable = true;

            Item.useTime = Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;

            Item.rare = ItemDefaults.RaritySinful;
            Item.value = ItemDefaults.ValueEyeOfCthulhu;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "SevenSoul", Language.GetTextValue("Mods.ShardsOfAtheria.Common.SinfulSoulCommonDesc",
                Language.GetTextValue("Mods.ShardsOfAtheria.ItemName.VirtuousDagger"))));
            base.ModifyTooltips(tooltips);
        }

        public override bool CanUseItem(Player player)
        {
            return player.GetModPlayer<SinfulPlayer>().SevenSoulUsed == 0;
        }

        public override bool? UseItem(Player player)
        {
            player.ClearBuff(SoulType);
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SinfulSoul>())
                .Register();
        }
    }

    public abstract class SinfulSoulBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoTimeDisplay[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
            base.SetStaticDefaults();
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.Sinful().SevenSoulUsed = Type;
            player.buffTime[buffIndex] = 18000;
            base.Update(player, ref buffIndex);
        }
    }

    public class SinfulSoul : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 52;
            Item.maxStack = 9999;
        }
    }
}
