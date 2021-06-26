using Microsoft.Xna.Framework;
using SagesMania.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace SagesMania.Items.Weapons
{
    public class HiddenBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Damage increases throughout progression\n" +
				"''Surprise! You're dead!''");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.Stabbing;
            item.damage = 26;
            item.melee = true;
            item.crit = 16;
            item.knockBack = 6;
            item.value = Item.sellPrice(gold: 10);
            item.rare = ItemRarityID.Blue;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
		}

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
			target.AddBuff(ModContent.BuffType<Penetration>(), 10 * 60);
        }

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
		{
			if (Main.hardMode)
			{
				add += .1f;
			}
			if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
			{
				add += .15f;
			}
			if (NPC.downedPlantBoss)
			{
				add += .15f;
			}
			if (NPC.downedGolemBoss)
			{
				add += .2f;
			}
			if (NPC.downedAncientCultist)
			{
				add += .5f;
			}
			if (NPC.downedMoonlord)
			{
				add += 1f;
			}
		}
	}
}