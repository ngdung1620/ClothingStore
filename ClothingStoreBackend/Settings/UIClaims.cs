
using System.Collections.Generic;

namespace ClothingStoreBackend.Settings
{
    public class UIClaims
    {
        public const string Administration = "Administration";
        public const string Member = "Member";
        public const string GroupCategoryRead = "GroupCategory.Read";
        public const string GroupCategoryWrite = "GroupCategory.Write";
        public const string CategoryRead = "Category.Read";
        public const string CategoryWrite = "Category.Write";

        public static ClaimInfo AccessPermissionClaims = new ClaimInfo("Quyền truy cập", "AccessPermissionClaim",
        new List<Permission>()
        {
            new Permission("Administration",Administration , 1),
            new Permission("Member", Member, 2)
        });

        public static ClaimInfo GroupCategoryClaims = new ClaimInfo("Quản lí nhóm thể loại", "GroupCategoryClaim",
        new List<Permission>()
        {
            new Permission("Đọc",GroupCategoryRead,1),
            new Permission("Ghi",GroupCategoryWrite,2)
        });

        public static ClaimInfo CategoryClaims = new ClaimInfo("Quản lí thể loại","CategoryClaim", new List<Permission>()
        {
            new Permission("Đọc",CategoryRead,1),
            new Permission("Ghi",CategoryWrite,2)
        });

    }
}