using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Projectiles.Summon;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.NPCDebuff
{
    public class StrikerTag : ModBuff
    {
        public static readonly int TagDamage = 5;

        public override string Texture => SoA.DebuffTemplate;

        public override void SetStaticDefaults()
        {
            BuffID.Sets.IsATagBuff[Type] = true;
        }
    }

    public class StrikerNPC : GlobalNPC
    {
        public int tagAmount;

        public override bool InstancePerEntity => true;

        public override void AI(NPC npc)
        {
            base.AI(npc);
            if (tagAmount > 0 && !npc.HasBuff<StrikerTag>())
            {
                tagAmount = 0;
            }
        }

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            // Only player attacks should benefit from this buff, hence the NPC and trap checks.
            if (projectile.npcProj || projectile.trap || !projectile.IsMinionOrSentryRelated)
                return;

            // SummonTagDamageMultiplier scales down tag damage for some specific minion and sentry projectiles for balance purposes.
            var projTagMultiplier = ProjectileID.Sets.SummonTagDamageMultiplier[projectile.type];
            if (npc.HasBuff<StrikerTag>())
            {
                // Apply a flat bonus to every hit
                modifiers.FlatBonusDamage += StrikerTag.TagDamage * tagAmount * projTagMultiplier;
            }
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            if (npc.HasBuff<StrikerTag>())
            {
                if (projectile.type != ModContent.ProjectileType<StrikerCurrent>() &&
                    projectile.type != ModContent.ProjectileType<StrikerRod>() &&
                    projectile.IsMinionOrSentryRelated)
                {
                    foreach (var targetNPC in Main.npc)
                    {
                        if (targetNPC.CanBeChasedBy())
                        {
                            if (targetNPC.HasBuff<StrikerTag>())
                            {
                                var vector2 = Vector2.Normalize(targetNPC.Center - npc.Center);
                                Projectile.NewProjectile(npc.GetSource_FromThis(),
                                    npc.Center, vector2 * 16f, ModContent.ProjectileType<StrikerCurrent>(),
                                    4 * tagAmount, 0, Main.myPlayer, targetNPC.whoAmI);
                            }
                        }
                    }
                }
            }
        }

        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (npc.HasBuff<StrikerTag>())
            {
                var texture = ModContent.Request<Texture2D>("ShardsOfAtheria/Assets/StrikerCrosshair");
                Rectangle frame = new(0, 52 * (tagAmount - 1), 52, 52);
                spriteBatch.Draw(texture.Value,
                    npc.Center - screenPos - new Vector2(26),
                    frame,
                    Color.White.UseA(150),
                    0,
                    new Vector2(),
                    1f,
                    SpriteEffects.None,
                    1);
            }
        }
    }
}
