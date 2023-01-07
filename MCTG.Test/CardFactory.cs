using MCTG.Models.Cards.SpellCards;
using MCTG.Models.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MCTG.Models.Cards.MonsterCards;

namespace MCTG.Test
{
    internal class CardFactory
    {
        IEnumerable<CardType> cardTypes;
        IEnumerable<ElementType> elementType;

        [SetUp]
        public void Setup()
        {
            cardTypes = Enum.GetValues(typeof(CardType)).Cast<CardType>().ToList();
            elementType = Enum.GetValues(typeof(ElementType)).Cast<ElementType>().ToList();
        }

        [Test]
        public void TestGetEmptyCard()
        {
            var emptyCard = Models.Cards.CardFactory.GetCard(CardType.Empty, ElementType.NaE, String.Empty, double.NaN);

            Assert.IsNotNull(emptyCard);
            Assert.That(EmptyCard.Instance(), Is.EqualTo(emptyCard));
        }

        [Test]
        public void TestNotExistingCard()
        {
            string nEname = "NonExistingCardTypeTestName";
            var nECard = Models.Cards.CardFactory.GetCard(CardType.Monster, ElementType.Water, nEname, -1);

            Assert.IsNull(nECard);
        }

        [Test]
        public void TestGetAllSpellCard()
        {
            //GetSpellCard Method?
            var fireSpellCard = Models.Cards.CardFactory.GetCard(CardType.Spell, ElementType.Fire, String.Empty, -1); //ToDo fix negative damage
            var waterSpellCard = Models.Cards.CardFactory.GetCard(CardType.Spell, ElementType.Water, String.Empty, -1);
            var regularSpellCard = Models.Cards.CardFactory.GetCard(CardType.Spell, ElementType.Regular, String.Empty, -1);

            Assert.IsInstanceOf<FireSpellCard>(fireSpellCard);
            Assert.IsInstanceOf<WaterSpellCard>(waterSpellCard);
            Assert.IsInstanceOf<RegularSpellCard>(regularSpellCard);
        }

        [Test]
        public void TestGetDragonCard()
        {
            var validDragonCard = Models.Cards.CardFactory.GetCard(CardType.Monster, ElementType.NaE, "Dragon", 10);
            var invalidDragonCard = Models.Cards.CardFactory.GetCard(CardType.Monster, ElementType.Fire, "Dragon", 10); //Dragon has no Element

            Assert.IsInstanceOf<DragonCard>(validDragonCard);
            Assert.IsNull(invalidDragonCard);
        }

        [Test]
        public void TestGetAllElfCards()
        {
            var fireElf = Models.Cards.CardFactory.GetCard(CardType.Monster, ElementType.Fire, "Elf", 10);
            var waterElf = Models.Cards.CardFactory.GetCard(CardType.Monster, ElementType.Water, "Elf", 10);
            var regularElf = Models.Cards.CardFactory.GetCard(CardType.Monster, ElementType.Regular, "Elf", 10);

            Assert.IsInstanceOf<FireElfCard>(fireElf);
            Assert.IsInstanceOf<WaterElfCard>(waterElf);
            Assert.IsInstanceOf<RegularElfCard>(regularElf);
        }

        [Test]
        public void TestGetAllGoblinCards()
        {
            var fireGoblin = Models.Cards.CardFactory.GetCard(CardType.Monster, ElementType.Fire, "Goblin", 10);
            var waterGoblin = Models.Cards.CardFactory.GetCard(CardType.Monster, ElementType.Water, "Goblin", 10);
            var regularGoblin = Models.Cards.CardFactory.GetCard(CardType.Monster, ElementType.Regular, "Goblin", 10);

            Assert.IsInstanceOf<FireGoblinCard>(fireGoblin);
            Assert.IsInstanceOf<WaterGoblinCard>(waterGoblin);
            Assert.IsInstanceOf<RegularGoblinCard>(regularGoblin);
        }

        [Test]
        public void TestGetKnightCard()
        {
            var knight = Models.Cards.CardFactory.GetCard(CardType.Monster, ElementType.NaE, "Knight", 10);

            Assert.IsInstanceOf<KnightCard>(knight);
        }


        [Test]
        public void TestGetKrakenCard()
        {
            var kraken = Models.Cards.CardFactory.GetCard(CardType.Monster, ElementType.NaE, "Kraken", 10);

            Assert.IsInstanceOf<KrakenCard>(kraken);
        }

        [Test]
        public void TestGetOrkCard()
        {
            var ork = Models.Cards.CardFactory.GetCard(CardType.Monster, ElementType.NaE, "Ork", 10);

            Assert.IsInstanceOf<OrkCard>(ork);
        }

        [Test]
        public void TestGetAllTrollCards()
        {
            var fireTroll = Models.Cards.CardFactory.GetCard(CardType.Monster, ElementType.Fire, "Troll", 10);
            var waterTroll = Models.Cards.CardFactory.GetCard(CardType.Monster, ElementType.Water, "Troll", 10);
            var regularTroll = Models.Cards.CardFactory.GetCard(CardType.Monster, ElementType.Regular, "Troll", 10);

            Assert.IsInstanceOf<FireTrollCard>(fireTroll);
            Assert.IsInstanceOf<WaterTrollCard>(waterTroll);
            Assert.IsInstanceOf<RegularTrollCard>(regularTroll);
        }

        [Test]
        public void TestGetAllWizzardCards()
        {
            var fireWizzard = Models.Cards.CardFactory.GetCard(CardType.Monster, ElementType.Fire, "Wizzard", 10);
            var waterWizzard = Models.Cards.CardFactory.GetCard(CardType.Monster, ElementType.Water, "Wizzard", 10);
            var regularWizzard = Models.Cards.CardFactory.GetCard(CardType.Monster, ElementType.Regular, "Wizzard", 10);

            Assert.IsInstanceOf<FireWizzardCard>(fireWizzard);
            Assert.IsInstanceOf<WaterWizzardCard>(waterWizzard);
            Assert.IsInstanceOf<RegularWizzardCard>(regularWizzard);
        }


    }
}
