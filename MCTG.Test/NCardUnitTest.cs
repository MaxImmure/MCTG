namespace MCTG.Test
{
    public class Tests
    {

        private Dictionary<string, ICard> _cards;

        [SetUp]
        public void Setup()
        {
            _cards = new Dictionary<string, ICard>();
            int dmg = 30;
            _cards.Add("empty", EmptyCard.getInstance());
            _cards.Add("firespell", new FireSpellCard(dmg));
            _cards.Add("normalspell", new NormalSpellCard(dmg));
            _cards.Add("waterspell", new WaterSpellCard(dmg));
        }

        [Test]
        [TestCase("troll", "goblin", ExpectedResult = _cards["troll"])]
        [TestCase("goblin", "troll", "troll")]
        public void TestAnyMethod(ICard c1, ICard c2) {}

        [Test]
        public void TestGoblinDragonFight() //TestMax_ShouldBeXWhenXGreaterY()
        {
            // AAA - pattern
            // Arrange

            ICard testCard1, testCard2;
            testCard1 = new FireGoblinCard(20.0);
            testCard2 = new WaterDragonCard(30.0);

            // Act
            double dmgGob = testCard1.getDamage(testCard2);
            double dmgDragon = testCard2.getDamage(testCard1);

            // Assert
            Assert.Zero(dmgGob);
            Assert.IsTrue(dmgDragon == 30);
        }
        public void TestFireOrkWaterSpellFight() //TestMax_ShouldBeXWhenXGreaterY()
        {
            // AAA - pattern
            // Arrange
            ICard testCard1, testCard2;
            testCard1 = new FireOrkCard(20.0);
            testCard2 = new WaterSpellCard(30.0);

            // Act
            double dmgFireOrk = testCard1.getDamage(testCard2);
            double dmgWaterSpell = testCard2.getDamage(testCard1);

            // Assert
            Assert.IsTrue(dmgFireOrk == 10.0);
            Assert.IsTrue(dmgWaterSpell == 60.0);
        }

        [Test]
        public void TestMonsterFightBattle() //TestMax_ShouldBeXWhenXGreaterY()
        {
            //AAA
            // Arrange
            WaterGoblinCard gob = new WaterGoblinCard(10);
            FireTrollCard troll = new FireTrollCard(15);

            // Act
            ICard fight1 = Battle.simulateCardFight(gob, troll),
                fight2 = Battle.simulateCardFight(troll, gob);

            // Assert
            Assert.That(troll, Is.EqualTo(fight1));
            Assert.That(troll, Is.EqualTo(fight2));
        }

    }
}