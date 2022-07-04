using Bkpm.ActivityTracker.Debugging;

namespace Bkpm.ActivityTracker
{
    public class ActivityTrackerConsts
    {
        public const string LocalizationSourceName = "ActivityTracker";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = false;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "c336c9bd8c83462ba2f4a7a7864a459f";
    }
}
