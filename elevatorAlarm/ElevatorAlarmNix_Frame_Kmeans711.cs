using System;
using System.Collections.Generic;
using OpenCvSharp;
using System.Threading;
using elevatorAlarm;

namespace elevatorAlarm
{
   public class ElevatorAlarmNix_Frame_Kmeans711
    {
        /* static void Main(string[] args)
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
             float[] d = new float[] { 516, 155, 561, 187, 0, 0, 330, 190 };//(楼层识别）* d[0]:为x坐标* d[1]:为y坐标* d[2]:图像x1* d[3]:图像y1
             float[] e = new float[] { 50, 150, 30, 0 };//e[0]canny下阈值50；e[1]canny上阈值150，e[2]:检测线段长度30(人检测)e[3]:记录载人阈值,初始化为0

             float[] f = new float[] { -10, 0, 0, 0, 0, 10, 500, 20, 500, 10, 1718, 1730, 8 };
             AlarmSystem3 AS3 = new AlarmSystem3();
             CannyBackgroundModeling2 CBM = new CannyBackgroundModeling2();
             backgroundModele = CBM.BackgroundModeling(filename1, e);
             string Secret = "@g5#*$y980{_";
             AS3.alarm3(filenameAlarm, Secret, backgroundModele, a, b, c, d, e);
         }
 */

       





        //ElevatorAlarmNix_Frame_Kmeans711

        //static void Main(string[] args)
        //{
        //    String filename1 = "D:\\0video\\video\\Modeling2.mp4";
        //    String filenameAlarm = "D:\\0video\\video\\2.mp4";//String filenameAlarm="D:\\0video\\long.mp4";
        //                                                      //String filenameAlarm = "D:\\0video\\video\\JavaWebTest.mp4";

        //    HashSet<Coordinate> backgroundModele;
        //    float[] a = new float[] { 0, 0, 0, 0, 0, 3, 0, 0, 1, 280, 131, 50 };
        //    float[] b = new float[] { 130, 110, 347, 180 };

        //  /*  float[] c = new float[] { -10, -10, -10, 10, 3, 130, 130, 130, 255, 255, 255, 
        //        4, 2, 23, 0.5f, 300, 80, 40 };//(楼层识别）//缩放是小数时加f，例如1.5f*/
        //    float[] c = new float[] { -10, -10, -10, 10, 3, 130, 130, 130, 255, 255, 255,
        //        2, 4, 20, 0.5f, 300, 80, 40 };//(楼层识别）//缩放是小数时加f，例如1.5f                                                                                                           //c[0]=1;//判定一个箭头一个数字,仅在有两个字符时起作用;c[0]=-10抛异常；c[0]=0:说明读取的是一个或三个字符
        //                                      //c[1];//c[1]!=-10时返回楼层数字信息
        //                                      //c[2]!=-10,c[1]=正负1时判定楼层箭头方向（
        //    float[] d = new float[] { 516, 155, 561, 187, 0, 0, 330, 190 };//(楼层识别）* d[0]:为x坐标* d[1]:为y坐标* d[2]:图像x1* d[3]:图像y1
        //    float[] e = new float[] { 50, 150, 30, 0 };//e[0]canny下阈值50；e[1]canny上阈值150，e[2]:检测线段长度30(人检测)e[3]:记录载人阈值,初始化为0

        //    float[] f = new float[] { -10, 0, 0, 0, 0, 10, 500, 20, 500, 10, 1718, 1730, 8 };

        //    string[] bodyNum = new string[] { "", "", "-1", "1000" };
        //    //bodyNum
        //    //bodyNum[0]:String api_key= "";
        //    //bodyNum[1]:String secret_key= "";
        //    //bodyNum[2]:统计的人数-1
        //    //bodyNum[3]:困人时1000帧调用一次

        //    ElevatorAlarmNix_Frame_Kmeans711 AS3 = new ElevatorAlarmNix_Frame_Kmeans711();
        //    CannyBackgroundModeling2 CBM = new CannyBackgroundModeling2();
        //    backgroundModele = CBM.BackgroundModeling(filename1, e);
        //    string Secret = "@g5#*$y980{_";
        //    // AS3.alarm3(filenameAlarm, Secret, backgroundModele, a, b, c, d, e,f);



        //    HashSet<Coordinate> backgroundModeleTemp = new HashSet<Coordinate>();
        //    Coordinate coordinate = new Coordinate(0, 0);
        //    backgroundModeleTemp.Add(coordinate);//初始化背景更新临时存放集合


        //    // 创建 VideoCapture 对象
        //    VideoCapture capture = new VideoCapture();
        //    // 使用 VideoCapture 对象读取本地视频
        //    capture.Open(filenameAlarm);
        //    //使用 Mat video 保存视频中的图像帧 针对每一帧 做处理
        //    Mat video = new Mat();

