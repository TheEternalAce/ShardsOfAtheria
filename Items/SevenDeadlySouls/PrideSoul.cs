using ShardsOfAtheria.Players;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SevenDeadlySouls
{
    public class PrideSoul : SevenSouls
    {
        public const string tip = "Every 20 seconds of not taking damage increases damage by 2%";

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault(tip);
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

    public class PrideBuff : SevenSoulsBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pride");
            Description.SetDefault(PrideSoul.tip);
            base.SetStaticDefaults();
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<SevenSoulPlayer>().SevenSoulUsed = 5;
            if (player.GetModPlayer<SoAPlayer>().inCombat > 0)
                player.GetModPlayer<PridePlayer>().prideTimer++;
            if (player.GetModPlayer<PridePlayer>().prideTimer >= 1200)
            {
                player.GetModPlayer<PridePlayer>().pride += .02f;
                player.GetModPlayer<PridePlayer>().prideTimer = 0;
            }
            player.GetDamage(DamageClass.Generic) += player.GetModPlayer<PridePlayer>().pride;
            base.Update(player, ref buffIndex);
        }
    }
}
