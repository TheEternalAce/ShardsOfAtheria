using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Projectiles.Weapon.Melee;
using ShardsOfAtheria.Systems;
using ShardsOfAtheria.Tiles.Crafting;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace ShardsOfAtheria.Items.Accessories
{
    //[AutoloadEquip(EquipType.HandsOn)]
    public class PrototypeAreusBand : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 24;
            Item.accessory = true;

            Item.damage = 36;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 4;
            Item.crit = 2;

            Item.shoot = ModContent.ProjectileType<PrototypeBandBlade>();
            Item.shootSpeed = 1;

            Item.rare = ItemDefaults.RarityAreus;
            Item.value = ItemDefaults.ValueDungeon;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.Shards().prototypeBand = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 8)
                .AddRecipeGroup(ShardsRecipes.Gold, 2)
                .AddIngredient(ModContent.ItemType<Jade>(), 4)
                .AddTile<AreusFabricator>()
                .Register();
        }

        public void UseEffect(Player player)
        {
            if (player.IsLocal())
            {
                if (Main.rand.NextBool(5))
                {
                    if (!player.immune)
                    {
                        var info = new Player.HurtInfo()
                        {
                            Damage = Item.damage,
                            DamageSource = PlayerDeathReason.ByCustomReason(
                                player.name + "'s band malfunctioned")
                        };
                        player.Hurt(info);
                        player.AddBuff(ModContent.BuffType<ElectricShock>(), 600);
                    }
                }
                else
                {
                    Vector2 velocity = Vector2.Normalize(Main.MouseWorld - player.Center);
                    Projectile.NewProjectile(player.GetSource_Accessory(Item), player.Center,
                        velocity, Item.shoot, player.GetWeaponDamage(Item),
                        player.GetWeaponKnockback(Item), player.whoAmI);
                }
            }
        }
    }
}