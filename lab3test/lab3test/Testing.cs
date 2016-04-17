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

        [Test]
        // Тестирование конструктора по названию паттерна и коду
        public void CodeQuestionTest_ConstructorByPatternNameAndCode()
        {
            String code = "true != false";
            PatternName pattern = (PatternName)rand.Next(PatternsCount);
            CodeQuestion Q = new CodeQuestion(pattern, code);
            Assert.AreEqual(pattern, Q.CorrectAnswer);
            Assert.AreEqual(code, Q.Code);
        }

        [Test]
        // Тестирование создания вопроса из файла
        public void CodeQuestionTest_FromFile()
        {
            String path = "./CodeQuestionTest_FromFile";
            String code = "true != false";
            PatternName pattern = (PatternName)rand.Next(PatternsCount);
            bool flag = true; // флаг, что при создании файла не возникло ошибок
            try
            {
                File.Create(path).Close();  // создаем файл с заданным содержимым
                File.WriteAllText(path, code);
            }
            catch (System.Exception e)
            {
                flag = false; // если возникла ошибка, устанавливаем флаг в false
            }
            if (flag)   // если при создании файла не возникло ошибки, переходим к тесту
            {
                CodeQuestion Q = CodeQuestion.FromFile(pattern, path);
                Assert.AreEqual(pattern, Q.CorrectAnswer);
                Assert.AreEqual(code, Q.Code);
            }
        }

        [Test]
        // Тестирование распознавания правильного ответа
        public void CodeQuestionTest_CorrectAnswer()
        {
            PatternName pattern = (PatternName)rand.Next(PatternsCount);
            CodeQuestion Q = new CodeQuestion(pattern, "true != false");
            Assert.IsTrue(Q.TryAnswer(pattern));
        }

        [Test]
        // Тестирование распознавания неправильного ответа
        public void CodeQuestionTest_IncorrectAnswer()
        {
            PatternName pattern1, pattern2;                     // выбираем два несовпадающих паттерна
            pattern1 = (PatternName)rand.Next(PatternsCount);
            do
            {
                pattern2 = (PatternName)rand.Next(PatternsCount);
            } while (pattern2 == pattern1);
            CodeQuestion Q = new CodeQuestion(pattern1, "true != false");
            Assert.IsFalse(Q.TryAnswer(pattern2));
        }

        // Тестирование класса PictureQuestion

        [Test]
        // Тестирование конструктора по умолчанию
        public void PictureQuestionTest_DefaultConstructor()
        {
            PictureQuestion Q = new PictureQuestion();
            Assert.AreEqual(PatternName.NoPattern, Q.CorrectAnswer);
        }

        [Test]
        // Тестирование конструктора по названию паттерна
        public void PictureQuestionTest_ConstructorByPatternName()
        {
            PatternName pattern = (PatternName)rand.Next(PatternsCount);
            PictureQuestion Q = new PictureQuestion(pattern);
            Assert.AreEqual(pattern, Q.CorrectAnswer);
        }

        [Test]
        // Тестирование конструктора по названию паттерна и изображению
        public void PictureQuestionTest_ConstructorByPatternNameAndPicture()
        {
            PatternName pattern = (PatternName)rand.Next(PatternsCount);
            PictureQuestion Q = new PictureQuestion(pattern, picture1);
            Assert.AreEqual(pattern, Q.CorrectAnswer);
            Assert.AreEqual(picture1, Q.Picture);
        }

        [Test]
        // Тестирование загрузки вопроса из файла
        public void PictureQuestionTest_FromFile()
        {
            String path = "./PictureQuestionTest_FromFile";
            PatternName pattern = (PatternName)rand.Next(PatternsCount);
            bool flag = true; // флаг, что при создании файла не возникло ошибок
            try
            {
                picture1.Save(path); // сохраняем изображение в заданный файл
            }
            catch (System.Exception e)
            {
                flag = false; // если возникла ошибка, устанавливаем флаг в false
            }
            if (flag)   // если при создании файла не возникло ошибки, переходим к тесту
            {
                PictureQuestion Q = PictureQuestion.FromFile(pattern, path);
                Assert.AreEqual(pattern, Q.CorrectAnswer);
                Assert.IsNotNull(Q.Picture);
            }
        }

        [Test]
        // Тестирование распознавания правильного ответа
        public void PictureQuestionTest_CorrectAnswer()
        {
            PatternName pattern = (PatternName)rand.Next(PatternsCount);
            PictureQuestion Q = new PictureQuestion(pattern, picture1);
            Assert.IsTrue(Q.TryAnswer(pattern));
        }

        [Test]
        // Тестирование распознавания неправильного ответа
        public void PictureQuestionTest_IncorrectAnswer()
        {
            PatternName pattern1, pattern2;                     // выбираем два несовпадающих паттерна
            pattern1 = (PatternName)rand.Next(PatternsCount);
            do
            {
                pattern2 = (PatternName)rand.Next(PatternsCount);
            } while (pattern2 == pattern1);
            PictureQuestion Q = new PictureQuestion(pattern1, picture1);
            Assert.IsFalse(Q.TryAnswer(pattern2));
        }
        // Тестирование класса PatternsGame
        
        [Test]
        // Тестирование добавления произвольного вопроса 
        public void PatternsGameTest_AddQuestion()
        {
            PatternsGame Game = new PatternsGame();

            // добавляем случайное число вопросов
            int N = rand.Next(25);
            for (int i = 0; i < N; i++) {
                Game.AddQuestion(new Question());
            }

            Question Q = new Question();
            Game.AddQuestion(Q);
            Assert.AreEqual(Q, Game.Questions[Game.Questions.Count-1]);     // проверяем, что в конце списка последний добавленный вопрос
        }

        [Test]
        // Нельзя добавлять null
        public void PatternsGameTest_AddQuestionNull()
        {
            PatternsGame Game = new PatternsGame();

            // добавляем случайное число вопросов
            int N = rand.Next(25);
            for (int i = 0; i < N; i++)
            {
                Game.AddQuestion(new Question());
            }

            int count1 = Game.Questions.Count;  // количество вопросов до попытки добавить null
            Game.AddQuestion(null);
            int count2 = Game.Questions.Count;  // количество вопросов после попытки добавить null
            Assert.AreEqual(count1, count2); // должны совпадать
        }

        [Test]
        // Тестирование добавления вопроса по паттерну и коду
        public void PatternsGameTest_AddQuestionPatternAndCode()
        {
            PatternsGame Game = new PatternsGame();

            // добавляем случайное число вопросов
            int N = rand.Next(25);
            for (int i = 0; i < N; i++)
            {
                Game.AddQuestion(new Question());
            }

            PatternName pattern = (PatternName)rand.Next(PatternsCount);
            String code = "true != false";
            Game.AddQuestion(pattern, code);

            Assert.AreEqual(code, ((CodeQuestion)(Game.Questions[Game.Questions.Count - 1])).Code); // совпадает код
            Assert.AreEqual(pattern, Game.Questions[Game.Questions.Count - 1].CorrectAnswer); // совпадает паттерн
        }

        [Test]
        // Тестирование добавления вопроса по паттерну и изображению
        public void PatternsGameTest_AddQuestionPatternAndPicture()
        {
            PatternsGame Game = new PatternsGame();

            // добавляем случайное число вопросов
            int N = rand.Next(25);
            for (int i = 0; i < N; i++)
            {
                Game.AddQuestion(new Question());
            }

            PatternName pattern = (PatternName)rand.Next(PatternsCount);
            Game.AddQuestion(pattern, picture1);

            Assert.AreEqual(picture1, ((PictureQuestion)(Game.Questions[Game.Questions.Count - 1])).Picture); // совпадает изображение
            Assert.AreEqual(pattern, Game.Questions[Game.Questions.Count - 1].CorrectAnswer); // совпадает паттерн
        }
        [Test]
        // Пока игра не началась, текущий вопрос - null
        public void PatternsGameTest_GameOffCurrentQuestionNull()
        {
            PatternsGame Game = new PatternsGame();
            PatternName pattern = (PatternName)rand.Next(PatternsCount);
            Game.AddQuestion(pattern, "true != false");
            Assert.AreEqual(null, Game.CurrentQuestion());
     
       }
        [Test]
        // Игра начинается с первого вопроса
        public void PatternsGameTest_GameStartCurrentQuestionFirst()
        {
            PatternsGame Game = new PatternsGame();
            PatternName pattern = (PatternName)rand.Next(PatternsCount);
            Game.AddQuestion(pattern, "true != false");
            Game.AddQuestion(pattern, "false != true");
            Game.Start();
            Assert.AreEqual(Game.Questions[0], Game.CurrentQuestion());
        }
        [Test]
        // За первым вопросом следует второй
        public void PatternsGameTest_GameCurrentQuestionSecondAfterFirst()
        {
            PatternsGame Game = new PatternsGame();
            PatternName pattern = (PatternName)rand.Next(PatternsCount);
            Game.AddQuestion(pattern, "true != false");
            Game.AddQuestion(pattern, "false != true");
            Game.Start();
            Game.SetAnswer((PatternName)rand.Next(PatternsCount));
            Assert.AreEqual(Game.Questions[1], Game.CurrentQuestion());
        }
    }
}