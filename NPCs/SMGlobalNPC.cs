using SagesMania.Items;
using SagesMania.Items.Accessories;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.NPCs
{
	public class SMGlobalNPC : GlobalNPC
	{
        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
			if (type == NPCID.ArmsDealer)
			{
				shop.item[nextSlot].SetDefaults(ModContent.ItemType<CO2Cartridge>());
				nextSlot++;
			}
		}
        public override void NPCLoot(NPC npc)
        {
			if (npc.type == NPCID.Mothron)
			{
				Item.NewItem(npc.getRect(), ModContent.ItemType<BrokenHeroGun>());
			}
		}
    }
}