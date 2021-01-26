using System;

using LoveAtFirstSightLashes.Models;

namespace LoveAtFirstSightLashes.ViewModels
{
    public class MeetingDetailViewModel : BaseViewModel
    {
        public Meeting Meeting { get; set; }
        public MeetingDetailViewModel(Meeting meeting = null)
        {
            
            Meeting = meeting;
        }
    }
}
