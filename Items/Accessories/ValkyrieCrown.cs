using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Weapon.Melee;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories
{
    [AutoloadEquip(EquipType.Face)]
    public class ValkyrieCrown : ModItem
    {
        public override void SetStaticDefaults()
        {
            ArmorIDs.Head.Sets.DrawHatHair[Item.faceSlot] = true;

            SacrificeTotal = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.accessory = true;
            Item.canBePlacedInVanityRegardlessOfConditions = true;

            Item.damage = 16;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 2;
            Item.crit = 3;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 50);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.ShardsOfAtheria().valkyrieCrown = true;
            if (player.ownedProjectileCounts[ModContent.ProjectileType<HardlightSword>()] < 2)
            {
                for (int i = 0; i < 2; i++)
                {
                    Projectile.NewProjectile(player.GetSource_Accessory(Item), player.Center, Vector2.Zero,
                        ModContent.ProjectileType<HardlightSword>(), (int)player.GetWeaponKnockback(Item),
                        player.GetWeaponKnockback(Item), player.whoAmI, i);
                }
            }
        }
    }
}
