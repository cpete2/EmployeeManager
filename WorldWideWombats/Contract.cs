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
using System.Text.RegularExpressions;

namespace Employee
{
    [Serializable]
    sealed class Contract : Employee
    {
        /// <summary>
        /// Purpose: Contract Salary propert to get the salary of a contracted employee.
        /// </summary>
        public override decimal ContractSalary { get; set; }
        /// <summary>
        /// Purpose: Default Constructor
        /// </summary>
        public Contract()
        {
            ContractSalary = 0.0M;
            EmpType = ETYPE.CNCT;
        }
        /// <summary>
        /// Purpose: Parameterized Constructor - With all string parameters
        /// </summary>
        /// <param name="eid">string Employee ID</param>
        /// <param name="name">string Employee Full Name</param>
        /// <param name="csal">string Constract Salary in $</param>
        public Contract(string eid, string nameF,string nameL, string middleInt, string marital, string phone, string department, string tit, string startDate, string csal)
            : base(eid, nameF,nameL,middleInt,marital,phone,department,tit,startDate)
        {
            //Check if data is okay using Regex
            rgx = new Regex(RGX_NUM_DECIMAL);
            check = rgx.Match(csal.ToString().Trim());
            if (!check.Success)
            {
                throw new Exception("Invalid value for contract salary!");
            }
            ContractSalary = decimal.Parse(csal);
            EmpType = ETYPE.CNCT;
        }
    }//End of Contract Class

}
