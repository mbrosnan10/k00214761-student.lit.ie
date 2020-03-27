namespace BusinessEntities
{
    public interface IMenu
    {
        int MenuId { get; set; }
        string MenuType { get; set; }
        string MenuName { get; set; }
        string MenuDescription { get; set; }
        bool IsActive { get; set; }
    }
}