using MCTG.BL;
using MCTG.Models;
using MCTG.Models.Cards.MonsterCards;
using MCTG.Models.Cards.SpellCards;
using MCTG.Models.Cards;

namespace MCTG.Test
{
    internal class FullBattle
    {

        

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void WinnerP1()
        {
            Battle b = new Battle();
            var g1 = Guid.NewGuid();
            User p1 = new User()
            {
                Guid = g1
                , Username = "P1"
                , Deck = new List<ICard>
                {
                    new KnightCard(Guid.NewGuid(), g1, 100)
                    , new KnightCard(Guid.NewGuid(), g1, 100)
                    , new KnightCard(Guid.NewGuid(), g1, 100)
                    , new KnightCard(Guid.NewGuid(), g1, 100)
                }.ToArray()
                , GameStats = new Stats()
            };
            var g2 = Guid.NewGuid();
            User p2 = new User()
            {
                Guid = g2
                , Username = "P2"
                , Deck = new List<ICard>
                {
                    new KnightCard(Guid.NewGuid(), g2, 10)
                    , new KnightCard(Guid.NewGuid(), g2, 10)
                    , new KnightCard(Guid.NewGuid(), g2, 10)
                    , new KnightCard(Guid.NewGuid(), g2, 10)
                }.ToArray()
                , GameStats = new Stats()
            };

            b.startBattle(ref p1, ref p2);


            // Assert
            Assert.IsTrue(p1.GameStats.elo == 103);
            Assert.IsTrue(p1.GameStats.wins == 1);
            Assert.IsTrue(p1.GameStats.draws == 0);
            Assert.IsTrue(p1.GameStats.looeses == 0);

            Assert.IsTrue(p2.GameStats.elo == 95);
            Assert.IsTrue(p2.GameStats.wins == 0);
            Assert.IsTrue(p2.GameStats.draws == 0);
            Assert.IsTrue(p2.GameStats.looeses == 1);
        }

        [Test]
        public void WinnerP2()
        {
            Battle b = new Battle();
            var g1 = Guid.NewGuid();
            User p1 = new User()
            {
                Guid = g1
                ,
                Username = "P1"
                ,
                Deck = new List<ICard>
                {
                    new KnightCard(Guid.NewGuid(), g1, 100)
                    , new KnightCard(Guid.NewGuid(), g1, 100)
                    , new KnightCard(Guid.NewGuid(), g1, 100)
                    , new KnightCard(Guid.NewGuid(), g1, 100)
                }.ToArray()
                ,
                GameStats = new Stats()
            };
            var g2 = Guid.NewGuid();
            User p2 = new User()
            {
                Guid = g2
                ,
                Username = "P2"
                ,
                Deck = new List<ICard>
                {
                    new WaterSpellCard(Guid.NewGuid(), g2, 1)
                    , new WaterSpellCard(Guid.NewGuid(), g2, 1)
                    , new WaterSpellCard(Guid.NewGuid(), g2, 1)
                    , new WaterSpellCard(Guid.NewGuid(), g2, 1)
                }.ToArray()
                ,
                GameStats = new Stats()
            };

            b.startBattle(ref p1, ref p2);


            // Assert
            Assert.IsTrue(p1.GameStats.elo == 95);
            Assert.IsTrue(p1.GameStats.wins == 0);
            Assert.IsTrue(p1.GameStats.draws == 0);
            Assert.IsTrue(p1.GameStats.looeses == 1);

            Assert.IsTrue(p2.GameStats.elo == 103);
            Assert.IsTrue(p2.GameStats.wins == 1);
            Assert.IsTrue(p2.GameStats.draws == 0);
            Assert.IsTrue(p2.GameStats.looeses == 0);
        }

        [Test]
        public void NoWinner_eqDraw()
        {
            Battle b = new Battle();
            var g1 = Guid.NewGuid();
            User p1 = new User()
            {
                Guid = g1
                ,
                Username = "P1"
                ,
                Deck = new List<ICard>
                {
                    new KnightCard(Guid.NewGuid(), g1, 100)
                    , new KnightCard(Guid.NewGuid(), g1, 100)
                    , new KnightCard(Guid.NewGuid(), g1, 100)
                    , new KnightCard(Guid.NewGuid(), g1, 100)
                }.ToArray()
                ,
                GameStats = new Stats()
            };
            var g2 = Guid.NewGuid();
            User p2 = new User()
            {
                Guid = g2
                ,
                Username = "P2"
                ,
                Deck = new List<ICard>
                {
                    new KnightCard(Guid.NewGuid(), g2, 100)
                    , new KnightCard(Guid.NewGuid(), g2, 100)
                    , new KnightCard(Guid.NewGuid(), g2, 100)
                    , new KnightCard(Guid.NewGuid(), g2, 100)
                }.ToArray()
                ,
                GameStats = new Stats()
            };

            b.startBattle(ref p1, ref p2);


            // Assert
            Assert.IsTrue(p1.GameStats.elo == 100);
            Assert.IsTrue(p1.GameStats.wins == 0);
            Assert.IsTrue(p1.GameStats.draws == 1);
            Assert.IsTrue(p1.GameStats.looeses == 0);

            Assert.IsTrue(p2.GameStats.elo == 100);
            Assert.IsTrue(p2.GameStats.wins == 0);
            Assert.IsTrue(p2.GameStats.draws == 1);
            Assert.IsTrue(p2.GameStats.looeses == 0);
        }


    }
}