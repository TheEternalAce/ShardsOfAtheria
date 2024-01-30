using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Weapons.Ammo;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.BuffItems
{
    public class PlagueInjection : ModItem
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

            Item.buffType = ModContent.BuffType<PlagueInfused>();
            Item.buffTime = 14400;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<EmptyNeedle>())
                .AddIngredient<PlagueCell>(1)
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
            player.AddBuff<InjectionShock>(300);
            player.AddBuff<Plague>(2);
            return true;
        }
    }

    public class PlagueInfused : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetAttackSpeed(DamageClass.Generic) -= Plague.SpeedReduction;
            player.moveSpeed -= Plague.SpeedReduction;
            player.accRunSpeed -= Plague.SpeedReduction;
            player.GetDamage(DamageClass.Generic) += 0.15f;
            player.statDefense += 10;
        }
    }
}