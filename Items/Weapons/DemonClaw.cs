using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace SagesMania.Items.Weapons
{
    public class DemonClaw : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Damage increases throughout progression\n" +
				"''Come now! Let's play!''");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.useTime = 8;
            item.useAnimation = 8;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.damage = 28;
            item.melee = true;
            item.crit = 16;
            item.knockBack = 6;
            item.value = Item.sellPrice(gold: 5);
            item.rare = ItemRarityID.Blue;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
			item.expert = true;
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