using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Projectiles.Melee.BalanceSwords;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class DarknessWithinLight : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddDamageType(6);
            Item.AddElement(0);
            Item.AddRedemptionElement(2);
            Item.AddRedemptionElement(9);
        }

        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 58;
            Item.scale = 1.5f;

            Item.damage = 98;
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
            Item.shoot = ModContent.ProjectileType<ShadowInLight>();
        }

        public override bool MeleePrefix()
        {
            return true;
        }

        public override bool? UseItem(Player player)
        {
            Item.FixSwing(player);
            return true;
        }
    }
}