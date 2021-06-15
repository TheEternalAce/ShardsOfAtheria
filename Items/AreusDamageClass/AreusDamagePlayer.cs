using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SagesMania.Items.AreusDamageClass
{
	// This class stores necessary player info for our custom damage class, such as damage multipliers, additions to knockback and crit, and our custom resource that governs the usage of the weapons of this damage class.
	public class AreusDamagePlayer : ModPlayer
	{
		public static AreusDamagePlayer ModPlayer(Player player)
		{
			return player.GetModPlayer<AreusDamagePlayer>();
		}

		// Vanilla only really has damage multipliers in code
		// And crit and knockback is usually just added to
		// As a modder, you could make separate variables for multipliers and simple addition bonuses
		public float areusDamageAdd;
		public float areusDamageMult = 1f;
		public float areusKnockback;
		public int areusCrit;

		// Here we include a custom resource, similar to mana or health.
		// Creating some variables to define the current value of our example resource as well as the current maximum value. We also include a temporary max value, as well as some variables to handle the natural regeneration of this resource.
		public int areusResourceCurrent;
		public const int DefaultAreusResourceMax = 100;
		public int areusResourceMax;
		public int areusResourceMax2;
		public float areusResourceRegenRate;
		internal int areusResourceRegenTimer = 0;
		public static readonly Color HealSreusResource = new Color(100, 150, 200); // We can use this for CombatText, if you create an item that replenishes exampleResourceCurrent.

		/*
		In order to make the Example Resource example straightforward, several things have been left out that would be needed for a fully functional resource similar to mana and health. 
		Here are additional things you might need to implement if you intend to make a custom resource:
		- Multiplayer Syncing: The current example doesn't require MP code, but pretty much any additional functionality will require this. ModPlayer.SendClientChanges and clientClone will be necessary, as well as SyncPlayer if you allow the user to increase exampleResourceMax.
		- Save/Load and increased max resource: You'll need to implement Save/Load to remember increases to your exampleResourceMax cap.
		- Resouce replenishment item: Use GlobalNPC.NPCLoot to drop the item. ModItem.OnPickup and ModItem.ItemSpace will allow it to behave like Mana Star or Heart. Use code similar to Player.HealEffect to spawn (and sync) a colored number suitable to your resource.
		*/

		public override void Initialize()
		{
			areusResourceMax = DefaultAreusResourceMax;
		}

		public override void ResetEffects()
		{
			ResetVariables();
		}

		public override void UpdateDead()
		{
			ResetVariables();
		}

		private void ResetVariables()
		{
			areusDamageAdd = 0f;
			areusDamageMult = 1f;
			areusKnockback = 0f;
			areusCrit = 0;
			areusResourceRegenRate = 1f;
			areusResourceMax2 = areusResourceMax;
		}
	}
}