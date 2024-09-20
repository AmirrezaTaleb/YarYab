using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using YarYab.Telegram.Bot.Models;

namespace YarYab.Telegram.Bot.Services
{
    public class UserService : IUserService
    {
        #region ctor
        private readonly ITelegramBotClient _bot;
        private readonly ILogger<UserService> _logger;
        public UserService(ITelegramBotClient bot, ILogger<UserService> logger)
        {
            _bot = bot;
            _logger = logger;
        }
        #endregion
        public async Task<UserProfileMessageModel> ShowProfile(Chat chat)
        {
            var model = new UserProfileMessageModel();
            model.Banner = ProfileBanner();
            model.InlineKeyboardMarkup = ProfileInlineKeyboardMarkup();
            model.Photo = ProfilePhoto();
            return model;
        }
        public async Task<UserLocationModel> CurentLocation(string UserId)
        {
            return new UserLocationModel(lat: 35.6892f, lang: 51.3890f);
        }
        public string ProfileBanner()
        {
            return "  نام کاربری : امیررضا  ";
        }
        public InlineKeyboardMarkup LocationInlineKeyboardMarkup()
        {
            return new InlineKeyboardMarkup()
                        .AddNewRow()
                            .AddButton("ثبت موقعیت مکانی", "change_or_set_my_location");
        }
        public InlineKeyboardMarkup ProfileInlineKeyboardMarkup()
        {
            return new InlineKeyboardMarkup(new[]
            {
        new[] { InlineKeyboardButton.WithCallbackData("📍 مشاهده موقعیت من", "my_location") },
        new[]
        {
            InlineKeyboardButton.WithCallbackData("❤️ مشاهده لایک کننده", "who_like_me"),
            InlineKeyboardButton.WithCallbackData("🔄 فعال/غیرفعال کردن لایک", "active_can_like")
        },
        new[]
        {
            InlineKeyboardButton.WithCallbackData("🚫 بلاک شده‌ها", "my_blocklist"),
            InlineKeyboardButton.WithCallbackData("👥 لیست مخاطبین", "my_contacts")
        },
        new[] { InlineKeyboardButton.WithCallbackData("🛠️ ویرایش اطلاعات پروفایل", "edit_profile") }
    });
        }
        public InlineKeyboardMarkup EditProfileInlineKeyboardMarkup()
        {
            return new InlineKeyboardMarkup(new[]
            {
        new[] { InlineKeyboardButton.WithCallbackData("⚧ ویرایش جنسیت", "edit_gender") },
        new[] { InlineKeyboardButton.WithCallbackData("📝 ویرایش نام", "edit_name") },
        new[] { InlineKeyboardButton.WithCallbackData("🎂 ویرایش سن", "edit_age") },
        new[] { InlineKeyboardButton.WithCallbackData("🏙️ ویرایش شهر", "edit_city") },
        new[] { InlineKeyboardButton.WithCallbackData("📸 ویرایش عکس", "edit_photo") },
                new[] { InlineKeyboardButton.WithCallbackData("🔙 بازگشت", "back_to_profile") }
    });
        }
        public FileStream ProfilePhoto()
        {
            return new FileStream("Files/images.png", FileMode.Open, FileAccess.Read);
        }

        public async Task SetLocation(string UserId, UserLocationModel userLocation)
        {
            await Task.Delay(1000);
        }

        public ReplyKeyboardMarkup SetLocaionReplyKeyboardMarkup()
        {
            return new ReplyKeyboardMarkup(true).AddButton(KeyboardButton.WithRequestLocation("برای ارسال لوکیشن اینجا کلیک کنید !"));

        }
        public ReplyKeyboardMarkup GenderOptionReplyKeyboardMarkup()
        {
            return new ReplyKeyboardMarkup(new[]
            {
        new[] { new KeyboardButton("👦 مرد"), new KeyboardButton("👧 زن") },
        new[] { new KeyboardButton("🏳️‍🌈 غیر دودویی"), new KeyboardButton("⚧️ تراجنسیتی") },
        new[] { new KeyboardButton("👩‍❤️‍👩 لزبین"), new KeyboardButton("👨‍❤️‍👨 گی") },
        new[] { new KeyboardButton("👨‍🦰 ترنس مرد"), new KeyboardButton("👩‍🦰 ترنس زن") },
        new[] { new KeyboardButton("🌈 بی جنسیت"), new KeyboardButton("🏳️‍⚧️ جنسیت سیال") }
    })
            {
                ResizeKeyboard = true,  // Adjust button size to fit the text
                OneTimeKeyboard = true   // Keyboard disappears after selection
            };
        }
        public ReplyKeyboardMarkup AgeOptionReplyKeyboardMarkup()
        {
            var keyboardButtons = new List<List<KeyboardButton>>();

            // Loop through ages from 10 to 90 and group them into rows
            for (int i = 10; i <= 90; i += 10)
            {
                // Create a row of buttons (you can adjust the grouping as needed)
                var row = new List<KeyboardButton>();
                for (int j = i; j < i + 10 && j <= 90; j++)
                {
                    row.Add(new KeyboardButton(j.ToString()));
                }
                keyboardButtons.Add(row);
            }

            return new ReplyKeyboardMarkup(keyboardButtons)
            {
                ResizeKeyboard = true,  // Adjust button sizes
                OneTimeKeyboard = true  // Keyboard disappears after selection
            };
        }
        public ReplyKeyboardMarkup CityOptionReplyKeyboardMarkup()
        {
            return new ReplyKeyboardMarkup(new[]
            {
        new[] { new KeyboardButton("تهران"), new KeyboardButton("تبریز") },
        new[] { new KeyboardButton("همدان"), new KeyboardButton(" زاهدان") },
     })
            {
                ResizeKeyboard = true,  // Adjust button size to fit the text
                OneTimeKeyboard = true   // Keyboard disappears after selection
            };
        }
        public async Task<bool> ActiveOrDeActiveLike(string UserId)
        {
            await Task.Delay(1000);
            return true;
        }

