using ShardsOfAtheria.Projectiles.Magic;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
    public class BloodScepter : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddElementAqua();
            Item.AddElementWood();
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 20;

            Item.damage = 125;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 4;
            Item.crit = 26;
            Item.mana = 14;

            Item.useTime = 10;
            Item.useAnimation = 30;
            Item.reuseDelay = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item43;
            Item.autoReuse = true;
            Item.staff[Type] = true;

            Item.shootSpeed = 10;
            Item.rare = ItemRarityID.Red;
            Item.shoot = ModContent.ProjectileType<BloodWaveFriendly>();
        }
    }
}