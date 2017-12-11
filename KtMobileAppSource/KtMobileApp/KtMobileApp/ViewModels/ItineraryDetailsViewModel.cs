﻿
using KT.BusinessLayer;
using KtMobileApp.Models;
using KtMobileApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KtMobileApp.ViewModels
{
    public class ItineraryDetailsViewModel : BaseViewModel
    {
        public ObservableCollection<ItineraryDailyBreakDown> ItineraryDailyBreakDown { get; set; }
        public Command LoadItineraryCommand { get; set; }


        private INavigation _navigation; // HERE

        public string Description { get; set; }
        public string DetailId { get; set; }

        public ItineraryDetailsViewModel(INavigation MainPageNavigation)
        {
            Title = "Trips";
            _navigation = MainPageNavigation;
            ItineraryDailyBreakDown = new ObservableCollection<ItineraryDailyBreakDown>();
            LoadItineraryCommand = new Command(() => ExecuteLoadItemsCommand());            
        }

        void ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                ItineraryDailyBreakDown.Clear();
                Itinerary itineraryManager = new Itinerary();
                DateTime startDate = DateTime.Today.AddDays(-2);
                var itineraryDailyBreakdown = itineraryManager.GetItineraryDays(1);
                int cntr = 0;
                foreach (ItineraryDayDto dto in itineraryDailyBreakdown)
                {
                    cntr++;
                    var newItem = new ItineraryDailyBreakDown(_navigation);
                    //TODO; change after demo
                    dto.ItineraryDayDate = startDate.ToString("MMM dd,yyyy");

                    newItem.DayNumber = dto.Day;
                    newItem.Location = dto.LocationName;
                    newItem.TripDayDate = dto.ItineraryDayDate;
                    newItem.ItineraryDayDescription = dto.CustomServiceDesc;

                    //Check for past days
                    if (startDate < DateTime.Today)
                    {
                        newItem.IsPast = true;
                    }

                    //Set selected item background color for current day
                    if (startDate == DateTime.Today)
                    {
                        newItem.CurrentTripDaySelectedItem = Color.FromHex("FFDEAD");
                    }
                    newItem.ImageResourcePassedPath = "KtMobileApp.Assets.Images.checked_48_48.png";
                    newItem.ImageResourceActivityPath = $"KtMobileApp.Assets.Images.Activity_{cntr}_128_128.png";



                    ItineraryDailyBreakDown.Add(newItem);

                    //Add next day
                    startDate = startDate.AddDays(1);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}