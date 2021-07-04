﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SchoolERPSYSTEM
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="SchoolErpSystem")]
	public partial class LinqDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public LinqDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["SchoolErpSystem"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public LinqDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public LinqDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public LinqDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public LinqDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.updateSubject")]
		public int updateSubject([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> id, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Subject", DbType="VarChar(50)")] string subject, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Class", DbType="VarChar(50)")] string @class)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), id, subject, @class);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.CrudSubject")]
		public ISingleResult<CrudSubjectResult> CrudSubject([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> id, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Subject", DbType="VarChar(50)")] string subject, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Class", DbType="VarChar(50)")] string @class, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="CrudOption", DbType="VarChar(50)")] string crudOption)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), id, subject, @class, crudOption);
			return ((ISingleResult<CrudSubjectResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.crudemp")]
		public ISingleResult<crudempResult> crudemp([global::System.Data.Linq.Mapping.ParameterAttribute(DbType="Int")] System.Nullable<int> id, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Empname", DbType="NVarChar(150)")] string empname, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Email", DbType="NVarChar(150)")] string email, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Salary", DbType="VarChar(50)")] string salary, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="CrudOption", DbType="VarChar(50)")] string crudOption)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), id, empname, email, salary, crudOption);
			return ((ISingleResult<crudempResult>)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.getStaffwithID")]
		public ISingleResult<getStaffwithIDResult> getStaffwithID([global::System.Data.Linq.Mapping.ParameterAttribute(Name="ID", DbType="Int")] System.Nullable<int> iD)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), iD);
			return ((ISingleResult<getStaffwithIDResult>)(result.ReturnValue));
		}
	}
	
	public partial class CrudSubjectResult
	{
		
		private int _SubjectID;
		
		private string _Subject;
		
		private string _Classes;
		
		public CrudSubjectResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SubjectID", DbType="Int NOT NULL")]
		public int SubjectID
		{
			get
			{
				return this._SubjectID;
			}
			set
			{
				if ((this._SubjectID != value))
				{
					this._SubjectID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Subject", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Subject
		{
			get
			{
				return this._Subject;
			}
			set
			{
				if ((this._Subject != value))
				{
					this._Subject = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Classes", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Classes
		{
			get
			{
				return this._Classes;
			}
			set
			{
				if ((this._Classes != value))
				{
					this._Classes = value;
				}
			}
		}
	}
	
	public partial class crudempResult
	{
		
		private int _Empid;
		
		private string _Empname;
		
		private string _Email;
		
		private string _Salary;
		
		public crudempResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Empid", DbType="Int NOT NULL")]
		public int Empid
		{
			get
			{
				return this._Empid;
			}
			set
			{
				if ((this._Empid != value))
				{
					this._Empid = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Empname", DbType="VarChar(150)")]
		public string Empname
		{
			get
			{
				return this._Empname;
			}
			set
			{
				if ((this._Empname != value))
				{
					this._Empname = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Email", DbType="VarChar(50)")]
		public string Email
		{
			get
			{
				return this._Email;
			}
			set
			{
				if ((this._Email != value))
				{
					this._Email = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Salary", DbType="NVarChar(20)")]
		public string Salary
		{
			get
			{
				return this._Salary;
			}
			set
			{
				if ((this._Salary != value))
				{
					this._Salary = value;
				}
			}
		}
	}
	
	public partial class getStaffwithIDResult
	{
		
		private int _StaffID;
		
		private int _Designation;
		
		private int _StaffType;
		
		private string _Firstname;
		
		private string _Lastname;
		
		private string _Gender;
		
		private decimal _Phone1;
		
		private System.Nullable<decimal> _Phone2;
		
		private string _Email;
		
		private System.DateTime _DateOfAppointment;
		
		private string _Nationality;
		
		private string _Address;
		
		private string _HighestQualification;
		
		private int _YearOfExperience;
		
		private string _PreviouseOrganization;
		
		private string _Image;
		
		public getStaffwithIDResult()
		{
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StaffID", DbType="Int NOT NULL")]
		public int StaffID
		{
			get
			{
				return this._StaffID;
			}
			set
			{
				if ((this._StaffID != value))
				{
					this._StaffID = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Designation", DbType="Int NOT NULL")]
		public int Designation
		{
			get
			{
				return this._Designation;
			}
			set
			{
				if ((this._Designation != value))
				{
					this._Designation = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_StaffType", DbType="Int NOT NULL")]
		public int StaffType
		{
			get
			{
				return this._StaffType;
			}
			set
			{
				if ((this._StaffType != value))
				{
					this._StaffType = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Firstname", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Firstname
		{
			get
			{
				return this._Firstname;
			}
			set
			{
				if ((this._Firstname != value))
				{
					this._Firstname = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Lastname", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Lastname
		{
			get
			{
				return this._Lastname;
			}
			set
			{
				if ((this._Lastname != value))
				{
					this._Lastname = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Gender", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Gender
		{
			get
			{
				return this._Gender;
			}
			set
			{
				if ((this._Gender != value))
				{
					this._Gender = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Phone1", DbType="Decimal(12,0) NOT NULL")]
		public decimal Phone1
		{
			get
			{
				return this._Phone1;
			}
			set
			{
				if ((this._Phone1 != value))
				{
					this._Phone1 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Phone2", DbType="Decimal(12,0)")]
		public System.Nullable<decimal> Phone2
		{
			get
			{
				return this._Phone2;
			}
			set
			{
				if ((this._Phone2 != value))
				{
					this._Phone2 = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Email", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Email
		{
			get
			{
				return this._Email;
			}
			set
			{
				if ((this._Email != value))
				{
					this._Email = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DateOfAppointment", DbType="Date NOT NULL")]
		public System.DateTime DateOfAppointment
		{
			get
			{
				return this._DateOfAppointment;
			}
			set
			{
				if ((this._DateOfAppointment != value))
				{
					this._DateOfAppointment = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Nationality", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Nationality
		{
			get
			{
				return this._Nationality;
			}
			set
			{
				if ((this._Nationality != value))
				{
					this._Nationality = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Address", DbType="VarChar(MAX) NOT NULL", CanBeNull=false)]
		public string Address
		{
			get
			{
				return this._Address;
			}
			set
			{
				if ((this._Address != value))
				{
					this._Address = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_HighestQualification", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string HighestQualification
		{
			get
			{
				return this._HighestQualification;
			}
			set
			{
				if ((this._HighestQualification != value))
				{
					this._HighestQualification = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_YearOfExperience", DbType="Int NOT NULL")]
		public int YearOfExperience
		{
			get
			{
				return this._YearOfExperience;
			}
			set
			{
				if ((this._YearOfExperience != value))
				{
					this._YearOfExperience = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PreviouseOrganization", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string PreviouseOrganization
		{
			get
			{
				return this._PreviouseOrganization;
			}
			set
			{
				if ((this._PreviouseOrganization != value))
				{
					this._PreviouseOrganization = value;
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Image", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string Image
		{
			get
			{
				return this._Image;
			}
			set
			{
				if ((this._Image != value))
				{
					this._Image = value;
				}
			}
		}
	}
}
#pragma warning restore 1591