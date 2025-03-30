using ShardsOfAtheria.Projectiles.Melee;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class BloodScythe : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddDamageType(6);
            Item.AddElement(1, 3);
            Item.AddEraser();
            Item.AddRedemptionElement(1, 12);
        }

        public override void SetDefaults()
        {
            Item.width = 94;
            Item.height = 76;

            Item.damage = 150;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6f;
            Item.crit = 96;

            Item.useTime = 50;
            Item.useAnimation = 50;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.channel = true;

            Item.shootSpeed = 1f;
            Item.rare = ItemDefaults.RarityDeath;
            Item.value = 321000;
            Item.shoot = ModContent.ProjectileType<DeathScythe>();
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