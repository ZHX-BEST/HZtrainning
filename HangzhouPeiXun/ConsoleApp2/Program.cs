using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HangzhouPeiXun;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            string json = "[{\"AbId\":\"3\",\"Volatage\":\"100.5\",\"Electricity \":\"23.09\",\"Wattage\":\"500.935\",\"Scription\":\"一切正常 \",\"SuberName\":\"王五 \"}]";
            HangzhouPeiXun.Controllers.SolveListController solveListController = new HangzhouPeiXun.Controllers.SolveListController();
            //DataTable dataTable = solveListController.PostSolve(json);
            string res= solveListController.PostSolve(json);
            //int count = dataTable.Rows.Count;
            //DataRow dataRow = dataTable.Rows[0];
            //Console.WriteLine(dataRow["AbId"]);
            Console.WriteLine(res);
            Console.ReadKey();
        }
    }
}
