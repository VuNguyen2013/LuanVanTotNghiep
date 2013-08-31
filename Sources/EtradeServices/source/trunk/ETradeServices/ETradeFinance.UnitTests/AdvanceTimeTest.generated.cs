﻿

/*
	File Generated by NetTiers templates [www.nettiers.com]
	Generated on : Tuesday, December 28, 2010
	Important: Do not modify this file. Edit the file AdvanceTimeTest.cs instead.
*/

#region Using directives

using System;
using System.Xml;
using System.Xml.Serialization;
using NUnit.Framework;
using ETradeFinance.Entities;
using ETradeFinance.DataAccess;
using ETradeFinance.DataAccess.Bases;

#endregion

namespace ETradeFinance.UnitTests
{
    /// <summary>
    /// Provides tests for the and <see cref="AdvanceTime"/> objects (entity, collection and repository).
    /// </summary>
   public partial class AdvanceTimeTest
    {
    	// the AdvanceTime instance used to test the repository.
		private AdvanceTime mock;
		
		// the TList<AdvanceTime> instance used to test the repository.
		private TList<AdvanceTime> mockCollection;
		
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
			System.Console.WriteLine("-- Testing the AdvanceTime Entity with the {0} --", ETradeFinance.DataAccess.DataRepository.Provider.Name);
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
		/// Inserts a mock AdvanceTime entity into the database.
		/// </summary>
		private void Step_01_Insert_Generated()
		{
			using (TransactionManager tm = CreateTransaction())
			{
				mock = CreateMockInstance(tm);
				Assert.IsTrue(DataRepository.AdvanceTimeProvider.Insert(tm, mock), "Insert failed");
										
				System.Console.WriteLine("DataRepository.AdvanceTimeProvider.Insert(mock):");			
				System.Console.WriteLine(mock);			
				
				//normally one would commit here
				//tm.Commit();
				//IDisposable will Rollback Transaction since it's left uncommitted
			}
		}
		
		
		/// <summary>
		/// Selects all AdvanceTime objects of the database.
		/// </summary>
		private void Step_02_SelectAll_Generated()
		{
			using (TransactionManager tm = CreateTransaction())
			{
				//Find
				int count = -1;
				
				mockCollection = DataRepository.AdvanceTimeProvider.Find(tm, null, "", 0, 10, out count );
				Assert.IsTrue(count >= 0 && mockCollection != null, "Query Failed to issue Find Command.");
				
				System.Console.WriteLine("DataRepository.AdvanceTimeProvider.Find():");			
				System.Console.WriteLine(mockCollection);
				
				// GetPaged
				count = -1;
				
				mockCollection = DataRepository.AdvanceTimeProvider.GetPaged(tm, 0, 10, out count);
				Assert.IsTrue(count >= 0 && mockCollection != null, "Query Failed to issue GetPaged Command.");
				System.Console.WriteLine("#get paged count: " + count.ToString());
			}
		}
		
		
		
		
		/// <summary>
		/// Deep load all AdvanceTime children.
		/// </summary>
		private void Step_03_DeepLoad_Generated()
		{
			using (TransactionManager tm = CreateTransaction())
			{
				int count = -1;
				mock =  CreateMockInstance(tm);
				mockCollection = DataRepository.AdvanceTimeProvider.GetPaged(tm, 0, 10, out count);
			
				DataRepository.AdvanceTimeProvider.DeepLoading += new EntityProviderBaseCore<AdvanceTime, AdvanceTimeKey>.DeepLoadingEventHandler(
						delegate(object sender, DeepSessionEventArgs e)
						{
							if (e.DeepSession.Count > 3)
								e.Cancel = true;
						}
					);

				if (mockCollection.Count > 0)
				{
					
					DataRepository.AdvanceTimeProvider.DeepLoad(tm, mockCollection[0]);
					System.Console.WriteLine("AdvanceTime instance correctly deep loaded at 1 level.");
									
					mockCollection.Add(mock);
					// DataRepository.AdvanceTimeProvider.DeepSave(tm, mockCollection);
				}
				
				//normally one would commit here
				//tm.Commit();
				//IDisposable will Rollback Transaction since it's left uncommitted
			}
		}
		
