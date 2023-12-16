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
            float multiplier = 1.3f;
            float adder = multiplier - 1f;
            player.Shards().areusKey = true;
            player.GetDamage(DamageClass.Generic) += adder;
            player.GetAttackSpeed(DamageClass.Generic) += adder;
            player.statLifeMax2 = (int)(player.statLifeMax2 * multiplier);
            player.moveSpeed += adder;
            player.statDefense *= multiplier;
            player.statManaMax2 = (int)(player.statManaMax2 * multiplier);
        }
    }
}