        //    while (capture.Read(video))
        //    {
        //        AS3.Fun_StraightLadderTrappedPeopleMonitoring_V1(video, backgroundModele, backgroundModeleTemp, a, b, c, d, e, f, bodyNum);
                

                
        //    }
        //}






        //门
        public void runObjectTracking2(object ob)//Mat video, float[] a, float[] b
        {
            object[] obb = (object[])ob;
            Mat video = (Mat)obb[0];
            float[] a = (float[])obb[1];
            float[] b = (float[])obb[2];
            var countdownEvent = (CountdownEvent)obb[3];
            if (!video.Empty())
            {
                ObjectTracking2 o1 = new ObjectTracking2();//new ObjectTracking2().objectsTracking(video, a, b)
                o1.objectsTracking(video, a, b);
            }
            countdownEvent.Signal();
        }

        //人
        public void runCannyBackgroundDetection2(object ob)//Mat video, HashSet<Coordinate> backgroundModele1, float[] e
        {
            object[] obb = (object[])ob;
            Mat video = (Mat)obb[0];
            HashSet<Coordinate> backgroundModele1 = (HashSet<Coordinate>)obb[1];
            float[] e = (float[])obb[2];
            var countdownEvent = (CountdownEvent)obb[3];
            if (!video.Empty())
            {
                CannyBackgroundDetection2 CBD = new CannyBackgroundDetection2();
                CBD.Detection(video, backgroundModele1, e);
            }
            countdownEvent.Signal();
        }
        //楼层信息
        public void runNixieTubeIdentification2(object ob)//Mat video, float[] c, float[] d
        {
            object[] obb = (object[])ob;
            Mat video = (Mat)obb[0];
            float[] c = (float[])obb[1];
            float[] d = (float[])obb[2];
            var countdownEvent = (CountdownEvent)obb[3];
            if (!video.Empty())
            {
                NixieTubeIdentification2_Kmeans nix = new NixieTubeIdentification2_Kmeans();
                nix.NixieTube(video, c, d);
            }
            countdownEvent.Signal();
        }

        public float[] Fun_StraightLadderTrappedPeopleMonitoring_V1(Mat video,
            HashSet<Coordinate> backgroundModele, HashSet<Coordinate> backgroundModeleTemp,
            float[] a, float[] b, float[] c, float[] d, float[] e, float[] f, string[] bodyNum)
        {
            //bodyNum
            //bodyNum[0]:String api_key= "";
            //bodyNum[1]:String secret_key= "";
            //bodyNum[2]:统计的人数
            //bodyNum[3]:困人时1000帧调用一次


            CountdownEvent count = new CountdownEvent(2);//用于线程的同步

            float[] elevatorStatus = new float[6];

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


            int baiduAlFrameInterval = int.Parse(bodyNum[3]);


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
                    ThreadPool.SetMaxThreads(3, 3);

                    Mat video1 = video.Clone();
                    Mat video2 = video.Clone();
                    Mat video3 = video.Clone();
                    //门
                    object[] ob1 = new object[4] { video1, a, b, count };
                    //人
                    object[] ob2 = new object[4] { video2, backgroundModele, e, count };
                    //楼层检测
                    object[] ob3 = new object[4] { video3, c, d, count };

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
                            elevatorStatus[4] = 1;
                            if (baiduAlFrameInterval >= 1000)//调用百度AL的频率，1000帧调一次
                            {
                                new BaiduAl().BodyNum(bodyNum, video);
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
                            elevatorStatus[4] = 0;
                            baiduAlFrameInterval = 1000;
                        }
                        bodyNum[3] = baiduAlFrameInterval.ToString();

                    }
                    

                    //Cv2.ImShow(" video", video);
                    // Cv2.WaitKey(10);
                }
            }
            else
            {
                count.Signal();
                count.Signal();
                count.Signal();
            }
           // count.Signal();
            count.Wait();
            Cv2.ImShow(" video", video);
            Cv2.WaitKey(1);

            elevatorStatus[0] = c[1];//楼层信息，初始化为-10；
            elevatorStatus[1] = c[2];//箭头信息，初始化为-10；c[2]=-10未识别箭头信息；c[2]=1或-1返回箭头信息
            elevatorStatus[2] = a[6] >= 3 ? 1 : 0;//门状态1为开0为关
            elevatorStatus[3] = e[3] > detectionSettingValue ? 1 : 0;//1代表有人，0代表无人
                                                                     //elevatorStatus[4]=1困人，0正常

            elevatorStatus[5] = int.Parse(bodyNum[2]);

            return elevatorStatus;
        }

    }
}
