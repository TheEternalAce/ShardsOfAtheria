using System.Collections.Generic;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Items.Weapons.Biochemical
{
	public class BiochemicalDamage : DamageClass
	{
		public override void SetStaticDefaults()
		{
			// Make weapons with this damage type have a tooltip of 'X example damage'.
			ClassName.SetDefault("biochemical damage");
		}

        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
		{
			if (damageClass == DamageClass.Generic)
				return StatInheritanceData.Full;

			return new StatInheritanceData(
				damageInheritance: 0f,
				critChanceInheritance: 0f,
				attackSpeedInheritance: 0f,
				armorPenInheritance: 0f,
				knockbackInheritance: 0f
			);
		}

        public override bool GetEffectInheritance(DamageClass damageClass)
        {
            return false;
        }
	}
}