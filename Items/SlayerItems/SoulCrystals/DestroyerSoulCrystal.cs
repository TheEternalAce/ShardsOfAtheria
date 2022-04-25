using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.NPCs;
using ShardsOfAtheria.Projectiles.Other;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SlayerItems.SoulCrystals
{
    public class DestroyerSoulCrystal : SoulCrystal
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Crystal (The Destroyer)");
            Tooltip.SetDefault("Summon a Destroyer's Probe that fires at your cursor besides you\n" +
                "Taking over 100 damage spawns another temporary Destroyer's Probe that will fire at nearby enemies");
        }

        public override bool? UseItem(Player player)
        {
            absorbSoulTimer--;
            if (absorbSoulTimer == 0 || ModContent.GetInstance<ClientSideConfig>().instantAbsorb)
            {
                Main.LocalPlayer.GetModPlayer<SlayerPlayer>().DestroyerSoul = SoulCrystalStatus.Absorbed;
                if (player.ownedProjectileCounts[ModContent.ProjectileType<TheDestroyersProbe>()] == 0)
                    Projectile.NewProjectile(player.GetProjectileSource_Item(Item), player.Center, Vector2.Zero, ModContent.ProjectileType<TheDestroyersProbe>(), 0, 0f, player.whoAmI);
            }
            return base.UseItem(player);
        }
    }
}
