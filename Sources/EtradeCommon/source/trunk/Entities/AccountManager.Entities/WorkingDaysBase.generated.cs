﻿
/*
	File generated by NetTiers templates [www.nettiers.com]
	Generated on : Monday, November 15, 2010
	Important: Do not modify this file. Edit the file WorkingDays.cs instead.
*/

#region using directives
using System;
using System.ComponentModel;
using System.Collections;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;

using AccountManager.Entities.Validation;
#endregion

namespace AccountManager.Entities
{
	///<summary>
	/// An object representation of the 'WorkingDays' table. [No description found the database]	
	///</summary>
	[Serializable]
	[DataObject, CLSCompliant(true)]
	public abstract partial class WorkingDaysBase : EntityBase, IWorkingDays, IEntityId<WorkingDaysKey>, System.IComparable, System.ICloneable, ICloneableEx, IEditableObject, IComponent, INotifyPropertyChanged
	{		
		#region Variable Declarations
		
		/// <summary>
		///  Hold the inner data of the entity.
		/// </summary>
		private WorkingDaysEntityData entityData;
		
		/// <summary>
		/// 	Hold the original data of the entity, as loaded from the repository.
		/// </summary>
		private WorkingDaysEntityData _originalData;
		
		/// <summary>
		/// 	Hold a backup of the inner data of the entity.
		/// </summary>
		private WorkingDaysEntityData backupData; 
		
		/// <summary>
		/// 	Key used if Tracking is Enabled for the <see cref="EntityLocator" />.
		/// </summary>
		private string entityTrackingKey;
		
		/// <summary>
		/// 	Hold the parent TList&lt;entity&gt; in which this entity maybe contained.
		/// </summary>
		/// <remark>Mostly used for databinding</remark>
		[NonSerialized]
		private TList<WorkingDays> parentCollection;
		
		private bool inTxn = false;
		
		/// <summary>
		/// Occurs when a value is being changed for the specified column.
		/// </summary>
		[field:NonSerialized]
		public event WorkingDaysEventHandler ColumnChanging;		
		
		/// <summary>
		/// Occurs after a value has been changed for the specified column.
		/// </summary>
		[field:NonSerialized]
		public event WorkingDaysEventHandler ColumnChanged;
		
		#endregion Variable Declarations
		
		#region Constructors
		///<summary>
		/// Creates a new <see cref="WorkingDaysBase"/> instance.
		///</summary>
		public WorkingDaysBase()
		{
			this.entityData = new WorkingDaysEntityData();
			this.backupData = null;
		}		
		
		///<summary>
		/// Creates a new <see cref="WorkingDaysBase"/> instance.
		///</summary>
		///<param name="_dateId">Id cua ngay(2=Thu 2, 3 = Thu 3, ..., 8=Chu nhat)</param>
		///<param name="_isWorkingDay">true neu la working day, nguoc lai false</param>
		public WorkingDaysBase(System.Int32 _dateId, System.Boolean _isWorkingDay)
		{
			this.entityData = new WorkingDaysEntityData();
			this.backupData = null;

			this.DateId = _dateId;
			this.IsWorkingDay = _isWorkingDay;
		}
		
		///<summary>
		/// A simple factory method to create a new <see cref="WorkingDays"/> instance.
		///</summary>
		///<param name="_dateId">Id cua ngay(2=Thu 2, 3 = Thu 3, ..., 8=Chu nhat)</param>
		///<param name="_isWorkingDay">true neu la working day, nguoc lai false</param>
		public static WorkingDays CreateWorkingDays(System.Int32 _dateId, System.Boolean _isWorkingDay)
		{
			WorkingDays newWorkingDays = new WorkingDays();
			newWorkingDays.DateId = _dateId;
			newWorkingDays.IsWorkingDay = _isWorkingDay;
			return newWorkingDays;
		}
				
		#endregion Constructors
			
		#region Properties	
		
		#region Data Properties		
		/// <summary>
		/// 	Gets or sets the DateId property. 
		///		Id cua ngay(2=Thu 2, 3 = Thu 3, ..., 8=Chu nhat)
		/// </summary>
		/// <value>This type is int.</value>
		/// <remarks>
		/// This property can not be set to null. 
		/// </remarks>




