using SagesMania.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.DecaEquipment
{
    public class DecaBlade : DecaEquipment
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ion's Deca Blade");
            Tooltip.SetDefault("'The blade of a godly machine'");
        }

        public override void SetDefaults()
        {
            item.damage = 200000;
            item.melee = true;
            item.knockBack = 6f;
            item.crit = 100;
            item.useTime = 20;
            item.useAnimation = 20;
            item.rare = ItemRarityID.Red;

            item.autoReuse = true;
            item.useTurn = true;
            item.UseSound = SoundID.Item1;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.width = 32;
            item.height = 32;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.noMelee = true;
                item.noUseGraphic = true;
                item.useTime = 17;
                item.useAnimation = 17;
                item.UseSound = SoundID.Item1;
                item.shoot = ModContent.ProjectileType<DecaBladeProj>();
                item.shootSpeed = 13f;
                item.useTurn = false;
            }
            else
            {
                item.noMelee = false;
                item.noUseGraphic = false;
                item.useTime = 20;
                item.useAnimation = 20;
                item.UseSound = SoundID.Item1;
                item.shoot = ProjectileID.None;
                item.useTurn = true;
            }
            return base.CanUseItem(player);
        }
    }
}