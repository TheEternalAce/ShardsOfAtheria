using MMZeroElements;
using ShardsOfAtheria.Projectiles.Weapon.Throwing;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Throwing
{
    public class KingsKusarigama : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;

            WeaponElements.Ice.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 50;

            Item.damage = 22;
            Item.DamageType = DamageClass.Throwing;
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
            Item.rare = ItemRarityID.Green;
            Item.value = 17400;
            Item.shoot = ModContent.ProjectileType<KusarigamaKing>();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
    }
}