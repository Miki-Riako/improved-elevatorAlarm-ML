using System;
using System.Collections.Generic;
using OpenCvSharp;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace elevatorAlarm
{
    class AlarmSystem3
    {
      /*  static void Main(string[] args)
        {
            String filename1 = "D:\\0video\\video\\Modeling2.mp4";
            String filenameAlarm = "D:\\0video\\video\\2.mp4";//String filenameAlarm="D:\\0video\\long.mp4";
                                                              //String filenameAlarm = "D:\\0video\\video\\JavaWebTest.mp4";

            HashSet<Coordinate> backgroundModele;
            float[] a = new float[] { 0, 0, 0, 0, 0, 3, 0, 0, 1, 280, 131, 50 };
            float[] b = new float[] { 130, 110, 347, 180 };

            float[] c = new float[] { -10, -10, -10, 10, 3, 130, 130, 130, 255, 255, 255, 4, 2, 23, 0.5f, 300, 80, 40 };//(楼层识别）//缩放是小数时加f，例如1.5f
                                                                                                                        //c[0]=1;//判定一个箭头一个数字,仅在有两个字符时起作用;c[0]=-10抛异常；c[0]=0:说明读取的是一个或三个字符
                                                                                                                        //c[1];//c[1]!=-10时返回楼层数字信息
                                                                                                                        //c[2]!=-10,c[1]=正负1时判定楼层箭头方向（
            float[] d = new float[] { 516, 155, 561, 187, 0, 0, 330, 196 };//(楼层识别）* d[0]:为x坐标* d[1]:为y坐标* d[2]:图像x1* d[3]:图像y1
            float[] e = new float[] { 50, 150, 30, 0 };//e[0]canny下阈值50；e[1]canny上阈值150，e[2]:检测线段长度30(人检测)e[3]:记录载人阈值,初始化为0

            float[] f = new float[] { -10, 0, 0, 0, 0, 10, 500, 20, 500, 10, 1718, 1730, 8 };


            string[] bodyNum = new string[] { "", "", "-1","1000" };
            //bodyNum[0]:String api_key= "";
            //bodyNum[1]:String secret_key= "";
            //bodyNum[2]:统计的人数
            //bodyNum[3]:困人时1000帧调用一次
            string[] baiduAl = new string[] { };
            AlarmSystem3 AS3 = new AlarmSystem3();
            CannyBackgroundModeling2 CBM = new CannyBackgroundModeling2();
            backgroundModele = CBM.BackgroundModeling(filename1, e);
            string Secret = "@g5#*$y980{_";
            AS3.alarm3_Frame(filenameAlarm, Secret, backgroundModele, a, b, c, d, e, f, bodyNum);
            //AS3.alarm3(filenameAlarm, Secret, backgroundModele, a, b, c, d, e);
        }
*/

        //门
        public void runObjectTracking2(object ob)//Mat video, float[] a, float[] b
        {
            object[] obb = (object[])ob;
            Mat video = (Mat)obb[0];
            float[] a = (float[])obb[1];
            float[] b = (float[])obb[2];
            if (!video.Empty())
            {
                ObjectTracking2 o1 = new ObjectTracking2();//new ObjectTracking2().objectsTracking(video, a, b)
                o1.objectsTracking(video, a, b);
            }
        }

        //人
        public void runCannyBackgroundDetection2(object ob)//Mat video, HashSet<Coordinate> backgroundModele1, float[] e
        {
            object[] obb = (object[])ob;
            Mat video= (Mat)obb[0];
            HashSet<Coordinate> backgroundModele1= (HashSet<Coordinate>)obb[1];
            float[] e= (float[])obb[2];
            if (!video.Empty())
            {
                CannyBackgroundDetection2 CBD = new CannyBackgroundDetection2();
                CBD.Detection(video, backgroundModele1, e);
            }
        }
        //楼层信息
        public void runNixieTubeIdentification2(object ob)//Mat video, float[] c, float[] d
        {
            object[] obb = (object[])ob;
            Mat video = (Mat)obb[0];
            float[] c = (float[])obb[1];
            float[] d = (float[])obb[2];
            if (!video.Empty())
            {
                NixieTubeIdentification2 nix = new NixieTubeIdentification2();
                nix.NixieTube(video, c, d);
            }
        }







        public void alarm3_Frame(String filename, String Secret, HashSet<Coordinate> backgroundModele, float[] a, float[] b, float[] c, float[] d, float[] e, float[] f, string[] bodyNum)
        {
            // 创建 VideoCapture 对象
            VideoCapture capture = new VideoCapture();
            // 使用 VideoCapture 对象读取本地视频
            capture.Open(filename);
            //使用 Mat video 保存视频中的图像帧 针对每一帧 做处理
            Mat video = new Mat();
            Mat src = video.Clone();

            //以下为默认值
            float temp = f[0];
            int doorNum = (int)f[1];//关门状态计数,关门状态持续帧数
            int peopleNumFlag = (int)f[2];//载人状态计数，载人状态持续帧数
            int ModelingFlag = (int)f[3];//背景已更新标志位
            int ModelingNum = (int)f[4];//背景更新帧数
            //以下为调试参数
            int detectionSettingValue = (int)f[5];//载人检测设定的阈值
            int ModelingNumSettingValueHeight = (int)f[6];//背景更新帧数设定值
            int ModelingNumSettingValueLow = (int)f[7];//背景更新帧数设定值
            int doorNumSettingValue = (int)f[8];//关门状态计数设定值
            int peopleNumFlagSettingValue = (int)f[9];//载人状态计数设定值
            int ModelingStartTime = (int)f[10];
            int ModelingEndTime = (int)f[11];
            int MannedThreshold = (int)f[12];//困人阈值，用于背景建模
            /*
             //以下为默认值
            float temp = -10;
            int doorNum = 0;//关门状态计数,关门状态持续帧数
            int peopleNumFlag = 0;//载人状态计数，载人状态持续帧数
            int ModelingFlag = 0;//背景已更新标志位
            int ModelingNum = 0;//背景更新帧数
            //以下为调试参数
            int detectionSettingValue = 10;//载人检测设定的阈值
            int ModelingNumSettingValueHeight = 500;//背景更新帧数设定值
            int ModelingNumSettingValueLow = 20;//背景更新帧数设定值
            int doorNumSettingValue = 500;//关门状态计数设定值
            int peopleNumFlagSettingValue = 10;//载人状态计数设定值
            int ModelingStartTime = 1718;
            int ModelingEndTime = 1730;
            int MannedThreshold = 8;//困人阈值，用于背景建模
            */

            HashSet<Coordinate> backgroundModeleTemp = new HashSet<Coordinate>();
            Coordinate coordinate = new Coordinate(0, 0);
            backgroundModeleTemp.Add(coordinate);//初始化背景更新临时存放集合

            BaiduAl baiduAl = new BaiduAl();//调用百度的人数统计
            int baiduAlFrameInterval = int.Parse(bodyNum[3]);

            while (capture.Read(video))
            {

                //北京时间查询函数
                DateTime currentTime = new DateTime();
                currentTime = System.DateTime.Now;
                int timeInt1 = currentTime.Day + currentTime.Month * 100 + currentTime.Year * 10000;
                if (timeInt1 < 20210909 && Secret == "@g5#*$y980{_")
                {
                    if (video.Empty())
                    {
                        Console.WriteLine("未读取到图片");
                    }
                    else
                    {
                        ThreadPool.SetMaxThreads(7, 7);

                        //门
                        object[] ob1 = new object[3] { video, a, b };
                        //人
                        object[] ob2 = new object[3] { video, backgroundModele, e };
                        //楼层检测
                        object[] ob3 = new object[3] { video, c, d };

                        //门检测
                        ThreadPool.QueueUserWorkItem(runObjectTracking2, ob1);
                        int timeInt = currentTime.Hour * 100 + currentTime.Minute;

                        //为下次建模开启允许标志位
                        if (timeInt > ModelingEndTime)
                        {
                            ModelingFlag = 0;
                        }

                        //建模载人阈值取略小于载人检测阈值//doorNum>=500//e[3]为载人检测的阈值//轿厢关门状态累积帧数，a[7]为guan
                        if (e[3] < MannedThreshold && ModelingFlag == 0 && a[7] == 4 && (timeInt > ModelingStartTime && timeInt < ModelingEndTime))
                        {

                            if (ModelingNum >= ModelingNumSettingValueLow && ModelingNum <= ModelingNumSettingValueHeight)
                            {
                                CannyBackgroundModeling2 CBM = new CannyBackgroundModeling2();
                                CBM.Modeling(src, backgroundModeleTemp, e);
                                Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!背景更新中");
                            }
                            if (ModelingNum == ModelingNumSettingValueHeight)
                            {
                                if (backgroundModeleTemp != null)
                                {
                                    backgroundModele.Clear();
                                    backgroundModele.UnionWith(backgroundModeleTemp);//模型更新!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                                    ModelingFlag = 1;
                                    for (int beijing = 0; beijing < 100; beijing++)
                                        Console.WriteLine("已完成背景更新&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
                                }
                            }

                            ModelingNum++;//背景更新帧数累加


                            //载人检测
                            ThreadPool.QueueUserWorkItem(runCannyBackgroundDetection2, ob2);
                        }
                        else
                        {
                            ModelingNum = 0;//背景更新帧数清零

                            if (a[7] >= 3)
                            {//轿厢关门状态累积帧数，a[7]为guan
                             //载人检测
                                ThreadPool.QueueUserWorkItem(runCannyBackgroundDetection2, ob2);
                                if (e[3] > detectionSettingValue)
                                {//e[3]为载人检测的阈值
                                    if (peopleNumFlag < peopleNumFlagSettingValue + 6)
                                    {
                                        peopleNumFlag++;
                                    }
                                    Console.WriteLine("载人检测阈值***" + e[3]);//e[3]为载人检测的阈值
                                }
                                else if (e[3] < detectionSettingValue)
                                {//e[3]为载人检测的阈值
                                    if (peopleNumFlag > peopleNumFlagSettingValue)
                                    {
                                        peopleNumFlag--;
                                    }
                                    else if (peopleNumFlag < peopleNumFlagSettingValue && peopleNumFlag > 0)
                                    {
                                        peopleNumFlag = 0;
                                    }
                                }
                            }
                            else
                            {
                                peopleNumFlag = 0;
                            }

                            //楼层检测
                            ThreadPool.QueueUserWorkItem(runNixieTubeIdentification2, ob3);
                            if (c[1] != -10)
                            {//当前楼层信息
                                Console.WriteLine("当前楼层信息#######################" + (int)c[1]);
                            }

                            //开门时，关门状态计数num清零
                            if (a[6] >= 2)
                            {//a[6]为kai,轿厢开门状态累积帧数
                                doorNum = 0;
                            }
                            if (peopleNumFlag == 0)
                            {//当轿厢中没有人时，楼层计数减二
                                if (doorNum > 2)
                                {
                                    doorNum -= 2;
                                }
                            }
                            //楼层连续多帧不变
                            if (c[1] != -10 && temp == -10)
                            {//当前楼层信息
                                temp = c[1];//c[1]为当前楼层信息
                            }
                            else if (c[1] != -10 && temp != -10)
                            {
                                if (temp != c[1])
                                {//当前楼层与上一帧楼层相同,楼层计数归零
                                    temp = c[1];
                                    doorNum = 0;
                                }
                                else if (temp == c[1])
                                {//当前楼层与上一帧楼层相同，楼层计数加一
                                    if (doorNum < doorNumSettingValue + 20)//520
                                        doorNum++;
                                }
                            }
                            else if (c[1] == -10)
                            {//未识别楼层时，楼层不变计数减一
                                if (doorNum > 0)
                                {
                                    doorNum--;
                                }
                            }




                            //困人判定
                            if (peopleNumFlag >= peopleNumFlagSettingValue && a[7] == 4 && doorNum >= doorNumSettingValue)
                            {//500
                                Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@疑似困人警报");
                                if (baiduAlFrameInterval >= 1000)//调用百度AL的频率，1000帧调一次
                                {
                                    baiduAl.BodyNum(bodyNum, video);
                                }
                                if (baiduAlFrameInterval > 0)
                                {
                                    baiduAlFrameInterval--;
                                }
                                else
                                {
                                    baiduAlFrameInterval = 1000;
                                }
                            }
                            else 
                            {
                                baiduAlFrameInterval = 1000;
                            }
                            bodyNum[3]=baiduAlFrameInterval.ToString();

                        }
                        Cv2.ImShow(" video", video);
                         Cv2.WaitKey(10);
                    }
                }
            }
        }




        //未用
        public void alarm3(String filename, String Secret, HashSet<Coordinate> backgroundModele, float[] a, float[] b, float[] c, float[] d, float[] e)
        {
            // 创建 VideoCapture 对象
            VideoCapture capture = new VideoCapture();
            // 使用 VideoCapture 对象读取本地视频
            capture.Open(filename);
            //使用 Mat video 保存视频中的图像帧 针对每一帧 做处理
            Mat video = new Mat();
            Mat src1 = null;
            Mat src2 = null;
            Mat src3 = null;
            Mat src4 = null;


            float temp = -10;
            int doorNum = 0;//关门状态计数,关门状态持续帧数
            int peopleNumFlag = 0;//载人状态计数，载人状态持续帧数
            int ModelingFlag = 0;//背景已更新标志位
            int ModelingNum = 0;//背景更新帧数

            HashSet<Coordinate> backgroundModeleTemp = new HashSet<Coordinate>();
            Coordinate coordinate = new Coordinate(0, 0);
            backgroundModeleTemp.Add(coordinate);//初始化背景更新临时存放集合


            /*MyRunnableObjectTracking3 ObjectTracking3 = new MyRunnableObjectTracking3(src1, a, b);
            MyRunnableCannyBackground3 CannyBackground3 = new MyRunnableCannyBackground3(src2, backgroundModele, e);
            MyRunnableOcr3 Ocr3 = new MyRunnableOcr3(src3, c, d);*/

            while (capture.Read(video))
            {
                /*src1 = video.Clone();
                src2 = video.Clone();
                src3 = video.Clone();
                src4 = video.Clone();*/
                //北京时间查询函数
                DateTime currentTime = new DateTime();
                currentTime = System.DateTime.Now;
                int timeInt1 = currentTime.Day + currentTime.Month * 100 + currentTime.Year * 10000;
                if (timeInt1 < 20220909 && Secret == "@g5#*$y980{_")
                {
                    if (video.Empty())
                    {
                        Console.WriteLine("未读取到图片");
                    }
                    else
                    {
                        ThreadPool.SetMaxThreads(10,10);
                        // ExecutorService executorPool = Executors.newFixedThreadPool(4);
                        //ThreadPool.SetMaxThreads(int workerThreads, int completionPortThreads)
                        // ThreadPool.SetMaxThreads(4, 4);
                        /*src1 = video.Clone();
                        src2 = video.Clone();
                        src3 = video.Clone();*/
                        //src4 = video.Clone();
                        //门
                        object[] ob1 = new object[3] { video, a, b };
                        //人
                        object[] ob2 = new object[3] { video, backgroundModele, e };
                        //楼层检测
                        object[] ob3 = new object[3] { video, c, d };

                        //门检测
                        ThreadPool.QueueUserWorkItem(runObjectTracking2,ob1);
                        //Thread threadObjectTracking = new Thread(new MyRunnableObjectTracking3(src1, a, b).run);
                        //threadObjectTracking.Start();


                        //executorPool.execute(new MyRunnableObjectTracking3(src1, a, b));//门检测
                        // ThreadPool.QueueUserWorkItem(new ObjectTracking2().objectsTracking, video, a, b);
                        /*Thread thread = new Thread(new ParameterizedThreadStart(new ObjectTracking2().objectsTracking));//创建线程

                        thread.Start(video, a, b); */                                                                            //启动线程
                        //北京时间查询函数
                        /*Calendar calendar = Calendar.getInstance(Locale.CHINA);
                        Date time = calendar.getTime();
                        // SimpleDateFormat data = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
                        SimpleDateFormat data = new SimpleDateFormat("HHmm");
                        String format = data.format(time);
                        int timeInt = Integer.parseInt(format);//得到系统的时间*/
                        int timeInt = currentTime.Hour * 100 + currentTime.Minute;

                        //为下次建模开启允许标志位
                        if (timeInt > 1730)
                        {
                            ModelingFlag = 0;
                        }

                        //建模载人阈值取略小于载人检测阈值//doorNum>=500//e[3]为载人检测的阈值//轿厢关门状态累积帧数，a[7]为guan
                        if (e[3] < 8 && ModelingFlag == 0 && a[7] == 4 && (timeInt > 1718 && timeInt < 1730))
                        {

                            if (ModelingNum >= 20 && ModelingNum <= 500)
                            {
                                CannyBackgroundModeling2 CBM = new CannyBackgroundModeling2();
                                CBM.Modeling(src4, backgroundModeleTemp, e);
                                Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!背景更新中");
                            }
                            if (ModelingNum == 500)
                            {
                                if (backgroundModeleTemp != null)
                                {
                                    backgroundModele.Clear();
                                    backgroundModele.UnionWith(backgroundModeleTemp);//模型更新!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                                    ModelingFlag = 1;
                                    for (int beijing = 0; beijing < 100; beijing++)
                                        Console.WriteLine("已完成背景更新&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&");
                                }
                            }

                            ModelingNum++;//背景更新帧数累加


                            //载人检测
                            ThreadPool.QueueUserWorkItem(runCannyBackgroundDetection2, ob2);
                            //Thread threadCannyBackground = new Thread(new MyRunnableCannyBackground3(src2, backgroundModele, e).run);                           
                            //threadCannyBackground.Start();



                            //Thread threadCannyBackground = new Thread(CannyBackground3.run);
                            //executorPool.execute(new MyRunnableCannyBackground3(src2, backgroundModele, e));//载人检测

                        }
                        else
                        {
                            ModelingNum = 0;//背景更新帧数清零

                            if (a[7] >= 3)
                            {//轿厢关门状态累积帧数，a[7]为guan
                             //载人检测
                                ThreadPool.QueueUserWorkItem(runCannyBackgroundDetection2, ob2);
                               // Thread threadCannyBackground = new Thread(new MyRunnableCannyBackground3(src2, backgroundModele, e).run);                          
                               // threadCannyBackground.Start();


                                //Thread threadCannyBackground = new Thread(CannyBackground3.run);
                                //executorPool.execute(new MyRunnableCannyBackground3(src2, backgroundModele, e));//载人检测
                                if (e[3] > 10)
                                {//e[3]为载人检测的阈值
                                    if (peopleNumFlag < 20)
                                    {
                                        peopleNumFlag++;
                                    }
                                    Console.WriteLine("载人检测阈值***" + e[3]);//e[3]为载人检测的阈值
                                }
                                else if (e[3] < 10)
                                {//e[3]为载人检测的阈值
                                    if (peopleNumFlag > 10)
                                    {
                                        peopleNumFlag--;
                                    }
                                    else if (peopleNumFlag < 10 && peopleNumFlag > 0)
                                    {
                                        peopleNumFlag = 0;
                                    }
                                }
                            }
                            else
                            {
                                peopleNumFlag = 0;
                            }

                            //楼层检测
                            ThreadPool.QueueUserWorkItem(runNixieTubeIdentification2, ob3);
                            //Thread threadOcr3 = new Thread(new MyRunnableOcr3(src3, c, d).run);                            
                            //threadOcr3.Start();

                            //Thread threadOcr3 = new Thread(Ocr3.run);
                            // executorPool.execute(new MyRunnableOcr3(src3, c, d));//楼层检测

                            if (c[1] != -10)
                            {//当前楼层信息
                                Console.WriteLine("当前楼层信息#######################" + (int)c[1]);
                            }

                            //开门时，关门状态计数num清零
                            if (a[6] >= 2)
                            {//a[6]为kai,轿厢开门状态累积帧数
                                doorNum = 0;
                            }
                            if (peopleNumFlag == 0)
                            {//当轿厢中没有人时，楼层计数减二
                                if (doorNum > 2)
                                {
                                    doorNum -= 2;
                                }
                            }
                            //楼层连续多帧不变
                            if (c[1] != -10 && temp == -10)
                            {//当前楼层信息
                                temp = c[1];//c[1]为当前楼层信息
                            }
                            else if (c[1] != -10 && temp != -10)
                            {
                                if (temp != c[1])
                                {//当前楼层与上一帧楼层相同,楼层计数归零
                                    temp = c[1];
                                    doorNum = 0;
                                }
                                else if (temp == c[1])
                                {//当前楼层与上一帧楼层相同，楼层计数加一
                                    if (doorNum < 520)//505
                                        doorNum++;
                                }
                            }
                            else if (c[1] == -10)
                            {//未识别楼层时，楼层不变计数减一
                                if (doorNum > 0)
                                {
                                    doorNum--;
                                }
                            }


                            //困人判定
                            if (peopleNumFlag >= 10 && a[7] == 4 && doorNum >= 500)
                            {//500
                               Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@疑似困人警报");
                            }
                            //System.out.println("当前帧程序处理时间："+(System.currentTimeMillis() - startTime)+"ms");
                        }
                        //executorPool.shutdown();
                        Cv2.ImShow(" video", video);
                        Cv2.WaitKey(10);
                    }
                }
               
                /*src1.Release();
                src2.Release();
                src3.Release();
                src4.Release();*/
            }
        }





        //背景减除，多线程实现//Thread threadCannyBackground = new Thread(new MyRunnableCannyBackground3(video, backgroundModele1, e).run);
        class MyRunnableCannyBackground3 
        {
            private  Mat video;
            private  HashSet<Coordinate> backgroundModele1;
            private  float[] e;


            public MyRunnableCannyBackground3(Mat video, HashSet<Coordinate> backgroundModele1, float[] e)
            {
                this.video = video;
                this.backgroundModele1 = backgroundModele1;
                this.e = e;
            }

            public void run(Mat video, HashSet<Coordinate> backgroundModele1, float[] e)
            {
                if (!video.Empty())
                {
                    CannyBackgroundDetection2 CBD = new CannyBackgroundDetection2();
                    CBD.Detection(video, backgroundModele1, e);
                }
            }

            public void run()
            {
                if (!video.Empty())
                {
                    CannyBackgroundDetection2 CBD = new CannyBackgroundDetection2();
                    CBD.Detection(video, backgroundModele1, e);
                }
            }
        }


        //门检测，多线程实现 //Thread threadObjectTracking = new Thread(new MyRunnableObjectTracking3(video, a, b).run);
        class MyRunnableObjectTracking3 
    {

            private  Mat video;
            private  float[] a;
            private  float[] b;

          /*  public void run(Mat video, float[] a, float[] b)
            {
                if (!video.Empty())
                {
                    ObjectTracking2 o1 = new ObjectTracking2();//new ObjectTracking2().objectsTracking(video, a, b)
                    o1.objectsTracking(video, a, b);
                }
            }*/

            public void run()
            {
                if (!video.Empty())
                {
                    ObjectTracking2 o1 = new ObjectTracking2();//new ObjectTracking2().objectsTracking(video, a, b)
                    o1.objectsTracking(video, a, b);
                }
            }

            public MyRunnableObjectTracking3(Mat video, float[] a, float[] b)
            {
                this.video = video;
                this.a = a;
                this.b = b;
            }
    }

        //楼层检测，多线程实现
        //楼层检测，多线程实现 //载人检测
        /* Thread threadOcr3 = new Thread(new MyRunnableOcr3(video, c, d).run);
         threadOcr3.Start();*/
        class MyRunnableOcr3 
        {
                private  Mat video;
                private  float[] c;
                private  float[] d;


                public MyRunnableOcr3(Mat video, float[] c, float[] d)
                {
                    this.video = video;

                    this.c = c;
                    this.d = d;

                }

            public void run(Mat video, float[] c, float[] d)
            {
                if (!video.Empty())
                {
                    NixieTubeIdentification2 nix = new NixieTubeIdentification2();
                    nix.NixieTube(video, c, d);
                }
            }

            public void run()
                {
                    if (!video.Empty())
                    {
                        NixieTubeIdentification2 nix = new NixieTubeIdentification2();
                        nix.NixieTube(video, c, d);
                    }
                }
        }





    }
}
