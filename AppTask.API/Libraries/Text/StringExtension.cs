using System.Text;

namespace AppTask.API.Libraries.Text
{
    public static class StringExtension
    {
        public static string GenerateHash(this string s,int lengh)
        {
            StringBuilder sb = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < lengh; i++) 
            {
                sb.Append(random.Next(0,9));
            }
            return sb.ToString();
        }
    }
}
