using SagesMania.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.DevItems.AceOfSpades2370
{
    public class AceOfSpades : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("''What is that object?''");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 22;

            item.damage = 70;
            item.ranged = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.knockBack = 4;
            item.crit = 4;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<AceOfSpadesProj>();
            item.shootSpeed = 15;
            item.rare = ItemRarityID.Cyan;
            item.value = Item.sellPrice(gold: 10);
        }
    }
}
