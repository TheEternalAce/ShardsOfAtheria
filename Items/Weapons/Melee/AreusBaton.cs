using Microsoft.Xna.Framework;
using ShardsOfAtheria.Projectiles.Melee.Baton;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Extensions;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class AreusBaton : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddAreus();
            Item.AddDamageType(5);
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 76;

            Item.damage = 82;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.knockBack = 3.84f;

            Item.useTime = 6;
            Item.useAnimation = 6;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;

            Item.shootSpeed = 3.2f;
            Item.rare = ItemDefaults.RarityDukeFishron;
            Item.value = Item.sellPrice(0, 4, 25);
            Item.shoot = ModContent.ProjectileType<AreusTonfa>();
        }

        public override bool MeleePrefix()
        {
            return true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useTime = 18;
                Item.useAnimation = 18;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.shootSpeed = 30f;
                Item.shoot = ModContent.ProjectileType<ElecBaton>();
            }
            else
            {
                Item.useTime = 4;
                Item.useAnimation = 4;
                Item.useStyle = ItemUseStyleID.Rapier;
                Item.shootSpeed = 3.2f;
                Item.shoot = ModContent.ProjectileType<AreusTonfa>();
            }
            return base.CanUseItem(player);
        }

        public override void HoldItem(Player player)
        {
            if (player.IsLocal())
            {
                if (player.ItemAnimationActive && player.altFunctionUse != 2) Item.noGrabDelay++;
                else if (player.itemAnimation == 0 && Item.noGrabDelay >= 60)
                {
                    Item.noGrabDelay = 0;
                    Vector2 position = player.Center;
                    Vector2 velocity = player.DirectionTo(Main.MouseWorld) * 30;
                    Projectile.NewProjectile(player.GetSource_FromThis(), position, velocity, ModContent.ProjectileType<ElecBaton>(),
                        player.GetWeaponDamage(Item) * 2, player.GetWeaponKnockback(Item));
                    player.SetDummyItemTime(30);
                }
                else if (Item.noGrabDelay > 0) Item.noGrabDelay = 0;
            }
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (type == ModContent.ProjectileType<AreusTonfa>())
            {
                velocity = velocity.RotatedByRandom(MathHelper.ToRadians(15f));
                velocity.Normalize();
                velocity *= 3.2f;
            }
            else if (type == ModContent.ProjectileType<ElecBaton>())
            {
                velocity.Normalize();
                velocity *= 30f;
            }
        }
    }
}