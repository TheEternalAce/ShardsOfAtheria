using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.PlayerBuff;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Projectiles.Weapon.Areus;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ShardsOfAtheria.Items.Weapons.Areus
{
    public class TheMourningStar : ModItem
    {
        public int blood;
        public const int BloodProjCost = 10;
        const int BloodCost = 1000;

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
            SacrificeTotal = 1;
            SoAGlobalItem.AreusWeapon.Add(Type);
            SoAGlobalItem.ElectricWeapon.Add(Type);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.Add(new TooltipLine(Mod, "Blood", $"{Language.GetTextValue("Mods.ShardsOfAtheria.General.AbsorbedBlood")} {blood}"));
        }

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 46;

            Item.damage = 150;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6;
            Item.crit = 50;

            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shoot = ModContent.ProjectileType<MourningStar>();
            Item.shootSpeed = 1;
            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(1);
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage += blood * 0.0001f;
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
                Item.shoot = ProjectileID.None;
                Item.noUseGraphic = false;
                if (player.HasBuff(ModContent.BuffType<ShadeState>()))
                {
                    CombatText.NewText(player.getRect(), Color.DarkGray, Language.GetTextValue("Mods.ShardsOfAtheria.General.ShadeStateActive"));
                }
                else if (blood < BloodCost)
                {
                    CombatText.NewText(player.getRect(), Color.Red, Language.GetTextValue("Mods.ShardsOfAtheria.General.InsufficientBlood"));
                }
                else
                {
                    player.AddBuff(ModContent.BuffType<ShadeState>(), 14400);
                    blood -= 1000;
                }
            }
            else
            {
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.UseSound = SoundID.Item1;
                Item.shoot = ModContent.ProjectileType<MourningStar>();
                Item.noUseGraphic = true;
            }
            return true;
        }

        public override void HoldItem(Player player)
        {
            player.buffImmune[BuffID.Bleeding] = false;
            player.AddBuff(BuffID.Bleeding, 300);
        }
    }
}
