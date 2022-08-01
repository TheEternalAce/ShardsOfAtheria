using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace ShardsOfAtheria.Items
{
	public class MemoryFragmentI : ModItem
	{
        public override string Texture => "ShardsOfAtheria/Items/MemoryFragment";

        public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("Can be used to upgrade Genesis and Ragnarok\n" +
                "'Memories broken the truth goes unspoken'\n" +
                "'The faint voice of a young boy echoes from within'");
			ItemID.Sets.ItemIconPulse[Item.type] = true;
			ItemID.Sets.ItemNoGravity[Item.type] = true;

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 5;
		}

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 44;
            Item.maxStack = 999;

            Item.rare = ItemRarityID.Blue;
			Item.value = Item.sellPrice(0, 1);
		}

        public override void PostUpdate()
		{
			Lighting.AddLight(Item.Center, Color.WhiteSmoke.ToVector3() * 1f * Main.essScale);
		}
    }

    public class MemoryFragmentII : MemoryFragmentI
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Memory Fragment II");
            base.SetStaticDefaults();
        }
    }

    public class MemoryFragmentIII : MemoryFragmentI
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Memory Fragment III");
            base.SetStaticDefaults();
        }
    }

    public class MemoryFragmentIV : MemoryFragmentI
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Memory Fragment IV");
            base.SetStaticDefaults();
        }
    }

    public class MemoryFragmentV : MemoryFragmentI
    {

    }
}
