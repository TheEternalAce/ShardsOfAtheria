using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Materials
{
    public class SoulOfTwilight : ModItem
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

        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            if (Main.dayTime && !Main.eclipse)
            {
                for (int j = 0; j < 10; j++)
                {
                    Dust.NewDust(Item.position, Item.width, Item.height, DustID.MagicMirror, Item.velocity.X, Item.velocity.Y, 150, default, 1.2f);
                }
                Item.active = false;
                Item.type = ItemID.None;
                Item.stack = 0;
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, Item.type);
                }
            }
            base.Update(ref gravity, ref maxFallSpeed);
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.WhiteSmoke.ToVector3() * 0.55f * Main.essScale);
        }
    }
}