using ShardsOfAtheria.Buffs.PlayerDebuff.SinDebuffs;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.SinfulSouls
{
    public class PrideSoul : SinfulSouls
    {
        public override int SoulBuffType => ModContent.BuffType<PrideBuff>();
    }

    public class PridePlayer : ModPlayer
    {
        public bool soulActive;
        public int noHitTimer = 0;
        public float flawlessBuff = 0;
        public int attacks = 0;
        public int attacksHit = 0;
        public int attackTimer = 0;
        public int hitCooldown = 0;
        public int hitDelay = 0;
        public bool prideful = false;

        public override void ResetEffects()
        {
            soulActive = false;
        }

        public override void PreUpdate()
        {
            if (!soulActive)
            {
                flawlessBuff = 0;
                noHitTimer = 0;
                attacks = 0;
                attacksHit = 0;
                hitCooldown = 0;
                prideful = false;
            }
            else
            {
                if (!Player.InCombat() && flawlessBuff > 0)
                {
                    noHitTimer--;
                    if (noHitTimer <= -60)
                    {
                        flawlessBuff -= .02f;
                        noHitTimer = 0;
                    }
                }

                if (attacks < attacksHit) attacksHit = attacks;

                if (attackTimer > 0)
                {
                    if (--attackTimer == 0)
                    {
                        if (attacksHit > attacks * 0.8) prideful = true;
                        else if (attacksHit < attacks * 0.75)
                        {
                            Player.AddBuff<Embarassment>(300);
                            attacks = 0;
                            attacksHit = 0;
                            prideful = false;
                        }
                    }
                }

                if (hitCooldown > 0) hitCooldown--;
            }
        }

        bool IncrementHits(NPC target)
        {
            return soulActive && target.lifeMax > 5 && Player.InCombat() && attacksHit < attacks && hitCooldown == 0;
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (IncrementHits(target) && !item.IsTool())
            {
                //item.GetGlobalItem<PrideWeapon>().uses++;
                hitCooldown = Player.itemTime;
                attacksHit++;
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (IncrementHits(target) && !proj.NonWhipSummon())
            {
                //Player.HeldItem.GetGlobalItem<PrideWeapon>().uses++;
                hitCooldown = Player.itemTime;
                attacksHit++;
            }
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            flawlessBuff = 0;
            noHitTimer = 0;
        }

        public override void UpdateDead()
        {
            prideful = false;
            flawlessBuff = 0;
            noHitTimer = 0;
            attacks = 0;
            attacksHit = 0;
            hitCooldown = 0;
        }
    }

    public class PrideWeapon : GlobalItem
    {
        public int uses = 0;
        int useTimer = 0;

        public override bool InstancePerEntity => true;

        public override void UpdateInventory(Item item, Player player)
        {
            if (!item.IsWeapon()) { uses = 0; useTimer = 0; }
            if (uses > 0)
            {
                if (++useTimer > 30)
                {
                    uses--;
                    useTimer = 0;
                }
                if (uses > 25) uses = 25;
            }
            else useTimer = 0;
        }

        public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
        {
            damage -= uses * 0.02f;
        }

        public override bool? UseItem(Item item, Player player)
        {
            PridePlayer pridePlayer = player.Pride();
            if (pridePlayer.soulActive && player.ItemAnimationJustStarted && player.InCombat() && item.IsWeapon() && !item.DamageType.CountsAsClass(DamageClass.Summon))
            {
                pridePlayer.attacks++;
                if (pridePlayer.attackTimer <= 0) pridePlayer.attackTimer = 300;
            }
            return null;
        }
    }

    public class PrideBuff : SinfulSoulBuff
    {
        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            tip = ShardsHelpers.Localize("Items.PrideSoul.Tooltip");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            var pride = player.Pride();
            pride.soulActive = true;
            if (player.InCombat()) pride.noHitTimer++;
            if (pride.noHitTimer >= 600)
            {
                pride.flawlessBuff += .02f;
                pride.noHitTimer = 0;
            }
            player.GetDamage(DamageClass.Generic) += pride.flawlessBuff;
            if (pride.prideful) player.moveSpeed += .15f;
            base.Update(player, ref buffIndex);
        }
    }
}
