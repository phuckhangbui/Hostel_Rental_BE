using System.Runtime.Serialization;

namespace DTOs.Enum
{
    public enum HostelTypeEnum
    {
        [EnumMember(Value = "On-campus dormitories")]
        OnCampusDormitories,
        [EnumMember(Value = "Off-campus dormitories")]
        OffCampusDormitories,
        [EnumMember(Value = "Serviced apartments")]
        ServicedApartments,
        [EnumMember(Value = "Homestay")]
        Homestay,
        [EnumMember(Value = "Private rentals")]
        PrivateRentals,
        [EnumMember(Value = "Shared houses")]
        SharedHouses,
    }

    public static class HostelTypeExtensions
    {
        public static string ToFriendlyString(this HostelTypeEnum hostelType)
        {
            var enumMemberAttribute = hostelType.GetType()
                .GetMember(hostelType.ToString())[0]
                .GetCustomAttributes(typeof(EnumMemberAttribute), false);

            if (enumMemberAttribute.Length > 0)
            {
                var attribute = (EnumMemberAttribute)enumMemberAttribute[0];
                return attribute.Value.Replace("-", " ");
            }

            return hostelType.ToString();
        }
    }
}
