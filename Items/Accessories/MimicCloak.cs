using ShardsOfAtheria.Buffs.PlayerDebuff.Cooldowns;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Accessories
{
    public class MimicCloak : ModItem
    {
        public override string Texture => SoA.PlaceholderTexture;

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 34;
            Item.accessory = true;

            Item.rare = ItemDefaults.RarityEarlyHardmode;
            Item.value = ItemDefaults.ValueEarlyHardmode;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.Shards().disguiseCloak = true;
            if (player.HasBuff<DisguiseRegenerating>())
            {
                player.GetDamage(DamageClass.Generic) += 0.5f;
                player.statDefense -= 15;
            }
        }
    }
}