using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Melee;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class EntropicSaber : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddDamageType(2, 11);
            Item.AddElement(1);
            Item.AddRedemptionElement(3);
        }

        public override void SetDefaults()
        {
            Item.width = 88;
            Item.height = 88;

            Item.damage = 26;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 4f;

            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.UseSound = SoundID.Item1;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 1f;
            Item.rare = ItemDefaults.RaritySlayer;
            Item.shoot = ModContent.ProjectileType<EntropicSlash_Red>();
        }

        public override bool MeleePrefix()
        {
            return true;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage = ShardsHelpers.ScaleByProggression(player, damage);
        }

        float bladeDirection = 1.1f;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            bladeDirection *= -1f;
            if (bladeDirection < 0) type = ModContent.ProjectileType<EntropicSlash_Blue>();
            float adjustedItemScale = player.GetAdjustedItemScale(Item); // Get the melee scale of the player and item.
            Projectile.NewProjectile(source, player.MountedCenter, velocity, type, damage, knockback, player.whoAmI, player.direction * player.gravDir * bladeDirection, player.itemAnimationMax, adjustedItemScale);
            NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI); // Sync the changes in multiplayer.

            if (Main.hardMode && player.IsLocal())
            {
                var spawnpos = Main.MouseWorld + Vector2.One.RotatedByRandom(MathHelper.TwoPi) * 100f;
                var vector = Vector2.Normalize(Main.MouseWorld - spawnpos) * 16;
                int cut = ModContent.ProjectileType<EntropicCut_Red>();
                if (Main.rand.NextBool()) cut -= 1;
                Projectile.NewProjectile(source, spawnpos, vector, cut, damage / 3, 0f);
            }
            return false;
        }
    }
}