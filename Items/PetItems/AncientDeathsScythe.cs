using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.Pets;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Projectiles.Pets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.PetItems
{
    public class AncientDeathsScythe : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 58;
            Item.height = 60;
            Item.master = true;
            Item.shoot = ModContent.ProjectileType<Scythe>();
            Item.buffType = ModContent.BuffType<AncientScythe>();

            Item.rare = ItemRarityID.Master;
            Item.value = ItemDefaults.ValueRelicTrophy;
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(Item.buffType, 3600);
            }
        }
    }
}