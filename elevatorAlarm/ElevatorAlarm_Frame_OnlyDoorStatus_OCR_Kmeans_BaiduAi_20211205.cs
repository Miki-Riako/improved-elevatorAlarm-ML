using System;
using OpenCvSharp;
using System.Threading;
using elevatorAlarm;
using System.Collections.Specialized;

namespace elevatorAlarm
{
    public class ElevatorAlarm_Frame_OnlyDoorStatus_OCR_Kmeans_BaiduAi_20211205
    {
        // String filenameAlarm = "D:\\0Aproject\\dongaoVideo\\1125—111610.mp4"
        private static Mat src;

        private static float[] a_jmx = new float[] { 1, 30, 1, 0, 10, 3, 1 };
        private static float[] b_jmx = new float[] { 336, 58, 384, 141 };
        private static float[] c_jmx = new float[] { 30, 0, 0, 185, 130, 115, 255,
                  40,0, 2, 1, 3, -1, -10, -10, 0, 3,7 };

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

        //(楼层识别）//缩放是小数时加f，例如1.5f
        //c[0]=1;//判定一个箭头一个数字,仅在有两个字符时起作用;c[0]=-10抛异常；c[0]=0:说明读取的是一个或三个字符
        //c[1];//c[1]!=-10时返回楼层数字信息
        //c[2]!=-10,c[1]=正负1时判定楼层箭头方向（

        private static float[] d_jmx = new float[] { 818, 105, 878, 140, 842, 143, 867, 176 };//前四个画箭头识别的区域，后四个画数字识别的区域
        private static int pointnum = 0;

        //static void Main(string[] args)
        //{
        //    String filenameAlarm = "E:\\困人智能检测程序和冬奥视频\\延庆冬奥运营中心中区1-能用.mp4";



        //    float[] a = new float[] { 1, 30, 1, 0, 5, 3, 1 };
        //    float[] b = new float[] {60, 23, 128, 51 };


        //    float[] c = new float[] { 30, 0, 0, 185, 130, 115, 255,
        //          40,0, 2, 1, 3, -1, -10, -10, 0, 3,7 };
        //    float[] d = new float[] { 776/4, 90/4, 824/4, 134/4, 783/4, 129/4, 822/4, 171/4 };//前四个画箭头识别的区域，后四个画数字识别的区域jmx
        //    //float[] d = new float[] { 818, 105, 878, 140, 842, 143, 867, 176 };//前四个画箭头识别的区域，后四个画数字识别的区域

        //    float[] currentDigital = new float[] { 0, 0, 0 };
        //    float[] elevatorStatus = new float[] { -10, -10, -1, 0, 0, 0, 30, 30 };
        //    float[] nixie = { -10, 30 };
        //    string[] bodyNum = new string[] { "", "", "0" };
        //    //bodyNum
        //    //bodyNum[0]:String api_key= "";
        //    //bodyNum[1]:String secret_key= "";
        //    //bodyNum[2]:统计的人数-1
        //    //bodyNum[3]:困人时1000帧调用一次


        //    string Secret = "@g5#*$y980{_";

        //    ElevatorAlarm_Frame_OnlyDoorStatus_OCR_Kmeans_BaiduAi_20211205 As = new ElevatorAlarm_Frame_OnlyDoorStatus_OCR_Kmeans_BaiduAi_20211205();

        //    // 创建 VideoCapture 对象
        //    VideoCapture capture = new VideoCapture();
        //    // 使用 VideoCapture 对象读取本地视频
        //    capture.Open(filenameAlarm);
        //    //使用 Mat video 保存视频中的图像帧 针对每一帧 做处理
        //    Mat video = new Mat();

        //    CvMouseCallback GetRGBCvMouseCallback = new CvMouseCallback(draw_circle);



        //    while (capture.Read(video))
        //    {
        //        if (!video.Empty())
        //        {

        //            As.Fun_StraightLadderTrappedPeopleMonitoring_V1(video, a, b, c, d, currentDigital, bodyNum, elevatorStatus, nixie);
        //            //缩放视频图像
        //            float scale1 = 0.5f * 1f;//10意思是放大10倍
        //            float width1 = video.Width;
        //            float height1 = video.Height;
        //            Cv2.Resize(video, video, new OpenCvSharp.Size(width1 * scale1, height1 * scale1));
        //            src = video.Clone();
        //            Cv2.ImShow("video", video);
        //           // Cv2.SetMouseCallback("video", GetRGBCvMouseCallback);

        //            Cv2.WaitKey();


        //        }
        //    }
        //}

        public static void draw_circle(MouseEvent @event, int x, int y, MouseEvent flags, IntPtr userdata)
        {
            Scalar scalar = new Scalar(0, 255, 0);

            if (@event == MouseEvent.LButtonDown)
            {
                Cv2.Circle(src, new Point(x, y), 2, scalar, 1, LineTypes.AntiAlias, 0);//绘制园 参数1:操作图像 2:圆心 3:半径 4:颜色 5:线宽  6:线型  7:缩放参数（0为不缩放）
                Cv2.ImShow("video", src);
                b_jmx[pointnum * 2 + 0] = (float)x;
                b_jmx[pointnum * 2 + 1] = (float)y;
                Console.WriteLine("第" + pointnum + "个点坐标：x=" + b_jmx[pointnum * 2 + 0] + ",y=" + b_jmx[pointnum * 2 + 1]);

                pointnum++;
            }
        }

