using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UkraineWarChronicles_Bot.Clients;
using UkraineWarChronicles_Bot.Model;
using Telegram.Bot;
using Telegram.Bot.Extensions;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Exceptions;

namespace UkraineWarChronicles_Bot
{
   
    public class UkraineWarChroniclesBot
    {
        TelegramBotClient BotClient = new TelegramBotClient("5574758882:AAEEFeN9E43pQnxjYPA6cC-gjwjWJDvEgNc");
        CancellationToken CancellationToken = new CancellationToken();
        ReceiverOptions ReceiverOptions = new ReceiverOptions { AllowedUpdates = { } };

        private static int Day = 1;

        private static int DayOfDate = 24;
        private static int MonthOfDate = 2;
        private static int YearOfDate = 2022;

        private static string Date = "24.02.2022";
        private static string DayDate = $"{Day}-а доба вторгнення - " + Date;       

        private static bool IsChoiceDay = false;
        private static bool IsChoiceNewDay = false;

        private static string Introduction = "🇺🇦Вітаю! Цей бот допоможе дізнатись про головне, що відбулось впродовж певного дня від початку Російського вторгнення в Україну.";
        private static string Help = "Обирай день для пошуку і отримуй: \n - новини з перевірених джерел; \n - втрати ворожих сил; \n - інтерактивну мапу ситуації на фронті; \n - звернення президента Володимира Зеленського; \n \n Введи команду: \n /choiceday - обрати день повномасштабного вторгнення; \n /currentwarmap - отримати мапу поточної ситуації на фронті; \n /help - отримати інформацію щодо роботи з ботом.";
        private static string ChoseDay = "🇺🇦Вибір доби із початку повномасштабного вторгнення. \n \n 24.02.2022 - 28.02.2022 — 1 - 5 дні вторгнення; \n \n 01.03.2022 - 31.03.2022 — 6 - 36 дні вторгнення; \n \n 01.04.2022 - 30.04.2022 — 37 - 66 дні вторгнення; \n \n 01.05.2022 - 31.05.2022 — 67 - 97 дні вторгнення; \n \n 01.06.2022 - 30.06.2022 — 98 - 127 дні вторгнення; \n \n 01.07.2022 - 04.07.2022 — 128 - 131 дні вторгнення. \n \n Отже, введи добу, яка тебе цікавить (наприклад - \"100\"):";
        
        public async Task Start()
        {
            BotClient.StartReceiving(HandlerUpdateAsync, HandlerError, ReceiverOptions, CancellationToken);
            var botMe = await BotClient.GetMeAsync();
            Console.WriteLine($"Бот {botMe.Username} почав працювати");            
        }

