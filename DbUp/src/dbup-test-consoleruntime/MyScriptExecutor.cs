using DbUp.Engine;
using DbUp.Engine.Output;
using DbUp.Engine.Transactions;
using DbUp.Oracle;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;

namespace DBUPTest
{
    class MyScriptExecutor : OracleScriptExecutor
    {
        /// <summary>
        /// Initializes an instance of the <see cref="OracleScriptExecutor"/> class.
        /// </summary>
        /// <param name="connectionManagerFactory"></param>
        /// <param name="log">The logging mechanism.</param>
        /// <param name="schema">The schema that contains the table.</param>
        /// <param name="variablesEnabled">Function that returns <c>true</c> if variables should be replaced, <c>false</c> otherwise.</param>
        /// <param name="scriptPreprocessors">Script Preprocessors in addition to variable substitution</param>
        /// <param name="journalFactory">Database journal</param>
        public MyScriptExecutor(Func<IConnectionManager> connectionManagerFactory, Func<IUpgradeLog> log, string schema, Func<bool> variablesEnabled,
            IEnumerable<IScriptPreprocessor> scriptPreprocessors, Func<IJournal> journalFactory)
            : base(connectionManagerFactory, log, schema, variablesEnabled, scriptPreprocessors, journalFactory)
        {

        }
        protected override void ExecuteCommandsWithinExceptionHandler(int index, SqlScript script, Action executeCommand)
        {
            try
            {
                executeCommand();
            }
            catch (OracleException exception)
            {
                //Log().WriteInformation("Oracle exception has occured in script: '{0}'", script.Name);
                Log().WriteError("{1}遇到错误： {2}", script.Name, script.ExecuteCommand?.Substring(0, Math.Min(script.Contents.Length, 50)), exception.Message);
                //Log().WriteError(exception.ToString());
                //throw;
            }
        }
    }
}
