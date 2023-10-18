using ShardsOfAtheria.Projectiles.Summon;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Summon
{
    public class AreusStriker : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;

            Item.damage = 12;
            Item.DamageType = DamageClass.Summon;

            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item5;
            Item.noMelee = true;
            Item.autoReuse = true;

            Item.shootSpeed = 12f;
            Item.value = 150000;
            Item.rare = ItemRarityID.Cyan;
            Item.shoot = ModContent.ProjectileType<StrikerRod>();
        }
    }
}