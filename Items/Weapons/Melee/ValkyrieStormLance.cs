using MMZeroElements;
using ShardsOfAtheria.Projectiles.Weapon.Melee;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class ValkyrieStormLance : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            WeaponElements.Electric.Add(Type);
        }

        public override void SetDefaults()
        {
            // A special method that sets a variety of item parameters that make the item act like a spear weapon.
            // To see everything DefaultToSpear() does, right click the method in Visual Studios and choose "Go To Definition" (or press F12).
            // The shoot speed will affect how far away the projectile spawns from the player's hand.
            // If you are using the custom AI in your projectile (and not aiStyle 19 and AIType = ProjectileID.JoustingLance), the standard value is 1f.
            // If you are using aiStyle 19 and AIType = ProjectileID.JoustingLance, then multiply the value by about 3.5f.
            Item.DefaultToSpear(ModContent.ProjectileType<ValkyrieStormLanceProj>(), 0.7f, 20);

            Item.DamageType = DamageClass.MeleeNoSpeed; // We need to use MeleeNoSpeed here so that attack speed doesn't effect our held projectile.

            Item.SetWeaponValues(50, 6f, 6); // A special method that sets the damage, knockback, and bonus critical strike chance.

            Item.SetShopValues(ItemRarityColor.Yellow8, Item.buyPrice(0, 2)); // A special method that sets the rarity and value.

            Item.channel = true; // Channel is important for our projectile.

            // This will make sure our projectile completely disappears on hurt.
            // It's not enough just to stop the channel, as the lance can still deal damage while being stowed
            // If two players charge at each other, the first one to hit should cancel the other's lance
            Item.StopAnimationOnHurt = true;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }
    }
}