		/// <summary>
		/// Updates a mock AdvanceTime entity into the database.
		/// </summary>
		private void Step_04_Update_Generated()
		{
			using (TransactionManager tm = CreateTransaction())
			{
				AdvanceTime mock = CreateMockInstance(tm);
				Assert.IsTrue(DataRepository.AdvanceTimeProvider.Insert(tm, mock), "Insert failed");
				
				UpdateMockInstance(tm, mock);
				Assert.IsTrue(DataRepository.AdvanceTimeProvider.Update(tm, mock), "Update failed.");			
				
				System.Console.WriteLine("DataRepository.AdvanceTimeProvider.Update(mock):");			
				System.Console.WriteLine(mock);
				
				//normally one would commit here
				//tm.Commit();
				//IDisposable will Rollback Transaction since it's left uncommitted
			}
		}
		
		
		/// <summary>
		/// Delete the mock AdvanceTime entity into the database.
		/// </summary>
		private void Step_05_Delete_Generated()
		{
			using (TransactionManager tm = CreateTransaction())
			{
				mock =  (AdvanceTime)CreateMockInstance(tm);
				DataRepository.AdvanceTimeProvider.Insert(tm, mock);
			
				Assert.IsTrue(DataRepository.AdvanceTimeProvider.Delete(tm, mock), "Delete failed.");
				System.Console.WriteLine("DataRepository.AdvanceTimeProvider.Delete(mock):");			
				System.Console.WriteLine(mock);
				
				//normally one would commit here
				//tm.Commit();
				//IDisposable will Rollback Transaction since it's left uncommitted
			}
		}
		
		#region Serialization tests
		
		/// <summary>
		/// Serialize the mock AdvanceTime entity into a temporary file.
		/// </summary>
		private void Step_06_SerializeEntity_Generated()
		{	
			using (TransactionManager tm = CreateTransaction())
			{
				mock =  CreateMockInstance(tm);
				string fileName = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "temp_AdvanceTime.xml");
			
				EntityHelper.SerializeXml(mock, fileName);
				Assert.IsTrue(System.IO.File.Exists(fileName), "Serialized mock not found");
					
				System.Console.WriteLine("mock correctly serialized to a temporary file.");			
			}
		}
		
		/// <summary>
		/// Deserialize the mock AdvanceTime entity from a temporary file.
		/// </summary>
		private void Step_07_DeserializeEntity_Generated()
		{
			string fileName = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "temp_AdvanceTime.xml");
			Assert.IsTrue(System.IO.File.Exists(fileName), "Serialized mock file not found to deserialize");
			
			using (System.IO.StreamReader sr = System.IO.File.OpenText(fileName))
			{
				object item = EntityHelper.DeserializeEntityXml<AdvanceTime>(sr.ReadToEnd());
				sr.Close();
			}
			System.IO.File.Delete(fileName);
			
