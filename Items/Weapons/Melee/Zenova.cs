using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Weapon.Melee;
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
            Item.ResearchUnlockCount = 1;
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
            Item.rare = ItemRarityID.Red;
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
                Item.useTime = 5;
                Item.useAnimation = 5;
                Item.reuseDelay = 0;
            }
            else
            {
                Item.useTime = 5;
                Item.useAnimation = 25;
                Item.reuseDelay = 10;
            }
            return base.CanUseItem(player);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            SoundEngine.PlaySound(Item.UseSound);
            if (player.altFunctionUse != 2)
            {
                velocity = velocity.RotatedByRandom(MathHelper.ToRadians(5));
            }
        }
    }
}