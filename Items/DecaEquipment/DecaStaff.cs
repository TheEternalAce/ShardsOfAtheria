using Microsoft.Xna.Framework;
using SagesMania.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.DecaEquipment
{
    public class DecaStaff : DecaEquipment
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ion's Deca Barrage");
            Tooltip.SetDefault("'The staff of a godly machine'");
        }

        public override void SetDefaults()
        {
            item.damage = 200000;
            item.melee = true;
            item.knockBack = 6f;
            item.crit = 100;
            item.useTime = 4;
            item.useAnimation = 20;
            item.reuseDelay = 22;
            item.rare = ItemRarityID.Red;

            item.shoot = ModContent.ProjectileType<DecaSwarmer>();
            item.shootSpeed = 13f;
            item.mana = 5;

            item.noMelee = true;
            Item.staff[item.type] = true;
            item.autoReuse = true;
            item.UseSound = SoundID.Item8;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.width = 50;
            item.height = 50;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5));
            speedX = perturbedSpeed.X;
            speedY = perturbedSpeed.Y;
            return true;
        }
    }
}