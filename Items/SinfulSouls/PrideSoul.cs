using ShardsOfAtheria.Players;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SinfulSouls
{
    public class PrideSoul : SinfulSouls
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 36;
            base.SetStaticDefaults();
        }

        public override bool? UseItem(Player player)
        {
            player.AddBuff(ModContent.BuffType<PrideBuff>(), 2);
            return base.UseItem(player);
        }
    }

    public class PridePlayer : ModPlayer
    {
        public int prideTimer;
        public float pride;

        public override void PreUpdate()
        {
            if (!Player.HasBuff(ModContent.BuffType<PrideBuff>()))
            {
                pride = 0;
                prideTimer = 0;
            }
            else if (Player.GetModPlayer<SoAPlayer>().inCombat == 0 && pride > 0)
            {
                prideTimer--;
                if (prideTimer <= -60)
                {
                    pride -= .02f;
                    prideTimer = 0;
                }
            }
        }

        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            pride = 0;
            prideTimer = 0;
        }

        public override void OnHitByProjectile(Projectile proj, int damage, bool crit)
        {
            pride = 0;
            prideTimer = 0;
        }
    }

    public class PrideBuff : SinfulSoulBuff
    {
        public override void SetStaticDefaults()
        {
            Description.SetDefault(Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.PrideSoul"));
            base.SetStaticDefaults();
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<SinfulPlayer>().SevenSoulUsed = 5;
            if (player.GetModPlayer<SoAPlayer>().inCombat > 0)
                player.GetModPlayer<PridePlayer>().prideTimer++;
            if (player.GetModPlayer<PridePlayer>().prideTimer >= 600)
            {
                player.GetModPlayer<PridePlayer>().pride += .02f;
                player.GetModPlayer<PridePlayer>().prideTimer = 0;
            }
            player.GetDamage(DamageClass.Generic) += player.GetModPlayer<PridePlayer>().pride;
            base.Update(player, ref buffIndex);
        }
    }
}
