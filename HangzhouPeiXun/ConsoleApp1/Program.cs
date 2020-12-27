using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HangzhouPeiXun;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string json= "[{\"AbId\":16,\"Volatage\":100.5,\"Electricity \":23.09,\"Wattage\":500.935,\"Scription\":\"一切正常 \",\"SuberName\":\"王五 \"}]";
            DataTable dataTable = new HangzhouPeiXun.Helper.jstodt().ToDataTable(json);
            
            foreach(DataRow dataRow in dataTable.Rows)
            {
                Console.WriteLine(dataRow[2]);
            }
            Console.ReadKey();
        }
    }
}