		[DescriptionAttribute(@"Id cua ngay(2=Thu 2, 3 = Thu 3, ..., 8=Chu nhat)"), System.ComponentModel.Bindable( System.ComponentModel.BindableSupport.Yes)]
		[DataObjectField(true, false, false)]
		public virtual System.Int32 DateId
		{
			get
			{
				return this.entityData.DateId; 
			}
			
			set
			{
				if (this.entityData.DateId == value)
					return;
					
				OnColumnChanging(WorkingDaysColumn.DateId, this.entityData.DateId);
				this.entityData.DateId = value;
				this.EntityId.DateId = value;
				if (this.EntityState == EntityState.Unchanged)
					this.EntityState = EntityState.Changed;
				OnColumnChanged(WorkingDaysColumn.DateId, this.entityData.DateId);
				OnPropertyChanged("DateId");
			}
		}
		
		/// <summary>
		/// 	Get the original value of the DateId property.
		///		Id cua ngay(2=Thu 2, 3 = Thu 3, ..., 8=Chu nhat)
		/// </summary>
		/// <remarks>This is the original value of the DateId property.</remarks>
		/// <value>This type is int</value>
		[BrowsableAttribute(false)/*, XmlIgnoreAttribute()*/]
		public  virtual System.Int32 OriginalDateId
		{
			get { return this.entityData.OriginalDateId; }
			set { this.entityData.OriginalDateId = value; }
		}
		
		/// <summary>
		/// 	Gets or sets the IsWorkingDay property. 
		///		true neu la working day, nguoc lai false
		/// </summary>
		/// <value>This type is bit.</value>
		/// <remarks>
		/// This property can not be set to null. 
		/// </remarks>




		[DescriptionAttribute(@"true neu la working day, nguoc lai false"), System.ComponentModel.Bindable( System.ComponentModel.BindableSupport.Yes)]
		[DataObjectField(false, false, false)]
		public virtual System.Boolean IsWorkingDay
		{
			get
			{
				return this.entityData.IsWorkingDay; 
			}
			
			set
			{
				if (this.entityData.IsWorkingDay == value)
					return;
					
				OnColumnChanging(WorkingDaysColumn.IsWorkingDay, this.entityData.IsWorkingDay);
				this.entityData.IsWorkingDay = value;
				if (this.EntityState == EntityState.Unchanged)
					this.EntityState = EntityState.Changed;
				OnColumnChanged(WorkingDaysColumn.IsWorkingDay, this.entityData.IsWorkingDay);
				OnPropertyChanged("IsWorkingDay");
			}
		}
		
		#endregion Data Properties		

		#region Source Foreign Key Property
				
		#endregion
		
		#region Children Collections
		#endregion Children Collections
		
		#endregion
		#region Validation
		
		/// <summary>
		/// Assigns validation rules to this object based on model definition.
		/// </summary>
		/// <remarks>This method overrides the base class to add schema related validation.</remarks>
		protected override void AddValidationRules()
		{
			//Validation rules based on database schema.
		}
   		#endregion
		
		#region Table Meta Data
		/// <summary>
		///		The name of the underlying database table.
		/// </summary>
		[BrowsableAttribute(false), XmlIgnoreAttribute(), ScriptIgnore()]
		public override string TableName
		{
			get { return "WorkingDays"; }
		}
		
		/// <summary>
		///		The name of the underlying database table's columns.
		/// </summary>
		[BrowsableAttribute(false), XmlIgnoreAttribute(), ScriptIgnore()]
		public override string[] TableColumns
		{
			get
			{
				return new string[] {"DateId", "IsWorkingDay"};
			}
		}
		#endregion 
		
		#region IEditableObject
		
		#region  CancelAddNew Event
		/// <summary>
        /// The delegate for the CancelAddNew event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		public delegate void CancelAddNewEventHandler(object sender, EventArgs e);
    
    	/// <summary>
		/// The CancelAddNew event.
		/// </summary>
		[field:NonSerialized]
		public event CancelAddNewEventHandler CancelAddNew ;

		/// <summary>
        /// Called when [cancel add new].
        /// </summary>
        public void OnCancelAddNew()
        {    
			if (!SuppressEntityEvents)
			{
	            CancelAddNewEventHandler handler = CancelAddNew ;
            	if (handler != null)
	            {    
    	            handler(this, EventArgs.Empty) ;
        	    }
	        }
        }
		#endregion 
		
		/// <summary>
		/// Begins an edit on an object.
		/// </summary>
		void IEditableObject.BeginEdit() 
	    {
	        //Console.WriteLine("Start BeginEdit");
	        if (!inTxn) 
	        {
	            this.backupData = this.entityData.Clone() as WorkingDaysEntityData;
	            inTxn = true;
	            //Console.WriteLine("BeginEdit");
	        }
	        //Console.WriteLine("End BeginEdit");
	    }
	
