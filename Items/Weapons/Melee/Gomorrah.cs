using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Projectiles.Melee.Gomorrah;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI.Chat;
using Terraria.Utilities;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class Gomorrah : ModItem
    {
        int gore = 0;

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

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2 && gore > 0)
            {
                Item.noUseGraphic = true;
                Item.noMelee = true;
            }
            else
            {
                Item.noUseGraphic = false;
                Item.noMelee = false;
            }
            return true;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips[tooltips.GetIndex("ItemName")].Text += $" ({gore})";
        }

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (Main.playerInventory || Main.gameMenu || !Main.PlayerLoaded)
                return;

            var center = position;
            center.X -= TextureAssets.InventoryBack.Value.Width / 2f * Main.inventoryScale;
            center.Y += TextureAssets.InventoryBack.Value.Height / 2f * Main.inventoryScale;
            string viscera = gore.ToString();

            var color = Color.Red;
            var font = FontAssets.MouseText.Value;
            ChatManager.DrawColorCodedStringWithShadow(spriteBatch, font, viscera, center + new Vector2(8f, -24f) * Main.inventoryScale, color, 0f, Vector2.Zero, new Vector2(1f) * Main.inventoryScale * 0.8f, spread: Main.inventoryScale);
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            bool canBeChased = target.chaseable && target.lifeMax > 5 && !target.immortal;
            if (canBeChased) gore += 5;
        }

        readonly int whip = ModContent.ProjectileType<TendonWhip>();
        readonly int spike = ModContent.ProjectileType<BoneSpike>();
        readonly int pile = ModContent.ProjectileType<FleshBit>();
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            WeightedRandom<int> goreProjectile = new();
            goreProjectile.Add(whip);
            goreProjectile.Add(spike);
            goreProjectile.Add(pile);

            type = goreProjectile;
            if (type == whip)
            {
                velocity *= 3f;
                damage = (int)(damage * 1.8f);
            }
            else if (type == spike)
            {
                velocity *= 16f;
                damage = (int)(damage * 1.5f);
            }
            else if (type == pile)
            {
                velocity *= 12f;
                damage = (int)(damage * 0.5f);
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2 && gore > 0)
            {
                gore--;
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
            return false;
        }
    }
}