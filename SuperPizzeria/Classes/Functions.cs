using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SuperPizzeria.Classes
{
    class Functions
    {
        public static bool VerifyPassword(string password)
        {
            if (password.Contains(" ")) return false;
            if (password.Length == 0) return false;
            return true;
        }

        public static int[] StringToIntArray(string str)
        {
            int[] arr=new int[new Regex("-").Matches(str).Count+1];
            string[] strArray = str.Split('-');
            for(int i =0; i<strArray.Length;i++)
            {
                arr[i] = Convert.ToInt32(strArray[i]);
            }
            return arr;
        }

        public static string IntArrayToString(int[] arr)
        {
            string str = "";
            foreach(int i in arr)
            {
                str += i + "-";
            }
            return str.Substring(0, str.Length - 1);
        }

        public static ArrayList StringToArrayList(string str)
        {
            ArrayList al = new ArrayList();
            string[] strArray = str.Split('-');
            for (int i = 0; i < strArray.Length; i++)
            {
                al.Add(Convert.ToInt32(strArray[i]));
            }
            return al;
        }

        public static string ArrayListToString(ArrayList arr)
        {
            string str = "";
            foreach (int i in arr)
            {
                str += i + "-";
            }
            return str.Substring(0, str.Length - 1);
        }

    }
}
