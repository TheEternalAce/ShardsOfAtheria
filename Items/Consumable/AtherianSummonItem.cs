using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.NPCs.Town.TheAtherian;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Consumable
{
    public class AtherianSummonItem : ModItem
    {
        public override string Texture => SoA.PlaceholderTexture;

        public override void SetStaticDefaults()
        {
            Item.AddElement(2);
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = 9999;

            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item37;
            Item.consumable = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.rare = ItemDefaults.RarityDungeon;
            Item.makeNPC = ModContent.NPCType<Atherian>();
        }

        public override void HoldItem(Player player)
        {
            Player.tileRangeX += 600;
            Player.tileRangeY += 600;
        }

        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<Atherian>());
        }

        public override void OnConsumeItem(Player player)
        {
            Main.NewText("An Atherian is spawned.", 255, 255, 255);
        }
    }
}
