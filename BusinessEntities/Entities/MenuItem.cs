namespace BusinessEntities
{
    public class MenuItem : IMenuItem
    {
        public int MenuItemId { get; set; }
        //public int? MenuId { get; set; }
        public string MenuItemName { get; set; }
        public decimal ItemPrice { get; set; }
        public string AllergyInfo { get; set; }
        int IMenuItem.MenuId { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public MenuItem()
        {

        }
        public MenuItem(int menuItemId )
        {
            MenuItemId = menuItemId;
            //MenuId = 0;
            MenuItemName = " ";
            ItemPrice =  0;
            AllergyInfo = " ";
        }

        public MenuItem(int menuItemId, /*int? menuId,*/ string menuItemName, decimal itemPrice, string allergyInfo)
        {
           this. MenuItemId = menuItemId;
          // this. MenuId = menuId;
            this.MenuItemName = menuItemName;
            this.ItemPrice = itemPrice;
          this.  AllergyInfo = allergyInfo;
        }
    }
}