using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ShardsOfAtheria.Utilities;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.NPCs.Variant.Harpy
{
    public abstract class Harpies : ModNPC
    {
        public int projectileType = 0;
        public int projectileDamage = 0;
        public int debuffType = 0;
        public List<IBestiaryInfoElement> bestiaryInfoElements = new();

        public virtual bool DebuffCondition => Main.expertMode || Main.hardMode;

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.Harpy];

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new()
            {
                Velocity = 1f
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

            NPC.AddDamageType(11);
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(NPCID.Harpy);
            NPC.damage = 0;
            NPC.defense = 0;
            AnimationType = NPCID.Harpy;
            Banner = NPC.type;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            if (Main.expertMode)
            {
                //NPC.damage /= 2;
            }
            if (Main.masterMode)
            {

            }
        }

        public override void AI()
        {
            NPC.ai[0] += 1f;
            if (NPC.ai[0] == 30f || NPC.ai[0] == 60f || NPC.ai[0] == 90f)
            {
                if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
                {
                    Vector2 position = NPC.Center;
                    Vector2 velocity = Vector2.Normalize(Main.player[NPC.target].Center - position);
                    if (Main.rand.NextBool(3 - (SoA.Massochist() ? 1 : 0)) && NPC.ai[0] == 30f)
                    {
                        SpecialAttack(velocity);
                        NPC.ai[0] = 91f;
                    }
                    else
                    {
                        velocity = velocity.RotatedByRandom(MathHelper.ToRadians(15));
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, velocity * 6f, projectileType, projectileDamage, 0f, Main.myPlayer);
                    }
                }
            }
            else if (NPC.ai[0] >= 400 + Main.rand.Next(400))
            {
                NPC.ai[0] = 0f;
            }
        }

        public virtual void SpecialAttack(Vector2 normalizedVelocity)
        {

        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            string key = this.GetLocalizationKey("Bestiary");
            bestiaryInfoElements.Add(new FlavorTextBestiaryInfoElement(key));
            bestiaryEntry.Info.AddRange(bestiaryInfoElements);
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Feather, 2));
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            if (DebuffCondition)
            {
                target.AddBuff(debuffType, 60);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Asset<Texture2D> texture = ModContent.Request<Texture2D>(Texture);
            SpriteEffects effects = NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            drawColor = NPC.GetNPCColorTintedByBuffs(drawColor);
            Main.EntitySpriteDraw(texture.Value, NPC.Center - screenPos, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() / 2f, 1f, effects);
            return false;
        }
    }
}
