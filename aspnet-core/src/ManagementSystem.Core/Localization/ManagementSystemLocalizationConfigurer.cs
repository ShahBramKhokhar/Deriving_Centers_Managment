﻿using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace ManagementSystem.Localization
{
    public static class ManagementSystemLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(AppConstants.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(ManagementSystemLocalizationConfigurer).GetAssembly(),
                        "ManagementSystem.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
