
namespace StreamsOfSound.Services
{
    public class PasswordGenerator
    {
        private static readonly Random Random = new Random();
        private static readonly char[] specialChars = { '@', '#', '$', '%', '*', '+' };
        private static readonly char[] upperAlphabet = { 'A', 'B', 'C', 'D', 'E', 'F',
                                                         'G', 'H', 'I', 'J', 'K', 'L', 
                                                         'M', 'N', 'O', 'P', 'Q', 'R', 
                                                         'S', 'T', 'U', 'V', 'W', 'X', 
                                                         'Y', 'Z'};
        private static readonly char[] lowerAlphabet = { 'a', 'b', 'c', 'd', 'e', 'f',
                                                         'g', 'h', 'i', 'j', 'k', 'l', 
                                                         'm', 'n', 'o', 'p', 'q', 'r', 
                                                         's', 't', 'u', 'v', 'w', 'x', 
                                                         'y', 'z'};
        private static readonly char[] numbers = { '0', '1', '2', '3', '4', '5',
                                                   '6', '7', '8', '9' };

        public static string GeneratePassword()
        {
            char[] password = new char[10];

            password[0] = upperAlphabet[Random.Next(0, 26)];

            password[1] = numbers[Random.Next(0, 10)];

            password[2] = specialChars[Random.Next(0, 6)];

            password[3] = lowerAlphabet[Random.Next(0, 26)];

            for (int i = 4; i < 10; i++)
            {
                char[] charSet = Random.Next(0, 2) == 0 ? upperAlphabet : numbers;
                password[i] = charSet[Random.Next(0, charSet.Length)];
            }

            return new string(password);
        }
    }
}
