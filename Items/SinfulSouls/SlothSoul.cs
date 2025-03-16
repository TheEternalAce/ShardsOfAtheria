using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SinfulSouls
{
    public class SlothSoul : SinfulSouls
    {
        public override int SoulBuffType => ModContent.BuffType<SlothBuff>();
    }

    public class SlothPlayer : ModPlayer
    {
        public bool soulActive;
        bool motiveSet = false;

        public override void ResetEffects()
        {
            soulActive = false;
        }

        public override void PreUpdate()
        {
            if (soulActive && Main.dayTime && Main.time == 0)
            {
                Player.TryClearBuff<Apathy>();
                Player.TryClearBuff<Motivation>();
                int motive = ModContent.BuffType<Apathy>();
                if (Main.rand.NextBool(3)) motive = ModContent.BuffType<Motivation>();
                Player.AddBuff(motive, 2);
            }
        }
    }

    public class SlothBuff : SinfulSoulBuff
    {
        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            tip = ShardsHelpers.Localize("Items.SlothSoul.Tooltip");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.Sloth().soulActive = true;
            player.lifeRegen += 16;
            if (player.mount.Active) player.statDefense += 5;
            base.Update(player, ref buffIndex);
        }
    }

    public class Apathy : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            BuffID.Sets.TimeLeftDoesNotDecrease[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (!player.Sloth().soulActive) { player.DelBuff(buffIndex); buffIndex--; }
            player.GetAttackSpeed(DamageClass.Generic) -= 0.1f;
            player.moveSpeed -= 0.1f;
            player.GetDamage(DamageClass.Generic) += 0.15f;
        }
    }

    public class Motivation : ModBuff
    {
        public override string Texture => "ShardsOfAtheria/Items/SinfulSouls/SlothBuff";
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            BuffID.Sets.TimeLeftDoesNotDecrease[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (!player.Sloth().soulActive) { player.DelBuff(buffIndex); buffIndex--; }
            player.GetAttackSpeed(DamageClass.Generic) += 0.1f;
            player.moveSpeed += 0.1f;
            player.GetDamage(DamageClass.Generic) -= 0.15f;
        }
    }
}
