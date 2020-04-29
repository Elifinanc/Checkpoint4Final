using Nancy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ApiRestCheckpoint4
{
    class FileModule : NancyModule
    {
        public FileModule()
        {
            Get("/order/{login}", parameters => ReturnOrder(parameters.login));
        }

        private object ReturnOrder(string login)
        {

            throw new NotImplementedException();
        }

        public string CreateFile()
        {
            string FilePath = @"C:/Users/konuk/source/repos/test.txt";
            JsonMessage Output = new JsonMessage();
            if (!File.Exists(FilePath))
            {
                using (FileStream fs = File.Create(FilePath))
                {
                       
                    Byte[] title = new UTF8Encoding(true).GetBytes("New Text File");
                    fs.Write(title, 0, title.Length);
                    byte[] author = new UTF8Encoding(true).GetBytes("Author's Name");
                    fs.Write(author, 0, author.Length);
                }
                Output.Message = "A new file is created!";
            }
            string output = JsonConvert.SerializeObject(Output.Message);
            return output;
        }
    }
}
