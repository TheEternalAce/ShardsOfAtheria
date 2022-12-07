using ShardsOfAtheria.Players;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SinfulSouls
{
    public class EnvySoul : SinfulSouls
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            base.SetStaticDefaults();
        }

        public override bool? UseItem(Player player)
        {
            player.AddBuff(ModContent.BuffType<EnvyBuff>(), 2);
            return base.UseItem(player);
        }
    }

    public class EnvyPlayer : ModPlayer
    {
        public bool targetFound;
        public bool envy;
        public NPC target;
        public int focusDamage;

        public override void ResetEffects()
        {
            targetFound = false;
            envy = false;
        }

        public override void PreUpdate()
        {
            targetFound = target != null;
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (envy)
            {
                if (target.life <= 0 && target.type == this.target.type)
                {
                    targetFound = false;
                    focusDamage = 0;
                }
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (envy)
            {
                if (target.life <= 0 && target == this.target)
                {
                    targetFound = false;
                    focusDamage = 0;
                }
            }
        }

        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (envy)
            {
                if (target != this.target || !targetFound)
                {
                    this.target = target;
                    focusDamage = 0;
                }
                else if (target == this.target)
                {
                    damage += focusDamage;
                    focusDamage++;
                }
            }
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (envy)
            {
                if (target != this.target || !targetFound)
                {
                    this.target = target;
                    focusDamage = 0;
                }
                else if (target == this.target)
                {
                    damage += focusDamage;
                    focusDamage++;
                }
            }
        }
    }

    public class EnvyBuff : SinfulSoulBuff
    {
        public override void SetStaticDefaults()
        {
            Description.SetDefault(Language.GetTextValue("Mods.ShardsOfAtheria.ItemTooltip.EnvySoul"));
            base.SetStaticDefaults();
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<SinfulPlayer>().SevenSoulUsed = 1;
            player.GetModPlayer<EnvyPlayer>().envy = true;
            player.AddBuff(BuffID.Darkness, 2);
            base.Update(player, ref buffIndex);
        }
    }
}
