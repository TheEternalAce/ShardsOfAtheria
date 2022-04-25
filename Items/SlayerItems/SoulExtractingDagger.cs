using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Projectiles.Tools;
using Terraria.Audio;
using ShardsOfAtheria.Items.SlayerItems.SoulCrystals;
using System.Collections.Generic;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace ShardsOfAtheria.Items.SlayerItems
{
	public class SoulExtractingDagger : SlayerItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Right click to cycle between absorbed Soul Crystals\n" +
                "Use to extract the currently selected Soul Crystal");
		}

		public override void SetDefaults()
		{
			Item.width = 36;
			Item.height = 40;
			Item.rare = ItemRarityID.Yellow;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.shoot = ModContent.ProjectileType<ExtractingSoul>();
			Item.shootSpeed = 16f;
			Item.noUseGraphic = true;
		}

		public override bool ConsumeItem(Player player)
		{
			return false;
		}

        public override bool CanRightClick()
        {
			return true;
        }

        public override void RightClick(Player player)
        {
			SelectSoul(player);
			if (!player.GetModPlayer<SlayerPlayer>().anySoulCrystals)
				CombatText.NewText(player.getRect(), Color.Blue, "You have no Soul Crystals to extract");
		}

        public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			Player player = Main.LocalPlayer;
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.None)
				tooltips.Add(new TooltipLine(Mod, "SelectedSoul", "Selected: [c/7E7E7E:None]"));

			//Selected Soul
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.King)
				tooltips.Add(new TooltipLine(Mod, "SelectedSoul", "Selected: [c/0000FF:King Slime]"));
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Eye)
				tooltips.Add(new TooltipLine(Mod, "SelectedSoul", "Selected: [c/FF0000:Eye of Cthulhu]"));
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Brain)
				tooltips.Add(new TooltipLine(Mod, "SelectedSoul", "Selected: [c/FF00A0:Brain of Cthulhu]"));
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Eater)
				tooltips.Add(new TooltipLine(Mod, "SelectedSoul", "Selected: [c/806080:Eater of Worlds]"));
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Valkyrie)
				tooltips.Add(new TooltipLine(Mod, "SelectedSoul", "Selected: [c/6464FF:Nova Stellar, the Lightning Valkyrie]"));
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Bee)
				tooltips.Add(new TooltipLine(Mod, "SelectedSoul", "Selected: [c/FFFF00:Queen Bee]"));
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Skull)
				tooltips.Add(new TooltipLine(Mod, "SelectedSoul", "Selected: [c/82825A:Skeletron]"));
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Wall)
				tooltips.Add(new TooltipLine(Mod, "SelectedSoul", "Selected:[c/825A5A:Wall of Flesh]"));
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Destroyer)
				tooltips.Add(new TooltipLine(Mod, "SelectedSoul", "Selected: [c/828296:The Destroyer]"));
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Twins)
				tooltips.Add(new TooltipLine(Mod, "SelectedSoul", "Selected: [c/828296:The Twins]"));
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Prime)
				tooltips.Add(new TooltipLine(Mod, "SelectedSoul", "Selected: [c/828296:Skeletron Prime]"));
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Plant)
				tooltips.Add(new TooltipLine(Mod, "SelectedSoul", "Selected: [c/FF5050:Plantera]"));
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Golem)
				tooltips.Add(new TooltipLine(Mod, "SelectedSoul", "Selected: [c/8C3C00:Golem]"));
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Duke)
				tooltips.Add(new TooltipLine(Mod, "SelectedSoul", "Selected: [c/00FF90:Duke Fishron]"));
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Empress)
				tooltips.Add(new TooltipLine(Mod, "SelectedSoul", "Selected: [c/FF80C0:Empress of Light]"));
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Lord)
				tooltips.Add(new TooltipLine(Mod, "SelectedSoul", "Selected: [c/C0F4E7:Moon Lord]"));
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Senterrra)
				tooltips.Add(new TooltipLine(Mod, "SelectedSoul", "Selected: [c/00FF00:Senterra, the Atherial Land]"));
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Genesis)
				tooltips.Add(new TooltipLine(Mod, "SelectedSoul", "Selected: [c/900090:Genesis, Atherial Time]"));
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Death)
				tooltips.Add(new TooltipLine(Mod, "SelectedSoul", "Selected: [c/303030:Elizabeth Norman, Death]"));

			//Available Souls
			if (!player.GetModPlayer<SlayerPlayer>().anySoulCrystals)
				tooltips.Add(new TooltipLine(Mod, "AvailableSoul", "[c/7E7E7E:No Souls Available]"));
			if (player.GetModPlayer<SlayerPlayer>().KingSoul == SoulCrystalStatus.Absorbed)
				tooltips.Add(new TooltipLine(Mod, "AvailableSoul", "[c/0000FF:King Slime]"));
			if (player.GetModPlayer<SlayerPlayer>().EyeSoul == SoulCrystalStatus.Absorbed)
				tooltips.Add(new TooltipLine(Mod, "AvailableSoul", "[c/FF0000:Eye of Cthulhu]"));
			if (player.GetModPlayer<SlayerPlayer>().BrainSoul == SoulCrystalStatus.Absorbed)
				tooltips.Add(new TooltipLine(Mod, "AvailableSoul", "[c/FF00A0:Brain of Cthulhu]"));
			if (player.GetModPlayer<SlayerPlayer>().EaterSoul == SoulCrystalStatus.Absorbed)
				tooltips.Add(new TooltipLine(Mod, "AvailableSoul", "[c/806080:Eater of Worlds]"));
			if (player.GetModPlayer<SlayerPlayer>().ValkyrieSoul == SoulCrystalStatus.Absorbed)
				tooltips.Add(new TooltipLine(Mod, "AvailableSoul", "[c/6464FF:Nova Stellar, the Lightning Valkyrie]"));
			if (player.GetModPlayer<SlayerPlayer>().BeeSoul == SoulCrystalStatus.Absorbed)
				tooltips.Add(new TooltipLine(Mod, "AvailableSoul", "[c/FFFF00:Queen Bee]"));
			if (player.GetModPlayer<SlayerPlayer>().SkullSoul == SoulCrystalStatus.Absorbed)
				tooltips.Add(new TooltipLine(Mod, "AvailableSoul", "[c/82825A:Skeletron]"));
			if (player.GetModPlayer<SlayerPlayer>().WallSoul == SoulCrystalStatus.Absorbed)
				tooltips.Add(new TooltipLine(Mod, "AvailableSoul", "[c/825A5A:Wall of Flesh]"));
			if (player.GetModPlayer<SlayerPlayer>().DestroyerSoul == SoulCrystalStatus.Absorbed)
				tooltips.Add(new TooltipLine(Mod, "AvailableSoul", "[c/828296:The Destroyer]"));
			if (player.GetModPlayer<SlayerPlayer>().TwinSoul == SoulCrystalStatus.Absorbed)
				tooltips.Add(new TooltipLine(Mod, "AvailableSoul", "[c/828296:The Twins]"));
			if (player.GetModPlayer<SlayerPlayer>().PrimeSoul == SoulCrystalStatus.Absorbed)
				tooltips.Add(new TooltipLine(Mod, "AvailableSoul", "[c/828296:Skeletron Prime]"));
			if (player.GetModPlayer<SlayerPlayer>().PlantSoul == SoulCrystalStatus.Absorbed)
				tooltips.Add(new TooltipLine(Mod, "AvailableSoul", "[c/FF5050:Plantera]"));
			if (player.GetModPlayer<SlayerPlayer>().GolemSoul == SoulCrystalStatus.Absorbed)
				tooltips.Add(new TooltipLine(Mod, "AvailableSoul", "[c/8C3C00:Golem]"));
			if (player.GetModPlayer<SlayerPlayer>().DukeSoul == SoulCrystalStatus.Absorbed)
				tooltips.Add(new TooltipLine(Mod, "AvailableSoul", "[c/00FF90:Duke Fishron]"));
			if (player.GetModPlayer<SlayerPlayer>().EmpressSoul == SoulCrystalStatus.Absorbed)
				tooltips.Add(new TooltipLine(Mod, "AvailableSoul", "[c/FF80C0:Empress of Light]"));
			if (player.GetModPlayer<SlayerPlayer>().LordSoul == SoulCrystalStatus.Absorbed)
				tooltips.Add(new TooltipLine(Mod, "AvailableSoul", "[c/C0F4E7:Moon Lord]"));
			if (player.GetModPlayer<SlayerPlayer>().LandSoul == SoulCrystalStatus.Absorbed)
				tooltips.Add(new TooltipLine(Mod, "AvailableSoul", "[c/00FF00:Senterra, the Atherial Land]"));
			if (player.GetModPlayer<SlayerPlayer>().TimeSoul == SoulCrystalStatus.Absorbed)
				tooltips.Add(new TooltipLine(Mod, "AvailableSoul", "[c/900090:Genesis, Atherial Time]"));
			if (player.GetModPlayer<SlayerPlayer>().DeathSoul == SoulCrystalStatus.Absorbed)
				tooltips.Add(new TooltipLine(Mod, "AvailableSoul", "[c/303030:Elizabeth Norman, Death]"));
		}

		public override bool CanUseItem(Player player)
        {
			return player.GetModPlayer<SlayerPlayer>().selectedSoul != SelectedSoul.None && player.GetModPlayer<SlayerPlayer>().anySoulCrystals;
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			Vector2 toPlayer;
			Vector2 spawnLocation;
			if (player.direction == 1)
				spawnLocation = player.Center + new Vector2(48, 0);
			else spawnLocation = player.Center + new Vector2(-48, 0);
			if (player.direction == 1)
				toPlayer = player.Center + new Vector2(-48, 0);
			else toPlayer = player.Center + new Vector2(48, 0);
			Projectile.NewProjectile(source, spawnLocation, toPlayer, type, 0, 0, player.whoAmI);
			return false;
		}

        public override void UpdateInventory(Player player)
		{
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.King && player.GetModPlayer<SlayerPlayer>().KingSoul != SoulCrystalStatus.Absorbed)
				player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Eye;
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Eye && player.GetModPlayer<SlayerPlayer>().EyeSoul != SoulCrystalStatus.Absorbed)
				player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Brain;
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Brain && player.GetModPlayer<SlayerPlayer>().BrainSoul != SoulCrystalStatus.Absorbed)
				player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Eater;
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Eater && player.GetModPlayer<SlayerPlayer>().EaterSoul != SoulCrystalStatus.Absorbed)
				player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Valkyrie;
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Valkyrie && player.GetModPlayer<SlayerPlayer>().ValkyrieSoul != SoulCrystalStatus.Absorbed)
				player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Bee;
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Bee && player.GetModPlayer<SlayerPlayer>().BeeSoul != SoulCrystalStatus.Absorbed)
				player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Skull;
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Skull && player.GetModPlayer<SlayerPlayer>().SkullSoul != SoulCrystalStatus.Absorbed)
				player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Wall;
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Wall && player.GetModPlayer<SlayerPlayer>().WallSoul != SoulCrystalStatus.Absorbed)
				player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Destroyer;
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Destroyer && player.GetModPlayer<SlayerPlayer>().DestroyerSoul != SoulCrystalStatus.Absorbed)
				player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Twins;
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Twins && player.GetModPlayer<SlayerPlayer>().TwinSoul != SoulCrystalStatus.Absorbed)
				player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Prime;
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Prime && player.GetModPlayer<SlayerPlayer>().PrimeSoul != SoulCrystalStatus.Absorbed)
				player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Plant;
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Plant && player.GetModPlayer<SlayerPlayer>().PlantSoul != SoulCrystalStatus.Absorbed)
				player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Golem;
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Golem && player.GetModPlayer<SlayerPlayer>().GolemSoul != SoulCrystalStatus.Absorbed)
				player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Duke;
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Duke && player.GetModPlayer<SlayerPlayer>().DukeSoul != SoulCrystalStatus.Absorbed)
				player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Empress;
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Empress && player.GetModPlayer<SlayerPlayer>().EmpressSoul != SoulCrystalStatus.Absorbed)
				player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Lord;
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Lord && player.GetModPlayer<SlayerPlayer>().LordSoul != SoulCrystalStatus.Absorbed)
				player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Senterrra;
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Senterrra && player.GetModPlayer<SlayerPlayer>().LandSoul != SoulCrystalStatus.Absorbed)
				player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Genesis;
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Genesis && player.GetModPlayer<SlayerPlayer>().TimeSoul != SoulCrystalStatus.Absorbed)
				player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Death;
			if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Death && player.GetModPlayer<SlayerPlayer>().DeathSoul != SoulCrystalStatus.Absorbed)
				player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.Eye;

			if (!player.GetModPlayer<SlayerPlayer>().anySoulCrystals)
				player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.None;
			else if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.None)
				player.GetModPlayer<SlayerPlayer>().selectedSoul = SelectedSoul.King;
		}

        public void SelectSoul(Player player)
        {
			player.GetModPlayer<SlayerPlayer>().selectedSoul++;
		}
	}

	public class SelectedSoul
    {
		public const int None = 0;
		public const int King = 1;
		public const int Eye = 2;
		public const int Brain = 3;
		public const int Eater = 4;
		public const int Valkyrie = 5;
		public const int Bee = 6;
		public const int Skull = 7;
		public const int Deerclops = 8;
		public const int Wall = 9;
		public const int Queen = 10;
		public const int Destroyer = 11;
		public const int Twins = 12;
		public const int Prime = 13;
		public const int Plant = 14;
		public const int Golem = 15;
		public const int Duke = 16;
		public const int Empress = 17;
		public const int Lord = 18;
		public const int Senterrra = 19;
		public const int Genesis = 20;
		public const int Death = 21;
	}
}