using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.SlayerItems;
using Terraria;
using ShardsOfAtheria.Items.SlayerItems.SoulCrystals;
using Terraria.ID;
using Terraria.ModLoader;

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
                Projectile.rotation = MathHelper.ToRadians(225);
            else Projectile.rotation = MathHelper.ToRadians(45);
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
            Player player = Main.LocalPlayer;
            player.GetModPlayer<SlayerPlayer>().soulCrystals--;
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.King)
            {
                player.GetModPlayer<SlayerPlayer>().KingSoul = false;
                Item.NewItem(player.GetSource_FromThis(), player.getRect(), ModContent.ItemType<KingSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Eye)
            {
                player.GetModPlayer<SlayerPlayer>().EyeSoul = false;
                Item.NewItem(player.GetSource_FromThis(), player.getRect(), ModContent.ItemType<EyeSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Brain)
            {
                player.GetModPlayer<SlayerPlayer>().BrainSoul = false;
                Item.NewItem(player.GetSource_FromThis(), player.getRect(), ModContent.ItemType<BrainSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Eater)
            {
                player.GetModPlayer<SlayerPlayer>().EaterSoul = false;
                Item.NewItem(player.GetSource_FromThis(), player.getRect(), ModContent.ItemType<EaterSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Valkyrie)
            {
                player.GetModPlayer<SlayerPlayer>().ValkyrieSoul = false;
                Item.NewItem(player.GetSource_FromThis(), player.getRect(), ModContent.ItemType<ValkyrieSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Bee)
            {
                player.GetModPlayer<SlayerPlayer>().BeeSoul = false;
                Item.NewItem(player.GetSource_FromThis(), player.getRect(), ModContent.ItemType<BeeSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Skull)
            {
                player.GetModPlayer<SlayerPlayer>().SkullSoul = false;
                Item.NewItem(player.GetSource_FromThis(), player.getRect(), ModContent.ItemType<SkullSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Deerclops)
            {
                player.GetModPlayer<SlayerPlayer>().DeerclopsSoul = false;
                Item.NewItem(player.GetSource_FromThis(), player.getRect(), ModContent.ItemType<DeerclopsSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Wall)
            {
                player.GetModPlayer<SlayerPlayer>().WallSoul = false;
                Item.NewItem(player.GetSource_FromThis(), player.getRect(), ModContent.ItemType<WallSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Queen)
            {
                player.GetModPlayer<SlayerPlayer>().QueenSoul = false;
                Item.NewItem(player.GetSource_FromThis(), player.getRect(), ModContent.ItemType<QueenSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Destroyer)
            {
                player.GetModPlayer<SlayerPlayer>().DestroyerSoul = false;
                Item.NewItem(player.GetSource_FromThis(), player.getRect(), ModContent.ItemType<DestroyerSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Twins)
            {
                player.GetModPlayer<SlayerPlayer>().TwinSoul = false;
                Item.NewItem(player.GetSource_FromThis(), player.getRect(), ModContent.ItemType<TwinsSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Prime)
            {
                player.GetModPlayer<SlayerPlayer>().PrimeSoul = false;
                Item.NewItem(player.GetSource_FromThis(), player.getRect(), ModContent.ItemType<PrimeSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Plant)
            {
                player.GetModPlayer<SlayerPlayer>().PlantSoul = false;
                Item.NewItem(player.GetSource_FromThis(), player.getRect(), ModContent.ItemType<PlantSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Golem)
            {
                player.GetModPlayer<SlayerPlayer>().GolemSoul = false;
                Item.NewItem(player.GetSource_FromThis(), player.getRect(), ModContent.ItemType<GolemSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Duke)
            {
                player.GetModPlayer<SlayerPlayer>().DukeSoul = false;
                Item.NewItem(player.GetSource_FromThis(), player.getRect(), ModContent.ItemType<DukeSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Empress)
            {
                player.GetModPlayer<SlayerPlayer>().EmpressSoul = false;
                Item.NewItem(player.GetSource_FromThis(), player.getRect(), ModContent.ItemType<EmpressSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Lunatic)
            {
                player.GetModPlayer<SlayerPlayer>().LunaticSoul = false;
                Item.NewItem(player.GetSource_FromThis(), player.getRect(), ModContent.ItemType<LunaticSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Lord)
            {
                player.GetModPlayer<SlayerPlayer>().LordSoul = false;
                Item.NewItem(player.GetSource_FromThis(), player.getRect(), ModContent.ItemType<LordSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Senterrra)
            {
                player.GetModPlayer<SlayerPlayer>().LandSoul = false;
                //Item.NewItem(player.GetSource_FromThis(), player.getRect(), ModContent.ItemType<SenterrraSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Genesis)
            {
                player.GetModPlayer<SlayerPlayer>().TimeSoul = false;
                //Item.NewItem(player.GetSource_FromThis(), player.getRect(), ModContent.ItemType<GenesisSoulCrystal>());
            }
            if (player.GetModPlayer<SlayerPlayer>().selectedSoul == SelectedSoul.Death)
            {
                player.GetModPlayer<SlayerPlayer>().DeathSoul = false;
                //Item.NewItem(player.GetSource_FromThis(), player.getRect(), ModContent.ItemType<DeathSoulCrystal>());
            }
        }
    }
}
