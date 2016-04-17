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
        [Test]
        // Выбор правильного ответа
        public void PatternsGameTest_SetAnswerCorrect()
        {
            PatternsGame Game = new PatternsGame();
            PatternName pattern = (PatternName)rand.Next(PatternsCount);
            Game.AddQuestion(pattern, "true != false");
            Game.Start();
            Game.SetAnswer(pattern);
            Assert.AreEqual(1, Game.CorrectAnswers);
            Assert.AreEqual(0, Game.IncorrectAnswers);
        }

        [Test]
        // Выбор неправильного ответа
        public void PatternsGameTest_SetAnswerIncorrect()
        {
            PatternsGame Game = new PatternsGame();
            PatternName pattern1 = (PatternName)rand.Next(PatternsCount), pattern2;     // выбираем два несовпадающих паттерна
            do
            {
                pattern2 = (PatternName)rand.Next(PatternsCount);
            } while (pattern2 == pattern1);
            Game.AddQuestion(pattern1, "true != false");
            Game.Start();
            Game.SetAnswer(pattern2);
            Assert.AreEqual(0, Game.CorrectAnswers);
            Assert.AreEqual(1, Game.IncorrectAnswers);
        }
        [Test]
        // После выбора ответа происходит переход к следующему вопросу
        public void PatternsGameTest_SetAnswerOpensNextQuestion()
        {
            PatternsGame Game = new PatternsGame();
            PatternName pattern = (PatternName)rand.Next(PatternsCount);
            Game.AddQuestion(pattern, "true != false");
            Game.AddQuestion(pattern, "false != true");
            Game.Start();
            Game.SetAnswer(pattern);
            Assert.AreEqual(Game.Questions[1], Game.CurrentQuestion());
        }

        [Test]
        // Варианты ответа содержат правильный
        public void PatternsGameTest_AnswerVariantsContainCorrect()
        {
            PatternsGame Game = new PatternsGame();
            PatternName pattern = (PatternName)rand.Next(PatternsCount);
            Game.AddQuestion(pattern, "true != false");
            Game.Start();
            List<PatternName> variants = Game.AnswerVariants(4);
            Assert.IsTrue(variants.Contains(pattern));
        }

        [Test]
        // Варианты ответа не повторяются
        public void PatternsGameTest_AnswerVariantsDoNotRepeat()
        {
            PatternsGame Game = new PatternsGame();
            PatternName pattern = (PatternName)rand.Next(PatternsCount);
            Game.AddQuestion(pattern, "true != false");
            Game.Start();
            List<PatternName> variants = Game.AnswerVariants(4);
            bool flag = true; // флаг, что не встретились совпадающие
            for (int i = 0; i < variants.Count; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (variants[i] == variants[j]) flag = false; // попарно сравниваем
                }
            }
            Assert.IsTrue(flag);
        }

        [Test]
        // Варианты ответа генерируются в требуемом количестве, но не больше, чем количество паттернов в перечислении PatternName
        public void PatternsGameTest_AnswerVariantsCountIsCorrect()
        {
            PatternsGame Game = new PatternsGame();
            PatternName pattern = (PatternName)rand.Next(PatternsCount);
            Game.AddQuestion(pattern, "true != false");
            Game.Start();
            int varcount = rand.Next(PatternsCount); // число вариантов ответа
            List<PatternName> variants = Game.AnswerVariants(varcount);
            Assert.AreEqual(varcount, variants.Count);
        }

        [Test]
        // Функция GetQuestionsCount возвращает количество вопросов
        public void PatternsGameTest_GetQuestionsCount()
        {
            PatternsGame Game = new PatternsGame();
            PatternName pattern = (PatternName)rand.Next(PatternsCount);

            // добавляем случайное количество вопросов
            int qcount = rand.Next(20);
            for (int i = 0; i < qcount; i++)
            {
                Game.AddQuestion(pattern, "true != false");
            }

            Assert.AreEqual(qcount, Game.GetQuestionsCount());
        }

        [Test]
        // Функция GetCorrectAnswers возвращает количество правильных ответов
        public void PatternsGameTest_GetCorrectAnswers()
        {
            PatternsGame Game = new PatternsGame();
            PatternName pattern1 = (PatternName)rand.Next(PatternsCount), pattern2;     // генерируем два несовпадающих паттерна
            do
            {
                pattern2 = (PatternName)rand.Next(PatternsCount);
            } while (pattern2 == pattern1);

            // генерируем случайное количество вопросов с одинаковым правильным ответом
            int qcount = rand.Next(20);
            for (int i = 0; i < qcount; i++)
            {
                Game.AddQuestion(pattern1, "true != false");
            }

            // начинаем игру
            Game.Start();
            int correct = 0;
            int incorrect = 0;
            for (int i = 0; i < qcount; i++)
            {
                if (rand.Next() % 2 == 1)
                {
                    Game.SetAnswer(pattern1); // отвечаем правильно и увеличиваем счетчик правильных ответов
                    correct++;
                }
                else
                {
                    Game.SetAnswer(pattern2); // отвечаем неправильно и увеличиваем счетчик неправильных ответов
                    incorrect++;
                }
            }

            Assert.AreEqual(correct, Game.GetCorrectAnswers());
        }

        [Test]
        // Функция GetInorrectAnswers возвращает количество неправильных ответов
        public void PatternsGameTest_GetIncorrectAnswers()
        {
            PatternsGame Game = new PatternsGame();
            PatternName pattern1 = (PatternName)rand.Next(PatternsCount), pattern2;     // генерируем два несовпадающих паттерна
            do
            {
                pattern2 = (PatternName)rand.Next(PatternsCount);
            } while (pattern2 == pattern1);

            // генерируем случайное количество вопросов с одинаковым правильным ответом
            int qcount = rand.Next(20);
            for (int i = 0; i < qcount; i++)
            {
                Game.AddQuestion(pattern1, "true != false");
            }

            // начинаем игру
            Game.Start();
            int correct = 0;
            int incorrect = 0;
            for (int i = 0; i < qcount; i++)
            {
                if (rand.Next() % 2 == 1)
                {
                    Game.SetAnswer(pattern1); // отвечаем правильно и увеличиваем счетчик правильных ответов
                    correct++;
                }
                else
                {
                    Game.SetAnswer(pattern2); // отвечаем неправильно и увеличиваем счетчик неправильных ответов
                    incorrect++;
                }
            }

            Assert.AreEqual(incorrect, Game.GetIncorrectAnswers());
        }

        [Test]
        // Функция GetCounter возвращает номер текущего вопроса
        public void PatternsGameTest_GetCounter()
        {
            PatternsGame Game = new PatternsGame();
            PatternName pattern = (PatternName)rand.Next(PatternsCount);

            // добавляем случайное количество вопросов
            int qcount = rand.Next(20);
            for (int i = 0; i < qcount; i++)
            {
                Game.AddQuestion(pattern, "true != false");
            }
            Game.Start();

            // отвечаем на случайное количество вопросов
            int count = rand.Next(qcount);
            for (int i = 0; i < count; i++)
            {
                Game.SetAnswer(pattern);
            }

            Assert.AreEqual(count, Game.GetCounter());
        }

        [Test]
        // Функция NewGame обнуляет счетчик
        public void PatternsGameTest_NewGameCounter()
        {
            PatternsGame Game = new PatternsGame();
            PatternName pattern1 = (PatternName)rand.Next(PatternsCount), pattern2; // выбираем два несовпадающих паттерна
            do
            {
                pattern2 = (PatternName)rand.Next(PatternsCount);
            } while (pattern2 == pattern1);

            // генерируем случайное число вопросов с одинаковым правильным ответом
            int qcount = rand.Next(20);
            for (int i = 0; i < qcount; i++)
            {
                Game.AddQuestion(pattern1, "true != false");
            }

            // проходим игру, делая случайное количество правильных и неправильных ответов
            Game.Start();
            for (int i = 0; i < qcount; i++)
            {
                if (rand.Next() % 2 == 1)
                {
                    Game.SetAnswer(pattern1);
                }
                else
                {
                    Game.SetAnswer(pattern2);
                }
            }

            // сбрасываем параметры игры
            Game.NewGame();

            Assert.AreEqual(0, Game.Counter);
        }

        [Test]
        // Функция NewGame обнуляет правильные ответы
        public void PatternsGameTest_NewGameCorrectAnswers()
        {
            PatternsGame Game = new PatternsGame();
            PatternName pattern1 = (PatternName)rand.Next(PatternsCount), pattern2; // выбираем два несовпадающих паттерна
            do
            {
                pattern2 = (PatternName)rand.Next(PatternsCount);
            } while (pattern2 == pattern1);

            // генерируем случайное число вопросов с одинаковым правильным ответом
            int qcount = rand.Next(20);
            for (int i = 0; i < qcount; i++)
            {
                Game.AddQuestion(pattern1, "true != false");
            }

            // проходим игру, делая случайное количество правильных и неправильных ответов
            Game.Start();
            for (int i = 0; i < qcount; i++)
            {
                if (rand.Next() % 2 == 1)
                {
                    Game.SetAnswer(pattern1);
                }
                else
                {
                    Game.SetAnswer(pattern2);
                }
            }

            // сбрасываем параметры игры
            Game.NewGame();

            Assert.AreEqual(0, Game.CorrectAnswers);
        }

        [Test]
        // Функция NewGame обнуляет неправильные ответы
        public void PatternsGameTest_NewGameIncorrectAnswers()
        {
            PatternsGame Game = new PatternsGame();
            PatternName pattern1 = (PatternName)rand.Next(PatternsCount), pattern2; // выбираем два несовпадающих паттерна
            do
            {
                pattern2 = (PatternName)rand.Next(PatternsCount);
            } while (pattern2 == pattern1);

            // генерируем случайное число вопросов с одинаковым правильным ответом
            int qcount = rand.Next(20);
            for (int i = 0; i < qcount; i++)
            {
                Game.AddQuestion(pattern1, "true != false");
            }

            // проходим игру, делая случайное количество правильных и неправильных ответов
            Game.Start();
            for (int i = 0; i < qcount; i++)
            {
                if (rand.Next() % 2 == 1)
                {
                    Game.SetAnswer(pattern1);
                }
                else
                {
                    Game.SetAnswer(pattern2);
                }
            }

            // сбрасываем параметры игры
            Game.NewGame();

            Assert.AreEqual(0, Game.IncorrectAnswers);
        }

        [Test]
        // Функция IsOn возвращает false, если игра не началась
        public void PatternsGameTest_IsOnNotStarted()
        {
            PatternsGame Game = new PatternsGame();
            Assert.IsFalse(Game.IsOn());
        }

        [Test]
        // Функция IsOn возвращает true, если игра началась
        public void PatternsGameTest_IsOnStarted()
        {
            PatternsGame Game = new PatternsGame();
            Game.Start();
            Assert.IsTrue(Game.IsOn());
        }

        [Test]
        // Функция IsOn возвращает false, если игра завершена
        public void PatternsGameTest_IsOnStopped()
        {
            PatternsGame Game = new PatternsGame();
            Game.Start();
            Game.Stop();
            Assert.IsFalse(Game.IsOn());
        }

        [Test]
        // Метод Start начинает игру
        public void PatternsGameTest_Start()
        {
            PatternsGame Game = new PatternsGame();
            Game.On = false;
            Game.Start();
            Assert.IsTrue(Game.IsOn());
        }

        [Test]
        // Метод Stop завершает игру
        public void PatternsGameTest_Stop()
        {
            PatternsGame Game = new PatternsGame();
            Game.On = true;
            Game.Stop();
            Assert.IsFalse(Game.IsOn());
        }
    }
}