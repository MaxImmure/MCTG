using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private static readonly Dictionary<string, Func<double, ICard>> Cards = new()
        {
            ["Empty"] = dmg => EmptyCard.Instance(),
            ["FireSpell"] = dmg => new FireSpellCard(dmg),
            ["WaterSpell"] = dmg => new WaterSpellCard(dmg),
            ["RegularSpell"] = dmg => new RegularSpellCard(dmg),
            ["FireElf"] = dmg => new FireElfCard(dmg),
            ["WaterElf"] = dmg => new WaterElfCard(dmg),
            ["RegularElf"] = dmg => new RegularElfCard(dmg),
            ["FireGoblin"] = dmg => new FireGoblinCard(dmg),
            ["WaterGoblin"] = dmg => new WaterGoblinCard(dmg),
            ["RegularGoblin"] = dmg => new RegularGoblinCard(dmg),
            ["Knight"] = dmg => new KnightCard(dmg),
            ["Kraken"] = dmg => new KrakenCard(dmg),
            ["Ork"] = dmg => new OrkCard(dmg),
            ["FireTroll"] = dmg => new FireTrollCard(dmg),
            ["WaterTroll"] = dmg => new WaterTrollCard(dmg),
            ["RegularTroll"] = dmg => new RegularTrollCard(dmg),
            ["FireWizzard"] = dmg => new FireWizzardCard(dmg),
            ["WaterWizzard"] = dmg => new WaterWizzardCard(dmg),
            ["RegularWizzard"] = dmg => new RegularWizzardCard(dmg),
            ["Dragon"] = dmg => new DragonCard(dmg)
        };

        public static ICard? GetCard(CardType type, ElementType element, string cardName, double damage)
        {
            try
            {
                switch (type)
                {
                    case (CardType.Empty):
                        return Cards[type.ToString()].Invoke(damage); //ToDo Test
                    case (CardType.Spell):
                        if (element.Equals(ElementType.NaE)) throw new KeyNotFoundException("There is no Spell without an Element"); //ToDo change Exception
                        return Cards[element.ToString() + type.ToString()].Invoke(damage);
                    case CardType.Monster:
                        switch (element)
                        {
                            case ElementType.NaE:
                                return Cards[cardName].Invoke(damage);
                            default: return Cards[element.ToString() + cardName].Invoke(damage);
                        }
                }
                //ToDo
            }
            catch (KeyNotFoundException)
            {
                return null;
            }

            return EmptyCard.Instance();
        }
    }
}
