﻿using System;
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

        public Testing()
        {
            rand = new Random();
            PatternsCount = Enum.GetValues(typeof(PatternName)).Length;
            picture1 = lab3test.Properties.Resources.picture1;
        }

        // Тестирование класса Question

        [Test]
        // Тестирование конструктора по умолчанию
        public void QuestionTest_DefaultConstructor()
        {
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


        [Test]
        // Тестирование распознавания правильного ответа
        public void QuestionTest_CorrectAnswer()
        {
            PatternName pattern = (PatternName)rand.Next(PatternsCount);
            Question Q = new Question(pattern);
            Assert.IsTrue(Q.TryAnswer(pattern));
        }

        [Test]
        // Тестирование распознавания неправильного ответа
        public void QuestionTest_IncorrectAnswer()
        {
            PatternName pattern1, pattern2;                     // выбираем два несовпадающих паттерна
            pattern1 = (PatternName)rand.Next(PatternsCount);
            do
            {
                pattern2 = (PatternName)rand.Next(PatternsCount);
            } while (pattern2 == pattern1);
            Question Q = new Question(pattern1);
            Assert.IsFalse(Q.TryAnswer(pattern2));
        }

        // Тестирование класса CodeQuestion

        [Test]
        // Тестирование конструктора по умолчанию
        public void CodeQuestionTest_DefaultConstructor()
        {
            CodeQuestion Q = new CodeQuestion();
            Assert.AreEqual(PatternName.NoPattern, Q.CorrectAnswer);
        }

        [Test]
        // Тестирование конструктора по названию паттерна
        public void CodeQuestionTest_ConstructorByPatternName()
        {
            PatternName pattern = (PatternName)rand.Next(PatternsCount);
            CodeQuestion Q = new CodeQuestion(pattern);
            Assert.AreEqual(pattern, Q.CorrectAnswer);
        }

       
       
       

    }

}