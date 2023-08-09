using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerBuff
{
    public class MessiahRekkoha : ModBuff
    {
        public override string Texture => SoA.BuffTemplate;

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<RekkohaPlayer>().Rekkoha = true;
            if (player.buffTime[buffIndex] % 10 == 0)
            {
                int heal = player.statLifeMax2 * 8 / 100;
                player.HealEffect(heal);
                player.statLife += heal;
            }
        }
    }

    public class RekkohaPlayer : ModPlayer
    {
        public bool Rekkoha;

        public override void ResetEffects()
        {
            Rekkoha = false;
        }

        public override void SetControls()
        {
            if (Rekkoha)
            {
                Player.controlRight = false;
                Player.controlLeft = false;
                Player.controlUp = false;
                Player.controlDown = false;
                Player.controlJump = false;
                Player.controlHook = false;
                Player.controlUseItem = false;
                Player.controlUseTile = false;
            }
        }

        public override bool FreeDodge(Player.HurtInfo info)
        {
            if (Rekkoha)
            {
                return true;
            }
            return base.FreeDodge(info);
        }
    }
}
