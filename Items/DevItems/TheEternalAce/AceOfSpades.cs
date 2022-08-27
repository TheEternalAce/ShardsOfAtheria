using ShardsOfAtheria.Projectiles.Weapon.Ranged;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DevItems.TheEternalAce
{
    public class AceOfSpades : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Use RMB to detonate the cards\n" +
                "'What is that object?'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 22;

            Item.damage = 70;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 4;
            Item.crit = 4;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 15;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(0, 2, 25);
            Item.shoot = ModContent.ProjectileType<AceOfSpadesProj>();
        }
    }
}
