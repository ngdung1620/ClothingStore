namespace ClothingStoreBackend.Settings
{
    public class Permission
    {
        public string Name { get; set; }
        public int Type { get; set; }
        public string DisplayName { get; set; }

        public Permission(string displayName,string name,int type)
        {
            Name = name;
            Type = type;
            DisplayName = displayName;
        }
    }
}