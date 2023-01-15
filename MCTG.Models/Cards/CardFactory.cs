using MCTG.Models.Cards.MonsterCards;
using MCTG.Models.Cards.SpellCards;

namespace MCTG.Models.Cards
{
    public enum CardType
    {
        Spell,
        Monster,
        Empty
    }

    public enum ElementType
    {
        Regular,
        Water,
        Fire,
        NaE
    }

    public class CardFactory
    {
        private static readonly Dictionary<string, Func<double,Guid,Guid, ICard>> Cards = new()
        {
            ["Empty"] = (dmg, id,owner) => EmptyCard.Instance(),
            ["FireSpell"] = (dmg,id, owner) => new FireSpellCard(id, owner,dmg),
            ["WaterSpell"] = (dmg, id, owner) => new WaterSpellCard(id, owner, dmg),
            ["RegularSpell"] = (dmg, id, owner) => new RegularSpellCard(id, owner, dmg),
            ["FireElf"] = (dmg, id,owner) => new FireElfCard(id,owner,dmg),
            ["WaterElf"] = (dmg, id, owner) => new WaterElfCard(id, owner, dmg),
            ["RegularElf"] = (dmg, id, owner) => new RegularElfCard(id, owner, dmg),
            ["FireGoblin"] = (dmg, id, owner) => new FireGoblinCard(id, owner, dmg),
            ["WaterGoblin"] = (dmg, id, owner) => new WaterGoblinCard(id, owner, dmg),
            ["RegularGoblin"] = (dmg, id, owner) => new RegularGoblinCard(id, owner, dmg),
            ["Knight"] = (dmg, id, owner) => new KnightCard(id, owner, dmg),
            ["Kraken"] = (dmg, id, owner) => new KrakenCard(id, owner, dmg),
            ["Ork"] = (dmg, id, owner) => new OrkCard(id, owner, dmg),
            ["FireTroll"] = (dmg, id, owner) => new FireTrollCard(id, owner, dmg),
            ["WaterTroll"] = (dmg, id, owner) => new WaterTrollCard(id, owner, dmg),
            ["RegularTroll"] = (dmg, id, owner) => new RegularTrollCard(id, owner, dmg),
            ["FireWizard"] = (dmg, id, owner) => new FireWizardCard(id, owner, dmg),
            ["WaterWizard"] = (dmg, id, owner) => new WaterWizardCard(id, owner, dmg),
            ["RegularWizard"] = (dmg, id, owner) => new RegularWizardCard(id, owner, dmg),
            ["Dragon"] = (dmg, id, owner) => new DragonCard(id, owner, dmg)
        };

        public static ICard? GetCard(string cardName, double damage, Guid cardId, Guid ownerId)
        {
            try
            {
                return Cards[cardName].Invoke(damage, cardId, ownerId);
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        public static ICard? GetCard(CardType type, ElementType element, string cardName, double damage, Guid cardId, Guid ownerId)
        {
            try
            {
                switch (type)
                {
                    case (CardType.Empty):
                        return Cards[type.ToString()].Invoke(damage, cardId, ownerId);
                    case (CardType.Spell):
                        if (element.Equals(ElementType.NaE)) 
                            throw new KeyNotFoundException("There is no Spell without an Element"); //ToDo change Exception
                        return Cards[element.ToString() + type.ToString()].Invoke(damage, cardId, ownerId);
                    case CardType.Monster:
                        switch (element)
                        {
                            case ElementType.NaE:
                                return Cards[cardName].Invoke(damage, cardId, ownerId);
                            default: 
                                return Cards[element.ToString() + cardName].Invoke(damage, cardId, ownerId);
                        }
                }
            }
            catch (KeyNotFoundException)
            {
                return null;
            }

            return null;
        }
    }
}
