// Project Prolog
// Name: Cory J Petersen
// CS 3260 Section 001
// Project: CS3260_Lab_08
// Date: 06/22/2015 12:44:44 PM
// 
// I declare that the following code was written by me or provided 
// by the instructor for this project. I understand that copying source
// code from any other source constitutes cheating, and that I will receive
// a zero on this project if I am found in violation of this policy.
// ---------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Employee
{
    sealed class BusinessRules : IBusinessRules
    {
        //Local variables
        private static readonly BusinessRules instance = new BusinessRules();
        private static BusinessRules _instance = null;
        public const string RGX_NUM_EMPID = @"^\d+$+";
        public const string RGX_NAME = @"[a-zA-z]+";
        public Regex rgx;
        public Match check;
        public SortedDictionary<uint, Employee> employees;
        /// <summary>
        /// Pupose: Instance method to return the intance of class to make class Singleton.
        /// Preconditions: None
        /// Postconditions: None
        /// Paramaters: None
        /// Returns: intance of the BuisnessRules class object.
        /// </summary>
        public static BusinessRules INSTANCE
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BusinessRules();
                }
                return _instance;
            }
        }
        /// <summary>
        /// Purpose: Indexer for the Buisiness rules class.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Employee this[string key]
        {
            get
            {
                rgx = new Regex(RGX_NUM_EMPID);
                check = rgx.Match(key.Trim());
                if (!check.Success)
                {
                    throw new Exception("Invalid employee id!");
                }
                uint id = Convert.ToUInt32(key);
                return employees[id];
            }
            set
            {
                rgx = new Regex(RGX_NUM_EMPID);
                check = rgx.Match(key.Trim());
                if (!check.Success)
                {
                    throw new Exception("Invalid employee id!");
                }
                uint id = Convert.ToUInt32(key);
                employees[id] = value;
            }
        }
        /// <summary>
        /// Purpose: BuisnessRules class default constructor.  Initialize variables.
        /// </summary>
        private BusinessRules()
        {
            employees = new SortedDictionary<uint, Employee>();
        }
        /// <summary>
        /// Purpose: This function finds all employees in the employee sorted dictionary that have the same last name as the parameter passed in and returns them in a list.
        /// </summary>
        /// <param name="nameL"></param>
        /// <returns></returns>
        public List<Employee> FindEmpByLastName(string nameL)
        {
            List<Employee> empList = new List<Employee>();
            rgx = new Regex(RGX_NAME);
            check = rgx.Match(nameL.Trim());
            if (!check.Success)
            {
                throw new Exception("Invalid employee last name!");
            }
            SortedDictionary<uint, Employee>.ValueCollection val = employees.Values;
            foreach(Employee emp in val)
            {
                if(emp.EmpNameLast.ToUpper() == nameL.ToUpper())
                {
                    empList.Add(emp);
                }
            }
            if (empList.Count == 0)
            {
                throw new Exception("No employees found with that last name!");
            }
            return empList;
        }
    }//End of BusinessRules Class

}
