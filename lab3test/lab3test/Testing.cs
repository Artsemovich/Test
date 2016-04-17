using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace lab3test
{
    [TestFixture]

    class Testing
    {
        Image picture1;
        Random rand;
        int PatternsCount;

        public Testing() {
            rand = new Random();
            PatternsCount = Enum.GetValues(typeof(PatternName)).Length;
            picture1 = lab3test.Properties.Resources.picture1;
        }

        // Тестирование класса Question

        [Test]
        // Тестирование конструктора по умолчанию
        public void QuestionTest_DefaultConstructor() { 
            Question Q = new Question();
            Assert.AreEqual(PatternName.NoPattern, Q.CorrectAnswer);
        }

        [Test]
        // Тестирование конструктора по идентификатору паттерна
        public void QuestionTest_ConstructorByPattern()
        {
            PatternName pattern = (PatternName)rand.Next(PatternsCount);
            Question Q = new Question(pattern);
            Assert.AreEqual(pattern, Q.CorrectAnswer);
        }

        
    }
}