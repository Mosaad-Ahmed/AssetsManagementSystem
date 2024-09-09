namespace AssetsManagementSystem.Others.Validation_Attribute
{
    public class PastOrPresentDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return true;

            if (value is DateOnly dateValue)
            {
                return dateValue <= DateOnly.FromDateTime(DateTime.Now);  
            }
            return false;
        }
    }

}
