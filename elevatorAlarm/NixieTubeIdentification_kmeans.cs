using System;
using System.Collections.Generic;
using OpenCvSharp;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elevatorAlarm
{
    //箭头在左
    public class ReComparer_K : IComparer<Rect>
    {
        //对x进行比较
        public int Compare(Rect r1, Rect r2)
        {
            if (r1.X > r2.X)
            {
                return 1;
            }
            else if (r1.X == r2.X)
            {
                return 0;
            }
            else
            {
                return -1;
            }
            //return (r1.X.CompareTo(r2.X));
        }
    }

    //箭头在上
    public class ReComparer1_K : IComparer<Rect>
    {
        float[] c;
        public ReComparer1_K(float[] c) {
            this.c = c;
        }
        //对x进行比较
        public int Compare(Rect r1, Rect r2)
        {
            if (r1.Y - r2.Y > c[16]) {
                return 1;
            } else if (r2.Y-r1.Y>c[16]) {
                return 0;
            }

            if (r1.X > r2.X)
            {
                return 1;
            }
            else if (r1.X == r2.X)
            {
                return 0;
            }
            else
            {
                return -1;
            }
            //return (r1.X.CompareTo(r2.X));
        }
    }

    public class NixieTubeIdentification2_Kmeans
    {
       /* static void Main(string[] args)
        {
                String filenameAlarm = "D:\\0video\\video\\2.mp4";//String filenameAlarm="D:\\0video\\long.mp4";
                                                                  //String filename = "C:\\Users\\Administrator\\Desktop\\2.mp4";
                                                                  // 创建 VideoCapture 对象
                VideoCapture capture = new VideoCapture();
                // 使用 VideoCapture 对象读取本地视频
                capture.Open(filenameAlarm);
                //使用 Mat video 保存视频中的图像帧 针对每一帧 做处理
                Mat video = new Mat();
                float[] c = new float[] { -10, -10, -10, 10, 3, 130, 130, 130, 255, 255, 255, 4, 2, 23, 0.5f, 300, 80, 40 };//(楼层识别）//缩放是小数时加f，例如1.5f
                                                                                                                            //c[0]=1;//判定一个箭头一个数字,仅在有两个字符时起作用;c[0]=-10抛异常；c[0]=0:说明读取的是一个或三个字符                                                                                                                 //c[1];//c[1]!=-10时返回楼层数字信息
                                                                                                                            //c[2]!=-10,c[1]=正负1时判定楼层箭头方向（
                float[] d = new float[] { 516, 155, 561, 187, 0, 0, 330, 196 };//(楼层识别）* d[0]:为x坐标* d[1]:为y坐标* d[2]:图像x1* d[3]:图像y1


                //int[] c = new int[] { 0, -10, -10 };
                //c[0]=1;//判定一个箭头一个数字,仅在有两个字符时起作用
                //c[1];//c[1]!=-10时返回楼层数字信息
                //c[2]!=-10,c[1]=正负1时判定楼层箭头方向
                // int[] d = new int[] { 516, 155, 561, 187 };



                NixieTubeIdentification2 nix = new NixieTubeIdentification2();
                while (capture.Read(video))
                {
                    if (!video.Empty())
                    {
                        nix.NixieTube(video, c, d);
                        if (c[1] != -10)
                            Console.WriteLine("当前楼层信息为" + c[1]);
                        Cv2.ImShow(" video", video);
                        //Cv2.ImShow("dealvideo", dealvideo);
                        Cv2.WaitKey(20);

                    }
                    else
                    {
                        break;
                    }
            }
        }*/

        /*static void Main(string[] args)
        {
            int sign = 1;
            if (sign == 1)
            {
                String filenameAlarm = "D:\\0video\\video\\2.mp4";//String filenameAlarm="D:\\0video\\long.mp4";
                                                                  //String filename = "C:\\Users\\Administrator\\Desktop\\2.mp4";
                                                                  // 创建 VideoCapture 对象
                VideoCapture capture = new VideoCapture();
                // 使用 VideoCapture 对象读取本地视频
                capture.Open(filenameAlarm);
                //使用 Mat video 保存视频中的图像帧 针对每一帧 做处理
                Mat video = new Mat();


                float[] c = new float[] { -10, -10, -10, 10, 3, 130, 130, 130, 255, 255, 255, 4, 2, 23, 0.5f, 300, 80, 40 };//(楼层识别）//缩放是小数时加f，例如1.5f
                                                                                                                            //c[0]=1;//判定一个箭头一个数字,仅在有两个字符时起作用;c[0]=-10抛异常；c[0]=0:说明读取的是一个或三个字符
                                                                                                                            //c[1];//c[1]!=-10时返回楼层数字信息
                                                                                                                            //c[2]!=-10,c[1]=正负1时判定楼层箭头方向（
                float[] d = new float[] { 516, 155, 561, 187, 0, 0, 330, 196 };//(楼层识别）* d[0]:为x坐标* d[1]:为y坐标* d[2]:图像x1* d[3]:图像y1


                //int[] c = new int[] { 0, -10, -10 };
                //c[0]=1;//判定一个箭头一个数字,仅在有两个字符时起作用
                //c[1];//c[1]!=-10时返回楼层数字信息
                //c[2]!=-10,c[1]=正负1时判定楼层箭头方向
                // int[] d = new int[] { 516, 155, 561, 187 };



                NixieTubeIdentification2 nix = new NixieTubeIdentification2();
                while (capture.Read(video))
                {
                    if (!video.Empty())
                    {
                        nix.NixieTube(video, c, d);
                        if (c[1] != -10)
                            Console.WriteLine("当前楼层信息为" + c[1]);
                        Cv2.ImShow(" video", video);
                        //Cv2.ImShow("dealvideo", dealvideo);
                        Cv2.WaitKey(20);

                    }
                    else
                    {
                        break;
                    }


                }
            }
            else
            {
                String filenameAlarm = "D:\\BaiduNetdiskDownload\\test1.mp4";

                // 创建 VideoCapture 对象
                VideoCapture capture = new VideoCapture();
                // 使用 VideoCapture 对象读取本地视频
                capture.Open(filenameAlarm);
                //使用 Mat video 保存视频中的图像帧 针对每一帧 做处理
                Mat video = new Mat();

                //bgr
                float[] c = new float[] { -10, -10, -10, 10, 3, 80, 80, 100, 210, 200, 255, 4, 2, 17, 1, 600, 60, 40 };//(楼层识别）//缩放是小数时加f，例如1.5f
                                                                                                                       //c[0]=1;//判定一个箭头一个数字,仅在有两个字符时起作用;c[0]=-10抛异常；c[0]=0:说明读取的是一个或三个字符
                                                                                                                       //c[1];//c[1]!=-10时返回楼层数字信息
                                                                                                                       //c[2]!=-10,c[1]=正负1时判定楼层箭头方向（
                float[] d = new float[] { 469, 123, 498, 155, 0, 0, (498 - 469) * c[3], (155 - 123) * c[3] };//(楼层识别）* d[0]:为x坐标* d[1]:为y坐标* d[2]:图像x1* d[3]:图像y1





                NixieTubeIdentification2 nix = new NixieTubeIdentification2();

                while (capture.Read(video))
                {
                    if (!video.Empty())
                    {
                        nix.NixieTube1(video, c, d);
                        if (c[1] != -10)
                            Console.WriteLine("当前楼层信息为" + c[1]);
                        Cv2.ImShow(" video", video);
                        //Cv2.ImShow("dealvideo", dealvideo);
                        Cv2.WaitKey(20);

                    }
                    else
                    {
                        break;
                    }
                }
            }



        }*/



        /*static void Main(string[] args)
        {
            String filenameAlarm = "D:\\BaiduNetdiskDownload\\test1.mp4";

            // 创建 VideoCapture 对象
            VideoCapture capture = new VideoCapture();
            // 使用 VideoCapture 对象读取本地视频
            capture.Open(filenameAlarm);
            //使用 Mat video 保存视频中的图像帧 针对每一帧 做处理
            Mat video = new Mat();

            //bgr
            float[] c = new float[] { -10, -10, -10, 10, 3, 80, 80, 100, 210, 200, 255, 4, 2, 17, 1, 600, 60, 40 };//(楼层识别）//缩放是小数时加f，例如1.5f
                                                                                                                   //c[0]=1;//判定一个箭头一个数字,仅在有两个字符时起作用;c[0]=-10抛异常；c[0]=0:说明读取的是一个或三个字符
                                                                                                                   //c[1];//c[1]!=-10时返回楼层数字信息
                                                                                                                   //c[2]!=-10,c[1]=正负1时判定楼层箭头方向（
            float[] d = new float[] { 469, 123, 498, 155, 0, 0, (498 - 469) * c[3], (155 - 123) * c[3] };//(楼层识别）* d[0]:为x坐标* d[1]:为y坐标* d[2]:图像x1* d[3]:图像y1





            NixieTubeIdentification2 nix = new NixieTubeIdentification2();

            while (capture.Read(video))
            {
                if (!video.Empty())
                {
                    nix.NixieTube1(video, c, d);
                    if (c[1] != -10)
                        Console.WriteLine("当前楼层信息为" + c[1]);
                    Cv2.ImShow(" video", video);
                    //Cv2.ImShow("dealvideo", dealvideo);
                    Cv2.WaitKey(20);

                }
                else
                {
                    break;
                }


            }

        }*/


        //数字与箭头从上到下D:\BaiduNetdiskDownload\test1.mp4
        public void NixieTube1(Mat video, float[] c, float[] d)
        {
            /*c={-10,-10,-10,10,3,130,130,130,255,255,255,4,2,23,0.5,300,80,40}
            *d={516, 155, 561, 187,0,0,330,196}
            * c[0]:初始化为-10；c[0] = -10证明不能识别，无楼层信息;; c[0] = 0;代表有楼层信息1,2或3个字符;;c[0] == 1一数字一箭头
            * c[1]:初始化为-10；c[1]=-10楼层信息未识别;;c[1]!=-10,c[1]为楼层信息
            * c[2]:初始化为-10；c[2]=-10未识别箭头信息；c[2]=1或-1返回箭头信息
            * c[3]:首次缩放图片，此处一般为放大,；例如c[3]=10
            * c[4]:例如取3；高斯模糊取值为奇数，3,5,7.......
            * c[5]~c[7]颜色分割低阈值，c[8]~c[10];颜色分割高阈值,例如取值：(130, 130, 130),(255, 255, 255)
            * c[11]图像膨胀数值;例如c[11]=4
            * c[12]图像腐蚀数值；例如c[12]=2
            * c[13]图像逆时针旋转的角度,为了矫正图像,例如23；不需要可设定为0.1
            * c[14]图像缩放程度,例如0.5
            * c[15]数字或箭头的最小面积，过滤无用信息;例如300
            * c[16]箭头在左边时，当是一个箭头一个数字时，箭头与数字的最小距离；例如80：：
            *      ：箭头在上面时，箭头与数字的上下距离
            *c[17]最高楼层;例如40
            * d[0]~d[3]图像预处理，划定图像处理范围，图一
            * d[4]~d[7]为了过滤处理后的信息，再次缩小处理范围，不作处理，可设定d[4]=0;d[5]=0;d[6]=图一宽*c[3];d[7]=图一高*c[3]
            * */

            List<Mat> mats = setMatImage1(video, c, d);

            if (c[0] != -10)//证明能识别，有楼层信息
                if (mats.Count == 1)
                {//一个字符，必定是数字
                    threadingMethod(mats, c);
                }
                else if (mats.Count == 2)
                {//两个字符，两个数字或一数一箭头
                    if (c[0] == 0)
                    {//c[0]=0代表不是一数一箭头，此处必为两个数字
                        threadingMethod(mats, c);
                    }
                    else if (c[0] == 1)
                    {//一数一箭头
                        Mat mats1 = null;


                        if (mats[0] != null)
                        {//箭头识别
                            mats1 = mats[0];
                            int arr = ArrowRecognition(mats1, c);
                            if (arr == 1)
                            {
                                System.Console.WriteLine("上升");
                            }
                            else if (arr == -1)
                            {
                                System.Console.WriteLine("下降");
                            }
                        }

                        mats.RemoveAt(0);//移除箭头，数字识别
                        threadingMethod(mats, c);

                    }
                }
                else if (mats.Count == 3)
                {//必为一箭头一数字
                    Mat mats1 = null;
                    if (mats[0] != null)
                    {
                        mats1 = mats[0];
                        int arr = ArrowRecognition(mats1, c);
                        if (arr == 1)
                        {
                            Console.WriteLine("上升");
                        }
                        else if (arr == -1)
                        {
                            Console.WriteLine("下降");
                        }
                    }

                    mats.RemoveAt(0);
                    threadingMethod(mats, c);

                }
        }



        //数字与箭头从上到下D:\BaiduNetdiskDownload\test1.mp4
        public List<Mat> setMatImage1(Mat src, float[] c, float[] d)
        {

            //读取图像到矩阵中,取灰度图像
            /*if(src.empty()){
                return new List<Mat>;
            }*/

            c[0] = -10;

            List<Mat> mats = new List<Mat>();

            Rect rect = new Rect((int)d[0], (int)d[1], (int)(d[2] - d[0]), (int)(d[3] - d[1]));
            src = src.SubMat(rect);

          

            //缩放视频图像
            float scale = c[3] * 1f;//10意思是放大10倍
            float width = src.Width;
            float height = src.Height;
            Cv2.Resize(src, src, new Size(width * scale, height * scale));
            //Imgcodecs.imwrite("C:\\Users\\Administrator\\Desktop\\internet.18.jpg",src);
           // Cv2.ImShow("src", src);

            Mat dst = new Mat(src.Size(), src.Type());
            Mat dstDilate = new Mat(src.Size(), src.Type());

            Mat dstErode = new Mat(src.Size(), src.Type());

            Mat srcGaussianBlur = new Mat(src.Size(), src.Type());
            Cv2.GaussianBlur(src, srcGaussianBlur, new Size(c[4], c[4]), 0, 0);//3,3

            //Mat srcCopy = srcGaussianBlur.Clone();
            //Cv2.ImShow("srcCopy", srcCopy);
            Kmeans kmeans = new Kmeans();
           dst = kmeans.Kmeans02(srcGaussianBlur, (int)c[11], (int)c[12], (int)c[4]);//dilateValue, erodeValue, gaussianBlurValue


            // Cv2.InRange(srcGaussianBlur, new Scalar(c[5], c[6], c[7]), new Scalar(c[8], c[9], c[10]), dst);//低(0,100,160)高(120,235,250)
            //Core.inRange(srcGaussianBlur, new Scalar(130, 130, 130), new Scalar(255, 255, 255), dst);
            //Mat dst1 = dst.clone();

            Mat knenel = Cv2.GetStructuringElement(MorphShapes.Ellipse, new Size(3, 3));
            Mat knene2 = Cv2.GetStructuringElement(MorphShapes.Ellipse, new Size(3, 3));
            Cv2.Dilate(dst, dstDilate, knenel, new Point(0, 0), (int)c[11]);
            Cv2.Erode(dstDilate, dstErode, knene2, new Point(0, 0), (int)c[12]);
            //Imgproc.dilate(dstErode,dstErode,knenel,new Point(0,0),1);


            Cv2.GaussianBlur(dstErode, srcGaussianBlur, new Size(7, 7), 0, 0);
            Cv2.Threshold(srcGaussianBlur, srcGaussianBlur, 0, 255, ThresholdTypes.Binary | ThresholdTypes.Otsu);


            //Imgproc.threshold(srcGaussianBlur, srcGaussianBlur, 0, 255, Imgproc.THRESH_BINARY_INV | Imgproc.THRESH_OTSU);

            //Imgcodecs.imwrite(newFileName,srcGaussianBlur);





            // Cv2.ImShow("srcGaussianBlur", srcGaussianBlur);//可删

            //C: \Users\Administrator\source\repos\NixieTubeIdentification2\NixieTubeIdentification2\Program.cs




            try
            {
                Point center = new Point(srcGaussianBlur.Width / 2.0, srcGaussianBlur.Height / 2.0);
                Mat affineTrans = Cv2.GetRotationMatrix2D(center, c[13], 1.0);//23
                                                                              //Imgproc.INTER_NEAREST
                Cv2.WarpAffine(srcGaussianBlur, srcGaussianBlur, affineTrans, srcGaussianBlur.Size(), InterpolationFlags.Nearest);

                //Rect rect1 = new Rect(0, 0, 330, 196);
                Rect rect1 = new Rect((int)d[4], (int)d[5], (int)d[6], (int)d[7]);
                srcGaussianBlur = srcGaussianBlur.SubMat(rect1);
                //缩放视频图像
                float scale1 = c[14] * 1f;//0.5意思是放大0.5倍
                float width1 = srcGaussianBlur.Width;
                float height1 = srcGaussianBlur.Height;
                Cv2.Resize(srcGaussianBlur, srcGaussianBlur, new Size(width1 * scale1, height1 * scale1));


                //Cv2.ImShow(" srcGaussianBlur", srcGaussianBlur);


                // 9 效果展示
                List<Rect> rects = process(srcGaussianBlur, (int)c[15]);



                rects.Sort(new ReComparer1_K(c));




                //为了判断箭头是否存在，定义min，max
                int min = srcGaussianBlur.Width;
                int max = 0;
                //System.out.println("发现 " + rects.size() + " 对象");
                for (int i = 0; i < rects.Count; i++)
                {//
                 // Cv2.Rectangle(srcGaussianBlur, new Point(rects[i].X, rects[i].Y), new Point(rects[i].X + rects[i].Width, rects[i].Y + rects[i].Height), new Scalar(255, 0, 0), 1, LineTypes.AntiAlias, 0);//Imgproc.LINE_AA
                    if (rects[i].X != 0 && rects[i].Y != 0 && rects[i].Height != 0 && rects[i].Width != 0)
                    {
                        int x2 = rects[i].X;
                        int y2 = rects[i].Y;
                        int width2 = rects[i].Width;//640（x1+width1,y1+height1）检测区域
                        int height2 = rects[i].Height;//1136*/
                        Rect rect2 = new Rect(x2, y2, width2, height2);
                        Mat roi = srcGaussianBlur.SubMat(rect2);
                        mats.Add(roi);
                        if (rects[i].Y < min)
                        {
                            min = rects[i].Y;
                        }
                        if (rects[i].Y > max)
                        {
                            max = rects[i].Y;
                        }
                    }
                }

                if (mats.Count > 0)
                {
                    c[0] = 0;
                }
                if (mats.Count == 2)
                {
                    if ((max - min) > c[16])
                    {//80
                        c[0] = 1;//判定有箭头（因为箭头与楼层数字上下关系）
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            src.Release();
            dst.Release();
            dstDilate.Release();
            dstErode.Release();
            srcGaussianBlur.Release();
            return mats;
        }












        //数字与箭头一排且从左到右D:\\0video\\video\\2.mp4
        public void NixieTube(Mat video, float[] c, float[] d)
        {
            /*c={-10,-10,-10,10,3,130,130,130,255,255,255,4,2,23,0.5,300,80,40}
            *d={516, 155, 561, 187,0,0,330,196}
            * c[0]:初始化为-10；c[0] = -10证明不能识别，无楼层信息;; c[0] = 0;代表有楼层信息1,2或3个字符;;c[0] == 1一数字一箭头
            * c[1]:初始化为-10；c[1]=-10楼层信息未识别;;c[1]!=-10,c[1]为楼层信息
            * c[2]:初始化为-10；c[2]=-10未识别箭头信息；c[2]=1或-1返回箭头信息
            * c[3]:首次缩放图片，此处一般为放大,；例如c[3]=10
            * c[4]:例如取3；高斯模糊取值为奇数，3,5,7.......
            * c[5]~c[7]颜色分割低阈值，c[8]~c[10];颜色分割高阈值,例如取值：(130, 130, 130),(255, 255, 255)
            * c[11]图像膨胀数值;例如c[11]=4
            * c[12]图像腐蚀数值；例如c[12]=2
            * c[13]图像逆时针旋转的角度,为了矫正图像,例如23；不需要可设定为0.1
            * c[14]图像缩放程度,例如0.5
            * c[15]数字或箭头的最小面积，过滤无用信息;例如300
            * c[16]当是一个箭头一个数字时，箭头与数字的最小距离；例如80
            *c[17]最高楼层;例如40
            * d[0]~d[3]图像预处理，划定图像处理范围，图一
            * d[4]~d[7]为了过滤处理后的信息，再次缩小处理范围，不作处理，可设定d[4]=0;d[5]=0;d[6]=图一宽*c[3];d[7]=图一高*c[3]
            * */

            List<Mat> mats = setMatImage(video, c, d);

            if (c[0] != -10)//证明能识别，有楼层信息
                if (mats.Count == 1)
                {//一个字符，必定是数字
                    threadingMethod(mats, c);
                }
                else if (mats.Count == 2)
                {//两个字符，两个数字或一数一箭头
                    if (c[0] == 0)
                    {//c[0]=0代表不是一数一箭头，此处必为两个数字
                        threadingMethod(mats, c);
                    }
                    else if (c[0] == 1)
                    {//一数一箭头
                        Mat mats1 = null;


                        if (mats[0] != null)
                        {//箭头识别
                            mats1 = mats[0];
                            int arr = ArrowRecognition(mats1, c);
                            if (arr == 1)
                            {
                                System.Console.WriteLine("上升");
                            }
                            else if (arr == -1)
                            {
                                System.Console.WriteLine("下降");
                            }
                        }

                        mats.RemoveAt(0);//移除箭头，数字识别
                        threadingMethod(mats, c);

                    }
                }
                else if (mats.Count == 3)
                {//必为一箭头一数字
                    Mat mats1 = null;
                    if (mats[0] != null)
                    {
                        mats1 = mats[0];
                        int arr = ArrowRecognition(mats1, c);
                        if (arr == 1)
                        {
                            Console.WriteLine("上升");
                        }
                        else if (arr == -1)
                        {
                            Console.WriteLine("下降");
                        }
                    }

                    mats.RemoveAt(0);
                    threadingMethod(mats, c);

                }

            /*  if(c[1]!=-10){
             *//*int flo=nix.floorIdentification(c,currentDigital);
                        if(flo!=-10)*//*
                System.out.println("########"+c[1]);
            }*/

        }






        //数字与箭头一排且从左到右D:\\0video\\video\\2.mp4
        public List<Mat> setMatImage(Mat src, float[] c, float[] d)
        {

            //读取图像到矩阵中,取灰度图像
            /*if(src.empty()){
                return new List<Mat>;
            }*/

            c[0] = -10;

            List<Mat> mats = new List<Mat>();

            Rect rect = new Rect((int)d[0], (int)d[1], (int)(d[2] - d[0]), (int)(d[3] - d[1]));
            src = src.SubMat(rect);

            //缩放视频图像
            float scale = c[3] * 1f;//10意思是放大10倍
            float width = src.Width;
            float height = src.Height;
            Cv2.Resize(src, src, new Size(width * scale, height * scale));
            //Imgcodecs.imwrite("C:\\Users\\Administrator\\Desktop\\internet.18.jpg",src);


            Mat srcCopy = src.Clone();
            Cv2.ImShow("img", srcCopy);
           // Cv2.WaitKey(20);

            Mat dst = new Mat(src.Size(), src.Type());
            Mat dstDilate = new Mat(src.Size(), src.Type());

            Mat dstErode = new Mat(src.Size(), src.Type());

            Mat srcGaussianBlur = new Mat(src.Size(), src.Type());
           Cv2.GaussianBlur(src, srcGaussianBlur, new Size(c[4], c[4]), 0, 0);//3,3

            Kmeans kmeans = new Kmeans();
            dst = kmeans.Kmeans02(srcGaussianBlur, (int)c[11], (int)c[12], (int)c[4]);//dilateValue, erodeValue, gaussianBlurValue
                                                                                               //Cv2.InRange(srcGaussianBlur, new Scalar(c[5], c[6], c[7]), new Scalar(c[8], c[9], c[10]), dst);//低(0,100,160)高(120,235,250)
                                                                                               // Cv2.ImShow("dst", dst);                                                                                             //Core.inRange(srcGaussianBlur, new Scalar(130, 130, 130), new Scalar(255, 255, 255), dst);
                                                                                               //Mat dst1 = dst.clone();

            Mat knenel = Cv2.GetStructuringElement(MorphShapes.Ellipse, new Size(3, 3));
            Mat knene2 = Cv2.GetStructuringElement(MorphShapes.Ellipse, new Size(3, 3));
            Cv2.Dilate(dst, dstDilate, knenel, new Point(0, 0), (int)c[11]);
            Cv2.Erode(dstDilate, dstErode, knene2, new Point(0, 0), (int)c[12]);
            //Imgproc.dilate(dstErode,dstErode,knenel,new Point(0,0),1);


            Cv2.GaussianBlur(dstErode, srcGaussianBlur, new Size(7, 7), 0, 0);
            Cv2.Threshold(srcGaussianBlur, srcGaussianBlur, 0, 255, ThresholdTypes.Binary | ThresholdTypes.Otsu);
            //Imgproc.threshold(srcGaussianBlur, srcGaussianBlur, 0, 255, Imgproc.THRESH_BINARY_INV | Imgproc.THRESH_OTSU);

            //Imgcodecs.imwrite(newFileName,srcGaussianBlur);





            // Cv2.ImShow("srcGaussianBlur", srcGaussianBlur);//可删

            //C: \Users\Administrator\source\repos\NixieTubeIdentification2\NixieTubeIdentification2\Program.cs




            try
            {
                Point center = new Point(srcGaussianBlur.Width / 2.0, srcGaussianBlur.Height / 2.0);
                Mat affineTrans = Cv2.GetRotationMatrix2D(center, c[13], 1.0);//23
                                                                              //Imgproc.INTER_NEAREST
                Cv2.WarpAffine(srcGaussianBlur, srcGaussianBlur, affineTrans, srcGaussianBlur.Size(), InterpolationFlags.Nearest);

                //Rect rect1 = new Rect(0, 0, 330, 196);
                Rect rect1 = new Rect((int)d[4], (int)d[5], (int)d[6], (int)d[7]);
                srcGaussianBlur = srcGaussianBlur.SubMat(rect1);

               // Cv2.ImShow("srcGaussianBlur", srcGaussianBlur);


               


                //缩放视频图像
                float scale1 = c[14] * 1f;//0.5意思是放大0.5倍
                float width1 = srcGaussianBlur.Width;
                float height1 = srcGaussianBlur.Height;
                Cv2.Resize(srcGaussianBlur, srcGaussianBlur, new Size(width1 * scale1, height1 * scale1));

                // 9 效果展示
                List<Rect> rects = process(srcGaussianBlur, (int)c[15]);



                rects.Sort(new ReComparer_K());

               


                //为了判断箭头是否存在，定义min，max
                int min = srcGaussianBlur.Width;
                int max = 0;
                //System.out.println("发现 " + rects.size() + " 对象");
                for (int i = 0; i < rects.Count; i++)
                {//
                 // Cv2.Rectangle(srcGaussianBlur, new Point(rects[i].X, rects[i].Y), new Point(rects[i].X + rects[i].Width, rects[i].Y + rects[i].Height), new Scalar(255, 0, 0), 1, LineTypes.AntiAlias, 0);//Imgproc.LINE_AA
                    if (rects[i].X != 0 && rects[i].Y != 0 && rects[i].Height != 0 && rects[i].Width != 0)
                    {
                        int x2 = rects[i].X;
                        int y2 = rects[i].Y;
                        int width2 = rects[i].Width;//640（x1+width1,y1+height1）检测区域
                        int height2 = rects[i].Height;//1136*/
                        Rect rect2 = new Rect(x2, y2, width2, height2);
                        Mat roi = srcGaussianBlur.SubMat(rect2);
                        mats.Add(roi);
                        if (rects[i].X < min)
                        {
                            min = rects[i].X;
                        }
                        if (rects[i].X > max)
                        {
                            max = rects[i].X;
                        }
                    }
                }

                if (mats.Count > 0)
                {
                    c[0] = 0;
                }
                if (mats.Count == 2)
                {
                    if ((max - min) > c[16])
                    {//80
                        c[0] = 1;//判定一个箭头一个数字
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            Cv2.WaitKey(1);

            src.Release();
            dst.Release();
            dstDilate.Release();
            dstErode.Release();
           // srcGaussianBlur.Release();
            return mats;
        }




        public  List<Rect> process(Mat video, int digitalArea)
        {
            // 1 跟踪物体在图像中的位置
            Mat[] contours;

            // 2找出图像中物体的位置
            Cv2.FindContours(video, out contours, new Mat(),
                RetrievalModes.External, ContourApproximationModes.ApproxSimple, new Point(0, 0));
            // 3 对象结果
            List<Rect> rects = new List<Rect>();
            Rect rect;
            if (contours.Length > 0)
            {// 4.1 如果发现图像
                for (int i = 0; i < contours.Length; i++)
                {
                    rect = new Rect();
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
                rect = new Rect();
                rect.X = rect.Y = rect.Width = rect.Height = 0;
                rects.Add(rect);
            }
            return rects;
        }







        //穿线法
        public void threadingMethod(List<Mat> mats, float[] c1)
        {
            //c1与c为同一数组
            //System.out.println("当前楼层：");//可删

            int num = 0;
            float temp = -10;
            c1[1] = -10;

            for (int k = 0; k < mats.Count; k++)
            {
                num++;
                Mat src = mats[k];

                /*使src白底黑字*/

                /*          //转灰度图片
                          Imgproc.cvtColor(src, src, Imgproc.COLOR_BGR2GRAY);
                          //二值取反,使图片白纸黑字
                          Imgproc.threshold(src, src, 0, 255, Imgproc.THRESH_BINARY_INV | Imgproc.THRESH_OTSU);*/

                if (2 * src.Cols < src.Rows)
                {//3 * src.cols() < src.rows()
                    temp = 1;//System.out.print(1);
                }
                else
                {

                    //竖线
                    int x_half = src.Cols / 2;
                    //上横线
                    int y_one_third = src.Rows / 3;
                    //下横线
                    int y_two_third = src.Rows * 2 / 3;
                    //每段数码管，0灭，1亮
                    int a = 0, b = 0, c = 0, d = 0, e = 0, f = 0, g = 0;

                    //竖线识别a,g,d段
                    for (int i = 0; i < src.Rows; i++)
                    {

                        if (i < y_one_third)
                        {
                            if (src.At<Vec3b>(i, x_half)[0] == 255) a = 1;
                        }
                        else if (i > y_one_third && i < y_two_third)
                        {
                            if (src.At<Vec3b>(i, x_half)[0] == 255) g = 1;
                        }
                        else
                        {
                            if (src.At<Vec3b>(i, x_half)[0] == 255) d = 1;
                        }
                    }

                    //上横线识别：
                    for (int j = 0; j < src.Cols; j++)
                    {

                        //f
                        if (j < x_half)
                        {
                            if (src.At<Vec3b>(y_one_third, j)[0] == 255) f = 1;
                        }
                        //b
                        else
                        {
                            if (src.At<Vec3b>(y_one_third, j)[0] == 255) b = 1;
                        }
                    }

                    //下横线识别：
                    for (int j = 0; j < src.Cols; j++)
                    {

                        //e
                        if (j < x_half)
                        {
                            if (src.At<Vec3b>(y_two_third, j)[0] == 255) e = 1;
                        }
                        //c
                        else
                        {
                            if (src.At<Vec3b>(y_two_third, j)[0] == 255) c = 1;
                        }
                    }


                    //七段管组成的数字
                    if (a == 1 && b == 1 && c == 1 && d == 1 && e == 1 && f == 1 && g == 0)
                    {
                        temp = 0;//System.out.print(0);
                    }
                    else if (a == 1 && b == 1 && c == 0 && d == 1 && e == 1 && f == 0 && g == 1)
                    {
                        temp = 2;//System.out.print(2);
                    }
                    else if (a == 1 && b == 1 && c == 1 && d == 1 && e == 0 && f == 0 && g == 1)
                    {
                        temp = 3;//System.out.print(3);
                    }
                    else if (a == 0 && b == 1 && c == 1 && d == 0 && e == 0 && f == 1 && g == 1)
                    {
                        temp = 4;//System.out.print(4);
                    }
                    else if (a == 1 && b == 0 && c == 1 && d == 1 && e == 0 && f == 1 && g == 1)
                    {
                        temp = 5;//System.out.print(5);
                    }
                    else if (a == 1 && b == 0 && c == 1 && d == 1 && e == 1 && f == 1 && g == 1)
                    {
                        temp = 6;//System.out.println(6);
                    }
                    else if (a == 1 && b == 1 && c == 1 && d == 0 && e == 0 && f == 0 && g == 0)
                    {
                        temp = 7;//System.out.print(7);
                    }
                    else if (a == 1 && b == 1 && c == 1 && d == 1 && e == 1 && f == 1 && g == 1)
                    {
                        temp = 8;//System.out.print(8);
                    }
                    else if (a == 1 && b == 1 && c == 1 && d == 1 && e == 0 && f == 1 && g == 1)
                    {
                        temp = 9;//System.out.print(9);
                    }
                    else
                    {
                        temp = -10;
                        //System.out.print("未识别");
                    }


                }

                src.Release();


                if (temp != -10)
                {
                    if (num == 1)
                    {
                        c1[1] = temp;
                    }
                    else if (num == 2)
                    {
                        c1[1] = c1[1] * 10 + temp;
                    }
                }
                else if (temp == -10)
                {
                    c1[1] = temp;
                }
                if (c1[1] > c1[17])
                {//c1[17]代表最高楼层
                    c1[1] = -10;
                }


            }

            // System.out.println();

        }


        //识别箭头方向
        public int ArrowRecognition(Mat srcGaussianBlur, float[] c)
        {


            c[2] = -10;


            Mat roi = srcGaussianBlur.Clone();

            double numBlackUp = 0;
            double numBlackDown = 0;
            for (int i = 0; i < (int)(roi.Height / 2); i++)
            {
                for (int j = 0; j < roi.Width; j++)
                {

                    if (roi.At<Vec3b>(i, j)[0] == 0)
                        numBlackUp++;
                }
            }
            for (int i = (int)(roi.Height / 2); i < roi.Height; i++)
            {
                for (int j = 0; j < roi.Width; j++)
                {

                    if (roi.At<Vec3b>(i, j)[0] == 0)
                        numBlackDown++;
                }
            }

            numBlackUp = numBlackUp / ((int)(roi.Height / 2) * roi.Width);
            numBlackDown = numBlackDown / (roi.Height * roi.Width - (int)(roi.Height / 2) * roi.Width);
            if (numBlackUp > numBlackDown)
            {
                c[2] = 1;
                return 1;
            }
            else
            {
                c[2] = -1;
                return -1;
            }

        }



    }
}
