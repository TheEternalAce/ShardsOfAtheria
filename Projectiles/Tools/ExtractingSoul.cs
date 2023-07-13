using BattleNetworkElements.Utilities;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Players;
using ShardsOfAtheria.Utilities;
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
            Projectile.AddFire();
            Projectile.AddElec();
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            Projectile.hostile = true;
            Projectile.damage = 100;
            Projectile.timeLeft = 120;

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

        public override bool CanHitPlayer(Player target)
        {
            if (target.whoAmI == Projectile.owner)
            {
                return true;
            }
            return base.CanHitPlayer(target);
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

                for (int i = 0; i < ModLoader.Mods.Length; i++)
                {
                    Mod mod = ModLoader.Mods[i];
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
                }

                SoA.Log("New Soul Crystal list: ", slayer.soulCrystalNames);
            }
        }
    }
}
