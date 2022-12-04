using System;

namespace NextBiggerTask
{
    public static class NumberExtension
    {
        /// <summary>
        /// Finds the nearest largest integer consisting of the digits of the given positive integer number and null if no such number exists.
        /// </summary>
        /// <param name="number">Source number.</param>
        /// <returns>
        /// The nearest largest integer consisting of the digits of the given positive integer and null if no such number exists.
        /// </returns>
        /// <exception cref="ArgumentException">Thrown when source number is less than 0.</exception>
        public static int? NextBiggerThan(int number)
        {
            _ = number >= 0 ? number : throw new ArgumentException($"Value of parameter {nameof(number)} cannot be negative.");

            // 1st step - searching for the biggest digit from right to left.
            int numberCopy = number;
            int remainder = 0;
            int previousRemainder;
            int tens = 10;
            for (; tens <= numberCopy * 10; tens *= 10)
            {
                previousRemainder = remainder;
                remainder = (numberCopy % tens) / (tens / 10);

                if (previousRemainder > remainder)
                {
                    numberCopy -= numberCopy % tens;

                    int remainderToAdd = previousRemainder * (tens / 10);
                    remainderToAdd += remainder * (tens / 100);
                    remainderToAdd += number % (tens / 100);

                    numberCopy += remainderToAdd;
                    break;
                }
            }

            // 2nd step - check for possible less values.
            int remainderToCheck = numberCopy % tens;
            if (remainderToCheck == number)
            {
                return null;
            }

            for (int outerDigit = 10; outerDigit <= tens; outerDigit *= 10)
            {
                int mostLeft = remainderToCheck % 10;
                for (int innerDigit = 100; innerDigit < tens; innerDigit *= 10)
                {
                    int compareWithMostLeft = (remainderToCheck % innerDigit) / (innerDigit / 10);
                    if (mostLeft < compareWithMostLeft)
                    {
                        int additionalRemainder = remainderToCheck % (innerDigit / 100);
                        remainderToCheck -= remainderToCheck % innerDigit;
                        remainderToCheck += (mostLeft * (innerDigit / 10)) + (compareWithMostLeft * (innerDigit / 100)) + additionalRemainder;
                    }
                }
            }

            numberCopy -= numberCopy % tens;
            numberCopy += remainderToCheck;
            return numberCopy == number ? null : numberCopy;
        }
    }
}
