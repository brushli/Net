using DbUp;
using DbUp.Engine;
using DbUp.Engine.Transactions;
using DbUp.Helpers;
using DbUp.Oracle;
using DbUp.ScriptProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DBUPTest
{
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly static string sqlDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SQL_object\\");
        /// <summary>
        /// 
        /// </summary>
        private readonly static string connectionString = "DATA SOURCE=192.168.7.7/his;PASSWORD=mhsoft;PERSIST SECURITY INFO=True;USER ID=mhsoft;";
        public const string SCHEMA = "MHSOFT";
        public const string TABLENAME = "T_SYS_DBUP";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try
            {
                var aa = Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory+"\\DBUPTestServer.exe").CustomAttributes.FirstOrDefault(f=>f.AttributeType.Equals(typeof(AssemblyVersionAttribute)));
                //string input = "3.4.5.6";
                //int[] output = input.Split('.').Select(int.Parse).ToArray();
                var files = new List<FileInfo>();
                string[] fileNames = Directory.GetFiles(sqlDirectory);
                int[] startVersion = { 5, 3, 5, 3 };
                int[] endVersion = { 5, 3, 6, 3 };
                var filteredFileNames = fileNames.Where(fileName =>
                {
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                    string[] parts = fileNameWithoutExtension.Split('_');
                    if (parts.Length != 3) return false;

                    string[] versionParts = parts[0].Split('.');
                    if (versionParts.Length != 4) return false;

                    bool isValidVersion = true;
                    for (int i = 0; i < versionParts.Length; i++)
                    {
                        int versionPart = int.Parse(versionParts[i]);
                        if (versionPart < startVersion[i] || versionPart > endVersion[i])
                        {
                            isValidVersion = false;
                            break;
                        }
                    }
                    return isValidVersion;
                }).Select(s=> Path.GetFileName(s)).ToList();
                Console.WriteLine("此次需要执行的脚本有以下：");
                foreach (string fileName in filteredFileNames)
                {
                    Console.WriteLine(fileName);
                }
                Console.Write("确认是否执行？[y/n],默认y：");
                var input=Console.ReadKey();
                if (input.Key==ConsoleKey.Enter|| input.Key==ConsoleKey.Y)
                {
                    SqlStart(filteredFileNames);
                }
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.ReadKey();
            }
            

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileNames"></param>
        static void SqlStart(List<string> fileNames)
        {
            // Create a DbUp migration runner
            var upgrader = DeployChanges.To
                .OracleDatabaseWithDefaultDelimiter(connectionString)
                //编码格式Encoding在默认的是UTF-8，但是经过测试，PLSQL保存出来的是默认格式
                .WithScripts(
                new FileSystemScriptProvider(sqlDirectory,
                new FileSystemScriptOptions()
                {
                    Extensions = new[] { "*.sql", "*.txt", "*.prc", "*.func" },
                    Filter = filea => fileNames.Contains(filea),
                    Encoding = Encoding.Default,
                    UseOnlyFilenameForScriptName = true
                }))
                .LogToConsole()
                .WithPreprocessor(new MyPreprocessor());
            upgrader.Configure(c =>
            {
                c.Journal = new OracleTableJournal(() => c.ConnectionManager, () => c.Log, SCHEMA, TABLENAME);
                c.ScriptExecutor = new MyScriptExecutor(() => c.ConnectionManager, () => c.Log, null, () => c.VariablesEnabled, c.ScriptPreprocessors, () => c.Journal);                
            });
            var ss = upgrader.Build();
            var result = ss.PerformUpgrade();

            // Display the result
            if (result.Successful)
            {
                Console.WriteLine("Success!");
            }
            else
            {
                Console.WriteLine("Error: " + result.Error.Message);
                Console.WriteLine(result.ErrorScript.Contents);
            }
        }
        private static void Upgrader_ScriptExecuted(object sender, EventArgs e)
        {
            var ss = sender;
        }
    }
}
