using OpenCvSharp;
using System;
using System.Collections;
using System.Collections.Generic;

namespace elevatorAlarm
{
    class ObjectTracking_OnlyDoorStatus
    {
        //冬奥电梯视频String filenameAlarm = "D:\\0Aproject\\dongaoVideo\\v1.mp4";
       /* static void Main(string[] args)
        {
            String filenameAlarm = "D:\\0Aproject\\dongaoVideo\\1125—111610.mp4";
            // float[] a = new float[] { 0, 0, 0, 0, 0, 1, 0, 0, 0, 500-111,230, 50,1 ,3,1};//8-14

            float[] a = new float[] { 1, 30, 1, 0, 60, 3, 1 };
            float[] b = new float[] { 336, 58, 384, 141 };

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

                ObjectTracking_OnlyDoorStatus ob = new ObjectTracking_OnlyDoorStatus();
                float[] vs = ob.objectsTracking(image, a, b);

                //放大视频图像
                float scale = 0.5f;
                float width = image.Width;
                float height = image.Height;
                Cv2.Resize(image, image, new OpenCvSharp.Size(width * scale, height * scale));

                Cv2.ImShow(" video", image);
                //for(int i=0;i<vs.Length;i++)
                // System.Console.WriteLine("vs:" + vs[i]);

                Cv2.WaitKey(20);

            }

        }*/



        public float[] objectsTracking(Mat video, float[] a, float[] b)
        {
            /*参数：
            int framesNum = (int)a[0];//间隔读取帧数；初始化设置为：1
            int doorStill = (int)a[1];//门静止状态；初始化设置为：10
            int frame = (int)a[2];//隔几帧读取,初始化设置为：>=1
            int kaiOrGuan = (int)a[3];//开关门状态；初始化设置为：0：关门/1：开门
            int doorLength = (int)a[4];//最小门缝长度
            int Dilate = (int)a[5];//膨胀
            int Erode = (int)a[6];//腐蚀
            */

            /*
            b[]划定门检测区域
            b[0]:为x坐标
            b[1]:为y坐标
            b[2]:图像x1
            b[3]:图像y1坐标
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
           Cv2.ImShow("111111video", video);
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
            
            int framesNum = (int)a[0];//间隔读取帧数；初始化设置为：0
            int doorStill = (int)a[1];//门静止状态；初始化设置为：10
            int frame = (int)a[2];//隔几帧读取
            int kaiOrGuan = (int)a[3];//开关门状态；初始化设置为：0：关门/1：开门
            int doorLength = (int)a[4];//最小门缝长度
            int Dilate = (int)a[5];//膨胀
            int Erode = (int)a[6];//腐蚀


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
            
            Cv2.Sobel(gray, threshold, MatType.CV_32F, 1, 0, 3);
            Cv2.ConvertScaleAbs(threshold, threshold);
            //Core.convertScaleAbs(threshold, threshold);
            Mat ximage = new Mat();
            Cv2.Normalize(threshold, threshold, 0, 255, NormTypes.MinMax);
            //Core.normalize(threshold, threshold, 0, 255, Core.NORM_MINMAX);
            //threshold.ConvertTo(ximage, MatType.CV_8UC1);
            threshold.ConvertTo(ximage, MatType.CV_8UC1);
            //Cv2.CvtColor(threshold,ximage, ColorConversionCodes.RGB2GRAY);
           

            Mat knenel = Cv2.GetStructuringElement(MorphShapes.Ellipse, new OpenCvSharp.Size(3, 3));
            Mat knene2 = Cv2.GetStructuringElement(MorphShapes.Ellipse, new OpenCvSharp.Size(3, 3));
            Cv2.Dilate(ximage, ximage, knenel, new OpenCvSharp.Point(0, 0), Dilate);//膨胀4
            Cv2.Erode(ximage, ximage, knene2, new OpenCvSharp.Point(0, 0), Erode);//腐蚀3
            Cv2.Threshold(ximage, dealvideo, 0, 255, ThresholdTypes.Binary | ThresholdTypes.Otsu);


            // 找出对应物体在图像中的坐标位置(X,Y)及宽、高(width,height)轮廓发现与位置标定
            List<Rect> rects = process(dealvideo, doorLength);

            //Console.WriteLine("&&&&&&&&&&&&&&&&&&&&&&&&&&& " +rects.Count);

            //Cv2.ImShow("dealvideo", dealvideo);

            //List<Rect> rects=process( dealvideo);

            // 在物体轮廓外画矩形
            //Console.WriteLine("发现 "+rects.Count+" 对象");


            /*  for (int i = 0; i < rects.Count; i++)
              {
                  Cv2.Rectangle(video, new Point(rects[i].X, rects[i].Y), new Point(rects[i].X + rects[i].Width, rects[i].Y + rects[i].Height), new Scalar(255, 0, 0), 1, LineTypes.AntiAlias, 0);
                  //Imgproc.rectangle(video,new Point(rects.get(i).x+x1, rects.get(i).y+y1), new Point(rects.get(i).x+x1 + rects.get(i).width, rects.get(i).y+y1 + rects.get(i).height), new Scalar(255, 0, 0),1,Imgproc.LINE_AA,0);
                  //Console.WriteLine("发现x " + rects[i].X);
              }*/



            if (a[0] == frame)
            {

                a[0] = 0;
                //注意修改
                Boolean sign = true;
                //int temp1=0;
               
                if (rects.Count>0) 
                {
                    if (doorStill < 10)
                    {
                        doorStill++;
                    }
                    else 
                    {
                        doorStill = 10;
                    }

                    sign = false;
                   // Console.WriteLine("rects[i].X    " + rects.Count);
                }
               

                if (sign) 
                {
                    doorStill =0;
                }

                if (doorStill == 10)
                {
                    kaiOrGuan = 0;
                } else 
                {
                    kaiOrGuan = 1;
                }

            }


            if (kaiOrGuan == 0)
            {
                Console.WriteLine("关门状态");
            }
            else if (kaiOrGuan == 1)
            {
                Console.WriteLine("开门状态");
            }


            a[0]++;
            a[1] = doorStill;
            a[3] = kaiOrGuan;

            video.Release();
           
            dealvideo.Release();
            dst.Release();
            gray.Release();
            affineTrans.Release();
            threshold.Release();
            ximage.Release();
            knenel.Release();
            knene2.Release();
            return a;
        }

        public List<Rect> process(Mat video, int doorLength)//RetrievalModes.FloodFill
        {
            Mat[] contours;
            Cv2.FindContours(video, out contours, new Mat(), 0, ContourApproximationModes.ApproxSimple);

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

                        if (y > 1)
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
