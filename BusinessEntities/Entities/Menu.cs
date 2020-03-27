namespace BusinessEntities
{
    public class Menu : IMenu
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuDescription { get; set; }
        public bool IsActive { get; set; }
        public string MenuType { get; set; }
        public Menu()
        {
        }

        public Menu(int menuId, string menuType, string menuName, string menuDescription, bool isActive)
        {
            MenuId = menuId;
            MenuType = menuType;
            MenuName = menuName;
            MenuDescription = menuDescription;
            IsActive = isActive;
        }
    }
}