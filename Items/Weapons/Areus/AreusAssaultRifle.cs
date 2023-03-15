using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Weapon.Areus;
using ShardsOfAtheria.Projectiles.Weapon.Magic;
using ShardsOfAtheria.Systems;
using ShardsOfAtheria.Tiles.Crafting;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Areus
{
    public class AreusAssaultRifle : OverchargeWeapon
    {
        private int fireMode;

        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            SoAGlobalItem.AreusWeapon.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.width = 70;
            Item.height = 28;
            Item.DefaultToRangedWeapon(ProjectileID.PurificationPowder, AmmoID.Bullet, 6, 16f);

            Item.damage = 96;
            Item.knockBack = 4f;
            Item.crit = 5;
            chargeVelocity = 4f;

            Item.UseSound = SoundID.Item11;

            Item.rare = ItemRarityID.Cyan;
            Item.value = Item.sellPrice(0, 4, 25);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<AreusShard>(), 15)
                .AddRecipeGroup(ShardsRecipes.Gold, 5)
                .AddIngredient(ItemID.FragmentVortex, 10)
                .AddTile(ModContent.TileType<AreusFabricator>())
                .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, 0);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (fireMode == 1)
            {
                velocity = velocity.RotatedByRandom(MathHelper.ToRadians(2));
            }
            if (fireMode == 2)
            {
                velocity = velocity.RotatedByRandom(MathHelper.ToRadians(5));
            }
        }

        public override bool CanConsumeAmmo(Item item, Player player)
        {
            if (fireMode == 1)
                return !(player.itemAnimation < Item.useAnimation - 2) || Main.rand.NextFloat() >= .66f;
            return Main.rand.NextFloat() >= .66f;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                chargeAmount = 0f;
                Item.useTime = 6;
                Item.useAnimation = 6;
                Item.reuseDelay = 20;
                Item.autoReuse = false;
                Item.shoot = ItemID.None;
                Item.UseSound = SoundID.Unlock;

                fireMode += 1;
                if (fireMode == 3)
                    fireMode = 0;

                if (fireMode == 0)
                    CombatText.NewText(player.Hitbox, Color.White, Language.GetTextValue("Mods.ShardsOfAtheria.Common.FiringMode1"));
                if (fireMode == 1)
                    CombatText.NewText(player.Hitbox, Color.White, Language.GetTextValue("Mods.ShardsOfAtheria.Common.FiringMode2"));
                if (fireMode == 2)
                    CombatText.NewText(player.Hitbox, Color.White, Language.GetTextValue("Mods.ShardsOfAtheria.Common.FiringMode3"));
            }
            else
            {
                Item.shoot = ProjectileID.PurificationPowder;
                chargeAmount = 0.1f;
                if (fireMode == 0)
                {
                    Item.UseSound = SoundID.Item11;
                    Item.useTime = 6;
                    Item.useAnimation = 6;
                    Item.reuseDelay = 0;
                    Item.autoReuse = false;
                }
                else if (fireMode == 1)
                {
                    Item.UseSound = SoundID.Item31;
                    Item.useTime = 4;
                    Item.useAnimation = 12;
                    Item.reuseDelay = 18;
                    Item.autoReuse = true;
                }
                else if (fireMode == 2)
                {
                    Item.UseSound = SoundID.Item11;
                    Item.useTime = 8;
                    Item.useAnimation = 8;
                    Item.reuseDelay = 0;
                    Item.autoReuse = true;
                }
            }
            return base.CanUseItem(player);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            switch (fireMode)
            {
                case 0:
                    tooltips.Add(new TooltipLine(Mod, "Fire mode", Language.GetTextValue("Mods.ShardsOfAtheria.Common.FiringMode1")));
                    break;
                case 1:
                    tooltips.Add(new TooltipLine(Mod, "Fire mode", Language.GetTextValue("Mods.ShardsOfAtheria.Common.FiringMode2")));
                    break;
                case 2:
                    tooltips.Add(new TooltipLine(Mod, "Fire mode", Language.GetTextValue("Mods.ShardsOfAtheria.Common.FiringMode3")));
                    break;
            }
            base.ModifyTooltips(tooltips);
        }

        public override void DoOverchargeEffect(Player player, int projType, float damageMultiplier, Vector2 velocity, float ai1 = 1f)
        {
            switch (fireMode)
            {
                case 0:
                    base.DoOverchargeEffect(player, ModContent.ProjectileType<LightningBoltFriendly>(), damageMultiplier, velocity, ai1);
                    break;
                case 1:
                    for (int i = 0; i < 3; i++)
                    {
                        Vector2 vel = Vector2.Normalize(velocity).RotatedByRandom(MathHelper.ToRadians(20f)) * Item.shootSpeed;
                        Projectile proj = Projectile.NewProjectileDirect(Item.GetSource_ItemUse(Item), player.Center, vel,
                            ModContent.ProjectileType<LightningBoltFriendly>(), (int)(player.GetWeaponDamage(Item) * damageMultiplier), Item.knockBack, player.whoAmI, 0f, 0f);
                        proj.DamageType = DamageClass.Ranged;
                    }
                    ConsumeOvercharge(player);
                    break;
                case 2:
                    base.DoOverchargeEffect(player, ModContent.ProjectileType<AreusGrenadeProj>(), damageMultiplier, velocity * 8, 0f);
                    break;
            }
        }
    }
}