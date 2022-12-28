using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Projectiles.Weapon.Areus.AreusTwinSabers;

namespace ShardsOfAtheria.Items.Weapons.Areus
{
    public class AreusTwinSabers : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            SoAGlobalItem.AreusWeapon.Add(Type);
            SoAGlobalItem.Eraser.Add(Type);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Blood", $"{Language.GetTextValue("Mods.ShardsOfAtheria.Common.AbsorbedBlood")} {blood}"));
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

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.shoot = ModContent.ProjectileType<AreusSaberProj>();
            }
            else
            {

            }
            return true;
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
