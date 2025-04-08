using ShardsOfAtheria.Common.Items;
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
            Item.AddDamageType(6);
            Item.AddElement(1);
            Item.AddElement(3);
            Item.AddRedemptionElement(12);
            Item.staff[Type] = true;
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

            Item.shootSpeed = 10;
            Item.rare = ItemDefaults.RarityDeath;
            Item.shoot = ModContent.ProjectileType<BloodWaveFriendly>();
        }
    }
}