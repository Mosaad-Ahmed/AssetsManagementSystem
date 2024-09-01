using System.Runtime.Serialization;

namespace AssetsManagementSystem.Models.Enums
{
    public enum UserRole
    {
        [EnumMember(Value = "USER")]
        user=0,

        [EnumMember(Value = "MANAGER")]
        manager=1,

        [EnumMember(Value = "AUDITOR")]
        auditor=2


    }
}
