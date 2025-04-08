using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Projectiles.Melee;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class Zenova : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
            Item.AddDamageType(4);

            Item.AddElement(0);
            Item.AddElement(1);
            Item.AddElement(2);
            Item.AddElement(3);

            Item.AddRedemptionElement(14);
        }

        public override void SetDefaults()
        {
            Item.width = 76;
            Item.height = 76;

            Item.damage = 150;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 3;
            Item.crit = 8;

            Item.useTime = 5;
            Item.useAnimation = 25;
            Item.reuseDelay = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.DD2_SkyDragonsFuryShot;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 15;
            Item.value = Item.sellPrice(0, 4);
            Item.rare = ItemDefaults.RarityMoonLord;
            Item.shoot = ModContent.ProjectileType<ZenovaProjectile>();
            Item.ArmorPenetration = 37;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.shoot = ModContent.ProjectileType<ZenovaProjectile>();
            }
            else
            {
                Item.shoot = ModContent.ProjectileType<ZenovaProjectile2>();
            }
            return base.CanUseItem(player);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            SoundEngine.PlaySound(Item.UseSound, player.Center);
            float rotation = 5f;
            if (player.altFunctionUse != 2)
            {
                velocity = new(0, -10 * Main.rand.NextFloat(0.33f, 1f));
                rotation *= 5f;
            }
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(rotation));
        }
    }
}