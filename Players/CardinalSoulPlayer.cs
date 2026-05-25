using Microsoft.Xna.Framework;
using ShardsOfAtheria.Globals;
using ShardsOfAtheria.ShardsUI.CardinalSelection;
using ShardsOfAtheria.Utilities;
using System.Linq;
using Terraria;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ShardsOfAtheria.Players
{
    /// <summary>
    /// Sins: Envy, Gluttony, Greed, Lust, Pride, Sloth, Wrath<br/>
    /// Virtues: Charity, Chassity, Diligence, Humility, Kindness, Patience, Temperance
    /// </summary>
    public static partial class CardinalSoulID
    {
        public const int Envy = 1;
        public const int Gluttony = 2;
        public const int Greed = 3;
        public const int Lust = 4;
        public const int Pride = 5;
        public const int Sloth = 6;
        public const int Wrath = 7;

        public const int Charity = 8;
        public const int Chassity = 9;
        public const int Diligence = 10;
        public const int Humility = 11;
        public const int Kindness = 12;
        public const int Patience = 13;
        public const int Temperance = 14;

        public static int ConvertToCounterpart(int soul)
        {
            int swap = soul switch
            {
                Envy => Kindness,
                Gluttony => Temperance,
                Greed => Charity,
                Lust => Chassity,
                Pride => Humility,
                Sloth => Diligence,
                Wrath => Patience,

                Charity => Greed,
                Chassity => Lust,
                Diligence => Sloth,
                Humility => Pride,
                Kindness => Envy,
                Patience => Wrath,
                Temperance => Gluttony,
                _ => 0,
            };
            return swap;
        }
    }

    public partial class CardinalSoulPlayer : ModPlayer
    {

        public override void SaveData(TagCompound tag)
        {
            tag["selectedSin"] = cardinalSoul;
        }

        public override void LoadData(TagCompound tag)
        {
            if (tag.TryGet("selectedSin", out int soul)) cardinalSoul = soul;
        }

        public override void Load()
        {
            On_Player.Heal += GluttonyHeal;
            //On_Player.AddBuff += On_GluttonyAddBuff;
        }

        public override void ResetEffects()
        {
            if (gluttonyFoodCoolDown < 0) gluttonyFoodCoolDown = 0;
            else if (gluttonyFoodCoolDown > 0) gluttonyFoodCoolDown--;

            if (wrathRetainFuryTime > 0) wrathRetainFuryTime--;
        }

        public override void PreUpdate()
        {
            GluttonyPreUpdate();
            PridePreUpdate();
            SlothPreUpdate();
        }

        public override void PostUpdate()
        {
            EnvyPostUpdate();
            LustPostUpdate();
            PridePostUpdate();
            SlothPostUpdate();
            WrathPostUpdate();
        }

        public override void OnRespawn()
        {
            GluttonySetHunger();
        }

        public override void OnEnterWorld()
        {
            GluttonySetHunger();

            if (!SoulActive)
            {
                string key = SoA.SinAbility.GetAssignedKeys().FirstOrDefault();
                string text = ShardsHelpers.LocalizeCommon("SinNotification", key);
                NetworkText networkText = NetworkText.FromLiteral(text);
                ChatHelper.SendChatMessageToClient(networkText, Color.Purple, Player.whoAmI);
            }
        }

        public override void UpdateBadLifeRegen()
        {
            GluttonyRegenDebuff();
            GreedFire();
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (SoA.SinAbility.JustPressed)
            {
                if (SoulActive && Player.controlTorch)
                {
                    SinfulUI.Instance.ToggleSelected();
                }
                else if (!SoulActive)
                    SinfulUI.Instance.ToggleSelections();
                else if (PridefulSinner) PrideCoin();
                else if (WrathfulSinner) WrathRage();
            }
        }

        public override void UpdateLifeRegen()
        {
            PrideRegen();
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (SoulActive)
            {
                var soulTarget = target.GetGlobalNPC<CardinalSoulNPC>();
                float multiplier = 1f;
                if (soulTarget.soulEdict[cardinalSoul]) multiplier = 0.5f;
                else if (soulTarget.soulAnathema[cardinalSoul]) multiplier = 2f;
                if (multiplier != 1f)
                {
                    modifiers.FinalDamage *= multiplier;
                    CombatText.NewText(target.Hitbox, Color.Purple, multiplier + "x", dot: true);
                }
            }

            EnvyModifyHit(target, ref modifiers);
            WrathModifyHit(target, ref modifiers);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            GluttonyHit(target, hit);
            EnvyHit(target);
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            PrideAddSuccessfulAttack(target, !item.IsTool());
            LustItemHit(target, item);
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            PrideAddSuccessfulAttack(target, !proj.NonWhipSummon());
            LustProjectileHit(target, proj);
        }

        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            GluttonyModifyHurt(ref modifiers);
            LustCrit(ref modifiers);
            WrathCrit(ref modifiers);
        }

        public override void PostHurt(Player.HurtInfo info)
        {
            PrideHurt();
            WrathHurt(info);
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            GreedDeath();
            PrideDeath();
        }

        public override bool OnPickup(Item item)
        {
            GluttonyPickUp(item);
            GreedPickUp(item);
            return base.OnPickup(item);
        }
    }
}
