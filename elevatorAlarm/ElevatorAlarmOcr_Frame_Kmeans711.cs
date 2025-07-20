using System;
using System.Collections.Generic;
using OpenCvSharp;
using System.Threading;
using elevatorAlarm;

namespace elevatorAlarm
{
   public class ElevatorAlarmOcr_Frame_Kmeans711
    {


       /* static void Main(string[] args)
        {
            String filename1 = "D:\\0video\\people\\Modeling\\1_1.mp4";
            String filenameAlarm = "D:\\0video\\floor\\6.mp4"; //String filenameAlarm="D:\\0video\\long.mp4";

            HashSet<Coordinate> backgroundModele;

            float[] a = new float[] { 0, 0, 0, 0, 0, 2, 0, 0, 1, 245, 75, 400 };//int[] a=new int[]{0,0,0,0,0,3,0,0,1,88,5,90};
            float[] b = new float[] { 0, 0, 510, 460 };//int[] b=new int[]{265,16,285,114};
            float[] c = new float[] { 30, 0, 0, 185, 130, 115, 255, 50, 1, 2, 2, 7, -1, -10, -10, 0, 3 };
            //float[] d = new float[] { 193, 19, 300, 75 };
            float[] d = new float[] { 156, 27, 177, 37 };
            float[] e = new float[] { 80, 240, 50, 0 };
            float[] currentDigital = new float[] { 0, 0, 0 };

            float[] f = new float[] { -10, 0, 0, 0, 0, 10, 500, 20, 500, 10, 1718, 1730, 8 };

            string[] bodyNum = new string[] { "", "", "-1", "1000" };
            //bodyNum
            //bodyNum[0]:String api_key= "";
            //bodyNum[1]:String secret_key= "";
            //bodyNum[2]:统计的人数-1
            //bodyNum[3]:困人时1000帧调用一次

            ElevatorAlarmOcr_Frame_Kmeans711 AS3 = new ElevatorAlarmOcr_Frame_Kmeans711();
            CannyBackgroundModeling2 CBM = new CannyBackgroundModeling2();
            backgroundModele = CBM.BackgroundModeling(filename1, e);
            string Secret = "@g5#*$y980{_";
            // AS3.alarm3(filenameAlarm, Secret, backgroundModele, a, b, c, d, e,f);



            HashSet<Coordinate> backgroundModeleTemp = new HashSet<Coordinate>();
            Coordinate coordinate = new Coordinate(0, 0);
            backgroundModeleTemp.Add(coordinate);//初始化背景更新临时存放集合


            // 创建 VideoCapture 对象
            VideoCapture capture = new VideoCapture();
            // 使用 VideoCapture 对象读取本地视频
            capture.Open(filenameAlarm);
            //使用 Mat video 保存视频中的图像帧 针对每一帧 做处理
            Mat video = new Mat();

            while (capture.Read(video))
            {
                if (!video.Empty())
                {
                    AS3.Fun_StraightLadderTrappedPeopleMonitoring_V1(video, backgroundModele, backgroundModeleTemp, a, b, c, d, e, f, currentDigital, bodyNum);
                    Cv2.ImShow(" video", video);
                    Cv2.WaitKey(10);

                }
            }
        }*/

        //D:\0Aproject\C#\elevatorAlarm20210402\elevatorAlarm\
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
            float[] nixie = (float[])obb[4];
            if (!video.Empty())
            {
                TestOcr2_kmeans ocr = new TestOcr2_kmeans();
                ocr.FloorIdentification(video, c, d, currentDigital,nixie);
            }
        }

        public float[] Fun_StraightLadderTrappedPeopleMonitoring_V1(Mat video,
         HashSet<Coordinate> backgroundModele, HashSet<Coordinate> backgroundModeleTemp,
         float[] a, float[] b, float[] c, float[] d, float[] e, float[] f, float[] currentDigital, string[] bodyNum,float[] nixie)
        {
            //bodyNum
            //bodyNum[0]:String api_key= "";
            //bodyNum[1]:String secret_key= "";
            //bodyNum[2]:统计的人数
            //bodyNum[3]:困人时1000帧调用一次


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
                    ThreadPool.SetMaxThreads(10, 10);

                    //门
                    object[] ob1 = new object[3] { video, a, b };
                    //人
                    object[] ob2 = new object[3] { video, backgroundModele, e };
                    //楼层检测
                    object[] ob3 = new object[5] { video, c, d, currentDigital,nixie };

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
                         ThreadPool.QueueUserWorkItem(runTestOcr2, ob3);
                        if (c[13] != -10)
                        {//当前楼层信息
                            Console.WriteLine("当前楼层信息#######################" + (int)c[13]);
                        }
                        if (c[14] == 1)
                        {
                            Console.WriteLine("电梯——上行");
                        }
                        else if (c[14] == -1)
                        {
                            Console.WriteLine("电梯——下行");
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

            elevatorStatus[0] = c[13];//楼层信息，初始化为-10；
            elevatorStatus[1] = c[14];//箭头信息，初始化为-10；c[2]=-10未识别箭头信息；c[2]=1或-1返回箭头信息
            elevatorStatus[2] = a[6] >= 3 ? 1 : 0;//门状态1为开0为关
            elevatorStatus[3] = e[3] > detectionSettingValue ? 1 : 0;//1代表有人，0代表无人
                                                                     //elevatorStatus[4]=1困人，0正常

            elevatorStatus[5] = int.Parse(bodyNum[2]);

            return elevatorStatus;
        }


       
    }
}
