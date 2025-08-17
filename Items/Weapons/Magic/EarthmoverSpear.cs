using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Projectiles.Magic.ThorSpear;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
    public class EarthmoverSpear : ZChargeable
    {
        public override int ZChargedItem => ModContent.ItemType<ZEarthmoverSpear>();

        public override void SetStaticDefaults()
        {
            Item.AddDamageType(7, 5);
            Item.AddElement(2);
            Item.AddRedemptionElement(5);
            Item.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Item.width = 72;
            Item.height = 72;

            Item.damage = 80;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 3.75f;
            Item.crit = 16;
            Item.mana = 30;

            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.staff[Item.type] = true;

            Item.shootSpeed = 1f;
            Item.rare = ItemDefaults.RarityEarlyHardmode;
            Item.value = Item.sellPrice(0, 4, 25);
            Item.shoot = ModContent.ProjectileType<EarthJavelin>();
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] == 0;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.HallowedBar, 15)
                .AddIngredient(ItemID.SoulofMight, 8)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}