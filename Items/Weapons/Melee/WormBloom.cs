using Microsoft.Xna.Framework;
using MMZeroElements;
using ShardsOfAtheria.Projectiles.Weapon.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Melee
{
	public class WormBloom : ModItem
	{
		public override void SetStaticDefaults()
		{
			SacrificeTotal = 1;
			WeaponElements.Ice.Add(Type);
		}

		public override void SetDefaults()
		{
			Item.width = 42;
			Item.height = 48;

			Item.damage = 33;
			Item.DamageType = DamageClass.Melee;
			Item.knockBack = 5;
			Item.crit = 5;

			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.UseSound = SoundID.Item1;

			Item.rare = ItemRarityID.Expert;
			Item.expert = true;
			Item.value = Item.sellPrice(0, 2, 25);
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			// Add the Onfire buff to the NPC for 1 second when the weapon hits an NPC
			// 60 frames = 1 second
			target.AddBuff(BuffID.Ichor, 600);
			target.AddBuff(BuffID.Weak, 600);
			target.AddBuff(BuffID.Chilled, 600);

			Projectile.NewProjectile(Item.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<CorruptRose>(), damage, knockback, player.whoAmI);
		}
	}
}