﻿using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs;
using Terraria.ID;

namespace ShardsOfAtheria.Projectiles.NPCProj.Variant
{
    public class BoneFeather : ModProjectile
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.BoneFeather;

        public override void SetDefaults()
        {
            Projectile refProj = new Projectile();
            refProj.SetDefaults(ProjectileID.HarpyFeather);

            Projectile.width = refProj.width;
            Projectile.height = refProj.height;

            Projectile.timeLeft = 5 * 60;
            Projectile.aiStyle = -1;
            Projectile.hostile = true;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if ((Main.expertMode || Main.hardMode) && Main.rand.NextBool(10))
            {
                target.AddBuff(BuffID.Stoned, 60);
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Bone, Projectile.velocity.X * 0.1f, Projectile.velocity.Y * 0.1f, 0, default, 0.75f);
            }
        }
    }
}