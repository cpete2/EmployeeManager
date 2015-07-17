// Project Prolog
// Name: Cory J Petersen
// CS 3260 Section 001
// Project: CS3260_Lab_12
// Date: 06/22/2015 12:44:44 PM
// 
// I declare that the following code was written by me or provided 
// by the instructor for this project. I understand that copying source
// code from any other source constitutes cheating, and that I will receive
// a zero on this project if I am found in violation of this policy. GIT TEST
// ---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Employee
{
    /// <summary>
    /// Employee Abstract, this is the base class.
    /// </summary>
    [Serializable]
    public abstract class Employee: object
    {           
        //Local Variables
        public const uint EMPLOYEENUMBER = 1000;
        public const string RGX_NAME = @"[a-zA-z]+";
        public const string RGX_MIDDLE_INIT = @"^[A-Z]{1}";
        public const string RGX_PHONE_NUM = @"^[0-9]{10}";
        public const string RGX_NUM_EMPID = @"^\d+$+";
        public const string RGX_NUM_DECIMAL = @"^([0-9]{1,3}(,[0-9]{3})*(([\\.,]{1}[0-9]*)|())$)|[0-9]+";
        public Regex rgx;
        public Match check;
        public SortedDictionary<string, Course> courses;
        /// <summary>
        /// Purpose: @this for the employee ID.
        /// </summary>
        public uint EmpID { get; set; }
        /// <summary>
        /// Purpose: This is an unum type for the Employee Type.
        /// </summary>
        public ETYPE EmpType { get; set; }
        /// <summary>
        /// Purpose: @this for the first name of an employee
        /// </summary>
        public string EmpNameFirst { get; set; }
        /// <summary>
        /// Purpose: @this for the last name of an employee
        /// </summary>
        public string EmpNameLast { get; set; }
        /// <summary>
        /// Purpose: MiddleInitial property.
        /// </summary>
        public string MiddleInitial { get; set; }
        /// <summary>
        /// Purpse: MaritalStatus Property
        /// </summary>
        public string MaritalStatus { get; set; }
        /// <summary>
        /// Purpose: Phone Number property
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Purpose: Department Property
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// Prupse: Title Property
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Purpose: StartDate Property
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// Default Constructor.
        /// </summary>
        public Employee() : base()
        {
            EmpID = EMPLOYEENUMBER-1;
            EmpType = ETYPE.NONE;
            EmpNameFirst = "none";
            EmpNameLast = "none";
            //----------------
            MiddleInitial = "none";
            MaritalStatus = null;
            PhoneNumber = "none";
            Department = "none";
            Title = "none";
            StartDate = new DateTime(1900, 01, 01);
        }
        /// <summary>
        /// Paramaterized Constructor.
        /// </summary>
        /// <param name="empId"></param>
        /// <param name="empname"></param>
        public Employee(uint empId, string empnameF, string empnameL, string middleInt, string marital, string phone, string department, string tit, string startDate)
        {
            //Check if data is okay using Regex
            rgx = new Regex(RGX_NUM_EMPID);
            check = rgx.Match(empId.ToString().Trim());
            if (!check.Success)
            {
                throw new Exception("Invalid Employee ID!");
            }
            rgx = new Regex(RGX_NAME);
            check = rgx.Match(empnameF.Trim());
            if (!check.Success)
            {
                throw new Exception("Invalid Employee First Name!");
            }
            check = rgx.Match(empnameL.Trim());
            if (!check.Success)
            {
                throw new Exception("Invalid Employee Last Name!");
            }
            rgx = new Regex(RGX_MIDDLE_INIT);
            check = rgx.Match(middleInt.Trim());
            if (!check.Success)
            {
                throw new Exception("Invalid Middle Initial!");
            }
            rgx = new Regex(RGX_NAME);
            check = rgx.Match(marital.Trim());
            if (!check.Success)
            {
                throw new Exception("Invalid Marital Status!");
            }
            rgx = new Regex(RGX_PHONE_NUM);
            check = rgx.Match(phone.Trim());
            if (!check.Success)
            {
                throw new Exception("Invalid Phone Number!");
            }
            rgx = new Regex(RGX_NAME);
            check = rgx.Match(department.Trim());
            if (!check.Success)
            {
                throw new Exception("Invalid Department!");
            }
            rgx = new Regex(RGX_NAME);
            check = rgx.Match(tit.Trim());
            if (!check.Success)
            {
                throw new Exception("Invalid Title!");
            }
            EmpID = empId;
            EmpNameFirst = empnameF;
            EmpNameLast = empnameL;
            EmpType = ETYPE.NONE;
            MiddleInitial = middleInt;
            MaritalStatus = marital;
            PhoneNumber = phone;
            Department = department;
            Title = tit;
            StartDate = Convert.ToDateTime(startDate);
        }
        /// <summary>
        /// Purpose: Hours Worked @this. To provide hours worked. Read/Write property.
        /// </summary>
        virtual public double HoursWorked { get; set; }
        /// <summary>
        /// Purpose: Hourly Rate property. To provide the hourly rate of employee. Read/Write property.
        /// </summary>
        virtual public decimal HourlyRate { get; set; }
        /// <summary>
        /// Purpose: Monthly Salary property to get and set a monthly salary value.
        /// </summary>
        virtual public decimal MonthlySalary { get; set; }
        /// <summary>
        /// Porperty: This is commision on sales.
        /// </summary>
        virtual public double ComSales { get; set; }
        /// <summary>
        /// Purpose: This is the gross sales.
        /// </summary>
        virtual public decimal GrossSales { get; set; }
        /// <summary>
        /// Purpose: Contract Salary propert to get the salary of a contracted employee.
        /// </summary>
        virtual public decimal ContractSalary { get; set; }
        /// <summary>
        /// Purpose: This is an atribute for Hourly employee. 
        /// </summary>
        virtual public decimal Overtime { get; set; }
        /// <summary>
        /// Purpose: This is an atribute for Salary and Sales empoloyees.
        /// </summary>
        virtual public decimal Benefits { get; set; }
        /// <summary>
        /// Purpose: This is an atribute for Sales employees.
        /// </summary>
        virtual public decimal Commission { get; set; }
        /// <summary>
        /// Purpose: This is an atribute for all employees except contract employees.
        /// </summary>
        virtual public decimal EducationalBenefits { get; set; }
        /// <summary>
        /// Purpose: Parameterized Constructor - With all string parameters
        /// </summary>
        /// <param name="eid"></param>
        /// <param name="ename"></param>
        public Employee(string eid, string empNameF, string empNameL, string middleInt, string marital, string phone, string department, string tit, string startDate)
            : base()
        {
            //Check if data is okay using Regex
            rgx = new Regex(RGX_NUM_EMPID);
            check = rgx.Match(eid.ToString().Trim());
            if (!check.Success)
            {
                throw new Exception("Invalid Employee ID!");
            }
            rgx = new Regex(RGX_NAME);
            check = rgx.Match(empNameF.Trim());
            if (!check.Success)
            {
                throw new Exception("Invalid Employee First Name!");
            }
            check = rgx.Match(empNameL.Trim());
            if (!check.Success)
            {
                throw new Exception("Invalid Employee Last Name!");
            }
            rgx = new Regex(RGX_MIDDLE_INIT);
            check = rgx.Match(middleInt.Trim());
            if (!check.Success)
            {
                throw new Exception("Invalid Middle Initial!");
            }
            rgx = new Regex(RGX_NAME);
            check = rgx.Match(marital.Trim());
            if (!check.Success)
            {
                throw new Exception("Invalid Marital Status!");
            }
            rgx = new Regex(RGX_PHONE_NUM);
            check = rgx.Match(phone.Trim());
            if (!check.Success)
            {
                throw new Exception("Invalid Phone Number!");
            }
            rgx = new Regex(RGX_NAME);
            check = rgx.Match(department.Trim());
            if (!check.Success)
            {
                throw new Exception("Invalid Department!");
            }
            rgx = new Regex(RGX_NAME);
            check = rgx.Match(tit.Trim());
            if (!check.Success)
            {
                throw new Exception("Invalid Title!");
            }
            EmpID = uint.Parse(eid);
            EmpNameFirst = empNameF;
            EmpNameLast = empNameL;
            EmpType = ETYPE.NONE;
            courses = new SortedDictionary<string, Course>();
            MiddleInitial = middleInt;
            MaritalStatus = marital;
            PhoneNumber = phone;
            Department = department;
            Title = tit;
            StartDate = Convert.ToDateTime(startDate);
        }
    }//End of Employee class
}//End of Employee namespace
