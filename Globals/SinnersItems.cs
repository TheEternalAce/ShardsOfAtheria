using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.Sinner;
using ShardsOfAtheria.Items.Weapons.Summon;
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
            if (player.Sinner().PridefulSinner)
                damage -= uses * 0.02f;
        }

        public override void ModifyShootStats(Item item, Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            if (player.Sinner().LustfulSinner)
            {
                float rotation = MathHelper.PiOver4;
                if (item.type == ModContent.ItemType<Lilith>()) rotation = MathHelper.Pi / 6f;
                velocity = velocity.RotatedByRandom(rotation);
            }
        }

        public override bool? UseItem(Item item, Player player)
        {
            var sinner = player.Sinner();
            bool wellFedBuff = item.buffType == BuffID.WellFed || item.buffType == BuffID.WellFed2 || item.buffType == BuffID.WellFed3;
            if (sinner.GluttonousSinner && ((item.buffType > 0 && !wellFedBuff) || item.healLife > 0 || item.healMana > 0) && !item.IsWeapon())
                player.AddBuff<GluttonyAcid>(SinnerPlayer.GluttonyAcidDuration);
            if (sinner.PridefulSinner && player.InCombat() && item.IsWeapon() && !item.DamageType.CountsAsClass(DamageClass.Summon))
            {
                uses++;
                sinner.prideAttacksMade++;
                if (sinner.prideAttackTimer <= 0) sinner.prideAttackTimer = 300;
            }
            return null;
        }

        public override bool ConsumeItem(Item item, Player player)
        {
            var sinner = player.Sinner();
            if (sinner.GluttonousSinner)
            {
                int gluttonyHealing = item.buffTime;
                if (item.buffType == BuffID.WellFed) gluttonyHealing /= 180;
                else if (item.buffType == BuffID.WellFed2) gluttonyHealing /= 60;
                else if (item.buffType == BuffID.WellFed3) gluttonyHealing /= 30;
                else gluttonyHealing = 0;
                if (gluttonyHealing > 0)
                {
                    player.Heal(gluttonyHealing / 2);
                    sinner.gluttonyHunger += gluttonyHealing;
                    player.buffImmune[ModContent.BuffType<GluttonyAcid>()] = true;
                }
            }
            return base.ConsumeItem(item, player);
        }
    }
}
