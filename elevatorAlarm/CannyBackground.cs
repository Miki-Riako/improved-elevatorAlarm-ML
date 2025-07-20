using OpenCvSharp;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elevatorAlarm
{
    class CannyBackground
    {
        //"D:\\0Aproject\\dongaoVideo\\CannyBackground\\v1JianMoShiPing.mp4";
        /*
                static void Main(string[] args)
                {

                    String filename1 = "D:\\0Aproject\\dongaoVideo\\CannyBackground\\v1JianMoShiPing.mp4";
                    String filename2 = "D:\\0Aproject\\dongaoVideo\\CannyBackground\\v1.mp4";
                    float[] e = new float[] { 50, 70, 30, 0, 0.3f };//e[0]canny下阈值50；e[1]canny上阈值150，e[2]:检测线段长度30(人检测)e[3]:记录载人阈值,初始化为0//e[4]图片缩放比例
                    CannyBackgroundModeling2 CBM = new CannyBackgroundModeling2();
                    CannyBackgroundDetection2 CBD = new CannyBackgroundDetection2();

                    HashSet<Coordinate> c1 = CBM.BackgroundModeling(filename1, e);


                    System.Console.WriteLine();
                    System.Console.WriteLine(c1.Count);

                    // 创建 VideoCapture 对象
                    VideoCapture capture = new VideoCapture();
                    // 使用 VideoCapture 对象读取本地视频
                    capture.Open(filename2);
                    //使用 Mat video 保存视频中的图像帧 针对每一帧 做处理
                    Mat video = new Mat();

                    int f = 0;
                    while (capture.Read(video))
                    {
                        if (video.Empty())
                        {
                            break;
                        }

                        f++;
                        if (f == 5)
                        {
                            f = 0;

                            //程序开始时间
                            // Stopwatch sw = new Stopwatch();
                            //sw.Start();//开始计时

                            CBD.Detection(video, c1, e);
                            Cv2.ImShow("video", video);
                            Cv2.WaitKey(20);

                            //sw.Stop();//结束计时
                            // Console.WriteLine(sw.Elapsed);



                        }

                    }
                }
        */


        //D:\BaiduNetdiskDownload\test1.mp4 // "D:\\0video\\video\\3.mp4";
        /* static void Main(string[] args)
         {


             // String filename1 = "D:\\0video\\video\\Modeling3.mp4";
             //String filename2 = "D:\\0video\\video\\3.mp4";
             // float[] e = new float[] { 50, 150, 30, 0 };//e[0]canny下阈值50；e[1]canny上阈值150，e[2]:检测线段长度30(人检测)e[3]:记录载人阈值,初始化为0

             String filename1 = "D:\\BaiduNetdiskDownload\\test1Model.mp4";
             String filename2 = "D:\\BaiduNetdiskDownload\\test1.mp4";
             float[] e = new float[] { 50, 150, 30, 0 };//e[0]canny下阈值50；e[1]canny上阈值150，e[2]:检测线段长度30(人检测)e[3]:记录载人阈值,初始化为0
             CannyBackgroundModeling2 CBM = new CannyBackgroundModeling2();
             CannyBackgroundDetection2 CBD = new CannyBackgroundDetection2();
             HashSet<Coordinate> c1 = CBM.BackgroundModeling(filename1, e);



             System.Console.WriteLine();
             System.Console.WriteLine(c1.Count);

             // 创建 VideoCapture 对象
             VideoCapture capture = new VideoCapture();
             // 使用 VideoCapture 对象读取本地视频
             capture.Open(filename2);
             //使用 Mat video 保存视频中的图像帧 针对每一帧 做处理
             Mat video = new Mat();

             int f = 0;
             while (capture.Read(video))
             {
                 if (video.Empty())
                 {
                     break;
                 }

                 f++;
                 if (f == 5)
                 {
                     f = 0;

                     //程序开始时间
                     // Stopwatch sw = new Stopwatch();
                     //sw.Start();//开始计时

                     CBD.Detection(video, c1, e);
                     Cv2.ImShow("video", video);
                     Cv2.WaitKey(20);

                     //sw.Stop();//结束计时
                     // Console.WriteLine(sw.Elapsed);



                 }

             }
         }*/




    }











    public class CannyBackgroundDetection2
    {

        public void Detection(Mat video, HashSet<Coordinate> c1, float[] e)
        {
            //e[0]canny下阈值50；e[1]canny上阈值150，e[2]:检测线段长度30,e[3]:记录载人阈值,初始化为0
            e[3] = 0;
            HashSet<Coordinate> c2 = new HashSet<Coordinate>();
            List<Coordinate> c3 = new List<Coordinate>();
            //Mat dst=new Mat(video.size(),video.type());

            //缩放视频图像
            float scale1 = e[4] * 1f;//10意思是放大10倍
            float width1 = video.Width;
            float height1 = video.Height;
            Cv2.Resize(video, video, new OpenCvSharp.Size(width1 * scale1, height1 * scale1));
            Mat dst = video.Clone();
            //高斯降噪
            Cv2.GaussianBlur(dst, dst, new Size(3, 3), 0, 0);

            //转灰度图片
            
            Cv2.CvtColor(dst, dst, ColorConversionCodes.RGB2GRAY);
            Cv2.Canny(dst, dst, (int)e[0], (int)e[1], 3, false);
            //边缘检测
            //Imgproc.Canny(dst, dst, 80, 240, 3, false);
            //转灰度图片
            // Cv2.Threshold(dst, dst, 0, 255, Imgproc.THRESH_BINARY | Imgproc.THRESH_OTSU);

            Cv2.Threshold(dst, dst, 0, 255, ThresholdTypes.Binary | ThresholdTypes.Otsu);
            //Cv2.ImShow("dst", dst);
            //Cv2.WaitKey(20);

            /* List<Coordinate> h = new List<Coordinate>();
             h.AddRange(c1);
             Mat newdst=new Mat(dst.Rows, dst.Cols, dst.Type(),0);
             for (int i = 0; i < h.Count; i++) {
                 newdst.Set(h[i].GetX(),h[i].GetY(),255);
             }

             Cv2.ImShow("newdst", newdst);
             Cv2.WaitKey(20);*/


            for (int i = 0; i < dst.Width; i++)
            {
                for (int j = 0; j < dst.Height; j++)
                {
                    if (dst.At<Vec3b>(j, i)[0] == 255)
                    {
                        Coordinate c = new Coordinate(i, j);
                        c2.Add(c);
                    }
                }
            }


            /* Mat newdst = new Mat(dst.Rows, dst.Cols, dst.Type(), 0);

             foreach (Coordinate s in c2)
             {
                 newdst.Set(s.GetY(), s.GetX(), 255);
             }



             Cv2.ImShow("newdst", newdst);*/



            // Cv2.WaitKey(20);


            //Console.WriteLine(c2.Count);
            // Console.WriteLine(c1.Count);

            c2.IntersectWith(c1);


           // Console.WriteLine(c2.Count);
            foreach (Coordinate s in c2)
            {
                dst.Set(s.GetY(), s.GetX(), 0);
            }
           // Cv2.ImShow("dst1", dst);

            // Console.WriteLine("c2_______"+c2.Count);
            //c2.retainAll( new AlarmSystem2().backgroundModele);

            /* Mat newdst = new Mat(dst.Rows, dst.Cols, dst.Type(), 0);

            foreach (Coordinate s in a)
            {
                newdst.Set(s.GetY(), s.GetX(), 255);
            }



            Cv2.ImShow("newdst", newdst);
            Cv2.WaitKey(20);*/

            /* c3.Clear();
             c3.AddRange(c2);
            // Console.WriteLine("c3_______" + c3.Count);
             //Console.WriteLine(c2.Count);
             //List<Coordinate> c3 = new List<Coordinate>(c2);
             for (int i = 0; i < c3.Count; i++)
             {
                 int x = c3[i].GetX();
                 int y = c3[i].GetY();
                 *//*System.Console.WriteLine("*******************************");
                 System.Console.WriteLine("*******************************");
                 System.Console.WriteLine("*******************************");
                 System.Console.WriteLine("*******************************");
                 System.Console.WriteLine("x的值为："+x);
                  System.Console.WriteLine("y的值为："+y);*//*
                 dst.Set(y, x, 0);
             }
 */



            List<Rect> rects = process(dst, (int)e[2]);
            //List<Rect> rects = process(dst, 150);

            List<int[]> a2 = new List<int[]>();

            for (int i = 0; i < rects.Count; i++)
            {
                //Imgproc.rectangle(video, new Point(rects.get(i).x, rects.get(i).y), new Point(rects.get(i).x + rects.get(i).width, rects.get(i).y + rects.get(i).height), new Scalar(255, 0, 0), 1, Imgproc.LINE_AA, 0);
                Cv2.Rectangle(dst, new Point(rects[i].X, rects[i].Y), new Point(rects[i].X + rects[i].Width, rects[i].Y + rects[i].Height), new Scalar(0, 0, 255), 1, LineTypes.AntiAlias, 0);
                if (rects[i].X != 0 && rects[i].Y != 0)
                {
                    int[] c = new int[] { rects[i].X, rects[i].Y };
                    a2.Add(c);
                    //System.out.print("X:"+rects.get(i).x+"Y:"+rects.get(i).y+"  ");
                }
            }


            e[3] = a2.Count;

            Console.WriteLine(a2.Count);

            Cv2.ImShow("dst", dst);


            // Cv2.WaitKey(20);

            dst.Release();
            //return a2;


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
                        }

                    }
                    else
                    {
                        rect.X = rect.Y = rect.Width = rect.Height = 0;
                    }
                    rects.Add(rect);
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



















    public class CannyBackgroundModeling2
    {





        public HashSet<Coordinate> BackgroundModeling(String filename1, float[] e)
        {
            //e[0]canny下阈值50；e[1]canny上阈值150，e[2]:检测线段长度30,e[3]:记录载人阈值,初始化为0
            CannyBackgroundModeling2 c1 = new CannyBackgroundModeling2();
            // 创建 VideoCapture 对象
            VideoCapture capture = new VideoCapture();
            // 使用 VideoCapture 对象读取本地视频
            //capture.open("D:\\0video\\Histogram\\test1\\12.mp4");
            capture.Open(filename1);
            //使用 Mat video 保存视频中的图像帧 针对每一帧 做处理
            Mat video = new Mat();
            //HashSet<Coordinate> hashSets = new HashSet<Coordinate>();
            HashSet<Coordinate> a1 = new HashSet<Coordinate>();
            //HashSet<Coordinate> a2 = new  HashSet<>();
            Coordinate a = new Coordinate(0, 0);
            a1.Add(a);

            //String modelVideoPath="C:\\Users\\Administrator\\Desktop\\beijing.jpg";
            int frame = 0;
            while (capture.Read(video))
            {
                if (video.Empty())
                {
                    break;
                }

                /* //放大视频图像
                 float scale=0.4f;
                 float width=video.width();
                 float height=video.height();
                 Imgproc.resize(video, video, new Size(width*scale,height*scale));*/
                frame++;
                if (frame == 5)
                {
                    //程序开始时间
                    // Stopwatch sw = new Stopwatch();
                    // sw.Start();//开始计时

                    //缩放视频图像
                    float scale1 =e[4] * 1f;//10意思是放大10倍
                    float width1 = video.Width;
                    float height1 = video.Height;
                    Cv2.Resize(video, video, new OpenCvSharp.Size(width1 * scale1, height1 * scale1));

                    c1.Modeling(video, a1, e);

                    frame = 0;

                    //Cv2.ImShow("video", video);
                    // Cv2.WaitKey(20);


                    //sw.Stop();//结束计时
                    // Console.WriteLine(sw.Elapsed);
                }

                //frame++;

            }
            Console.WriteLine(a1.Count);
            return a1;
        }

        public HashSet<Coordinate> Modeling(Mat video, HashSet<Coordinate> a, float[] e)//HashSet<Coordinate>
        {

            //e[0]canny下阈值50；e[1]canny上阈值150，e[2]:检测线段长度30,e[3]:记录载人阈值,初始化为0



            // HashSet<Coordinate> d = new HashSet<Coordinate>();
            //Mat dst=new Mat(video.size(),video.type());








            Mat dst = video.Clone();
            //高斯降噪
            Cv2.GaussianBlur(dst, dst, new Size(3, 3), 0, 0);

            //转灰度图片
            Cv2.CvtColor(dst, dst, ColorConversionCodes.RGB2GRAY);


            Cv2.Canny(dst, dst, (int)e[0], (int)e[1], 3, false);
            //边缘检测
            //Cv2.Canny(dst, dst, 80, 240, 3, false);
            //Imgproc.Canny(dst, dst, 100, 300, 3, false);
            /*int channels = dst.width();
        System.out.println(channels);*/
            //Cv2.Threshold(dst, dst, 0, 255, ThresholdTypes.Otsu);
            Cv2.Threshold(dst, dst, 0, 255, ThresholdTypes.Binary | ThresholdTypes.Otsu);
            byte[] data = new byte[dst.Width * dst.Height * dst.Channels()];


            /*Cv2.ImShow("dstmodel", dst);
            System.Console.WriteLine("*");
            Cv2.WaitKey(20);*/


            dst.GetArray(0, 0, data);
            int pv;
            int size = 0;

            for (int i = 0; i < data.Length; i++)
            {
                // System.Console.WriteLine("data" + i + "的值为" + data[i]);
            }

            for (int i = 0; i < dst.Height; i++)
            {
                for (int j = 0; j < dst.Width; j++)
                {//遍历方式逐行
                    pv = data[size] & 0xff;
                    // pv = data[size];
                    if (pv == 255)
                    {
                        Coordinate c = new Coordinate(j, i);
                        a.Add(c);
                    }
                    size++;
                }
            }




            /* Mat newdst = new Mat(dst.Rows, dst.Cols, dst.Type(), 0);

             foreach (Coordinate s in a)
             {
                 newdst.Set(s.GetY(), s.GetX(), 255);
             }



             Cv2.ImShow("newdst", newdst);
             Cv2.WaitKey(20);*/

            System.Console.WriteLine(a.Count);
            //放大视频图像
            //        float scale=1f;
            //        float width=dst.width();
            //        float height=dst.height();
            //        Imgproc.resize(dst, dst, new Size(width*scale,height*scale));
            // System.out.println("当前帧程序处理时间：" + (System.currentTimeMillis() - startTime) + "ms");
            return a;



        }
    }


    public class Coordinate
    {
        private int x;
        private int y;

        public int GetX()
        {
            return x;
        }

        public void SetX(int x)
        {
            this.x = x;
        }

        public int GetY()
        {
            return y;
        }

        public void SetY(int y)
        {
            this.y = y;
        }

        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object obj)
        {
            return obj is Coordinate coordinate &&
                   x == coordinate.x &&
                   y == coordinate.y;
        }

        public override int GetHashCode()
        {
            int hashCode = 1502939027;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            return hashCode;
        }
    }
}
