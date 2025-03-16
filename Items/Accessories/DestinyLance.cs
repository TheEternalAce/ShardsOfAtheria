using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Melee;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories
{
    public class DestinyLance : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddDamageType(3);
            Item.AddElement(0);
            Item.AddRedemptionElement(2);
            Item.AddRedemptionElement(13);
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 36;
            Item.accessory = true;

            Item.damage = 42;
            Item.knockBack = 6;
            Item.crit = 2;

            Item.shoot = ModContent.ProjectileType<SpearOfDestiny>();
            Item.rare = ItemDefaults.RarityEarlyHardmode;
            Item.value = 57500;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.Shards().destinyLance = true;
            if (player.ownedProjectileCounts[Item.shoot] == 0)
            {
                Vector2 velocity = Vector2.Zero;
                if (player == Main.LocalPlayer) velocity = player.Center.DirectionTo(Main.MouseWorld);
                Projectile.NewProjectile(player.GetSource_Accessory(Item), player.Center, velocity, Item.shoot, player.GetWeaponDamage(Item), player.GetWeaponKnockback(Item));
            }
        }
    }
}