﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KtMobileApp
{
    public class BaseContentPage: ContentPage
    {
        public void SendAppearing()
        {
            OnAppearing();
        }

        public void SendDisappearing()
        {
            OnDisappearing();
        }
    }
}