        private Task HandlerError(ITelegramBotClient botClient, Exception exception, CancellationToken CancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Помилка в телеграм бот АПІ:\n {apiRequestException.ErrorCode}" +
                $"\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        private async Task HandlerUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken CancellationToken)
        {
            if (update.Type == UpdateType.Message && update?.Message?.Text != null)
            {
                await HandlerMessageAsync(botClient, update.Message);
            }
            //add
            //
            if (update?.Type == UpdateType.CallbackQuery)
            {
                await HandlerCallbackQuery(botClient, update.CallbackQuery);
            }
            //
        }
        //add
        //
        private async Task HandlerCallbackQuery(ITelegramBotClient botClient, CallbackQuery? callbackQuery)
        {
            if (callbackQuery.Data.StartsWith(""))
            {
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, text: $":");
                return;
            }
            if (callbackQuery.Data.StartsWith(""))
            {
                await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, text: $"");
                return;
            }
            await botClient.SendTextMessageAsync(callbackQuery.Message.Chat.Id, text: $" \n{callbackQuery.Data}");
            return;
        }
        //
        private async Task HandlerMessageAsync(ITelegramBotClient botClient, Message message)
        {
            if (message.Text == "/start")
            {
                IsChoiceDay = false;
                IsChoiceNewDay = false;

                await botClient.SendTextMessageAsync(message.Chat.Id, Introduction);
                await botClient.SendTextMessageAsync(message.Chat.Id, "https://t.me/suspilnenews/11630");
                await botClient.SendTextMessageAsync(message.Chat.Id, Help);
                return;
            }
            else
            if (message.Text == "/help")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, Help);
                return;
            }
            else
            if (message.Text == "/choiceday")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, ChoseDay);
                IsChoiceNewDay = true;
                return;
            }
            else
            if (IsChoiceNewDay)
            {
                if (int.TryParse(message.Text, out Day) && Day > 0 && Day < 132)
                {
                    IsChoiceDay = true;

                    IsChoiceNewDay = false;

                    if(Day < 5)
                    {
                        DayOfDate = 24 + Day;
                        MonthOfDate = 2;
                    }
                    else if (Day == 5)
                    {
                        DayOfDate = 1;
                        MonthOfDate = 3;
                    }
                    else if( Day > 5 && Day <= 36 )
                    {
                        DayOfDate = Day - 5;
                        MonthOfDate = 3;
                    }
                    else if (Day > 36 && Day <= 66)
                    {
                        DayOfDate = Day - 36;
                        MonthOfDate = 4;                        
                    }
                    else if (Day > 66 && Day <= 97)
                    {
                        DayOfDate = Day - 66;
                        MonthOfDate = 5;                        
                    }
                    else if (Day > 97 && Day <= 127)
                    {
                        DayOfDate = Day - 97;
                        MonthOfDate = 6;                        
                    }
                    else if (Day > 127 && Day <= 131)
                    {
                        DayOfDate = Day - 127;
                        MonthOfDate = 7;                        
                    }

                    if(DayOfDate < 10)
                    {
                        Date = $"0{DayOfDate}.0{MonthOfDate}.{YearOfDate}";
                    }
                    else
                    {
                        Date = $"{DayOfDate}.0{MonthOfDate}.{YearOfDate}";
                    }                    

                    if(Day%10 == 3)
                    {
                        DayDate = $"{Day}-тя доба вторгнення - " + Date;
                    }
                    else
                    {
                        DayDate = $"{Day}-а доба вторгнення - " + Date;
                    }
                    
                    await botClient.SendTextMessageAsync(message.Chat.Id, DayDate);

                    ReplyKeyboardMarkup replyKeyboardMarkup = new
                        (
                        new[]
                            {
                        new KeyboardButton [] { "Головні новини", "Втрати ворожих сил"},
                        new KeyboardButton [] { "Інтерактивна мапа ситуації на фронті", "Звернення президента"}
                            }
                        )
                    {
                        ResizeKeyboard = true
                    };

                    await botClient.SendTextMessageAsync(message.Chat.Id, "Вибери пункт меню:", replyMarkup: replyKeyboardMarkup);
                                        
                }
                else
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Помилково введено день! Спробуйте ще раз, будь ласка:");
                    return;
                }
            }           
            else
            if (message.Text == "/currentwarmap")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "https://deepstatemap.live");
                return;
            }
            else
            if (IsChoiceDay && message.Text == "Головні новини")
            {
                string URL1 = $"https://novynarnia.com/hronika-oborony-ukrayiny-den-{Day}/";
                string URL2 = $"https://www.radiosvoboda.org/z/2735/{YearOfDate}/{MonthOfDate}/{DayOfDate}";
                
                NewsClient client = new NewsClient();
                NewsOfDay News = client.GetNewsByDayAsync(Day.ToString()).Result;

                //Console.WriteLine(News.GetText());

                await botClient.SendTextMessageAsync(message.Chat.Id, $"{News.GetTitle()}");
                //await botClient.SendTextMessageAsync(message.Chat.Id, $"{News.GetTitle()} \n \n Дізнатись більше:");

                await botClient.SendTextMessageAsync(message.Chat.Id, URL1);

                await botClient.SendTextMessageAsync(message.Chat.Id, URL2);

                return;
            }
            else
            if (IsChoiceDay && message.Text == "Втрати ворожих сил")
            {
                LossesEnemyClient client = new LossesEnemyClient();
                VideoOfDay Videos = client.GetLossesEnemyByDayAsync(Day.ToString()).Result;

                foreach (var element in Videos.contents)
                {
                    if (element.video.title.Contains($" {Day} "))
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, $"{element.video.title}. \n \n https://www.youtube.com/watch?v={element.video.videoId}");
                        return;
                    }
                }

                await botClient.SendTextMessageAsync(message.Chat.Id, "На жаль, не вдалось знайти інформацію про втрати.");

                return;
            }
            else
            if (IsChoiceDay && message.Text == "Інтерактивна мапа ситуації на фронті")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, $"https://liveuamap.com/uk/time/" + Date);

                return;
            }
            else
            if (IsChoiceDay && message.Text == "Звернення президента")
            {
                SpeechPresidentClient client = new SpeechPresidentClient();
                VideoOfDay Videos = client.GetSpeechPresidentByDayAsync(Day.ToString()).Result;

                foreach (var element in Videos.contents)
                {
                    if(Day < 10)
                    {
                        if (element.video.title.Contains($"0{Day} "))
                        {
                            await botClient.SendTextMessageAsync(message.Chat.Id, $"{element.video.title}. \n \n https://www.youtube.com/watch?v={element.video.videoId}");
                            return;
                        }
                    }
                    else
                    {
                        if (element.video.title.Contains($"{Day} "))
                        {
                            await botClient.SendTextMessageAsync(message.Chat.Id, $"{element.video.title}. \n \n https://www.youtube.com/watch?v={element.video.videoId}");
                            return;
                        }
                    }                   
                    
                }

                await botClient.SendTextMessageAsync(message.Chat.Id, "На жаль, не вдалось знайти звернення президента.");
                                
                return;
            }            
        }
    }
}
