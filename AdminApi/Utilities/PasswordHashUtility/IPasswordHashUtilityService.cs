namespace AdminApi.Utilities.PasswordHashUtility
{
    public interface IPasswordHashUtilityService
    {
        public bool VerifyPassword(string password, byte[] dbPassword);
    }
}
