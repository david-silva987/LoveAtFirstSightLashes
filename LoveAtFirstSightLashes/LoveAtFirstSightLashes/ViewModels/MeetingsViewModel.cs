using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using LoveAtFirstSightLashes.Models;
using LoveAtFirstSightLashes.Views;

namespace LoveAtFirstSightLashes.ViewModels
{
    public class MeetingsViewModel : BaseViewModel
    {
        public ObservableCollection<Meeting> Meetings { get; set; }
        public Command LoadMeetingsCommand { get; set; }

        public MeetingsViewModel()
        {
            Meetings = new ObservableCollection<Meeting>();
            LoadMeetingsCommand = new Command(async () => await ExecuteLoadMeetingsCommand());

            MessagingCenter.Subscribe<NewRDV, Meeting>(this, "AddItem", async (obj, meeting) =>
            {
                var newMeeting = meeting as Meeting;
                Meetings.Add(newMeeting);
                await DataStore.AddItemAsync(newMeeting);
            });
        }

        async Task ExecuteLoadMeetingsCommand()
        {
            IsBusy = true;

            try
            {
                Meetings.Clear();
                var items = await DataStore.GetMeetingssAsync(true);
                foreach (var meeting in Meetings)
                {
                    Meetings.Add(meeting);
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