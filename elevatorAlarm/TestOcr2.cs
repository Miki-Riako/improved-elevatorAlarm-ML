using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace elevatorAlarm
{
   public class TestOcr2
    {



        //static void Main(string[] args)
        //{
        //   // float[] c = new float[] { 30, 0, 0, 185, 130, 115, 255, 50, 1, 2, 2, 7, -1, -10, -10, 0, 3 };
        //    //float[] d = new float[] { 193, 19, 300, 75 };
        //    //float[] currentDigital = new float[] { 0, 0, 0 };
        //    //String filenameAlarm = "D:\\0video\\video\\11.mp4"; 
        //    //String filenameAlarm="D:\\0video\\long.mp4";
        //    //String filename = "C:\\Users\\Administrator\\Desktop\\2.mp4";

        //    float[] c = new float[] {5, 115,95,88, 255,255, 255, 0,    ////c[0]楼层高度，减少识别错误.c[1]-c[6]b0,g0,r0,b1,g1,r1;
        //       0, 1, 1, 3, -1, -10, -10, 0, 3 };//c[7]箭头位置信息0，在楼层信息上


        //    float[] d = new float[] { 382, 47, 411, 95 };
        //    float[] currentDigital = new float[] { 0, 0, 0 };
        //    String filenameAlarm = "E:\\困人智能检测程序和冬奥视频\\延庆冬奥运营中心中区1-能用.mp4";

        //    // 创建 VideoCapture 对象
        //    VideoCapture capture = new VideoCapture();
        //    // 使用 VideoCapture 对象读取本地视频
        //    capture.Open(filenameAlarm);
        //    //使用 Mat video 保存视频中的图像帧 针对每一帧 做处理
        //    Mat video = new Mat();

        //    while (capture.Read(video))
        //    {
        //        if (!video.Empty())
        //        {
        //            TestOcr2 t2 = new TestOcr2();
        //            t2.FloorIdentification(video, c, d, currentDigital);
        //            if (c[13] != -10)
        //            {
        //                Console.WriteLine("当前楼层信息为" + c[13]);
        //            }
        //            if (c[14] == -1)
        //            {
        //                Console.WriteLine("电梯向下运行");
        //            }
        //            else if (c[14] == 1)
        //            {
        //                Console.WriteLine("电梯向上运行");
        //            }

        //            Cv2.ImShow(" video", video);
        //            //Cv2.ImShow("dealvideo", dealvideo);
        //           Cv2.WaitKey(10);

        //        }
        //        else
        //        {
        //            break;
        //        }


        //    }


        //}




        /*
     * FloorIdentification参数说明
     * imageStr图片保存OCR读取地址
     * videoStr视频文件地址
     * buildingHeight:楼层高度，减少识别错误
     * numberlntervals:间隔帧读取
     * b0,g0,r0,b1,g1,r1颜色分割参数
     * minarea:数字箭头最小面积，去除干扰
     * arrowPosition:代表箭头位置（1：在数字左边；0：在数字上面；-1：在数字右边）
     * dilateValue:图像膨胀数值
     * erodeValue:图像腐蚀数值
     * gaussianBlurValue高斯模糊阈值（一般为3，只能取奇数）
     * directionAdjustment箭头方向的调整，对不同形式箭头进行微调(1或-1)
     * */


        //public  int FloorIdentification(String imageAddress,String videoAddress,int buildingHeight,int numberIntervals,int b0,int g0,int r0,int b1,int g1,int r1,int minarea,int arrowPosition,int dilateValue,int erodeValue,int gaussianBlurValue,int directionAdjustment){    //buildingHeight30//numberIntervals:6
        public float FloorIdentification(Mat video, float[] c, float[] d, float[] currentDigital)
        {
            //int framesNum=c[15];//间隔读取帧数，c[15],与c[16]配合使用，c[15]初始化为0，c[16]要间隔的帧数
            int frame = (int)c[16];//隔几帧读取
            c[15]++;
            if (c[15] == frame)
            {
                c[15] = 0;

                int buildingHeight = (int)c[0];//楼层高度，减少识别错误
                /*int numberIntervals=c[1];//间隔帧读取*/
                int b0 = (int)c[1];//颜色分割参数
                int g0 = (int)c[2];
                int r0 = (int)c[3];
                int b1 = (int)c[4];
                int g1 = (int)c[5];
                int r1 = (int)c[6];
                int minarea = (int)c[7];//数字箭头最小面积，去除干扰
                int arrowPosition = (int)c[8];//代表箭头位置（1：在数字左边；0：在数字上面；-1：在数字右边）
                int dilateValue = (int)c[9];//图像膨胀数值
                int erodeValue = (int)c[10];//图像腐蚀数值
                int gaussianBlurValue = (int)c[11];//高斯模糊阈值（一般为3，只能取奇数）
                int directionAdjustment = (int)c[12];//箭头方向的调整，对不同形式箭头进行微调(1或-1)
                                                     //* * int currentFloor=c[13];//初始值为-10；楼层数字;c[13]=-10代表无楼层信息
                                                     //* * int arrowDirection=c[14];//初始值为-10；箭头方向，c[14]=-10代表无箭头；c[14]=1;箭头向上；c[14]=-1;箭头向下
                                                     //* * int framesNum=c[15];//间隔读取帧数，c[15],与c[16]配合使用，c[15]初始化为0，c[16]要间隔的帧数
                                                     //* * int frame=a[16];//隔几帧读取



                /*
                 * FloorIdentification参数说明
                 * newFileName图片保存OCR读取地址
                 * video读取的一帧视频
                 * buildingHeight:楼层高度，减少识别错误

                 * b0,g0,r0,b1,g1,r1颜色分割参数
                 * minarea:数字箭头最小面积，去除干扰
                 * arrowPosition:代表箭头位置（1：在数字左边；0：在数字上面；-1：在数字右边）
                 * dilateValue:图像膨胀数值
                 * erodeValue:图像腐蚀数值
                 * gaussianBlurValue高斯模糊阈值（一般为3，只能取奇数）
                 * directionAdjustment箭头方向的调整，对不同形式箭头进行微调(1或-1)
                 * */


                /*
               d[]划定门检测区域
               * d[0]:为x坐标
               * d[1]:为y坐标
               * d[2]:图像x1
               * d[3]:图像y1
               *
               * */

                /*
                 * int[] currentDigital保存三帧楼层信息；初始化全部设置为：0
                 */

                OpenCvSharp.Rect rect = new OpenCvSharp.Rect((int)d[0],
                    (int)d[1], (int)(d[2] - d[0]), (int)(d[3] - d[1]));
                video = video.SubMat(rect);
                float scale2= 4.0f * 1f;//10意思是放大10倍
                float width2 = video.Width;
                float height2 = video.Height;
                Cv2.Resize(video, video, new OpenCvSharp.Size(width2 * scale2, height2 * scale2));

                // Cv2.ImShow("dealvideo", video);
                // Cv2.WaitKey(0);
                c[13] = -10;

                //ITesseract instance = new Tesseract();

                // instance.setDatapath(tess4JFileName);
                // instance.setLanguage("eng");
                TesseractEngine instance = new TesseractEngine(@".\tessdata\", "eng3");
                /*int currentDigital[]=new int[]{0,0,0};//当前楼层
                int framesNum=0;//间隔读取帧数*/
                VideoProcessing videoProcessing = new VideoProcessing();
                //基于颜色对图片进行分割
                DigitalImageProcessing digitalImageProcessing = new DigitalImageProcessing();


                /* framesNum++;
                 if(framesNum==numberIntervals){
                     framesNum=0;


                     int index=0;
                     //程序开始时间*/
                // long startTime = System.currentTimeMillis();

                Mat srcVideo = video.Clone();

                Mat srcVideo1 = digitalImageProcessing.setMatImage(srcVideo, b0, g0, r0, b1, g1, r1, dilateValue, erodeValue, gaussianBlurValue);
                Mat srcVideo2 = srcVideo1.Clone();
               // Cv2.ImShow("srcVideo1", srcVideo1);
                //将数字分离出来，分割出非数字字符，并将其置为白色(将箭头区域置为白色或者黑色，与背景颜色一致)
                srcVideo1 = digitalImageProcessing.DigitalSegmentation1(srcVideo1, minarea, arrowPosition);
                //保存当前帧图像，颜色分割
              
               

                // BufferedImage textImage = null;
                Mat textImage = videoProcessing.frameProcessing(srcVideo1);
                // Cv2.ImShow("srcVideo1", textImage);


                /*framesNum++;*/
                //识别电梯所在楼层
                if (textImage != null)
                    //new AlarmSystem2().currentFloor = videoProcessing.floorIdentification(buildingHeight, textImage, instance, currentDigital);
                    c[13] = videoProcessing.floorIdentification(buildingHeight, textImage, instance, currentDigital);

                /*if (c[13] != -10)
                {
                    Console.WriteLine("当前楼层：" + c[13]);
                }*/
                //电梯运行方向识别
                Mat srcVideo3 = digitalImageProcessing.setMatImage(srcVideo, b0, g0, r0, b1, g1, r1, dilateValue, erodeValue, gaussianBlurValue);
                //Cv2.ImShow("srcVideo3", textImage);
                c[14] = digitalImageProcessing.ArrowRecognition(srcVideo3, minarea, arrowPosition);
                if (c[14] == directionAdjustment)
                {
                    c[14] = -1;
                    // Console.WriteLine("电梯向下运行");
                }
                else if (c[14] == -directionAdjustment)
                {
                    c[14] = 1;
                    // Console.WriteLine("电梯向上运行");
                }


                /*else if(direction==0){
                    new AlarmSystem2().currentFloor=-10;
                    currentDigital[0]=0;
                    currentDigital[1]=0;
                    currentDigital[2]=0;
                }*/


                //System.out.println("OCR Result: \n" + ocrResult + "\n 耗时：" + (System.currentTimeMillis() - startTime) + "ms");

                //缩放视频图像
                float scale = 4.5f;//0.5意思是放大0.5倍
                float width = video.Width;
                float height = video.Height;
                Cv2.Resize(video, video, new OpenCvSharp.Size(width * scale, height * scale));

                //缩放视频图像
                float scale1 = 4.5f;//0.5意思是放大0.5倍
                float width1 = srcVideo1.Width;
                float height1 = srcVideo1.Height;
                Cv2.Resize(srcVideo1, srcVideo1, new OpenCvSharp.Size(width1 * scale1, height1 * scale1));

                //System.out.println("当前帧程序处理时间："+(System.currentTimeMillis() - startTime)+"ms");

                /*  HighGui.imshow("video", video);
                  HighGui.imshow("dst", dst);
                  //HighGui.imshow("srcVideo2",srcVideo2);
                  HighGui.waitKey(10);*/

                //if(framesNum==0)
                //System.out.println("当前帧程序处理时间："+(System.currentTimeMillis() - startTime)+"ms");
                video.Release();

                srcVideo.Release();
                srcVideo1.Release();
                srcVideo2.Release();
                //dst.release();
                /*roi.release();
                src.release();*/

                /* }*/


                //video.release();

            }
            return c[13];
        }





        public class DigitalImageProcessing
        {



            public static List<OpenCvSharp.Rect> process(Mat video, int digitalArea)
            {
                // 1 跟踪物体在图像中的位置
                Mat[] contours;

                // 2找出图像中物体的位置
                Cv2.FindContours(video, out contours, new Mat(),
                    RetrievalModes.External, ContourApproximationModes.ApproxSimple, new OpenCvSharp.Point(0, 0));
                // 3 对象结果
                List<OpenCvSharp.Rect> rects = new List<OpenCvSharp.Rect>();
                OpenCvSharp.Rect rect;
                if (contours.Length > 0)
                {// 4.1 如果发现图像
                    for (int i = 0; i < contours.Length; i++)
                    {
                        rect = new OpenCvSharp.Rect();
                        double area = Cv2.ContourArea(contours[i]);
                        if (area > digitalArea)
                        {
                            rect = Cv2.BoundingRect(contours[i]);
                        }
                        else
                        {
                            rect.X = rect.Y = rect.Width = rect.Height = 0;
                        }
                        rects.Add(rect);
                    }
                }
                else
                {// 4.2 如果没有发现图像
                    rect = new OpenCvSharp.Rect();
                    rect.X = rect.Y = rect.Width = rect.Height = 0;
                    rects.Add(rect);
                }
                return rects;
            }



            /*  //跟踪物体在图像中的位置
              public List<OpenCvSharp.Rect> process(Mat video, int minarea)
              {

                  *//*//对需标记位置的图像预处理，灰度，并二值取反
                  //转灰度图片
                  Imgproc.cvtColor(video, video, Imgproc.COLOR_BGR2GRAY);
                  //二值取反,使图片白纸黑字
                  Imgproc.threshold(video, video, 0, 255, Imgproc.THRESH_BINARY_INV | Imgproc.THRESH_OTSU);*//*

                  // 1 跟踪物体在图像中的位置
                  List<MatOfPoint> contours = new ArrayList<MatOfPoint>();
                  // 2找出图像中物体的位置
                  Imgproc.findContours(video, contours, new Mat(), Imgproc.RETR_EXTERNAL, Imgproc.CHAIN_APPROX_SIMPLE, new Point(0, 0));
                  // 3 对象结果
                  List<Rect> rects = new ArrayList<Rect>();
                  Rect rect;
                  if (contours.size() > 0)
                  {// 4.1 如果发现图像
                      for (int i = 0; i < contours.size(); i++)
                      {
                          //rect=new Rect();
                          double area = Imgproc.contourArea(contours.get(i));
                          if (area > minarea)
                          {//注意此处面积为数字大小，应该小于数字大小//200
                              rect = Imgproc.boundingRect(contours.get(i));

                              rects.add(rect);
                          } *//*else {
                          rect.x = rect.y = rect.width = rect.height = 0;
                      }
                      rects.add(rect);*//*
                      }
                  }
                  else
                  {// 4.2 如果没有发现图像
                      rect = new Rect();
                      rect.x = rect.y = rect.width = rect.height = 0;
                      rects.add(rect);
                  }
                  return rects;
              }*/

            /*
             * 基于颜色对图片进行分割
             * */
            public Mat setMatImage(Mat image, int b0, int g0, int r0, int b1, int g1, int r1,
                int dilateValue, int erodeValue, int gaussianBlurValue)
            {

                Mat src = image.Clone();

                /*//确定检测区域  !!!注意此处检测区域大小应根据实际情况调整
                int x1=630;
                int y1=0;
                int width1=960-x1;//640（x1+width1,y1+height1）检测区域
                int height1=330;//1136
                Rect rect=new Rect(x1,y1,width1,height1);
                Mat roi=src.submat(rect);
                 src=roi.clone();*/

                Mat dst = new Mat(src.Size(), src.Type());
                Mat dstDilate = new Mat(src.Size(), src.Type());

                Mat dstErode = new Mat(src.Size(), src.Type());

                Mat srcGaussianBlur = new Mat(src.Size(), src.Type());
                //高斯滤波
                Cv2.GaussianBlur(src, srcGaussianBlur, new OpenCvSharp.Size(gaussianBlurValue, gaussianBlurValue), 0, 0);
                //颜色分割  !!!注意颜色分割数值的大小应根据实际图像设定
                Cv2.InRange(srcGaussianBlur, new Scalar(b0, g0, r0), new Scalar(b1, g1, r1), dst);//低(10,100,190)高(120,180,260)new Scalar(0, 100,160),new Scalar(120, 235, 250)
               // Cv2.ImShow("yansefenge", dst);
              //  Cv2.WaitKey(0);
                //腐蚀膨胀

                Mat knenel = Cv2.GetStructuringElement(MorphShapes.Ellipse, new OpenCvSharp.Size(3, 3));
                Mat knene2 = Cv2.GetStructuringElement(MorphShapes.Ellipse, new OpenCvSharp.Size(3, 3));
                Cv2.Dilate(dst, dstDilate, knenel, new OpenCvSharp.Point(0, 0), dilateValue);//膨胀4
                Cv2.Erode(dstDilate, dstErode, knene2, new OpenCvSharp.Point(0, 0), erodeValue);//腐蚀3


                Cv2.GaussianBlur(dstErode, srcGaussianBlur, new OpenCvSharp.Size(7, 7), 0, 0);
              //  Cv2.ImShow("fushi", srcGaussianBlur);
               // Cv2.WaitKey(0);

                //二值化(黑底白字)
                Cv2.Threshold(srcGaussianBlur, srcGaussianBlur, 0, 255, ThresholdTypes.Binary | ThresholdTypes.Otsu);
                //Cv2.ImShow("erzhihua", srcGaussianBlur);
              // Cv2.WaitKey(0);

                //分割出非数字字符，并将其置为白色(将箭头区域置为白色)
                /* DigitalImageProcessing d1=new DigitalImageProcessing();
                 List<Rect> rects=d1.process(srcGaussianBlur);
                 int t=0;
                 int Max=src.rows();
                 for(int i=0;i<rects.size();i++){
                     if(rects.get(i).y<Max&&rects.get(i).y!=0){
                         Max=rects.get(i).y;
                         t=i;
                     }
                 }
                 System.out.println(t);
                 Rect rect =new Rect(rects.get(t).x,rects.get(t).y,rects.get(t).width,rects.get(t).height);
                 Mat roi=new Mat(new Size(rects.get(t).width,rects.get(t).height),srcGaussianBlur.type(),new Scalar(255,255,255));
                 roi.copyTo(srcGaussianBlur.submat(rect));*/

                src.Release();
                dst.Release();
                dstDilate.Release();
                dstErode.Release();


                return srcGaussianBlur;
            }


            //将数字分离出来，分割出非数字字符，并将其置为白色(将箭头区域置为白色或者黑色，与背景颜色一致)

            public Mat DigitalSegmentation1(Mat srcGaussianBlur, int minarea, int arrowPosition)
            {//arrowPosition代表箭头位置（1：在数字左边；0：在数字上面；-1：在数字右边）
                //DigitalImageProcessing d1 = new DigitalImageProcessing();
                List<OpenCvSharp.Rect> rects = process(srcGaussianBlur, minarea);//注意！！minarea根据实际选择
                int t = 0;

                //Console.WriteLine(rects.Count+"*************************************");

                if (rects.Count >= 2)
                {

                    if (arrowPosition == 0)
                    {
                        int MinY = srcGaussianBlur.Rows;
                        for (int i = 0; i < rects.Count; i++)
                        {
                            if (rects[i].Y < MinY && rects[i].Y != 0)
                            {
                                MinY = rects[i].Y;
                                t = i;
                            }
                        }
                    }
                    else if (arrowPosition == 1)
                    {
                        int MinX = srcGaussianBlur.Cols;
                        for (int i = 0; i < rects.Count; i++)
                        {
                            if (rects[i].X < MinX && rects[i].X != 0)
                            {
                                MinX = rects[i].X;
                                t = i;
                            }
                        }
                    }
                    else if (arrowPosition == -1)
                    {
                        int MaxX = 0;
                        for (int i = 0; i < rects.Count; i++)
                        {
                            if (rects[i].X > MaxX && rects[i].X != 0)
                            {
                                MaxX = rects[i].X;
                                t = i;
                            }
                        }
                    }
                    if (rects[t].Width == 0 || rects[t].Height == 0) return srcGaussianBlur;

                    //Console.WriteLine(rects[t].X + "____" + rects[t].Y + "_____" + rects[t].Width + "_________" + rects[t].Height);
                    // Cv2.ImShow("srcGaussianBlur1", srcGaussianBlur);
                    OpenCvSharp.Rect rect = new OpenCvSharp.Rect(rects[t].X, rects[t].Y, rects[t].Width, rects[t].Height);
                    Mat roi = new Mat(rects[t].Height, rects[t].Width, srcGaussianBlur.Type(), new Scalar(0, 0, 0));//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    roi.CopyTo(srcGaussianBlur.SubMat(rect));

                    roi.Release();

                    //Cv2.ImShow("srcGaussianBlur", srcGaussianBlur);

                    return srcGaussianBlur;

                }
                else
                {
                    return srcGaussianBlur;
                }


            }

            //识别箭头方向
            public int ArrowRecognition(Mat srcGaussianBlur, int minarea, int arrowPosition)
            {
                List<OpenCvSharp.Rect> rects = process(srcGaussianBlur, minarea);//注意！！minarea根据实际选择
                int t = -1;

                /* int Min=0;
                 int num=0;*/

                if (rects.Count >= 2)
                {

                    if (arrowPosition == 0)
                    {
                        int MinY = srcGaussianBlur.Rows;//row与rows啥区别？
                        for (int i = 0; i < rects.Count; i++)
                        {
                            if (rects[i].Y < MinY && rects[i].Y != 0)
                            {
                                MinY = rects[i].Y;
                                t = i;
                            }
                        }
                    }
                    else if (arrowPosition == 1)
                    {
                        int MinX = srcGaussianBlur.Cols;
                        for (int i = 0; i < rects.Count; i++)
                        {
                            if (rects[i].X < MinX && rects[i].X != 0)
                            {
                                MinX = rects[i].X;
                                t = i;
                            }
                        }
                    }
                    else if (arrowPosition == -1)
                    {
                        int MaxX = 0;
                        for (int i = 0; i < rects.Count; i++)
                        {
                            if (rects[i].X > MaxX && rects[i].X != 0)
                            {
                                MaxX = rects[i].X;
                                t = i;
                            }
                        }
                    }

                    if (t != -1)
                    {
                        OpenCvSharp.Rect rect = new OpenCvSharp.Rect(rects[t].X, rects[t].Y, rects[t].Width, rects[t].Height);
                        Mat roi = srcGaussianBlur.SubMat(rect);

                        double numBlackUp = 0;
                        double numBlackDown = 0;
                        for (int i = 0; i < (int)(roi.Height / 2); i++)
                        {
                            for (int j = 0; j < roi.Width; j++)
                            {

                                if (roi.At<Vec3b>(i, j)[0] == 255)
                                    numBlackUp++;
                            }
                        }
                        for (int i = (int)(roi.Height / 2); i < roi.Height; i++)
                        {
                            for (int j = 0; j < roi.Width; j++)
                            {

                                if (roi.At<Vec3b>(i, j)[0] == 255)
                                    numBlackDown++;
                            }
                        }

                        numBlackUp = numBlackUp / ((int)(roi.Height / 2) * roi.Width);
                        numBlackDown = numBlackDown / (roi.Height * roi.Width - (int)(roi.Height / 2) * roi.Width);
                        if (numBlackUp > numBlackDown)
                        {
                            return 1;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        return -10;
                    }

                }
                else
                {
                    return -10;
                }


            }

        }


        public class VideoProcessing
        {


            //写入新一帧楼层信息并删除旧一帧信息
            public float[] changeArray1(float[] currentDigital, float digitsSum)
            {
                currentDigital[2] = currentDigital[1];
                currentDigital[1] = currentDigital[0];
                currentDigital[0] = digitsSum;
                return currentDigital;
            }
            /* public int[] changeArray2(int[] currentDigital,int digitsSum ){
                 currentDigital[2]=digitsSum;
                 return currentDigital;
             }*/

            //处理当前帧,调用颜色分割,保存给OCR识别
            public Mat frameProcessing(Mat video)
            {


                Mat src = video.Clone();

                //基于颜色对图片进行分割
                /* DigitalImageProcessing digitalImageProcessing=new DigitalImageProcessing();*/
                /*//提取数字信息
                Mat dst=digitalImageProcessing.setMatImage(src);*/

                /*
                           Mat dst=digitalImageProcessing.DigitalSegmentation1(src);//将箭头消除*/

                //二值化
                Cv2.Threshold(src, src, 0, 255, ThresholdTypes.Binary | ThresholdTypes.Otsu);

                Cv2.CvtColor(src, src, ColorConversionCodes.BayerGR2RGB);//!!!!!!!!!!!!!!!!
                                                                         // Cv2.CvtColor(src, src, Imgproc.COLOR_GRAY2RGB);

                /*  textImage = null;
                  if (src.Height > 0 && src.Width > 0)
                  {
                      textImage = new BufferedImage(src.width(), src.height(),
                              BufferedImage.TYPE_3BYTE_BGR);
                      WritableRaster raster = textImage.getRaster();
                      DataBufferByte dataBuffer = (DataBufferByte)raster.getDataBuffer();
                      byte[] data = dataBuffer.getData();
                      src.get(0, 0, data);

                  }*/
                return src;

                //if(!src.empty())

                /*Imgcodecs.imwrite(newFileName, src);
                return src;*/

                //src.release();
                //dst.release();



            }

            //OCR识别
            public int OcrRecognition(Mat Image, TesseractEngine instance, int buildingHeight)
            {

                Bitmap textImage = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(Image);
                try
                {
                    //ocr识别
                    /*File newfile = new File(newFileName);
                    if (!newfile.exists()) {
                        return -10;
                    }
                    String ocrResult = instance.doOCR(newfile);*/
                    if (textImage == null)
                        return -10;
                    Page page = instance.Process(textImage, PageSegMode.SingleBlock);
                    String ocrResult = page.GetText();
                    if (ocrResult.Substring(0,1) == "L" || ocrResult.Substring(0, 1) == "q" || ocrResult.Substring(0, 1) == "t" || ocrResult.Substring(0, 1) == "I")
                        ocrResult = "4";

                    Console.WriteLine("OCR-------------------:" + ocrResult);
                    if (ocrResult != null)
                    {
                        int length = ocrResult.Length;

                        //过滤ocr识别信息,只保留数字
                        int digitsNum = 0;
                        int digitsSum = 0;
                        for (int i = 0; i < length; i++)
                        {
                            if (ocrResult[i] > 47 && ocrResult[i] < 58)
                            {
                                digitsNum++;
                                if (digitsNum > 2)
                                {
                                    break;
                                }
                                else if (digitsNum == 1)
                                {
                                    digitsSum = ocrResult[i] - 48;
                                }
                                else
                                {
                                    digitsSum = digitsSum * 10 + ocrResult[i] - 48;
                                }
                            }
                        }


                        //保留特定楼层
                        if (digitsSum <= buildingHeight && digitsSum != 0)
                        {//30意思是楼高不超过30层 !!!注意具体楼层根据实际情况设定
                            return digitsSum;
                        }
                        else
                        {
                            return -10;//返回-10意味着未识别正确信息
                        }
                    }
                    else
                    {
                        return -10;
                    }
                }
                catch (Exception)
                {

                    return -10;
                    throw;//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                }


            }

            /*根据三帧信息判断楼层数字*/
            public float floorIdentification(int buildingHeight, Mat textImage, TesseractEngine instance, float[] currentDigital)
            {
                VideoProcessing videoProcessing = new VideoProcessing();
                //DigitalImageProcessing digitalImageProcessing=new DigitalImageProcessing();
                /*if(framesNum==numberIntervals){*/
                /*framesNum=0;*/

                /* int index=0;
                 //程序开始时间
                 long startTime = System.currentTimeMillis();*/


                float a = currentDigital[0];
                float b = currentDigital[1];
                float c = currentDigital[2];
                if (a == 0 || b == 0 || c == 0)
                {//初始化并保存三帧楼层信息

                    //OCR识别
                    int digital = videoProcessing.OcrRecognition(textImage, instance, buildingHeight);
                   // Console.WriteLine("OCR:"+ digital);
                    if (digital != -10)
                    {
                        videoProcessing.changeArray1(currentDigital, digital);
                    }
                    if (currentDigital[2] != 0)
                    {
                        a = currentDigital[0];
                        b = currentDigital[1];
                        c = currentDigital[2];
                        if (Math.Abs(a - b) > 3 || Math.Abs(a - c) > 3 || Math.Abs(b - c) > 3)
                        {//包含不符合要求的数据，就重新初始化//注意！！！！！！原来为2
                            currentDigital[0] = 0;
                            currentDigital[1] = 0;
                            currentDigital[2] = 0;
                        }
                    }
                    return -10;//表明电梯楼层信息初始化
                }
                else if (a != 0 && b != 0 && c != 0)
                {
                    //OCR识别
                    int digital = videoProcessing.OcrRecognition(textImage, instance, buildingHeight);
                   // Console.WriteLine("OCR:" + digital);
                    if (digital != -10)
                    {
                        videoProcessing.changeArray1(currentDigital, digital);
                    }
                    a = currentDigital[0];
                    b = currentDigital[1];
                    c = currentDigital[2];
                    if (Math.Abs(a - b) > 5)
                    {//注意！！！！！！原来为2或3
                        currentDigital[0] = b;
                        /*currentDigital[0]=0;*/
                    }

                    //System.out.println("当前楼层：" + currentDigital[0]);
                    return currentDigital[0];


                    /*为了视频5临时删掉，解决箭头与数字分别使用不同颜色分割阈值问题*/
                    /*  //电梯运行方向识别
                      int direction=digitalImageProcessing.ArrowRecognition(srcVideo2,minarea,arrowPosition);
                      if(direction==1){
                          System.out.println("电梯向下运行");
                      }else if(direction==-1){
                          System.out.println("电梯向上运行");
                      }*/
                }
                return -10;//表明电梯楼层信息初始化
                /*    return 0;
                }else{
                        return framesNum;
                    }*/
            }
        }







    }
}
