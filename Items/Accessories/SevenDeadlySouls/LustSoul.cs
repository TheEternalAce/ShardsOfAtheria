using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories.SevenDeadlySouls
{
    public class LustSoul : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increases minion slots by 5\n" +
                "Damage reduced by 20%\n" +
                "Sway NPCs into lowering their prices\n" +
                "Lovestruck buff\n" +
                "Damaging enemies may cause them to drop hearts");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 36;
            Item.value = Item.buyPrice(gold: 2, silver: 10);
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Generic) -= .2f;
            player.maxMinions += 5;
            player.AddBuff(BuffID.Lovestruck, 2);
            player.discount = true;
            player.GetModPlayer<LustPlayer>().lustSoul = true;
        }
    }

    public class LustPlayer : ModPlayer
    {
        public bool lustSoul;

        public override void ResetEffects()
        {
            lustSoul = false;
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (lustSoul && Main.rand.Next(59) == 0)
                Item.NewItem(target.GetSource_OnHurt(item), target.getRect(), ItemID.Heart);
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (lustSoul && Main.rand.Next(99) == 0)
                Item.NewItem(target.GetSource_OnHurt(proj), target.getRect(), ItemID.Heart);
        }
    }
}
