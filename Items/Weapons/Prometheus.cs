using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Items.Placeable;
using ShardsOfAtheria.Projectiles.Weapon.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons
{
    public class Prometheus : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Left Click to swing an energy scythe, <right> to throw a fireball\n" +
                "'It seems like you're worthy of playing his little game, his game of destiny!'");
        }

        public override void SetDefaults()
        {
            Item.noMelee = false;
            Item.noUseGraphic = false;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.damage = 112;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 13;
            Item.mana = 0;
            Item.shoot = ProjectileID.None;
            Item.useTurn = true;
            Item.width = 54;
            Item.height = 48;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.sellPrice(0,  10);
            Item.rare = ItemRarityID.Red;
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<BionicBarItem>(), 7)
                .AddIngredient(ItemID.Ectoplasm, 5)
                .AddIngredient(ItemID.HellstoneBar, 10)
                .AddIngredient(ModContent.ItemType<SoulOfSpite>(), 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }

        public override bool AltFunctionUse(Player player) => true;

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.noMelee = true;
                Item.noUseGraphic = true;
                Item.useTime = 17;
                Item.useAnimation = 17;
                Item.damage = 97;
                Item.DamageType = DamageClass.Ranged;
                Item.knockBack = 6;
                Item.mana = 15;
                Item.UseSound = SoundID.Item20;
                Item.shoot = ModContent.ProjectileType<PrometheusFire>();
                Item.shootSpeed = 13f;
                Item.useTurn = false;
            }
            else
            {
                Item.noMelee = false;
                Item.noUseGraphic = false;
                Item.useTime = 30;
                Item.useAnimation = 30;
                Item.damage = 112;
                Item.DamageType = DamageClass.Melee;
                Item.knockBack = 13;
                Item.mana = 0;
                Item.UseSound = SoundID.Item71;
                Item.shoot = ProjectileID.None;
                Item.useTurn = true;
            }
            return base.CanUseItem(player);
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (player.HasBuff(ModContent.BuffType<Overdrive>()))
            {
                target.AddBuff(BuffID.CursedInferno, 10 * 60);
                player.AddBuff(BuffID.Ichor, 10 * 60);
            }
            target.AddBuff(BuffID.OnFire, 10 * 60);
            player.AddBuff(BuffID.WeaponImbueIchor, 10 * 60);
        }
    }
}