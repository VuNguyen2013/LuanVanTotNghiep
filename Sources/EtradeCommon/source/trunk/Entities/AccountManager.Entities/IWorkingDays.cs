﻿using System;
using System.ComponentModel;

namespace AccountManager.Entities
{
	/// <summary>
	///		The data structure representation of the 'WorkingDays' table via interface.
	/// </summary>
	/// <remarks>
	/// 	This struct is generated by a tool and should never be modified.
	/// </remarks>
	public interface IWorkingDays 
	{
		/// <summary>			
		/// DateId : Id cua ngay(2=Thu 2, 3 = Thu 3, ..., 8=Chu nhat)
		/// </summary>
		/// <remarks>Member of the primary key of the underlying table "WorkingDays"</remarks>
		System.Int32 DateId { get; set; }
				
		/// <summary>
		/// keep a copy of the original so it can be used for editable primary keys.
		/// </summary>
		System.Int32 OriginalDateId { get; set; }
			
		
		
		/// <summary>
		/// IsWorkingDay : true neu la working day, nguoc lai false
		/// </summary>
		System.Boolean  IsWorkingDay  { get; set; }
			
		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>A new object that is a copy of this instance.</returns>
		System.Object Clone();
		
		#region Data Properties

		#endregion Data Properties

	}
}

