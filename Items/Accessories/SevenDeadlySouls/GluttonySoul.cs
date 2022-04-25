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
                "Killing enemies heals 20% of max Life");

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
            if (Player.GetModPlayer<GluttonyPlayer>().gluttonySoul)
            {
                Player player = Main.LocalPlayer;
                if (target.lifeMax > 5 && target.life > 0)
                {
                    player.statLife += 15;
                    CombatText.NewText(Player.getRect(), Colors.RarityGreen, 15);
                }
                if (target.life <= 0)
                {
                    player.statLife += (int)(player.statLifeMax2 * .2f);
                    CombatText.NewText(player.getRect(), Colors.RarityGreen, (int)(player.statLifeMax2 * .2f));
                }
            }
        }
    }
}
