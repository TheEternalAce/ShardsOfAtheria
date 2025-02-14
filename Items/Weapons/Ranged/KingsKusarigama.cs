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
            Item.AddElement(1);
            Item.AddRedemptionElement(3);
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 50;
            Item.master = true;

            Item.damage = 16;
            Item.DamageType = DamageClass.Ranged;
            if (SoA.ServerConfig.throwingWeapons) Item.DamageType = DamageClass.Throwing;
            Item.knockBack = 4;
            Item.crit = 3;

            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 16f;
            Item.rare = ItemRarityID.Master;
            Item.value = 17400;
            Item.shoot = ModContent.ProjectileType<KusarigamaKing>();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
    }
}