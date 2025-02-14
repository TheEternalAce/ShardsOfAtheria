using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Ranged;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class ZHansMachineGun : HansMachineGun
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            Item.AddElement(0);
            Item.AddRedemptionElement(2);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.damage = 96;
            Item.knockBack = 7f;

            Item.rare = ItemRarityID.Purple;
            Item.value = Item.sellPrice(0, 4, 25);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            base.ModifyShootStats(player, ref position, ref velocity, ref type, ref damage, ref knockback);
            if (type == ModContent.ProjectileType<HansBullet>()) type = ModContent.ProjectileType<ZHansBullet>();
        }
    }
}