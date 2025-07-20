using OpenCvSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elevatorAlarm
{
   public class ObjectTracking2
    {
        //"E:\\困人智能检测程序和冬奥视频\\延庆冬奥运营中心中区1-能用.mp4";
//        static void Main(string[] args)
//        {
//            String filenameAlarm = "D:\\0Aproject\\dongaoVideo\\v1.mp4";

//            filenameAlarm = "E:\\困人智能检测程序和冬奥视频\\延庆冬奥运营中心中区1-能用.mp4";
//            float[] a = new float[] { 0, 0, 0, 0, 0, 1, 0, 0, //0-7
//                0, 163, 74,32,1 ,3,1};//8-14
//            /*参数：

//int temp=a[0];//上一帧位置；初始化设置为：0
//framesNum=a[1];//间隔读取帧数；初始化设置为：0
//int Increment=a[2];//开门帧数累积；初始化设置为：0
//int  Diminishing=a[3];//关门帧数累积；初始化设置为：0
//int doorStill=a[4];//门静止状态；初始化设置为：0
//int frame=a[5];//隔几帧读取
//int kai=a[6];//开门状态；初始化设置为：0
//int guan=a[7];//关门状态；初始化设置为：0
//int j=a[8];//:j=1时左侧门；j=0时右侧门
//int Maxx=a[9];//门检测最右边框
//int Minx=a[10];//门检测最左边框
//int doorLength =a[11]//最小门缝长度
//int kaiAndGuanZhenShuLeiJi=a[12];
//*/

//            float[] b = new float[] { 100, 38, 263, 83 };//宽467，高84


//            // Opens MP4 file (ffmpeg is probably needed)
//            var capture = new VideoCapture(filenameAlarm);
//            //int sleepTime = (int)Math.Round(1000 / capture.Fps);

//            Mat image = new Mat();


//            // When the movie playback reaches end, Mat.data becomes NULL.
//            while (capture.Read(image))
//            {
//                // same as cvQueryFrame
//                if (image.Empty())
//                    break;
//                ObjectTracking2 ob = new ObjectTracking2();
//                Cv2.ImShow(" video", image);
//                float[] vs = ob.objectsTracking(image, a, b);
//                //for(int i=0;i<vs.Length;i++)
//                // System.Console.WriteLine("vs:" + vs[i]);

//                Cv2.WaitKey(20);

//            }

//        }

        /*static void Main(string[] args)
        {

            String filenameAlarm = "D:\\0video\\video\\2.mp4";
            float[] a = new float[] { 0, 0, 0, 0, 0, 3, 0, 0, 1, 280, 131, 50 };
            float[] b = new float[] { 130, 110, 347, 180 };
            // Opens MP4 file (ffmpeg is probably needed)
            var capture = new VideoCapture(filenameAlarm);
            //int sleepTime = (int)Math.Round(1000 / capture.Fps);

            Mat image = new Mat();


            // When the movie playback reaches end, Mat.data becomes NULL.
            while (capture.Read(image))
            {
                // same as cvQueryFrame
                if (image.Empty())
                    break;
                float[] vs = objectsTracking(image, a, b);
                //for(int i=0;i<vs.Length;i++)
                // System.Console.WriteLine("vs:" + vs[i]);
                Cv2.ImShow(" video", image);
                //Cv2.ImShow("dealvideo", dealvideo);
                Cv2.WaitKey(20);

            }

        }
*/



        //冬奥
        /*static void Main(string[] args)
        {
            *//*String filenameAlarm = "D:\\BaiduNetdiskDownload\\test1.mp4";
            float[] a = new float[] { 0, 0, 0, 0, 0, 2, 0, 0, 1, 244, 77, 65 };
            float[] b = new float[] { 77, 92, 244, 163 };*//*



            String filenameAlarm = "D:\\0Aproject\\dongaoVideo\\20211009222955.mp4";
            float[] a = new float[] { 0, 0, 0, 0, 0, 2, 0, 0,
                1, 278, 66, 50};
            float[] b = new float[] { 66, 91, 278, 161 };
            // Opens MP4 file (ffmpeg is probably needed)
            var capture = new VideoCapture(filenameAlarm);
            //int sleepTime = (int)Math.Round(1000 / capture.Fps);

            Mat image = new Mat();


            // When the movie playback reaches end, Mat.data becomes NULL.
            while (capture.Read(image))
            {
                // same as cvQueryFrame
                if (image.Empty())
                    break;
                ObjectTracking2 ob = new ObjectTracking2();
                float[] vs = ob.objectsTracking(image, a, b);
                //for(int i=0;i<vs.Length;i++)
                // System.Console.WriteLine("vs:" + vs[i]);
                Cv2.ImShow(" video", image);
                Cv2.WaitKey(20);

            }

        }*/

        public float[] objectsTracking(Mat video, float[] a, float[] b)
        {
            /*参数：

            int temp=a[0];//上一帧位置；初始化设置为：0
            framesNum=a[1];//间隔读取帧数；初始化设置为：0
            int Increment=a[2];//开门帧数累积；初始化设置为：0
            int  Diminishing=a[3];//关门帧数累积；初始化设置为：0
            int doorStill=a[4];//门静止状态；初始化设置为：0
            int frame=a[5];//隔几帧读取
            int kai=a[6];//开门状态；初始化设置为：0
            int guan=a[7];//关门状态；初始化设置为：0
            int j=a[8];//:j=1时左侧门；j=0时右侧门
            int Maxx=a[9];//门检测最右边框
            int Minx=a[10];//门检测最左边框
            int doorLength =a[11]//最小门缝长度
            int kaiAndGuanZhenShuLeiJi=a[12];
            */

            /*
            b[]划定门检测区域
            * b[0]:为x坐标
            * b[1]:为y坐标
            * b[2]:图像x1
            * b[3]:图像y1坐标
            *
            * */


            //确定检测区域  !!!注意此处检测区域大小应根据实际情况调整
            /*int x1=630;
            int y1=0;
            int width1=960-x1;//640（x1+width1,y1+height1）检测区域
            int height1=330;//1136*/
            Rect rect = new Rect((int)b[0], (int)b[1], (int)(b[2] - b[0]), (int)(b[3] - b[1]));

            //Rect roi = new Rect(400, 300, 400, 400);//首先要用个rect确定我们的兴趣区域在哪
            video = new Mat(video, rect);
            //video = video.Submat(rect);

            //调整数字图片的角度
            OpenCvSharp.Point center = new OpenCvSharp.Point(video.Width / 2.0, video.Height / 2.0);
            Mat affineTrans = Cv2.GetRotationMatrix2D(center, -2, 1.0);//13
                                                                       //Imgproc.INTER_NEAREST
            Cv2.WarpAffine(video, video, affineTrans, video.Size(),
                InterpolationFlags.Nearest);

            //Cv2.ImShow("111111video", video);
            //Cv2.WaitKey(20);


            Mat dst = new Mat();
            Mat dealvideo = new Mat();
            Mat gray = new Mat();
            Mat threshold = new Mat();
            //Mat lines=new Mat();
            int temp = (int)a[0];//上一帧位置
                                 //int framesNum=a[1];//间隔读取帧数
            int Increment = (int)a[2];//开门帧数累积
            int Diminishing = (int)a[3];//关门帧数累积
            int doorStill = (int)a[4];//门静止状态
            int frame = (int)a[5];//隔几帧读取
            int kai = (int)a[6];//开门状态
            int guan = (int)a[7];//关门状态
            int j = (int)a[8];//:j=1时左侧门；j=0时右侧门
            //int Maxx = (int)(a[9] - b[0]);//门检测最右边框
           // int Minx = (int)(a[10] - b[0]);//门检测最左边框
           int Maxx = (int)a[9] ;//门检测最右边框
           int Minx = (int)a[10];//门检测最左边框
            int doorLength = (int)a[11];//最小门缝长度
            int kaiAndGuanZhenShuLeiJi = (int)a[12];
            int Dilate = (int)a[13];//膨胀
            int Erode = (int)a[14];//腐蚀


            /*//放大视频图像
            float scale=2f;
            float width=video.width();
            float height=video.height();
            Imgproc.resize(video, video, new Size(width*scale,height*scale));*/

            /* //高斯降噪
             Imgproc.GaussianBlur(video, dst, new Size(3,3),0,0);

             //转灰度图片
             Imgproc.cvtColor(dst, gray, Imgproc.COLOR_BGR2GRAY);*/
            //转灰度图片
            // Cv2.CvtColor(video, gray, ColorConversionCodes.BGR2BGRA,0);
            Cv2.CvtColor(video, gray, ColorConversionCodes.RGB2GRAY, 0);


            //边缘检测
            //Imgproc.Canny(gray, threshold, 100, 300, 3, false);
            //Imgproc.Scharr(gray,threshold,CvType.CV_32F,1,0);
            //Imgproc.Laplacian(gray,threshold,CvType.CV_32F);
            Cv2.Sobel(gray, threshold, MatType.CV_32F, 1, 0, 3);
            Cv2.ConvertScaleAbs(threshold, threshold);
            //Core.convertScaleAbs(threshold, threshold);
            Mat ximage = new Mat();
            Cv2.Normalize(threshold, threshold, 0, 255, NormTypes.MinMax);
            //Core.normalize(threshold, threshold, 0, 255, Core.NORM_MINMAX);
            //threshold.ConvertTo(ximage, MatType.CV_8UC1);
            threshold.ConvertTo(ximage, MatType.CV_8UC1);
            //Cv2.CvtColor(threshold,ximage, ColorConversionCodes.RGB2GRAY);
            //二值化z
            //bin.binaryzation(gray);
            //Imgproc.adaptiveThreshold(threshold,dealvideo,255,Imgproc.ADAPTIVE_THRESH_GAUSSIAN_C,Imgproc.THRESH_BINARY,55,10);
            //Cv2.Threshold(ximage, dealvideo, 0, 255, Imgproc.THRESH_BINARY | Imgproc.THRESH_OTSU);

            Mat knenel = Cv2.GetStructuringElement(MorphShapes.Ellipse, new OpenCvSharp.Size(3, 3));
            Mat knene2 = Cv2.GetStructuringElement(MorphShapes.Ellipse, new OpenCvSharp.Size(3, 3));
            Cv2.Dilate(ximage, ximage, knenel, new OpenCvSharp.Point(0, 0), Dilate);//膨胀4
            Cv2.Erode(ximage, ximage, knene2, new OpenCvSharp.Point(0, 0), Erode);//腐蚀3
            Cv2.Threshold(ximage, dealvideo, 0, 255, ThresholdTypes.Binary | ThresholdTypes.Otsu);
          

            // 找出对应物体在图像中的坐标位置(X,Y)及宽、高(width,height)轮廓发现与位置标定
            List<Rect> rects = process(dealvideo, doorLength);
            
            //Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&& " +rects.Count);

            Cv2.ImShow("dealvideo", dealvideo);

            //List<Rect> rects=process( dealvideo);

            // 在物体轮廓外画矩形
           //Console.WriteLine("发现 "+rects.Count+" 对象");


            /*  for (int i = 0; i < rects.Count; i++)
              {
                  Cv2.Rectangle(video, new Point(rects[i].X, rects[i].Y), new Point(rects[i].X + rects[i].Width, rects[i].Y + rects[i].Height), new Scalar(255, 0, 0), 1, LineTypes.AntiAlias, 0);
                  //Imgproc.rectangle(video,new Point(rects.get(i).x+x1, rects.get(i).y+y1), new Point(rects.get(i).x+x1 + rects.get(i).width, rects.get(i).y+y1 + rects.get(i).height), new Scalar(255, 0, 0),1,Imgproc.LINE_AA,0);
                  //Console.WriteLine("发现x " + rects[i].X);
              }*/


            
            if (a[1] == frame)
            {
                
                a[1] = 0;
                int oneFrameDoorPosition = -1;
                int num = 0;
                int M;
                
                if (j == 1)
                {
                    M = Maxx;//此处Min意味着最靠近左门框
                }
                else
                {
                    M = Minx;//此处Min意味着最靠近右门框
                }
                //注意修改

                //int temp1=0;
                string positonInformation ="线条数："+ rects.Count+",位置信息：";
                for (int i = 0; i < rects.Count; i++)
                {
                    /*if (rects[i].X!=0) {
                        Console.WriteLine("X     "+ rects[i].X+"Y      "+ rects[i].Y);
                    }*/
                    //Imgproc.rectangle(video, new Point(rects.get(i).x, rects.get(i).y), new Point(rects.get(i).x + rects.get(i).width, rects.get(i).y + rects.get(i).height), new Scalar(255, 0, 0), 1, Imgproc.LINE_AA, 0);
                    //Imgproc.rectangle(video,new Point(rects.get(i).x+x1, rects.get(i).y+y1), new Point(rects.get(i).x+x1 + rects.get(i).width, rects.get(i).y+y1 + rects.get(i).height), new Scalar(255, 0, 0),1,Imgproc.LINE_AA,0);
                    //System.out.println(temp+":"+rects.get(i).x);
                    positonInformation += rects[i].X+",";
                    if (j == 1)
                    {//左侧门
                        if ((rects[i].X < Maxx) && (rects[i].X > Minx))
                        {//190,40
                            num++;
                           
                            oneFrameDoorPosition = rects[i].X;
                            if (oneFrameDoorPosition < M)
                            {
                                M = oneFrameDoorPosition;
                            }
                        }
                    }
                    else if (j == 0)
                    {//右侧
                        if ((rects[i].X < Maxx) && (rects[i].X > Minx))
                        {//190,40
                            num++;
                           // Console.WriteLine("rects[i].X    " + rects[i].X);
                            oneFrameDoorPosition = rects[i].X;
                            if (oneFrameDoorPosition > M)
                            {
                                M = oneFrameDoorPosition;
                            }
                        }
                    }                    
                }
                Console.WriteLine(positonInformation);
                //Console.WriteLine("NUM    "+num);
                /* if (num==0) 
                 {
                     //门静止帧增加
                     doorStill++;
                     if (doorStill >= kaiAndGuanZhenShuLeiJi)
                     {
                         //doorStill = kaiAndGuanZhenShuLeiJi;
                         Increment = 0;
                         Diminishing = 0;
                     }
                     if (Increment > 0)
                     {
                         Increment--;
                     }
                     if (Diminishing > 0)
                     {
                         Diminishing--;
                     }
                 }*/

                if (num != 0)
                {
                    if (j == 1)
                    {
                        oneFrameDoorPosition = M;
                    }
                    else if (j == 0)
                    {
                        oneFrameDoorPosition = M;
                    }

                    if (oneFrameDoorPosition != 0 && oneFrameDoorPosition != -1)
                    {

                        if (temp == 0)
                        {//初始化一帧
                            temp = oneFrameDoorPosition;
                        }

                        if (temp > oneFrameDoorPosition)
                        { //开门j=1;
                            Increment++;//开门方向帧累积j=1;
                            if (Increment >= kaiAndGuanZhenShuLeiJi)
                            {
                                Increment = kaiAndGuanZhenShuLeiJi;
                                Diminishing = 0;
                                doorStill = 0;
                            }
                            if (Diminishing > 0)
                            {
                                Diminishing--;
                            }
                            if (doorStill > 0)
                            {
                                doorStill--;
                            }
                        }
                        else if (temp < oneFrameDoorPosition)
                        { //关门j=1
                            Diminishing++;//关门方向帧增加j=1
                            if (Diminishing >= kaiAndGuanZhenShuLeiJi)
                            {
                               Diminishing = kaiAndGuanZhenShuLeiJi;
                                Increment = 0;
                                doorStill = 0;
                            }
                            if (Increment > 0)
                            {
                                Increment--;
                            }
                            if (doorStill > 0)
                            {
                                doorStill--;
                            }
                        }
                        else
                        { //门静止帧增加
                            doorStill++;
                            if (doorStill >= kaiAndGuanZhenShuLeiJi)
                            {
                                doorStill = kaiAndGuanZhenShuLeiJi;
                             
                            }
                            if (Increment > 0)
                            {
                                Increment--;
                            }
                            if (Diminishing > 0)
                            {
                                Diminishing--;
                            }
                        }

                        /*Console.WriteLine("Increment"+Increment);
                        Console.WriteLine("Diminishing" + Diminishing);
                        Console.WriteLine("doorStill" + doorStill);*/

                        if (j == 1)
                        {
                            if (Increment == kaiAndGuanZhenShuLeiJi)
                            {
                                
                                    Console.WriteLine("!!!!!!!!!!!!!开门的相对速度为!!!!!!!!!!!!" +
                                        (temp - oneFrameDoorPosition));
                                kai++;
                                if (guan > 0)
                                {
                                    guan--;
                                }
                            }
                            if (Diminishing == kaiAndGuanZhenShuLeiJi)
                            {
                               
                                    Console.WriteLine("!!!!!!!!!!!关门的相对速度为!!!!!!!!!!" + 
                                        (oneFrameDoorPosition - temp));
                                guan++;
                                if (kai > 0)
                                {
                                    kai--;
                                }
                            }
                        }
                        else if (j == 0)
                        {
                            if (Increment == kaiAndGuanZhenShuLeiJi)
                            {
                               // System.Console.WriteLine("!!!!!!!!!!!!关门的相对速度为!!!!!!!!!!!!!!" + (temp - oneFrameDoorPosition));
                                guan++;
                                if (kai > 0)
                                {
                                    kai--;
                                }
                            }
                            if (Diminishing == kaiAndGuanZhenShuLeiJi)
                            {
                               // System.Console.WriteLine("!!!!!!!!!!!!!开门的相对速度为!!!!!!!!!!!!!!" + (oneFrameDoorPosition - temp));
                                kai++;
                                if (guan > 0)
                                {
                                    guan--;
                                }
                            }
                        }

                        temp = oneFrameDoorPosition;
                    }
                }
                /*else
                {//门静止帧增加
                    doorStill++;
                    if (doorStill >= 3)
                    {
                        doorStill = 3;
                    }
                    if (Increment > 0)
                    {
                        Increment--;
                    }
                    if (Diminishing > 0)
                    {
                        Diminishing--;
                    }

                    temp = 0;
                }*/


            }

            a[1]++;


            //Imgproc.rectangle(dealvideo,rects,new Scalar(0, 0, 255), 3, 8, 0);
            //展示最终的效果
            //HighGui.imshow("roi", roi);


            /* Cv2.ImShow(" video", video);
             Cv2.ImShow("dealvideo", dealvideo);
             Cv2.WaitKey(20);*/
            /* HighGui.imshow("dealvideo", dealvideo);
             HighGui.imshow(" video", video);
             HighGui.waitKey(50);*/


            //System.out.println("当前帧程序处理时间："+(System.currentTimeMillis() - startTime)+"ms");

            video.Release();
            dealvideo.Release();
            dst.Release();
            threshold.Release();
            ximage.Release();

            if (kai > kaiAndGuanZhenShuLeiJi+1)
            {
                kai = kaiAndGuanZhenShuLeiJi + 1;
            }
            if (guan > kaiAndGuanZhenShuLeiJi+1)
            {
                guan = kaiAndGuanZhenShuLeiJi + 1;
            }


            a[0] = temp;
            //a[1]=framesNum;
            a[2] = Increment;
            a[3] = Diminishing;
            a[4] = doorStill;
            a[6] = kai;
            a[7] = guan;

            return a;
        }


        public  List<Rect> process(Mat video, int doorLength)//RetrievalModes.FloodFill
        {
            Mat[] contours;
            Cv2.FindContours(video, out contours, new Mat(), 0, ContourApproximationModes.ApproxSimple);
          //  Cv2.ImShow("dgfdfd", video);
            
            //  对象结果
            List<Rect> rects = new List<Rect>();
            Rect rect;
            if (contours.Length > 0)
            {// 如果发现图像
                int y = 0;

                for (int i = 0; i < contours.Length; i++)
                {
                    rect = new Rect();
                    double length = Cv2.ArcLength(contours[i], false);
                    //double area = Imgproc.contourArea(contours.get(i));

                    if (length > doorLength)
                    {
                        y++;
                        
                        if (y >= 1)
                        {
                            rect = Cv2.BoundingRect(contours[i]);
                            //rect = Imgproc.boundingRect(contours.get(i));
                            rects.Add(rect);
                        }

                    }
                    /*else
                    {
                        rect.X = rect.Y = rect.Width = rect.Height = 0;
                    }*/
                    
                }
            }
            else
            {// 如果没有发现图像
                rect = new Rect();
                rect.X = rect.Y = rect.Width = rect.Height = 0;
                rects.Add(rect);
            }

            return rects;

        }
    }
}