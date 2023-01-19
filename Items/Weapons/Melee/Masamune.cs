using MMZeroElements;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
	public class Masamune : ModItem
	{
		public override void SetStaticDefaults()
		{
			SacrificeTotal = 1;
			WeaponElements.Metal.Add(Type);
		}

		public override void SetDefaults()
		{
			Item.width = 68;
			Item.height = 70;

			Item.damage = 500;
			Item.DamageType = DamageClass.Melee;
			Item.knockBack = 5;
			Item.crit = 50;
			Item.useTime = 40;
			Item.useAnimation = 40;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.UseSound = SoundID.Item1;
			Item.rare = ItemRarityID.Red;
			Item.value = Item.sellPrice(0, 20);
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			// Add the Onfire buff to the NPC for 1 second when the weapon hits an NPC
			// 60 frames = 1 second
			target.AddBuff(BuffID.Ichor, 600);
			target.AddBuff(BuffID.Weak, 600);
			target.AddBuff(BuffID.Chilled, 600);
		}
	}
}