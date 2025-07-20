using System;
using System.Collections.Generic;
using OpenCvSharp;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace elevatorAlarm
{
    class AlarmSystem2
    {

      /*  static void Main(string[] args)
        {
            String filename1 = "D:\\0video\\people\\Modeling\\1.mp4";
            String filenameAlarm = "D:\\0video\\floor\\6.mp4"; //String filenameAlarm="D:\\0video\\long.mp4";
            //String filenameAlarm = "D:\\0video\\video\\JavaWebTest.mp4";

            HashSet<Coordinate> backgroundModele;
            float[] a = new float[] { 0, 0, 0, 0, 0, 2, 0, 0, 1, 245, 75, 400 };//int[] a=new int[]{0,0,0,0,0,3,0,0,1,88,5,90};
            float[] b = new float[] { 0, 0, 510, 460 };//int[] b=new int[]{265,16,285,114};
            float[] c = new float[] { 30, 0, 0, 185, 130, 115, 255, 50, 1, 2, 2, 7, -1, -10, -10, 0, 3 };
            float[] d = new float[] { 193, 19, 300, 75 };
            float[] e = new float[] { 80, 240, 50, 0 };
            float[] currentDigital = new float[] { 0, 0, 0 };
            AlarmSystem2 AS2 = new AlarmSystem2();
            CannyBackgroundModeling2 CBM = new CannyBackgroundModeling2();
            backgroundModele = CBM.BackgroundModeling(filename1, e);
            string Secret = "@g5#*$y980{_";
            AS2.alarm3(filenameAlarm, Secret, backgroundModele, a, b, c, d, e, currentDigital);
        }*/


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
            Mat video = (Mat)obb[0];
            HashSet<Coordinate> backgroundModele1 = (HashSet<Coordinate>)obb[1];
            float[] e = (float[])obb[2];
            if (!video.Empty())
            {
                CannyBackgroundDetection2 CBD = new CannyBackgroundDetection2();
                CBD.Detection(video, backgroundModele1, e);
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
            if (!video.Empty())
            {
                TestOcr2 ocr = new TestOcr2();
                ocr.FloorIdentification(video, c, d,currentDigital);
            }
        }


        public void alarm3(String filename, String Secret, HashSet<Coordinate> backgroundModele, float[] a, float[] b, float[] c, float[] d, float[] e, float[] currentDigital)
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
                        ThreadPool.SetMaxThreads(7,7);
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
                        object[] ob3 = new object[4] { video, c, d , currentDigital};

                        //门检测
                        ThreadPool.QueueUserWorkItem(runObjectTracking2, ob1);
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
                            ThreadPool.QueueUserWorkItem(runTestOcr2, ob3);
                            //Thread threadOcr3 = new Thread(new MyRunnableOcr3(src3, c, d).run);                            
                            //threadOcr3.Start();

                            //Thread threadOcr3 = new Thread(Ocr3.run);
                            // executorPool.execute(new MyRunnableOcr3(src3, c, d));//楼层检测

                            if (c[13] != -10)
                            {//当前楼层信息
                                Console.WriteLine("当前楼层信息#######################" + (int)c[13]);
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
                            if (c[13] != -10 && temp == -10)
                            {//当前楼层信息
                                temp = c[13];//c[1]为当前楼层信息
                            }
                            else if (c[13] != -10 && temp != -10)
                            {
                                if (temp != c[13])
                                {//当前楼层与上一帧楼层相同,楼层计数归零
                                    temp = c[13];
                                    doorNum = 0;
                                }
                                else if (temp == c[13])
                                {//当前楼层与上一帧楼层相同，楼层计数加一
                                    if (doorNum < 520)//505
                                        doorNum++;
                                }
                            }
                            else if (c[13] == -10)
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



/*

        //背景减除，多线程实现//Thread threadCannyBackground = new Thread(new MyRunnableCannyBackground3(video, backgroundModele1, e).run);
        class MyRunnableCannyBackground3
        {
            private Mat video;
            private HashSet<Coordinate> backgroundModele1;
            private float[] e;


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

            private Mat video;
            private float[] a;
            private float[] b;

            *//*  public void run(Mat video, float[] a, float[] b)
              {
                  if (!video.Empty())
                  {
                      ObjectTracking2 o1 = new ObjectTracking2();//new ObjectTracking2().objectsTracking(video, a, b)
                      o1.objectsTracking(video, a, b);
                  }
              }*//*

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
        *//* Thread threadOcr3 = new Thread(new MyRunnableOcr3(video, c, d).run);
         threadOcr3.Start();*//*
        class MyRunnableOcr3
        {
            private Mat video;
            private float[] c;
            private float[] d;


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
        }*/


    }
}
