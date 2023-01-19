using MMZeroElements;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class Cataracnia : ModItem
    {
        public override void SetStaticDefaults()
        {
            SacrificeTotal = 1;
            WeaponElements.Metal.Add(Type);
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 54;

            Item.damage = 20;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6;
            Item.crit = 4;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;

            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 10);
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Ichor, 120);
            player.AddBuff(BuffID.Shine, 7200);
            player.AddBuff(BuffID.NightOwl, 7200);
            player.AddBuff(BuffID.Spelunker, 7200);
            player.AddBuff(BuffID.Hunter, 7200);
            player.AddBuff(BuffID.Dangersense, 7200);
        }
    }
}