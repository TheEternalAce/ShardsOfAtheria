using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Projectiles.Melee.BloodArtifact;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class BloodScythe : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;

            Item.AddDamageType(11);
            Item.AddElement(1, 3);
            Item.AddEraser();
            Item.AddRedemptionElement(1, 12);
        }

        public override void SetDefaults()
        {
            Item.width = 94;
            Item.height = 76;

            Item.damage = 150;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6f;
            Item.crit = 96;

            Item.useTime = 50;
            Item.useAnimation = 50;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.channel = true;

            Item.shootSpeed = 1f;
            Item.rare = ItemDefaults.RarityDeath;
            Item.value = 321000;
            Item.shoot = ModContent.ProjectileType<DeathScythe>();
        }

        public override bool MeleePrefix()
        {
            return true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                float attackSpeed = player.GetWeaponAttackSpeed(Item);
                int useTime = (int)(40 / attackSpeed);
                player.itemTime = useTime;
                player.itemAnimation = useTime;

                velocity *= 16f;
                type = ModContent.ProjectileType<BloodSwordFriendly>();
                damage = (int)(damage * 0.4f);
            }
        }

        public override bool? UseItem(Player player)
        {
            Item.FixSwing(player);
            return true;
        }
    }
}