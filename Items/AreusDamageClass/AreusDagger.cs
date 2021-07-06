using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using SagesMania.Tiles;
using SagesMania.Items.Placeable;
using SagesMania.Projectiles;
using Microsoft.Xna.Framework;
using SagesMania.Buffs;

namespace SagesMania.Items.AreusDamageClass
{
    public class AreusDagger : AreusDamageItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("A dagger with ''shocking'' potential");
        }

        public override void SafeSetDefaults()
        {
            item.damage = 146;
            item.width = 32;
            item.height = 34;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 6;
            item.value = Item.sellPrice(gold: 30);
            item.rare = ItemRarityID.Cyan;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<ElectricBolt>();
            item.shootSpeed = 20;

            areusResourceCost = 1;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<AreusOreItem>(), 20);
            recipe.AddIngredient(ItemID.FragmentVortex, 10);
            recipe.AddTile(ModContent.TileType<AreusForge>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            // Add the Onfire buff to the NPC for 1 second when the weapon hits an NPC
            // 60 frames = 1 second
            target.AddBuff(BuffID.Electrified, 600);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.HasBuff(ModContent.BuffType<Overdrive>()))
                type = ModContent.ProjectileType<ElectricBlade>();
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
    }
}