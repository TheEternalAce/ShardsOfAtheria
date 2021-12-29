using ShardsOfAtheria.Projectiles.Weapon;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DevItems.AceOfSpades2370
{
    public class AceOfSpades : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'What is that object?'");
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 22;

            Item.damage = 70;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.knockBack = 4;
            Item.crit = 4;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<AceOfSpadesProj>();
            Item.shootSpeed = 15;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(gold: 10);
        }
    }
}
