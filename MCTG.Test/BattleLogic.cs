using MCTG.BL;
using MCTG.Models.Cards.MonsterCards;
using MCTG.Models.Cards.SpellCards;
using MCTG.Models.Cards;

namespace MCTG.Test
{
    internal class BattleLogic
    {

        private Dictionary<string, ICard> _cards; //ToDo remove or implement

        [SetUp]
        public void Setup()
        {
            _cards = new Dictionary<string, ICard>();
            int dmg = 30;
            _cards.Add("empty", EmptyCard.Instance());
            _cards.Add("firespell", new FireSpellCard(Guid.NewGuid(), Guid.Empty, dmg));
            _cards.Add("regularspell", new RegularSpellCard(Guid.NewGuid(), Guid.Empty, dmg));
            _cards.Add("waterspell", new WaterSpellCard(Guid.NewGuid(), Guid.Empty, dmg));
        }

        [Test]
        public void TestGoblinDragonFight()
        {
            // AAA - pattern
            // Arrange

            ICard testCard1, testCard2;
            testCard1 = new FireGoblinCard(Guid.NewGuid(), Guid.Empty, 20.0);
            testCard2 = new DragonCard(Guid.NewGuid(), Guid.Empty, 30.0);

            // Act
            double dmgGob = testCard1.GetDamage(testCard2);
            double dmgDragon = testCard2.GetDamage(testCard1);

            // Assert
            Assert.Zero(dmgGob);
            Assert.IsTrue(dmgDragon == 30);
        }

        [Test]
        public void TestOrkWaterSpellFight()
        {
            // AAA - pattern
            // Arrange
            ICard testCard1, testCard2;
            testCard1 = new OrkCard(Guid.NewGuid(), Guid.Empty, 20.0);
            testCard2 = new WaterSpellCard(Guid.NewGuid(), Guid.Empty, 30.0);

            // Act
            double dmgFireOrk = testCard1.GetDamage(testCard2);
            double dmgWaterSpell = testCard2.GetDamage(testCard1);

            // Assert
            Assert.IsTrue(dmgFireOrk == 40.0);
            Assert.IsTrue(dmgWaterSpell == 15.0);
        }

        [Test]
        public void TestFireGoblinWaterSpellFight()
        {
            // AAA - pattern
            // Arrange
            ICard testCard1, testCard2;
            testCard1 = new FireGoblinCard(Guid.NewGuid(), Guid.Empty, 20.0);
            testCard2 = new WaterSpellCard(Guid.NewGuid(), Guid.Empty, 30.0);

            // Act
            double dmgFireOrk = testCard1.GetDamage(testCard2);
            double dmgWaterSpell = testCard2.GetDamage(testCard1);

            // Assert
            Assert.IsTrue(dmgFireOrk == 10.0);
            Assert.IsTrue(dmgWaterSpell == 60.0);
        }

        [Test]
        public void TestMonsterFightBattle()
        {
            //AAA
            // Arrange
            WaterGoblinCard gob = new WaterGoblinCard(Guid.NewGuid(), Guid.Empty, 10);
            FireTrollCard troll = new FireTrollCard(Guid.NewGuid(), Guid.Empty, 15);

            // Act
            ICard fight1 = Battle.simulateCardFight(gob, troll),
                fight2 = Battle.simulateCardFight(troll, gob);

            // Assert
            Assert.That(troll, Is.EqualTo(fight1));
            Assert.That(troll, Is.EqualTo(fight2));
        }

        [Test]
        public void TestSpellFightBattle()
        {
            FireSpellCard fireSpell = new(Guid.NewGuid(), Guid.Empty, 10);
            WaterSpellCard waterSpell = new(Guid.NewGuid(), Guid.Empty, 20);

            FireSpellCard fireSpell1 = new (Guid.NewGuid(), Guid.Empty, 20);
            WaterSpellCard waterSpell1 = new(Guid.NewGuid(), Guid.Empty, 5);

            FireSpellCard fireSpell2 = new (Guid.NewGuid(), Guid.Empty, 90);

            ICard fight1 = Battle.simulateCardFight(fireSpell, waterSpell),
                fight2 = Battle.simulateCardFight(fireSpell1, waterSpell1),
                fight3 = Battle.simulateCardFight(fireSpell2, waterSpell1);

            Assert.That(waterSpell, Is.EqualTo(fight1));
            Assert.That(EmptyCard.Instance(), Is.EqualTo(fight2));
            Assert.That(fireSpell2, Is.EqualTo(fight3));
        }

        [Test]
        public void TestMixedBattle()
        {
            FireSpellCard fs = new(Guid.NewGuid(), Guid.Empty, 10);
            WaterSpellCard ws = new(Guid.NewGuid(), Guid.Empty, 10);
            RegularSpellCard rs = new(Guid.NewGuid(), Guid.Empty, 10);

            WaterGoblinCard watergoblin = new(Guid.NewGuid(), Guid.Empty, 10);
            KnightCard knight = new(Guid.NewGuid(), Guid.Empty, 15);

            ICard fight1 = Battle.simulateCardFight(fs, watergoblin),
                fight2 = Battle.simulateCardFight(ws, watergoblin),
                fight3 = Battle.simulateCardFight(rs, watergoblin),
                fight4 = Battle.simulateCardFight(rs, knight);

            Assert.That(watergoblin, Is.EqualTo(fight1));
            Assert.That(EmptyCard.Instance(), Is.EqualTo(fight2));
            Assert.That(rs, Is.EqualTo(fight3));
            Assert.That(knight, Is.EqualTo(fight4));
        }
    }
}