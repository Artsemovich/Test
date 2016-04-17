﻿using System;
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
           ;
        }
        // Конструктор, создает вопрос с заданным правильным ответом
        public CodeQuestion(PatternName pattern)
        {
            ;
        }
        // Конструктор, создает вопрос с заданным правильным ответом и кодом программы
        public CodeQuestion(PatternName pattern, String pattern_code)
        {
           ;
        }
        // Статический метод, возвращает вопрос с заданным правильным ответом, код берет из файла
        public CodeQuestion (PatternName pattern, String FileName)
        {
            ;
        }
    };
}

 