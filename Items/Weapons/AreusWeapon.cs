using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ShardsOfAtheria.Items.Weapons
{
	// This class handles everything for our custom damage class
	// Any class that we wish to be using our custom damage class will derive from this class, instead of ModItem
	public abstract class AreusWeapon : ModItem
	{
		public int areusCharge = 100;
		public int areusChargeFull = 100;
		public int chargeCost = 1;

		// Make sure you can't use the item if you don't have enough resource and then use resourceCost otherwise.
		public override bool CanUseItem(Player player)
		{
			if (!ModContent.GetInstance<ServerSideConfig>().areusWeaponsCostMana)
			{
				if (areusCharge > 0)
                {
					areusCharge -= chargeCost;
					return true;
				}
				return false;
			}
			else return base.CanUseItem(player);
		}

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
			if (!ModContent.GetInstance<ServerSideConfig>().areusWeaponsCostMana)
				tooltips.Add(new TooltipLine(Mod, "Charge", $"{areusCharge}%"));
			if (!ModContent.GetInstance<ServerSideConfig>().areusWeaponsCostMana)
				tooltips.Add(new TooltipLine(Mod, "Cost", $"Charge cost {chargeCost}%"));
		}

        public override void UpdateInventory(Player player)
		{
			if (areusCharge > areusChargeFull)
				areusCharge = areusChargeFull;
		}

        public override bool ConsumeItem(Player player)
        {
            return false;
        }

        public override bool CanRightClick()
        {
            return Main.LocalPlayer.HasItem(ModContent.ItemType<AreusChargePack>()) && areusCharge < areusChargeFull;
        }

        public override void RightClick(Player player)
        {
            int areusChargePackIndex = Main.LocalPlayer.FindItem(ModContent.ItemType<AreusChargePack>());
            Main.LocalPlayer.inventory[areusChargePackIndex].stack--;
			areusCharge += 50;
			SoundEngine.PlaySound(SoundID.NPCHit53);
            CombatText.NewText(player.Hitbox, Color.Aqua, 50);
        }

        public override void SaveData(TagCompound tag)
        {
			tag["areusCharge"] = areusCharge;
			tag["areusChargeFull"] = areusChargeFull;
        }

		public override void LoadData(TagCompound tag)
		{
			areusCharge = tag.GetInt("areusCharge");
			areusChargeFull = tag.GetInt("areusChargeFull");
		}

        public override void NetSend(BinaryWriter writer)
        {
			writer.Write(areusCharge);
			writer.Write(areusChargeFull);
        }

        public override void NetReceive(BinaryReader reader)
        {
			areusCharge = reader.ReadInt32();
			areusChargeFull = reader.ReadInt32();
        }
    }
}
