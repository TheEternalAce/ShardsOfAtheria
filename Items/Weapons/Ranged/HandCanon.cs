using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Tools.ToggleItems;
using ShardsOfAtheria.Projectiles.Ranged;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using WebCom.Effects.ScreenShaking;

namespace ShardsOfAtheria.Items.Weapons.Ranged
{
    public class HandCanon : ModItem
    {
        public int charge;
        const int MaxCharge = 300;

        public override void SetStaticDefaults()
        {
            ItemID.Sets.IsRangedSpecialistWeapon[Type] = true;
            Item.AddDamageType(4);
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
            Item.shoot = ModContent.ProjectileType<PlasmaShot>();
        }

        public override void UpdateInventory(Player player)
        {
            var cable = ToggleableTool.GetInstance<BrokenCable>(player);
            if ((!player.ItemAnimationActive || player.HeldItem != Item) && (cable is null || !cable.Active))
            {
                if (charge < MaxCharge)
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
                if (charge == MaxCharge - 1) SoundEngine.PlaySound(SoundID.MaxMana, player.Center);
            }
            if (player.HeldItem == Item && player.ItemAnimationActive && player.ItemAnimationEndingOrEnded) charge = 0;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (charge >= MaxCharge) damage += 1f;
        }

        public override bool CanUseItem(Player player)
        {
            if (charge == MaxCharge)
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
            if (charge >= MaxCharge) ScreenShake.ShakeScreen(6, 60);
            SoundEngine.PlaySound(Item.UseSound, player.Center);
            return base.UseItem(player);
        }
    }
}