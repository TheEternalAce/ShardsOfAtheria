namespace ShardsOfAtheria.Players
{
    public partial class CardinalSoulPlayer
    {
        public bool Charity => cardinalSoul == CardinalSoulID.Charity;
        public bool Chassity => cardinalSoul == CardinalSoulID.Chassity;
        public bool Diligence => cardinalSoul == CardinalSoulID.Diligence;
        public bool Humility => cardinalSoul == CardinalSoulID.Humility;
        public bool Kindness => cardinalSoul == CardinalSoulID.Kindness;
        public bool Patience => cardinalSoul == CardinalSoulID.Patience;
        public bool Temperance => cardinalSoul == CardinalSoulID.Temperance;
        public bool Saint => cardinalSoul > 7;
    }
}
