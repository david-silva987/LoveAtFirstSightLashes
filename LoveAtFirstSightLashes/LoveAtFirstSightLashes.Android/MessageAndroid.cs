﻿
using Android.App;
using Android.Widget;
using LoveAtFirstSightLashes.Droid;
using LoveAtFirstSightLashes.Interfaces;

/**
 * Génération de toasts avec Xamarin Partie Android
 * https://stackoverflow.com/questions/35279403/toast-equivalent-for-xamarin-forms
 */

[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
namespace LoveAtFirstSightLashes.Droid
{
    class MessageAndroid : IMessage
    {

        public void ShortAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
        public void LongAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

    }
}