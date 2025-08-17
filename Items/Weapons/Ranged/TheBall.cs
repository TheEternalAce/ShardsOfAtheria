using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Projectiles.Ranged;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class TheBall : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.IsRangedSpecialistWeapon[Type] = true;
            Item.AddDamageType(1);
            Item.AddElement(3);
            Item.AddRedemptionElement(10);
        }

        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 14;

            Item.damage = 99;
            Item.DamageType = DamageClass.Ranged.TryThrowing();
            Item.knockBack = 7;
            Item.crit = 15;

            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 8f;
            Item.rare = ItemDefaults.RarityMoonLord;
            Item.value = 46700;
            Item.shoot = ModContent.ProjectileType<BallinBall>();
        }
    }
}