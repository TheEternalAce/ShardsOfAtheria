using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.SlayerItems;
using Terraria;
using ShardsOfAtheria.Items.SlayerItems.SoulCrystals;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Players;
using System;

namespace ShardsOfAtheria.Projectiles.Tools
{
    public class ExtractingSoul : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Extracting Dagger");
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

            Projectile.velocity = Vector2.Normalize(owner.Center - Projectile.Center) * 10;
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
            int soulCrystal = 0;
            if (Main.myPlayer == Projectile.owner)
            {
                SlayerPlayer slayer = Main.player[Projectile.owner].GetModPlayer<SlayerPlayer>();
                if (slayer.selectedSoul == SelectedSoul.King)
                {
                    soulCrystal = ModContent.ItemType<KingSoulCrystal>();
                    slayer.soulCrystals.Remove(soulCrystal);
                }
                if (slayer.selectedSoul == SelectedSoul.Eye)
                {
                    soulCrystal = ModContent.ItemType<EyeSoulCrystal>();
                    slayer.soulCrystals.Remove(soulCrystal);
                }
                if (slayer.selectedSoul == SelectedSoul.Brain)
                {
                    soulCrystal = ModContent.ItemType<BrainSoulCrystal>();
                    slayer.soulCrystals.Remove(soulCrystal);
                }
                if (slayer.selectedSoul == SelectedSoul.Eater)
                {
                    soulCrystal = ModContent.ItemType<EaterSoulCrystal>();
                    slayer.soulCrystals.Remove(soulCrystal);
                }
                if (slayer.selectedSoul == SelectedSoul.Valkyrie)
                {
                    soulCrystal = ModContent.ItemType<ValkyrieSoulCrystal>();
                    slayer.soulCrystals.Remove(soulCrystal);
                }
                if (slayer.selectedSoul == SelectedSoul.Bee)
                {
                    soulCrystal = ModContent.ItemType<BeeSoulCrystal>();
                    slayer.soulCrystals.Remove(soulCrystal);
                }
                if (slayer.selectedSoul == SelectedSoul.Skull)
                {
                    soulCrystal = ModContent.ItemType<SkullSoulCrystal>();
                    slayer.soulCrystals.Remove(soulCrystal);
                }
                if (slayer.selectedSoul == SelectedSoul.Deerclops)
                {
                    soulCrystal = ModContent.ItemType<DeerclopsSoulCrystal>();
                    slayer.soulCrystals.Remove(soulCrystal);
                }
                if (slayer.selectedSoul == SelectedSoul.Wall)
                {
                    soulCrystal = ModContent.ItemType<WallSoulCrystal>();
                    slayer.soulCrystals.Remove(soulCrystal);
                }
                if (slayer.selectedSoul == SelectedSoul.Queen)
                {
                    soulCrystal = ModContent.ItemType<QueenSoulCrystal>();
                    slayer.soulCrystals.Remove(soulCrystal);
                }
                if (slayer.selectedSoul == SelectedSoul.Destroyer)
                {
                    soulCrystal = ModContent.ItemType<DestroyerSoulCrystal>();
                    slayer.soulCrystals.Remove(soulCrystal);
                }
                if (slayer.selectedSoul == SelectedSoul.Twins)
                {
                    soulCrystal = ModContent.ItemType<TwinsSoulCrystal>();
                    slayer.soulCrystals.Remove(soulCrystal);
                }
                if (slayer.selectedSoul == SelectedSoul.Prime)
                {
                    soulCrystal = ModContent.ItemType<PrimeSoulCrystal>();
                    slayer.soulCrystals.Remove(soulCrystal);
                }
                if (slayer.selectedSoul == SelectedSoul.Plant)
                {
                    soulCrystal = ModContent.ItemType<PlantSoulCrystal>();
                    slayer.soulCrystals.Remove(soulCrystal);
                }
                if (slayer.selectedSoul == SelectedSoul.Golem)
                {
                    soulCrystal = ModContent.ItemType<GolemSoulCrystal>();
                    slayer.soulCrystals.Remove(soulCrystal);
                }
                if (slayer.selectedSoul == SelectedSoul.Duke)
                {
                    soulCrystal = ModContent.ItemType<DukeSoulCrystal>();
                    slayer.soulCrystals.Remove(soulCrystal);
                }
                if (slayer.selectedSoul == SelectedSoul.Empress)
                {
                    soulCrystal = ModContent.ItemType<EmpressSoulCrystal>();
                    slayer.soulCrystals.Remove(soulCrystal);
                }
                if (slayer.selectedSoul == SelectedSoul.Lunatic)
                {
                    soulCrystal = ModContent.ItemType<LunaticSoulCrystal>();
                    slayer.soulCrystals.Remove(soulCrystal);
                }
                if (slayer.selectedSoul == SelectedSoul.Lord)
                {
                    soulCrystal = ModContent.ItemType<LordSoulCrystal>();
                    slayer.soulCrystals.Remove(soulCrystal);
                }
                if (slayer.selectedSoul == SelectedSoul.Senterrra)
                {
                    //soulCrystal = ModContent.ItemType<SenterrraSoulCrystal>();
                    slayer.soulCrystals.Remove(soulCrystal);
                }
                if (slayer.selectedSoul == SelectedSoul.Genesis)
                {
                    //soulCrystal = ModContent.ItemType<GenesisSoulCrystal>();
                    slayer.soulCrystals.Remove(soulCrystal);
                }
                if (slayer.selectedSoul == SelectedSoul.Death)
                {
                    //soulCrystal = ModContent.ItemType<DeathSoulCrystal>();
                    slayer.soulCrystals.Remove(soulCrystal);
                }

                int newItem = Item.NewItem(Projectile.GetSource_DropAsItem(), Projectile.Hitbox, soulCrystal);
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
