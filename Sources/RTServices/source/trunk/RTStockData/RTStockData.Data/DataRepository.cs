#region Using directives

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Provider;
using System.Web.Configuration;
using System.Web;
using RTStockData.Entities;
using RTStockData.Data;
using RTStockData.Data.Bases;

#endregion

namespace RTStockData.Data
{
	/// <summary>
	/// This class represents the Data source repository and gives access to all the underlying providers.
	/// </summary>
    [CLSCompliant(true)]
    public sealed class DataRepository
    {
        private static volatile NetTiersProvider _provider = null;
        private static volatile NetTiersProviderCollection _providers = null;
        private static volatile NetTiersServiceSection _section = null;
        private static volatile Configuration _config = null;

        private static object SyncRoot = new object();

        private DataRepository()
        {
        }

        #region Public LoadProvider
        /// <summary>
        /// Enables the DataRepository to programatically create and 
        /// pass in a <c>NetTiersProvider</c> during runtime.
        /// </summary>
        /// <param name="provider">An instatiated NetTiersProvider.</param>
        public static void LoadProvider(NetTiersProvider provider)
        {
            LoadProvider(provider, false);
        }

        /// <summary>
        /// Enables the DataRepository to programatically create and 
        /// pass in a <c>NetTiersProvider</c> during runtime.
        /// </summary>
        /// <param name="provider">An instatiated NetTiersProvider.</param>
        /// <param name="setAsDefault">ability to set any valid provider as the default provider for the DataRepository.</param>
        public static void LoadProvider(NetTiersProvider provider, bool setAsDefault)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");

            if (_providers == null)
            {
                lock (SyncRoot)
                {
                    if (_providers == null)
                        _providers = new NetTiersProviderCollection();
                }
            }

            if (_providers[provider.Name] == null)
            {
                lock (_providers.SyncRoot)
                {
                    _providers.Add(provider);
                }
            }

            if (_provider == null || setAsDefault)
            {
                lock (SyncRoot)
                {
                    if (_provider == null || setAsDefault)
                        _provider = provider;
                }
            }
        }
        #endregion

