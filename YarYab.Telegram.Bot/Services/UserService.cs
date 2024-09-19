﻿using System;
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
            return new UserLocationModel(userId: 3, lat: 35.6892f, lang: 51.3890f);
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
            return new InlineKeyboardMarkup()
                 .AddNewRow()
                     .AddButton("مشاهده موقعیت location من", "my_location")
                     .AddNewRow()
                     .AddButton("مشاهده لایک کننده", "my_like")
                     .AddButton("فعال یا غیر فعال کردن لایک", "active_can_like")
                     .AddNewRow()
                     .AddButton("بلاک شده ها", "my_blocklist")
                     .AddButton("لیست مخاطبین", "my_contacts")
                     .AddNewRow()
                     .AddButton("ویرایش اطلاعات پروفایل", "edit_profile");
        }
        public FileStream ProfilePhoto()
        {
            return new FileStream("Files/images.png", FileMode.Open, FileAccess.Read);
        }

        public async Task SetLocation(UserLocationModel userLocation)
        {
            await Task.Delay(1000);
        }

        public ReplyKeyboardMarkup SetLocaionReplyKeyboardMarkup()
        {
            return new ReplyKeyboardMarkup(true).AddButton(KeyboardButton.WithRequestLocation("برای ارسال لوکیشن اینجا کلیک کنید !"));

        }
    }











    public interface IUserService
    {
        Task<UserProfileMessageModel> ShowProfile(Chat chat);
        Task<UserLocationModel> CurentLocation(string UserId);
        Task SetLocation(UserLocationModel userLocation);
        InlineKeyboardMarkup LocationInlineKeyboardMarkup();
        InlineKeyboardMarkup ProfileInlineKeyboardMarkup();
        ReplyKeyboardMarkup SetLocaionReplyKeyboardMarkup();
        FileStream ProfilePhoto();
        string ProfileBanner();
    }
}