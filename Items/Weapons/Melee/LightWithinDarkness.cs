using ShardsOfAtheria.Projectiles.Melee.BalanceSwords;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class LightWithinDarkness : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddElement(2);
            Item.AddRedemptionElement(7);
            Item.AddRedemptionElement(8);
        }

        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 68;
            Item.scale = 1.5f;

            Item.damage = 72;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6;
            Item.crit = 4;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 6;
            Item.rare = ItemDefaults.RarityEarlyHardmode;
            Item.value = 100000;
            Item.shoot = ModContent.ProjectileType<LightInShadow>();
        }

        public override bool? UseItem(Player player)
        {
            Item.FixSwing(player);
            return true;
        }
    }
}