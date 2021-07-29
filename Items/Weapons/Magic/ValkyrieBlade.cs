using Microsoft.Xna.Framework;
using SagesMania.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SagesMania.Items.Weapons.Magic
{
    public class ValkyrieBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.damage = 40;
            item.magic = true;
            item.crit = 16;
            item.knockBack = 6;
            item.value = Item.sellPrice(gold: 10);
            item.rare = ItemRarityID.Blue;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<FeatherBladeFriendly>();
            item.shootSpeed = 16;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (Main.rand.NextFloat() <= .5f)
                return false;
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
    }
}