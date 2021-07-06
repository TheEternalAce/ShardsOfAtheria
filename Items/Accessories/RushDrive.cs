using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Accessories
{
    public class RushDrive : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Gives the wearer a ''phase 2'' when below 50% life\n" +
                "Press 'Toggle Phase Type' to chose between two phase types:\n" +
                "Offensive: Sacrifice 20 defense for 20% increased damage and 10% increased crit chance\n" +
                "Defensive: Sacrifice 20% damage for 20 defense and 15% reduced damage\n" +
                "Always get 20% increased movement speed");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.accessory = true;
            item.value = Item.sellPrice(silver: 30);
            item.rare = ItemRarityID.Blue;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.statLife <= player.statLifeMax2 / 2)
            {
                player.GetModPlayer<SMPlayer>().rushDrive = true;
                player.moveSpeed += .2f;
                if (player.GetModPlayer<SMPlayer>().phaseSwitch == 1)
                {
                    player.statDefense += 20;
                    player.endurance += .1f;
                    player.allDamage -= .2f;
                }
                else
                {
                    player.statDefense -= 20;
                    player.allDamage += .2f;
                    player.meleeCrit += 10;
                    player.magicCrit += 10;
                    player.rangedCrit += 10;
                }
            }
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (player.GetModPlayer<SMPlayer>().livingMetal) return true;
            else return false;
        }
    }
}