using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Systems;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.BuffItems
{
    public class SoulInjection : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 30;
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 48;
            Item.maxStack = 9999;

            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
            Item.useTurn = true;

            Item.rare = ItemDefaults.RarityDungeon;
            Item.value = ItemDefaults.ValueBuffPotion;

            Item.buffType = ModContent.BuffType<SoulInfused>();
            Item.buffTime = 14400;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<EmptyNeedle>())
                .AddRecipeGroup(ShardsRecipes.Soul, 10)
                .AddTile(TileID.Bottles)
                .Register();
        }

        public override void OnConsumeItem(Player player)
        {
            player.QuickSpawnItem(player.GetSource_FromThis(), ModContent.ItemType<EmptyNeedle>());
        }

        public override bool CanUseItem(Player player)
        {
            if (!player.HasBuff(ModContent.BuffType<InjectionShock>()))
                return true;
            else return false;
        }

        public override bool? UseItem(Player player)
        {
            player.AddBuff(ModContent.BuffType<InjectionShock>(), 300);
            return true;
        }
    }

    public class SoulInfused : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.statLifeMax2 += 50;
            player.statDefense += 10;
            player.GetDamage(DamageClass.Generic) += .15f;
            player.lifeRegen += 10;
        }
    }
}