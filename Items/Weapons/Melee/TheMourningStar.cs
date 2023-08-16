using ShardsOfAtheria.Buffs.PlayerDebuff;
using ShardsOfAtheria.Projectiles.Weapon.Melee.BloodthirstySword;
using ShardsOfAtheria.ShardsConditions;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class TheMourningStar : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            Item.AddAreus(true);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var shards = Main.LocalPlayer.Shards();
            string key = "Mods.ShardsOfAtheria.Common.MourningStarKills";
            string text = Language.GetTextValue(key, shards.mourningStarKills);
            tooltips.Insert(ShardsTooltipHelper.GetIndex(tooltips, "OneDropLogo"),
                new TooltipLine(Mod, "Kills", text));
        }

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 46;
            Item.scale = 1.5f;

            Item.damage = 150;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6;
            Item.crit = 50;

            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;

            Item.shoot = ModContent.ProjectileType<MourningStar>();
            Item.shootSpeed = 1;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(1);
        }

        public override void HoldItem(Player player)
        {
            if (!player.Shards().anastasiaPride)
            {
                player.AddBuff(ModContent.BuffType<CorruptedBlood>(), 300);
            }
        }

        public override bool? UseItem(Player player)
        {
            Item.FixSwing(player);
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddCondition(SoAConditions.Upgrade)
                .AddIngredient(ModContent.ItemType<AreusKatana>())
                .AddIngredient(ItemID.BeetleHusk, 20)
                .AddIngredient(ItemID.SoulofFright, 14)
                .Register();
        }
    }
}