		/// <summary>
		/// Discards changes since the last <c>BeginEdit</c> call.
		/// </summary>
	    void IEditableObject.CancelEdit() 
	    {
	        //Console.WriteLine("Start CancelEdit");
	        if (this.inTxn) 
	        {
	            this.entityData = this.backupData;
	            this.backupData = null;
				this.inTxn = false;

				if (this.bindingIsNew)
	        	//if (this.EntityState == EntityState.Added)
	        	{
					if (this.parentCollection != null)
						this.parentCollection.Remove( (WorkingDays) this ) ;
				}	            
	        }
	        //Console.WriteLine("End CancelEdit");
	    }
	
		/// <summary>
		/// Pushes changes since the last <c>BeginEdit</c> or <c>IBindingList.AddNew</c> call into the underlying object.
		/// </summary>
	    void IEditableObject.EndEdit() 
	    {
	        //Console.WriteLine("Start EndEdit" + this.custData.id + this.custData.lastName);
	        if (this.inTxn) 
	        {
	            this.backupData = null;
				if (this.IsDirty) 
				{
					if (this.bindingIsNew) {
						this.EntityState = EntityState.Added;
						this.bindingIsNew = false ;
					}
					else
						if (this.EntityState == EntityState.Unchanged) 
							this.EntityState = EntityState.Changed ;
				}

				this.bindingIsNew = false ;
	            this.inTxn = false;	            
	        }
	        //Console.WriteLine("End EndEdit");
	    }
	    
	    /// <summary>
        /// Gets or sets the parent collection of this current entity, if available.
        /// </summary>
        /// <value>The parent collection.</value>
	    [XmlIgnore]
        [ScriptIgnore]
		[Browsable(false)]
	    public override object ParentCollection
	    {
	        get 
	        {
	            return this.parentCollection;
	        }
	        set 
	        {
	            this.parentCollection = value as TList<WorkingDays>;
	        }
	    }
	    
	    /// <summary>
        /// Called when the entity is changed.
        /// </summary>
	    private void OnEntityChanged() 
	    {
	        if (!SuppressEntityEvents && !inTxn && this.parentCollection != null) 
	        {
	            this.parentCollection.EntityChanged(this as WorkingDays);
	        }
	    }


		#endregion
		
		#region ICloneable Members
		///<summary>
		///  Returns a Typed WorkingDays Entity 
		///</summary>
		protected virtual WorkingDays Copy(IDictionary existingCopies)
		{
			if (existingCopies == null)
			{
				// This is the root of the tree to be copied!
				existingCopies = new Hashtable();
			}

			//shallow copy entity
			WorkingDays copy = new WorkingDays();
			existingCopies.Add(this, copy);
			copy.SuppressEntityEvents = true;
				copy.DateId = this.DateId;
					copy.OriginalDateId = this.OriginalDateId;
				copy.IsWorkingDay = this.IsWorkingDay;
			
		
			copy.EntityState = this.EntityState;
			copy.SuppressEntityEvents = false;
			return copy;
		}		
		
		
		
		///<summary>
		///  Returns a Typed WorkingDays Entity 
		///</summary>
		public virtual WorkingDays Copy()
		{
			return this.Copy(null);	
		}
		
		///<summary>
		/// ICloneable.Clone() Member, returns the Shallow Copy of this entity.
		///</summary>
		public object Clone()
		{
			return this.Copy(null);
		}
		
		///<summary>
		/// ICloneableEx.Clone() Member, returns the Shallow Copy of this entity.
		///</summary>
		public object Clone(IDictionary existingCopies)
		{
			return this.Copy(existingCopies);
		}
		
		///<summary>
		/// Returns a deep copy of the child collection object passed in.
		///</summary>
		public static object MakeCopyOf(object x)
		{
			if (x == null)
				return null;
				
			if (x is ICloneable)
			{
				// Return a deep copy of the object
				return ((ICloneable)x).Clone();
			}
			else
				throw new System.NotSupportedException("Object Does Not Implement the ICloneable Interface.");
		}
		
		///<summary>
		/// Returns a deep copy of the child collection object passed in.
		///</summary>
		public static object MakeCopyOf(object x, IDictionary existingCopies)
		{
			if (x == null)
				return null;
			
			if (x is ICloneableEx)
			{
				// Return a deep copy of the object
				return ((ICloneableEx)x).Clone(existingCopies);
			}
			else if (x is ICloneable)
			{
				// Return a deep copy of the object
				return ((ICloneable)x).Clone();
			}
			else
				throw new System.NotSupportedException("Object Does Not Implement the ICloneable or IClonableEx Interface.");
		}
		
		
		///<summary>
		///  Returns a Typed WorkingDays Entity which is a deep copy of the current entity.
		///</summary>
		public virtual WorkingDays DeepCopy()
		{
			return EntityHelper.Clone<WorkingDays>(this as WorkingDays);	
		}
		#endregion
		
