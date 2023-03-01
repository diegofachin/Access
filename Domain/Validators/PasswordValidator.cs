namespace Domain.Validators;

public class PasswordValidator
{
    public static bool Validate(string password)
    {
        if (password.Length < 8 || password.Length > 200)
            return false;

        if (!password.Any(char.IsUpper))
            return false;

        if (!password.Any(char.IsLower))
            return false;

        if (password.Contains(" "))
            return false;

        string specialCharacter = @"%!@#$%^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
        char[] specialCharacterArray = specialCharacter.ToCharArray();
        foreach (char ch in specialCharacterArray)
        {
            if (password.Contains(ch))
                return true;
        }

        return true;
    }
}
