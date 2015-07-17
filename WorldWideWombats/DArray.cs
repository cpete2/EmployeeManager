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
using System.Collections;

namespace Employee
{
    public class DArray<T> : object, IEnumerable
    {
        //--------------------Member fields---------------
        private int _Capacity;
        private T[] _iArray;
        private int _Top;
        private const int ASIZE = 4;
        private static readonly DArray<T> instance = new DArray<T>();
        private static DArray<T> _instance = null;
        /// <summary>
        /// Pupose: Instance method to return the intance of class to make class Singleton.
        /// Preconditions: None
        /// Postconditions: None
        /// Paramaters: None
        /// Returns: intance of the BuisnessRules class object.
        /// </summary>
        public static DArray<T> INSTANCE
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DArray<T>();
                }
                return _instance;
            }
        }
        //-------------------Member Properties-----------
        /// <summary>
        /// Purpose: Read only property for _Top
        /// </summary>
        public int Top { get { return _Top; } }
        /// <summary>
        /// Purpose: Readonly property for _Capacity
        /// </summary>
        public int Capacity { get { return _Capacity; } }
        //------------------Member Indexer---------------
        /// <summary>
        /// Purpose: Indexer for the _iArray
        /// </summary>
        /// <param name="index">Array Index Value</param>
        /// <returns>Indexed Value of the array</returns>
        public T this[int index]
        {
            get
            {
                if (index < _Top && _Top >= 0 && _Top <= _Capacity)
                    return _iArray[index];
                else
                {
                    //ERROR HERE
                    throw new IndexOutOfRangeException();
                }
            }
            set
            {
                if (index < _Top && _Top >= 0 && _Top < _Capacity)
                {
                    _iArray[index] = value;
                }
                else if (index == _Top && _Top < _Capacity)
                    _iArray[_Top++] = value;
                else if (index == _Top || _Top >= _Capacity)
                {
                    Resize();
                    _iArray[_Top++] = value;
                    return;
                }
                else
                    //ERROR  HERE
                    throw new IndexOutOfRangeException();
            }
        }
        //------------------Member Methods---------------
        /// <summary>
        /// Purpose: Default Constructor for initializing the fields.
        /// </summary>
        public DArray()
        {
            _iArray = new T[ASIZE];
            _Capacity = ASIZE;
            _Top = 0;
        }
        /// <summary>
        /// Purpose: Doubles the capacity of the array
        /// </summary>
        private void Resize()
        {
            T[] temp = new T[_Capacity * 2];
            _iArray.CopyTo(temp, 0);
            _Capacity *= 2;
            _iArray = temp;
            temp = null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            IEnumerator myEnum = new MyEnumerator<T>(_iArray);
            return myEnum;
        }
    }//End of DArray Class

}
