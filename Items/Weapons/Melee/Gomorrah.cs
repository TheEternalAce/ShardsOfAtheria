using Microsoft.Xna.Framework;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Melee.GomorrahProjectiles;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class Gomorrah : SinfulItem
    {
        bool throwDefault = false;

        public override int RequiredSin => CardinalSoulID.Gluttony;

        // Base damages: 50, 130, 230
        public override int[] DamageSpread => [0, 80, 100];

        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
            Item.AddDamageType(11);
            Item.AddElement(3);
            Item.AddRedemptionElement(12);

            Item.AddFinesseWeapon();
        }

        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 48;

            Item.damage = 50;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6;
            Item.crit = 8;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;

            Item.shootSpeed = 1f;
            Item.value = Item.sellPrice(0, 2, 25);
            Item.rare = ItemDefaults.RarityEarlyHardmode;
            Item.shoot = ProjectileID.PurificationPowder;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            throwDefault = !throwDefault;
        }

        public override bool ConsumeItem(Player player)
        {
            return false;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            var sinner = player.CardinalSoul();
            bool cleaverThrow = throwDefault ? !player.controlUp : player.controlUp;
            if ((player.altFunctionUse == 2 && sinner.gluttonyHunger > 100) || cleaverThrow)
            {
                Item.noUseGraphic = true;
                Item.noMelee = true;
            }
            else if (!cleaverThrow)
            {
                Item.noUseGraphic = false;
                Item.noMelee = false;
            }
            return base.CanUseItem(player);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine line = new(Mod, "PrimaryAttackMode", this.GetLocalizedValue("PrimaryAttackMode" + (throwDefault ? 1 : 0)));
            tooltips.Insert(tooltips.GetIndex("Tooltip#"), line);
        }

        readonly int whip = ModContent.ProjectileType<TendonWhip>();
        readonly int spike = ModContent.ProjectileType<BoneSpike>();
        readonly int pile = ModContent.ProjectileType<FleshBit>();
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            bool cleaverThrow = throwDefault ? !player.controlUp : player.controlUp;
            if (player.altFunctionUse == 2 && player.CardinalSoul().gluttonyHunger > 100)
            {
                WeightedRandom<int> goreProjectile = new();
                goreProjectile.Add(whip);
                goreProjectile.Add(spike);
                goreProjectile.Add(pile);

                type = goreProjectile;

                if (type == whip)
                {
                    velocity *= 3f;
                    damage = (int)(damage * 1.5f);
                }
                else if (type == spike)
                {
                    velocity *= 16f;
                    damage = (int)(damage * 1.8f);
                }
                else if (type == pile)
                {
                    velocity *= 12f;
                    damage = (int)(damage * 0.5f);
                }
            }
            else if (cleaverThrow)
            {
                type = ModContent.ProjectileType<Cleaver>();
                velocity *= 15f;
                if (NPC.downedGolemBoss) velocity *= 2f;
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            var sinner = player.CardinalSoul();
            bool cleaverThrow = throwDefault ? !player.controlUp : player.controlUp;
            if (player.altFunctionUse == 2 && sinner.gluttonyHunger >= 100)
            {
                sinner.gluttonyHunger -= 100;
                if (type == pile)
                {
                    int numberProjectiles = 2 + Main.rand.Next(0, 3);
                    float rotation = MathHelper.ToRadians(20);
                    if (numberProjectiles > 1)
                    {
                        for (int i = 0; i < numberProjectiles; i++)
                        {
                            Vector2 newVelocity = velocity.RotatedByRandom(rotation);
                            newVelocity *= 1f - Main.rand.NextFloat(0.3f);
                            Projectile.NewProjectile(source, position, newVelocity, type, damage, knockback);
                        }
                    }
                }
                return true;
            }
            if (cleaverThrow) return true;
            return false;
        }
    }
}