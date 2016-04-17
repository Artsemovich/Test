using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab3test
{
    public partial class Form1 : Form
    {
        PatternsGame Game;
        List<PatternName> AnswerVariants;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Game = new PatternsGame();
            Game.AddQuestion(CodeQuestion.FromFile(PatternName.Composite, "Composite.txt"));
            Game.AddQuestion(PictureQuestion.FromFile(PatternName.Decorator, "Decorator.png"));
            Game.AddQuestion(PictureQuestion.FromFile(PatternName.Delegation, "Delegation.png"));
            Game.AddQuestion(PictureQuestion.FromFile(PatternName.FactoryMethod, "FactoryMethod.jpg"));
            Game.AddQuestion(PictureQuestion.FromFile(PatternName.Flyweight, "Flyweight.png"));
            Game.AddQuestion(PictureQuestion.FromFile(PatternName.InformationExpert, "InformationExpert.png"));
            Game.AddQuestion(CodeQuestion.FromFile(PatternName.Proxy, "Proxy.txt"));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Game.IsOn() == false)
            {
                Game.NewGame();
                if (Game.GetQuestionsCount() > 0)
                {
                    Game.Start();
                    button1.Text = "Стоп";
                    button2.Visible = true;
                    button3.Visible = true;
                    button4.Visible = true;
                    button5.Visible = true;
                    ShowQuestion();
                }
            }
            else
            {
                Game.Stop();
                if (Game.IsOn() == false)
                {
                    MessageBox.Show("Верных ответов: " + Game.GetCorrectAnswers() + ", неверных: " + Game.GetIncorrectAnswers() + ", из " + Game.GetQuestionsCount() + " вопросов.");
                }
                pictureBox1.Visible = false;
                textBox1.Visible = false;
                button1.Text = "Старт";
                button2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
                button5.Visible = false;
            }
        }

        private void ShowQuestion()
        {
            if (Game.IsOn())
            {
                Question question = Game.CurrentQuestion();
                if (question.GetType() == typeof(CodeQuestion))
                {
                    pictureBox1.Visible = false;
                    textBox1.Visible = true;
                    textBox1.Text = ((CodeQuestion)question).Code;
                }
                else if (question.GetType() == typeof(PictureQuestion))
                {
                    pictureBox1.Visible = true;
                    textBox1.Visible = false;
                    pictureBox1.Image = ((PictureQuestion)question).Picture;
                }
                AnswerVariants = Game.AnswerVariants(4);
                button2.Text = PatternsGame.PatternString(AnswerVariants[0]);
                button3.Text = PatternsGame.PatternString(AnswerVariants[1]);
                button4.Text = PatternsGame.PatternString(AnswerVariants[2]);
                button5.Text = PatternsGame.PatternString(AnswerVariants[3]);
            }
            else
            {
                pictureBox1.Visible = false;
                textBox1.Visible = false;
                button1.Text = "Старт";
                button2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
                button5.Visible = false;
            }
        }

        private void SetAnswer(PatternName answer)
        {
            Game.SetAnswer(answer);
            if (Game.IsOn() == false)
            {
                MessageBox.Show("Верных ответов: " + Game.GetCorrectAnswers() + ", неверных: " + Game.GetIncorrectAnswers() + ", из " + Game.GetQuestionsCount() + " вопросов.");
            }
            ShowQuestion();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SetAnswer(AnswerVariants[0]);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SetAnswer(AnswerVariants[1]);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SetAnswer(AnswerVariants[2]);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SetAnswer(AnswerVariants[3]);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
