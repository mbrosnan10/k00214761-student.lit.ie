namespace BusinessEntities
{
    public interface IMenuItem
    {
        int MenuItemId { get; set; }
        //int? MenuId { get; set; }
        string MenuItemName { get; set; }
        decimal ItemPrice { get; set; }
        string AllergyInfo { get; set; }
        int MenuId { get; set; }
    }
}