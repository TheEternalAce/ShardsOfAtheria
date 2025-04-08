using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.AnyDebuff;
using ShardsOfAtheria.Buffs.PlayerDebuff.Cooldowns;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Items.SinfulSouls;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Melee;
using ShardsOfAtheria.Projectiles.Ranged;
using ShardsOfAtheria.ShardsConditions;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class Yamiko : SinfulItem
    {
        public override int RequiredSin => SinfulPlayer.Wrath;

        public override void SetStaticDefaults()
        {
            Item.AddDamageType(11);
            Item.AddElement(0);
            Item.AddElement(2);
            Item.AddRedemptionElement(2);
            Item.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 50;

            Item.damage = 50;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6;
            Item.crit = 6;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;

            Item.shootSpeed = 1;
            Item.rare = ItemDefaults.RarityEarlyHardmode;
            Item.value = Item.sellPrice(0, 1, 25);
            Item.shoot = ModContent.ProjectileType<YamikoSlash>();
        }

        public override bool MeleePrefix()
        {
            return true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2 && !player.HasBuff(ModContent.BuffType<YamikoDashCooldown>()))
            {
                player.velocity = Vector2.Normalize(Main.MouseWorld - player.Center).RotatedByRandom(MathHelper.ToRadians(15)) * 16;
                string deathText = $"{player.name} cut {(player.Male ? "himself" : "herself")}";
                if (SoA.ServerConfig.yamikoInsult)
                {
                    string[] insult = ["How did you manage that? Dumbass.", "Good job idiot, you fatally cut yourself. ", "How could you be so stupid?"];
                    int i = Main.rand.Next(insult.Length);
                    deathText = insult[i] + " (" + deathText + ")";
                }
                player.Hurt(PlayerDeathReason.ByCustomReason(deathText), 100, 0);
                player.SetImmuneTimeForAllTypes(player.longInvince ? 50 : 30);
            }
            return null;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float adjustedItemScale = player.GetAdjustedItemScale(Item); // Get the melee scale of the player and item.
            Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), type, damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale);
            NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI); // Sync the changes in multiplayer.

            if (player.altFunctionUse == 2 && !player.HasBuff(ModContent.BuffType<YamikoDashCooldown>()))
            {
                float numberProjectiles = 4;
                float rotation = MathHelper.ToRadians(10);
                position += Vector2.Normalize(velocity) * 10f;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 10; // Watch out for dividing by 0 if there is only 1 projectile.
                    Projectile proj = Projectile.NewProjectileDirect(source, position, perturbedSpeed, ModContent.ProjectileType<PrometheusFire>(), damage, knockback, player.whoAmI, 2f);
                    proj.DamageType = DamageClass.Melee;
                }
                player.AddBuff(ModContent.BuffType<ElectricShock>(), 120);
                player.AddBuff(ModContent.BuffType<YamikoDashCooldown>(), 120);
            }
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<WrathSoul>()
                .AddCondition(SoAConditions.TransformArmament)
                .Register();
        }
    }
}