        ///<summary>
        /// Configuration based provider loading, will load the providers on first call.
        ///</summary>
        private static void LoadProviders()
        {
            // Avoid claiming lock if providers are already loaded
            if (_provider == null)
            {
                lock (SyncRoot)
                {
                    // Do this again to make sure _provider is still null
                    if (_provider == null)
                    {
                        // Load registered providers and point _provider to the default provider
                        _providers = new NetTiersProviderCollection();

                        ProvidersHelper.InstantiateProviders(NetTiersSection.Providers, _providers, typeof(NetTiersProvider));
                        _provider = _providers[NetTiersSection.DefaultProvider];

                        if (_provider == null)
                        {
                            throw new ProviderException("Unable to load default NetTiersProvider");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the provider.
        /// </summary>
        /// <value>The provider.</value>
        public static NetTiersProvider Provider
        {
            get { LoadProviders(); return _provider; }
        }

        /// <summary>
        /// Gets the provider collection.
        /// </summary>
        /// <value>The providers.</value>
        public static NetTiersProviderCollection Providers
        {
            get { LoadProviders(); return _providers; }
        }

        /// <summary>
        /// Creates a new <c cref="TransactionManager"/> instance from the current datasource.
        /// </summary>
        /// <returns></returns>
        public TransactionManager CreateTransaction()
        {
            return _provider.CreateTransaction();
        }

        #region Configuration

        /// <summary>
        /// Gets a reference to the configured NetTiersServiceSection object.
        /// </summary>
        public static NetTiersServiceSection NetTiersSection
        {
            get
            {
                // Try to get a reference to the default <netTiersService> section
                _section = WebConfigurationManager.GetSection("netTiersService") as NetTiersServiceSection;

                if (_section == null)
                {
                    // otherwise look for section based on the assembly name
                    _section = WebConfigurationManager.GetSection("RTStockData.Data") as NetTiersServiceSection;
                }

                #region Design-Time Support

                if (_section == null)
                {
                    // lastly, try to find the specific NetTiersServiceSection for this assembly
                    foreach (ConfigurationSection temp in Configuration.Sections)
                    {
                        if (temp is NetTiersServiceSection)
                        {
                            _section = temp as NetTiersServiceSection;
                            break;
                        }
                    }
                }

                #endregion Design-Time Support

                if (_section == null)
                {
                    throw new ProviderException("Unable to load NetTiersServiceSection");
                }

                return _section;
            }
        }

        #region Design-Time Support

        /// <summary>
        /// Gets a reference to the application configuration object.
        /// </summary>
        public static Configuration Configuration
        {
            get
            {
                if (_config == null)
                {
                    // load specific config file
                    if (HttpContext.Current != null)
                    {
                        _config = WebConfigurationManager.OpenWebConfiguration("~");
                    }
                    else
                    {
                        String configFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Replace(".config", "").Replace(".temp", "");

                        // check for design mode
                        if (configFile.ToLower().Contains("devenv.exe"))
                        {
                            _config = GetDesignTimeConfig();
                        }
                        else
                        {
                            _config = ConfigurationManager.OpenExeConfiguration(configFile);
                        }
                    }
                }

                return _config;
            }
        }

        private static Configuration GetDesignTimeConfig()
        {
            ExeConfigurationFileMap configMap = null;
            Configuration config = null;
            String path = null;

            // Get an instance of the currently running Visual Studio IDE.
            EnvDTE80.DTE2 dte = (EnvDTE80.DTE2)System.Runtime.InteropServices.Marshal.GetActiveObject("VisualStudio.DTE.9.0");

            if (dte != null)
            {
                dte.SuppressUI = true;

                EnvDTE.ProjectItem item = dte.Solution.FindProjectItem("web.config");
                if (item != null)
                {
                    if (!item.ContainingProject.FullName.ToLower().StartsWith("http:"))
                    {
                        System.IO.FileInfo info = new System.IO.FileInfo(item.ContainingProject.FullName);
                        path = String.Format("{0}\\{1}", info.Directory.FullName, item.Name);
                        configMap = new ExeConfigurationFileMap();
                        configMap.ExeConfigFilename = path;
                    }
                    else
                    {
                        configMap = new ExeConfigurationFileMap();
                        configMap.ExeConfigFilename = item.get_FileNames(0);
                    }
                }

                /*
                Array projects = (Array) dte2.ActiveSolutionProjects;
                EnvDTE.Project project = (EnvDTE.Project) projects.GetValue(0);
                System.IO.FileInfo info;

                foreach ( EnvDTE.ProjectItem item in project.ProjectItems )
                {
                    if ( String.Compare(item.Name, "web.config", true) == 0 )
                    {
                        info = new System.IO.FileInfo(project.FullName);
                        path = String.Format("{0}\\{1}", info.Directory.FullName, item.Name);
                        configMap = new ExeConfigurationFileMap();
                        configMap.ExeConfigFilename = path;
                        break;
                    }
                }
                */
            }

            config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
            return config;
        }

        #endregion Design-Time Support

        #endregion Configuration

        #region Connections

        /// <summary>
        /// Gets a reference to the ConnectionStringSettings collection.
        /// </summary>
        public static ConnectionStringSettingsCollection ConnectionStrings
        {
            get
            {
                // use default ConnectionStrings if _section has already been discovered
                if (_config == null && _section != null)
                {
                    return WebConfigurationManager.ConnectionStrings;
                }

                return Configuration.ConnectionStrings.ConnectionStrings;
            }
        }

        // dictionary of connection providers
        private static Dictionary<String, ConnectionProvider> _connections;

        /// <summary>
        /// Gets the dictionary of connection providers.
        /// </summary>
        public static Dictionary<String, ConnectionProvider> Connections
        {
            get
            {
                if (_connections == null)
                {
                    lock (SyncRoot)
                    {
                        if (_connections == null)
                        {
                            _connections = new Dictionary<String, ConnectionProvider>();

                            // add a connection provider for each configured connection string
                            foreach (ConnectionStringSettings conn in ConnectionStrings)
                            {
                                _connections.Add(conn.Name, new ConnectionProvider(conn.Name, conn.ConnectionString));
                            }
                        }
                    }
                }

                return _connections;
            }
        }

        /// <summary>
        /// Adds the specified connection string to the map of connection strings.
        /// </summary>
        /// <param name="connectionStringName">The connection string name.</param>
        /// <param name="connectionString">The provider specific connection information.</param>
        public static void AddConnection(String connectionStringName, String connectionString)
        {
            lock (SyncRoot)
            {
                Connections.Remove(connectionStringName);
                ConnectionProvider connection = new ConnectionProvider(connectionStringName, connectionString);
                Connections.Add(connectionStringName, connection);
            }
        }

        /// <summary>
        /// Provides ability to switch connection string at runtime.
        /// </summary>
        public sealed class ConnectionProvider
        {
            private NetTiersProvider _provider;
            private NetTiersProviderCollection _providers;
            private String _connectionStringName;
            private String _connectionString;


            /// <summary>
            /// Initializes a new instance of the ConnectionProvider class.
            /// </summary>
            /// <param name="connectionStringName">The connection string name.</param>
            /// <param name="connectionString">The provider specific connection information.</param>
            public ConnectionProvider(String connectionStringName, String connectionString)
            {
                _connectionString = connectionString;
                _connectionStringName = connectionStringName;
            }

            /// <summary>
            /// Gets the provider.
            /// </summary>
            public NetTiersProvider Provider
            {
                get { LoadProviders(); return _provider; }
            }

            /// <summary>
            /// Gets the provider collection.
            /// </summary>
            public NetTiersProviderCollection Providers
            {
                get { LoadProviders(); return _providers; }
            }

            /// <summary>
            /// Instantiates the configured providers based on the supplied connection string.
            /// </summary>
            private void LoadProviders()
            {
                DataRepository.LoadProviders();

                // Avoid claiming lock if providers are already loaded
                if (_providers == null)
                {
                    lock (SyncRoot)
                    {
                        // Do this again to make sure _provider is still null
                        if (_providers == null)
                        {
                            // apply connection information to each provider
                            for (int i = 0; i < NetTiersSection.Providers.Count; i++)
                            {
                                NetTiersSection.Providers[i].Parameters["connectionStringName"] = _connectionStringName;
                                // remove previous connection string, if any
                                NetTiersSection.Providers[i].Parameters.Remove("connectionString");

                                if (!String.IsNullOrEmpty(_connectionString))
                                {
                                    NetTiersSection.Providers[i].Parameters["connectionString"] = _connectionString;
                                }
                            }

                            // Load registered providers and point _provider to the default provider
                            _providers = new NetTiersProviderCollection();

                            ProvidersHelper.InstantiateProviders(NetTiersSection.Providers, _providers, typeof(NetTiersProvider));
                            _provider = _providers[NetTiersSection.DefaultProvider];
                        }
                    }
                }
            }
        }

        #endregion Connections

        #region Static properties

        #region NearestWorkingDatesProvider

        ///<summary>
        /// Gets the current instance of the Data Access Logic Component for the <see cref="NearestWorkingDates"/> business entity.
        /// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
        ///</summary>
        public static NearestWorkingDatesProviderBase NearestWorkingDatesProvider
        {
            get
            {
                LoadProviders();
                return _provider.NearestWorkingDatesProvider;
            }
        }

        #endregion

        #region CompanyInfoProvider

        ///<summary>
        /// Gets the current instance of the Data Access Logic Component for the <see cref="CompanyInfo"/> business entity.
        /// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
        ///</summary>
        public static CompanyInfoProviderBase CompanyInfoProvider
        {
            get
            {
                LoadProviders();
                return _provider.CompanyInfoProvider;
            }
        }

        #endregion

        #region SecurityRealtimeProvider

        ///<summary>
        /// Gets the current instance of the Data Access Logic Component for the <see cref="SecurityRealtime"/> business entity.
        /// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
        ///</summary>
        public static SecurityRealtimeProviderBase SecurityRealtimeProvider
        {
            get
            {
                LoadProviders();
                return _provider.SecurityRealtimeProvider;
            }
        }

        #endregion

        #region MatchedProvider

        ///<summary>
        /// Gets the current instance of the Data Access Logic Component for the <see cref="Matched"/> business entity.
        /// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
        ///</summary>
        public static MatchedProviderBase MatchedProvider
        {
            get
            {
                LoadProviders();
                return _provider.MatchedProvider;
            }
        }

        #endregion

        #region UpcomStocksProvider

        ///<summary>
        /// Gets the current instance of the Data Access Logic Component for the <see cref="UpcomStocks"/> business entity.
        /// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
        ///</summary>
        public static UpcomStocksProviderBase UpcomStocksProvider
        {
            get
            {
                LoadProviders();
                return _provider.UpcomStocksProvider;
            }
        }

        #endregion

        #region TotalmarketProvider

        ///<summary>
        /// Gets the current instance of the Data Access Logic Component for the <see cref="Totalmarket"/> business entity.
        /// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
        ///</summary>
        public static TotalmarketProviderBase TotalmarketProvider
        {
            get
            {
                LoadProviders();
                return _provider.TotalmarketProvider;
            }
        }

        #endregion

        #region LeProvider

        ///<summary>
        /// Gets the current instance of the Data Access Logic Component for the <see cref="Le"/> business entity.
        /// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
        ///</summary>
        public static LeProviderBase LeProvider
        {
            get
            {
                LoadProviders();
                return _provider.LeProvider;
            }
        }

        #endregion

        #region UpcomMarketProvider

        ///<summary>
        /// Gets the current instance of the Data Access Logic Component for the <see cref="UpcomMarket"/> business entity.
        /// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
        ///</summary>
        public static UpcomMarketProviderBase UpcomMarketProvider
        {
            get
            {
                LoadProviders();
                return _provider.UpcomMarketProvider;
            }
        }

        #endregion

        #region HastcMarketProvider

        ///<summary>
        /// Gets the current instance of the Data Access Logic Component for the <see cref="HastcMarket"/> business entity.
        /// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
        ///</summary>
        public static HastcMarketProviderBase HastcMarketProvider
        {
            get
            {
                LoadProviders();
                return _provider.HastcMarketProvider;
            }
        }

        #endregion

        #region LanguageProvider

        ///<summary>
        /// Gets the current instance of the Data Access Logic Component for the <see cref="Language"/> business entity.
        /// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
        ///</summary>
        public static LanguageProviderBase LanguageProvider
        {
            get
            {
                LoadProviders();
                return _provider.LanguageProvider;
            }
        }

        #endregion

        #region HastcStocksProvider

        ///<summary>
        /// Gets the current instance of the Data Access Logic Component for the <see cref="HastcStocks"/> business entity.
        /// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
        ///</summary>
        public static HastcStocksProviderBase HastcStocksProvider
        {
            get
            {
                LoadProviders();
                return _provider.HastcStocksProvider;
            }
        }

        #endregion

        #region CompanyInfoLanguageProvider

        ///<summary>
        /// Gets the current instance of the Data Access Logic Component for the <see cref="CompanyInfoLanguage"/> business entity.
        /// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
        ///</summary>
        public static CompanyInfoLanguageProviderBase CompanyInfoLanguageProvider
        {
            get
            {
                LoadProviders();
                return _provider.CompanyInfoLanguageProvider;
            }
        }

        #endregion

        #region IndexsProvider

        ///<summary>
        /// Gets the current instance of the Data Access Logic Component for the <see cref="Indexs"/> business entity.
        /// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
        ///</summary>
        public static IndexsProviderBase IndexsProvider
        {
            get
            {
                LoadProviders();
                return _provider.IndexsProvider;
            }
        }

        #endregion

        #region HastcTransactionsProvider

        ///<summary>
        /// Gets the current instance of the Data Access Logic Component for the <see cref="HastcTransactions"/> business entity.
        /// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
        ///</summary>
        public static HastcTransactionsProviderBase HastcTransactionsProvider
        {
            get
            {
                LoadProviders();
                return _provider.HastcTransactionsProvider;
            }
        }

        #endregion

        #region UpcomTransactionsProvider

        ///<summary>
        /// Gets the current instance of the Data Access Logic Component for the <see cref="UpcomTransactions"/> business entity.
        /// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
        ///</summary>
        public static UpcomTransactionsProviderBase UpcomTransactionsProvider
        {
            get
            {
                LoadProviders();
                return _provider.UpcomTransactionsProvider;
            }
        }

        #endregion

        #region HoseTransactionsProvider

        ///<summary>
        /// Gets the current instance of the Data Access Logic Component for the <see cref="HoseTransactions"/> business entity.
        /// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
        ///</summary>
        public static HoseTransactionsProviderBase HoseTransactionsProvider
        {
            get
            {
                LoadProviders();
                return _provider.HoseTransactionsProvider;
            }
        }

        #endregion


        #endregion

        #region Query/Filters

        #region NearestWorkingDatesFilters

        /// <summary>
        /// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
        /// that is used exclusively with a <see cref="NearestWorkingDates"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class NearestWorkingDatesFilters : NearestWorkingDatesFilterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the NearestWorkingDatesFilters class.
            /// </summary>
            public NearestWorkingDatesFilters() : base() { }

            /// <summary>
            /// Initializes a new instance of the NearestWorkingDatesFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public NearestWorkingDatesFilters(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the NearestWorkingDatesFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public NearestWorkingDatesFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion NearestWorkingDatesFilters

        #region NearestWorkingDatesQuery

        /// <summary>
        /// A strongly-typed instance of the <see cref="NearestWorkingDatesParameterBuilder"/> class
        /// that is used exclusively with a <see cref="NearestWorkingDates"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class NearestWorkingDatesQuery : NearestWorkingDatesParameterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the NearestWorkingDatesQuery class.
            /// </summary>
            public NearestWorkingDatesQuery() : base() { }

            /// <summary>
            /// Initializes a new instance of the NearestWorkingDatesQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public NearestWorkingDatesQuery(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the NearestWorkingDatesQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public NearestWorkingDatesQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion NearestWorkingDatesQuery

        #region CompanyInfoFilters

        /// <summary>
        /// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
        /// that is used exclusively with a <see cref="CompanyInfo"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class CompanyInfoFilters : CompanyInfoFilterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the CompanyInfoFilters class.
            /// </summary>
            public CompanyInfoFilters() : base() { }

            /// <summary>
            /// Initializes a new instance of the CompanyInfoFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public CompanyInfoFilters(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the CompanyInfoFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public CompanyInfoFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion CompanyInfoFilters

        #region CompanyInfoQuery

        /// <summary>
        /// A strongly-typed instance of the <see cref="CompanyInfoParameterBuilder"/> class
        /// that is used exclusively with a <see cref="CompanyInfo"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class CompanyInfoQuery : CompanyInfoParameterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the CompanyInfoQuery class.
            /// </summary>
            public CompanyInfoQuery() : base() { }

            /// <summary>
            /// Initializes a new instance of the CompanyInfoQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public CompanyInfoQuery(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the CompanyInfoQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public CompanyInfoQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion CompanyInfoQuery

        #region SecurityRealtimeFilters

        /// <summary>
        /// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
        /// that is used exclusively with a <see cref="SecurityRealtime"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class SecurityRealtimeFilters : SecurityRealtimeFilterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the SecurityRealtimeFilters class.
            /// </summary>
            public SecurityRealtimeFilters() : base() { }

            /// <summary>
            /// Initializes a new instance of the SecurityRealtimeFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public SecurityRealtimeFilters(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the SecurityRealtimeFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public SecurityRealtimeFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion SecurityRealtimeFilters

        #region SecurityRealtimeQuery

        /// <summary>
        /// A strongly-typed instance of the <see cref="SecurityRealtimeParameterBuilder"/> class
        /// that is used exclusively with a <see cref="SecurityRealtime"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class SecurityRealtimeQuery : SecurityRealtimeParameterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the SecurityRealtimeQuery class.
            /// </summary>
            public SecurityRealtimeQuery() : base() { }

            /// <summary>
            /// Initializes a new instance of the SecurityRealtimeQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public SecurityRealtimeQuery(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the SecurityRealtimeQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public SecurityRealtimeQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion SecurityRealtimeQuery

        #region MatchedFilters

        /// <summary>
        /// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
        /// that is used exclusively with a <see cref="Matched"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class MatchedFilters : MatchedFilterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the MatchedFilters class.
            /// </summary>
            public MatchedFilters() : base() { }

            /// <summary>
            /// Initializes a new instance of the MatchedFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public MatchedFilters(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the MatchedFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public MatchedFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion MatchedFilters

        #region MatchedQuery

        /// <summary>
        /// A strongly-typed instance of the <see cref="MatchedParameterBuilder"/> class
        /// that is used exclusively with a <see cref="Matched"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class MatchedQuery : MatchedParameterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the MatchedQuery class.
            /// </summary>
            public MatchedQuery() : base() { }

            /// <summary>
            /// Initializes a new instance of the MatchedQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public MatchedQuery(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the MatchedQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public MatchedQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion MatchedQuery

        #region UpcomStocksFilters

        /// <summary>
        /// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
        /// that is used exclusively with a <see cref="UpcomStocks"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class UpcomStocksFilters : UpcomStocksFilterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the UpcomStocksFilters class.
            /// </summary>
            public UpcomStocksFilters() : base() { }

            /// <summary>
            /// Initializes a new instance of the UpcomStocksFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public UpcomStocksFilters(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the UpcomStocksFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public UpcomStocksFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion UpcomStocksFilters

        #region UpcomStocksQuery

        /// <summary>
        /// A strongly-typed instance of the <see cref="UpcomStocksParameterBuilder"/> class
        /// that is used exclusively with a <see cref="UpcomStocks"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class UpcomStocksQuery : UpcomStocksParameterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the UpcomStocksQuery class.
            /// </summary>
            public UpcomStocksQuery() : base() { }

            /// <summary>
            /// Initializes a new instance of the UpcomStocksQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public UpcomStocksQuery(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the UpcomStocksQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public UpcomStocksQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion UpcomStocksQuery

        #region TotalmarketFilters

        /// <summary>
        /// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
        /// that is used exclusively with a <see cref="Totalmarket"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class TotalmarketFilters : TotalmarketFilterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the TotalmarketFilters class.
            /// </summary>
            public TotalmarketFilters() : base() { }

            /// <summary>
            /// Initializes a new instance of the TotalmarketFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public TotalmarketFilters(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the TotalmarketFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public TotalmarketFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion TotalmarketFilters

        #region TotalmarketQuery

        /// <summary>
        /// A strongly-typed instance of the <see cref="TotalmarketParameterBuilder"/> class
        /// that is used exclusively with a <see cref="Totalmarket"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class TotalmarketQuery : TotalmarketParameterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the TotalmarketQuery class.
            /// </summary>
            public TotalmarketQuery() : base() { }

            /// <summary>
            /// Initializes a new instance of the TotalmarketQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public TotalmarketQuery(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the TotalmarketQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public TotalmarketQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion TotalmarketQuery

        #region LeFilters

        /// <summary>
        /// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
        /// that is used exclusively with a <see cref="Le"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class LeFilters : LeFilterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the LeFilters class.
            /// </summary>
            public LeFilters() : base() { }

            /// <summary>
            /// Initializes a new instance of the LeFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public LeFilters(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the LeFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public LeFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion LeFilters

        #region LeQuery

        /// <summary>
        /// A strongly-typed instance of the <see cref="LeParameterBuilder"/> class
        /// that is used exclusively with a <see cref="Le"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class LeQuery : LeParameterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the LeQuery class.
            /// </summary>
            public LeQuery() : base() { }

            /// <summary>
            /// Initializes a new instance of the LeQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public LeQuery(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the LeQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public LeQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion LeQuery

        #region UpcomMarketFilters

        /// <summary>
        /// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
        /// that is used exclusively with a <see cref="UpcomMarket"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class UpcomMarketFilters : UpcomMarketFilterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the UpcomMarketFilters class.
            /// </summary>
            public UpcomMarketFilters() : base() { }

            /// <summary>
            /// Initializes a new instance of the UpcomMarketFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public UpcomMarketFilters(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the UpcomMarketFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public UpcomMarketFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion UpcomMarketFilters

        #region UpcomMarketQuery

        /// <summary>
        /// A strongly-typed instance of the <see cref="UpcomMarketParameterBuilder"/> class
        /// that is used exclusively with a <see cref="UpcomMarket"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class UpcomMarketQuery : UpcomMarketParameterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the UpcomMarketQuery class.
            /// </summary>
            public UpcomMarketQuery() : base() { }

            /// <summary>
            /// Initializes a new instance of the UpcomMarketQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public UpcomMarketQuery(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the UpcomMarketQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public UpcomMarketQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion UpcomMarketQuery

        #region HastcMarketFilters

        /// <summary>
        /// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
        /// that is used exclusively with a <see cref="HastcMarket"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class HastcMarketFilters : HastcMarketFilterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the HastcMarketFilters class.
            /// </summary>
            public HastcMarketFilters() : base() { }

            /// <summary>
            /// Initializes a new instance of the HastcMarketFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public HastcMarketFilters(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the HastcMarketFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public HastcMarketFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion HastcMarketFilters

        #region HastcMarketQuery

        /// <summary>
        /// A strongly-typed instance of the <see cref="HastcMarketParameterBuilder"/> class
        /// that is used exclusively with a <see cref="HastcMarket"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class HastcMarketQuery : HastcMarketParameterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the HastcMarketQuery class.
            /// </summary>
            public HastcMarketQuery() : base() { }

            /// <summary>
            /// Initializes a new instance of the HastcMarketQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public HastcMarketQuery(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the HastcMarketQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public HastcMarketQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion HastcMarketQuery

        #region LanguageFilters

        /// <summary>
        /// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
        /// that is used exclusively with a <see cref="Language"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class LanguageFilters : LanguageFilterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the LanguageFilters class.
            /// </summary>
            public LanguageFilters() : base() { }

            /// <summary>
            /// Initializes a new instance of the LanguageFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public LanguageFilters(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the LanguageFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public LanguageFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion LanguageFilters

        #region LanguageQuery

        /// <summary>
        /// A strongly-typed instance of the <see cref="LanguageParameterBuilder"/> class
        /// that is used exclusively with a <see cref="Language"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class LanguageQuery : LanguageParameterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the LanguageQuery class.
            /// </summary>
            public LanguageQuery() : base() { }

            /// <summary>
            /// Initializes a new instance of the LanguageQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public LanguageQuery(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the LanguageQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public LanguageQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion LanguageQuery

        #region HastcStocksFilters

        /// <summary>
        /// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
        /// that is used exclusively with a <see cref="HastcStocks"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class HastcStocksFilters : HastcStocksFilterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the HastcStocksFilters class.
            /// </summary>
            public HastcStocksFilters() : base() { }

            /// <summary>
            /// Initializes a new instance of the HastcStocksFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public HastcStocksFilters(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the HastcStocksFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public HastcStocksFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion HastcStocksFilters

        #region HastcStocksQuery

        /// <summary>
        /// A strongly-typed instance of the <see cref="HastcStocksParameterBuilder"/> class
        /// that is used exclusively with a <see cref="HastcStocks"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class HastcStocksQuery : HastcStocksParameterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the HastcStocksQuery class.
            /// </summary>
            public HastcStocksQuery() : base() { }

            /// <summary>
            /// Initializes a new instance of the HastcStocksQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public HastcStocksQuery(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the HastcStocksQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public HastcStocksQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion HastcStocksQuery

        #region CompanyInfoLanguageFilters

        /// <summary>
        /// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
        /// that is used exclusively with a <see cref="CompanyInfoLanguage"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class CompanyInfoLanguageFilters : CompanyInfoLanguageFilterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the CompanyInfoLanguageFilters class.
            /// </summary>
            public CompanyInfoLanguageFilters() : base() { }

            /// <summary>
            /// Initializes a new instance of the CompanyInfoLanguageFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public CompanyInfoLanguageFilters(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the CompanyInfoLanguageFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public CompanyInfoLanguageFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion CompanyInfoLanguageFilters

        #region CompanyInfoLanguageQuery

        /// <summary>
        /// A strongly-typed instance of the <see cref="CompanyInfoLanguageParameterBuilder"/> class
        /// that is used exclusively with a <see cref="CompanyInfoLanguage"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class CompanyInfoLanguageQuery : CompanyInfoLanguageParameterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the CompanyInfoLanguageQuery class.
            /// </summary>
            public CompanyInfoLanguageQuery() : base() { }

            /// <summary>
            /// Initializes a new instance of the CompanyInfoLanguageQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public CompanyInfoLanguageQuery(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the CompanyInfoLanguageQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public CompanyInfoLanguageQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion CompanyInfoLanguageQuery

        #region IndexsFilters

        /// <summary>
        /// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
        /// that is used exclusively with a <see cref="Indexs"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class IndexsFilters : IndexsFilterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the IndexsFilters class.
            /// </summary>
            public IndexsFilters() : base() { }

            /// <summary>
            /// Initializes a new instance of the IndexsFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public IndexsFilters(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the IndexsFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public IndexsFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion IndexsFilters

        #region IndexsQuery

        /// <summary>
        /// A strongly-typed instance of the <see cref="IndexsParameterBuilder"/> class
        /// that is used exclusively with a <see cref="Indexs"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class IndexsQuery : IndexsParameterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the IndexsQuery class.
            /// </summary>
            public IndexsQuery() : base() { }

            /// <summary>
            /// Initializes a new instance of the IndexsQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public IndexsQuery(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the IndexsQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public IndexsQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion IndexsQuery

        #region HastcTransactionsFilters

        /// <summary>
        /// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
        /// that is used exclusively with a <see cref="HastcTransactions"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class HastcTransactionsFilters : HastcTransactionsFilterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the HastcTransactionsFilters class.
            /// </summary>
            public HastcTransactionsFilters() : base() { }

            /// <summary>
            /// Initializes a new instance of the HastcTransactionsFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public HastcTransactionsFilters(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the HastcTransactionsFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public HastcTransactionsFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion HastcTransactionsFilters

        #region HastcTransactionsQuery

        /// <summary>
        /// A strongly-typed instance of the <see cref="HastcTransactionsParameterBuilder"/> class
        /// that is used exclusively with a <see cref="HastcTransactions"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class HastcTransactionsQuery : HastcTransactionsParameterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the HastcTransactionsQuery class.
            /// </summary>
            public HastcTransactionsQuery() : base() { }

            /// <summary>
            /// Initializes a new instance of the HastcTransactionsQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public HastcTransactionsQuery(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the HastcTransactionsQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public HastcTransactionsQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion HastcTransactionsQuery

        #region UpcomTransactionsFilters

        /// <summary>
        /// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
        /// that is used exclusively with a <see cref="UpcomTransactions"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class UpcomTransactionsFilters : UpcomTransactionsFilterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the UpcomTransactionsFilters class.
            /// </summary>
            public UpcomTransactionsFilters() : base() { }

            /// <summary>
            /// Initializes a new instance of the UpcomTransactionsFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public UpcomTransactionsFilters(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the UpcomTransactionsFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public UpcomTransactionsFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion UpcomTransactionsFilters

        #region UpcomTransactionsQuery

        /// <summary>
        /// A strongly-typed instance of the <see cref="UpcomTransactionsParameterBuilder"/> class
        /// that is used exclusively with a <see cref="UpcomTransactions"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class UpcomTransactionsQuery : UpcomTransactionsParameterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the UpcomTransactionsQuery class.
            /// </summary>
            public UpcomTransactionsQuery() : base() { }

            /// <summary>
            /// Initializes a new instance of the UpcomTransactionsQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public UpcomTransactionsQuery(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the UpcomTransactionsQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public UpcomTransactionsQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion UpcomTransactionsQuery

        #region HoseTransactionsFilters

        /// <summary>
        /// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
        /// that is used exclusively with a <see cref="HoseTransactions"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class HoseTransactionsFilters : HoseTransactionsFilterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the HoseTransactionsFilters class.
            /// </summary>
            public HoseTransactionsFilters() : base() { }

            /// <summary>
            /// Initializes a new instance of the HoseTransactionsFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public HoseTransactionsFilters(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the HoseTransactionsFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public HoseTransactionsFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion HoseTransactionsFilters

        #region HoseTransactionsQuery

        /// <summary>
        /// A strongly-typed instance of the <see cref="HoseTransactionsParameterBuilder"/> class
        /// that is used exclusively with a <see cref="HoseTransactions"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class HoseTransactionsQuery : HoseTransactionsParameterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the HoseTransactionsQuery class.
            /// </summary>
            public HoseTransactionsQuery() : base() { }

            /// <summary>
            /// Initializes a new instance of the HoseTransactionsQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public HoseTransactionsQuery(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the HoseTransactionsQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public HoseTransactionsQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion HoseTransactionsQuery

        #region BasketInfoProvider

        ///<summary>
        /// Gets the current instance of the Data Access Logic Component for the <see cref="BasketInfo"/> business entity.
        /// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
        ///</summary>
        public static BasketInfoProviderBase BasketInfoProvider
        {
            get
            {
                LoadProviders();
                return _provider.BasketInfoProvider;
            }
        }

        #endregion

        #region IndexInfoProvider

        ///<summary>
        /// Gets the current instance of the Data Access Logic Component for the <see cref="IndexInfo"/> business entity.
        /// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
        ///</summary>
        public static IndexInfoProviderBase IndexInfoProvider
        {
            get
            {
                LoadProviders();
                return _provider.IndexInfoProvider;
            }
        }

        #endregion

        #region IndexInfoHistoryProvider

        ///<summary>
        /// Gets the current instance of the Data Access Logic Component for the <see cref="IndexInfoHistory"/> business entity.
        /// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
        ///</summary>
        public static IndexInfoHistoryProviderBase IndexInfoHistoryProvider
        {
            get
            {
                LoadProviders();
                return _provider.IndexInfoHistoryProvider;
            }
        }

        #endregion


        #endregion

        #region Query/Filters

        #region BasketInfoFilters

        /// <summary>
        /// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
        /// that is used exclusively with a <see cref="BasketInfo"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class BasketInfoFilters : BasketInfoFilterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the BasketInfoFilters class.
            /// </summary>
            public BasketInfoFilters() : base() { }

            /// <summary>
            /// Initializes a new instance of the BasketInfoFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public BasketInfoFilters(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the BasketInfoFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public BasketInfoFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion BasketInfoFilters

        #region BasketInfoQuery

        /// <summary>
        /// A strongly-typed instance of the <see cref="BasketInfoParameterBuilder"/> class
        /// that is used exclusively with a <see cref="BasketInfo"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class BasketInfoQuery : BasketInfoParameterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the BasketInfoQuery class.
            /// </summary>
            public BasketInfoQuery() : base() { }

            /// <summary>
            /// Initializes a new instance of the BasketInfoQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public BasketInfoQuery(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the BasketInfoQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public BasketInfoQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion BasketInfoQuery

        #region IndexInfoFilters

        /// <summary>
        /// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
        /// that is used exclusively with a <see cref="IndexInfo"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class IndexInfoFilters : IndexInfoFilterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the IndexInfoFilters class.
            /// </summary>
            public IndexInfoFilters() : base() { }

            /// <summary>
            /// Initializes a new instance of the IndexInfoFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public IndexInfoFilters(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the IndexInfoFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public IndexInfoFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion IndexInfoFilters

        #region IndexInfoQuery

        /// <summary>
        /// A strongly-typed instance of the <see cref="IndexInfoParameterBuilder"/> class
        /// that is used exclusively with a <see cref="IndexInfo"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class IndexInfoQuery : IndexInfoParameterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the IndexInfoQuery class.
            /// </summary>
            public IndexInfoQuery() : base() { }

            /// <summary>
            /// Initializes a new instance of the IndexInfoQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public IndexInfoQuery(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the IndexInfoQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public IndexInfoQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion IndexInfoQuery

        #region IndexInfoHistoryFilters

        /// <summary>
        /// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
        /// that is used exclusively with a <see cref="IndexInfoHistory"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class IndexInfoHistoryFilters : IndexInfoHistoryFilterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the IndexInfoHistoryFilters class.
            /// </summary>
            public IndexInfoHistoryFilters() : base() { }

            /// <summary>
            /// Initializes a new instance of the IndexInfoHistoryFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public IndexInfoHistoryFilters(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the IndexInfoHistoryFilters class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public IndexInfoHistoryFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion IndexInfoHistoryFilters

        #region IndexInfoHistoryQuery

        /// <summary>
        /// A strongly-typed instance of the <see cref="IndexInfoHistoryParameterBuilder"/> class
        /// that is used exclusively with a <see cref="IndexInfoHistory"/> object.
        /// </summary>
        [CLSCompliant(true)]
        public class IndexInfoHistoryQuery : IndexInfoHistoryParameterBuilder
        {
            #region Constructors

            /// <summary>
            /// Initializes a new instance of the IndexInfoHistoryQuery class.
            /// </summary>
            public IndexInfoHistoryQuery() : base() { }

            /// <summary>
            /// Initializes a new instance of the IndexInfoHistoryQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            public IndexInfoHistoryQuery(bool ignoreCase) : base(ignoreCase) { }

            /// <summary>
            /// Initializes a new instance of the IndexInfoHistoryQuery class.
            /// </summary>
            /// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
            /// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
            public IndexInfoHistoryQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

            #endregion Constructors
        }

        #endregion IndexInfoHistoryQuery
        #endregion

    }
}
