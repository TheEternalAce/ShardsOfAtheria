using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Items.Placeable;
using Terraria;
using ShardsOfAtheria.Projectiles.Weapon.Magic;
using Terraria.GameContent.Creative;
using ShardsOfAtheria.Items.Potions;

namespace ShardsOfAtheria.Items.Weapons.Areus
{
    public class AreusStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 60;

            Item.damage = 130;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 3.75f;
            Item.crit = 16;
            Item.mana = 6;

            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item43;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.staff[Item.type] = true;

            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(0, 4, 25);
            Item.shoot = ModContent.ProjectileType<ElectricBolt>();
            Item.shootSpeed = 16f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 10)
                .AddIngredient(ItemID.FragmentVortex, 7)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (player.HasBuff(ModContent.BuffType<Conductive>()))
            {
                damage += .15f;
            }
        }
    }
}