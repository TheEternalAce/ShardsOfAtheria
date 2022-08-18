using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Placeable.Furniture;
using ShardsOfAtheria.Projectiles.Weapon.Melee.Messiah;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class TheMessiah : ModItem
    {
        public int charge;
        public bool theMessiah;

        public override void OnCreate(ItemCreationContext context)
        {
            theMessiah = false;
        }

        public override void SaveData(TagCompound tag)
        {
            tag["theMessiah"] = theMessiah;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.ContainsKey("theMessiah"))
            {
                theMessiah = tag.GetBool("theMessiah");
            }
        }

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Passively charges while in hand and not in use\n" +
                "Max charge level increases damage by 200% and critical strike chance by 60%\n" +
                "'I am the messiah!'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 56;

            Item.damage = 400;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6;
            Item.crit = 0;

            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 15;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(1);
            Item.shoot = ModContent.ProjectileType<MessiahAirSlash>();
        }

        public override void UpdateInventory(Player player)
        {
            if (!theMessiah && Main.myPlayer == player.whoAmI)
            {
                SoundEngine.PlaySound(new SoundStyle($"{nameof(ShardsOfAtheria)}/Sounds/Item/TheMessiah"));
                theMessiah = true;
            }

            if (charge >= 80 && player.ownedProjectileCounts[ModContent.ProjectileType<ChargeOrb>()] < 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    Projectile.NewProjectile(Item.GetSource_FromThis(), player.Center, Vector2.Zero, ModContent.ProjectileType<ChargeOrb>(), 0, 0, player.whoAmI, i);
                }
            }
            if (charge < 200)
                charge += 1;
            else charge = 200;
            if (charge == 199)
            {
                SoundEngine.PlaySound(SoundID.MaxMana);
                CombatText.NewText(player.getRect(), Color.SkyBlue, "Charge ready!");
            }
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (charge >= 200)
            {
                damage += charge * .1f;
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                if (Main.projectile[i].type == ModContent.ProjectileType<ChargeOrb>())
                {
                    Main.projectile[i].Kill();
                }
            }
            if (charge == 200)
            {
                float numberProjectiles = 5;
                float rotation = MathHelper.ToRadians(15);
                position += Vector2.Normalize(velocity) * 10f;
                velocity = velocity.RotatedBy(MathHelper.ToRadians(15 * (Main.MouseWorld.X > player.Center.X ? -1 : 1)));
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                    Projectile.NewProjectile(source, position, (perturbedSpeed / 15f) * 10f, ProjectileID.Spark, damage, knockback, player.whoAmI);
                }
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<MessiahChargeSlash>(), damage, knockback, player.whoAmI);
                return false;
            }
            else if (player.velocity.Y == 0)
            {
                Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<MessiahRanbu1>(), damage, knockback, player.whoAmI);
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}