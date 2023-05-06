using MMZeroElements.Utilities;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.ModCondition;
using ShardsOfAtheria.Projectiles.Weapon.Melee.BloodthirstySword;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class TheMourningStar : ModItem
    {
        public int blood = 0;
        public const int BloodProjCost = 10;

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
            Item.ResearchUnlockCount = 1;
            SoAGlobalItem.DarkAreusWeapon.Add(Type);
            Item.AddIceDefault();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Blood", $"{Language.GetTextValue("Mods.ShardsOfAtheria.Common.AbsorbedBlood")}: {blood}"));
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

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage += blood * 0.0001f;
        }

        public override void HoldItem(Player player)
        {
            player.buffImmune[BuffID.Bleeding] = false;
            player.AddBuff(BuffID.Bleeding, 300);
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
