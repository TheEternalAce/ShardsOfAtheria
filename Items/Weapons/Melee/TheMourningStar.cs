using BattleNetworkElements.Utilities;
using ShardsOfAtheria.Buffs.PlayerDebuff;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.ModCondition;
using ShardsOfAtheria.Projectiles.Weapon.Melee.BloodthirstySword;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class TheMourningStar : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            SoAGlobalItem.DarkAreusWeapon.Add(Type);
            Item.AddAqua();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            var shards = Main.LocalPlayer.Shards();
            tooltips.Insert(ShardsTooltipHelper.GetIndex(tooltips, "OneDropLogo"),
                new TooltipLine(Mod, "Kills", $"Kills: {shards.mourningStarKills}"));
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
            player.AddBuff(ModContent.BuffType<CorruptedBlood>(), 300);
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
