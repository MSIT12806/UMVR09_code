using Microsoft.VisualStudio.TestTools.UnitTesting;
using Console2048;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2048.Tests
{
    [TestClass()]
    public class FightCharacterTests
    {
        FightCharacter playerTest = new Player();
        FightCharacter giantTest = new Giant();
        Random random = new Random();
        [TestMethod()]
        public void GetHurtTest()
        {
            int v = random.Next();            
            int r = playerTest.Hp - v;
            playerTest.GetHurt(v);
            Assert.AreEqual(r, playerTest.Hp);
        }
    }
    [TestClass()]
    public class PlayerTests
    {
        Player playerTest = new Player();
        Random random = new Random();
        #region SetFightPropertyTest
        [TestMethod()]
        public void PowerSkillTest()
        {
            playerTest.Power = 35;
            playerTest.SetFightProperty();
            Assert.IsTrue( playerTest.Skills.Keys.Contains("勢如破竹"));
        }
        [TestMethod()]
        public void EnduranceSkillTest()
        {
            playerTest.Endurance = 35;
            playerTest.SetFightProperty();
            Assert.IsTrue( playerTest.Skills.Keys.Contains("血牛爆發"));
        }
        [TestMethod()]
        public void AgileSkillTest()
        {
            playerTest.Agile = 35;
            playerTest.SetFightProperty();
            Assert.IsTrue( playerTest.Skills.Keys.Contains("暗隱伏擊"));
        }
        [TestMethod()]
        public void SwordSkillTest()
        {
            playerTest.SwordPoint = 36;
            playerTest.SetFightProperty();
            Assert.IsTrue( playerTest.Skills.Keys.Contains("天崩地裂斬"));
            Assert.IsTrue( playerTest.Skills.Keys.Contains("以劍代盾"));
            playerTest.Skills.Clear();
            playerTest.SwordPoint = 36;
            playerTest.ShieldPoint = 21;
            playerTest.SetFightProperty();
            Assert.IsTrue(playerTest.Skills.Keys.Contains("精良狀態"));
        }
        #endregion

    }
}