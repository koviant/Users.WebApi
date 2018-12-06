using System;
using System.Configuration;

namespace ConfigConstants
{

    public static class ConfigConstants
    {
        public static string DatabaseName { get; }

        public static string UsersTableName { get; }

        public static int UserFirstNameLength { get; }

        public static int UserLastNameLength { get; }

        public static int UserLoginNameLength { get; }

        public static int UserPasswordLength { get; }

        public static string AddressesTableName { get; }

        public static int AddressDescriptionLength { get; }

        public static int AddressValueLength { get; }

        static ConfigConstants()
        {
            DatabaseName = GetValueFromConfig("databaseName");

            UsersTableName = GetValueFromConfig("usersTableName");
            UserFirstNameLength = GetIntValueFromConfig("userFirstNameLength");
            UserLastNameLength = GetIntValueFromConfig("userLastNameLength");
            UserLoginNameLength = GetIntValueFromConfig("userLoginNameLength");
            UserPasswordLength = GetIntValueFromConfig("userPasswordLength");

            AddressesTableName = GetValueFromConfig("addressesTableName");
            AddressDescriptionLength = GetIntValueFromConfig("addressDescriptionLength");
            AddressValueLength = GetIntValueFromConfig("addressValueLength");
        }

        private static int GetIntValueFromConfig(string key) => int.Parse(GetValueFromConfig(key));

        private static string GetValueFromConfig(string key) => ConfigurationManager.AppSettings[key];
    }
}
