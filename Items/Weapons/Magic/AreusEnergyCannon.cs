using ShardsOfAtheria.Projectiles.Magic;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
    public class AreusEnergyCannon : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddAreus();
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 36;

            Item.damage = 96;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 7f;
            Item.crit = 14;
            Item.mana = 10;

            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item38;
            Item.autoReuse = true;
            Item.noMelee = true;

            Item.shootSpeed = 20f;
            Item.rare = ItemDefaults.RarityAreus;
            Item.value = ItemDefaults.ValueLunarPillars;
            Item.shoot = ModContent.ProjectileType<AreusEnergyBlast>();
        }
    }
}