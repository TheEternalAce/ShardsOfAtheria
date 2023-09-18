using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories
{
    public class AreusKey : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 22;
            Item.accessory = true;

            Item.rare = ItemDefaults.RarityPlantera;
            Item.value = ItemDefaults.ValueHardmodeDungeon;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.Shards().areusKey = true;
            player.GetDamage(DamageClass.Generic) += 1.5f;
            player.GetAttackSpeed(DamageClass.Generic) += 0.5f;
            player.statLifeMax2 *= (int)1.5f;
            player.moveSpeed += .25f;
            player.statDefense *= (int)1.5f;
            player.statManaMax2 *= (int)1.5f;
        }
    }
}