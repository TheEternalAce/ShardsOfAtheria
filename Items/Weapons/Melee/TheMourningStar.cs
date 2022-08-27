using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Items.Placeable.Furniture;
using ShardsOfAtheria.Projectiles.Weapon.Melee;
using ShardsOfAtheria.Projectiles.Weapon.Melee.Messiah;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class TheMourningStar : ModItem
    {
        public bool bloodProj;
        public int blood;
        const int bloodCost = 200;

        public override void OnCreate(ItemCreationContext context)
        {
            blood = 0;
        }

        public override void SaveData(TagCompound tag)
        {
            tag["blood"] = blood;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("blood"))
            {
                blood = tag.GetInt("blood");
            }
        }

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'The areus the blade was forged from has turned dark and lost its electrical properties'\n" +
                "Absorbs blood on hitting enemies\n" +
                "Right click to toggle shooting projectiles\n" +
                "Shooting projectiles costs 5 blood\n" +
                "Right click while holding Left Alt with 1000 blood to activate Shade State\n" +
                "Shade State increases damage by 15% and defense by 20");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Blood", "Absorbed blood: " + blood));
        }

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 46;
            Item.scale = 1.5f;

            Item.damage = 150;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6;
            Item.crit = 50;

            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;

            Item.shoot = ModContent.ProjectileType<BloodCutter>();
            Item.shootSpeed = 15;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(1);
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage += blood * .01f;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useStyle = ItemUseStyleID.HoldUp;
                Item.UseSound = SoundID.Item82;
                if (Main.keyState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftAlt))
                {
                    if (player.HasBuff(ModContent.BuffType<ShadeState>()))
                    {
                        CombatText.NewText(player.getRect(), Color.DarkGray, "Shade State already active");
                    }
                    else if (blood < 1000)
                    {
                        CombatText.NewText(player.getRect(), Color.Red, "Not enough blood");
                    }
                    else
                    {
                        player.AddBuff(ModContent.BuffType<ShadeState>(), 14400);
                        blood -= 1000;
                    }
                }
                else
                {
                    bloodProj = !bloodProj;
                    CombatText.NewText(player.getRect(), Color.Red, bloodProj ? "Enabled" : "Disabled");
                }
            }
            else
            {
                Item.useStyle = ItemUseStyleID.Swing;
                Item.UseSound = SoundID.Item1;
                Item.shoot = ModContent.ProjectileType<BloodCutter>();
            }
            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (blood >= 5 && bloodProj && player.altFunctionUse != 2)
            {
                blood -= 5;
                return true;
            }
            else return false;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            blood += 10;
            if (target.life <= 0)
            {
                blood += 90;
            }
            base.OnHitNPC(player, target, damage, knockBack, crit);
        }

        public override void HoldItem(Player player)
        {
            player.AddBuff(BuffID.Bleeding, 300);
        }

        public override void UpdateInventory(Player player)
        {
            player.AddBuff(BuffID.Bleeding, 300);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 15)
                .AddIngredient(ItemID.BeetleHusk, 20)
                .AddIngredient(ItemID.SoulofFright, 20)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
