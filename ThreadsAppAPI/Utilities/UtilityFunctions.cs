using System.Net.Mail;
using System.Text.RegularExpressions;

namespace ThreadsAppAPI.Utilities;

public class UtilityFunctions
{
    public static bool IsEmailValid(string emailaddress)
    {
        try
        {
            MailAddress m = new MailAddress(emailaddress);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    } 
    
    public static bool IsPasswordValid(string password)
    {
        // Validate strong password
        Regex validateGuidRegex = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$");
        return validateGuidRegex.IsMatch(password);
    }
}

