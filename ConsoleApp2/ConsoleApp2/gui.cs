using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System.Threading;


namespace ConsoleApp2
{
    public partial class DevicesList : Form
    {
        BluetoothAddress ba = BluetoothAddress.Parse("1C:B7:2C:51:70:AC");
        public BluetoothDeviceInfo[] myArray;
        public int phoneChoosed;
        int tries;

        public DevicesList()
        {
            InitializeComponent();
            textBox1.Visible = false;
            textBox2.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            button2.Visible = false;
            button3.Visible = false;

            tries = 0;

        }
        //private Button button1;
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DevicesList());

        }
        private static void BluetoothClientConnectCallback(IAsyncResult ar)
        {
            //Have no problem with this
            Console.WriteLine(((BluetoothClient)ar.AsyncState).Connected);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
            
            /* listBox1.Items.Clear();
             BluetoothDeviceInfo[] myArr = Program.getArray();

             for( int i =0; i < myArr.Length; i++)
             {
                 listBox1.Items.Add(myArr[i]);
             }*/
          //  


        private void button1_Click_1(object sender, EventArgs e)
        {
            button1.Enabled = false;
           // Console.WriteLine("i AM RIGHT DAMMN HERE !!!!");
            //listBox1.Items.Add("Sally");
            //listBox1.Items.Add("Craig");


            // InitializeComponent();
            //List<Device> devices = new List<Device>(); //create list of devices 
            // label1.Visible = false;

            // try
            {
              using (BluetoothClient bc = new BluetoothClient())
              {

                  BluetoothDeviceInfo[] array = bc.DiscoverDevices();
                   
                    Console.WriteLine("scanning ...\n");
                  int count = 0;
                  while (count == 0)
                  {
                      array = bc.DiscoverDevices(); //scan devices 
                      count = array.Length; //count the number of devices
                  }

                  Console.WriteLine("num of devices " + count);

                  for (int i = 0; i < count; i++)
                  {

                      Console.WriteLine(array[i].DeviceName);
                      Console.WriteLine(array[i].Rssi);
                      Console.WriteLine("Length installed services: " + array[i].InstalledServices.Length);


                      string range = array[i].Rssi.ToString();

                      //  cbxDevices.Items.Add(array[i].DeviceName.ToString());


                  }
                    //setArray(array);
                    //PAIRING-----

                    for (int i = 0; i < count; i++)
                  {
                      Console.WriteLine((i + 1) + "." + array[i].DeviceName + "\n");
                      listBox1.Items.Add((i + 1) + "." + array[i].DeviceName);
                    }
                   // label3.Visible = false;
                    Console.WriteLine("Choose a phone to connect:\n ");
                    textBox1.Visible = true;
                    label1.Visible = true;
                    button2.Visible = true;

                    textBox2.Visible = true;
                    label2.Visible = true;
                    button3.Visible = true;

                    textBox2.Enabled = false;

                    myArray = array;
                }
            }
                }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            button2.Enabled = false;
            textBox2.Enabled = true;

            using (BluetoothClient bc = new BluetoothClient())
            {
                string a = textBox1.Text;

                String choose = textBox1.Text;

                //Console.WriteLine("you chosed \n "+ choose);
                if (choose != null && int.Parse(choose) > 0 && int.Parse(choose)< myArray.Length+1)
                {
                    phoneChoosed = int.Parse(choose) - 1;
                    Console.WriteLine("you choosed :" + choose);
                    BluetoothSecurity.PairRequest(myArray[phoneChoosed].DeviceAddress, "123456");
                }
                else
                {
                    textBox1.Clear();
                    textBox1.Enabled = true;
                    button2.Enabled = true;
                    textBox2.Enabled = false;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        { 
            using (BluetoothClient bc = new BluetoothClient())
            {
                Console.WriteLine("enter you pin \n ");
                string pinString = textBox2.Text;
                if (pinString.Length != 6) // did not succe
                {
                    tries++;

                    if (tries < 3)
                    {
                        textBox2.Clear();
                    }
                    else
                        Application.Exit();
                    return;
                }
                Console.WriteLine("you entered pin\n " + pinString);
                bool succed = false;
                try
                {
                    Console.WriteLine("lets set pin and see if succeded\n ");
                    succed = BluetoothSecurity.SetPin(myArray[phoneChoosed].DeviceAddress, pinString);
                }
                catch
                {
                    Console.WriteLine("xxx\n");
                }

                if (succed)
                {
                    myArray[phoneChoosed].Refresh();
                    string deviceNameOfPhoneChoosed = myArray[phoneChoosed].DeviceName;

                    textBox2.Visible = false;
                    label2.Visible = false;
                    button3.Visible = false;
                    textBox1.Visible = false;
                    label1.Visible = false;
                    button2.Visible = false;
                    Devices.Visible = false;
                    listBox1.Visible = false;
                    button1.Visible = false;

                    while (true)
                    {
                        bool inRange = false;
                        myArray = bc.DiscoverDevices();
                        for (int i = 0; i < myArray.Length; i++)
                        {
                            if (string.Compare(myArray[i].DeviceName, deviceNameOfPhoneChoosed) == 0)
                            {
                                inRange = true;
                            }
                        }

                        if (inRange)
                        {
                            Console.WriteLine("In range");
                        }
                        else
                        {
                            Console.WriteLine("Out of range");
                            LockScreen.LockWorkStation();
                        }


                    }
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        //string stRemoteName = bc.GetRemoteMachineName(ba);
        //label1.Text = stRemoteName;

    }
    }
           // }
