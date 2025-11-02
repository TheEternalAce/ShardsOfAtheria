using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SinfulSouls
{
    public class GreedSoul : SinfulSouls
    {
        public override int SoulBuffType => ModContent.BuffType<GreedBuff>();
    }

    public class GreedPlayer : ModPlayer
    {
        public bool soulActive;

        public override void ResetEffects()
        {
            soulActive = false;
        }

        public override bool OnPickup(Item item)
        {
            if (item.value > 1000) Player.AddBuff<GreedFire>(150);
            return base.OnPickup(item);
        }

        public override void UpdateBadLifeRegen()
        {
            if (Player.HasBuff<GreedFire>())
            {
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;
                Player.lifeRegenTime = 0;
                Player.lifeRegen -= 20;
            }
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            if (soulActive)
            {
                int i = 0;
                foreach (var item in Player.inventory)
                {
                    if (item.IsAir) continue;
                    if (i <= 9) continue;
                    if (Main.rand.NextBool()) continue;
                    Item.NewItem(Player.GetSource_Death(), Player.Hitbox, item);
                    item.TurnToAir();
                    i++;
                }
            }
        }
    }

    public class GreedBuff : SinfulSoulBuff
    {
        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            tip = ShardsHelpers.Localize("Items.GreedSoul.Tooltip");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.HasItem(ItemID.GoldCoin))
            {
                for (int i = 0; i < player.inventory[player.FindItem(ItemID.GoldCoin)].stack && i < 10; i++)
                {
                    player.GetDamage(DamageClass.Generic) += .05f;
                    player.statDefense -= 2;
                }
            }
            player.Greed().soulActive = true;
            base.Update(player, ref buffIndex);
        }
    }

    public class GreedFire : ModBuff
    {
        public override void SetStaticDefaults()
        {
            BuffID.Sets.LongerExpertDebuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
            Main.debuff[Type] = true;
        }
    }
}
