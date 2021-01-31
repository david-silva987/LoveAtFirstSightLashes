namespace LoveAtFirstSightLashes.Models
{
    public enum MenuItemType
    {
        MyMeeting,
        SearchClient,
        AddClient
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}