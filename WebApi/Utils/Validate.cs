namespace Utils
{
    public static class Validate
    {
        public static bool IsNullOrEmptyOrWhiteSpace(string? value)
        {
            if(string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value)) return false;
            return true;
        }
    }
}