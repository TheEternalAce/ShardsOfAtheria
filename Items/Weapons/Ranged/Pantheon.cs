using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using ShardsOfAtheria.Projectiles.Other;
using Terraria.GameContent.Creative;
using ShardsOfAtheria.Items.Potions;
using ShardsOfAtheria.Projectiles.Weapon.Melee;
using Terraria.ModLoader.IO;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class Pantheon : ModItem
    {
        public int gold = 0;

        public override void SaveData(TagCompound tag)
        {
            tag["gold"] = gold;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("gold"))
            {
                gold = tag.GetInt("gold");
            }
        }

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Right click with Gold or Platinum bars in inventory to increase damage and projectiles fired by 1\n" +
                "Fires up to 5 projectiles");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 36;

            Item.damage = 5;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 2f;
            Item.crit = 6;

            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item5;
            Item.noMelee = true;

            Item.shootSpeed = 16f;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 5);
            Item.shoot = ProjectileID.PurificationPowder;
            Item.useAmmo = AmmoID.Arrow;
        }

        public override bool CanRightClick()
        {
            return Main.LocalPlayer.HasItem(ItemID.GoldBar) || Main.LocalPlayer.HasItem(ItemID.PlatinumBar);
        }

        public override bool ConsumeItem(Player player)
        {
            return false;
        }

        public override void UpdateInventory(Player player)
        {
            if (player.dead)
            {
                gold = 0;
            }
        }

        public override void HoldItem(Player player)
        {
            Item.damage = 5;
            if (player.dead)
            {
                gold = 0;
            }
            for (int i = 0; i < gold; i++)
            {
                Item.damage += 1;
            }
        }

        public override void RightClick(Player player)
        {
            gold++;
            if (Main.LocalPlayer.HasItem(ItemID.GoldBar))
            {
                player.inventory[player.FindItem(ItemID.GoldBar)].stack--;
            }
            else if (Main.LocalPlayer.HasItem(ItemID.PlatinumBar))
            {
                player.inventory[player.FindItem(ItemID.PlatinumBar)].stack--;
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 1 + gold;
            if (numberProjectiles > 5)
            {
                numberProjectiles = 5;
            }
            if (numberProjectiles > 1)
            {
                float rotation = MathHelper.ToRadians(5);
                position += Vector2.Normalize(velocity) * 10f;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                    Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
                }
                return false;
            }
            else return true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 5);
        }
    }
}