        public async Task<List<UserModel>> WhoLikeMe(string UserId)
        {
            await Task.Delay(1000);
            return
                new List<UserModel>() {
                new UserModel() { CityTitle = "Tehran", LastActivity = DateTime.Now.AddHours(-1), LastSeen = DateTime.Now.AddHours(-0.5), Name = "Amirreza", UserId = "/User_Amirrafkja3" ,Location =new UserLocationModel(lat: 35.6892f, lang: 51.3890f)},
                new UserModel() { CityTitle = "Tehran", LastActivity = DateTime.Now.AddHours(-2), LastSeen = DateTime.Now.AddHours(-0.75), Name = "زهرا", UserId = "/User_Zahrasdnia" ,Location =new UserLocationModel(lat: 36.6892f, lang: 51.3890f)}
                };
        }

        public async Task<List<UserModel>> ContactsById(string UserId)
        {
            await Task.Delay(1000);
            return
                new List<UserModel>() {
                new UserModel() { CityTitle = "Tehran", LastActivity = DateTime.Now.AddHours(-1), LastSeen = DateTime.Now.AddHours(-0.5), Name = "Amirreza", UserId = "/User_Amirrafkja3" ,Location =new UserLocationModel(lat: 35.6892f, lang: 51.3890f)},
                new UserModel() { CityTitle = "Tehran", LastActivity = DateTime.Now.AddHours(-2), LastSeen = DateTime.Now.AddHours(-0.75), Name = "زهرا", UserId = "/User_Zahrasdnia" ,Location =new UserLocationModel(lat: 36.6892f, lang: 51.3890f)}
                };
        }

        public async Task<List<UserModel>> BlackListById(string UserId)
        {
            await Task.Delay(1000);
            return
                new List<UserModel>() {
                new UserModel() { CityTitle = "Tehran", LastActivity = DateTime.Now.AddHours(-1), LastSeen = DateTime.Now.AddHours(-0.5), Name = "Amirreza", UserId = "/User_Amirrafkja3" ,Location =new UserLocationModel(lat: 35.6892f, lang: 51.3890f)},
                new UserModel() { CityTitle = "Tehran", LastActivity = DateTime.Now.AddHours(-2), LastSeen = DateTime.Now.AddHours(-0.75), Name = "زهرا", UserId = "/User_Zahrasdnia" ,Location =new UserLocationModel(lat: 36.6892f, lang: 51.3890f)}
                };
        }

        public async Task EditName(string UserId, string NewName)
        {
            await Task.Delay(1000);
        }

        public async Task EditAge(string UserId, int NewAge)
        {
            await Task.Delay(1000);
        }
        public int GetGenderIdByText(string gender)
        {
            switch (gender)
            {
                case "👦 مرد":
                    return 1;
                case "👧 زن":
                    return 2;
                case "🏳️‍🌈 غیر دودویی":
                    return 3;
                case "⚧️ تراجنسیتی":
                    return 4;
                case "👩‍❤️‍👩 لزبین":
                    return 5;
                case "👨‍❤️‍👨 گی":
                    return 6;
                case "👨‍🦰 ترنس مرد":
                    return 7;
                case "👩‍🦰 ترنس زن":
                    return 8;
                case "🌈 بی جنسیت":
                    return 9;
                case "🏳️‍⚧️ جنسیت سیال":
                    return 10;
                default:
                    return 0; // Default case if no match is found
            }
        }
        public int GetCityIdByText(string CityName)
        {
            return 9;
        }
        public async Task EditPhoto(string UserId, byte[] NewPhoto)
        {
            await Task.Delay(1000);
        }

        public async Task EditCity(string UserId, int CityId)
        {
            await Task.Delay(1000);
        }

        public async Task EditGender(string UserId, int genderId)
        {
            await Task.Delay(1000);
        }
    }











    public interface IUserService
    {
        public int GetGenderIdByText(string gender);
        public int GetCityIdByText(string city);
        public ReplyKeyboardMarkup GenderOptionReplyKeyboardMarkup();
        public ReplyKeyboardMarkup AgeOptionReplyKeyboardMarkup();
        public ReplyKeyboardMarkup CityOptionReplyKeyboardMarkup();
        public InlineKeyboardMarkup EditProfileInlineKeyboardMarkup();
        Task<UserProfileMessageModel> ShowProfile(Chat chat);
        Task<UserLocationModel> CurentLocation(string UserId);
        Task<List<UserModel>> WhoLikeMe(string UserId);
        Task EditName(string UserId, string NewName);
        Task EditAge(string UserId, int NewAge);
        Task EditPhoto(string UserId, byte[] NewPhoto);
        Task EditCity(string UserId, int CityId);
        Task EditGender(string UserId, int genderId);
        Task<List<UserModel>> ContactsById(string UserId);
        Task<List<UserModel>> BlackListById(string UserId);
        Task<bool> ActiveOrDeActiveLike(string UserId);
        Task SetLocation(string UserId, UserLocationModel userLocation);
        InlineKeyboardMarkup LocationInlineKeyboardMarkup();
        InlineKeyboardMarkup ProfileInlineKeyboardMarkup();
        ReplyKeyboardMarkup SetLocaionReplyKeyboardMarkup();
        FileStream ProfilePhoto();
        string ProfileBanner();
    }
}
