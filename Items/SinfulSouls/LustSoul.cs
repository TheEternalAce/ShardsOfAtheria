using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SinfulSouls
{
    public class LustSoul : SinfulSouls
    {
        public override int SoulBuffType => ModContent.BuffType<LustBuff>();
    }

    public class LustPlayer : ModPlayer
    {
        public bool soulActive;

        public override void ResetEffects()
        {
            soulActive = false;
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (soulActive && Main.rand.NextBool(50)) Item.NewItem(target.GetSource_OnHurt(item), target.getRect(), ItemID.Heart);
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (soulActive)
            {
                int heartChance = 100;
                if (proj.DamageType.CountsAsClass(DamageClass.Melee)) heartChance = 50;
                if (Main.rand.NextBool(heartChance)) Item.NewItem(target.GetSource_OnHurt(proj), target.getRect(), ItemID.Heart);
            }
        }

        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            if (soulActive)
            {
                float critChance = 0.25f;
                if (Main.expertMode) critChance += 0.06f;
                if (Main.masterMode) critChance += 0.12f;
                if (Main.hardMode) critChance += 0.08f;
                if (NPC.downedPlantBoss) critChance += 0.06f;
                if (NPC.downedAncientCultist) critChance += 0.12f;
                if (Main.rand.NextFloat() < critChance)
                {
                    modifiers.FinalDamage *= 2;
                    SoundEngine.PlaySound(SoundID.AbigailCry, Player.Center);
                }
            }
        }
    }

    public class LustBuff : SinfulSoulBuff
    {
        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            tip = ShardsHelpers.Localize("Items.LustSoul.Tooltip");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.maxMinions += 3;
            player.GetDamage(DamageClass.Generic) -= 0.2f;
            player.moveSpeed -= 0.1f;
            player.GetModPlayer<LustPlayer>().soulActive = true;
            base.Update(player, ref buffIndex);
        }
    }
}
