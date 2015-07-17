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
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Employee
{
    [Serializable]
    public class Course
    {
        //Local Variables
        public const string RGX_NAME = @"[a-zA-z]+";
        public const string RGX_COURSE_ID = @"^[\S]+$";
        public const string RGX_NUM_EMPID = @"^\d+$+";
        public const string RGX_NUM_DECIMAL = @"^[0-9]{1,3}(,[0-9]{3})*(([\\.,]{1}[0-9]*)|())$";
        public const string RGX_LET_GRADE = @"^A-?|[BCD][+-]?|[SN]?F|W$";
        public Regex rgx;
        public Match check;
        /// <summary>
        /// Purpose: Property for the course ID. 
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// Purpose: Property for the course description.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Purpose: Property for the course grade.
        /// </summary>
        public string  Grade { get; set; }
        /// <summary>
        /// Purpose: Property for the Aproval date. 
        /// </summary>
        public DateTime ApprovalDate { get; set; }
        /// <summary>
        /// Purpose:Property for the Credit Hours.
        /// </summary>
        public int CreditHours { get; set; }
        /// <summary>
        /// Purpose: Default Constructor.
        /// </summary>
        public Course()
        {
            ID = null;
            Description = null;
            Grade = null;
            ApprovalDate = new DateTime(1900,01,01);
        }
        /// <summary>
        /// Purpose: This is the paramterized constructor.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="desc"></param>
        /// <param name="grd"></param>
        /// <param name="ad"></param>
        public Course(string id, string desc, string grd, string ad,string ch)
        {
            //Check input before creation.
            //Check if data is okay using Regex
            rgx = new Regex(RGX_COURSE_ID);
            check = rgx.Match(id.ToString().Trim());
            if (!check.Success)
            {
                throw new Exception("Invalid course ID!\nCheck that ID doesn't contain a blank space.");
            }
            rgx = new Regex(RGX_NAME);
            check = rgx.Match(desc.Trim());
            if (!check.Success)
            {
                throw new Exception("Invalid course description!");
            }
            //No need to check for date time because there is always a date value, never comes in null.
            rgx = new Regex(RGX_LET_GRADE);
            check = rgx.Match(grd.Trim());
            if (!check.Success)
            {
                throw new Exception("Invalid letter grade selected!");
            }
            rgx = new Regex(RGX_NUM_DECIMAL);
            check = rgx.Match(ch.Trim());
            if (!check.Success)
            {
                throw new Exception("Invalid credit hourse selected!");
            }
            ID = id;
            Description = desc;
            Grade = grd;
            ApprovalDate = Convert.ToDateTime(ad);
            CreditHours = Convert.ToInt32(ch);
        }
    }
}
