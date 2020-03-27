namespace BusinessEntities
{
    public interface IRoom
    {
        int RoomId { get; }
        string RoomType { get; set; }
        int MaxGuests { get; set; }
        string CleaningStatus { get; set; }
        bool IsUsable { get; set; }
        bool AllowSmoking { get; set; }
        bool HasCot { get; set; }
    }
}