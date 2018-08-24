# Secret Message Encoder
This application allows you to hide a message within the image pixels


<h1>TOP SECRET IMAGE APPLICATIONS</h1>


<h5>APPLICATION DESCRIPTION : Encode & Decode</h5>

We have implemented a way to send secret messages in digital images.  Our first application, Encode, lets you upload an image in .PPM P3 (ASCII) format and encode messages in the pixels.  After encoding your image, open our second application, Decode, to extract the message. 

<h5>USER GUIDE for ENCODE & DECODE APPLICATIONS</h5>

To use the first application, make sure all pictures are in .PPM P3 (ASCII) format.  The best images to use are noisy pictures with dark colors and depth, as opposed to images with flat colors. IMAGE A is a better picture to hide messages in.  Hiding messages in a picture with a white background (IMAGE B), will possibly show the pixel change.


IMAGE A 
![Preview](http://images6.fanpop.com/image/photos/32300000/Sea-Life-sea-life-32310790-1600-1200.jpg)                     

IMAGE B 
![Preview](http://superawesomevectors.com/wp-content/uploads/2015/01/flat-pencil-vector-icon.jpg)                     

          

After selecting your picture that is in P3 .PPM format, type in your message.  The message can only contain 255 characters in the text box.  Once the max length of characters is entered, the program will not let you enter any more characters.  

Once you have entered your message, make sure to end it with a “%” sign; this lets the second application know that it is the end of your message.  If a “%” is not entered, the application will prompt you to enter “%” at the end. Press ENCODE to convert your message.

Next, Go to File and Click “Save”, rename your image and click “Save” again. Open the second application. Go to “File” then “Open”.  Select your encoded .PPM file, then “OK” (or simply double click).  Press DECODE, and your message will appear.  Keep in mind that the image will not appear, only the extracted message.

                
