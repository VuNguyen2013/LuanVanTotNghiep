﻿#region Using directives

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Provider;
using System.Web.Configuration;
using System.Web;
using AccountManager.Entities;
using AccountManager.DataAccess;
using AccountManager.DataAccess.Bases;

#endregion

namespace AccountManager.DataAccess
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
				lock(SyncRoot)
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
                    if(_provider == null || setAsDefault)
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

				if ( _section == null )
				{
					// otherwise look for section based on the assembly name
					_section = WebConfigurationManager.GetSection("AccountManager.DataAccess") as NetTiersServiceSection;
				}

				if ( _section == null )
				{
					throw new ProviderException("Unable to load NetTiersServiceSection");
				}

				return _section;
			}
		}

		#endregion Configuration

		#region Connections

		/// <summary>
		/// Gets a reference to the ConnectionStringSettings collection.
		/// </summary>
		public static ConnectionStringSettingsCollection ConnectionStrings
		{
			get
			{
				return WebConfigurationManager.ConnectionStrings;
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
				if ( _connections == null )
				{
					lock (SyncRoot)
                	{
						if (_connections == null)
						{
							_connections = new Dictionary<String, ConnectionProvider>();
		
							// add a connection provider for each configured connection string
							foreach ( ConnectionStringSettings conn in ConnectionStrings )
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
				if ( _providers == null )
				{
					lock ( SyncRoot )
					{
						// Do this again to make sure _provider is still null
						if ( _providers == null )
						{
							// apply connection information to each provider
							for ( int i = 0; i < NetTiersSection.Providers.Count; i++ )
							{
								NetTiersSection.Providers[i].Parameters["connectionStringName"] = _connectionStringName;
								// remove previous connection string, if any
								NetTiersSection.Providers[i].Parameters.Remove("connectionString");

								if ( !String.IsNullOrEmpty(_connectionString) )
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
		
		#region OpenCustAccountProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="OpenCustAccount"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static OpenCustAccountProviderBase OpenCustAccountProvider
		{
			get 
			{
				LoadProviders();
				return _provider.OpenCustAccountProvider;
			}
		}
		
		#endregion
		
		#region BrokerAccountProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BrokerAccount"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BrokerAccountProviderBase BrokerAccountProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BrokerAccountProvider;
			}
		}
		
		#endregion
		
		#region MainCustAccountProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="MainCustAccount"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static MainCustAccountProviderBase MainCustAccountProvider
		{
			get 
			{
				LoadProviders();
				return _provider.MainCustAccountProvider;
			}
		}
		
		#endregion
		
		#region ResearchProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Research"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static ResearchProviderBase ResearchProvider
		{
			get 
			{
				LoadProviders();
				return _provider.ResearchProvider;
			}
		}
		
		#endregion
		
		#region SmsCountProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="SmsCount"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static SmsCountProviderBase SmsCountProvider
		{
			get 
			{
				LoadProviders();
				return _provider.SmsCountProvider;
			}
		}
		
		#endregion
		
		#region SubCustAccountProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="SubCustAccount"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static SubCustAccountProviderBase SubCustAccountProvider
		{
			get 
			{
				LoadProviders();
				return _provider.SubCustAccountProvider;
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
		
		#region SubCustAccountPermissionProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="SubCustAccountPermission"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static SubCustAccountPermissionProviderBase SubCustAccountPermissionProvider
		{
			get 
			{
				LoadProviders();
				return _provider.SubCustAccountPermissionProvider;
			}
		}
		
		#endregion
		
		#region HolidaysProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Holidays"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static HolidaysProviderBase HolidaysProvider
		{
			get 
			{
				LoadProviders();
				return _provider.HolidaysProvider;
			}
		}
		
		#endregion
		
		#region BrokerAmPermissionProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BrokerAmPermission"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BrokerAmPermissionProviderBase BrokerAmPermissionProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BrokerAmPermissionProvider;
			}
		}
		
		#endregion
		
		#region BuyRightProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BuyRight"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BuyRightProviderBase BuyRightProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BuyRightProvider;
			}
		}
		
		#endregion
		
		#region BrokerPermissionProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="BrokerPermission"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static BrokerPermissionProviderBase BrokerPermissionProvider
		{
			get 
			{
				LoadProviders();
				return _provider.BrokerPermissionProvider;
			}
		}
		
		#endregion
		
		#region CustServicesPermissionProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="CustServicesPermission"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static CustServicesPermissionProviderBase CustServicesPermissionProvider
		{
			get 
			{
				LoadProviders();
				return _provider.CustServicesPermissionProvider;
			}
		}
		
		#endregion
		
		#region ConfigurationsProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="Configurations"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static ConfigurationsProviderBase ConfigurationsProvider
		{
			get 
			{
				LoadProviders();
				return _provider.ConfigurationsProvider;
			}
		}
		
		#endregion
		
		#region WorkingDaysProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="WorkingDays"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static WorkingDaysProviderBase WorkingDaysProvider
		{
			get 
			{
				LoadProviders();
				return _provider.WorkingDaysProvider;
			}
		}
		
		#endregion
		
		#region CustomerActionHistoryProvider

		///<summary>
		/// Gets the current instance of the Data Access Logic Component for the <see cref="CustomerActionHistory"/> business entity.
		/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
		///</summary>
		public static CustomerActionHistoryProviderBase CustomerActionHistoryProvider
		{
			get 
			{
				LoadProviders();
				return _provider.CustomerActionHistoryProvider;
			}
		}
		
		#endregion
		
		
		#endregion
	}
	
	#region Query/Filters
		
	#region OpenCustAccountFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="OpenCustAccount"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class OpenCustAccountFilters : OpenCustAccountFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the OpenCustAccountFilters class.
		/// </summary>
		public OpenCustAccountFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the OpenCustAccountFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public OpenCustAccountFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the OpenCustAccountFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public OpenCustAccountFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion OpenCustAccountFilters
	
	#region OpenCustAccountQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="OpenCustAccountParameterBuilder"/> class
	/// that is used exclusively with a <see cref="OpenCustAccount"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class OpenCustAccountQuery : OpenCustAccountParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the OpenCustAccountQuery class.
		/// </summary>
		public OpenCustAccountQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the OpenCustAccountQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public OpenCustAccountQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the OpenCustAccountQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public OpenCustAccountQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion OpenCustAccountQuery
		
	#region BrokerAccountFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BrokerAccount"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BrokerAccountFilters : BrokerAccountFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BrokerAccountFilters class.
		/// </summary>
		public BrokerAccountFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BrokerAccountFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BrokerAccountFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BrokerAccountFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BrokerAccountFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BrokerAccountFilters
	
	#region BrokerAccountQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BrokerAccountParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BrokerAccount"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BrokerAccountQuery : BrokerAccountParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BrokerAccountQuery class.
		/// </summary>
		public BrokerAccountQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BrokerAccountQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BrokerAccountQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BrokerAccountQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BrokerAccountQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BrokerAccountQuery
		
	#region MainCustAccountFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="MainCustAccount"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class MainCustAccountFilters : MainCustAccountFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MainCustAccountFilters class.
		/// </summary>
		public MainCustAccountFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the MainCustAccountFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public MainCustAccountFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the MainCustAccountFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public MainCustAccountFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion MainCustAccountFilters
	
	#region MainCustAccountQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="MainCustAccountParameterBuilder"/> class
	/// that is used exclusively with a <see cref="MainCustAccount"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class MainCustAccountQuery : MainCustAccountParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the MainCustAccountQuery class.
		/// </summary>
		public MainCustAccountQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the MainCustAccountQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public MainCustAccountQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the MainCustAccountQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public MainCustAccountQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion MainCustAccountQuery
		
	#region ResearchFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Research"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ResearchFilters : ResearchFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ResearchFilters class.
		/// </summary>
		public ResearchFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the ResearchFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ResearchFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ResearchFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ResearchFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ResearchFilters
	
	#region ResearchQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ResearchParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Research"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ResearchQuery : ResearchParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ResearchQuery class.
		/// </summary>
		public ResearchQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the ResearchQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ResearchQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ResearchQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ResearchQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ResearchQuery
		
	#region SmsCountFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="SmsCount"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class SmsCountFilters : SmsCountFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SmsCountFilters class.
		/// </summary>
		public SmsCountFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the SmsCountFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public SmsCountFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the SmsCountFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public SmsCountFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion SmsCountFilters
	
	#region SmsCountQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SmsCountParameterBuilder"/> class
	/// that is used exclusively with a <see cref="SmsCount"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class SmsCountQuery : SmsCountParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SmsCountQuery class.
		/// </summary>
		public SmsCountQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the SmsCountQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public SmsCountQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the SmsCountQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public SmsCountQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion SmsCountQuery
		
	#region SubCustAccountFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="SubCustAccount"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class SubCustAccountFilters : SubCustAccountFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SubCustAccountFilters class.
		/// </summary>
		public SubCustAccountFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the SubCustAccountFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public SubCustAccountFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the SubCustAccountFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public SubCustAccountFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion SubCustAccountFilters
	
	#region SubCustAccountQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SubCustAccountParameterBuilder"/> class
	/// that is used exclusively with a <see cref="SubCustAccount"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class SubCustAccountQuery : SubCustAccountParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SubCustAccountQuery class.
		/// </summary>
		public SubCustAccountQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the SubCustAccountQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public SubCustAccountQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the SubCustAccountQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public SubCustAccountQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion SubCustAccountQuery
		
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
		
	#region SubCustAccountPermissionFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="SubCustAccountPermission"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class SubCustAccountPermissionFilters : SubCustAccountPermissionFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SubCustAccountPermissionFilters class.
		/// </summary>
		public SubCustAccountPermissionFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the SubCustAccountPermissionFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public SubCustAccountPermissionFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the SubCustAccountPermissionFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public SubCustAccountPermissionFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion SubCustAccountPermissionFilters
	
	#region SubCustAccountPermissionQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SubCustAccountPermissionParameterBuilder"/> class
	/// that is used exclusively with a <see cref="SubCustAccountPermission"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class SubCustAccountPermissionQuery : SubCustAccountPermissionParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the SubCustAccountPermissionQuery class.
		/// </summary>
		public SubCustAccountPermissionQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the SubCustAccountPermissionQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public SubCustAccountPermissionQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the SubCustAccountPermissionQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public SubCustAccountPermissionQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion SubCustAccountPermissionQuery
		
	#region HolidaysFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Holidays"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class HolidaysFilters : HolidaysFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HolidaysFilters class.
		/// </summary>
		public HolidaysFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the HolidaysFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public HolidaysFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the HolidaysFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public HolidaysFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion HolidaysFilters
	
	#region HolidaysQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="HolidaysParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Holidays"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class HolidaysQuery : HolidaysParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the HolidaysQuery class.
		/// </summary>
		public HolidaysQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the HolidaysQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public HolidaysQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the HolidaysQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public HolidaysQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion HolidaysQuery
		
	#region BrokerAmPermissionFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BrokerAmPermission"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BrokerAmPermissionFilters : BrokerAmPermissionFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BrokerAmPermissionFilters class.
		/// </summary>
		public BrokerAmPermissionFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BrokerAmPermissionFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BrokerAmPermissionFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BrokerAmPermissionFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BrokerAmPermissionFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BrokerAmPermissionFilters
	
	#region BrokerAmPermissionQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BrokerAmPermissionParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BrokerAmPermission"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BrokerAmPermissionQuery : BrokerAmPermissionParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BrokerAmPermissionQuery class.
		/// </summary>
		public BrokerAmPermissionQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BrokerAmPermissionQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BrokerAmPermissionQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BrokerAmPermissionQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BrokerAmPermissionQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BrokerAmPermissionQuery
		
	#region BuyRightFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BuyRight"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BuyRightFilters : BuyRightFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BuyRightFilters class.
		/// </summary>
		public BuyRightFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BuyRightFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BuyRightFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BuyRightFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BuyRightFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BuyRightFilters
	
	#region BuyRightQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BuyRightParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BuyRight"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BuyRightQuery : BuyRightParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BuyRightQuery class.
		/// </summary>
		public BuyRightQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BuyRightQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BuyRightQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BuyRightQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BuyRightQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BuyRightQuery
		
	#region BrokerPermissionFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BrokerPermission"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BrokerPermissionFilters : BrokerPermissionFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BrokerPermissionFilters class.
		/// </summary>
		public BrokerPermissionFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the BrokerPermissionFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BrokerPermissionFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BrokerPermissionFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BrokerPermissionFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BrokerPermissionFilters
	
	#region BrokerPermissionQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="BrokerPermissionParameterBuilder"/> class
	/// that is used exclusively with a <see cref="BrokerPermission"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BrokerPermissionQuery : BrokerPermissionParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BrokerPermissionQuery class.
		/// </summary>
		public BrokerPermissionQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the BrokerPermissionQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BrokerPermissionQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BrokerPermissionQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BrokerPermissionQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BrokerPermissionQuery
		
	#region CustServicesPermissionFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="CustServicesPermission"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CustServicesPermissionFilters : CustServicesPermissionFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CustServicesPermissionFilters class.
		/// </summary>
		public CustServicesPermissionFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the CustServicesPermissionFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public CustServicesPermissionFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the CustServicesPermissionFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public CustServicesPermissionFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion CustServicesPermissionFilters
	
	#region CustServicesPermissionQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="CustServicesPermissionParameterBuilder"/> class
	/// that is used exclusively with a <see cref="CustServicesPermission"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CustServicesPermissionQuery : CustServicesPermissionParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CustServicesPermissionQuery class.
		/// </summary>
		public CustServicesPermissionQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the CustServicesPermissionQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public CustServicesPermissionQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the CustServicesPermissionQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public CustServicesPermissionQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion CustServicesPermissionQuery
		
	#region ConfigurationsFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="Configurations"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ConfigurationsFilters : ConfigurationsFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ConfigurationsFilters class.
		/// </summary>
		public ConfigurationsFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the ConfigurationsFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ConfigurationsFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ConfigurationsFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ConfigurationsFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ConfigurationsFilters
	
	#region ConfigurationsQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ConfigurationsParameterBuilder"/> class
	/// that is used exclusively with a <see cref="Configurations"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class ConfigurationsQuery : ConfigurationsParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the ConfigurationsQuery class.
		/// </summary>
		public ConfigurationsQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the ConfigurationsQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public ConfigurationsQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the ConfigurationsQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public ConfigurationsQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion ConfigurationsQuery
		
	#region WorkingDaysFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="WorkingDays"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class WorkingDaysFilters : WorkingDaysFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the WorkingDaysFilters class.
		/// </summary>
		public WorkingDaysFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the WorkingDaysFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public WorkingDaysFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the WorkingDaysFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public WorkingDaysFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion WorkingDaysFilters
	
	#region WorkingDaysQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="WorkingDaysParameterBuilder"/> class
	/// that is used exclusively with a <see cref="WorkingDays"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class WorkingDaysQuery : WorkingDaysParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the WorkingDaysQuery class.
		/// </summary>
		public WorkingDaysQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the WorkingDaysQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public WorkingDaysQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the WorkingDaysQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public WorkingDaysQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion WorkingDaysQuery
		
	#region CustomerActionHistoryFilters
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="CustomerActionHistory"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CustomerActionHistoryFilters : CustomerActionHistoryFilterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CustomerActionHistoryFilters class.
		/// </summary>
		public CustomerActionHistoryFilters() : base() { }

		/// <summary>
		/// Initializes a new instance of the CustomerActionHistoryFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public CustomerActionHistoryFilters(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the CustomerActionHistoryFilters class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public CustomerActionHistoryFilters(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion CustomerActionHistoryFilters
	
	#region CustomerActionHistoryQuery
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="CustomerActionHistoryParameterBuilder"/> class
	/// that is used exclusively with a <see cref="CustomerActionHistory"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class CustomerActionHistoryQuery : CustomerActionHistoryParameterBuilder
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the CustomerActionHistoryQuery class.
		/// </summary>
		public CustomerActionHistoryQuery() : base() { }

		/// <summary>
		/// Initializes a new instance of the CustomerActionHistoryQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public CustomerActionHistoryQuery(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the CustomerActionHistoryQuery class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public CustomerActionHistoryQuery(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion CustomerActionHistoryQuery
	#endregion

	
}