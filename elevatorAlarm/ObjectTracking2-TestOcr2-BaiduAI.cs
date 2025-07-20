using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Threading;

namespace elevatorAlarm
{
    public class ObjectTracking2TestOcr2BaiduAI
    {
        private static void Main(string[] args)
        {
            String filenameAlarm = "D:\\0Aproject\\dongaoVideo\\v1.mp4";
            filenameAlarm = "E:\\困人智能检测程序和冬奥视频\\延庆冬奥运营中心中区1-能用.mp4";
            float[] a = new float[] { 0, 0, 0, 0, 0, 1, 0, 0, //0-7
                                      0, 163, 50,32,1 ,3,1};//8-14
            /*参数：
            a[0]上一帧位置；初始化设置为：0
            a[1]间隔读取帧数；初始化设置为：0
            a[2]开门帧数累积；初始化设置为：0
            a[3]关门帧数累积；初始化设置为：0
            a[4]门静止状态；初始化设置为：0
            a[5]隔几帧读取
            a[6]开门状态；初始化设置为：0
            a[7]关门状态；初始化设置为：0
            a[8]1时左侧门；0时右侧门
            a[9]门检测最右边框
            a[10]门检测最左边框
            a[11]最小门缝长度
            */
            float[] b = new float[] { 100, 38, 263, 83 };//宽467，高84
            float[] c = new float[] {5, 115,95,88, 255,255, 255, 0,    ////c[0]楼层高度，减少识别错误.c[1]-c[6]b0,g0,r0,b1,g1,r1;
                                     0, 1, 1, 3, -1, -10, -10, 0, 3 };//c[7]箭头位置信息0，在楼层信息上
            float[] d = new float[] { 382, 47, 411, 95 };
            float[] currentDigital = new float[] { 0, 0, 0 };
            var capture = new VideoCapture(filenameAlarm);//???
            Mat image = new Mat();//???
            int sum = 0;
            int closedDoorStatussSustainedFramNum = 0;
            int currentFloorsSustainedFramNum = 0;
            int previousFloor = -10;
            int numberByLadder = 0;
            int thereArePeopleSustainedFramNum = 0;
            float previousDoorPositon = 0;
            float doorPositonSustainedFramNum = 0;
            // When the movie playback reaches end, Mat.data becomes NULL.
            //以上都是定义变量
            while (capture.Read(image))
            {
                DateTime beforDT = DateTime.Now;//先定义时间，后面的总用时就是它
                if (image.Empty()) break;
                ObjectTracking2 ob = new ObjectTracking2();//在另一个文件中
                Cv2.ImShow(" video", image);
                Cv2.WaitKey(20);//CV2看不懂？？
                string doorInformation = "";
                float[] vs = ob.objectsTracking(image, a, b);
                Console.WriteLine("-------" + a[0]);
                if (Math.Abs(a[0] - previousDoorPositon) > 5)
                {
                    previousDoorPositon = a[0];
                    doorPositonSustainedFramNum = 0;
                }
                else doorPositonSustainedFramNum++;
                if (doorPositonSustainedFramNum > 30 && (a[9] - a[0]) > (a[0] - a[10]))
                {
                    closedDoorStatussSustainedFramNum++;
                    doorInformation = "门关闭状态";
                }
                else
                {
                    closedDoorStatussSustainedFramNum = 0;
                    doorInformation = "门打开状态";
                }
                doorInformation += a[4] + "," + a[6] + "," + a[7];
                TestOcr2 t2 = new TestOcr2();
                t2.FloorIdentification(image, c, d, currentDigital);
                if ((int)c[13] != previousFloor)
                {
                    previousFloor = (int)c[13];
                    currentFloorsSustainedFramNum = 0;
                    closedDoorStatussSustainedFramNum = 0;
                }
                {
                    currentFloorsSustainedFramNum++;
                }
                if (c[13] != -10) doorInformation += "，  当前楼层:" + c[13];
                if (c[14] == -1) doorInformation += ",   电梯向下运行";
                else if (c[14] == 1) doorInformation += ",   电梯向上运行";
                if (sum == 30)
                {
                    //放大视频图像
                    float scale = 0.5f;
                    float width = image.Width;
                    float height = image.Height;
                    Cv2.Resize(image, image, new OpenCvSharp.Size(width * scale, height * scale));
                    // Cv2.ImShow(" video", image);
                    BaiduAl baiduAl = new BaiduAl();
                    String[] bodyNum = { "", "", "0" };
                    bodyNum = baiduAl.BodyNum(bodyNum, image);
                    sum = 0;
                    Console.WriteLine(bodyNum[2] + "人");
                    Thread.Sleep(1500);
                    numberByLadder = Convert.ToInt32(bodyNum[2]);
                    if (numberByLadder == 0) thereArePeopleSustainedFramNum = 0;
                    else thereArePeopleSustainedFramNum++;
                }
                sum++;
                if (thereArePeopleSustainedFramNum > 5 && currentFloorsSustainedFramNum > 1000 && closedDoorStatussSustainedFramNum > 900) doorInformation += ",  电梯疑似困人！";
                else doorInformation += ",  电梯运行正常！";
                Console.WriteLine(doorInformation);
                DateTime afterDT = System.DateTime.Now;
                TimeSpan ts = afterDT.Subtract(beforDT);
                Console.WriteLine("DateTime总共花费{0}ms.", ts.TotalMilliseconds);
                //检测输出电梯的状态
            }
        }
        public float[] objectsTracking(Mat video, float[] a, float[] b)
        {
            Rect rect = new Rect((int)b[0], (int)b[1], (int)(b[2] - b[0]), (int)(b[3] - b[1]));
            //Rect roi = new Rect(400, 300, 400, 400);//首先要用个rect确定我们的兴趣区域在哪
            video = new Mat(video, rect);
            //调整数字图片的角度
            OpenCvSharp.Point center = new OpenCvSharp.Point(video.Width / 2.0, video.Height / 2.0);
            Mat affineTrans = Cv2.GetRotationMatrix2D(center, -2, 1.0);
            Cv2.WarpAffine(video, video, affineTrans, video.Size(),
                InterpolationFlags.Nearest);
            Mat dst = new Mat();
            Mat dealvideo = new Mat();
            Mat gray = new Mat();
            Mat threshold = new Mat();
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
            int Maxx = (int)a[9];//门检测最右边框
            int Minx = (int)a[10];//门检测最左边框
            int doorLength = (int)a[11];//最小门缝长度
            int kaiAndGuanZhenShuLeiJi = (int)a[12];
            int Dilate = (int)a[13];//膨胀
            int Erode = (int)a[14];//腐蚀
            Cv2.CvtColor(video, gray, ColorConversionCodes.RGB2GRAY, 0);
            Cv2.Sobel(gray, threshold, MatType.CV_32F, 1, 0, 3);
            Cv2.ConvertScaleAbs(threshold, threshold);
            Mat ximage = new Mat();
            Cv2.Normalize(threshold, threshold, 0, 255, NormTypes.MinMax);
            threshold.ConvertTo(ximage, MatType.CV_8UC1);
            Mat knenel = Cv2.GetStructuringElement(MorphShapes.Ellipse, new OpenCvSharp.Size(3, 3));
            Mat knene2 = Cv2.GetStructuringElement(MorphShapes.Ellipse, new OpenCvSharp.Size(3, 3));
            Cv2.Dilate(ximage, ximage, knenel, new OpenCvSharp.Point(0, 0), Dilate);//膨胀4
            Cv2.Erode(ximage, ximage, knene2, new OpenCvSharp.Point(0, 0), Erode);//腐蚀3
            Cv2.Threshold(ximage, dealvideo, 0, 255, ThresholdTypes.Binary | ThresholdTypes.Otsu);
            List<Rect> rects = process(dealvideo, doorLength);
            Cv2.ImShow("dealvideo", dealvideo);
            if (a[1] == frame)
            {
                a[1] = 0;
                int oneFrameDoorPosition = -1;
                int num = 0;
                int M;
                if (j == 1) M = Maxx;//此处Min意味着最靠近左门框
                else M = Minx;//此处Min意味着最靠近右门框
                //注意修改
                //int temp1=0;
                for (int i = 0; i < rects.Count; i++)
                {
                    if (j == 1)
                    {
                        if ((rects[i].X < Maxx) && (rects[i].X > Minx))
                        {
                            num++;
                            oneFrameDoorPosition = rects[i].X;
                            if (oneFrameDoorPosition < M)
                            {
                                M = oneFrameDoorPosition;
                            }
                        }
                    }
                    else if (j == 0)
                    {
                        if ((rects[i].X < Maxx) && (rects[i].X > Minx))
                        {
                            num++;
                            oneFrameDoorPosition = rects[i].X;
                            if (oneFrameDoorPosition > M) M = oneFrameDoorPosition;
                        }
                    }
                }
                if (num != 0)
                {
                    if (j == 1) oneFrameDoorPosition = M;
                    else if (j == 0) oneFrameDoorPosition = M;
                    if (oneFrameDoorPosition != 0 && oneFrameDoorPosition != -1)
                    {
                        if (temp == 0) temp = oneFrameDoorPosition;
                        if (temp > oneFrameDoorPosition)
                        {
                            Increment++;
                            if (Increment >= kaiAndGuanZhenShuLeiJi)
                            {
                                Increment = kaiAndGuanZhenShuLeiJi;
                                Diminishing = 0;
                                doorStill = 0;
                            }
                            if (Diminishing > 0) Diminishing--;
                            if (doorStill > 0) doorStill--;
                        }
                        else if (temp < oneFrameDoorPosition)
                        {
                            Diminishing++;
                            if (Diminishing >= kaiAndGuanZhenShuLeiJi)
                            {
                                Diminishing = kaiAndGuanZhenShuLeiJi;
                                Increment = 0;
                                doorStill = 0;
                            }
                            if (Increment > 0) Increment--;
                            if (doorStill > 0) doorStill--;
                        }
                        else
                        {
                            doorStill++;
                            if (doorStill >= kaiAndGuanZhenShuLeiJi) doorStill = kaiAndGuanZhenShuLeiJi;
                            if (Increment > 0) Increment--;
                            if (Diminishing > 0) Diminishing--;
                        }
                        if (j == 1)
                        {
                            if (Increment == kaiAndGuanZhenShuLeiJi)
                            {
                                Console.WriteLine("!!!!!!!!!!!!!开门的相对速度为!!!!!!!!!!!!" +
                                                 (temp - oneFrameDoorPosition));
                                kai++;
                                if (guan > 0) guan--;
                            }
                            if (Diminishing == kaiAndGuanZhenShuLeiJi)
                            {
                                Console.WriteLine("!!!!!!!!!!!关门的相对速度为!!!!!!!!!!" +
                                                 (oneFrameDoorPosition - temp));
                                guan++;
                                if (kai > 0) kai--;
                            }
                        }
                        else if (j == 0)
                        {
                            if (Increment == kaiAndGuanZhenShuLeiJi)
                            {
                                System.Console.WriteLine("!!!!!!!!!!!!关门的相对速度为!!!!!!!!!!!!!!" + (temp - oneFrameDoorPosition));
                                guan++;
                                if (kai > 0) kai--;
                            }
                            if (Diminishing == kaiAndGuanZhenShuLeiJi)
                            {
                                System.Console.WriteLine("!!!!!!!!!!!!!开门的相对速度为!!!!!!!!!!!!!!" + (oneFrameDoorPosition - temp));
                                kai++;
                                if (guan > 0) guan--;
                            }
                        }
                        temp = oneFrameDoorPosition;
                    }
                }
            }
            a[1]++;
            video.Release();
            dealvideo.Release();
            dst.Release();
            threshold.Release();
            ximage.Release();
            if (kai > kaiAndGuanZhenShuLeiJi + 1) kai = kaiAndGuanZhenShuLeiJi + 1;
            if (guan > kaiAndGuanZhenShuLeiJi + 1) guan = kaiAndGuanZhenShuLeiJi + 1;
            a[0] = temp;
            a[2] = Increment;
            a[3] = Diminishing;
            a[4] = doorStill;
            a[6] = kai;
            a[7] = guan;
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
                    if (length > doorLength)
                    {
                        y++;
                        if (y > 1)
                        {
                            rect = Cv2.BoundingRect(contours[i]);
                            rects.Add(rect);
                        }
                    }
                }
            }
            else
            {
                rect = new Rect();
                rect.X = rect.Y = rect.Width = rect.Height = 0;
                rects.Add(rect);
            }
            return rects;
        }
    }
}