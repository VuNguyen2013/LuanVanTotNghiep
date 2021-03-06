﻿

/*
	File Generated by NetTiers templates [www.nettiers.com]
	Generated on : Wednesday, October 20, 2010
	Important: Do not modify this file. Edit the file CustServicesPermissionTest.cs instead.
*/

#region Using directives

using System;
using System.Xml;
using System.Xml.Serialization;
using NUnit.Framework;
using AccountManager.Entities;
using AccountManager.DataAccess;
using AccountManager.DataAccess.Bases;

#endregion

namespace AccountManager.UnitTests
{
    /// <summary>
    /// Provides tests for the and <see cref="CustServicesPermission"/> objects (entity, collection and repository).
    /// </summary>
   public partial class CustServicesPermissionTest
    {
    	// the CustServicesPermission instance used to test the repository.
		private CustServicesPermission mock;
		
		// the TList<CustServicesPermission> instance used to test the repository.
		private TList<CustServicesPermission> mockCollection;
		
		private static TransactionManager CreateTransaction()
		{
			TransactionManager transactionManager = null;
			if (DataRepository.Provider.IsTransactionSupported)
			{
				transactionManager = DataRepository.Provider.CreateTransaction();
				transactionManager.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
			}			
			return transactionManager;
		}
		       
        /// <summary>
		/// This method is used to construct the test environment prior to running the tests.
		/// </summary>        
        static public void Init_Generated()
        {		
        	System.Console.WriteLine(new String('-', 75));
			System.Console.WriteLine("-- Testing the CustServicesPermission Entity with the {0} --", AccountManager.DataAccess.DataRepository.Provider.Name);
			System.Console.WriteLine(new String('-', 75));
        }
    
    	/// <summary>
		/// This method is used to restore the environment after the tests are completed.
		/// </summary>
		static public void CleanUp_Generated()
        {   		
			System.Console.WriteLine("All Tests Completed");
			System.Console.WriteLine();
        }
    
    
		/// <summary>
		/// Inserts a mock CustServicesPermission entity into the database.
		/// </summary>
		private void Step_01_Insert_Generated()
		{
			using (TransactionManager tm = CreateTransaction())
			{
				mock = CreateMockInstance(tm);
				Assert.IsTrue(DataRepository.CustServicesPermissionProvider.Insert(tm, mock), "Insert failed");
										
				System.Console.WriteLine("DataRepository.CustServicesPermissionProvider.Insert(mock):");			
				System.Console.WriteLine(mock);			
				
				//normally one would commit here
				//tm.Commit();
				//IDisposable will Rollback Transaction since it's left uncommitted
			}
		}
		
		
		/// <summary>
		/// Selects all CustServicesPermission objects of the database.
		/// </summary>
		private void Step_02_SelectAll_Generated()
		{
			using (TransactionManager tm = CreateTransaction())
			{
				//Find
				int count = -1;
				
				mockCollection = DataRepository.CustServicesPermissionProvider.Find(tm, null, "", 0, 10, out count );
				Assert.IsTrue(count >= 0 && mockCollection != null, "Query Failed to issue Find Command.");
				
				System.Console.WriteLine("DataRepository.CustServicesPermissionProvider.Find():");			
				System.Console.WriteLine(mockCollection);
				
				// GetPaged
				count = -1;
				
				mockCollection = DataRepository.CustServicesPermissionProvider.GetPaged(tm, 0, 10, out count);
				Assert.IsTrue(count >= 0 && mockCollection != null, "Query Failed to issue GetPaged Command.");
				System.Console.WriteLine("#get paged count: " + count.ToString());
			}
		}
		
		
		
		
		/// <summary>
		/// Deep load all CustServicesPermission children.
		/// </summary>
		private void Step_03_DeepLoad_Generated()
		{
			using (TransactionManager tm = CreateTransaction())
			{
				int count = -1;
				mock =  CreateMockInstance(tm);
				mockCollection = DataRepository.CustServicesPermissionProvider.GetPaged(tm, 0, 10, out count);
			
				DataRepository.CustServicesPermissionProvider.DeepLoading += new EntityProviderBaseCore<CustServicesPermission, CustServicesPermissionKey>.DeepLoadingEventHandler(
						delegate(object sender, DeepSessionEventArgs e)
						{
							if (e.DeepSession.Count > 3)
								e.Cancel = true;
						}
					);

				if (mockCollection.Count > 0)
				{
					
					DataRepository.CustServicesPermissionProvider.DeepLoad(tm, mockCollection[0]);
					System.Console.WriteLine("CustServicesPermission instance correctly deep loaded at 1 level.");
									
					mockCollection.Add(mock);
					// DataRepository.CustServicesPermissionProvider.DeepSave(tm, mockCollection);
				}
				
				//normally one would commit here
				//tm.Commit();
				//IDisposable will Rollback Transaction since it's left uncommitted
			}
		}
		
