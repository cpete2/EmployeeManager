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
    sealed class Hourly : Employee
    {
        /// <summary>
        /// Purpose: Hourly Rate property. To provide the hourly rate of employee. Read/Write property.
        /// </summary>
        public override decimal HourlyRate { get; set; }
        /// <summary>
        /// Purpose: Hours Worked @this. To provide hours worked. Read/Write property.
        /// </summary>
        public override double HoursWorked { get; set; }
        /// <summary>
        /// Purpose: This is the Overtime property.
        /// </summary>
        public override decimal Overtime { get; set; }
        /// <summary>
        /// Purpose: This is the EducationalBenefits property.
        /// </summary>
        public override decimal EducationalBenefits { get; set; }
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Hourly() : base()
        {
            HourlyRate = 0.0M;
            HoursWorked = 0.0;
            EmpType = ETYPE.HRLY;
        }
        /// <summary>
        /// Purpose: Parameterized Constructor - With all string parameters
        /// </summary>
        /// <param name="eid">string Employee ID</param>
        /// <param name="name">string Employee Full Name</param>
        /// <param name="hrate">string Employee Hourly Rate</param>
        /// <param name="hwk">string Employee Hours Worked</param>
        public Hourly(string eid, string nameF, string nameL, string middleInt, string marital, string phone, string department, string tit, string startDate, string hrate, string hwk)
            : base(eid, nameF, nameL,middleInt,marital,phone,department,tit,startDate)
        {
            //Check if data is okay using Regex
            rgx = new Regex(RGX_NUM_DECIMAL);
            check = rgx.Match(hrate.ToString().Trim());
            if (!check.Success)
            {
                throw new Exception("Invalid value for hourly rate!");
            }
            rgx = new Regex(RGX_NUM_DECIMAL);
            check = rgx.Match(hwk.Trim());
            if (!check.Success)
            {
                throw new Exception("Invalid value for hours worked!");
            }
            HourlyRate = decimal.Parse(hrate);
            HoursWorked = double.Parse(hwk);
            EmpType = ETYPE.HRLY;
        }
    }//End of Hourly Class

}
