using Microsoft.Xna.Framework;
using ShardsOfAtheria.Buffs.PlayerBuff;
using ShardsOfAtheria.Projectiles.Magic;
using ShardsOfAtheria.Projectiles.Melee;
using ShardsOfAtheria.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Players
{
    public partial class AreusArmorPlayer
    {
        public bool guardSetPrevious;
        public bool guardSet;
        public int areusEnergy;
        public const int AREUS_ENERGY_MAX = 100;
        public const int EnergyTimerMax = 20;
        public int energyTimer;

        private void GuardActive_Melee()
        {
            Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, Vector2.Zero, ModContent.ProjectileType<AreusShockwave_MeleeArmor>(),
                (int)ClassDamage.ApplyTo(30 + areusEnergy), 0f);
            SoundEngine.PlaySound(SoundID.Item38, Player.Center);
        }
        private void GuardActive_Ranged()
        {
            if (!Player.HasBuff<ElectricMarksman>())
            {
                Player.AddBuff<ElectricMarksman>(areusEnergy);
            }
        }
        private void GuardActive_Magic()
        {
            if (areusEnergy >= AREUS_ENERGY_MAX)
            {
                SoundEngine.PlaySound(SoundID.NPCDeath56);
                float numberProjectiles = 3; // 3 shots
                float rotation = MathHelper.ToRadians(10);
                var source = Player.GetSource_FromThis();
                var position = Player.Center;
                var velocity = Main.MouseWorld - Player.Center;
                float speed = 14f;
                int type = ModContent.ProjectileType<ElectricWave>();
                int damage = 40;
                float knockback = 0;
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                    perturbedSpeed.Normalize();
                    perturbedSpeed *= speed;
                    Projectile.NewProjectile(source, position, perturbedSpeed,
                        type, damage, knockback, Player.whoAmI);
                }
            }
        }
        private void GuardActive_Summon()
        {
            if (areusEnergy >= AREUS_ENERGY_MAX)
            {
                if (!Player.HasBuff<ChargedMinions>())
                {
                    Player.AddBuff<ChargedMinions>(600);
                }
            }
        }
    }
}