		/// <summary>
		/// Updates a mock CustServicesPermission entity into the database.
		/// </summary>
		private void Step_04_Update_Generated()
		{
			using (TransactionManager tm = CreateTransaction())
			{
				CustServicesPermission mock = CreateMockInstance(tm);
				Assert.IsTrue(DataRepository.CustServicesPermissionProvider.Insert(tm, mock), "Insert failed");
				
				UpdateMockInstance(tm, mock);
				Assert.IsTrue(DataRepository.CustServicesPermissionProvider.Update(tm, mock), "Update failed.");			
				
				System.Console.WriteLine("DataRepository.CustServicesPermissionProvider.Update(mock):");			
				System.Console.WriteLine(mock);
				
				//normally one would commit here
				//tm.Commit();
				//IDisposable will Rollback Transaction since it's left uncommitted
			}
		}
		
		
		/// <summary>
		/// Delete the mock CustServicesPermission entity into the database.
		/// </summary>
		private void Step_05_Delete_Generated()
		{
			using (TransactionManager tm = CreateTransaction())
			{
				mock =  (CustServicesPermission)CreateMockInstance(tm);
				DataRepository.CustServicesPermissionProvider.Insert(tm, mock);
			
				Assert.IsTrue(DataRepository.CustServicesPermissionProvider.Delete(tm, mock), "Delete failed.");
				System.Console.WriteLine("DataRepository.CustServicesPermissionProvider.Delete(mock):");			
				System.Console.WriteLine(mock);
				
				//normally one would commit here
				//tm.Commit();
				//IDisposable will Rollback Transaction since it's left uncommitted
			}
		}
		
		#region Serialization tests
		
		/// <summary>
		/// Serialize the mock CustServicesPermission entity into a temporary file.
		/// </summary>
		private void Step_06_SerializeEntity_Generated()
		{	
			using (TransactionManager tm = CreateTransaction())
			{
				mock =  CreateMockInstance(tm);
				string fileName = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "temp_CustServicesPermission.xml");
			
				EntityHelper.SerializeXml(mock, fileName);
				Assert.IsTrue(System.IO.File.Exists(fileName), "Serialized mock not found");
					
				System.Console.WriteLine("mock correctly serialized to a temporary file.");			
			}
		}
		
		/// <summary>
		/// Deserialize the mock CustServicesPermission entity from a temporary file.
		/// </summary>
		private void Step_07_DeserializeEntity_Generated()
		{
			string fileName = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "temp_CustServicesPermission.xml");
			Assert.IsTrue(System.IO.File.Exists(fileName), "Serialized mock file not found to deserialize");
			
			using (System.IO.StreamReader sr = System.IO.File.OpenText(fileName))
			{
				object item = EntityHelper.DeserializeEntityXml<CustServicesPermission>(sr.ReadToEnd());
				sr.Close();
			}
			System.IO.File.Delete(fileName);
			
