using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SinfulSouls
{
    public class EnvySoul : SinfulSouls
    {
        public override int SoulBuffType => ModContent.BuffType<EnvyBuff>();
    }

    public class EnvyPlayer : ModPlayer
    {
        public bool targetFound;
        public bool soulActive;
        public NPC target;
        public int targetStrikes;

        public override void ResetEffects()
        {
            targetFound = false;
            soulActive = false;
        }

        public override void PreUpdate()
        {
            targetFound = target != null;
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (soulActive)
            {
                if (target.life <= 0 && target == this.target)
                {
                    targetFound = false;
                    targetStrikes = 0;
                }
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (soulActive)
            {
                if (target.life <= 0 && target == this.target)
                {
                    targetFound = false;
                    targetStrikes = 0;
                }
            }
        }

        public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (soulActive)
            {
                if (target != this.target || !targetFound)
                {
                    this.target = target;
                    targetStrikes = 0;
                }
                else if (target == this.target)
                {
                    modifiers.FlatBonusDamage += targetStrikes * 3;
                    targetStrikes++;
                }
            }
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (soulActive)
            {
                if (target != this.target || !targetFound)
                {
                    this.target = target;
                    targetStrikes = 0;
                }
                else if (target == this.target)
                {
                    modifiers.FlatBonusDamage += targetStrikes * 3;
                    targetStrikes++;
                }
            }
        }
    }

    public class EnvyBuff : SinfulSoulBuff
    {
        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            tip = ShardsHelpers.Localize("Items.EnvySoul.Tooltip");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.Envy().soulActive = true;
            base.Update(player, ref buffIndex);
        }
    }
}
