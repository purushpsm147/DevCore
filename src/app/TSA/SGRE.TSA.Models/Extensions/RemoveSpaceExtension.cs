namespace SGRE.TSA.Models.Extensions
{
    public static class RemoveSpaceExtension
    {
        public static string RemoveSpace(this string value)
        {
            if(value != null)
            {
                value = value.Replace(" ",string.Empty);
            }
            return value;
        }
    }
}
