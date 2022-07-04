using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace Bkpm.ActivityTracker.Localization
{
    public static class ActivityTrackerLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(ActivityTrackerConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(ActivityTrackerLocalizationConfigurer).GetAssembly(),
                        "Bkpm.ActivityTracker.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
