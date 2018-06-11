﻿using MvvmCross.Platform.Converters;
using System;
using System.Globalization;

namespace RemindMe.Core.Converters
{
    public class TimestampToReadableDateValueConverter : MvxValueConverter<long, string>
    {
        protected override string Convert(long value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(value);
            DateTime date = dateTimeOffset.LocalDateTime;

            string dayOfWeek = date.ToString("dddd");
            string month = date.ToString("MMM");

            if (culture.Name == "en-US")
            {
                return string.Format("{0}, {1} {2}, {3}", dayOfWeek, month, date.Day, date.Year);
            }
            else
            {
                return string.Format("{0} {1} {2} {3}", dayOfWeek, date.Day, month, date.Year);
            }
        }
    }
}
