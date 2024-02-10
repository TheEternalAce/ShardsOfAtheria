using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Items.Weapons.Ammo;
using ShardsOfAtheria.Projectiles.Ranged;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class PlagueRailgun : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddElement(3);
            Item.AddRedemptionElement(11);
        }

        public override void SetDefaults()
        {
            Item.width = 112;
            Item.height = 28;

            Item.damage = 30;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 7f;
            Item.crit = 6;

            Item.useTime = 48;
            Item.useAnimation = 48;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item38;
            Item.noMelee = true;

            Item.rare = ItemDefaults.RarityDungeon;
            Item.value = ItemDefaults.ValueEarlyHardmode;
            Item.shoot = ModContent.ProjectileType<PlagueBeam>();
            Item.shootSpeed = 1f;
            Item.useAmmo = ModContent.ItemType<PlagueCell>();
            Item.ArmorPenetration = 20;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<BionicBarItem>(20)
                .AddIngredient(ItemID.HallowedBar, 10)
                .AddIngredient<PlagueCell>(30)
                .AddIngredient(ItemID.Wire, 20)
                .AddTile(TileID.Anvils)
                .Register();
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() < 0.66f;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            int multiplier = 1;
            if (NPC.downedMoonlord)
            {
                multiplier++;
            }
            if (NPC.downedAncientCultist)
            {
                multiplier++;
            }
            if (NPC.downedGolemBoss)
            {
                multiplier++;
            }
            if (NPC.downedPlantBoss)
            {
                multiplier++;
            }
            if (NPC.downedMechBossAny)
            {
                multiplier++;
            }
            if (Main.hardMode)
            {
                multiplier++;
            }
            if (NPC.downedBoss3)
            {
                multiplier++;
            }
            if (NPC.downedBoss2)
            {
                multiplier++;
            }
            if (NPC.downedBoss1)
            {
                multiplier++;
            }
            damage *= multiplier;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 3);
        }

        public override void HoldItem(Player player)
        {
            player.scope = true;
        }
    }
}