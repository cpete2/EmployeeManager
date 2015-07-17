// Project Prolog
// Name: Cory J Petersen
// CS 3260 Section 001
// Project: CS3260_Lab_12
// Date: 06/17/2015 12:44:44 PM
// 
// I declare that the following code was written by me or provided 
// by the instructor for this project. I understand that copying source
// code from any other source constitutes cheating, and that I will receive
// a zero on this project if I am found in violation of this policy.
// ---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Employee
{
    public enum ETYPE { SAL=1, HRLY, SLS, CNCT, NONE}

    /// <summary>
    /// This is the MnWindow class for the Form Displayed
    /// </summary>
    public partial class FrmWindow : Form
    {
        //Private local constant variables to check whats been selected in the Combo box.
        private const string HRLY_EMP = "Hourly Employee";
        private const string SRLY_EMP = "Salary Employee";
        private const string CONT_EMP = "Contract Employee";
        private const string SALE_EMP = "Sales Employee";
        private const string SEARCH_LAST_NAME = "Last Name";
        private const string SEARCH_EMP_ID = "Employee ID";
        private const string ER_MSG_NAME_INVD_DATA="Invalid Data";
        private const string ER_MSG_NAME_ERR = "ERROR OCCURED";
        private const string ER_MSG_NAME_NOTFOUND = "Not Found";
        private const string ER_MSG_NAME_INDEX_OUTOFBOUNDS = "Duplicate Key";
        private const string ER_MSG_NAME_INVALIDDB = "Invalid Database";
        private const string ER_MSG_EMPNAME="Invalid Employee Name";
        private const string ER_MSG_EMPID="Invalid Employee ID";
        private const string ER_MSG_EMPHRLYWAGE="Invalid Hourly Wage";
        private const string ER_MSG_EMPHRSWORKED="Invalid Hours Worked";
        private const string ER_MSG_EMPSALARY="Invalid Salary";
        private const string ER_MSG_EMPGROS = "Invalid Gross Sales";
        private const string ER_MSG_EMPCOMISION = "Invalid commision of sales";
        private const string ER_MSG_EMPTYPE = "Please Select an employee type!";
        private const string ER_MSG_INDEX_OUTOFBOUNDS = "This Employee ID already exsists";
        private const string ER_MSG_EMPID_NOEXSIST = "Employee ID does not exsis.";
        private const string ER_MSG_INVALIDDB = "The database you are trying to open is not an employee database. Please select an employee database.";
        private const int MAX = 4;
        private uint empID_holder;
        private BusinessRules _rules;
        private Employee _obj_holder;
        private Employee _currentViewedEmp;
        private Course _course_holder;
        private Course _currentViewedCourse;
        private FileIO _file;
        private int _count;
        private uint _ptestUint;
        private decimal _ptestDec;
        private double _ptestDouble;
        private int _testDataCount;
        private List<string> _searchList;
        private List<string> _searchListEI;
        /// <summary>
        /// Purpose: Constructor to intialize the FrmMain and Controls
        /// </summary>
        public FrmWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Purpose: Values to be loaded on window load.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MnWindow_Load_1(object sender, EventArgs e)
        {
            CBxEmpType.Items.Add("Hourly Employee");
            CBxEmpType.Items.Add("Salary Employee");
            CBxEmpType.Items.Add("Contract Employee");
            CBxEmpType.Items.Add("Sales Employee");
            CBxSearchType.Items.Add("Employee ID");
            CBxSearchType.Items.Add("Last Name");
            CBxEIGrade.Items.Add("A");
            CBxEIGrade.Items.Add("A-");
            CBxEIGrade.Items.Add("B+");
            CBxEIGrade.Items.Add("B");
            CBxEIGrade.Items.Add("B-");
            CBxEIGrade.Items.Add("C+");
            CBxEIGrade.Items.Add("C");
            CBxEIGrade.Items.Add("C-");
            CBxEIGrade.Items.Add("D+");
            CBxEIGrade.Items.Add("D");
            CBxEIGrade.Items.Add("D-");
            CBxEIGrade.Items.Add("E");
            CBxMarital.Items.Add("Married");
            CBxMarital.Items.Add("Single");
            CBxEICreditHours.Items.Add(1);
            CBxEICreditHours.Items.Add(2);
            CBxEICreditHours.Items.Add(3);
            CBxEICreditHours.Items.Add(4);
            CBxEICreditHours.Items.Add(5);
            Lbl01.Hide();
            Lbl02.Hide();
            Lbl03.Hide();
            TxtBxLable1.Hide();
            TxtBxLable2.Hide();
            TxtBxLable3.Hide();
            _count = 0;
            _testDataCount = 0;
            _rules = BusinessRules.INSTANCE;
            _currentViewedEmp = null;
            _currentViewedCourse = null;
           _file = new FileIO();
            _course_holder = null;
            _searchList = new List<string>();
            _searchListEI = new List<string>();
            BtnSaveEmp.Enabled = false;
            UpdateEducBenefitsTab();
        }
        /// <summary>
        /// Purpose: Combo Box Event handler. Changes display of screen when Combo box is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbBxEmpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Object selected = CBxEmpType.SelectedItem;
            string item = selected.ToString();
            switch (item)
            {
                case HRLY_EMP:
                    HourlyView();
                    break;
                case SRLY_EMP:
                    SalaryContractView();
                    break;
                case CONT_EMP:
                    SalaryContractView();
                    break;
                case SALE_EMP:
                    SalesView();
                    break;
            }
         
        }
        /// <summary>
        /// Purpose: This is a simple class for how the view of Just Sales emp looks on the main window.
        /// Preconditions: None
        /// Postconditions: None
        /// </summary>
        void SalesView()
        {
            Lbl01.Show();
            Lbl02.Show();
            Lbl03.Show();
            TxtBxLable1.Show();
            TxtBxLable2.Show();
            TxtBxLable3.Show();
            Lbl01.Text = "Salary:";
            Lbl02.Text = "Commision:";
            Lbl03.Text = "Gross Sales: ";
        }
        /// <summary>
        /// Purpose: This is a simple class for how the view of Just hourly looks on the main window.
        /// Preconditions: None
        /// Postconditions: None
        /// </summary>
        void HourlyView()
        {
            Lbl01.Show();
            Lbl02.Show();
            Lbl03.Hide();
            TxtBxLable1.Show();
            TxtBxLable2.Show();
            TxtBxLable3.Hide();
            Lbl01.Text = "Hourly Rate:";
            Lbl02.Text = "Hours Worked: ";
        }
        /// <summary>
        /// Purpose: This is a simple class for how the view of Just salary looks on the main window.
        /// Preconditions: None
        /// Postconditions: None
        /// </summary>
        void SalaryContractView()
        {
            Lbl01.Show();
            Lbl02.Hide();
            Lbl03.Hide();
            TxtBxLable1.Show();
            TxtBxLable2.Hide();
            TxtBxLable3.Hide();
            Lbl01.Text = "Salary:";
        }
        /// <summary>
        /// Purpose: Exit Button Even Handler. Exits the App when pressed.
        ///  Preconditions: None
        /// Postconditions: None
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        /// <summary>
        /// Purpose: The BtnClear event handler, clears all text when clear button is pressed.
        /// Preconditions: None
        /// Postconditions: None
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearMainWindow();
        }
        /// <summary>
        /// Purpose: Simple functions to clear the all data out of text boxs in the main window.
        /// Preconditions: None
        /// Postconditions: None
        /// </summary>
        private void ClearMainWindow()
        {
            TxtBxEmpID.Clear();
            TxtFirstName.Clear();
            TxtLastName.Clear();
            TxtMiddInit.Clear();
            CBxMarital.Text = null;
            TxtPhoneNum.Clear();
            TxtDepartment.Clear();
            TxtTitle.Clear();
            TxtBxLable1.Clear();
            TxtBxLable2.Clear();
            TxtBxLable3.Clear();
        }
        /// <summary>
        /// Purpose: This is the AddEmp button even handler. Handles necesary steps when you add en employee
        /// Preconditions: None
        /// Postconditions: None
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddEmp_Click(object sender, EventArgs e)
        {


            Object selected = CBxEmpType.SelectedItem;
            if (selected == null)
            {
                MessageBox.Show(ER_MSG_EMPTYPE, ER_MSG_NAME_INVD_DATA, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            else
            {
                string item = selected.ToString();
                switch (item)
                {
                    case HRLY_EMP:
                        try
                        {
                            _obj_holder= new Hourly(TxtBxEmpID.Text, TxtFirstName.Text, TxtLastName.Text,TxtMiddInit.Text,CBxMarital.Text,TxtPhoneNum.Text,TxtDepartment.Text,TxtTitle.Text,MCalStartDate.SelectionRange.Start.ToString(),TxtBxLable1.Text, TxtBxLable2.Text);
                            _rules.employees.Add(_obj_holder.EmpID,_obj_holder);
                            empID_holder =_obj_holder.EmpID;
                            _obj_holder = _rules.employees[empID_holder];
                            RTBxBack.AppendText("\n" + _obj_holder.EmpNameFirst +" "+ _obj_holder.EmpNameLast+"\n");
                            RTBxBack.AppendText(_obj_holder.EmpID.ToString() + "\n");
                            RTBxBack.AppendText(_obj_holder.EmpType.ToString() + "\n");
                            RTBxBack.AppendText(_obj_holder.HourlyRate.ToString() + "\n");
                            RTBxBack.AppendText(_obj_holder.HoursWorked.ToString() + "\n");
                            BtnClear_Click(sender, e);
                            _count++;
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message, ER_MSG_NAME_ERR, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                        break;
                    case SALE_EMP:
                        try
                        {
                            _obj_holder = new Sales(TxtBxEmpID.Text, TxtFirstName.Text,TxtLastName.Text, TxtMiddInit.Text, CBxMarital.Text, TxtPhoneNum.Text, TxtDepartment.Text, TxtTitle.Text, MCalStartDate.SelectionRange.Start.ToString(), TxtBxLable1.Text, TxtBxLable2.Text, TxtBxLable3.Text);
                            _rules.employees.Add(_obj_holder.EmpID, _obj_holder);
                            empID_holder = _obj_holder.EmpID;
                            _obj_holder = _rules.employees[empID_holder];
                            RTBxBack.AppendText("\n" + _obj_holder.EmpNameFirst + " " + _obj_holder.EmpNameLast + "\n");
                            RTBxBack.AppendText(_obj_holder.EmpID.ToString() + "\n");
                            RTBxBack.AppendText(_obj_holder.EmpType.ToString() + "\n");
                            RTBxBack.AppendText(_obj_holder.MonthlySalary.ToString() + "\n");
                            RTBxBack.AppendText(_obj_holder.ComSales.ToString() + "\n");
                            RTBxBack.AppendText(_obj_holder.GrossSales.ToString() + "\n");
                            BtnClear_Click(sender, e);
                            _count++;
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message, ER_MSG_NAME_ERR, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                        break;
                    case CONT_EMP:
                        try
                        {
                            _obj_holder = new Contract(TxtBxEmpID.Text, TxtFirstName.Text,TxtLastName.Text, TxtMiddInit.Text, CBxMarital.Text, TxtPhoneNum.Text, TxtDepartment.Text, TxtTitle.Text, MCalStartDate.SelectionRange.Start.ToString(), TxtBxLable1.Text);
                            _rules.employees.Add(_obj_holder.EmpID, _obj_holder);
                            empID_holder = _obj_holder.EmpID;
                            _obj_holder = _rules.employees[empID_holder];
                            RTBxBack.AppendText("\n" + _obj_holder.EmpNameFirst + " " + _obj_holder.EmpNameLast + "\n");
                            RTBxBack.AppendText(_obj_holder.EmpID.ToString() + "\n");
                            RTBxBack.AppendText(_obj_holder.EmpType.ToString() + "\n");
                            RTBxBack.AppendText(_obj_holder.ContractSalary.ToString() + "\n");
                            BtnClear_Click(sender, e);
                            _count++;
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message, ER_MSG_NAME_ERR, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                        break;
                    case SRLY_EMP:
                        try
                        {
                            _obj_holder = new Salary(TxtBxEmpID.Text, TxtFirstName.Text,TxtLastName.Text, TxtMiddInit.Text, CBxMarital.Text, TxtPhoneNum.Text, TxtDepartment.Text, TxtTitle.Text, MCalStartDate.SelectionRange.Start.ToString(), TxtBxLable1.Text);
                            _rules.employees.Add(_obj_holder.EmpID, _obj_holder);
                            empID_holder = _obj_holder.EmpID;
                            _obj_holder = _rules.employees[empID_holder];
                            RTBxBack.AppendText("\n" + _obj_holder.EmpNameFirst + " " + _obj_holder.EmpNameLast + "\n");
                            RTBxBack.AppendText(_obj_holder.EmpID.ToString() + "\n");
                            RTBxBack.AppendText(_obj_holder.EmpType.ToString() + "\n");
                            RTBxBack.AppendText(_obj_holder.MonthlySalary.ToString() + "\n");
                            BtnClear_Click(sender, e);
                            _count++;
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message, ER_MSG_NAME_ERR, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                        break;
                }//End of switch
            }//End of else
        }//End of AddBtn even handler
        /// <summary>
        /// Purpose: This is the Test button even handler. This will add Data to feild for easy testing.
        /// Preconditions: None. 
        /// Postconditions: None
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTestData_Click(object sender, EventArgs e)
        {
            switch (_testDataCount)
            {
                //Contract Employee Test
                case 0:
                    CBxEmpType.SelectedIndex = 0;
                    TxtFirstName.Text = "Bilbo";
                    TxtLastName.Text = "Baggins";
                    TxtMiddInit.Text = "J";
                    CBxMarital.Text = "Married";
                    TxtPhoneNum.Text = "8013394058";
                    TxtDepartment.Text = "ICS";
                    TxtTitle.Text = "Manager";
                    TxtBxEmpID.Text = "1234";
                    TxtBxLable1.Text = "12,500";
                    _testDataCount++;
                    break;
                case 1:
                    CBxEmpType.SelectedIndex = 1;
                    TxtFirstName.Text = "Frodo";
                    TxtLastName.Text = "Baggins";
                    TxtBxEmpID.Text = "2236";
                    TxtMiddInit.Text = "F";
                    CBxMarital.Text = "Single";
                    TxtPhoneNum.Text = "8018889999";
                    TxtDepartment.Text = "Computer Science";
                    TxtTitle.Text = "T.A.";
                    TxtBxLable1.Text = "20.00";
                    TxtBxLable2.Text = "50";
                    _testDataCount++;
                    break;
                case 2:
                    CBxEmpType.SelectedIndex = 2;
                    TxtFirstName.Text = "Samwise";
                    TxtLastName.Text = "Gampshee";
                    TxtBxEmpID.Text = "6985";
                    TxtMiddInit.Text = "Z";
                    CBxMarital.Text = "Married";
                    TxtPhoneNum.Text = "4357778989";
                    TxtDepartment.Text = "HR";
                    TxtTitle.Text = "Customer Rep.";
                    TxtBxLable1.Text = "100,500";
                    _testDataCount++;
                    break;
                case 3:
                    CBxEmpType.SelectedIndex = 3;
                    TxtFirstName.Text = "Gandolf";
                    TxtLastName.Text = "TheGray";
                    TxtMiddInit.Text = "A";
                    CBxMarital.Text = "Single";
                    TxtPhoneNum.Text = "9098887676";
                    TxtDepartment.Text = "Finance";
                    TxtTitle.Text = "Analyst";
                    TxtBxEmpID.Text = "01224";
                    TxtBxLable1.Text = "70,000";
                    TxtBxLable2.Text = "10.0";
                    TxtBxLable3.Text = "40,000";
                    _testDataCount=0;
                    break;
                    
            }
        }
        /// <summary>
        /// Purpose: This is the Save Button DB event handler. Handles what happens when the Save DB button is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, EventArgs e)
        {
            _file.DB = _rules.employees;
            _file.CloseDB();
            _rules.employees = _file.DB;
            RTBxBack.Text = "";
            ClearMainWindow();
            TxtDBName.Text = "";
            _searchList.Clear();
            LBxFoundEmp.DataSource = null;
            _currentViewedEmp = null;
            TxtSearch.Text = "";
            ClearMainWindow();
        }
        /// <summary>
        /// Purpose: This is the Open DB Button event handler. Handles what happens when the Open DB button is pressed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOpen_Click(object sender, EventArgs e)
        {
            bool _validDB = true;
            try {
                _file.OpenDB();
                _file.ReadDB();
                _searchList.Clear();
                LBxFoundEmp.DataSource = null;
                _currentViewedEmp = null;
                TxtSearch.Text = "";
                ClearMainWindow();
            }
            catch (SerializationException)
            {
                MessageBox.Show(ER_MSG_INVALIDDB, ER_MSG_NAME_INVALIDDB, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                _validDB = false;
            }
            catch (IOException)
            {
                MessageBox.Show(ER_MSG_INVALIDDB, ER_MSG_NAME_INVALIDDB, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                _validDB = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ER_MSG_NAME_ERR, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                _validDB = false;
            }
            if (_validDB)
            {
                _rules.employees = _file.DB;
                TxtDBName.Text = _file.FileDBName;
                if (_rules.employees.Count != 0)
                {
                    UpdateRichTextBox();
                }
                else
                {
                    RTBxBack.Text = "";
                    RTBxBack.AppendText("--Display--");
                }
            }
        }
        /// <summary>
        /// Purpose: To update the rich text box on the second tab with all contents of _rules.employees
        /// </summary>
        public void UpdateRichTextBox()
        {
            RTBxBack.Text = "";
            RTBxBack.AppendText("--Display--");
            foreach(KeyValuePair<uint,Employee> p in _rules.employees)
            {
                RTBxBack.AppendText("\n" + p.Value.EmpNameFirst.ToString() + " " + p.Value.EmpNameLast.ToString() + "\n");
                RTBxBack.AppendText(p.Value.EmpID.ToString() + "\n");
                RTBxBack.AppendText(p.Value.EmpType.ToString() + "\n");
                if(p.Value is Hourly)
                {
                    Hourly _h = (Hourly)p.Value;
                    RTBxBack.AppendText(_h.HourlyRate.ToString() + "\n");
                    RTBxBack.AppendText(_h.HoursWorked.ToString() + "\n");
                }
                else if(p.Value is Contract)
                {
                    Contract _c = (Contract)p.Value;
                    RTBxBack.AppendText(_c.ContractSalary.ToString() + "\n");
                }
                else if(p.Value is Salary)
                {
                    Salary _s = (Salary)p.Value;
                    RTBxBack.AppendText(_s.MonthlySalary.ToString() + "\n");
                }
                else if (p.Value is Sales)
                {
                    Sales _s = (Sales)p.Value;
                    RTBxBack.AppendText(_s.MonthlySalary.ToString() + "\n");
                    RTBxBack.AppendText(_s.ComSales.ToString() + "\n");
                    RTBxBack.AppendText(_s.GrossSales.ToString() + "\n");
                }
         
            }
        }
        /// <summary>
        /// Purpose: This is a function to update the main tab with currect selected employee.
        /// </summary>
        public void UpdateMainTab()
        {
            ClearMainWindow();
            if (_currentViewedEmp is Hourly)
            {
                HourlyView();
                TxtFirstName.Text = _currentViewedEmp.EmpNameFirst;
                TxtLastName.Text = _currentViewedEmp.EmpNameLast;
                TxtBxEmpID.Text = _currentViewedEmp.EmpID.ToString();
                CBxEmpType.SelectedIndex = CBxEmpType.Items.IndexOf(HRLY_EMP);
                TxtMiddInit.Text = _currentViewedEmp.MiddleInitial;
                CBxMarital.Text = _currentViewedEmp.MaritalStatus;
                TxtPhoneNum.Text = _currentViewedEmp.PhoneNumber;
                TxtDepartment.Text = _currentViewedEmp.Department;
                TxtTitle.Text = _currentViewedEmp.Title;
                MCalStartDate.SetDate(Convert.ToDateTime(_currentViewedEmp.StartDate));
                TxtBxLable1.Text = _currentViewedEmp.HourlyRate.ToString();
                TxtBxLable2.Text = _currentViewedEmp.HoursWorked.ToString();
            }
            else if(_currentViewedEmp is Contract)
            {
                SalaryContractView();
                TxtFirstName.Text = _currentViewedEmp.EmpNameFirst;
                TxtLastName.Text = _currentViewedEmp.EmpNameLast;
                TxtBxEmpID.Text = _currentViewedEmp.EmpID.ToString();
                CBxEmpType.SelectedIndex = CBxEmpType.Items.IndexOf(CONT_EMP);
                TxtMiddInit.Text = _currentViewedEmp.MiddleInitial;
                CBxMarital.Text = _currentViewedEmp.MaritalStatus;
                TxtPhoneNum.Text = _currentViewedEmp.PhoneNumber;
                TxtDepartment.Text = _currentViewedEmp.Department;
                TxtTitle.Text = _currentViewedEmp.Title;
                MCalStartDate.SetDate(Convert.ToDateTime(_currentViewedEmp.StartDate));
                TxtBxLable1.Text = _currentViewedEmp.ContractSalary.ToString();
            }
            else if (_currentViewedEmp is Sales)
            {
                SalesView();
                TxtFirstName.Text = _currentViewedEmp.EmpNameFirst;
                TxtLastName.Text = _currentViewedEmp.EmpNameLast;
                TxtBxEmpID.Text = _currentViewedEmp.EmpID.ToString();
                CBxEmpType.SelectedIndex = CBxEmpType.Items.IndexOf(SALE_EMP);
                TxtMiddInit.Text = _currentViewedEmp.MiddleInitial;
                CBxMarital.Text = _currentViewedEmp.MaritalStatus;
                TxtPhoneNum.Text = _currentViewedEmp.PhoneNumber;
                TxtDepartment.Text = _currentViewedEmp.Department;
                TxtTitle.Text = _currentViewedEmp.Title;
                MCalStartDate.SetDate(Convert.ToDateTime(_currentViewedEmp.StartDate));
                TxtBxLable1.Text = _currentViewedEmp.MonthlySalary.ToString();
                TxtBxLable2.Text = _currentViewedEmp.ComSales.ToString();
                TxtBxLable3.Text = _currentViewedEmp.GrossSales.ToString();
            }
            else if(_currentViewedEmp is Salary)
            {
                SalaryContractView();
                TxtFirstName.Text = _currentViewedEmp.EmpNameFirst;
                TxtLastName.Text = _currentViewedEmp.EmpNameLast;
                TxtBxEmpID.Text = _currentViewedEmp.EmpID.ToString();
                CBxEmpType.SelectedIndex = CBxEmpType.Items.IndexOf(SRLY_EMP);
                TxtMiddInit.Text = _currentViewedEmp.MiddleInitial;
                CBxMarital.Text = _currentViewedEmp.MaritalStatus;
                TxtPhoneNum.Text = _currentViewedEmp.PhoneNumber;
                TxtDepartment.Text = _currentViewedEmp.Department;
                TxtTitle.Text = _currentViewedEmp.Title;
                MCalStartDate.SetDate(Convert.ToDateTime(_currentViewedEmp.StartDate));
                TxtBxLable1.Text = _currentViewedEmp.MonthlySalary.ToString();
            }
            
        }
        /// <summary>
        /// Purpse: This is the event handler for the Add button on the third tab to add a class to an employee benifit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEIAddSave_Click(object sender, EventArgs e)
        {
            try {
                //First Grab info. Second add to courses list of employee.
                string cID = TxtEICourseID.Text;
                string cDesc = TxtEICourseDesc.Text;
                string cGrade = CBxEIGrade.Text;
                string cAprDate = MCalAprv.SelectionRange.Start.ToString();
                string cHour = CBxEICreditHours.Text;
                //Second Add course to employee's list
                _course_holder = new Course(cID, cDesc, cGrade, cAprDate, cHour);
                _currentViewedEmp.courses.Add(cID, _course_holder);
                //Third Update selectbox area.
                AddToSearchListEI(_course_holder.ID, _course_holder.Description);
                LBxEICourses.DataSource = null;
                LBxEICourses.DataSource = _searchListEI;
                //Fourth remove course info from screen
                TxtEICourseID.Text = "";
                TxtEICourseDesc.Text = "";
                CBxEIGrade.SelectedItem = null;
                CBxEICreditHours.SelectedItem = null;
                _course_holder = null;
            } 
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, ER_MSG_NAME_ERR, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        /// <summary>
        /// Purpose: This is the event handler for the search button on the front tab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            Object selected = CBxSearchType.SelectedItem;
            if (selected == null)
            {
                MessageBox.Show(ER_MSG_EMPTYPE, ER_MSG_NAME_INVD_DATA, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            else
            {
                string item = selected.ToString();
                string itemSearch = TxtSearch.Text;
                switch (item)
                {
                    case SEARCH_EMP_ID:
                        //Code for search on emp id.
                        try
                        {
                            _currentViewedEmp = _rules[itemSearch];
                            _searchList.Clear();
                            AddToSearchList(_currentViewedEmp.EmpNameFirst,_currentViewedEmp.EmpNameLast, _currentViewedEmp.EmpID);
                            LBxFoundEmp.DataSource = null;
                            LBxFoundEmp.DataSource = _searchList;
                        }
                        catch(OverflowException)
                        {
                            MessageBox.Show(ER_MSG_EMPID, ER_MSG_NAME_INVD_DATA, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        catch(KeyNotFoundException)
                        {
                            MessageBox.Show(ER_MSG_EMPID_NOEXSIST, ER_MSG_NAME_NOTFOUND, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message, ER_MSG_NAME_ERR, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        break;
                    case SEARCH_LAST_NAME:
                        //Code for search on last name.
                        try
                        {
                            _searchList.Clear();
                            List<Employee> foundEmp = _rules.FindEmpByLastName(itemSearch);
                            foreach(Employee emp in foundEmp)
                            {
                                AddToSearchList(emp.EmpNameFirst, emp.EmpNameLast, emp.EmpID);
                            }
                            LBxFoundEmp.DataSource = null;
                            LBxFoundEmp.DataSource = _searchList;
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message, ER_MSG_NAME_ERR, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        break;
                }

            }
        }//end of BtnSearch_Click even handler
        /// <summary>
        /// Purpose: This function concatinates the employee name with their ID to be insterted into the _searchList. It then inserts
        ///          that string into the list for display in the search results list box.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        private void AddToSearchList(string namef,string nameL, uint id)
        {
            string listName = string.Format("{0} {1}({2})", namef,nameL, id);
            _searchList.Add(listName);
        }
        /// <summary>
        /// Purpose: To get the ID of an imployee from the _searchList so that you can look up in the employee in the sorted dictionary.
        /// </summary>
        /// <param name="listName"></param>
        /// <returns>uint</returns>
        private uint GetIDFromSearchList(string listName)
        {
            uint id;
            string[] word = listName.Split(' ', '(', ')');
            string lastID = word[word.Length - 2];
            id = Convert.ToUInt32(lastID);
            return id;
        }
        /// <summary>
        /// Purpose: To create a coursid + course description entry for the course table list.
        /// </summary>
        /// <param name="courseID"></param>
        /// <param name="courseDescription"></param>
        private void AddToSearchListEI (string courseID, string courseDescription)
        {
            string listName = string.Format("{0} --- {1}", courseID, courseDescription);
            _searchListEI.Add(listName);
        }
        /// <summary>
        /// Purpose:
        /// </summary>
        /// <param name="listname"></param>
        /// <returns></returns>
        private string GetCourseIDFromSearchListEI(string listname)
        { 
            string[] word = listname.Split(' ');
            return word[0];
        }
        /// <summary>
        /// Purpose: This is the event handler for with an item is selected in the search box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LBxFoundEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string currentItem = LBxFoundEmp.SelectedItem.ToString();
                uint id = GetIDFromSearchList(currentItem);
                _currentViewedEmp = _rules.employees[id];
                UpdateMainTab();
                UpdateEducBenefitsTab();
                BtnSaveEmp.Enabled = true;
                BtnAddEmp.Enabled = false;
            }
            catch (NullReferenceException)
            {
            }
        
        }
        /// <summary>
        /// Purpse: Event handler for the save emp button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveEmp_Click(object sender, EventArgs e)
        {
            Object selected = CBxEmpType.SelectedItem;
            string item = selected.ToString();
            switch (item)
            {
                case HRLY_EMP:
                    try
                    {
                        _obj_holder = new Hourly(TxtBxEmpID.Text, TxtFirstName.Text, TxtLastName.Text, TxtMiddInit.Text, CBxMarital.Text, TxtPhoneNum.Text, TxtDepartment.Text, TxtTitle.Text, MCalStartDate.SelectionRange.Start.ToString(), TxtBxLable1.Text, TxtBxLable2.Text);
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message, ER_MSG_NAME_ERR, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                    break;
                case SALE_EMP:
                    try
                    {
                        _obj_holder = new Sales(TxtBxEmpID.Text, TxtFirstName.Text, TxtLastName.Text, TxtMiddInit.Text, CBxMarital.Text, TxtPhoneNum.Text, TxtDepartment.Text, TxtTitle.Text, MCalStartDate.SelectionRange.Start.ToString(), TxtBxLable1.Text, TxtBxLable2.Text, TxtBxLable3.Text);
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message, ER_MSG_NAME_ERR, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                    break;
                case CONT_EMP:
                    try
                    {
                        _obj_holder = new Contract(TxtBxEmpID.Text, TxtFirstName.Text, TxtLastName.Text, TxtMiddInit.Text, CBxMarital.Text, TxtPhoneNum.Text, TxtDepartment.Text, TxtTitle.Text, MCalStartDate.SelectionRange.Start.ToString(), TxtBxLable1.Text);
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message, ER_MSG_NAME_ERR, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                    break;
                case SRLY_EMP:
                    try
                    {
                        _obj_holder = new Salary(TxtBxEmpID.Text, TxtFirstName.Text, TxtLastName.Text, TxtMiddInit.Text, CBxMarital.Text, TxtPhoneNum.Text, TxtDepartment.Text, TxtTitle.Text, MCalStartDate.SelectionRange.Start.ToString(), TxtBxLable1.Text);
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message, ER_MSG_NAME_ERR, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                    break;
            }
            if(_obj_holder.EmpID == _currentViewedEmp.EmpID)
            {
                _rules.employees[_obj_holder.EmpID] = _obj_holder;
            }
            else
            {
                _rules.employees.Remove(_currentViewedEmp.EmpID);
                _rules.employees.Add(_obj_holder.EmpID, _obj_holder);
            }
            _searchList.Clear();
            LBxFoundEmp.DataSource = null;
            LBxFoundEmp.DataSource = _searchList;
            UpdateRichTextBox();
            ClearMainWindow();
            ClearEducBenifitsTab();
            BtnSaveEmp.Enabled = false;
            BtnAddEmp.Enabled = true;
            _currentViewedEmp = null;
            UpdateEducBenefitsTab();
        }
        /// <summary>
        /// Purpose: Simple function to clear the contents in the educaitonal benefits tab.
        /// </summary>
        public void ClearEducBenifitsTab()
        {
            TxtEIEmpName.Text = "";
            TxtEIEmpID.Text = "";
            TxtEICourseID.Text = "";
            TxtEICourseDesc.Text = "";
            CBxEIGrade.Text = "";
            CBxEICreditHours.Text = "";

        }
        /// <summary>
        /// Purpose:Simple funcion to update educational benefits tab with currently viewed employee.
        /// </summary>
        public void UpdateEducBenefitsTab()
        {
            //If you aren't looking at an employee already inserted.
            if (_currentViewedEmp == null)
            {
                TxtEIEmpName.Text = "SELECT EMP ON MAIN PAGE";
                TxtEIEmpID.Text = "";
                TxtEICourseID.Text = "";
                TxtEICourseDesc.Text = "";
                _searchListEI.Clear();
                LBxEICourses.DataSource = null;
                LBxEICourses.DataSource = _searchListEI;
                DisableEBInput();
            }
            //When looking at an already selected employee.
            else
            {
                TxtEIEmpName.Text = _currentViewedEmp.EmpNameFirst + _currentViewedEmp.EmpNameLast;
                TxtEIEmpID.Text = _currentViewedEmp.EmpID.ToString();
                EnableEBInput();
                //Check if employee had courses list.
                if (_currentViewedEmp.courses.Count != 0)
                {
                    _searchListEI.Clear();
                    foreach (KeyValuePair<string, Course> c in _currentViewedEmp.courses)
                    {
                        _searchListEI.Add(string.Format("{0} --- {1}", c.Key, c.Value.Description));

                    }
                    LBxEICourses.DataSource = null;
                    LBxEICourses.DataSource = _searchListEI;
                }
                else
                {
                    TxtEICourseID.Text = "";
                    TxtEICourseDesc.Text = "";
                    CBxEIGrade.Text = null;
                    CBxEICreditHours.Text = null;
                    _searchListEI.Clear();
                    LBxEICourses.DataSource = null;
                    LBxEICourses.DataSource = _searchListEI;
                    BtnEISvCourse.Enabled = false;

                }
            }
        }
        /// <summary>
        /// Purpose: Even Handler for the Month 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MCalAprv_DateChanged(object sender, DateRangeEventArgs e)
        {

        }
        /// <summary>
        /// Purpose: To enable all input object on the Educational benefits tab.
        /// </summary>
        public void DisableEBInput()
        {
            TxtEICourseID.Enabled = false;
            TxtEICourseDesc.Enabled = false;
            CBxEIGrade.Enabled = false;
            MCalAprv.Enabled = false;
            CBxEICreditHours.Enabled = false;
            BtnEIAddSave.Enabled = false;
            BtnEISvCourse.Enabled = false;
        }
        /// <summary>
        /// Purpose: To enable all input object on the Educational benefits tab.
        /// </summary>
        public void EnableEBInput()
        {
            TxtEICourseID.Enabled = true;
            TxtEICourseDesc.Enabled = true;
            CBxEIGrade.Enabled = true;
            MCalAprv.Enabled = true;
            CBxEICreditHours.Enabled = true;
            BtnEIAddSave.Enabled = true;
            BtnEISvCourse.Enabled = true;
        }
        /// <summary>
        /// Purpose: Event handler for the Save Course button on the educational benefits page. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSvCourse_Click(object sender, EventArgs e)
        {
            try
            {
                _course_holder = new Course(TxtEICourseID.Text, TxtEICourseDesc.Text, CBxEIGrade.Text, MCalAprv.SelectionRange.Start.ToString(), CBxEICreditHours.Text);
                if(_course_holder.ID == _currentViewedCourse.ID)
                {
                    _currentViewedEmp.courses[_course_holder.ID] = _course_holder;
                    _rules.employees[_currentViewedEmp.EmpID] = _currentViewedEmp;
                }
                else
                {
                    _currentViewedEmp.courses.Remove(_currentViewedCourse.ID);
                    _currentViewedEmp.courses[_course_holder.ID] = _course_holder;
                    _rules.employees[_currentViewedEmp.EmpID] = _currentViewedEmp;
                }
                UpdateEducBenefitsTab();
                TxtEICourseID.Text = "";
                TxtEICourseDesc.Text = "";
                CBxEICreditHours.Text = "";
                CBxEIGrade.Text = "";
                _currentViewedCourse = null;
                _course_holder = null;
                BtnEIAddSave.Enabled = true;
                BtnEISvCourse.Enabled = false;
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, ER_MSG_NAME_ERR, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        /// <summary>
        /// Purpose: This is an event handler for the List Box of courses.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LBxEICourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            try {
                string currentItem = LBxEICourses.SelectedItem.ToString();
                if (currentItem != null)
                {
                    string id = GetCourseIDFromSearchListEI(currentItem);
                    _currentViewedCourse = _currentViewedEmp.courses[id];
                    TxtEICourseID.Text = _currentViewedCourse.ID;
                    TxtEICourseDesc.Text = _currentViewedCourse.Description;
                    CBxEIGrade.Text = _currentViewedCourse.Grade;
                    MCalAprv.SetDate(_currentViewedCourse.ApprovalDate);
                    CBxEICreditHours.Text = _currentViewedCourse.CreditHours.ToString();
                    BtnEIAddSave.Enabled = false;
                    BtnEISvCourse.Enabled = true;
                }
            }
            catch (NullReferenceException ex)
            {
                //Ignore null caught excpetion.
            }
           
        }

  
    }

}
