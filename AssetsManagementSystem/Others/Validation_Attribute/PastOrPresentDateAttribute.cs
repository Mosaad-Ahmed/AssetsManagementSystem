namespace AssetsManagementSystem.Others.Validation_Attribute
{
    public class PastOrPresentDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime dateValue)
            {
                return dateValue <= DateTime.Today;
            }
            return true;
        }
    }

}
