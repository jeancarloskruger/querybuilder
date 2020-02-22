using QueryBuilder;
using System;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var arguments = new Arguments(args);
                var reader = new Reader(arguments);
                var factory = new QueryFactory(reader.ReaderJson());
                factory.PrintQuery();
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
    }
}
