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
    sealed class Sales : Salary
    {
        /// <summary>
        /// Porperty: This is commision on sales.
        /// </summary>
        public override double ComSales { get; set; }
        /// <summary>
        /// Purpose: This is the gross sales.
        /// </summary>
        public override decimal GrossSales { get; set; }
        /// <summary>
        /// Purpose: This is the Benefits property.
        /// </summary>
        public override decimal Benefits { get; set; }
        /// <summary>
        /// Purpose: This is the Comission property.
        /// </summary>
        public override decimal Commission { get; set; }
        /// <summary>
        /// Purpose: Default Constructor
        /// </summary>
        public Sales() : base()
        {
            ComSales = 0.0;
            GrossSales = 0.0M;
            EmpType = ETYPE.SLS;
        }
        /// <summary>
        /// Purpose: Parameterized Constructor - With all string parameters
        /// </summary>
        /// <param name="eid">string Employee ID</param>
        /// <param name="name">string Employee Full Name</param>
        /// <param name="sal">string Salary monthly salary in $</param>
        /// <param name="comsal">string Sales commission on gross sales in %</param>
        /// <param name="gsal">string Sales gross sales in $</param>
        public Sales(string empid, string empnameF,string empnameL, string middleInt, string marital, string phone, string department, string tit, string startDate,string sal, string comsal, string gsal)
            : base(empid, empnameF,empnameL, middleInt, marital, phone, department, tit, startDate,sal)
        {
            //Check if data is okay using Regex
            rgx = new Regex(RGX_NUM_DECIMAL);
            check = rgx.Match(comsal.ToString().Trim());
            if (!check.Success)
            {
                throw new Exception("Invalid value for sales commision rate!");
            }
            rgx = new Regex(RGX_NUM_DECIMAL);
            check = rgx.Match(gsal.Trim());
            if (!check.Success)
            {
                throw new Exception("Invalid value for gross sales!");
            }
            ComSales = double.Parse(comsal);
            GrossSales = decimal.Parse(gsal);
            EmpType = ETYPE.SLS;
        }

    }//End of Sales Class

}
