using ShardsOfAtheria.Projectiles.Other;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.SevenDeadlySouls
{
    public class GluttonySoul : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increases melee damage by 15%\n" +
                "Defense reduced by 15\n" +
                "Starving debuff\n" +
                "Critical strikes on enemies creates a food chunk that will heal you when it makes contact\n" +
                "If an attack kills an enemy the food chuck will heal for more");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.buyPrice(gold: 2, silver: 10);
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Melee) += .15f;
            player.statDefense -= 15;
            player.AddBuff(BuffID.Starving, 2);
            player.GetModPlayer<GluttonyPlayer>().gluttonySoul = true;
        }
    }

    public class GluttonyPlayer : ModPlayer
    {
        public bool gluttonySoul;

        public override void ResetEffects()
        {
            gluttonySoul = false;
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (gluttonySoul)
            {
                if (target.life > 0 && crit)
                {
                    Projectile.NewProjectile(target.GetSource_OnHurt(Player), target.Center, Player.Center - target.Center * 10f, ModContent.ProjectileType<FoodChunk>(), 0, 0, Player.whoAmI, 0f);
                }
                if (target.life <= 0)
                {
                    Projectile.NewProjectile(target.GetSource_OnHurt(Player), target.Center, Player.Center - target.Center * 10f, ModContent.ProjectileType<FoodChunk>(), 0, 0, Player.whoAmI, 1f);
                }
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (gluttonySoul)
            {
                if (target.lifeMax > 5 && target.life > 0 && crit)
                {
                    Projectile.NewProjectile(target.GetSource_OnHurt(Player), target.Center, Player.Center - target.Center * 10f, ModContent.ProjectileType<FoodChunk>(), 0, 0, Player.whoAmI, 0f);
                }
                if (target.life <= 0)
                {
                    Projectile.NewProjectile(target.GetSource_OnHurt(Player), target.Center, Player.Center - target.Center * 10f, ModContent.ProjectileType<FoodChunk>(), 0, 0, Player.whoAmI, 1f);
                }
            }
        }
    }
}
