using ShardsOfAtheria.Common.Items;
using ShardsOfAtheria.Projectiles.Melee;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class AreusStrikeChain : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.AddAreus();
            Item.AddDamageType(5);
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 42;

            Item.damage = 160;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 6;
            Item.crit = 4;

            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.shootSpeed = 26;
            Item.rare = ItemDefaults.RarityLunarPillars;
            Item.value = 200000;
            Item.shoot = ModContent.ProjectileType<StrikeChain>();

            SoA.TryDungeonCall("addFinesseWeapon", Type);
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] == 0;
        }
    }
}