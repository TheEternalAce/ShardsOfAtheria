using Microsoft.Xna.Framework;
using MMZeroElements;
using ShardsOfAtheria.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static ShardsOfAtheria.Utilities.Entry;

namespace ShardsOfAtheria.Projectiles.Tools
{
    public class ExtractingSoul : ModProjectile
    {
        int pos = 48;

        public override void SetStaticDefaults()
        {
            ProjectileElements.Fire.Add(Type);
            ProjectileElements.Electric.Add(Type);
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.hostile = true;
            Projectile.damage = 100;
            Projectile.timeLeft = 300;

            DrawOffsetX = -21;
            DrawOriginOffsetX = 15;
        }

        public override void AI()
        {
            Player owner = Main.LocalPlayer;

            int newDirection = Projectile.Center.X > owner.Center.X ? 1 : -1;
            owner.ChangeDir(newDirection);
            Projectile.direction = newDirection;

            pos -= 5;
            Projectile.Center = owner.Center + new Vector2(pos * owner.direction, 0);
            if (owner.direction == 1)
                Projectile.rotation = MathHelper.ToRadians(245);
            else Projectile.rotation = MathHelper.ToRadians(65);
        }

        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            Projectile.Kill();
            ExtractSoul();
            for (int i = 0; i < 20; i++)
            {
                if (Main.rand.NextBool(3))
                    Dust.NewDustDirect(target.Center, 4, 4, DustID.SandstormInABottle, .2f, .2f, 0, Color.Purple, 1.2f);
                Lighting.AddLight(target.Center, TorchID.Purple);
            }
        }

        public void ExtractSoul()
        {
            if (Main.myPlayer == Projectile.owner)
            {
                SlayerPlayer slayer = Main.player[Projectile.owner].GetModPlayer<SlayerPlayer>();
                PageEntry entry = entries[(int)Projectile.ai[1]];

                slayer.soulCrystals.Remove(entry.crystalItem);

                int newItem = Item.NewItem(Projectile.GetSource_DropAsItem(), Projectile.Hitbox, entry.crystalItem);
                Main.item[newItem].noGrabDelay = 0; // Set the new item to be able to be picked up instantly

                // Here we need to make sure the item is synced in multiplayer games.
                if (Main.netMode == NetmodeID.MultiplayerClient && newItem >= 0)
                {
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, newItem, 1f);
                }
            }
        }
    }
}
