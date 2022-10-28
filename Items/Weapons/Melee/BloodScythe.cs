using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class BloodScythe : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 70;
            Item.height = 64;

            Item.damage = 190;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6;
            Item.crit = 100;
            Item.autoReuse = false;
            Item.useTurn = true;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item71;

            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 4);
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.WitheredArmor, 600);
            target.AddBuff(BuffID.WitheredWeapon, 600);
            target.AddBuff(BuffID.Ichor, 600);
            target.AddBuff(BuffID.Bleeding, 600);
            target.AddBuff(BuffID.Daybreak, 600);
        }
    }
}