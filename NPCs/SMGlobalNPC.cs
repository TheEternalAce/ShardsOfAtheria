using SagesMania.Buffs;
using SagesMania.Items;
using SagesMania.Items.Accessories;
using SagesMania.Items.Weapons;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.NPCs
{
	public class SMGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public bool infected;
        public bool zenovaJavelin;

        public override void ResetEffects(NPC npc)
        {
            infected = false;
            zenovaJavelin = false;
        }

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
			if (npc.type == NPCID.Mothron && Main.rand.NextFloat() < 0.25f)
			{
				Item.NewItem(npc.getRect(), ModContent.ItemType<BrokenHeroGun>());
            }
            if (npc.type == NPCID.MartianSaucerCore && Main.rand.NextFloat() < 0.25f)
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<ReactorMeltdown>());
            }
            if (Main.dayTime && Main.rand.NextFloat() < .2f && Main.LocalPlayer.ZoneOverworldHeight && !(Main.LocalPlayer.ZoneCorrupt || Main.LocalPlayer.ZoneCrimson && Main.eclipse))
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<SoulOfDaylight>());
            }
            if (!Main.dayTime && Main.rand.NextFloat() < .2f && Main.LocalPlayer.ZoneOverworldHeight && !(Main.LocalPlayer.ZoneCorrupt || Main.LocalPlayer.ZoneCrimson && Main.eclipse))
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<SoulOfStarlight>());
            }
            if (Main.eclipse && Main.rand.NextFloat() < .2f && Main.LocalPlayer.ZoneOverworldHeight && !(Main.LocalPlayer.ZoneCorrupt || Main.LocalPlayer.ZoneCrimson))
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<SoulOfDaylight>());
                Item.NewItem(npc.getRect(), ModContent.ItemType<SoulOfStarlight>());
            }
            if (!Main.dayTime && Main.rand.NextFloat() < .2f && Main.LocalPlayer.ZoneUnderworldHeight && !(Main.LocalPlayer.ZoneHoly))
            {
                Item.NewItem(npc.getRect(), ModContent.ItemType<SoulOfSpite>());
            }
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (infected)
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second.
                npc.lifeRegen -= 10;
            }
            if (zenovaJavelin)
            {
                // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects.
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                // lifeRegen is measured in 1/2 life per second. Therefore, this effect causes 8 life lost per second.
                npc.lifeRegen -= 100;
            }
        }

        public override void GetChat(NPC npc, ref string chat)
        {
            if (Main.LocalPlayer.HasBuff(ModContent.BuffType<Megamerged>()))
            {
                switch (Main.rand.Next(3))
                {
                    case 0:
                        chat = "Huh? Who are you?!";
                        break;
                    case 1:
                        chat = "You better back off maverick!";
                        break;
                    default:
                        chat = "Get away from me, i'm not doing any business with you.";
                        break;
                }
            }
        }

        public override bool PreChatButtonClicked(NPC npc, bool firstButton)
        {
            return !Main.LocalPlayer.HasBuff(ModContent.BuffType<Megamerged>());
        }
    }
}