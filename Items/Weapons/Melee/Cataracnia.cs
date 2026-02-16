using ShardsOfAtheria.Projectiles.Melee;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    // Possibly dedicated to torrah???
    public class Cataracnia : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddDamageType(11);
            Item.AddElement(1);
            Item.AddElement(3);
            Item.AddRedemptionElement(12);
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 54;
            Item.master = true;

            Item.damage = 20;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6;
            Item.crit = 4;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;

            Item.shootSpeed = 16;
            Item.rare = ItemRarityID.Master;
            Item.value = Item.sellPrice(0, 10);

            SoA.TryDungeonCall("addFinesseWeapon", Type);
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            foreach (Projectile projectile in Main.projectile)
            {
                if (projectile.active)
                {
                    if (projectile.type == ModContent.ProjectileType<CataracniaEye>())
                    {
                        if (projectile.owner == player.whoAmI)
                        {
                            projectile.Kill();
                        }
                    }
                }
            }
            if (player.altFunctionUse == 2)
            {
                Item.shoot = ModContent.ProjectileType<CataracniaEye>();
            }
            else
            {
                Item.shoot = ProjectileID.None;
            }
            return base.CanUseItem(player);
        }
    }
}