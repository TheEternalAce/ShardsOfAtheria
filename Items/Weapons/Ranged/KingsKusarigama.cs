using ShardsOfAtheria.Projectiles.Ranged;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class KingsKusarigama : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.IsRangedSpecialistWeapon[Type] = true;
            Item.AddDamageType(0, 11);
            Item.AddElement(1);
            Item.AddRedemptionElement(3);
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 50;
            Item.master = true;

            Item.damage = 26;
            Item.DamageType = DamageClass.Ranged.TryThrowing();
            Item.knockBack = 4;
            Item.crit = 3;

            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;

            Item.shootSpeed = 5.33f;
            Item.rare = ItemRarityID.Master;
            Item.value = 17400;
            Item.shoot = ModContent.ProjectileType<KusarigamaKing>();
        }
    }
}