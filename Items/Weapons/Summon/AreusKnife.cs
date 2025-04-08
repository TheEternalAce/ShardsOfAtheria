using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Projectiles.Summon.Active;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Summon
{
    public class AreusKnife : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddAreus();
            Item.AddDamageType(5);
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 38;

            Item.damage = 64;
            Item.DamageType = DamageClass.SummonMeleeSpeed;

            Item.useTime = 22;
            Item.useAnimation = 22;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;

            Item.shootSpeed = 12f;
            Item.value = 150000;
            Item.rare = ItemDefaults.RarityEarlyHardmode;
            Item.shoot = ModContent.ProjectileType<AreusBlade>();
        }
    }
}