		#region Methods	
			
		///<summary>
		/// Revert all changes and restore original values.
		///</summary>
		public override void CancelChanges()
		{
			IEditableObject obj = (IEditableObject) this;
			obj.CancelEdit();

			this.entityData = null;
			if (this._originalData != null)
			{
				this.entityData = this._originalData.Clone() as WorkingDaysEntityData;
			}
			else
			{
				//Since this had no _originalData, then just reset the entityData with a new one.  entityData cannot be null.
				this.entityData = new WorkingDaysEntityData();
			}
		}	
		
		/// <summary>
		/// Accepts the changes made to this object.
		/// </summary>
		/// <remarks>
		/// After calling this method, properties: IsDirty, IsNew are false. IsDeleted flag remains unchanged as it is handled by the parent List.
		/// </remarks>
		public override void AcceptChanges()
		{
			base.AcceptChanges();

			// we keep of the original version of the data
			this._originalData = null;
			this._originalData = this.entityData.Clone() as WorkingDaysEntityData;
		}
		
		#region Comparision with original data
		
		/// <summary>
		/// Determines whether the property value has changed from the original data.
		/// </summary>
		/// <param name="column">The column.</param>
		/// <returns>
		/// 	<c>true</c> if the property value has changed; otherwise, <c>false</c>.
		/// </returns>
		public bool IsPropertyChanged(WorkingDaysColumn column)
		{
			switch(column)
			{
					case WorkingDaysColumn.DateId:
					return entityData.DateId != _originalData.DateId;
					case WorkingDaysColumn.IsWorkingDay:
					return entityData.IsWorkingDay != _originalData.IsWorkingDay;
			
				default:
					return false;
			}
		}
		
		/// <summary>
		/// Determines whether the property value has changed from the original data.
		/// </summary>
		/// <param name="columnName">The column name.</param>
		/// <returns>
		/// 	<c>true</c> if the property value has changed; otherwise, <c>false</c>.
		/// </returns>
		public override bool IsPropertyChanged(string columnName)
		{
			return 	IsPropertyChanged(EntityHelper.GetEnumValue< WorkingDaysColumn >(columnName));
		}
		
		/// <summary>
		/// Determines whether the data has changed from original.
		/// </summary>
		/// <returns>
		/// 	<c>true</c> if data has changed; otherwise, <c>false</c>.
		/// </returns>
		public bool HasDataChanged()
		{
			bool result = false;
			result = result || entityData.DateId != _originalData.DateId;
			result = result || entityData.IsWorkingDay != _originalData.IsWorkingDay;
			return result;
		}	
		
		///<summary>
		///  Returns a WorkingDays Entity with the original data.
		///</summary>
		public WorkingDays GetOriginalEntity()
		{
			if (_originalData != null)
				return CreateWorkingDays(
				_originalData.DateId,
				_originalData.IsWorkingDay
				);
				
			return (WorkingDays)this.Clone();
		}
		#endregion
	
	#region Value Semantics Instance Equality
        ///<summary>
        /// Returns a value indicating whether this instance is equal to a specified object using value semantics.
        ///</summary>
        ///<param name="Object1">An object to compare to this instance.</param>
        ///<returns>true if Object1 is a <see cref="WorkingDaysBase"/> and has the same value as this instance; otherwise, false.</returns>
        public override bool Equals(object Object1)
        {
			// Cast exception if Object1 is null or DbNull
			if (Object1 != null && Object1 != DBNull.Value && Object1 is WorkingDaysBase)
				return ValueEquals(this, (WorkingDaysBase)Object1);
			else
				return false;
        }

        /// <summary>
		/// Serves as a hash function for a particular type, suitable for use in hashing algorithms and data structures like a hash table.
        /// Provides a hash function that is appropriate for <see cref="WorkingDaysBase"/> class 
        /// and that ensures a better distribution in the hash table
        /// </summary>
        /// <returns>number (hash code) that corresponds to the value of an object</returns>
        public override int GetHashCode()
        {
			return this.DateId.GetHashCode() ^ 
					this.IsWorkingDay.GetHashCode();
        }
		
		///<summary>
		/// Returns a value indicating whether this instance is equal to a specified object using value semantics.
		///</summary>
		///<param name="toObject">An object to compare to this instance.</param>
		///<returns>true if toObject is a <see cref="WorkingDaysBase"/> and has the same value as this instance; otherwise, false.</returns>
		public virtual bool Equals(WorkingDaysBase toObject)
		{
			if (toObject == null)
				return false;
			return ValueEquals(this, toObject);
		}
		#endregion
		
