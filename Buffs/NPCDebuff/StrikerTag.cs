using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShardsOfAtheria.Projectiles.Weapon.Summon;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Buffs.NPCDebuff
{
    public class StrikerTag : ModBuff
    {
        public static readonly int TagDamage = 12;

        public override string Texture => SoA.DebuffTemplate;

        public override void SetStaticDefaults()
        {
            BuffID.Sets.IsAnNPCWhipDebuff[Type] = true;
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
                if (projectile.type != ModContent.ProjectileType<StrikerCurrent>())
                {
                    foreach (var npc2 in Main.npc)
                    {
                        if (npc2.CanBeChasedBy())
                        {
                            if (npc2.HasBuff<StrikerTag>())
                            {
                                var vector2 = Vector2.Normalize(npc2.Center - npc.Center);
                                Projectile.NewProjectile(npc.GetSource_FromThis(),
                                    npc.Center, vector2 * 16f, ModContent.ProjectileType<StrikerCurrent>(),
                                33 / 3 * tagAmount, 0, Main.myPlayer, npc2.whoAmI);
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
                    Color.White,
                    0,
                    new Vector2(),
                    1f,
                    SpriteEffects.None,
                    1);
            }
        }
    }
}