			System.Console.WriteLine("mock correctly deserialized from a temporary file.");
		}
		
		/// <summary>
		/// Serialize a AdvanceTime collection into a temporary file.
		/// </summary>
		private void Step_08_SerializeCollection_Generated()
		{
			using (TransactionManager tm = CreateTransaction())
			{
				string fileName = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "temp_AdvanceTimeCollection.xml");
				
				mock = CreateMockInstance(tm);
				TList<AdvanceTime> mockCollection = new TList<AdvanceTime>();
				mockCollection.Add(mock);
			
				EntityHelper.SerializeXml(mockCollection, fileName);
				
				Assert.IsTrue(System.IO.File.Exists(fileName), "Serialized mock collection not found");
				System.Console.WriteLine("TList<AdvanceTime> correctly serialized to a temporary file.");					
			}
		}
		
		
		/// <summary>
		/// Deserialize a AdvanceTime collection from a temporary file.
		/// </summary>
		private void Step_09_DeserializeCollection_Generated()
		{
			string fileName = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "temp_AdvanceTimeCollection.xml");
			Assert.IsTrue(System.IO.File.Exists(fileName), "Serialized mock file not found to deserialize");
			
			XmlSerializer mySerializer = new XmlSerializer(typeof(TList<AdvanceTime>)); 
			using (System.IO.FileStream myFileStream = new System.IO.FileStream(fileName,  System.IO.FileMode.Open))
			{
				TList<AdvanceTime> mockCollection = (TList<AdvanceTime>) mySerializer.Deserialize(myFileStream);
				myFileStream.Close();
			}
			
			System.IO.File.Delete(fileName);
			System.Console.WriteLine("TList<AdvanceTime> correctly deserialized from a temporary file.");	
		}
		#endregion
		
		
		
		/// <summary>
		/// Check the foreign key dal methods.
		/// </summary>
		private void Step_10_FK_Generated()
		{
			using (TransactionManager tm = CreateTransaction())
			{
				AdvanceTime entity = CreateMockInstance(tm);
				bool result = DataRepository.AdvanceTimeProvider.Insert(tm, entity);
				
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
				AdvanceTime entity = CreateMockInstance(tm);
				bool result = DataRepository.AdvanceTimeProvider.Insert(tm, entity);
				
				Assert.IsTrue(result, "Could Not Test IX, Insert Failed");

			
				AdvanceTime t0 = DataRepository.AdvanceTimeProvider.GetById(tm, entity.Id);
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
				
				AdvanceTime entity = mock.Copy() as AdvanceTime;
				entity = (AdvanceTime)mock.Clone();
				Assert.IsTrue(AdvanceTime.ValueEquals(entity, mock), "Clone is not working");
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
				AdvanceTime mock = CreateMockInstance(tm);
				bool result = DataRepository.AdvanceTimeProvider.Insert(tm, mock);
				
				Assert.IsTrue(result, "Could Not Test FindByQuery, Insert Failed");

				AdvanceTimeQuery query = new AdvanceTimeQuery();
			
				query.AppendEquals(AdvanceTimeColumn.StartTime, mock.StartTime.ToString());
				query.AppendEquals(AdvanceTimeColumn.EndTime, mock.EndTime.ToString());
				query.AppendEquals(AdvanceTimeColumn.AdvanceType, mock.AdvanceType.ToString());
				
				TList<AdvanceTime> results = DataRepository.AdvanceTimeProvider.Find(tm, query);
				
				Assert.IsTrue(results.Count == 1, "Find is not working correctly.  Failed to find the mock instance");
			}
		}
						
		#region Mock Instance
		///<summary>
		///  Returns a Typed AdvanceTime Entity with mock values.
		///</summary>
		static public AdvanceTime CreateMockInstance_Generated(TransactionManager tm)
		{		
			AdvanceTime mock = new AdvanceTime();
						
			mock.StartTime = TestUtility.Instance.RandomDateTime();
			mock.EndTime = TestUtility.Instance.RandomDateTime();
			mock.AdvanceType = TestUtility.Instance.RandomNumber();
			
		
			// create a temporary collection and add the item to it
			TList<AdvanceTime> tempMockCollection = new TList<AdvanceTime>();
			tempMockCollection.Add(mock);
			tempMockCollection.Remove(mock);
			
		
		   return (AdvanceTime)mock;
		}
		
		
		///<summary>
		///  Update the Typed AdvanceTime Entity with modified mock values.
		///</summary>
		static public void UpdateMockInstance_Generated(TransactionManager tm, AdvanceTime mock)
		{
			mock.StartTime = TestUtility.Instance.RandomDateTime();
			mock.EndTime = TestUtility.Instance.RandomDateTime();
			mock.AdvanceType = TestUtility.Instance.RandomNumber();
			
		}
		#endregion
    }
}