using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.AreusChips
{
    public class BerserkChip : AreusArmorChip
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            slotType = SlotChest;
        }

        public override void ChipEffect(Player player)
        {
            base.ChipEffect(player);
            player.GetDamage(DamageClass.Generic) += 0.15f;
            player.GetModPlayer<BerserkPlayer>().berserk = true;
        }
    }

    public class BerserkPlayer : ModPlayer
    {
        public bool berserk;

        public override void ResetEffects()
        {
            berserk = false;
        }
    }

    public class BerserkProjectile : GlobalProjectile
    {
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (source is EntitySource_ItemUse_WithAmmo itemSource)
            {
                if (itemSource.Player.GetModPlayer<BerserkPlayer>().berserk)
                {
                    var rotation = MathHelper.ToRadians(15f);
                    projectile.velocity = projectile.velocity.RotatedByRandom(rotation);
                }
            }
        }
    }
}