using ShardsOfAtheria.Projectiles.Weapon.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class Cataracnia : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
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