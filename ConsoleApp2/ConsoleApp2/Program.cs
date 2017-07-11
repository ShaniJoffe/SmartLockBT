using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;


//using Bluetooth = Windows.Devices.Bluetooth;
using System.Runtime;
using System.Windows.Forms;

namespace ConsoleApp2
{
    class Program
    {
        public static BluetoothDeviceInfo[]  myArr;

        static void setArray(BluetoothDeviceInfo[] arr)
        {
            myArr = arr; //or whatever
        }
        public static BluetoothDeviceInfo[] getArray()
        {
            return myArr;
        }
      //  [STAThread]
        //static void main
    }
}
