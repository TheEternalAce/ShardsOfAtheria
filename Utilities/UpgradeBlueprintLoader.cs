using ShardsOfAtheria.Globals;
using ShardsOfAtheria.ShardsConditions;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ShardsOfAtheria.Utilities
{
    public class UpgradeBlueprintLoader : ILoadable
    {
        public static readonly List<UpgradeBlueprint> upgrades = [];

        public static int Register(UpgradeBlueprint blueprint)
        {
            upgrades.Add(blueprint);
            SoAGlobalItem.UpgradeableItem.Add(blueprint.BaseItemType);
            return upgrades.Count - 1;
        }

        public void Load(Mod mod)
        {
        }

        public void Unload()
        {
        }

        public static UpgradeBlueprint Find(Item item, Player player)
        {
            UpgradeBlueprint blueprint = null;
            foreach (var upgradeBlueprint in upgrades)
            {
                if (upgradeBlueprint.BaseItemType == item.type) blueprint = upgradeBlueprint;
                if (blueprint != null)
                {
                    bool blueprintItemPrereqisiteMet = blueprint.CheckItem == null || blueprint.CheckItem(item);
                    bool blueprintPlayerPrereqisiteMet = blueprint.CheckPlayer == null || blueprint.CheckPlayer(player);
                    if (blueprintItemPrereqisiteMet && blueprintPlayerPrereqisiteMet) break;
                }
            }
            return blueprint;
        }
    }

    public abstract class UpgradeBlueprint : ModType, ILocalizedModType
    {
        public int Type { get; private set; }

        public abstract int BaseItemType { get; }
        public abstract Item ResultItem { get; }
        public abstract int[,] Materials { get; }
        public virtual Func<Item, bool> CheckItem => null;
        public virtual Func<Player, bool> CheckPlayer => null;
        public virtual int TimeToUpgrade => 1200;

        public string LocalizationCategory => "Atherian.Blueprint";

        protected virtual LocalizedText ItemCheck => this.GetLocalization("ItemCheckFailed", () => "");
        protected virtual LocalizedText PlayerCheck => this.GetLocalization("PlayerCheckFailed", () => "");

        private LocalizedText _itemCheckFailed;
        private LocalizedText _playerCheckFailed;

        public LocalizedText GetItemFailMessage() => _itemCheckFailed;
        public LocalizedText GetPlayerFailMessage() => _playerCheckFailed;

        public bool ReplaceItem => BaseItemType != ResultItem.type;
        public bool RequisitesMet(Item item, Player player) => (CheckItem == null || CheckItem(item)) && (CheckPlayer == null || CheckPlayer(player));

        public UpgradeBlueprint() { }

        public virtual void ModifyItem(Item item, Player player)
        {
        }

        protected sealed override void Register()
        {
            _itemCheckFailed = ItemCheck;
            _playerCheckFailed = PlayerCheck;
            Type = UpgradeBlueprintLoader.Register(this);
        }

        public virtual void CreateRecipe()
        {
            Recipe recipe = Recipe.Create(ResultItem.type);
            recipe.AddIngredient(BaseItemType);
            for (int i = 0; i < Materials.GetLength(0); i++)
            {
                recipe.AddIngredient(Materials[i, 0], Materials[i, 1]);
            }
            recipe.AddCondition(SoAConditions.Upgrade);
            recipe.AddCondition(Mod.GetLocalization("Conditions.BlueprintRequisites"), () => RequisitesMet(null, Main.LocalPlayer));
            recipe.Register();
        }
    }
}
