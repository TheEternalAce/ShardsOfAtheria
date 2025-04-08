using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.Pets;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Projectiles.Pets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.PetItems
{
    public class SmallHardlightCrest : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.master = true;
            Item.shoot = ModContent.ProjectileType<SmolNova>();
            Item.buffType = ModContent.BuffType<NovaPetBuff>();

            Item.rare = ItemRarityID.Master;
            Item.value = ItemDefaults.ValueDungeon;
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