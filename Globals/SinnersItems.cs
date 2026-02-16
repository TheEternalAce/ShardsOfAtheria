using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.PlayerDebuff.SinDebuffs;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Globals
{
    public class SinnersItems : GlobalItem
    {
        public int uses = 0;
        int useTimer = 0;

        public override bool InstancePerEntity => true;

        public override void UpdateInventory(Item item, Player player)
        {
            if (!item.IsWeapon()) { uses = 0; useTimer = 0; }
            if (uses > 0)
            {
                if (++useTimer > 30)
                {
                    uses--;
                    useTimer = 0;
                }
                if (uses > 25) uses = 25;
            }
            else useTimer = 0;
        }

        public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
        {
            damage -= uses * 0.02f;
        }

        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.Sinner().sinID == SinnerPlayer.LUST) velocity = velocity.RotatedByRandom(MathHelper.PiOver4);
        }

        public override bool? UseItem(Item item, Player player)
        {
            var sinner = player.Sinner();
            bool wellFedBuff = item.buffType == BuffID.WellFed || item.buffType == BuffID.WellFed2 || item.buffType == BuffID.WellFed3;
            if (sinner.sinID == SinnerPlayer.GLUTTONY && ((item.buffType > 0 && !wellFedBuff) || item.healLife > 0 || item.healMana > 0) && !item.IsWeapon())
            {
                player.AddBuff<GluttonyAcid>(300);
            }
            if (sinner.sinID == SinnerPlayer.PRIDE && player.InCombat() && item.IsWeapon() && !item.DamageType.CountsAsClass(DamageClass.Summon))
            {
                uses++;
                sinner.attacksMade++;
                if (sinner.attackTimer <= 0) sinner.attackTimer = 300;
            }
            return null;
        }

        public override bool ConsumeItem(Item item, Player player)
        {
            var sinner = player.Sinner();
            if (sinner.sinID == SinnerPlayer.GLUTTONY)
            {
                int gluttonyHealing = item.buffTime;
                if (item.buffType == BuffID.WellFed) gluttonyHealing /= 180;
                else if (item.buffType == BuffID.WellFed2) gluttonyHealing /= 60;
                else if (item.buffType == BuffID.WellFed3) gluttonyHealing /= 30;
                else gluttonyHealing = 0;
                if (gluttonyHealing > 0)
                {
                    player.Heal(gluttonyHealing / 2);
                    sinner.hunger += gluttonyHealing;
                }
            }
            return base.ConsumeItem(item, player);
        }
    }
}
