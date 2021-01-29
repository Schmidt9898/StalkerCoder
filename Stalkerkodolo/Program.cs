using System;
using System.Security.Cryptography;
using System.Text;

namespace Stalkerkodolo
{
    class Program
    {
        static void Main(string[] args)
        {


            generatorcodolo gman = new generatorcodolo();
            gman.code_valid("valid.txt");
            int number=0;
            int.TryParse( Console.ReadLine(),out number);
            gman.generate_file(number,"pass.txt");

            /* Console.WriteLine("Hello World!");
             string s = Console.ReadLine();
             SHA256 hash = SHA256.Create();
             byte[] data = hash.ComputeHash(Encoding.UTF8.GetBytes(s));

             string trypass = Convert.ToBase64String(data);
             Console.WriteLine(trypass);*/
            Console.WriteLine("keszvan basszad megfele");
            Console.ReadKey();
            
        }
    }
}
