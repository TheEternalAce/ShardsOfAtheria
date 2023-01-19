using Microsoft.Xna.Framework;
using ShardsOfAtheria.Players;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using WebmilioCommons.Effects.ScreenShaking;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class HandCanon : ModItem
    {
        public int charge;

        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 24;

            Item.damage = 65;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 4;
            Item.crit = 5;

            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item11;
            Item.noMelee = true;

            Item.shootSpeed = 20f;
            Item.rare = ItemRarityID.Yellow;
            Item.value = 22500;
            Item.shoot = ProjectileID.Grenade;
            Item.useAmmo = ItemID.Grenade;
        }

        public override bool CanConsumeAmmo(Item item, Player player)
        {
            return !(player.itemAnimation < Item.useAnimation);
        }

        public override void UpdateInventory(Player player)
        {
            if (++charge == 300)
            {
                SoundEngine.PlaySound(SoundID.MaxMana);
                CombatText.NewText(player.getRect(), Color.SkyBlue, Language.GetTextValue("Mods.ShardsOfAtheria.Common.FullCharge"));
            }
            if (charge > 301)
            {
                Item.useAnimation = 30;
                charge = 301;
            }
            else Item.useAnimation = 10;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            base.ModifyWeaponDamage(player, ref damage);
            if (charge >= 300)
                damage += 1f;
        }

        int projType;

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            projType = type;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[projType] < 3;
        }

        public override bool? UseItem(Player player)
        {
            if (charge >= 300)
            {
                ScreenShake.ShakeScreen(6, 60);
            }
            charge = 0;
            SoundEngine.PlaySound(Item.UseSound.Value);
            OverchargePlayer overchargePlayer = player.GetModPlayer<OverchargePlayer>();
            if (overchargePlayer.overcharged)
            {
                overchargePlayer.overcharge = 0f;
            }
            return base.UseItem(player);
        }
    }
}