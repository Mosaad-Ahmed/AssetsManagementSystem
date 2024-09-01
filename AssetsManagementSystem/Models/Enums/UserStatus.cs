using System.Runtime.Serialization;

namespace AssetsManagementSystem.Models.Enums
{
    public enum UserStatus
    {
        [EnumMember(Value = "Active")]
        Active = 0,

        [EnumMember(Value = "Inactive")]
        Inactive = 1,

        [EnumMember(Value = "Suspended")]
        Suspended = 2
    }
}
