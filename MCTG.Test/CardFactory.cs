using MCTG.Models.Cards.SpellCards;
using MCTG.Models.Cards.MonsterCards;
using MCTG.Models.Cards;

namespace MCTG.Test
{
    internal class CardFactory
    {

        [SetUp]
        public void Setup()
        { }
        
        [Test]
        public void TestGetEmptyCard()
        {
            var emptyCard = Models.Cards.CardFactory.GetCard(CardType.Empty, ElementType.NaE, String.Empty, double.NaN, Guid.Empty, Guid.Empty);

            Assert.IsNotNull(emptyCard);
            Assert.That(EmptyCard.Instance(), Is.EqualTo(emptyCard));
        }

        [Test]
        public void TestNotExistingCard()
        {
            string nEname = "NonExistingCardTypeTestName";
            var nECard = Models.Cards.CardFactory.GetCard(CardType.Monster, ElementType.Water, nEname, -1, Guid.Empty, Guid.Empty);

            Assert.IsNull(nECard);
        }

        [Test]
        public void TestGetAllSpellCard()
        {
            //GetSpellCard Method?
            var fireSpellCard = Models.Cards.CardFactory.GetCard(CardType.Spell, ElementType.Fire, String.Empty, -1, Guid.Empty, Guid.Empty); //ToDo fix negative damage
            var waterSpellCard = Models.Cards.CardFactory.GetCard(CardType.Spell, ElementType.Water, String.Empty, -1, Guid.Empty, Guid.Empty);
            var regularSpellCard = Models.Cards.CardFactory.GetCard(CardType.Spell, ElementType.Regular, String.Empty, -1, Guid.Empty, Guid.Empty);

            Assert.IsInstanceOf<FireSpellCard>(fireSpellCard);
            Assert.IsInstanceOf<WaterSpellCard>(waterSpellCard);
            Assert.IsInstanceOf<RegularSpellCard>(regularSpellCard);
        }

        [Test]
        public void TestGetDragonCard()
        {
            var validDragonCard = Models.Cards.CardFactory.GetCard(CardType.Monster, ElementType.NaE, "Dragon", 10, Guid.Empty, Guid.Empty);
            var invalidDragonCard = Models.Cards.CardFactory.GetCard(CardType.Monster, ElementType.Fire, "Dragon", 10, Guid.Empty, Guid.Empty); //Dragon has no Element

            Assert.IsInstanceOf<DragonCard>(validDragonCard);
            Assert.IsNull(invalidDragonCard);
        }

        [Test]
        public void TestGetAllElfCards()
        {
            var fireElf = Models.Cards.CardFactory.GetCard(CardType.Monster, ElementType.Fire, "Elf", 10, Guid.Empty, Guid.Empty);
            var waterElf = Models.Cards.CardFactory.GetCard(CardType.Monster, ElementType.Water, "Elf", 10, Guid.Empty, Guid.Empty);
            var regularElf = Models.Cards.CardFactory.GetCard(CardType.Monster, ElementType.Regular, "Elf", 10, Guid.Empty, Guid.Empty);

            Assert.IsInstanceOf<FireElfCard>(fireElf);
            Assert.IsInstanceOf<WaterElfCard>(waterElf);
            Assert.IsInstanceOf<RegularElfCard>(regularElf);
        }

        [Test]
        public void TestGetAllGoblinCards()
        {
            var fireGoblin = Models.Cards.CardFactory.GetCard(CardType.Monster, ElementType.Fire, "Goblin", 10, Guid.Empty, Guid.Empty);
            var waterGoblin = Models.Cards.CardFactory.GetCard(CardType.Monster, ElementType.Water, "Goblin", 10, Guid.Empty, Guid.Empty);
            var regularGoblin = Models.Cards.CardFactory.GetCard(CardType.Monster, ElementType.Regular, "Goblin", 10, Guid.Empty, Guid.Empty);

            Assert.IsInstanceOf<FireGoblinCard>(fireGoblin);
            Assert.IsInstanceOf<WaterGoblinCard>(waterGoblin);
            Assert.IsInstanceOf<RegularGoblinCard>(regularGoblin);
        }

        [Test]
        public void TestGetKnightCard()
        {
            var knight = Models.Cards.CardFactory.GetCard(CardType.Monster, ElementType.NaE, "Knight", 10, Guid.Empty, Guid.Empty);

            Assert.IsInstanceOf<KnightCard>(knight);
        }

        [Test]
        public void TestGetKrakenCard()
        {
            var kraken = Models.Cards.CardFactory.GetCard(CardType.Monster, ElementType.NaE, "Kraken", 10, Guid.Empty, Guid.Empty);

            Assert.IsInstanceOf<KrakenCard>(kraken);
        }

        [Test]
        public void TestGetOrkCard()
        {
            var ork = Models.Cards.CardFactory.GetCard(CardType.Monster, ElementType.NaE, "Ork", 10, Guid.Empty, Guid.Empty);

            Assert.IsInstanceOf<OrkCard>(ork);
        }

        [Test]
        public void TestGetAllTrollCards()
        {
            var fireTroll = Models.Cards.CardFactory.GetCard(CardType.Monster, ElementType.Fire, "Troll", 10, Guid.Empty, Guid.Empty);
            var waterTroll = Models.Cards.CardFactory.GetCard(CardType.Monster, ElementType.Water, "Troll", 10, Guid.Empty, Guid.Empty);
            var regularTroll = Models.Cards.CardFactory.GetCard(CardType.Monster, ElementType.Regular, "Troll", 10, Guid.Empty, Guid.Empty);

            Assert.IsInstanceOf<FireTrollCard>(fireTroll);
            Assert.IsInstanceOf<WaterTrollCard>(waterTroll);
            Assert.IsInstanceOf<RegularTrollCard>(regularTroll);
        }

        [Test]
        public void TestGetAllWizardCards()
        {
            var fireWizzard = Models.Cards.CardFactory.GetCard(CardType.Monster, ElementType.Fire, "Wizard", 10, Guid.Empty, Guid.Empty);
            var waterWizzard = Models.Cards.CardFactory.GetCard(CardType.Monster, ElementType.Water, "Wizard", 10, Guid.Empty, Guid.Empty);
            var regularWizzard = Models.Cards.CardFactory.GetCard(CardType.Monster, ElementType.Regular, "Wizard", 10, Guid.Empty, Guid.Empty);

            Assert.IsInstanceOf<FireWizardCard>(fireWizzard);
            Assert.IsInstanceOf<WaterWizardCard>(waterWizzard);
            Assert.IsInstanceOf<RegularWizardCard>(regularWizzard);
        }


    }
}
