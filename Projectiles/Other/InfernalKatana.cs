using Microsoft.Xna.Framework;
using ShardsOfAtheria.Items.Weapons.Melee;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
//using WebCom.Effects.ScreenShaking;

namespace ShardsOfAtheria.Projectiles.Other
{
    public class InfernalKatana : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10; // The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2; // The recording mode
            Projectile.AddElement(0);
            Projectile.AddRedemptionElement(2);
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;

            Projectile.aiStyle = 0;
            Projectile.tileCollide = false;
            Projectile.light = 1f;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 20;
        }

        public override void AI()
        {
            Projectile.SetVisualOffsets(new Vector2(48, 54));
            float rotation = MathHelper.PiOver4;
            Projectile.rotation = Projectile.velocity.ToRotation() + rotation;
            if (++Projectile.ai[1] >= 10f)
            {
                if (Projectile.ai[0] == 1f)
                {
                    Projectile.velocity *= 0.65f;
                }
            }

            if (Projectile.ai[0] != 1)
            {
                var player = Projectile.GetPlayerOwner();
                if (Projectile.Colliding(Projectile.Hitbox, player.Hitbox))
                {
                    string customDeath = player.name + "'s death was not supposed to happen";
                    Player.HurtInfo info = new()
                    {
                        Damage = 1,
                        Knockback = 0f,
                        Dodgeable = false,
                        DamageSource = PlayerDeathReason.ByCustomReason(customDeath)
                    };
                    player.Hurt(info);
                    OnHitPlayer(player, info);
                }
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            Projectile.ai[0] = 1;
            foreach (Item item in target.inventory)
            {
                if (item.type == ModContent.ItemType<AreusKatana>())
                {
                    item.TurnToAir();
                    int type = ModContent.ItemType<TheMourningStar>();
                    int newItem = Item.NewItem(target.GetSource_DropAsItem(),
                        target.getRect(), type);
                    Main.item[newItem].noGrabDelay = 0;
                    target.Shards().sacrificedKatana = false;
                    target.statLife = 10;

                    if (SoA.ClientConfig.dialogue)
                    {
                        string key = SoA.LocalizeCommon + "AreusKatanaTransformation";
                        if (Main.netMode == NetmodeID.MultiplayerClient && newItem >= 0)
                        {
                            NetMessage.SendData(MessageID.SyncItem, -1, -1, null, newItem, 1f);
                            ChatHelper.SendChatMessageToClient(NetworkText.FromKey(key),
                                Color.White, target.whoAmI);
                        }
                        else
                        {
                            Main.NewText(Language.GetText(key));
                        }
                    }
                    SoundEngine.PlaySound(SoundID.ScaryScream, target.Center);
                    break;
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            lightColor = Color.White;
            Projectile.DrawAfterImage(lightColor);
            Projectile.DrawBloomTrail(Color.Firebrick.UseA(50), SoA.DiamondBloom, MathHelper.PiOver2);
            return base.PreDraw(ref lightColor);
        }
    }
}