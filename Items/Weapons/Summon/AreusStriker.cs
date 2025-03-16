using ShardsOfAtheria.Projectiles.Summon.Active;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Summon
{
    public class AreusStriker : ModItem
    {
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(6);

        public override void SetStaticDefaults()
        {
            Item.AddAreus();
        }

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 30;

            Item.damage = 12;
            Item.DamageType = DamageClass.SummonMeleeSpeed;
            Item.mana = 8;

            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item5;
            Item.noMelee = true;
            Item.autoReuse = true;

            Item.shootSpeed = 12f;
            Item.value = 150000;
            Item.rare = ItemDefaults.RarityDungeon;
            Item.shoot = ModContent.ProjectileType<StrikerRod>();
        }
    }
}