using ShardsOfAtheria.ItemDropRules.Conditions;
using ShardsOfAtheria.Items.Weapons.Ammo;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ShardsOfAtheria.Items
{
    public class AmmoBag_Arrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ammo Bag (Arrow)");
            Tooltip.SetDefault("Gives a stack of a random arrow type");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 10;
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 22;
            Item.maxStack = 9999;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(0, 5);
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            return;
            LeadingConditionRule isHardmode = new LeadingConditionRule(new Conditions.IsHardmode());
            LeadingConditionRule downedSkeletron = new LeadingConditionRule(new Conditions.IsHardmode());
            LeadingConditionRule downedMoonLord = new LeadingConditionRule(new DownedMoonLord());

            int[] preHardmodeArrowDrop = { ItemID.WoodenArrow, ItemID.FlamingArrow, ItemID.FrostburnArrow, ItemID.BoneArrow, ItemID.JestersArrow, ItemID.UnholyArrow };
            int[] postSkeletronArrowDrop = { ItemID.HellfireArrow };
            int[] hardmodeArrowDrop = { ItemID.ChlorophyteArrow, ItemID.CursedArrow, ItemID.HolyArrow, ItemID.IchorArrow, ItemID.VenomArrow, ModContent.ItemType<SpectralArrow>() };
            int[] postMoonLordArrowDrop = { ItemID.MoonlordArrow };

            IItemDropRule preHardmodeArrow = ItemDropRule.OneFromOptions(1, preHardmodeArrowDrop);
            IItemDropRule postSkeletronArrow = ItemDropRule.OneFromOptions(1, postSkeletronArrowDrop);
            IItemDropRule hardmodeArrow = ItemDropRule.OneFromOptions(1, hardmodeArrowDrop);
            IItemDropRule postMoonLordArrow = ItemDropRule.OneFromOptions(1, postMoonLordArrowDrop);

            isHardmode.OnSuccess(hardmodeArrow);
            downedSkeletron.OnSuccess(postSkeletronArrow);
            downedMoonLord.OnSuccess(postMoonLordArrow);

            IItemDropRule arrowDrop = new OneFromRulesRule(1, preHardmodeArrow, isHardmode, downedSkeletron, downedMoonLord);

            itemLoot.Add(arrowDrop);
        }

        public override void RightClick(Player player)
        {
            var ammoChooser = new WeightedRandom<int>();
            var source = player.GetSource_OpenItem(Type);

            ammoChooser.Add(ItemID.WoodenArrow);
            ammoChooser.Add(ItemID.FlamingArrow);
            ammoChooser.Add(ItemID.FrostburnArrow);
            ammoChooser.Add(ItemID.BoneArrow);
            ammoChooser.Add(ItemID.JestersArrow);
            ammoChooser.Add(ItemID.UnholyArrow);

            if (NPC.downedBoss3)
            {
                ammoChooser.Add(ItemID.HellfireArrow);
            }

            if (Main.hardMode)
            {
                ammoChooser.Add(ItemID.ChlorophyteArrow);
                ammoChooser.Add(ItemID.CursedArrow);
                ammoChooser.Add(ItemID.HolyArrow);
                ammoChooser.Add(ItemID.IchorArrow);
                ammoChooser.Add(ItemID.VenomArrow);
            }

            if (NPC.downedMoonlord)
            {
                ammoChooser.Add(ItemID.MoonlordArrow);
            }

            Main.LocalPlayer.QuickSpawnItem(source, ammoChooser, 9999);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AmmoBag>())
                .AddIngredient(ItemID.WoodenArrow, 100)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}