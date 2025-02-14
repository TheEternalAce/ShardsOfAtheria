using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.PlayerDebuff
{
    public class MementoMori : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            var shards = player.Shards();
            if (shards.deathInevitibility > 0) player.buffTime[buffIndex] = 2;
            player.GetDamage(DamageClass.Generic) -= 0.1f * shards.deathInevitibility;
            player.statDefense -= 10 * shards.deathInevitibility;
            player.statLifeMax2 -= player.statLifeMax2 / 10 * shards.deathInevitibility;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, int buffIndex, ref BuffDrawParams drawParams)
        {
            var shards = Main.LocalPlayer.Shards();
            int shakeIntensity = shards.deathInevitibility;
            if (shakeIntensity > 6) shakeIntensity = 6;
            if (shakeIntensity > 0)
            {
                Vector2 shake = new(Main.rand.NextFloat(-1, 2), Main.rand.NextFloat(-1, 2));

                drawParams.Position += shake * shakeIntensity;
                drawParams.TextPosition += shake * shakeIntensity;
            }
            return base.PreDraw(spriteBatch, buffIndex, ref drawParams);
        }
    }
}