        //D:\0Aproject\C#\elevatorAlarm20210402\elevatorAlarm\
        //门
        public void runObjectTracking2(object ob)//Mat video, float[] a, float[] b
        {
            object[] obb = (object[])ob;
            Mat video = (Mat)obb[0];
            float[] a = (float[])obb[1];
            float[] b = (float[])obb[2];
            ObjectTracking_OnlyDoorStatus o1 = (ObjectTracking_OnlyDoorStatus)obb[3];
            if (!video.Empty())
            {
                // = new ObjectTracking_OnlyDoorStatus();//new ObjectTracking2().objectsTracking(video, a, b)
                o1.objectsTracking(video, a, b);
                video.Release();
            }

        }



        //人数统计
        public void runBaiduAl(object ob)//Mat video, HashSet<Coordinate> backgroundModele1, float[] e
        {
            object[] obb = (object[])ob;
            Mat video = (Mat)obb[1];
            String[] bodyNum = (String[])obb[0];
            BaiduAl baiduAl = (BaiduAl)obb[2];
            if (!video.Empty())
            {
                //BaiduAl baiduAl = new BaiduAl();
                bodyNum = baiduAl.BodyNum(bodyNum, video);
                video.Release();
            }
        }

        //楼层信息
        public void runTestOcr2(object ob)//Mat video, float[] c, float[] d, float[] currentDigital
        {
            object[] obb = (object[])ob;
            Mat video = (Mat)obb[0];
            float[] c = (float[])obb[1];
            float[] d = (float[])obb[2];
            float[] currentDigital = (float[])obb[3];
            TestOcr2_kmeans ocr = (TestOcr2_kmeans)obb[4];
            float[] nixie = (float[])obb[5];
            if (!video.Empty())
            {
                //TestOcr2_kmeans ocr = new TestOcr2_kmeans();
                ocr.FloorIdentification(video, c, d, currentDigital, nixie);
                video.Release();
            }
        }

        public float[] Fun_StraightLadderTrappedPeopleMonitoring_V1(Mat video,
         float[] a, float[] b, float[] c, float[] d, float[] currentDigital, string[] bodyNum, float[] elevatorStatus, float[] nixie)
        {
            //bodyNum
            //bodyNum[0]:String api_key= "";
            //bodyNum[1]:String secret_key= "";
            //bodyNum[2]:统计的人数
            //bodyNum[3]:困人时1000帧调用一次

            //elevatorStatus[6]：人数统计的进行帧数
            //elevatorStatus[7]：人数统计的目标帧数

            int cur = (int)elevatorStatus[6];

            Mat video1 = video.Clone();
            Mat video2 = video.Clone();
            Mat video3 = video.Clone();

            //创建对象
            ObjectTracking_OnlyDoorStatus o1 = new ObjectTracking_OnlyDoorStatus();
            BaiduAl baiduAl = new BaiduAl();
            TestOcr2_kmeans ocr = new TestOcr2_kmeans();


            //北京时间查询函数
            DateTime currentTime = new DateTime();
            currentTime = System.DateTime.Now;
            int timeInt1 = currentTime.Day + currentTime.Month * 100 + currentTime.Year * 10000;
            if (timeInt1 < 20230101)
            {
                if (video.Empty())
                {
                    Console.WriteLine("未读取到图片");
                }
                else
                {
                    //单线程方式
                    o1.objectsTracking(video1, a, b);
                    cur++;
                    if (cur >= elevatorStatus[7])
                    {

                        bodyNum = baiduAl.BodyNum(bodyNum, video2);
                        Console.WriteLine("检测结果为：" + "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" + bodyNum[2]);
                        cur = 0;
                    }
                    ocr.FloorIdentification(video3, c, d, currentDigital, nixie);

                    //多线程方式
                    /* ThreadPool.SetMaxThreads(3, 3);

                     //门
                     object[] ob1 = new object[4] { video1, a, b,o1 };
                     //人数统计
                     object[] ob2 = new object[3] { bodyNum, video2, baiduAl };
                     //楼层检测
                     object[] ob3 = new object[6] { video3, c, d, currentDigital, ocr ,nixie};

                     //门检测
                    // ThreadPool.QueueUserWorkItem(runObjectTracking2, ob1);

                     cur++;
                     if (cur >= elevatorStatus[7])
                     {
                         //人数统计
                         //ThreadPool.QueueUserWorkItem(runBaiduAl, ob2);
                         Console.WriteLine("检测结果为："+ "@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" + bodyNum[2]);
                         cur = 0;
                     }
 */
                    //楼层检测
                    // ThreadPool.QueueUserWorkItem(runTestOcr2, ob3);
                    //Cv2.ImShow(" video", video);
                    // Cv2.WaitKey(10);
                }
            }
            //门状态
            if (a[3] == 0)
            {
                Console.WriteLine("关门状态");
            }
            else if (a[3] == 1)
            {
                Console.WriteLine("开门状态");
            }

            //当前楼层
            if (c[13] != -10)
            {
                Console.WriteLine("当前楼层：" + c[13]);
            }

            //电梯上下行方向
            if (c[14] == c[12])
            {
                c[14] = -1;
                Console.WriteLine("电梯向下运行");
            }
            else if (c[14] == -c[12])
            {
                c[14] = 1;
                Console.WriteLine("电梯向上运行");
            }

            elevatorStatus[0] = c[13];//楼层信息，初始化为-10；
            elevatorStatus[1] = c[14];//箭头信息，初始化为-10；c[2]=-10未识别箭头信息；c[2]=1或-1返回箭头信息
            elevatorStatus[2] = a[3];//门状态1为开0为关
                                     // elevatorStatus[3] = e[3] > detectionSettingValue ? 1 : 0;//1代表有人，0代表无人
                                     //elevatorStatus[4]=1困人，0正常

            elevatorStatus[5] = int.Parse(bodyNum[2]);//人数
            elevatorStatus[6] = cur;



            return elevatorStatus;
        }




    }
}
