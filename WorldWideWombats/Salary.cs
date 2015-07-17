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
    class Salary : Employee
    {
        /// <summary>
        /// Purpose: Monthly Salary property to get and set a monthly salary value.
        /// </summary>
        public override decimal MonthlySalary { get; set; }
        /// <summary>
        /// Purpose: This is the Benefits property.
        /// </summary>
        public override decimal Benefits { get; set; }
        /// <summary>
        /// Purpose: This is the EducationalBenefits property.
        /// </summary>
        public override decimal EducationalBenefits { get; set; }
        /// <summary>
        /// Purpose: Default Constructor
        /// </summary>
        public Salary() : base()
        {
            MonthlySalary = 0.0M;
            EmpType = ETYPE.SAL;
        }
        /// <summary>
        /// Purpose: Parameterized Constructor - With all string parameters
        /// </summary>
        /// <param name="eid">string Employee ID</param>
        /// <param name="name">string Employee Full Name</param>
        /// <param name="sal">string Salary in $</param>
        public Salary(string eid, string nameF, string nameL, string middleInt, string marital, string phone, string department, string tit, string startDate, string sal)
            : base(eid, nameF,nameL,middleInt,marital,phone,department,tit,startDate)
        {
            //Check if data is okay using Regex
            rgx = new Regex(RGX_NUM_DECIMAL);
            check = rgx.Match(sal.ToString().Trim());
            if (!check.Success)
            {
                throw new Exception("Invalid value for monthly salary!");
            }
            MonthlySalary = decimal.Parse(sal);
            EmpType = ETYPE.SAL;
        }
    }//End of Salary Class

}
