using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Items.Materials;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Projectiles.Melee;
using ShardsOfAtheria.Systems;
using ShardsOfAtheria.Tiles.Crafting;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace ShardsOfAtheria.Items.Accessories
{
    //[AutoloadEquip(EquipType.HandsOn)]
    public class PrototypeAreusBand : ModItem
    {
        int delay = 0;
        const int DELAY_MAX = 15;
        bool Wait => delay > 0;

        public override void SetStaticDefaults()
        {
            Item.AddAreus();
            Item.AddDamageType(11);
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

            Item.shoot = ModContent.ProjectileType<PrototypeBandSlash>();
            Item.shootSpeed = 1;

            Item.rare = ItemDefaults.RarityEarlyHardmode;
            Item.value = ItemDefaults.ValueDungeon;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.Shards().prototypeBand = true;
            if (delay > 0)
            {
                delay--;
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<AreusShard>(8)
                .AddRecipeGroup(ShardsRecipes.Gold, 2)
                .AddIngredient<Jade>(4)
                .AddIngredient(ItemID.SoulofNight, 15)
                .AddTile<AreusFabricator>()
                .Register();
        }

        public void UseEffect(Player player)
        {
            if (player.IsLocal())
            {
                if (!Wait)
                {
                    bool fireBlade = true;
                    if (!player.immune)
                    {
                        if (Main.rand.NextBool(5))
                        {
                            fireBlade = false;
                            var meleeDamage = player.GetTotalDamage(DamageClass.Melee);
                            var info = new Player.HurtInfo()
                            {
                                Damage = (int)meleeDamage.ApplyTo(Item.damage),
                                DamageSource = PlayerDeathReason.ByCustomReason(
                                    player.name + "'s band malfunctioned")
                            };
                            player.Hurt(info);
                            player.AddBuff(ModContent.BuffType<ElectricShock>(), 600);
                        }
                    }
                    if (fireBlade)
                    {
                        Vector2 velocity = Main.MouseWorld - player.Center;
                        velocity.Normalize();
                        velocity *= 16f;
                        Projectile.NewProjectile(player.GetSource_Accessory(Item),
                            player.Center, velocity, Item.shoot,
                            player.GetWeaponDamage(Item), player.GetWeaponKnockback(Item),
                            player.whoAmI);
                    }
                    delay = DELAY_MAX;
                }
            }
        }
    }
}