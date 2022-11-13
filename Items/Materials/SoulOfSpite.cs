using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Materials
{
    public class SoulOfSpite : ModItem
    {
        public override void SetStaticDefaults()
        {
            // ticksperframe, frameCount
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 4));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true;
            ItemID.Sets.ItemIconPulse[Item.type] = true;
            ItemID.Sets.ItemNoGravity[Item.type] = true;

            SacrificeTotal = 25;
        }

        public override void SetDefaults()
        {
            Item refItem = new();
            refItem.SetDefaults(ItemID.SoulofNight);
            Item.width = refItem.width;
            Item.height = refItem.height;
            Item.maxStack = 9999;

            Item.rare = ItemRarityID.Blue;
            Item.value = 1000;
        }

        // The following 2 methods are purely to show off these 2 hooks. Don't use them in your own code.
        /*
		public override void GrabRange(Player player, ref int grabRange)

		{
			grabRange *= 3;
		}

		public override bool GrabStyle(Player player)
		{
			Vector2 vectorItemToPlayer = player.Center - item.Center;
			Vector2 movement = -vectorItemToPlayer.SafeNormalize(default(Vector2)) * 0.1f;
			item.velocity = item.velocity + movement;
			item.velocity = Collision.TileCollision(item.position, item.velocity, Item.width, Item.height);
			return true;
		}
		*/

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.DarkRed.ToVector3() * 0.55f * Main.essScale);
        }
    }
}