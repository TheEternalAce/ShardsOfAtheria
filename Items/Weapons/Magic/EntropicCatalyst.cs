using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Items.Tools.ToggleItems;
using ShardsOfAtheria.Projectiles.Magic.EntropyCatalyst;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Magic
{
    public class EntropicCatalyst : ModItem
    {
        int bombCooldown;

        public override void SetStaticDefaults()
        {
            Item.AddDamageType(3, 4);
            Item.AddElement(0);
            Item.AddRedemptionElement(2);
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 52;

            Item.damage = 20;
            Item.DamageType = DamageClass.Magic;
            Item.knockBack = 8f;
            Item.mana = 6;

            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.autoReuse = true;

            Item.shootSpeed = 16f;
            Item.rare = ItemDefaults.RaritySlayer;
            Item.value = ItemDefaults.ValueEyeOfCthulhu;
            Item.shoot = ModContent.ProjectileType<CatalystShockwave>();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useTime = 8;
                Item.useAnimation = 8;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.noUseGraphic = true;
                Item.shootSpeed = 4f;
                Item.shoot = ModContent.ProjectileType<CatalystBomb>();
                if (bombCooldown > 0) return false;
            }
            else
            {
                Item.useTime = 28;
                Item.useAnimation = 28;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.noUseGraphic = false;
                Item.shootSpeed = 16f;
                Item.shoot = ModContent.ProjectileType<CatalystShockwave>();
            }
            return base.CanUseItem(player);
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage = ShardsHelpers.ScaleByProggression(player, damage);
        }

        public override void UpdateInventory(Player player)
        {
            if (bombCooldown > 0) bombCooldown--;
            if (bombCooldown == 1)
            {
                SoundEngine.PlaySound(SoundID.MaxMana, player.Center);
                ShardsHelpers.DustRing(player.Center, 3f, DustID.Torch);
            }
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                velocity = new Vector2(player.direction, -1.5f);
                velocity.Normalize();
                velocity *= Item.shootSpeed;
                velocity += player.velocity;
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                var devCard = ToggleableTool.GetInstance<DevelopersKeyCard>(player);
                bool cardActive = devCard != null && devCard.Active;
                bombCooldown = 120;
                if (NPC.downedPlantBoss) bombCooldown /= 2;
                if (NPC.downedMoonlord) bombCooldown /= 2;
                if (cardActive) bombCooldown = 0;
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (bombCooldown > 0)
            {
                float dividend = 120f;
                if (NPC.downedPlantBoss) dividend /= 2;
                if (NPC.downedMoonlord) dividend /= 2;
                float percent = bombCooldown / dividend;
                var hitbox = frame;
                hitbox.X = (int)(position.X - origin.X);
                hitbox.Y = (int)(position.Y - origin.Y);
                int bottom = hitbox.Bottom;
                int top = hitbox.Top;
                int steps = (int)((bottom - top) * percent);
                for (int i = 0; i < steps; i += 1)
                {
                    spriteBatch.Draw(TextureAssets.MagicPixel.Value,
                        new Rectangle(hitbox.X, hitbox.Bottom - i, hitbox.Width, 1),
                        Color.White * 0.75f);
                }
            }
            return base.PreDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }
    }
}