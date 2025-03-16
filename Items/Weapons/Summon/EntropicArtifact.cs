using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Buffs.Summons;
using ShardsOfAtheria.Items.Tools.ToggleItems;
using ShardsOfAtheria.Projectiles.Summon.Minions.Star;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Summon
{
    public class EntropicArtifact : ModItem
    {
        int manualCooldown;

        public override void SetStaticDefaults()
        {
            Item.AddDamageType(5);
            Item.AddElement(2);
            Item.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 42;

            Item.damage = 14;
            Item.DamageType = DamageClass.Summon;
            Item.knockBack = 5;
            Item.crit = 5;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.UseSound = SoundID.Item82;
            Item.noMelee = true;

            Item.rare = ItemDefaults.RaritySlayer;
            Item.value = ItemDefaults.ValueEyeOfCthulhu;
            Item.shoot = ModContent.ProjectileType<RedStar>();

            Item.buffType = ModContent.BuffType<EntropicStar>();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        //public override bool CanUseItem(Player player)
        //{
        //    if (player.altFunctionUse == 2)
        //    {
        //        Item.shoot = ModContent.ProjectileType<RedStar>();
        //        Item.mana = 0;
        //    }
        //    else
        //    {
        //        Item.shoot = ProjectileID.None;
        //        Item.mana = 8;
        //    }
        //    return base.CanUseItem(player);
        //}

        public override bool? UseItem(Player player)
        {
            int manaCost = (int)(20 * player.manaCost);
            int starType = ModContent.ProjectileType<RedStar>();
            if (player.altFunctionUse == 2 && player.ownedProjectileCounts[starType] > 0 && manualCooldown == 0 && player.statMana >= manaCost)
            {
                foreach (Projectile proj in Main.projectile)
                {
                    if (proj.active && proj.type == starType && proj.owner == player.whoAmI)
                    {
                        var devCard = ToggleableTool.GetInstance<DevelopersKeyCard>(player);
                        bool cardActive = devCard != null && devCard.Active;
                        (proj.ModProjectile as RedStar).shoot = true;
                        (proj.ModProjectile as RedStar).shootingTimer = 0;
                        if (!cardActive) manualCooldown = 120;
                        player.MinionAttackTargetNPC = ShardsHelpers.FindClosestNPCIndex(Main.MouseWorld);
                        player.statMana -= manaCost;
                        player.manaRegenDelay = 60;
                        break;
                    }
                }
            }
            return true;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage = ShardsHelpers.ScaleByProggression(player, damage);
        }

        public override void UpdateInventory(Player player)
        {
            if (manualCooldown == 1)
            {
                SoundEngine.PlaySound(SoundID.MaxMana, player.Center);
                ShardsHelpers.DustRing(player.Center, 3f, DustID.GemAmethyst);
            }
            if (manualCooldown > 0) manualCooldown--;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            // Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position
            position = player.Center;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.ownedProjectileCounts[type] > 0)
            {
                player.Slayer().artifactExtraSummons++;
                return false;
            }
            else player.Slayer().artifactExtraSummons = 0;
            // This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
            player.AddBuff(Item.buffType, 2);

            // Minions have to be spawned manually, then have originalDamage assigned to the damage of the summon item
            var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
            projectile.originalDamage = Item.damage;

            // Since we spawned the projectile manually already, we do not need the game to spawn it for ourselves anymore, so return false
            return false;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (manualCooldown > 0)
            {
                float percent = manualCooldown / 120f;
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
