using System.Runtime.Serialization;

namespace AssetsManagementSystem.Models.Enums
{
    public enum TransferStatus
    {
        [EnumMember(Value = "Pending")]
        Pending = 0,

        [EnumMember(Value = "Approved")]
        Approved = 1,

        [EnumMember(Value = "Rejected")]
        Rejected = 2
    }
}
