﻿
/*
	File generated by NetTiers templates [www.nettiers.com]
	Generated on : Friday, November 19, 2010
	Important: Do not modify this file. Edit the file Holidays.cs instead.
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
	/// An object representation of the 'Holidays' table. [No description found the database]	
	///</summary>
	[Serializable]
	[DataObject, CLSCompliant(true)]
	public abstract partial class HolidaysBase : EntityBase, IHolidays, IEntityId<HolidaysKey>, System.IComparable, System.ICloneable, ICloneableEx, IEditableObject, IComponent, INotifyPropertyChanged
	{		
		#region Variable Declarations
		
		/// <summary>
		///  Hold the inner data of the entity.
		/// </summary>
		private HolidaysEntityData entityData;
		
		/// <summary>
		/// 	Hold the original data of the entity, as loaded from the repository.
		/// </summary>
		private HolidaysEntityData _originalData;
		
		/// <summary>
		/// 	Hold a backup of the inner data of the entity.
		/// </summary>
		private HolidaysEntityData backupData; 
		
		/// <summary>
		/// 	Key used if Tracking is Enabled for the <see cref="EntityLocator" />.
		/// </summary>
		private string entityTrackingKey;
		
		/// <summary>
		/// 	Hold the parent TList&lt;entity&gt; in which this entity maybe contained.
		/// </summary>
		/// <remark>Mostly used for databinding</remark>
		[NonSerialized]
		private TList<Holidays> parentCollection;
		
		private bool inTxn = false;
		
		/// <summary>
		/// Occurs when a value is being changed for the specified column.
		/// </summary>
		[field:NonSerialized]
		public event HolidaysEventHandler ColumnChanging;		
		
		/// <summary>
		/// Occurs after a value has been changed for the specified column.
		/// </summary>
		[field:NonSerialized]
		public event HolidaysEventHandler ColumnChanged;
		
		#endregion Variable Declarations
		
		#region Constructors
		///<summary>
		/// Creates a new <see cref="HolidaysBase"/> instance.
		///</summary>
		public HolidaysBase()
		{
			this.entityData = new HolidaysEntityData();
			this.backupData = null;
		}		
		
		///<summary>
		/// Creates a new <see cref="HolidaysBase"/> instance.
		///</summary>
		///<param name="_holiday">Ngay le (Chi luu tru ngay, khong luu tru gio)</param>
		///<param name="_note">Ghi chu</param>
		public HolidaysBase(System.DateTime _holiday, System.String _note)
		{
			this.entityData = new HolidaysEntityData();
			this.backupData = null;

			this.Holiday = _holiday;
			this.Note = _note;
		}
		
		///<summary>
		/// A simple factory method to create a new <see cref="Holidays"/> instance.
		///</summary>
		///<param name="_holiday">Ngay le (Chi luu tru ngay, khong luu tru gio)</param>
		///<param name="_note">Ghi chu</param>
		public static Holidays CreateHolidays(System.DateTime _holiday, System.String _note)
		{
			Holidays newHolidays = new Holidays();
			newHolidays.Holiday = _holiday;
			newHolidays.Note = _note;
			return newHolidays;
		}
				
		#endregion Constructors
			
		#region Properties	
		
		#region Data Properties		
		/// <summary>
		/// 	Gets or sets the Holiday property. 
		///		Ngay le (Chi luu tru ngay, khong luu tru gio)
		/// </summary>
		/// <value>This type is datetime.</value>
		/// <remarks>
		/// This property can not be set to null. 
		/// </remarks>




		[DescriptionAttribute(@"Ngay le (Chi luu tru ngay, khong luu tru gio)"), System.ComponentModel.Bindable( System.ComponentModel.BindableSupport.Yes)]
		[DataObjectField(true, false, false)]
		public virtual System.DateTime Holiday
		{
			get
			{
				return this.entityData.Holiday; 
			}
			
			set
			{
				if (this.entityData.Holiday == value)
					return;
					
				OnColumnChanging(HolidaysColumn.Holiday, this.entityData.Holiday);
				this.entityData.Holiday = value;
				this.EntityId.Holiday = value;
				if (this.EntityState == EntityState.Unchanged)
					this.EntityState = EntityState.Changed;
				OnColumnChanged(HolidaysColumn.Holiday, this.entityData.Holiday);
				OnPropertyChanged("Holiday");
			}
		}
		
		/// <summary>
		/// 	Get the original value of the Holiday property.
		///		Ngay le (Chi luu tru ngay, khong luu tru gio)
		/// </summary>
		/// <remarks>This is the original value of the Holiday property.</remarks>
		/// <value>This type is datetime</value>
		[BrowsableAttribute(false)/*, XmlIgnoreAttribute()*/]
		public  virtual System.DateTime OriginalHoliday
		{
			get { return this.entityData.OriginalHoliday; }
			set { this.entityData.OriginalHoliday = value; }
		}
		
		/// <summary>
		/// 	Gets or sets the Note property. 
		///		Ghi chu
		/// </summary>
		/// <value>This type is nvarchar.</value>
		/// <remarks>
		/// This property can be set to null. 
		/// </remarks>




		[XmlElement(IsNullable=true)]
		[DescriptionAttribute(@"Ghi chu"), System.ComponentModel.Bindable( System.ComponentModel.BindableSupport.Yes)]
		[DataObjectField(false, false, true, 255)]
		public virtual System.String Note
		{
			get
			{
				return this.entityData.Note; 
			}
			
			set
			{
				if (this.entityData.Note == value)
					return;
					
				OnColumnChanging(HolidaysColumn.Note, this.entityData.Note);
				this.entityData.Note = value;
				if (this.EntityState == EntityState.Unchanged)
					this.EntityState = EntityState.Changed;
				OnColumnChanged(HolidaysColumn.Note, this.entityData.Note);
				OnPropertyChanged("Note");
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
			ValidationRules.AddRule( CommonRules.StringMaxLength, 
				new CommonRules.MaxLengthRuleArgs("Note", "Note", 255));
		}
   		#endregion
		
		#region Table Meta Data
		/// <summary>
		///		The name of the underlying database table.
		/// </summary>
		[BrowsableAttribute(false), XmlIgnoreAttribute(), ScriptIgnore()]
		public override string TableName
		{
			get { return "Holidays"; }
		}
		
		/// <summary>
		///		The name of the underlying database table's columns.
		/// </summary>
		[BrowsableAttribute(false), XmlIgnoreAttribute(), ScriptIgnore()]
		public override string[] TableColumns
		{
			get
			{
				return new string[] {"Holiday", "Note"};
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
	            this.backupData = this.entityData.Clone() as HolidaysEntityData;
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
						this.parentCollection.Remove( (Holidays) this ) ;
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
	            this.parentCollection = value as TList<Holidays>;
	        }
	    }
	    
	    /// <summary>
        /// Called when the entity is changed.
        /// </summary>
	    private void OnEntityChanged() 
	    {
	        if (!SuppressEntityEvents && !inTxn && this.parentCollection != null) 
	        {
	            this.parentCollection.EntityChanged(this as Holidays);
	        }
	    }


		#endregion
		
		#region ICloneable Members
		///<summary>
		///  Returns a Typed Holidays Entity 
		///</summary>
		protected virtual Holidays Copy(IDictionary existingCopies)
		{
			if (existingCopies == null)
			{
				// This is the root of the tree to be copied!
				existingCopies = new Hashtable();
			}

			//shallow copy entity
			Holidays copy = new Holidays();
			existingCopies.Add(this, copy);
			copy.SuppressEntityEvents = true;
				copy.Holiday = this.Holiday;
					copy.OriginalHoliday = this.OriginalHoliday;
				copy.Note = this.Note;
			
		
			copy.EntityState = this.EntityState;
			copy.SuppressEntityEvents = false;
			return copy;
		}		
		
		
		
		///<summary>
		///  Returns a Typed Holidays Entity 
		///</summary>
		public virtual Holidays Copy()
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
		///  Returns a Typed Holidays Entity which is a deep copy of the current entity.
		///</summary>
		public virtual Holidays DeepCopy()
		{
			return EntityHelper.Clone<Holidays>(this as Holidays);	
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
				this.entityData = this._originalData.Clone() as HolidaysEntityData;
			}
			else
			{
				//Since this had no _originalData, then just reset the entityData with a new one.  entityData cannot be null.
				this.entityData = new HolidaysEntityData();
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
			this._originalData = this.entityData.Clone() as HolidaysEntityData;
		}
		
		#region Comparision with original data
		
		/// <summary>
		/// Determines whether the property value has changed from the original data.
		/// </summary>
		/// <param name="column">The column.</param>
		/// <returns>
		/// 	<c>true</c> if the property value has changed; otherwise, <c>false</c>.
		/// </returns>
		public bool IsPropertyChanged(HolidaysColumn column)
		{
			switch(column)
			{
					case HolidaysColumn.Holiday:
					return entityData.Holiday != _originalData.Holiday;
					case HolidaysColumn.Note:
					return entityData.Note != _originalData.Note;
			
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
			return 	IsPropertyChanged(EntityHelper.GetEnumValue< HolidaysColumn >(columnName));
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
			result = result || entityData.Holiday != _originalData.Holiday;
			result = result || entityData.Note != _originalData.Note;
			return result;
		}	
		
		///<summary>
		///  Returns a Holidays Entity with the original data.
		///</summary>
		public Holidays GetOriginalEntity()
		{
			if (_originalData != null)
				return CreateHolidays(
				_originalData.Holiday,
				_originalData.Note
				);
				
			return (Holidays)this.Clone();
		}
		#endregion
	
	#region Value Semantics Instance Equality
        ///<summary>
        /// Returns a value indicating whether this instance is equal to a specified object using value semantics.
        ///</summary>
        ///<param name="Object1">An object to compare to this instance.</param>
        ///<returns>true if Object1 is a <see cref="HolidaysBase"/> and has the same value as this instance; otherwise, false.</returns>
        public override bool Equals(object Object1)
        {
			// Cast exception if Object1 is null or DbNull
			if (Object1 != null && Object1 != DBNull.Value && Object1 is HolidaysBase)
				return ValueEquals(this, (HolidaysBase)Object1);
			else
				return false;
        }

        /// <summary>
		/// Serves as a hash function for a particular type, suitable for use in hashing algorithms and data structures like a hash table.
        /// Provides a hash function that is appropriate for <see cref="HolidaysBase"/> class 
        /// and that ensures a better distribution in the hash table
        /// </summary>
        /// <returns>number (hash code) that corresponds to the value of an object</returns>
        public override int GetHashCode()
        {
			return this.Holiday.GetHashCode() ^ 
					((this.Note == null) ? string.Empty : this.Note.ToString()).GetHashCode();
        }
		
		///<summary>
		/// Returns a value indicating whether this instance is equal to a specified object using value semantics.
		///</summary>
		///<param name="toObject">An object to compare to this instance.</param>
		///<returns>true if toObject is a <see cref="HolidaysBase"/> and has the same value as this instance; otherwise, false.</returns>
		public virtual bool Equals(HolidaysBase toObject)
		{
			if (toObject == null)
				return false;
			return ValueEquals(this, toObject);
		}
		#endregion
		
		///<summary>
		/// Determines whether the specified <see cref="HolidaysBase"/> instances are considered equal using value semantics.
		///</summary>
		///<param name="Object1">The first <see cref="HolidaysBase"/> to compare.</param>
		///<param name="Object2">The second <see cref="HolidaysBase"/> to compare. </param>
		///<returns>true if Object1 is the same instance as Object2 or if both are null references or if objA.Equals(objB) returns true; otherwise, false.</returns>
		public static bool ValueEquals(HolidaysBase Object1, HolidaysBase Object2)
		{
			// both are null
			if (Object1 == null && Object2 == null)
				return true;

			// one or the other is null, but not both
			if (Object1 == null ^ Object2 == null)
				return false;
				
			bool equal = true;
			if (Object1.Holiday != Object2.Holiday)
				equal = false;
			if ( Object1.Note != null && Object2.Note != null )
			{
				if (Object1.Note != Object2.Note)
					equal = false;
			}
			else if (Object1.Note == null ^ Object2.Note == null )
			{
				equal = false;
			}
					
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
			//return this. GetPropertyName(SourceTable.PrimaryKey.MemberColumns[0]) .CompareTo(((HolidaysBase)obj).GetPropertyName(SourceTable.PrimaryKey.MemberColumns[0]));
		}
		
		/*
		// static method to get a Comparer object
        public static HolidaysComparer GetComparer()
        {
            return new HolidaysComparer();
        }
        */

        // Comparer delegates back to Holidays
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
        public int CompareTo(Holidays rhs, HolidaysColumn which)
        {
            switch (which)
            {
            	
            	
            	case HolidaysColumn.Holiday:
            		return this.Holiday.CompareTo(rhs.Holiday);
            		
            		                 
            	
            	
            	case HolidaysColumn.Note:
            		return this.Note.CompareTo(rhs.Note);
            		
            		                 
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
				
		#region IEntityKey<HolidaysKey> Members
		
		// member variable for the EntityId property
		private HolidaysKey _entityId;

		/// <summary>
		/// Gets or sets the EntityId property.
		/// </summary>
		[XmlIgnore]
        [ScriptIgnore]
		public virtual HolidaysKey EntityId
		{
			get
			{
				if ( _entityId == null )
				{
					_entityId = new HolidaysKey(this);
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
					entityTrackingKey = new System.Text.StringBuilder("Holidays")
					.Append("|").Append( this.Holiday.ToString()).ToString();
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
				"{3}{2}- Holiday: {0}{2}- Note: {1}{2}{4}", 
				this.Holiday,
				(this.Note == null) ? string.Empty : this.Note.ToString(),
				System.Environment.NewLine, 
				this.GetType(),
				this.Error.Length == 0 ? string.Empty : string.Format("- Error: {0}\n",this.Error));
		}
		
		#endregion ToString Method
		
		#region Inner data class
		
	/// <summary>
	///		The data structure representation of the 'Holidays' table.
	/// </summary>
	/// <remarks>
	/// 	This struct is generated by a tool and should never be modified.
	/// </remarks>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Serializable]
	internal protected class HolidaysEntityData : ICloneable, ICloneableEx
	{
		#region Variable Declarations
		private EntityState currentEntityState = EntityState.Added;
		
		#region Primary key(s)
		/// <summary>			
		/// Holiday : Ngay le (Chi luu tru ngay, khong luu tru gio)
		/// </summary>
		/// <remarks>Member of the primary key of the underlying table "Holidays"</remarks>
		public System.DateTime Holiday;
			
		/// <summary>
		/// keep a copy of the original so it can be used for editable primary keys.
		/// </summary>
		public System.DateTime OriginalHoliday;
		
		#endregion
		
		#region Non Primary key(s)
		
		
		/// <summary>
		/// Note : Ghi chu
		/// </summary>
		public System.String		  Note = null;
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
			HolidaysEntityData _tmp = new HolidaysEntityData();
						
			_tmp.Holiday = this.Holiday;
			_tmp.OriginalHoliday = this.OriginalHoliday;
			
			_tmp.Note = this.Note;
			
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
				
			HolidaysEntityData _tmp = new HolidaysEntityData();
						
			_tmp.Holiday = this.Holiday;
			_tmp.OriginalHoliday = this.OriginalHoliday;
			
			_tmp.Note = this.Note;
			
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
		/// <param name="column">The <see cref="HolidaysColumn"/> which has raised the event.</param>
		public virtual void OnColumnChanging(HolidaysColumn column)
		{
			OnColumnChanging(column, null);
			return;
		}
		
		/// <summary>
		/// Raises the <see cref="ColumnChanged" /> event.
		/// </summary>
		/// <param name="column">The <see cref="HolidaysColumn"/> which has raised the event.</param>
		public virtual void OnColumnChanged(HolidaysColumn column)
		{
			OnColumnChanged(column, null);
			return;
		} 
		
		
		/// <summary>
		/// Raises the <see cref="ColumnChanging" /> event.
		/// </summary>
		/// <param name="column">The <see cref="HolidaysColumn"/> which has raised the event.</param>
		/// <param name="value">The changed value.</param>
		public virtual void OnColumnChanging(HolidaysColumn column, object value)
		{
			if(IsEntityTracked && EntityState != EntityState.Added && !EntityManager.TrackChangedEntities)
				EntityManager.StopTracking(entityTrackingKey);
				
			if (!SuppressEntityEvents)
			{
				HolidaysEventHandler handler = ColumnChanging;
				if(handler != null)
				{
					handler(this, new HolidaysEventArgs(column, value));
				}
			}
		}
		
		/// <summary>
		/// Raises the <see cref="ColumnChanged" /> event.
		/// </summary>
		/// <param name="column">The <see cref="HolidaysColumn"/> which has raised the event.</param>
		/// <param name="value">The changed value.</param>
		public virtual void OnColumnChanged(HolidaysColumn column, object value)
		{
			if (!SuppressEntityEvents)
			{
				HolidaysEventHandler handler = ColumnChanged;
				if(handler != null)
				{
					handler(this, new HolidaysEventArgs(column, value));
				}
			
				// warn the parent list that i have changed
				OnEntityChanged();
			}
		} 
		#endregion
			
	} // End Class
	
	
	#region HolidaysEventArgs class
	/// <summary>
	/// Provides data for the ColumnChanging and ColumnChanged events.
	/// </summary>
	/// <remarks>
	/// The ColumnChanging and ColumnChanged events occur when a change is made to the value 
	/// of a property of a <see cref="Holidays"/> object.
	/// </remarks>
	public class HolidaysEventArgs : System.EventArgs
	{
		private HolidaysColumn column;
		private object value;
		
		///<summary>
		/// Initalizes a new Instance of the HolidaysEventArgs class.
		///</summary>
		public HolidaysEventArgs(HolidaysColumn column)
		{
			this.column = column;
		}
		
		///<summary>
		/// Initalizes a new Instance of the HolidaysEventArgs class.
		///</summary>
		public HolidaysEventArgs(HolidaysColumn column, object value)
		{
			this.column = column;
			this.value = value;
		}
		
		///<summary>
		/// The HolidaysColumn that was modified, which has raised the event.
		///</summary>
		///<value cref="HolidaysColumn" />
		public HolidaysColumn Column { get { return this.column; } }
		
		/// <summary>
        /// Gets the current value of the column.
        /// </summary>
        /// <value>The current value of the column.</value>
		public object Value{ get { return this.value; } }

	}
	#endregion
	
	///<summary>
	/// Define a delegate for all Holidays related events.
	///</summary>
	public delegate void HolidaysEventHandler(object sender, HolidaysEventArgs e);
	
	#region HolidaysComparer
		
	/// <summary>
	///	Strongly Typed IComparer
	/// </summary>
	public class HolidaysComparer : System.Collections.Generic.IComparer<Holidays>
	{
		HolidaysColumn whichComparison;
		
		/// <summary>
        /// Initializes a new instance of the <see cref="T:HolidaysComparer"/> class.
        /// </summary>
		public HolidaysComparer()
        {            
        }               
        
        /// <summary>
        /// Initializes a new instance of the <see cref="T:HolidaysComparer"/> class.
        /// </summary>
        /// <param name="column">The column to sort on.</param>
        public HolidaysComparer(HolidaysColumn column)
        {
            this.whichComparison = column;
        }

		/// <summary>
        /// Determines whether the specified <c cref="Holidays"/> instances are considered equal.
        /// </summary>
        /// <param name="a">The first <c cref="Holidays"/> to compare.</param>
        /// <param name="b">The second <c>Holidays</c> to compare.</param>
        /// <returns>true if objA is the same instance as objB or if both are null references or if objA.Equals(objB) returns true; otherwise, false.</returns>
        public bool Equals(Holidays a, Holidays b)
        {
            return this.Compare(a, b) == 0;
        }

		/// <summary>
        /// Gets the hash code of the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public int GetHashCode(Holidays entity)
        {
            return entity.GetHashCode();
        }

        /// <summary>
        /// Performs a case-insensitive comparison of two objects of the same type and returns a value indicating whether one is less than, equal to, or greater than the other.
        /// </summary>
        /// <param name="a">The first object to compare.</param>
        /// <param name="b">The second object to compare.</param>
        /// <returns></returns>
        public int Compare(Holidays a, Holidays b)
        {
        	EntityPropertyComparer entityPropertyComparer = new EntityPropertyComparer(this.whichComparison.ToString());
        	return entityPropertyComparer.Compare(a, b);
        }

		/// <summary>
        /// Gets or sets the column that will be used for comparison.
        /// </summary>
        /// <value>The comparison column.</value>
        public HolidaysColumn WhichComparison
        {
            get { return this.whichComparison; }
            set { this.whichComparison = value; }
        }
	}
	
	#endregion
	
	#region HolidaysKey Class

	/// <summary>
	/// Wraps the unique identifier values for the <see cref="Holidays"/> object.
	/// </summary>
	[Serializable]
	[CLSCompliant(true)]
	public class HolidaysKey : EntityKeyBase
	{
		#region Constructors
		
		/// <summary>
		/// Initializes a new instance of the HolidaysKey class.
		/// </summary>
		public HolidaysKey()
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the HolidaysKey class.
		/// </summary>
		public HolidaysKey(HolidaysBase entity)
		{
			this.Entity = entity;

			#region Init Properties

			if ( entity != null )
			{
				this.Holiday = entity.Holiday;
			}

			#endregion
		}
		
		/// <summary>
		/// Initializes a new instance of the HolidaysKey class.
		/// </summary>
		public HolidaysKey(System.DateTime _holiday)
		{
			#region Init Properties

			this.Holiday = _holiday;

			#endregion
		}
		
		#endregion Constructors

		#region Properties
		
		// member variable for the Entity property
		private HolidaysBase _entity;
		
		/// <summary>
		/// Gets or sets the Entity property.
		/// </summary>
		public HolidaysBase Entity
		{
			get { return _entity; }
			set { _entity = value; }
		}
		
		// member variable for the Holiday property
		private System.DateTime _holiday;
		
		/// <summary>
		/// Gets or sets the Holiday property.
		/// </summary>
		public System.DateTime Holiday
		{
			get { return _holiday; }
			set
			{
				if ( this.Entity != null )
					this.Entity.Holiday = value;
				
				_holiday = value;
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
				Holiday = ( values["Holiday"] != null ) ? (System.DateTime) EntityUtil.ChangeType(values["Holiday"], typeof(System.DateTime)) : DateTime.MinValue;
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

			values.Add("Holiday", Holiday);

			#endregion Init Dictionary

			return values;
		}
		
		///<summary>
		/// Returns a String that represents the current object.
		///</summary>
		public override string ToString()
		{
			return String.Format("Holiday: {0}{1}",
								Holiday,
								System.Environment.NewLine);
		}

		#endregion Methods
	}
	
	#endregion	

	#region HolidaysColumn Enum
	
	/// <summary>
	/// Enumerate the Holidays columns.
	/// </summary>
	[Serializable]
	public enum HolidaysColumn : int
	{
		/// <summary>
		/// Holiday : Ngay le (Chi luu tru ngay, khong luu tru gio)
		/// </summary>
		[EnumTextValue("Holiday")]
		[ColumnEnum("Holiday", typeof(System.DateTime), System.Data.DbType.DateTime, true, false, false)]
		Holiday = 1,
		/// <summary>
		/// Note : Ghi chu
		/// </summary>
		[EnumTextValue("Note")]
		[ColumnEnum("Note", typeof(System.String), System.Data.DbType.String, false, false, true, 255)]
		Note = 2
	}//End enum

	#endregion HolidaysColumn Enum

} // end namespace