		///<summary>
		/// Determines whether the specified <see cref="WorkingDaysBase"/> instances are considered equal using value semantics.
		///</summary>
		///<param name="Object1">The first <see cref="WorkingDaysBase"/> to compare.</param>
		///<param name="Object2">The second <see cref="WorkingDaysBase"/> to compare. </param>
		///<returns>true if Object1 is the same instance as Object2 or if both are null references or if objA.Equals(objB) returns true; otherwise, false.</returns>
		public static bool ValueEquals(WorkingDaysBase Object1, WorkingDaysBase Object2)
		{
			// both are null
			if (Object1 == null && Object2 == null)
				return true;

			// one or the other is null, but not both
			if (Object1 == null ^ Object2 == null)
				return false;
				
			bool equal = true;
			if (Object1.DateId != Object2.DateId)
				equal = false;
			if (Object1.IsWorkingDay != Object2.IsWorkingDay)
				equal = false;
					
			return equal;
		}
		
		#endregion
		
		#region IComparable Members
		///<summary>
		/// Compares this instance to a specified object and returns an indication of their relative values.
		///<param name="obj">An object to compare to this instance, or a null reference (Nothing in Visual Basic).</param>
		///</summary>
		///<returns>A signed integer that indicates the relative order of this instance and obj.</returns>
		public virtual int CompareTo(object obj)
		{
			throw new NotImplementedException();
			//return this. GetPropertyName(SourceTable.PrimaryKey.MemberColumns[0]) .CompareTo(((WorkingDaysBase)obj).GetPropertyName(SourceTable.PrimaryKey.MemberColumns[0]));
		}
		
		/*
		// static method to get a Comparer object
        public static WorkingDaysComparer GetComparer()
        {
            return new WorkingDaysComparer();
        }
        */

        // Comparer delegates back to WorkingDays
        // Employee uses the integer's default
        // CompareTo method
        /*
        public int CompareTo(Item rhs)
        {
            return this.Id.CompareTo(rhs.Id);
        }
        */

/*
        // Special implementation to be called by custom comparer
        public int CompareTo(WorkingDays rhs, WorkingDaysColumn which)
        {
            switch (which)
            {
            	
            	
            	case WorkingDaysColumn.DateId:
            		return this.DateId.CompareTo(rhs.DateId);
            		
            		                 
            	
            	
            	case WorkingDaysColumn.IsWorkingDay:
            		return this.IsWorkingDay.CompareTo(rhs.IsWorkingDay);
            		
            		                 
            }
            return 0;
        }
        */
	
		#endregion
		
		#region IComponent Members
		
		private ISite _site = null;

		/// <summary>
		/// Gets or Sets the site where this data is located.
		/// </summary>
		[XmlIgnore]
        [ScriptIgnore]
		[SoapIgnore]
		[Browsable(false)]
		public ISite Site
		{
			get{ return this._site; }
			set{ this._site = value; }
		}

		#endregion

		#region IDisposable Members
		
		/// <summary>
		/// Notify those that care when we dispose.
		/// </summary>
		[field:NonSerialized]
		public event System.EventHandler Disposed;

		/// <summary>
		/// Clean up. Nothing here though.
		/// </summary>
		public virtual void Dispose()
		{
			this.parentCollection = null;
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}
		
