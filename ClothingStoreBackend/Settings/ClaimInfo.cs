using System.Collections.Generic;

namespace ClothingStoreBackend.Settings
{
    public class ClaimInfo
    {
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public List<Permission> Permissions { get; set; }

        public ClaimInfo(string displayName, string name, List<Permission> permissions)
        {
            DisplayName = displayName;
            Name = name;
            Permissions = permissions;
        }
    }
}