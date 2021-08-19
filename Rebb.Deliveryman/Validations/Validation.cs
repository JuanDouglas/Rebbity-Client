using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Views;
namespace Rebb.Tools
{
    public class Validation
    {
        char[] valuesForIsClear = { '@', '.', '-', '/', ',' };
        public bool ValidationCPF(string cpf)
        {

            bool vlreturn = false;
            string cpfClear = "";
            if (cpf.Length != 0)
            {
                cpfClear = formatZero(cpf);
                cpfClear = cpfClear.Insert(9, "-");
                cpfClear = cpfClear.Insert(6, ".");
                cpfClear = cpfClear.Insert(3, ".");
                vlreturn = true;
            }
            Formatting.resultado = cpfClear;
            return vlreturn;
        }

        public bool ValidationEmail(string email)
        {
            bool vlreturn = false;
            if (email.Length != 0)
            {

            }
            return vlreturn;
        }

        public bool ValidationPhoneNumber(string PhoneNumber)
        {
            bool vlreturn = false;
            string phoneClear = "";
            if (PhoneNumber.Length != 0)
            {
                phoneClear = formatZero(PhoneNumber);
                phoneClear = phoneClear.Insert(0, "(");
                phoneClear = phoneClear.Insert(2, ")");
                phoneClear = phoneClear.Insert(6, ".");
                phoneClear = phoneClear.Insert(3, ".");
                vlreturn = true;
            }
            Formatting.resultado = phoneClear;
            return vlreturn;
        }

        private string formatZero(string formatValue)
        {
            string resultValue = "";

            string[] extra = formatValue.Split();
            for (int i = 0; i < formatValue.Length; i++)
            {
                for (int ii = 0; ii < valuesForIsClear.Length; ii++)
                {
                    if (valuesForIsClear[ii] == Convert.ToChar(extra[i]))
                    {
                        extra[i] = "";
                    }
                }
            }
            for (int i = 0; i < formatValue.Length; i++)
            {
                resultValue += extra[i];
            }            
            return resultValue;
        }
    }


}
