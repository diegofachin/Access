using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validators;

public class NumberCardValidator
{
    public static bool Validate(string numberCard)
    {
        if (numberCard == null)
            return false;

        numberCard = numberCard.Replace("-", "");
        numberCard = numberCard.Replace(" ", "");
        int num = 0;
        bool flag = false;

        foreach (char current in numberCard.Reverse<char>())
        {
            if (current < '0' || current > '9')
            {
                return false;
            }

            int i = (int)((current - '0') * (flag ? '\u0002' : '\u0001'));
            flag = !flag;

            while (i > 0)
            {
                num += i % 10;
                i /= 10;
            }
        }

        return num % 10 == 0;
    }
}
