using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Stalkerkodolo
{
    class generatorcodolo
    {
        string randomdatafilename = "names.txt";
        char[] peter = { '@', '&', '#', '$', '!','ß','\''};
        string[] allname;
        Dictionary<string, string> all_key_value;
        List<KeyValuePair<string, string>> valid_keys;
        SHA256 hash;
        Random rng;


        public generatorcodolo()
        {
            allname=System.IO.File.ReadAllLines(randomdatafilename);
            valid_keys = new List<KeyValuePair<string, string>>();
            all_key_value = new Dictionary<string, string>();
            hash = SHA256.Create();
            rng = new Random();
        }
        public void code_valid(string filename)
        {
            string[] sorok = System.IO.File.ReadAllLines(filename);



            foreach (string sor in sorok)
            {
                string[] temp = sor.Split(':');

                byte[] data = hash.ComputeHash(Encoding.UTF8.GetBytes(temp[0]));

                string hashedkey = Convert.ToBase64String(data);

                valid_keys.Add(new KeyValuePair<string, string>(hashedkey, temp[1]));
            }
        }
        KeyValuePair<string,string> generateRow()
        {
             byte[] data= hash.ComputeHash(Encoding.UTF8.GetBytes(rng.Next().ToString()));
                string randomhash = Convert.ToBase64String(data);

            string randomname = allname[rng.Next(allname.Length)];

            if(randomname.Length<5)
            {
                randomname += allname[rng.Next(allname.Length)];
            }
            randomname=randomname.Insert(rng.Next(randomname.Length - 1), rng.Next(100).ToString());

            randomname = randomname.Insert(rng.Next(randomname.Length - 1), peter[rng.Next(peter.Length)].ToString());
            if(rng.Next(2)==1)
            {
                randomname = randomname.Insert(rng.Next(randomname.Length - 1), peter[rng.Next(peter.Length)].ToString());
            }

            //Console.WriteLine(randomhash+"   "+randomname);
            KeyValuePair<string, string> return_ = new KeyValuePair<string, string>(randomhash, randomname);
            return return_;
        }
        public void generate_file(int n,string outputfilename)
        {

            int skip = (n - 1) / (valid_keys.Count+2);
            int s = 0;
            int v_id = 0;
            for (int i = 0; i < n; i++)
            {


                var rngkey=generateRow();
                if(s>=skip)
                {
                    //validkey
                    if(v_id<valid_keys.Count)
                    {
                        var T = valid_keys[v_id];
                        all_key_value.Add(T.Key, T.Value);
                        v_id++;
                    }
                    s = 0;
                    continue;
                }else
                {
                    while(all_key_value.ContainsKey(rngkey.Key))
                    {
                        rngkey = generateRow();
                    }
                    all_key_value.Add(rngkey.Key,rngkey.Value);
                }
                s++;
            }
            if(v_id==valid_keys.Count)
            {
                Console.WriteLine("ok");
            }
            else
            {
                Console.WriteLine("nem ok");
            }


            //write to file
            List<string> kimenet = new List<string>();
            foreach(var sor in all_key_value)
            {
                kimenet.Add(sor.Key + ":" + sor.Value);
            }
            System.IO.File.WriteAllLines(outputfilename,kimenet);


        }

    }
}
