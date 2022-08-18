using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.SlayerItems;
using Terraria;
using ShardsOfAtheria.Items.SlayerItems.SoulCrystals;
using Terraria.ID;
using Terraria.ModLoader;
using ShardsOfAtheria.Players;

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
                Player player = Main.player[Projectile.owner];
                if (player.GetModPlayer<SlayerPlayer>().soulCrystals > 0)
                {
                    player.GetModPlayer<SlayerPlayer>().soulCrystals--;
                }
                if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.King)
                {
                    player.GetModPlayer<SlayerPlayer>().KingSoul = false;
                    soulCrystal = ModContent.ItemType<KingSoulCrystal>();
                }
                if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Eye)
                {
                    player.GetModPlayer<SlayerPlayer>().EyeSoul = false;
                    soulCrystal = ModContent.ItemType<EyeSoulCrystal>();
                }
                if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Brain)
                {
                    player.GetModPlayer<SlayerPlayer>().BrainSoul = false;
                    soulCrystal = ModContent.ItemType<BrainSoulCrystal>();
                }
                if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Eater)
                {
                    player.GetModPlayer<SlayerPlayer>().EaterSoul = false;
                    soulCrystal = ModContent.ItemType<EaterSoulCrystal>();
                }
                if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Valkyrie)
                {
                    player.GetModPlayer<SlayerPlayer>().ValkyrieSoul = false;
                    soulCrystal = ModContent.ItemType<ValkyrieSoulCrystal>();
                }
                if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Bee)
                {
                    player.GetModPlayer<SlayerPlayer>().BeeSoul = false;
                    soulCrystal = ModContent.ItemType<BeeSoulCrystal>();
                }
                if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Skull)
                {
                    player.GetModPlayer<SlayerPlayer>().SkullSoul = false;
                    soulCrystal = ModContent.ItemType<SkullSoulCrystal>();
                }
                if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Deerclops)
                {
                    player.GetModPlayer<SlayerPlayer>().DeerclopsSoul = false;
                    soulCrystal = ModContent.ItemType<DeerclopsSoulCrystal>();
                }
                if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Wall)
                {
                    player.GetModPlayer<SlayerPlayer>().WallSoul = false;
                    soulCrystal = ModContent.ItemType<WallSoulCrystal>();
                }
                if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Queen)
                {
                    player.GetModPlayer<SlayerPlayer>().QueenSoul = false;
                    soulCrystal = ModContent.ItemType<QueenSoulCrystal>();
                }
                if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Destroyer)
                {
                    player.GetModPlayer<SlayerPlayer>().DestroyerSoul = false;
                    soulCrystal = ModContent.ItemType<DestroyerSoulCrystal>();
                }
                if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Twins)
                {
                    player.GetModPlayer<SlayerPlayer>().TwinSoul = false;
                    soulCrystal = ModContent.ItemType<TwinsSoulCrystal>();
                }
                if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Prime)
                {
                    player.GetModPlayer<SlayerPlayer>().PrimeSoul = false;
                    soulCrystal = ModContent.ItemType<PrimeSoulCrystal>();
                }
                if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Plant)
                {
                    player.GetModPlayer<SlayerPlayer>().PlantSoul = false;
                    soulCrystal = ModContent.ItemType<PlantSoulCrystal>();
                }
                if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Golem)
                {
                    player.GetModPlayer<SlayerPlayer>().GolemSoul = false;
                    soulCrystal = ModContent.ItemType<GolemSoulCrystal>();
                }
                if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Duke)
                {
                    player.GetModPlayer<SlayerPlayer>().DukeSoul = false;
                    soulCrystal = ModContent.ItemType<DukeSoulCrystal>();
                }
                if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Empress)
                {
                    player.GetModPlayer<SlayerPlayer>().EmpressSoul = false;
                    soulCrystal = ModContent.ItemType<EmpressSoulCrystal>();
                }
                if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Lunatic)
                {
                    player.GetModPlayer<SlayerPlayer>().LunaticSoul = false;
                    soulCrystal = ModContent.ItemType<LunaticSoulCrystal>();
                }
                if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Lord)
                {
                    player.GetModPlayer<SlayerPlayer>().LordSoul = false;
                    soulCrystal = ModContent.ItemType<LordSoulCrystal>();
                }
                if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Senterrra)
                {
                    player.GetModPlayer<SlayerPlayer>().LandSoul = false;
                    //soulCrystal = ModContent.ItemType<SenterrraSoulCrystal>();
                }
                if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Genesis)
                {
                    player.GetModPlayer<SlayerPlayer>().TimeSoul = false;
                    //soulCrystal = ModContent.ItemType<GenesisSoulCrystal>();
                }
                if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Death)
                {
                    player.GetModPlayer<SlayerPlayer>().DeathSoul = false;
                    //soulCrystal = ModContent.ItemType<DeathSoulCrystal>();
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
