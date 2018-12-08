﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using RemindMe.Core.Models;
using RemindMe.Core.Repositories;
using RemindMe.Core.Services;
using RemindMe.Core.ViewModels;
using RemindMe.Test.Fakes;
using RemindMe.Test.Helpers;
using System;

namespace RemindMe.Test
{
    [TestClass]
    public class ReminderValidationTests
    {
        private ReminderEditViewModel _fakedReminderViewModel;

        [TestInitialize]
        public void Init()
        {
            _fakedReminderViewModel = GetReminderViewModelWithFakes();
        }

        [TestMethod]
        public void AReminderAlreadyPastSinceTenMinutesMustBeInvalid()
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.UtcNow.AddMinutes(-10);
            Reminder reminder = ReminderDatasetProvider.SingletonInstance.GetReminderWithoutCommentByDefiningTimestamp(dateTimeOffset);

            bool isReminderValid = IsReminderValid(reminder);

            Assert.IsFalse(isReminderValid);
        }

        [TestMethod]
        public void AReminderAlreadyPastSinceFortySevenHoursMustBeInvalid()
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.UtcNow.AddHours(1).AddDays(-2);
            Reminder reminder = ReminderDatasetProvider.SingletonInstance.GetReminderWithoutCommentByDefiningTimestamp(dateTimeOffset);

            bool isReminderValid = IsReminderValid(reminder);

            Assert.IsFalse(isReminderValid);
        }

        [TestMethod]
        public void AReminderThatNotifyInOneHourMustBeValid()
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.UtcNow.AddHours(1);
            Reminder reminder = ReminderDatasetProvider.SingletonInstance.GetReminderWithoutCommentByDefiningTimestamp(dateTimeOffset);

            bool isReminderValid = IsReminderValid(reminder);

            Assert.IsTrue(isReminderValid);
        }

        [TestMethod]
        public void AReminderWithoutTitleMustBeInvalid()
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.UtcNow.AddMinutes(5);
            Reminder reminder = ReminderDatasetProvider.SingletonInstance.GetReminderWithoutTitleByDefiningTimestamp(dateTimeOffset);

            bool isReminderValid = IsReminderValid(reminder);

            Assert.IsFalse(isReminderValid);
        }

        [TestMethod]
        public void AReminderWithoutCommentMustBeValid()
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.UtcNow.AddMinutes(5);
            Reminder reminder = ReminderDatasetProvider.SingletonInstance.GetReminderWithoutCommentByDefiningTimestamp(dateTimeOffset);

            bool isReminderValid = IsReminderValid(reminder);

            Assert.IsTrue(isReminderValid);
        }

        [TestMethod]
        public void AReminderWithoutDateMustBeInvalid()
        {
            Reminder reminder = ReminderDatasetProvider.SingletonInstance.GetReminderWithoutDateByDefiningTimestamp();

            bool isReminderValid = IsReminderValid(reminder);

            Assert.IsFalse(isReminderValid);
        }

        private ReminderEditViewModel GetReminderViewModelWithFakes()
        {
            DialogServiceDummy dialogService = new DialogServiceDummy();
            DatabaseConnectionFake connectionService = DatabaseConnectionFake.SingletonInstance;

            ReminderRepository repository = new ReminderRepository(connectionService);
            ReminderDataService dataService = new ReminderDataService(repository);
            ReminderEditViewModel viewModel = new ReminderEditViewModel(dataService, dialogService);

            return viewModel;
        }

        private bool IsReminderValid(Reminder reminder)
        {
            _fakedReminderViewModel.SelectedReminder = reminder;

            PrivateObject privateFakedReminderViewModel = new PrivateObject(_fakedReminderViewModel);
            privateFakedReminderViewModel.Invoke("SetReminderDayAndTime");
            return (bool)privateFakedReminderViewModel.Invoke("Validate");
        }
    }
}
