using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3test
{
    // Перечисление паттернов. NoPattern - отсутствие паттерна.
    public enum PatternName
    {
        NoPattern,
        Delegation, DelegationEventModel, Proxy,
        Adapter, Decorator, Bridge, Composite, Flyweight, Iterator, InformationExpert,
        Factory, FactoryMethod, AbstractFactory, Prototype, Builder, Singleton, ObjectPool,
        State, Observer, Memento, Command, Visitor, Indirection, ChainOfResponsibility, Mediator,
        Strategy, Interpreter, TemplateMethod, Controller, PureFabrication, DontTalkToStrangers,
        MVC
    };

    // Вопрос. От этого класса наследуются CodeQuestion и PictureQuestion
    public class Question
    {
        public PatternName CorrectAnswer; // Правильный ответ

        // Конструктор по умолчанию
        public Question()
        {
            CorrectAnswer = PatternName.NoPattern;
        }

        // Конструктор вопроса с заданным правильным ответом
        public Question(PatternName pattern)
        {
            CorrectAnswer = pattern;
        }

        // Проверка, правильный ли выбран ответ
        public bool TryAnswer(PatternName Answer)
        {
            if (CorrectAnswer == Answer) return true;
            return false;
        }
    };

    // Вопрос "угадать паттерн по коду"
    public class CodeQuestion : Question
    {
        public String Code; // Код программы

        // Конструктор по умолчанию
        public CodeQuestion()
        {
            CorrectAnswer = PatternName.NoPattern;
            Code = "";
        }
        // Конструктор, создает вопрос с заданным правильным ответом
        public CodeQuestion(PatternName pattern)
        {
            CorrectAnswer = pattern;
            Code = "";
        }
        // Конструктор, создает вопрос с заданным правильным ответом и кодом программы
        public CodeQuestion(PatternName pattern, String pattern_code)
        {
            CorrectAnswer = pattern;
            Code = pattern_code;
        }
        // Статический метод, возвращает вопрос с заданным правильным ответом, код берет из файла
        public static CodeQuestion FromFile(PatternName pattern, String FileName)
        {
            try
            {
                CodeQuestion Q = new CodeQuestion(pattern, File.ReadAllText(FileName));
                return Q;
            }
            catch (System.Exception e)
            {
                return null; // Если не получилось вытащить код из файла, возвращаем null
            }
        }
    };

    // Вопрос "угадать паттерн по изображению"
    public class PictureQuestion : Question
    {
        public Image Picture; // Изображение (UML-диаграмма)

        // Конструктор по умолчанию
        public PictureQuestion()
        {
            CorrectAnswer = PatternName.NoPattern;
            Picture = null;
        }
        // Конструктор, создает вопрос с заданным правильным ответом
        public PictureQuestion(PatternName pattern)
        {
            CorrectAnswer = pattern;
            Picture = null;
        }
        // Конструктор, создает вопрос с заданным правильным ответом и изображением
        public PictureQuestion(PatternName pattern, Image pattern_picture)
        {
            CorrectAnswer = pattern;
            Picture = pattern_picture;
        }
        // Статический метод, возвращает вопрос с заданным правильным ответом, изображение берет из файла
        public static PictureQuestion FromFile(PatternName pattern, String FileName)
        {
            try
            {
                PictureQuestion Q = new PictureQuestion(pattern, Image.FromFile(FileName));
                return Q;
            }
            catch (System.Exception e)
            {
                return null; // Если не получилось вытащить изображение из файла, возвращаем null
            }
        }
    }
    public class PatternsGame
    {
        public List<Question> Questions; // Список вопросов игры
        public int CorrectAnswers; // Количество правильных ответов, выбранных игроком
        public int IncorrectAnswers; // Количество неправильных ответов, выбранных игроком
        public int Counter; // Номер текущего вопроса
        public bool On; // Флаг, что идёт игра

        // Статический метод, возвращает название паттерна
        public static String PatternString(PatternName pattern)
        {
            switch (pattern)
            {
                case PatternName.Delegation:
                    return "Делегирование";
                case PatternName.DelegationEventModel:
                    return "Модель на делегатах";
                case PatternName.Proxy:
                    return "Прокси";
                case PatternName.Adapter:
                    return "Адаптер";
                case PatternName.Decorator:
                    return "Декторатор";
                    break;
                case PatternName.Bridge:
                    return "Мост";
                case PatternName.Composite:
                    return "Компоновщик";
                case PatternName.Flyweight:
                    return "Приспоспобленец";
                case PatternName.Iterator:
                    return "Итератор";
                case PatternName.InformationExpert:
                    return "Информационный эксперт";
                case PatternName.Factory:
                    return "Фабрика";
                case PatternName.FactoryMethod:
                    return "Фабричный метод";
                case PatternName.AbstractFactory:
                    return "Абстрактная фабрика";
                case PatternName.Prototype:
                    return "Прототип";
                case PatternName.Builder:
                    return "Строитель";
                case PatternName.Singleton:
                    return "Одиночка";
                case PatternName.ObjectPool:
                    return "Пул объектов";
                case PatternName.State:
                    return "Состояние";
                case PatternName.Observer:
                    return "Наблюдатель";
                case PatternName.Memento:
                    return "Хранитель";
                case PatternName.Command:
                    return "Команда";
                case PatternName.Visitor:
                    return "Посетитель";
                case PatternName.Indirection:
                    return "Перенаправление";
                case PatternName.ChainOfResponsibility:
                    return "Цепочка обязанностей";
                case PatternName.Mediator:
                    return "Посредник";
                case PatternName.Strategy:
                    return "Стратегия";
                case PatternName.Interpreter:
                    return "Интерпретатор";
                case PatternName.TemplateMethod:
                    return "Шаблонный метод";
                case PatternName.Controller:
                    return "Контроллер";
                case PatternName.PureFabrication:
                    return "Искусственный";
                case PatternName.DontTalkToStrangers:
                    return "Не разговаривайте с неизвестными";
                case PatternName.MVC:
                    return "MVC";
                case PatternName.NoPattern:
                    return "Паттернов нет";
            }
            return "";
        }

        // Конструктор по умолчанию
        public PatternsGame()
        {
            ;
        }

        // Добавляет вопрос к списку вопросов игры
        public void AddQuestion(Question q)
        {
            ;
        }

        // Создает вопрос типа "угадать паттерн по коду" и добавляет его к списку вопросов
        public void AddQuestion(PatternName answer, String code)
        {
            ;
        }

        // Создает вопрос типа "угадать паттерн по изображению" и добавляет его к списку вопросов
        public void AddQuestion(PatternName answer, Image picture)
        {
            ;
        }

        // Если идёт игра, возвращает текущий вопрос. Иначе возвращает null
        public Question CurrentQuestion()
        {
            return null;
        }

        // Если выбранный пользователем ответ правильный, увеличивает счётчик правильных ответов, иначе увеличивает счетчик неправильных.
        // После чего увеличивает счётчик текущего вопроса.
        // Если вопросы закончились, завершает игру.
        public void SetAnswer(PatternName Answer)
        {
            ;
        }

        

        // Возвращает количество вопросов в игре
        public int GetQuestionsCount()
        {
            return Questions.Count;
        }

        // Возвращает количество выбранных игроком правильных ответов
        public int GetCorrectAnswers()
        {
            return CorrectAnswers;
        }

        // Возвращает количество выбранных игроком неправильных ответов
        public int GetIncorrectAnswers()
        {
            return IncorrectAnswers;
        }

        // Возвращает счетчик текущего вопроса
        public int GetCounter()
        {
            return Counter;
        }

        // Обнуляет параметры игры, но не запускает игру
        public void NewGame()
        {
           ;
        }

        // Проверяет, идет ли игра
        public bool IsOn()
        {
            return On;
        }

        // Запускает игру, но не обнуляет её параметры
        public void Start()
        {
            On = true;
        }

        // Завершает игру
        public void Stop()
        {
            On = false;
        }
    };

}

 