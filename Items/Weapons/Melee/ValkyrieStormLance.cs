using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles.Weapon.Melee;
using ShardsOfAtheria.Items.SlayerItems;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class ValkyrieStormLance : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Briefly shocks enemies\n" +
                "'The stolen lance of a slain Valkyrie'");

            SoAGlobalItem.SlayerItem.Add(Type);

            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 50;

            Item.damage = 50;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6;
            Item.crit = 6;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;

            Item.shootSpeed = 3.5f;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 2);
            Item.shoot = ModContent.ProjectileType<ValkyrieStormLanceProj>();
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }
    }
}