using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Weapon.Melee;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class ValkyrieBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Might shoot out a feather blade");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.damage = 40;
            Item.DamageType = DamageClass.Melee;
            Item.crit = 6;
            Item.knockBack = 6;
            Item.value = Item.sellPrice(0,  10);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<FeatherBladeFriendly>();
            Item.shootSpeed = 16;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Main.rand.NextFloat() <= .66f)
                return false;
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}