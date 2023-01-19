using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Items.Bases;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ShardsOfAtheria.Items.Weapons.Areus
{
    public class Pantheon : SinfulItem
    {
        public int gold = 0;
        public int arrows;
        public int maxArrows;

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
            SacrificeTotal = 1;
            SoAGlobalItem.AreusWeapon.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 42;

            Item.damage = 25;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 2f;
            Item.crit = 6;

            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item5;
            Item.noMelee = true;

            Item.shootSpeed = 16f;
            Item.rare = ItemRarityID.Red;
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
            maxArrows = gold >= 5 ? 5 : gold == 0 ? 1 : gold + 1;
            if (player.dead)
            {
                gold = 0;
            }
        }

        public override bool? CanChooseAmmo(Item ammo, Player player)
        {
            if (!ammo.consumable)
            {
                arrows = maxArrows;
            }

            return base.CanChooseAmmo(ammo, player);
        }

        public override void OnConsumeAmmo(Item ammo, Player player)
        {
            arrows = Math.Min(ammo.stack, maxArrows);
            ammo.stack -= arrows;
            base.OnConsumeAmmo(ammo, player);
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
            float numberProjectiles = arrows;
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
            return new Vector2(0, 0);
        }
    }
}