using Newtonsoft.Json;
using QueryBuilder;
using System;
using System.IO;

namespace Program
{
    public class Reader
    {

        private string JsonSql { get; set; }
        public Reader(Arguments arguments)
        {
            try
            {
                JsonSql = File.ReadAllText(arguments.FileJson);
            }
            catch (Exception ex)
            {

                throw new Exception("Unable open the JSON file. Error:", ex);
            }

          
        }

        public ReaderQuery ReaderJson()
        {
            try
            {
                return JsonConvert.DeserializeObject<ReaderQuery>(JsonSql);
            }
            catch (Exception ex)
            {

                throw new Exception("Unable generate SQL. Invalid Json. Error:", ex);
            }

        }
    }
}
