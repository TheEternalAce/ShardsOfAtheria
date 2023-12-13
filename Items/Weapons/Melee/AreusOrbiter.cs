using ShardsOfAtheria.Projectiles.Melee.AreusOrbit;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class AreusOrbiter : ModItem
    {
        public override string Texture => SoA.PlaceholderTexture;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            Item.AddAreus();
            Item.AddElementElec();
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 76;

            Item.damage = 170;
            Item.DamageType = DamageClass.Melee;

            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;

            Item.shootSpeed = 1f;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(0, 4, 25);
            Item.shoot = ModContent.ProjectileType<OrbiterMagnet>();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useStyle = ItemUseStyleID.Swing;
                Item.shoot = ModContent.ProjectileType<AreusOrbitingSaw>();
                Item.shootSpeed = 16;
            }
            else
            {
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.shoot = ModContent.ProjectileType<OrbiterMagnet>();
                Item.shootSpeed = 16;
            }
            return base.CanUseItem(player);
        }
    }
}