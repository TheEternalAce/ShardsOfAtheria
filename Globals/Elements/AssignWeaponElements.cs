using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Globals.Elements
{
    public class AssignWeaponElements : GlobalItem
    {
        List<int> MetalWeapon = SoAGlobalItem.MetalWeapon;

        public override bool InstancePerEntity => true;

        public override void SetDefaults(Item item)
        {
            int type = item.type;
            switch (type)
            {
                case ItemID.IronPickaxe:
                    MetalWeapon.Contains(type);
                    break;
            }
        }
    }
}
