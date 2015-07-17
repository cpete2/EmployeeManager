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
using System.Collections;

namespace Employee
{
    public class MyEnumerator<T> : object, IEnumerator
    {
        //-----------Member Feilds----------
        private T[] myArray;
        private object _Current;
        private int _Top = -1;
        //----------Member Properties-------
        /// <summary>
        /// Purpose: Property for current element
        /// </summary>
        public object Current
        {
            get { return _Current; }
        }
        //-----------Member Methods---------
        /// <summary>
        /// Purpose: Paramaterized Constructor
        /// </summary>
        /// <param name="eArray">An array reference from the DArray class</param>
        public MyEnumerator(T[] eArray)
        {
            myArray = eArray;
            _Current = 0;
        }
        /// <summary>
        /// Prupse: To get reference to the next element.
        /// Preconditions:
        /// Postconditions:
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            if (_Top >= -1 && _Top < myArray.Length - 1)
            {
                _Top++;
                _Current = myArray[_Top];
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// Purpose: To reset the Enumerator
        /// </summary>
        public void Reset()
        {
            _Top = -1;
            _Current = null;
        }
    }//End of MyEnumerator Class

}
