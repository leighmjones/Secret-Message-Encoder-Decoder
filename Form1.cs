using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace SecretImageDECODE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1.Enabled = false;
        }

       
        string format;
        int count = 0;
        PPM2 ppmFile2 = new PPM2();
        

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Shows dialog box
            openFileDialog1.ShowDialog();

            openFileDialog1.Filter = "PPM Files|*.ppm";
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            //Opens the files & reads it
            //ppmFile2.LoadPPM(openFileDialog1.FileName);
            StreamReader file = new StreamReader(openFileDialog1.FileName);

            format = file.ReadLine();
            file.Close();
            //Checks the format of the ppm file
            if (format == "P3")//P3
            {
                // Send filename to method
                ppmFile2.LoadPPM(openFileDialog1.FileName);
                button1.Enabled = true;
            }
            else//P6
            {
                //Sends filename to method
                 ppmFile2.LoadPPMP6(openFileDialog1.FileName);
                button1.Enabled = true;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Grabs user position
            try
            {
                ppmFile2.position = int.Parse(textBox2.Text);
            
              
               int msg;

                if (format == "P3")
                {
                    ppmFile2.position = (ppmFile2.position * 3);

                    /*starts at user position and increments by 3, checks if x is less than the file length*/
                    for (int x = ppmFile2.position; x < ppmFile2.myData.Count; x += 3)
                    {
                        /*Reads the values of the List*/
                        msg = Convert.ToInt32(ppmFile2.myData[x]);

                        /*counts every character passed through*/
                        count++;

                        /*Checks to see if count is less that 255 and Checks to see if msg has a value of 37(%) - our break point*/
                        if (count < 255 && msg != 37)
                        {
                            /*Prints out the message if there is not a break point*/
                            textBox1.Text += (char)msg;
                        }
                        else
                        {
                            x = ppmFile2.myData.Count;
                        }

                    }

                }
                else
                {
                    ppmFile2.position = (ppmFile2.position * 3) + ppmFile2.startindex;

                    for (int x = ppmFile2.position; x < ppmFile2.myBytes.Length; x += 3)
                    {
                        /*Reads the values of the List*/
                        msg = Convert.ToInt32(ppmFile2.myBytes[x]);

                        /*counts every character passed through*/
                        count++;

                        /*Checks to see if count is less that 255 or if msg has a value of 37(%) - our break point*/
                        if (count < 255 && msg != 37)
                        {
                            /*Prints out the message if there is not a break point*/
                            textBox1.Text += (char)msg;
                        }
                        else
                        {
                            x = ppmFile2.myBytes.Length;
                        }

                    }
                }
            }
            catch
              
            {
                MessageBox.Show("Numbers only!");
            }

        }

    }   

    

}
