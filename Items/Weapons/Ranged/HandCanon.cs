using Microsoft.Xna.Framework;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Effects.ScreenShaking;
//using WebCom.Effects.ScreenShaking;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class HandCanon : ModItem
    {
        public int charge;

        public override void SetStaticDefaults()
        {
            ItemID.Sets.IsRangedSpecialistWeapon[Type] = true;
            Item.AddElement(0);
            Item.AddRedemptionElement(15);
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 24;
            Item.master = true;

            Item.damage = 65;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 4;
            Item.crit = 5;

            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item11;
            Item.noMelee = true;

            Item.shootSpeed = 16f;
            Item.rare = ItemRarityID.Master;
            Item.value = 22500;
            Item.shoot = ProjectileID.Grenade;
            Item.useAmmo = ItemID.Grenade;
        }

        public override bool CanConsumeAmmo(Item item, Player player)
        {
            return player.itemAnimation == player.itemAnimationMax;
        }

        public override void UpdateInventory(Player player)
        {
            if (!player.ItemAnimationActive || player.HeldItem != Item)
            {
                if (charge < 300)
                {
                    charge++;
                    if (SoA.ClientConfig.chargeSound && charge % 25 == 0) SoundEngine.PlaySound(SoA.ZeroCharge, player.Center);
                    for (int i = 0; i < 10; i++)
                    {
                        Vector2 spawnPos = player.Center + Main.rand.NextVector2CircularEdge(50, 50);
                        Dust dust = Dust.NewDustDirect(spawnPos, 0, 0, DustID.Torch, 0, 0, 100);
                        dust.velocity = player.velocity;
                        if (Main.rand.NextBool(3)) dust.velocity += Vector2.Normalize(player.Center - dust.position) * Main.rand.NextFloat(5f);
                        dust.noGravity = true;
                    }
                }
                if (charge == 300 - 1) SoundEngine.PlaySound(SoundID.MaxMana, player.Center);
            }
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            base.ModifyWeaponDamage(player, ref damage);
            if (charge >= 300)
                damage += 1f;
        }

        public override bool CanUseItem(Player player)
        {
            if (charge == 300)
            {
                Item.useTime = 10;
                Item.useAnimation = 30;
                Item.reuseDelay = 6;
            }
            else
            {
                Item.useTime = Item.useAnimation = 28;
                Item.reuseDelay = 0;
            }
            return true;
        }

        public override bool? UseItem(Player player)
        {
            if (charge >= 300)
            {
                ScreenShake.ShakeScreen(6, 60);
            }
            charge = 0;
            SoundEngine.PlaySound(Item.UseSound.Value);
            return base.UseItem(player);
        }
    }
}