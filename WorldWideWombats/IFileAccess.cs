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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee
{
    interface IFileAccess
    {
        /// <summary>
        /// Purpose: To serialize and write out sorted dictionary of Employees.
        /// </summary>
        void WriteDB();
        /// <summary>
        /// Purpose: To Deserialize and Read Employees into sorted dictionary of employees.
        /// </summary>
        void ReadDB();
        /// <summary>
        /// Prupose: To open a serialized file of empmloyees for reading.
        /// </summary>
        void OpenDB();
        /// <summary>
        /// Purpose: To close a file of employees after writing. 
        /// </summary>
        void CloseDB();
        SortedDictionary<uint, Employee> DB { get; set; }
    }
}
