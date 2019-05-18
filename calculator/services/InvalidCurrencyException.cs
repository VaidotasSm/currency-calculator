using System;

namespace calculator.services
{
    public class InvalidCurrencyException : BusinessException
    {
        public string InvalidValue { get; set; }
        public InvalidCurrencyException(string invalidValue) : base($"Invalid Currency '{invalidValue}'")
        {
            InvalidValue = invalidValue;
        }
    }
}