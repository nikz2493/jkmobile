namespace Utility
{
    public interface IGenerator
    {
        string GenerateHtml(int verificationCode);
        int GetVerificationCode(int noOfDigits);
        string GetVerificationHtml(int totalDigits, int verificationCode = 0);
    }
}