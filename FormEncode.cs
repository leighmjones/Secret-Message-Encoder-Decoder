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
using System.Drawing.Imaging;

namespace Secret_Image_Builder
{
    public partial class FormEncode : Form
    {
    
        
        public FormEncode()
        {
            InitializeComponent();

            textBox1.Enabled = false;
            encodeP3Button.Enabled = false;
        }

        private Bitmap bmp;
        
        string format;
        PPM ppmFile = new PPM();
        
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }  
   

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "PPM Files|*.ppm";


            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

                if (format == "P3")
                {
                    //opens the file
                    StreamWriter writer = new StreamWriter(saveFileDialog1.FileName);

                    for (int x = 0; x < ppmFile.myData.Count; x++)
                    {
                        //Save normally
                        writer.WriteLine(ppmFile.myData[x]);

                    }
                    //closes the file
                    writer.Close();
                }
                else
                {

                    File.WriteAllBytes(saveFileDialog1.FileName, ppmFile.myBytes);
                }

            }
             
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            StreamReader file = new StreamReader(openFileDialog1.FileName);

            format = file.ReadLine();
            
            file.Close();
            //Checks the format of the ppm file
            if(format == "P3")//P3
            {
                // Send filename to method
                bmp = ppmFile.LoadPPMP3(openFileDialog1.FileName);

                textBox1.Enabled = true;
            }
            else//P6
            {
                //Sends filename to method
                bmp = ppmFile.LoadPPMP6(openFileDialog1.FileName);

                textBox1.Enabled = true;
            }

            //displays file as an image
            pictureBox1.Image = bmp;
        }
      

        //Encode button 
        private void button1_Click(object sender, EventArgs e)
        {
            int value;
            
            ppmFile.myList.Clear();
           

            if (format == "P6")
            {
                ppmFile.position = (int.Parse(textBox3.Text) * 3) + ppmFile.startindex;
                //Checks to see if the user position is greater than the PPM file length
              
            }
            else
            {
                ppmFile.position = (int.Parse(textBox3.Text) * 3);
                //Checks to see if the user position is greater than the PPM file length
               
            }
            

            //Loops through the message and encodes
            foreach (char c in textBox1.Text)
            {
                value = Convert.ToInt32(c);

                //stores the value
                ppmFile.Encode(value, format);
                

                //debugging.....shows encoded value
                textBox2.Text = value.ToString();
            }
            
            
            //Checks to see if the user followed instructions...
            if (textBox2.Text != "37")
            {
                MessageBox.Show("Please end message with a %");
                textBox2.Clear();
            }
            else
            {
                MessageBox.Show("Encoded!");

            }

        }

      
        private void button1_Click_1(object sender, EventArgs e)
        {
            int value;

            if (format == "P6")
            {
                ppmFile.position = (int.Parse(textBox3.Text) * 3) + ppmFile.startindex;
                

                //Loops through the message and encodes
                foreach (char c in textBox1.Text)
                {
                    value = Convert.ToInt32(c);

                    if (ppmFile.position > ppmFile.myBytes.Length)
                    {
                        MessageBox.Show("Not a valid position, try a lower number.");
                        break;
                    }
                     
                    //stores the value
                    ppmFile.Encode(value, format);
                }

                if (ppmFile.position < ppmFile.myBytes.Length)
                {
                    MessageBox.Show("Great position. Click ENCODE.");
                    encodeP3Button.Enabled = true;

                }

            }
            else
            {
                ppmFile.position = (int.Parse(textBox3.Text) * 3);
                //Checks to see if the user position is greater than the PPM file length
              //Loops through the message and encodes
                foreach (char c in textBox1.Text)
                {
                    value = Convert.ToInt32(c);

                    if (ppmFile.position > ppmFile.myData.Count)
                    {
                        MessageBox.Show("Not a valid position, try a lower number.");
                        break;
                    }
                        //stores the value
                        ppmFile.Encode(value, format);
                }

                if (ppmFile.position < ppmFile.myData.Count)
                {
                    MessageBox.Show("Great position. Click ENCODE.");
                    encodeP3Button.Enabled = true;

                }
            }
        }
    }
}
