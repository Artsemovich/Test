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
            ;
        }

        // Конструктор вопроса с заданным правильным ответом
        public Question(PatternName pattern)
        {
            ;
        }

        // Проверка, правильный ли выбран ответ

    }
}

 