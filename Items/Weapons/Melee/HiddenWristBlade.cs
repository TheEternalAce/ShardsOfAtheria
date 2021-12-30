using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using ShardsOfAtheria.Projectiles.Weapon;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
    public class HiddenWristBlade : ModItem
    {
        private int firstStrike = 0;
        private bool holding;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Holding this and not attacking builds up First Strike\n" +
                "First strike multiplies base weapon damage by 40");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.damage = 26;
            Item.DamageType = DamageClass.Melee;
            Item.crit = 16;
            Item.knockBack = 6;
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = false;
			Item.expert = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<HiddenWristBladeProj>();
            Item.shootSpeed = 2.1f;
		}

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
			target.AddBuff(ModContent.BuffType<Penetration>(), 10 * 60);
        }

        public override void HoldItem(Player player)
        {
            firstStrike++;
            if (firstStrike == 600) { 
                CombatText.NewText(player.getRect(), Color.Yellow, "First Strike Ready");
                Item.damage = 1040;
            }
            if (firstStrike >= 601)
                firstStrike = 601;
            holding = true;
        }

        public override void UpdateInventory(Player player)
        {
            if (!holding)
                firstStrike = 0;
            if (firstStrike < 600)
                Item.damage = 26;
        }

        public override bool? UseItem(Player player)
        {
            firstStrike = 0;
            return base.UseItem(player);
        }
    }
}