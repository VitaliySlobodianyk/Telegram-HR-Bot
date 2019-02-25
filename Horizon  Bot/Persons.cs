using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon__Bot
{
    public class Resume
    {
        public string F3_1 = "ФИО Кандидата: ";
        public string F3_2 = "Номер телефона: ";
        public string F3_3 = "Возраст: ";
        public string F3_4 = "Город проживания: ";
        public string F3_5 = "Стаж по правам: ";
        public string F3_6 = "Стаж в такси: ";
        public string F3_7 = "Есть ли аккаунт UBER: ";
        public string F3_8 = "Судимости: ";
        public string F3_9 = "ДТП: ";
        public string F3_11 = "Ваш агент: ";
        public string F3_12 = "Время и дата собеседования: ";
        public string F3_13 = "Ваше Имя и Фамилия: ";
        public string F3_14 = "Партнер, к которому записан кандидат: ";







        public string F2_1 = "ФИО Кандидата: ";
        public string F2_2 = "Номер телефона: ";
        public string F2_3 = "Возраст: ";
        public string F2_4 = "Город проживания: ";
        public string F2_5 = "Стаж по правам: ";
        public string F2_6 = "Стаж в такси: ";
        public string F2_7 = "Есть ли аккаунт UBER: ";
        public string F2_8 = "Судимости: ";
        public string F2_9 = "ДТП: ";
        public string F2_11 = "Ваш агент: ";
        public string F2_12 = "Время и дата собеседования: ";
        public string F2_13 = "Ваше Имя и Фамилия: ";
        public string F2_14 = "Партнер, к которому записан кандидат: ";

        public string Make_Resume()
        {
            string CV;

            CV = "\nАнкета:\n" + F2_1 + "\n" + F2_2 + "\n" + F2_3 + "\n" + F2_4 + "\n" + F2_5 + "\n" + F2_6 + "\n" + F2_7 + "\n" +
                F2_8 + "\n" + F2_9 + "\n" + F2_11 + "\n" + F2_12 + "\n" + F2_13 + "\n" + F2_14 + "\n";
            return CV;
        }
    }

    class Post
    {
        public Stack<double> Codes = new Stack<double>();

        public long Chat_ID;

        public Resume Resume = new Resume();

    }
}
