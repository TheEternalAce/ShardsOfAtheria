using Microsoft.Xna.Framework;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static ShardsOfAtheria.Utilities.Entry;

namespace ShardsOfAtheria.Projectiles.Tools
{
    public class ExtractingSoul : ModProjectile
    {
        int pos = 48;

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft = 60;

            DrawOffsetX = -21;
            DrawOriginOffsetX = 15;
        }

        public override void AI()
        {
            Player player = Projectile.GetPlayerOwner();

            int newDirection = Projectile.Center.X > player.Center.X ? 1 : -1;
            player.ChangeDir(newDirection);
            Projectile.direction = newDirection;
            Projectile.spriteDirection = -newDirection;
            Projectile.SetVisualOffsets(new Vector2(28, 60));

            pos -= 5;
            Projectile.Center = player.Center + new Vector2(pos * player.direction, 0);
            if (player.direction == 1)
                Projectile.rotation = MathHelper.ToRadians(290);
            else Projectile.rotation = MathHelper.ToRadians(70);

            if (Projectile.Colliding(Projectile.Hitbox, player.Hitbox))
            {
                string pronoun = player.Male ? "his" : "her";
                string customDeath = player.name + " removed " + pronoun + " own soul";
                Player.HurtInfo info = new()
                {
                    Damage = 100,
                    Knockback = 0f,
                    Dodgeable = false,
                    DamageSource = PlayerDeathReason.ByCustomReason(customDeath)
                };
                player.Hurt(info);
                OnHitPlayer(player, info);
            }
        }

        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hit)
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
                SlayerPlayer slayer = Main.player[Projectile.owner].Slayer();
                PageEntry entry = entries[(int)Projectile.ai[1]];

                slayer.soulCrystalNames.Remove(entry.crystalItem);

                Mod mod = SoA.Instance;
                if (mod.TryFind(entry.crystalItem, out ModItem modItem))
                {
                    int newItem = Item.NewItem(Projectile.GetSource_DropAsItem(), Projectile.Hitbox, modItem.Type);
                    Main.item[newItem].noGrabDelay = 0; // Set the new item to be able to be picked up instantly

                    // Here we need to make sure the item is synced in multiplayer games.
                    if (Main.netMode == NetmodeID.MultiplayerClient && newItem >= 0)
                    {
                        NetMessage.SendData(MessageID.SyncItem, -1, -1, null, newItem, 1f);
                    }
                }

                SoA.LogInfo(slayer.soulCrystalNames, "New Soul Crystal list: ");
            }
        }
    }
}
