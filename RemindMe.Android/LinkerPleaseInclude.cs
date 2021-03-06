﻿using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Input;
using Android.App;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Droid.Views;
using RemindMe.Core.Models;

namespace RemindMe.Android
{
    // This is a dummy class to force MvvmCross link (Linker issue)
    // Application crashes everywhere without it, in release mode
    // This is just read by the linker, it's not instancied or whatever
    public class LinkerPleaseInclude
    {
        public void Include(Button button)
        {
            button.Click += (s, e) => button.Text = button.Text + "";
        }

        public void Include(View view)
        {
            view.Click += (s, e) => view.ContentDescription = view.ContentDescription + "";
        }

        public void Include(TextView text)
        {
            text.AfterTextChanged += (sender, args) => text.Text = "" + text.Text;
            text.Hint = "" + text.Hint;
            text.Error = "" + text.Error;
            text.SetError("" + text.Error, null);
        }

        public void Include(EditText text)
        {
            text.AfterTextChanged += (sender, args) => text.Text = "" + text.Text;
            text.Hint = "" + text.Hint;
            text.Error = "" + text.Error;
            text.SetError("" + text.Error, null);
        }

        public void Include(Activity act)
        {
            act.Title = act.Title + "";
            var test = act.Title;
        }

        public void Include(INotifyCollectionChanged changed)
        {
            changed.CollectionChanged += (s, e) => { var test = $"{e.Action}{e.NewItems}{e.NewStartingIndex}{e.OldItems}{e.OldStartingIndex}"; };
        }

        public void Include(ICommand command)
        {
            command.CanExecuteChanged += (s, e) => { if (command.CanExecute(null)) command.Execute(null); };
        }

        public void Include(MvvmCross.Platform.IoC.MvxPropertyInjector injector)
        {
            injector = new MvvmCross.Platform.IoC.MvxPropertyInjector();
        }

        public void Include(System.ComponentModel.INotifyPropertyChanged changed)
        {
            changed.PropertyChanged += (sender, e) => {
                var test = e.PropertyName;
            };
        }

        public void Include(MvxTaskBasedBindingContext context)
        {
            context.Dispose();
            var context2 = new MvxTaskBasedBindingContext();
            context2.Dispose();
        }

        public void Include(RelativeLayout relativeLayout)
        {
            relativeLayout.Visibility = ViewStates.Visible;
            var test = relativeLayout.Visibility;
        }

        public void Include(MvxListView list)
        {
            list.ItemsSource = new List<Reminder>();
            var itemsSource = list.ItemsSource;
        }
    }
}