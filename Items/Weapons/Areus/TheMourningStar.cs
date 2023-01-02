using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Projectiles.Weapon.Areus;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ShardsOfAtheria.Items.Weapons.Areus
{
    public class TheMourningStar : ModItem
    {
        public int blood;
        public const int BloodProjCost = 10;

        public override void OnCreate(ItemCreationContext context)
        {
            blood = 0;
        }

        public override void SaveData(TagCompound tag)
        {
            tag["blood"] = blood;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("blood"))
            {
                blood = tag.GetInt("blood");
            }
        }

        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            SoAGlobalItem.DarkAreusWeapon.Add(Type);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Blood", $"{Language.GetTextValue("Mods.ShardsOfAtheria.Common.AbsorbedBlood")}: {blood}"));
        }

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 46;

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

            Item.shoot = ModContent.ProjectileType<MourningStar>();
            Item.shootSpeed = 1;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(1);
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage += blood * 0.0001f;
        }

        public override void HoldItem(Player player)
        {
            player.buffImmune[BuffID.Bleeding] = false;
            player.AddBuff(BuffID.Bleeding, 300);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddCondition(NetworkText.FromKey("Mods.ShardsOfAtheria.RecipeConditions.Upgrade"), r => false)
                .AddIngredient(ModContent.ItemType<AreusKatana>())
                .AddIngredient(ItemID.BeetleHusk, 20)
                .AddIngredient(ItemID.SoulofFright, 14)
                .Register();
        }
    }
}
