using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.PlayerBuff;
using ShardsOfAtheria.Buffs.PlayerDebuff;
using ShardsOfAtheria.Projectiles.Melee.BloodthirstySword;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class TheMourningStar : ModItem
    {
        public int blood = 600;

        public override void SetStaticDefaults()
        {
            Item.AddAreus(true);
            Item.AddElement(1);
            Item.AddElement(3);
            Item.AddRedemptionElement(12);
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
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;

            Item.shoot = ModContent.ProjectileType<MourningStar>();
            Item.shootSpeed = 1;
            Item.rare = ItemRarityID.Cyan;
            Item.value = 100000;
        }

        public override void UpdateInventory(Player player)
        {
            if (blood > 0)
            {
                blood--;
            }
            if (blood == 0)
            {
                if (!player.HasBuff<CorruptedBlood>())
                {
                    CombatText.NewText(player.Hitbox, Color.DarkRed, "Feed me");
                }
                player.AddBuff<CorruptedBlood>(300);
            }
            if (blood > 1200)
            {
                player.AddBuff(ModContent.BuffType<MourningSatisfaction>(), blood - 1200);
            }
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (player.HasBuff<MourningSatisfaction>())
            {
                damage += 0.5f;
            }
        }

        public override bool? UseItem(Player player)
        {
            Item.FixSwing(player);
            return true;
        }
    }
}
