using OCM.Debugging;

namespace OCM
{
    public class OCMConsts
    {
        public const string LocalizationSourceName = "OCM";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "c750f7de8a534570923cc79e31362597";
    }
}
