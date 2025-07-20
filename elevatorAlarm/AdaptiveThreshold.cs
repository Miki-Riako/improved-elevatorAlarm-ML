using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace elevatorAlarm
{
    class AdaptiveThreshold
    {
      /*  static void Main(string[] args) {
            for (int i=0;i<=16;i++) 
            {
                String str = "C:\\Users\\Administrator\\Desktop\\" + i + ".png";
                AdaptiveThreshold1(str);
                Cv2.WaitKey(1000);
            }
            Cv2.WaitKey();

        }*/
        public static  void AdaptiveThreshold1(String str) {
            Mat img1 = Cv2.ImRead(str, ImreadModes.Grayscale);//IMREAD_GRAYSCALE
           //Cv2.ImShow("img1", img1);
            Mat img2 = Cv2.ImRead(str);
            //Cv2.Resize(img1,img1,new OpenCvSharp.Size(),300,300,InterpolationFlags.Area);
            Cv2.CvtColor(img1, img1, ColorConversionCodes.BayerRG2GRAY);//进行，灰度处理COLOR_RGB2GRAY
            //Cv2.ImShow("res2", img1);
           Cv2.MedianBlur(img1, img1, 5);//中值滤波
            
            Mat res1 =new Mat();
            Cv2.AdaptiveThreshold(img1,res1, 255,AdaptiveThresholdTypes.MeanC,ThresholdTypes.Binary,33,-10);
            Cv2.ImShow("res1",res1);
            Cv2.ImShow("img2", img2);
            //img1 = Cv2.resize(img1, (300, 300), interpolation = cv2.INTER_AREA)
            //    cv2.imshow('img1', img1)



            //    res1 = cv2.adaptiveThreshold(img1, 255, cv2.ADAPTIVE_THRESH_GAUSSIAN_C, cv2.THRESH_BINARY, 25, 5)
            //    res2 = cv2.adaptiveThreshold(img1, 255, cv2.ADAPTIVE_THRESH_MEAN_C, cv2.THRESH_BINARY, 25, 5)


            //    cv2.imshow('res1', res1)

        }





    }
}
