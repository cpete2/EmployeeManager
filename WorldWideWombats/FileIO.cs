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
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Employee
{
    class FileIO: object, IFileAccess
    {
        private OpenFileDialog _openDlg;
        private SaveFileDialog _saveDlg;
        private FileStream _fs;
        private BinaryFormatter _bf;
        public SortedDictionary<uint, Employee> DB { get; set; }
        public string FileDBName { get; set; }
        /// <summary>
        /// Purpose: To serialize the DB sorted dictionary out to the file in the FileStream instance.
        /// </summary>
        public void WriteDB()
        {
            _bf.Serialize(_fs, DB);
        }
        /// <summary>
        /// Purpose: To deserialize a file from the FileStream into the DB SortedDictionary.
        /// </summary>
        public void ReadDB()
        {
            if (_fs != null)
            {
                //DB.Clear();
                _fs.Seek(0, SeekOrigin.Begin);
                DB = (SortedDictionary<uint, Employee>)_bf.Deserialize(_fs);
            }
        }
        /// <summary>
        /// Purpose: To open a database file and assign it to the FileStream for reading.
        /// </summary>
        public void OpenDB()
        {
                string _filePath = null;
                _openDlg.Filter = "All Files *.*|*.*|Text Files *.txt|*.txt|Binary Files *.bin|*.bin|Serialized Files *.ser|*.ser";
                _openDlg.FilterIndex = 1;
                if (_openDlg.ShowDialog() == DialogResult.OK)
                {
                    _filePath = _openDlg.FileName;
                    FileDBName = System.IO.Path.GetFileName(_filePath);
                    if (_fs == null)
                    {
                        _fs = new FileStream(_filePath, FileMode.Open);
                    }
                    else
                    {
                        _fs.Close();
                        _fs = new FileStream(_filePath, FileMode.Open);
                    }

                }
        }
        /// <summary>
        /// Purpose: To close a save out and deserialize an open file.
        /// </summary>
        public void CloseDB()
        {
            string _filePath = null;
            _saveDlg.Filter = "All Files *.*|*.*|Text Files *.txt|*.txt|Binary Files *.bin|*.bin|Serialized Files *.ser|*.ser";
           // _saveDlg.FilterIndex = 1;
            if (_saveDlg.ShowDialog() == DialogResult.OK)
            {
                _filePath = _saveDlg.FileName;
                FileDBName = System.IO.Path.GetFileName(_filePath);
                _fs = new FileStream(_filePath, FileMode.Create, FileAccess.ReadWrite);
                WriteDB();
                _fs.Close();
                _fs = null;
                DB.Clear();
            }
            
        }
        /// <summary>
        /// Purpose: Default Constructor
        /// </summary>
        public FileIO()
        {
            _openDlg = new OpenFileDialog();
            _saveDlg = new SaveFileDialog();
            _bf = new BinaryFormatter();
            DB = new SortedDictionary<uint, Employee>();
        }
    }
}