			System.Console.WriteLine("mock correctly deserialized from a temporary file.");
		}
		
		/// <summary>
		/// Serialize a CustServicesPermission collection into a temporary file.
		/// </summary>
		private void Step_08_SerializeCollection_Generated()
		{
			using (TransactionManager tm = CreateTransaction())
			{
				string fileName = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "temp_CustServicesPermissionCollection.xml");
				
				mock = CreateMockInstance(tm);
				TList<CustServicesPermission> mockCollection = new TList<CustServicesPermission>();
				mockCollection.Add(mock);
			
				EntityHelper.SerializeXml(mockCollection, fileName);
				
				Assert.IsTrue(System.IO.File.Exists(fileName), "Serialized mock collection not found");
				System.Console.WriteLine("TList<CustServicesPermission> correctly serialized to a temporary file.");					
			}
		}
		
		
		/// <summary>
		/// Deserialize a CustServicesPermission collection from a temporary file.
		/// </summary>
		private void Step_09_DeserializeCollection_Generated()
		{
			string fileName = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "temp_CustServicesPermissionCollection.xml");
			Assert.IsTrue(System.IO.File.Exists(fileName), "Serialized mock file not found to deserialize");
			
			XmlSerializer mySerializer = new XmlSerializer(typeof(TList<CustServicesPermission>)); 
			using (System.IO.FileStream myFileStream = new System.IO.FileStream(fileName,  System.IO.FileMode.Open))
			{
				TList<CustServicesPermission> mockCollection = (TList<CustServicesPermission>) mySerializer.Deserialize(myFileStream);
				myFileStream.Close();
			}
			
			System.IO.File.Delete(fileName);
			System.Console.WriteLine("TList<CustServicesPermission> correctly deserialized from a temporary file.");	
		}
		#endregion
		
		
		
		/// <summary>
		/// Check the foreign key dal methods.
		/// </summary>
		private void Step_10_FK_Generated()
		{
			using (TransactionManager tm = CreateTransaction())
			{
				CustServicesPermission entity = CreateMockInstance(tm);
				bool result = DataRepository.CustServicesPermissionProvider.Insert(tm, entity);
				
				Assert.IsTrue(result, "Could Not Test FK, Insert Failed");
				
			}
		}
		
		
		/// <summary>
		/// Check the indexes dal methods.
		/// </summary>
		private void Step_11_IX_Generated()
		{
			using (TransactionManager tm = CreateTransaction())
			{
				CustServicesPermission entity = CreateMockInstance(tm);
				bool result = DataRepository.CustServicesPermissionProvider.Insert(tm, entity);
				
				Assert.IsTrue(result, "Could Not Test IX, Insert Failed");

			
				CustServicesPermission t0 = DataRepository.CustServicesPermissionProvider.GetByCustServicesPermissionId(tm, entity.CustServicesPermissionId);
			}
		}
		
		/// <summary>
		/// Test methods exposed by the EntityHelper class.
		/// </summary>
		private void Step_20_TestEntityHelper_Generated()
		{
			using (TransactionManager tm = CreateTransaction())
			{
				mock = CreateMockInstance(tm);
				
				CustServicesPermission entity = mock.Copy() as CustServicesPermission;
				entity = (CustServicesPermission)mock.Clone();
				Assert.IsTrue(CustServicesPermission.ValueEquals(entity, mock), "Clone is not working");
			}
		}
		
		/// <summary>
		/// Test Find using the Query class
		/// </summary>
		private void Step_30_TestFindByQuery_Generated()
		{
			using (TransactionManager tm = CreateTransaction())
			{
				//Insert Mock Instance
				CustServicesPermission mock = CreateMockInstance(tm);
				bool result = DataRepository.CustServicesPermissionProvider.Insert(tm, mock);
				
				Assert.IsTrue(result, "Could Not Test FindByQuery, Insert Failed");

				CustServicesPermissionQuery query = new CustServicesPermissionQuery();
			
				query.AppendEquals(CustServicesPermissionColumn.CustServicesPermissionId, mock.CustServicesPermissionId.ToString());
				if(mock.PermissionName != null)
					query.AppendEquals(CustServicesPermissionColumn.PermissionName, mock.PermissionName.ToString());
				
				TList<CustServicesPermission> results = DataRepository.CustServicesPermissionProvider.Find(tm, query);
				
				Assert.IsTrue(results.Count == 1, "Find is not working correctly.  Failed to find the mock instance");
			}
		}
						
		#region Mock Instance
		///<summary>
		///  Returns a Typed CustServicesPermission Entity with mock values.
		///</summary>
		static public CustServicesPermission CreateMockInstance_Generated(TransactionManager tm)
		{		
			CustServicesPermission mock = new CustServicesPermission();
						
			mock.CustServicesPermissionId = TestUtility.Instance.RandomNumber();
			mock.PermissionName = TestUtility.Instance.RandomString(49, false);;
			
		
			// create a temporary collection and add the item to it
			TList<CustServicesPermission> tempMockCollection = new TList<CustServicesPermission>();
			tempMockCollection.Add(mock);
			tempMockCollection.Remove(mock);
			
		
		   return (CustServicesPermission)mock;
		}
		
		
		///<summary>
		///  Update the Typed CustServicesPermission Entity with modified mock values.
		///</summary>
		static public void UpdateMockInstance_Generated(TransactionManager tm, CustServicesPermission mock)
		{
			mock.PermissionName = TestUtility.Instance.RandomString(49, false);;
			
		}
		#endregion
    }
}
