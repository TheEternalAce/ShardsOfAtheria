using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Items.SlayerItems.SoulCrystals;
using ShardsOfAtheria.Items.Weapons;
using ShardsOfAtheria.Projectiles;
using ShardsOfAtheria.Projectiles.Other;
using ShardsOfAtheria.Projectiles.Weapon.Magic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace ShardsOfAtheria.Items
{
	public class SoAGlobalItem : GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.SlimeCrown || item.type == ItemID.SuspiciousLookingEye || item.type == ItemID.BloodySpine || item.type == ItemID.WormFood || item.type == ItemID.Abeemination
                || item.type == ItemID.ClothierVoodooDoll || item.type == ItemID.DeerThing || item.type == ItemID.GuideVoodooDoll || item.type == ItemID.QueenSlimeCrystal || item.type == ItemID.MechanicalWorm
                || item.type == ItemID.MechanicalSkull || item.type == ItemID.MechanicalEye || item.type == ItemID.LihzahrdPowerCell || item.type == ItemID.TruffleWorm || item.type == ItemID.EmpressButterfly
                || item.type == ItemID.CelestialSigil)
            {
                item.value = Item.buyPrice(0, 5);
            }
        }

        public override bool? UseItem(Item item, Player player)
        {
            Vector2 vel = Vector2.Normalize(Main.MouseWorld - player.Center);
            if (player.GetModPlayer<SlayerPlayer>().SkullSoul == SoulCrystalStatus.Absorbed && item.damage > 0 && player.itemAnimation == item.useTime)
            {
                Projectile.NewProjectile(player.GetProjectileSource_Item(item), player.Center, vel * 3.5f, ProjectileID.BookOfSkullsSkull, 40, 3.5f, player.whoAmI);
            }
            if (player.GetModPlayer<SlayerPlayer>().PrimeSoul == SoulCrystalStatus.Absorbed && item.damage > 0 && player.itemAnimation == item.useTime)
            {
                if (Main.rand.Next(2) == 0)
                    Projectile.NewProjectile(player.GetProjectileSource_Item(item), player.Center, vel * 10f, ProjectileID.MiniRetinaLaser, 40, 3.5f, player.whoAmI);
                else Projectile.NewProjectile(player.GetProjectileSource_Item(item), player.Center, vel * 8f, ProjectileID.RocketI, 40, 3.5f, player.whoAmI);
            }
            if (player.GetModPlayer<SlayerPlayer>().EaterSoul == SoulCrystalStatus.Absorbed && item.damage > 0 && player.itemAnimation == item.useTime)
            {
                Projectile.NewProjectile(player.GetProjectileSource_Item(item), player.Center, vel * 16f, ModContent.ProjectileType<VileShot>(), 30, 1, player.whoAmI);
                SoundEngine.PlaySound(SoundID.Item17);
            }
            if (player.GetModPlayer<SlayerPlayer>().PlantSoul == SoulCrystalStatus.Absorbed && item.damage > 0 && player.itemAnimation == item.useTime)
            {
                Projectile.NewProjectile(player.GetProjectileSource_Item(item), player.Center, vel * 16f, ModContent.ProjectileType<VenomSeed>(), 30, 1, player.whoAmI);
                SoundEngine.PlaySound(SoundID.Item17);
            }
            return base.UseItem(item, player);
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if (player.HeldItem.damage > 0 && player.HasBuff(ModContent.BuffType<CreeperShield>()))
                return false;
            return base.CanUseItem(item, player);
        }

        public override void UpdateInventory(Item item, Player player)
        {
            if (player.GetModPlayer<SlayerPlayer>().BrainSoul == SoulCrystalStatus.None)
            {
                Item copy = new(item.type);
                item.damage = copy.damage;
            }
        }
    }
}
