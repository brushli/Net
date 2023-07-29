using Lee.EMS.Debugging;

namespace Lee.EMS
{
    public class EMSConsts
    {
        public const string LocalizationSourceName = "EMS";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "70c9b941da394c679b8280783fa52737";
    }
}
