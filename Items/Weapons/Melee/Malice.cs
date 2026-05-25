using ShardsOfAtheria.Buffs.Sinner;
using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Projectiles.Melee.MaliceProjectiles;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class Malice : SinfulItem
    {
        int hateCounter = 0;
        int hateFallOffTimer = 0;
        const int HateFallOffTimerMax = 60;

        public override int RequiredSin => CardinalSoulID.Wrath;

        // Base damages: 50, 130, 230
        public override int[] DamageSpread => [0, 80, 100];

        public override void SetStaticDefaults()
        {
            Item.AddDamageType(11);
            Item.AddElement(0);
            Item.AddElement(2);
            Item.AddRedemptionElement(2);
            Item.AddRedemptionElement(7);
        }

        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 50;

            Item.damage = 50;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6;
            Item.crit = 6;

            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
            Item.channel = true;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 1f;
            Item.rare = ItemDefaults.RarityEarlyHardmode;
            Item.value = Item.sellPrice(0, 1, 25);
            Item.shoot = ModContent.ProjectileType<MaliceSpear>();
        }

        public override bool MeleePrefix()
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] == 0 && base.CanUseItem(player);
        }

        public override bool? UseItem(Player player)
        {
            if (player.Shards().InCombat) hateCounter++;
            if (hateCounter >= 30)
                player.AddBuff<Hatred>(300);
            return null;
        }

        public override void UpdateInventory(Player player)
        {
            if (!player.ItemAnimationActive && hateCounter > 0 && !player.HasBuff<Hatred>())
            {
                hateFallOffTimer++;
                if (hateFallOffTimer >= HateFallOffTimerMax)
                    hateCounter--;
            }
            else hateFallOffTimer = 0;
        }
    }
}