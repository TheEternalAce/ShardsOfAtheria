using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.Audio;

namespace ShardsOfAtheria.Items.SlayerItems
{
    public class HandCanon : SlayerItem
    {
        public int charge;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Hold in hand to charge\n" +
                "After 5 seconds of charging damage is increased and fire a burst of 3 grenades\n" +
                "'Demoman TF2'");
        }

        public override void SetDefaults()
        {
            Item.damage = 65;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.width = 34;
            Item.height = 24;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 4;
            Item.UseSound = SoundID.Item11;
            Item.crit = 5;
            Item.rare = ModContent.RarityType<SlayerRarity>();
            Item.shoot = ProjectileID.RocketI;
            Item.shootSpeed = 10f;
            Item.useAmmo = AmmoID.Rocket;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override bool CanConsumeAmmo(Item item, Player player)
        {
            return !(player.itemAnimation < Item.useAnimation);
        }

        public override void HoldItem(Player player)
        {
            if (++charge == 300)
            {
                SoundEngine.PlaySound(SoundID.MaxMana);
                CombatText.NewText(player.getRect(), Color.SkyBlue, "Charge ready!");
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

        public override bool? UseItem(Player player)
        {
            charge = 0;
            SoundEngine.PlaySound(Item.UseSound.Value);
            return base.UseItem(player);
        }
    }
}