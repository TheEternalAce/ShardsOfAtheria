using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Ranged.DeckOfCards;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.DedicatedItems.TheEternalAce
{
    public class DeckOfCards : ModItem
    {
        int choiceCard = 0;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            Item.AddElement(0);
            Item.AddElement(1);
            Item.AddElement(2);
            Item.AddElement(3);
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 22;

            Item.damage = 70;
            Item.DamageType = DamageClass.Ranged;
            if (SoA.ServerConfig.throwingWeapons) Item.DamageType = DamageClass.Throwing;
            Item.knockBack = 4;
            Item.crit = 4;

            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 15;
            Item.rare = ItemDefaults.RarityDevSet;
            Item.value = ItemDefaults.ValueEarlyHardmode;
            Item.shoot = ModContent.ProjectileType<AceOfSpades>();
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2 || player.Overdrive())
            {
                if (++choiceCard >= 4) choiceCard = 0;
                Item.shoot = ChooseCard(player);
            }
            return base.CanUseItem(player);
        }

        int ChooseCard(Player player)
        {
            Item.crit = 4;
            var type = choiceCard switch
            {
                1 => ModContent.ProjectileType<AceOfHearts>(),
                2 => ModContent.ProjectileType<AceOfClubs>(),
                3 => ModContent.ProjectileType<AceOfDiamonds>(),
                _ => ModContent.ProjectileType<AceOfSpades>(),
            };
            if (type == ModContent.ProjectileType<AceOfDiamonds>()) Item.crit = 12;
            string key = this.GetLocalizationKey("CardSuit" + choiceCard);
            if (!player.Overdrive()) CombatText.NewText(player.getRect(), Color.Cyan, Language.GetTextValue(key));
            return type;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (player.Overdrive()) damage *= 0.5f;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.Overdrive())
            {
                type = ModContent.ProjectileType<AceOfClubs>() + Main.rand.Next(4);
                velocity *= Main.rand.NextFloat(0.66f, 1.33f);
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.Overdrive())
            {
                int type2 = ModContent.ProjectileType<AceOfClubs>();
                for (int i = 0; i < 2; i++)
                {
                    var perturbedSpeed = velocity.RotatedByRandom(MathHelper.PiOver4) * Main.rand.NextFloat(0.66f, 1f);
                    var card = type2 + Main.rand.Next(4);
                    Projectile.NewProjectile(source, position, perturbedSpeed, card, damage, knockback);
                }
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override void UpdateInventory(Player player)
        {
            string key = this.GetLocalizationKey("CardSuit" + choiceCard);
            string key2 = this.GetLocalizationKey("DisplayName");
            string name = Language.GetTextValue(key2) + " (" + Language.GetTextValue(key) + ")";
            Item.SetNameOverride(name);
        }
    }
}
