using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Windows.Forms;


namespace Secret_Image_Builder
{
    class PPM
    {
        
        //holds user position
        public int position;
       
        //List to hold value of the encoded list
        public List<string> myList = new List<string>();

        //List to hold the ppm P3 file for saving
        public List<string> myData = new List<string>();

        //List to hold the ppm  P6 file for saving
        public byte[] myBytes;

        //holds start index to encode
        public int startindex;

        //P3 Method
        public Bitmap LoadPPMP3(string fileName)
        {
            //Reads the file
            StreamReader InputFile2 = new StreamReader(fileName);
            

            //Read the first four lines of ppm file and assign them
            string PpmType = InputFile2.ReadLine();
            string PpmComment = InputFile2.ReadLine();
            string PpmSize = InputFile2.ReadLine();
            string PpmMax = InputFile2.ReadLine();
            
            //Adds the first 4 lines to the list
            myData.Add(PpmType.ToString());
            myData.Add(PpmComment.ToString());
            myData.Add(PpmSize.ToString());
            myData.Add(PpmMax.ToString());


            // Split the values of the 3rd line and store the values to appropriate variables
            string[] values = PpmSize.Split();
            int width = Convert.ToInt32(values[0]);
            int height = Convert.ToInt32(values[1]);

            //Create instance of bitmap
            Bitmap bmp = new Bitmap(width, height);


            //Set intial value for x and y axis
            int x = 0;
            int y = 0;
            
            // Retrieve the remaining bytes
            while (!InputFile2.EndOfStream)
            {
                  
                  //Reads the colors from the file, and adds to the List
                  byte red = Convert.ToByte(InputFile2.ReadLine());
                  myData.Add(red.ToString());
                  byte grn = Convert.ToByte(InputFile2.ReadLine());
                  myData.Add(grn.ToString());
                  byte blue = Convert.ToByte(InputFile2.ReadLine());
                  myData.Add(blue.ToString());


                  // Set RGB Value
                  bmp.SetPixel(x, y, Color.FromArgb(red, grn, blue));

                  // Increment x by 1
                  x++;

                  // if statement to move location of where pixel is drawn
                  if (x == width)
                  {
                      y++;
                      x = 0;
                  }

            }
            
            // end while
            InputFile2.Close();
              return bmp;
            
        }

        //P6 Method        
        public Bitmap LoadPPMP6(string fileName)
        {
            //calls the method
            int [] ppmdata =PPMSIZE(fileName);


            //Splits the PPMSize 
            int width = Convert.ToInt32(ppmdata[0]);
            int height = Convert.ToInt32(ppmdata[1]);
            startindex = ppmdata[2];

            //Create instance of bitmap
            Bitmap bmp = new Bitmap(width, height);
            
            //Reads the file
            byte [] InputFileBytes = File.ReadAllBytes(fileName);

            //add bytes to list
            myBytes = InputFileBytes;
            myList.Add(myBytes.ToString());
            //Set intial value for x and y axis
            int x = 0;
            int y = 0;
            
            for (int i = ppmdata[2]; i < InputFileBytes.Length; i+=3)
            {
                //starts read pixel bytes 
                byte red = InputFileBytes[i];
                //myBytes.Add(red);
                byte grn = InputFileBytes[i + 1];
                //myBytes.Add(grn);
                byte blue = InputFileBytes[i + 2];
                //myBytes.Add(blue);

                // Set RGB Value
                bmp.SetPixel(x, y, Color.FromArgb(red, grn, blue));
               
                // Increment x by 1
                x++;
               
                // if statement to move location of where pixel is drawn
                if (x == width)
                {
                     y++;
                     x = 0;
                }
             
            }//end for
                
            return bmp;

        }//end method
        
        //Encode Method
        public void Encode(int value, string format)
        {

            //checks for format
            if (format =="P3")
            {
                //P3
                   myData[position] = value.ToString();

                    position += 3; 
                
                
            }
            else
            {
                //P6
                myBytes[position] = (byte)value;

                position += 3;
            }
            
        }
        
        //PPM Size Method
        public int[] PPMSIZE(string fileName)
        {
            byte[] pfile = File.ReadAllBytes(fileName);
            int[] returnSize = new int[3];
            int newLine = 0;
            int index = 0;
            string tempSize = "";
            string[] Size;
           
            while (newLine < 4)
            {

                if (pfile[index] == 0x0A)
                {
                    newLine++;
                }

                if (newLine == 2)
                {
                    if (pfile[index] != 10)
                    {
                        tempSize += (char)pfile[index];
                    }
                }

                index++;
            }

            Size = tempSize.Split();
            returnSize[0] = int.Parse(Size[0]);
            returnSize[1] = int.Parse(Size[1]);
            returnSize[2] = index; 

            return returnSize;
        }

    }
}
    
    


    

