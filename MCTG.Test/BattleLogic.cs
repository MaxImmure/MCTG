using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCTG.DAL;
using MCTG.Models;
using MCTG.Models.Cards.MonsterCards;
using MCTG.Models.Cards.SpellCards;
using MCTG.Models.Cards;

namespace MCTG.Test
{
    internal class BattleLogic
    {

        private Dictionary<string, ICard> _cards;

        [SetUp]
        public void Setup()
        {
            _cards = new Dictionary<string, ICard>();
            int dmg = 30;
            _cards.Add("empty", EmptyCard.Instance());
            _cards.Add("firespell", new FireSpellCard(dmg));
            _cards.Add("regularspell", new RegularSpellCard(dmg));
            _cards.Add("waterspell", new WaterSpellCard(dmg));
        }

        [Test]
        public void TestGoblinDragonFight() //TestMax_ShouldBeXWhenXGreaterY()
        {
            // AAA - pattern
            // Arrange

            ICard testCard1, testCard2;
            testCard1 = new FireGoblinCard(20.0);
            testCard2 = new DragonCard(30.0);

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
            testCard1 = new OrkCard(20.0);
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