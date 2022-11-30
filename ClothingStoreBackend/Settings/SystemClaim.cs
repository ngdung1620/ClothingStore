using System.Collections.Generic;

namespace ClothingStoreBackend.Settings
{
    public class SystemClaim
    {
        public static List<ClaimInfo> Claims = new List<ClaimInfo>()
        {
            UIClaims.AccessPermissionClaims,
            UIClaims.GroupCategoryClaims,
            UIClaims.CategoryClaims
        };
    }
}