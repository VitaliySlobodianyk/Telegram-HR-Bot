using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;

namespace Horizon__Bot
{
   
    class HrBot
    {


        private static List<Post> Persons = new List<Post>();


        public static string TOKEN = "Your TOKEN";
        private static readonly TelegramBotClient Bot = new TelegramBotClient(TOKEN);
        private static readonly long Channel_ID = -1001226801228;

       
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    GetMessages().Wait();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex);
                }
            }
        }

        static async Task GetMessages()
        {
            
            TelegramBotClient bot = new TelegramBotClient(TOKEN);
            int offset = 0;
            int timeout = 0;

            try
            {
                await bot.SetWebhookAsync("");

                while (true)
                {
                    var updates = await bot.GetUpdatesAsync(offset, timeout);

                    foreach (var update in updates)
                    {
                        var message = update.Message;
                 
                        if (update.Message != null)
                        {

                       await  Bot_OnMessage(bot, update);

                        }
                        offset = update.Id + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }
    

    public static async Task Bot_OnMessage(object sender, Telegram.Bot.Types.Update e) {
        
                                

                    Post Code;


                    if (Is_Init(Persons, e.Message.Chat.Id))
                    {

                        Code = Return_Chat(Persons, e.Message.Chat.Id);

                    }
                    else {
                        Code = new Post();
                        Code.Chat_ID = e.Message.Chat.Id;
                        Persons.Add(Code);
                    }
          

            Txt_check(e.Message.Text, Code);
                     

            await Bot_Messages(e, sender, Code);
                                           
        }

        public static bool Is_Init(List<Post> Persons, long Chat_Id) {

            foreach (Post el in Persons) {

                if (el.Chat_ID == Chat_Id) {
                    return true;

                }

            }

            return false;
        }

        public static Post Return_Chat(List<Post> Persons, long Chat_Id) {

            foreach (Post el in Persons)
            {

                if (el.Chat_ID == Chat_Id)
                {
                    return el;

                }
            }

            return null;

        }

        public static void Txt_check(string Message, Post el)
        {
            
            const int Code_Yes = 11;
            const int Code_No = 10;
            const int Retry = 20;
            const int Edit = 21;
            const int Finish = 99;
            const int Instruction = 13;

            const int Code_Send = 200;  //send 200 - 200

            const int Name = 31;
            const int Number = 32;
            const int Age = 33;
            const int City = 34;
            const int Stag = 35;
            const int Taxi = 36;
            const int Uber = 37;
            const int Jail = 38;
            const int DTP = 39;
            const int Agent = 40;
            const int Date = 41;
            const int Filler = 42;
            const int Partner = 43;
         

            switch (Message)
            {

                case ("1)ФИО Кандидата"):
                    {
                        el.Codes.Push(Name);

                    }
                    break;
                case ("2)Номер телефона"):
                    {
                        el.Codes.Push(Number);

                    }
                    break;
                case ("3)Возраст"):
                    {
                        el.Codes.Push(Age);

                    }
                    break;
                case ("4)Город проживания"):
                    {
                        el.Codes.Push(City);

                    }
                    break;
                case ("5)Стаж по правам"):
                    {
                        el.Codes.Push(Stag);

                    }
                    break;
                case ("6)Стаж в такси"):
                    {
                        el.Codes.Push(Taxi);

                    }
                    break;
                case ("7)Есть ли аккаунт UBER"):
                    {
                        el.Codes.Push(Uber);

                    }
                    break;
                case ("8)Судимости"):
                    {
                        el.Codes.Push(Jail);

                    }
                    break;
                case ("9)ДТП"):
                    {
                        el.Codes.Push(DTP);

                    }
                    break;
                case ("10)Ваш агент"):
                    {
                        el.Codes.Push(Agent);

                    }
                    break;
                case ("11)Время и дата собеседования"):
                    {
                        el.Codes.Push(Date);

                    }
                    break;
                case ("12)Ваше Имя и Фамилия"):
                    {
                        el.Codes.Push(Filler);

                    }
                    break;
                case ("13)Партнер"):
                    {
                        el.Codes.Push(Partner);

                    }
                    break;


                case ("да"):
                    {
                        el.Codes.Push(Code_Yes);
                        
                    }
                    break;

                case ("Да"):
                    {
                        el.Codes.Push(Code_Yes);
                      
                    }
                    break;


                case ("Заполнить анкету"):
                    {
                        el.Codes.Push(Code_Yes);

                    }break;
                case ("Инструкции"):
                    {
                        el.Codes.Push(Instruction);

                    }
                    break;
                case ("Отправить анкету"):
                    {
                        el.Codes.Push(Code_Send);
                        el.Codes.Push(Code_Send);
                    }
                    break;
                case ("Подтверждение"):
                    {
                        el.Codes.Push(Code_Yes);                       
                    }
                    break;

                case ("Откорректировать"):
                    {
                        Console.WriteLine("EDIT");
                        el.Codes.Push(Edit);
                    }
                    break;

                case ("Перезапустить заполнение"):
                    {
                        el.Codes.Push(Retry);
                    }
                    break;

                case ("No"):
                    el.Codes.Push(Code_No);
                    break;


                case ("Не оставлять анкету"):
                    el.Codes.Push(Finish);
                    break;
                case ("Вернуться назад"):
                    el.Codes.Push(Finish);
                    break;                   
                case ("/finish"):
                    el.Codes.Push(Finish);
                    break;

                default:
                    el.Codes.Push(100);
                    break;
            }
        }

        public static async Task Bot_Messages(Telegram.Bot.Types.Update e, object sender, Post el)
        {
            const string Init = "Привет!\nЭтот бот обрабатывает анкеты для HR агенства\n" +
                "Horizon Recruiting.\n Если вы хотите оставить анкету введите \"Да\"  ";

            string Instructions = "Введите  свою  информацию согласно форме:"
             + el.Resume.Make_Resume();
            const string I2_1 = "ФИО Кандидата: ";
            const string I2_2 = "Номер телефона: ";
            const string I2_3 = "Возраст: ";
            const string I2_4 = "Город проживания: ";
            const string I2_5 = "Стаж по правам: ";
            const string I2_6 = "Стаж в такси: ";
            const string I2_7 = "Есть ли аккаунт UBER: ";
            const string I2_8 = "Судимости: ";
            const string I2_9 = "ДТП: ";
            const string I2_11 = "Ваш агент: ";
            const string I2_12 = "Время и дата собеседования: ";
            const string I2_13 = "Ваше имя и фамилия: ";
            const string I2_14 = "Партнер, к которому записан кандидат: ";
            const string Confirmation = "Подтверждаете заполнение?";  // Не участвует  в  логе  
            const string Code_Bye = "Спасибо, Ваша анкета принята";
            const string Instruction = "Как пользоваться ботом?\n1.Нажмите кнопку 'Заполнить анкету'\n2.Ознакомтесь со списком формы и нажмите кнопку 'Подтверждение'\n3.По порядку заполните анкету(Напишите в ответ нужную информацию)\n4.Проверьте правильность заполнения, если всё соответсвует, нажмите 'Отправить анкету', если в какой то строке есть неточности, нажмите 'Откорректировать', и откорректируйте тот пункт, где вы допустили ошибку\n5.Если вы передумали отправлять анкету, нажмите 'Не оставлять анкету'\n6.Если хотите ввести заново, нажмите 'Перезапустить заполнение'  ";


            if (el.Codes.Count > 1)
            {

                bool Txt_Chk = el.Codes.Peek() == 11 || el.Codes.Peek() == 100 || el.Codes.Peek() == 10;
                bool edit = (el.Codes.ElementAt(1) == 31 || el.Codes.ElementAt(1) == 32 || el.Codes.ElementAt(1) == 33 || el.Codes.ElementAt(1) == 34 || el.Codes.ElementAt(1) == 35 || el.Codes.ElementAt(1) == 36 || el.Codes.ElementAt(1) == 37 || el.Codes.ElementAt(1) == 38 || el.Codes.ElementAt(1) == 39 || el.Codes.ElementAt(1) == 40 || el.Codes.ElementAt(1) == 41 || el.Codes.ElementAt(1) == 42 || el.Codes.ElementAt(1) == 43);

                if (el.Codes.Peek() == 11 && (el.Codes.ElementAt(1) == 0))
                {

                    var kbq = new ReplyKeyboardMarkup

                    {
                        Keyboard = new[]  {
                            new[]{

                                new KeyboardButton("Заполнить анкету"),
                                new KeyboardButton("Инструкции")
                            }

                        },
                        ResizeKeyboard = true

                    };

                    el.Codes.Push(1);

                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, Init, ParseMode.Html, false, false, 0, kbq);
                }
                else if (el.Codes.Peek() == 13 )
                {
                    var kbd = new ReplyKeyboardMarkup

                    {
                        Keyboard = new[]  {
                            new[]{

                                new KeyboardButton("Заполнить анкету"),
                                new KeyboardButton("Вернуться назад")
                                
                            }

                        },
                        ResizeKeyboard = true

                    };
                
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, Instruction, ParseMode.Html, false, false, 0, kbd);
                    
                } 

                else if (el.Codes.Peek() == 11 && (el.Codes.ElementAt(1) == 1)||(el.Codes.Peek() == 11 && (el.Codes.ElementAt(1) == 13)))
                {
                    el.Codes.Push(2);
                    var kbd = new ReplyKeyboardMarkup
                    {
                        Keyboard = new[] {
                            new[]{
                                new KeyboardButton("Подтверждение")
                            },
                            new[]{
                                 new KeyboardButton("Не оставлять анкету")
                            }
                        },
                        ResizeKeyboard = true
                    };
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, Instructions);
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, Confirmation, ParseMode.Html, false, false, 0, kbd);
                   
                }

                else if (el.Codes.Peek() == 11 && (el.Codes.ElementAt(1) == 2))
                {
                    el.Codes.Push(2.1);
                    ReplyKeyboardRemove keyboardRemove = new ReplyKeyboardRemove();
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, I2_1, ParseMode.Default, false, false, 0, keyboardRemove);
                }

                else if (el.Codes.Peek() == 100 && (el.Codes.ElementAt(1) == 2.1))
                {
                    el.Resume.F2_1 += e.Message.Text;
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, I2_2);
                    el.Codes.Push(2.2);

                }
                else if (Txt_Chk && (el.Codes.ElementAt(1) == 2.2))
                {
                    el.Resume.F2_2 += e.Message.Text;

                    el.Codes.Push(2.3);
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, I2_3);

                }
                else if (el.Codes.Peek() == 100 && (el.Codes.ElementAt(1) == 2.3))
                {
                    el.Resume.F2_3 += e.Message.Text;

                    el.Codes.Push(2.4);
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, I2_4);

                }
                else if (el.Codes.Peek() == 100 && (el.Codes.ElementAt(1) == 2.4))
                {
                    el.Resume.F2_4 += e.Message.Text;

                    el.Codes.Push(2.5);
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, I2_5);

                }
                else if (Txt_Chk && (el.Codes.ElementAt(1) == 2.5))
                {
                    el.Resume.F2_5 += e.Message.Text;

                    el.Codes.Push(2.6);
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, I2_6);

                }
                else if (Txt_Chk && (el.Codes.ElementAt(1) == 2.6))
                {
                    el.Resume.F2_6 += e.Message.Text;

                    el.Codes.Push(2.7);
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, I2_7);
                }

                else if (Txt_Chk && (el.Codes.ElementAt(1) == 2.7))
                {
                    el.Resume.F2_7 += e.Message.Text;

                    el.Codes.Push(2.8);
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, I2_8);
                }
                else if (Txt_Chk && (el.Codes.ElementAt(1) == 2.8))
                {
                    el.Resume.F2_8 += e.Message.Text;

                    el.Codes.Push(2.9);
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, I2_9);

                }
                else if (Txt_Chk && (el.Codes.ElementAt(1) == 2.9))
                {
                    el.Resume.F2_9 += e.Message.Text;

                    el.Codes.Push(2.11);
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, I2_11);

                }
                else if (Txt_Chk && (el.Codes.ElementAt(1) == 2.11))
                {
                    el.Resume.F2_11 += e.Message.Text;

                    el.Codes.Push(2.12);
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, I2_12);

                }
                else if (Txt_Chk && (el.Codes.ElementAt(1) == 2.12))
                {
                    el.Resume.F2_12 += e.Message.Text;
                    el.Codes.Push(2.13);
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, I2_13);
                }
                else if (Txt_Chk && (el.Codes.ElementAt(1) == 2.13))
                {
                    el.Resume.F2_13 += e.Message.Text;
                    el.Codes.Push(2.14);
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, I2_14);
                }
                else if (Txt_Chk && (el.Codes.ElementAt(1) == 2.14))
                {
                    el.Resume.F2_14 += e.Message.Text;

                    el.Codes.Push(3);
                    el.Codes.Push(3);

                    await Bot_Messages(e, sender, el);

                }
                else if ((el.Codes.Peek() == 3 && (el.Codes.ElementAt(1) == 3)) || (Txt_Chk && edit))
                {
                    if (Txt_Chk && edit)
                    {
                        switch (el.Codes.ElementAt(1))
                        {
                            case 31:
                                el.Resume.F2_1 = el.Resume.F3_1 + e.Message.Text;
                                break;
                            case 32:
                                el.Resume.F2_2 = el.Resume.F3_2 + e.Message.Text;
                                break;
                            case 33:
                                el.Resume.F2_3 = el.Resume.F3_3 +  e.Message.Text;
                                break;
                            case 34:
                                el.Resume.F2_4 = el.Resume.F3_4 + e.Message.Text;
                                break;
                            case 35:
                                el.Resume.F2_5 = el.Resume.F3_5 + e.Message.Text;
                                break;
                            case 36:
                                el.Resume.F2_6 = el.Resume.F3_6 + e.Message.Text;
                                break;
                            case 37:
                                el.Resume.F2_7 = el.Resume.F3_7 + e.Message.Text;
                                break;
                            case 38:
                                el.Resume.F2_8 = el.Resume.F3_8 + e.Message.Text;
                                break;
                            case 39:
                                el.Resume.F2_9 = el.Resume.F3_9 + e.Message.Text;
                                break;
                            case 40:
                                el.Resume.F2_11 = el.Resume.F3_11 + e.Message.Text;
                                break;
                            case 41:
                                el.Resume.F2_12 = el.Resume.F3_12 + e.Message.Text;
                                break;
                            case 42:
                                el.Resume.F2_13 = el.Resume.F3_13 + e.Message.Text;
                                break;
                            case 43:
                                el.Resume.F2_14 = el.Resume.F3_14 + e.Message.Text;
                                break;
                        }
                    }

                    var kbd = new ReplyKeyboardMarkup
                    {

                        Keyboard = new[] {
                            new[]{
                                new KeyboardButton("Отправить анкету"),
                                new KeyboardButton("Откорректировать")

                            },
                            new[]{
                                 new KeyboardButton("Не оставлять анкету"),
                                 new KeyboardButton("Перезапустить заполнение")
                            }
                        },
                        ResizeKeyboard = true
                    };
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, el.Resume.Make_Resume());
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, Confirmation, ParseMode.Html, false, false, 0, kbd);
                }

                else if (el.Codes.Peek() == 200 && (el.Codes.ElementAt(1) == 200))
                {

                    ReplyKeyboardRemove keyboardRemove = new ReplyKeyboardRemove();
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, Code_Bye,ParseMode.Html, false, false, 0, keyboardRemove);
                    
                    Console.WriteLine("Sended Resume!");

                    string CV = el.Resume.Make_Resume();

                    Console.WriteLine("\nRESUME:\n" + CV);
                    await Bot.SendTextMessageAsync(Channel_ID, CV);
                    el.Codes.Clear();
                    el.Resume = new Resume();

                    el.Codes.Push(99);
                    await Bot_Messages(e, sender, el);

                }
                         
                else if (el.Codes.Peek() == 21 )// 300 is edited  markup
            {
               
                var kbd = new ReplyKeyboardMarkup
                {
                    Keyboard = new[] {
                            new[]{
                                new KeyboardButton("1)ФИО Кандидата"),
                                new KeyboardButton("2)Номер телефона"),
                                 new KeyboardButton("3)Возраст")
                            },
                            new[]{

                                 new KeyboardButton("4)Город проживания"),
                                 new KeyboardButton("5)Стаж по правам"),
                                  new KeyboardButton("6)Стаж в такси")
                            },
                            new[]{
                                 new KeyboardButton("7)Есть ли аккаунт UBER"),
                                 new KeyboardButton("8)Судимости"),
                                 new KeyboardButton("9)ДТП")
                            },
                             new[]{
                                 new KeyboardButton("10)Ваш агент"),
                                  new KeyboardButton("11)Время и дата собеседования"),
                                 new KeyboardButton("12)Ваше Имя и Фамилия"),
                            },
                             new[]{
                                 new KeyboardButton("13)Партнер")
                            }
                        },
                    ResizeKeyboard = true
                };

                await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Выберите что отредактировать", ParseMode.Html, false, false, 0, kbd);
            }

            else if (el.Codes.Peek() == 31)
            {
                    ReplyKeyboardRemove keyboardRemove = new ReplyKeyboardRemove();
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id,"Старое "+ el.Resume.F2_1 , ParseMode.Html, false, false, 0, keyboardRemove);
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Введите новое ФИО:");
            }
            else if (el.Codes.Peek() == 32)
                {
                    ReplyKeyboardRemove keyboardRemove = new ReplyKeyboardRemove();
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Старый " + el.Resume.F2_2, ParseMode.Html, false, false, 0, keyboardRemove);
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Введите новый номер телефона:");
                }
            else if (el.Codes.Peek() == 33)
            {
                    ReplyKeyboardRemove keyboardRemove = new ReplyKeyboardRemove();
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Старый " + el.Resume.F2_3, ParseMode.Html, false, false, 0, keyboardRemove);
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Введите новый Возраст:");
            }
                else if (el.Codes.Peek() == 34)
            {
                    ReplyKeyboardRemove keyboardRemove = new ReplyKeyboardRemove();
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Старый " + el.Resume.F2_4, ParseMode.Html, false, false, 0, keyboardRemove);
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Введите новый Город проживания:");
            }
                else if (el.Codes.Peek() == 35)
            {
                    ReplyKeyboardRemove keyboardRemove = new ReplyKeyboardRemove();
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Старый " + el.Resume.F2_5, ParseMode.Html, false, false, 0, keyboardRemove);
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Введите новый Стаж по правам:");
            }
            else if (el.Codes.Peek() == 36)
            {
                    ReplyKeyboardRemove keyboardRemove = new ReplyKeyboardRemove();
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Старый " + el.Resume.F2_6, ParseMode.Html, false, false, 0, keyboardRemove);
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Введите новый Стаж в такси:");
            }
            else if (el.Codes.Peek() == 37)
            {
                    ReplyKeyboardRemove keyboardRemove = new ReplyKeyboardRemove();
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, el.Resume.F2_7, ParseMode.Html, false, false, 0, keyboardRemove);
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Введите новый статус: ");
            }
            else if (el.Codes.Peek() == 38)
            {
                    ReplyKeyboardRemove keyboardRemove = new ReplyKeyboardRemove();
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, el.Resume.F2_8, ParseMode.Html, false, false, 0, keyboardRemove);
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Введите новый статус: ");
            }
            else if (el.Codes.Peek() == 39)
            {
                    ReplyKeyboardRemove keyboardRemove = new ReplyKeyboardRemove();
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, el.Resume.F2_9, ParseMode.Html, false, false, 0, keyboardRemove);
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Введите новый статус: ");
            }
            else if (el.Codes.Peek() == 40)
            {
                    ReplyKeyboardRemove keyboardRemove = new ReplyKeyboardRemove();
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id,"Старая информация: "+ el.Resume.F2_11, ParseMode.Html, false, false, 0, keyboardRemove);
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Введите Вашего агента: ");
            }
            else if (el.Codes.Peek() == 41)
            {
                    ReplyKeyboardRemove keyboardRemove = new ReplyKeyboardRemove();
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Старая: " + el.Resume.F2_12, ParseMode.Html, false, false, 0, keyboardRemove);
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Введите новую дату:");
            }
            else if (el.Codes.Peek() == 42)
            {
                    ReplyKeyboardRemove keyboardRemove = new ReplyKeyboardRemove();
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Старая информация: " + el.Resume.F2_13, ParseMode.Html, false, false, 0, keyboardRemove);
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Введите новую: ");
            }
            else if (el.Codes.Peek() == 43)
            {
                   ReplyKeyboardRemove keyboardRemove = new ReplyKeyboardRemove();
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Старый " + el.Resume.F2_14, ParseMode.Html, false, false, 0, keyboardRemove);
                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Введите нового партнера:");
            }

            else if (el.Codes.Peek() == 20)
            {
                el.Codes.Push(1);
                el.Codes.Push(11);
                el.Resume = new Resume();
                await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Перезапуск...");
                await Bot_Messages(e, sender, el);
            }
              
                else if (el.Codes.Peek() == 99 )
            {
                el.Codes.Clear();
                el.Resume = new Resume();
                await Bot_Messages(e, sender, el);
            }
                else if ( el.Codes.Peek() == 13)
                {
                                       
                }
                else
            {              
                el.Codes.Pop();
                el.Codes.Pop();
                await Bot_Messages(e, sender, el);
            }

        }

            else if (el.Codes.Count() == 0)
            {
                el.Codes.Push(0);
                el.Codes.Push(11);
                await Bot_Messages(e, sender, el);

            }
            else if (el.Codes.Peek() != 0)
            {
                el.Codes.Pop();
                el.Codes.Push(0);
                el.Codes.Push(11);
                await Bot_Messages(e, sender, el);

            }
            else
            {
                await Bot.SendTextMessageAsync(e.Message.Chat.Id, Init);
                await Bot.SendTextMessageAsync(e.Message.Chat.Id, Confirmation);
            }
            }

        public static async Task Edit_Resume(Telegram.Bot.Types.Update e, object sender, Post el) {

        } 
          
        }
    }


