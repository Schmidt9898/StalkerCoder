using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
//using System.Threading.Tasks;


namespace ablak
{
    class Database
    {


        //string filestring = "MB01:Katus$hka01,MB02:K@ble02";

        Dictionary<string,string> tree = new Dictionary<string, string>();

        SHA256 hash;
        
        public Database(string filename)
        {
            hash = SHA256.Create();
            //string[] splittomb = filestring.Split(',');//todo
            string[] sorok = System.IO.File.ReadAllLines(filename);



            foreach(string sor in sorok)
            {
                string[] temp = sor.Split(':');
                tree.Add(temp[0], temp[1]);
            }
        }

        public string getPass(string key)
        {

            byte[] data = hash.ComputeHash(Encoding.UTF8.GetBytes(key));

            key = Convert.ToBase64String(data);



            if (tree.ContainsKey(key))
            {
                return tree[key];
            }else
            {
                return null;
            }
        }




    }
}
