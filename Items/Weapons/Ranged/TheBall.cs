using ShardsOfAtheria.Projectiles.Weapon.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class TheBall : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            ItemID.Sets.IsRangedSpecialistWeapon[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 14;

            Item.damage = 99;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 7;
            Item.crit = 15;

            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 16f;
            Item.rare = ItemRarityID.Red;
            Item.value = 46700;
            Item.shoot = ModContent.ProjectileType<BallinBall>();
        }
    }
}