		/// <summary>
		/// Clean up.
		/// </summary>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				EventHandler handler = Disposed;
				if (handler != null)
					handler(this, EventArgs.Empty);
			}
		}
		
		#endregion
				
		#region IEntityKey<WorkingDaysKey> Members
		
		// member variable for the EntityId property
		private WorkingDaysKey _entityId;

		/// <summary>
		/// Gets or sets the EntityId property.
		/// </summary>
		[XmlIgnore]
        [ScriptIgnore]
		public virtual WorkingDaysKey EntityId
		{
			get
			{
				if ( _entityId == null )
				{
					_entityId = new WorkingDaysKey(this);
				}

				return _entityId;
			}
			set
			{
				if ( value != null )
				{
					value.Entity = this;
				}
				
				_entityId = value;
			}
		}
		
		#endregion
		
		#region EntityState
		/// <summary>
		///		Indicates state of object
		/// </summary>
		/// <remarks>0=Unchanged, 1=Added, 2=Changed</remarks>
		[BrowsableAttribute(false) , XmlIgnoreAttribute(), ScriptIgnore()]
		public override EntityState EntityState 
		{ 
			get{ return entityData.EntityState;	 } 
			set{ entityData.EntityState = value; } 
		}
		#endregion 
		
		#region EntityTrackingKey
		///<summary>
		/// Provides the tracking key for the <see cref="EntityLocator"/>
		///</summary>
		[XmlIgnore]
        [ScriptIgnore]
		public override string EntityTrackingKey
		{
			get
			{
				if(entityTrackingKey == null)
					entityTrackingKey = new System.Text.StringBuilder("WorkingDays")
					.Append("|").Append( this.DateId.ToString()).ToString();
				return entityTrackingKey;
			}
			set
		    {
		        if (value != null)
                    entityTrackingKey = value;
		    }
		}
		#endregion 
		
		#region ToString Method
		
		///<summary>
		/// Returns a String that represents the current object.
		///</summary>
		public override string ToString()
		{
			return string.Format(System.Globalization.CultureInfo.InvariantCulture,
				"{3}{2}- DateId: {0}{2}- IsWorkingDay: {1}{2}{4}", 
				this.DateId,
				this.IsWorkingDay,
				System.Environment.NewLine, 
				this.GetType(),
				this.Error.Length == 0 ? string.Empty : string.Format("- Error: {0}\n",this.Error));
		}
		
		#endregion ToString Method
		
		#region Inner data class
		
	/// <summary>
	///		The data structure representation of the 'WorkingDays' table.
	/// </summary>
	/// <remarks>
	/// 	This struct is generated by a tool and should never be modified.
	/// </remarks>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Serializable]
	internal protected class WorkingDaysEntityData : ICloneable, ICloneableEx
	{
		#region Variable Declarations
		private EntityState currentEntityState = EntityState.Added;
		
		#region Primary key(s)
		/// <summary>			
		/// DateId : Id cua ngay(2=Thu 2, 3 = Thu 3, ..., 8=Chu nhat)
		/// </summary>
		/// <remarks>Member of the primary key of the underlying table "WorkingDays"</remarks>
		public System.Int32 DateId;
			
		/// <summary>
		/// keep a copy of the original so it can be used for editable primary keys.
		/// </summary>
		public System.Int32 OriginalDateId;
		
		#endregion
		
		#region Non Primary key(s)
		
		
		/// <summary>
		/// IsWorkingDay : true neu la working day, nguoc lai false
		/// </summary>
		public System.Boolean		  IsWorkingDay = false;
		#endregion
			
		#region Source Foreign Key Property
				
		#endregion
		#endregion Variable Declarations
	
		#region Data Properties

		#endregion Data Properties
		
		#region Clone Method

		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		public Object Clone()
		{
			WorkingDaysEntityData _tmp = new WorkingDaysEntityData();
						
			_tmp.DateId = this.DateId;
			_tmp.OriginalDateId = this.OriginalDateId;
			
			_tmp.IsWorkingDay = this.IsWorkingDay;
			
			#region Source Parent Composite Entities
			#endregion
		
			#region Child Collections
			#endregion Child Collections
			
			//EntityState
			_tmp.EntityState = this.EntityState;
			
			return _tmp;
		}
		
		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		public object Clone(IDictionary existingCopies)
		{
			if (existingCopies == null)
				existingCopies = new Hashtable();
				
			WorkingDaysEntityData _tmp = new WorkingDaysEntityData();
						
			_tmp.DateId = this.DateId;
			_tmp.OriginalDateId = this.OriginalDateId;
			
			_tmp.IsWorkingDay = this.IsWorkingDay;
			
			#region Source Parent Composite Entities
			#endregion
		
			#region Child Collections
			#endregion Child Collections
			
			//EntityState
			_tmp.EntityState = this.EntityState;
			
			return _tmp;
		}
		
		#endregion Clone Method
		
		/// <summary>
		///		Indicates state of object
		/// </summary>
		/// <remarks>0=Unchanged, 1=Added, 2=Changed</remarks>
		[BrowsableAttribute(false), XmlIgnoreAttribute(), ScriptIgnore()]
		public EntityState	EntityState
		{
			get { return currentEntityState;  }
			set { currentEntityState = value; }
		}
	
	}//End struct











		#endregion
		
				
		
		#region Events trigger
		/// <summary>
		/// Raises the <see cref="ColumnChanging" /> event.
		/// </summary>
		/// <param name="column">The <see cref="WorkingDaysColumn"/> which has raised the event.</param>
		public virtual void OnColumnChanging(WorkingDaysColumn column)
		{
			OnColumnChanging(column, null);
			return;
		}
		
		/// <summary>
		/// Raises the <see cref="ColumnChanged" /> event.
		/// </summary>
		/// <param name="column">The <see cref="WorkingDaysColumn"/> which has raised the event.</param>
		public virtual void OnColumnChanged(WorkingDaysColumn column)
		{
			OnColumnChanged(column, null);
			return;
		} 
		
		
		/// <summary>
		/// Raises the <see cref="ColumnChanging" /> event.
		/// </summary>
		/// <param name="column">The <see cref="WorkingDaysColumn"/> which has raised the event.</param>
		/// <param name="value">The changed value.</param>
		public virtual void OnColumnChanging(WorkingDaysColumn column, object value)
		{
			if(IsEntityTracked && EntityState != EntityState.Added && !EntityManager.TrackChangedEntities)
				EntityManager.StopTracking(entityTrackingKey);
				
			if (!SuppressEntityEvents)
			{
				WorkingDaysEventHandler handler = ColumnChanging;
				if(handler != null)
				{
					handler(this, new WorkingDaysEventArgs(column, value));
				}
			}
		}
		
		/// <summary>
		/// Raises the <see cref="ColumnChanged" /> event.
		/// </summary>
		/// <param name="column">The <see cref="WorkingDaysColumn"/> which has raised the event.</param>
		/// <param name="value">The changed value.</param>
		public virtual void OnColumnChanged(WorkingDaysColumn column, object value)
		{
			if (!SuppressEntityEvents)
			{
				WorkingDaysEventHandler handler = ColumnChanged;
				if(handler != null)
				{
					handler(this, new WorkingDaysEventArgs(column, value));
				}
			
				// warn the parent list that i have changed
				OnEntityChanged();
			}
		} 
		#endregion
			
	} // End Class
	
	
	#region WorkingDaysEventArgs class
	/// <summary>
	/// Provides data for the ColumnChanging and ColumnChanged events.
	/// </summary>
	/// <remarks>
	/// The ColumnChanging and ColumnChanged events occur when a change is made to the value 
	/// of a property of a <see cref="WorkingDays"/> object.
	/// </remarks>
	public class WorkingDaysEventArgs : System.EventArgs
	{
		private WorkingDaysColumn column;
		private object value;
		
		///<summary>
		/// Initalizes a new Instance of the WorkingDaysEventArgs class.
		///</summary>
		public WorkingDaysEventArgs(WorkingDaysColumn column)
		{
			this.column = column;
		}
		
		///<summary>
		/// Initalizes a new Instance of the WorkingDaysEventArgs class.
		///</summary>
		public WorkingDaysEventArgs(WorkingDaysColumn column, object value)
		{
			this.column = column;
			this.value = value;
		}
		
		///<summary>
		/// The WorkingDaysColumn that was modified, which has raised the event.
		///</summary>
		///<value cref="WorkingDaysColumn" />
		public WorkingDaysColumn Column { get { return this.column; } }
		
		/// <summary>
        /// Gets the current value of the column.
        /// </summary>
        /// <value>The current value of the column.</value>
		public object Value{ get { return this.value; } }

	}
	#endregion
	
	///<summary>
	/// Define a delegate for all WorkingDays related events.
	///</summary>
	public delegate void WorkingDaysEventHandler(object sender, WorkingDaysEventArgs e);
	
	#region WorkingDaysComparer
		
	/// <summary>
	///	Strongly Typed IComparer
	/// </summary>
	public class WorkingDaysComparer : System.Collections.Generic.IComparer<WorkingDays>
	{
		WorkingDaysColumn whichComparison;
		
		/// <summary>
        /// Initializes a new instance of the <see cref="T:WorkingDaysComparer"/> class.
        /// </summary>
		public WorkingDaysComparer()
        {            
        }               
        
        /// <summary>
        /// Initializes a new instance of the <see cref="T:WorkingDaysComparer"/> class.
        /// </summary>
        /// <param name="column">The column to sort on.</param>
        public WorkingDaysComparer(WorkingDaysColumn column)
        {
            this.whichComparison = column;
        }

		/// <summary>
        /// Determines whether the specified <c cref="WorkingDays"/> instances are considered equal.
        /// </summary>
        /// <param name="a">The first <c cref="WorkingDays"/> to compare.</param>
        /// <param name="b">The second <c>WorkingDays</c> to compare.</param>
        /// <returns>true if objA is the same instance as objB or if both are null references or if objA.Equals(objB) returns true; otherwise, false.</returns>
        public bool Equals(WorkingDays a, WorkingDays b)
        {
            return this.Compare(a, b) == 0;
        }

		/// <summary>
        /// Gets the hash code of the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public int GetHashCode(WorkingDays entity)
        {
            return entity.GetHashCode();
        }

        /// <summary>
        /// Performs a case-insensitive comparison of two objects of the same type and returns a value indicating whether one is less than, equal to, or greater than the other.
        /// </summary>
        /// <param name="a">The first object to compare.</param>
        /// <param name="b">The second object to compare.</param>
        /// <returns></returns>
        public int Compare(WorkingDays a, WorkingDays b)
        {
        	EntityPropertyComparer entityPropertyComparer = new EntityPropertyComparer(this.whichComparison.ToString());
        	return entityPropertyComparer.Compare(a, b);
        }

		/// <summary>
        /// Gets or sets the column that will be used for comparison.
        /// </summary>
        /// <value>The comparison column.</value>
        public WorkingDaysColumn WhichComparison
        {
            get { return this.whichComparison; }
            set { this.whichComparison = value; }
        }
	}
	
	#endregion
	
	#region WorkingDaysKey Class

	/// <summary>
	/// Wraps the unique identifier values for the <see cref="WorkingDays"/> object.
	/// </summary>
	[Serializable]
	[CLSCompliant(true)]
	public class WorkingDaysKey : EntityKeyBase
	{
		#region Constructors
		
		/// <summary>
		/// Initializes a new instance of the WorkingDaysKey class.
		/// </summary>
		public WorkingDaysKey()
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the WorkingDaysKey class.
		/// </summary>
		public WorkingDaysKey(WorkingDaysBase entity)
		{
			this.Entity = entity;

			#region Init Properties

			if ( entity != null )
			{
				this.DateId = entity.DateId;
			}

			#endregion
		}
		
		/// <summary>
		/// Initializes a new instance of the WorkingDaysKey class.
		/// </summary>
		public WorkingDaysKey(System.Int32 _dateId)
		{
			#region Init Properties

			this.DateId = _dateId;

			#endregion
		}
		
		#endregion Constructors

		#region Properties
		
		// member variable for the Entity property
		private WorkingDaysBase _entity;
		
		/// <summary>
		/// Gets or sets the Entity property.
		/// </summary>
		public WorkingDaysBase Entity
		{
			get { return _entity; }
			set { _entity = value; }
		}
		
		// member variable for the DateId property
		private System.Int32 _dateId;
		
		/// <summary>
		/// Gets or sets the DateId property.
		/// </summary>
		public System.Int32 DateId
		{
			get { return _dateId; }
			set
			{
				if ( this.Entity != null )
					this.Entity.DateId = value;
				
				_dateId = value;
			}
		}
		
		#endregion

		#region Methods
		
		/// <summary>
		/// Reads values from the supplied <see cref="IDictionary"/> object into
		/// properties of the current object.
		/// </summary>
		/// <param name="values">An <see cref="IDictionary"/> instance that contains
		/// the key/value pairs to be used as property values.</param>
		public override void Load(IDictionary values)
		{
			#region Init Properties

			if ( values != null )
			{
				DateId = ( values["DateId"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["DateId"], typeof(System.Int32)) : (int)0;
			}

			#endregion
		}

		/// <summary>
		/// Creates a new <see cref="IDictionary"/> object and populates it
		/// with the property values of the current object.
		/// </summary>
		/// <returns>A collection of name/value pairs.</returns>
		public override IDictionary ToDictionary()
		{
			IDictionary values = new Hashtable();

			#region Init Dictionary

			values.Add("DateId", DateId);

			#endregion Init Dictionary

			return values;
		}
		
		///<summary>
		/// Returns a String that represents the current object.
		///</summary>
		public override string ToString()
		{
			return String.Format("DateId: {0}{1}",
								DateId,
								System.Environment.NewLine);
		}

		#endregion Methods
	}
	
	#endregion	

	#region WorkingDaysColumn Enum
	
	/// <summary>
	/// Enumerate the WorkingDays columns.
	/// </summary>
	[Serializable]
	public enum WorkingDaysColumn : int
	{
		/// <summary>
		/// DateId : Id cua ngay(2=Thu 2, 3 = Thu 3, ..., 8=Chu nhat)
		/// </summary>
		[EnumTextValue("DateId")]
		[ColumnEnum("DateId", typeof(System.Int32), System.Data.DbType.Int32, true, false, false)]
		DateId = 1,
		/// <summary>
		/// IsWorkingDay : true neu la working day, nguoc lai false
		/// </summary>
		[EnumTextValue("IsWorkingDay")]
		[ColumnEnum("IsWorkingDay", typeof(System.Boolean), System.Data.DbType.Boolean, false, false, false)]
		IsWorkingDay = 2
	}//End enum

	#endregion WorkingDaysColumn Enum

} // end namespace