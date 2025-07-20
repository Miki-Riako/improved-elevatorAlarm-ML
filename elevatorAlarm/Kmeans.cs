using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elevatorAlarm
{
   public class Kmeans
    {
        /*static void Main(string[] args)
        {
            Kmeans01();
        }*/


        public  Mat Kmeans02(Mat src1, int dilateValue, int erodeValue, int gaussianBlurValue)
        {
            Mat src = src1.Clone();
            var columnVector = src.Reshape(cn: 3, rows: src.Rows * src.Cols);

            // convert to floating point, it is a requirement of the k-means method of OpenCV.
            var samples = new Mat();
            columnVector.ConvertTo(samples, MatType.CV_32FC3);

            var clustersCount = 2;
            var bestLabels = new Mat();
            var centers = new Mat();
            Cv2.Kmeans(
                data: samples,
                k: clustersCount,
                bestLabels: bestLabels,
                criteria:
                    new TermCriteria(type: CriteriaType.Eps | CriteriaType.MaxIter, maxCount: 10, epsilon: 0.1),
                  // new TermCriteria(type:CriteriaTypes.Eps|CriteriaTypes.MaxIter, maxCount: 10, epsilon: 0.1),
                attempts: 3,
                flags: KMeansFlags.PpCenters,
                centers: centers);

            /*
                        var clusteredImage = new Mat(src.Rows, src.Cols, src.Type());
                        for (var size = 0; size < src.Cols * src.Rows; size++)
                        {
                            var clusterIndex = bestLabels.At<int>(0, size);
                            var newPixel = new Vec3b
                            {
                                Item0 = (byte)(centers.At<float>(clusterIndex, 0)),
                                Item1 = (byte)(centers.At<float>(clusterIndex, 1)),
                                Item2 = (byte)(centers.At<float>(clusterIndex, 2))
                            };
                            clusteredImage.Set(size / src.Cols, size % src.Cols, newPixel);
                        }*/
            Scalar[] colors =
            {
                new Scalar(255, 255, 255),
                new Scalar(0, 0, 0),

                new Scalar(255, 100, 100),
                new Scalar(255, 0, 255),
                new Scalar(0, 255, 255)
            };

            int width = src.Cols;
            int height = src.Rows;
            // 显示图像分割后的结果，一维转多维
            Mat result = Mat.Zeros(src.Size(), src.Type());
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    int index = row * width + col;
                    var clusterIdx = bestLabels.At<int>(index, 0);
                    Vec3b color = new Vec3b
                    {
                        Item0 = (byte)colors[clusterIdx][0],
                        Item1 = (byte)colors[clusterIdx][1],
                        Item2 = (byte)colors[clusterIdx][2]
                    };
                    result.Set<Vec3b>(row, col, color);
                }
            }
           // Cv2.ImShow("img", result);
            //确保黑底白字
            double numBlack = 0, numWhite = 0;
            for (int i = 0; i < (int)result.Height; i++)
            {
                for (int j = 0; j < result.Width; j++)
                {

                    if (result.At<Vec3b>(i, j)[0] == 255)
                    {
                        numWhite++;
                    }
                    else if (result.At<Vec3b>(i, j)[0] == 0)
                    {
                        numBlack++;
                    }
                }
            }
            if (numWhite > numBlack)
            {
                for (int i = 0; i < (int)result.Height; i++)
                {
                    for (int j = 0; j < result.Width; j++)
                    {
                        //读取源图的像素
                        int b = result.At<Vec3b>(i, j)[0];
                        int g = result.At<Vec3b>(i, j)[1];
                        int r = result.At<Vec3b>(i, j)[2];

                        Vec3b color = new Vec3b
                        {
                            Item0 = (byte)(255 - b), //反转像素   (byte)( Math.Max(r, Math.Max(b, g)));
                            Item1 = (byte)(255 - g), //                 (byte)(Math.Max(r, Math.Max(b, g)));
                            Item2 = (byte)(255 - r) //                   (byte)(Math.Max(r, Math.Max(b, g)));
                        };
                        /*  
                         Vec3b color = new Vec3b //反转像素
                        {
                           Item0 = (byte)Math.Abs(src.Get<Vec3b>(row, col).Item0 - 255),
                           Item1 = (byte)Math.Abs(src.Get<Vec3b>(row, col).Item1 - 255),
                           Item2 = (byte)Math.Abs(src.Get<Vec3b>(row, col).Item2 - 255)
                        };
                        */
                        //Math.Max(r, Math.Max(b, g));取灰度，min也可以，但是亮度不同

                        //赋值
                        result.Set<Vec3b>(i, j, color);

                    }
                }
            }
            //Cv2.ImShow("clustersCount", result);

             result = result.CvtColor(ColorConversionCodes.BGR2GRAY);
            //缩放视频图像
            //float scale1 = 1 * 1f;//10意思是放大10倍
            //float width1 = result.Width;
            //float height1 = result.Height;
            //Cv2.Resize(result, result, new OpenCvSharp.Size(width1 * scale1, height1 * scale1));
           

            //高斯滤波
            //Cv2.GaussianBlur(result, result, new OpenCvSharp.Size(gaussianBlurValue, gaussianBlurValue), 0, 0);
          
            Mat knenel = Cv2.GetStructuringElement(MorphShapes.Ellipse, new OpenCvSharp.Size(3, 3));
            Mat knene2 = Cv2.GetStructuringElement(MorphShapes.Ellipse, new OpenCvSharp.Size(3, 3));
            Cv2.Dilate(result, result, knenel, new OpenCvSharp.Point(0, 0), dilateValue);//膨胀4
            Cv2.Erode(result, result, knene2, new OpenCvSharp.Point(0, 0), erodeValue);//腐蚀3

            //二值化(黑底白字)
            Cv2.Threshold(result, result, 0, 255, ThresholdTypes.Binary | ThresholdTypes.Otsu);
             //Cv2.ImShow(" srcVivideo123", result);
            //src.Release();
            //knene2.Release();
            // knenel.Release();
            return result;
        }





































        /*public  void Kmeans01()
        {
            Mat video = new Mat("C:/Users/Administrator/Desktop/5.png", ImreadModes.AnyDepth | ImreadModes.AnyColor);
            //Cv2.ImShow("img", video);
            *//*OpenCvSharp.Rect rect = new OpenCvSharp.Rect(156, 27, 177, 37);
        
            Mat src1 = video.SubMat(rect);
            Mat src = src1.Clone();*//*
            Mat src = video.Clone();

            // Cv2.Blur(src, src, new Size(3,3));//D:\0Aproject\C#\knean_20210627\kmeans_1\kmeans_1.sln


            // Converts the MxNx3 image into a Kx3 matrix where K=MxN and
            // each row is now a vector in the 3-D space of RGB.
            // change to a Mx3 column vector (M is number of pixels in image)
            var columnVector = src.Reshape(cn: 3, rows: src.Rows * src.Cols);

            // convert to floating point, it is a requirement of the k-means method of OpenCV.
            var samples = new Mat();
            columnVector.ConvertTo(samples, MatType.CV_32FC3);

            var clustersCount = 2;
            var bestLabels = new Mat();
            var centers = new Mat();
            Cv2.Kmeans(
                data: samples,
                k: clustersCount,
                bestLabels: bestLabels,
                criteria:
                    new TermCriteria(type: CriteriaType.Eps | CriteriaType.MaxIter, maxCount: 10, epsilon: 0.1),
                attempts: 3,
                flags: KMeansFlags.PpCenters,
                centers: centers);

*//*
            var clusteredImage = new Mat(src.Rows, src.Cols, src.Type());
            for (var size = 0; size < src.Cols * src.Rows; size++)
            {
                var clusterIndex = bestLabels.At<int>(0, size);
                var newPixel = new Vec3b
                {
                    Item0 = (byte)(centers.At<float>(clusterIndex, 0)),
                    Item1 = (byte)(centers.At<float>(clusterIndex, 1)),
                    Item2 = (byte)(centers.At<float>(clusterIndex, 2))
                };
                clusteredImage.Set(size / src.Cols, size % src.Cols, newPixel);
            }*//*


            Scalar[] colors =
        {
                new Scalar(255, 255, 255),
                new Scalar(0, 0, 0),
                
                new Scalar(255, 100, 100),
                new Scalar(255, 0, 255),
                new Scalar(0, 255, 255)
            };

            int width = src.Cols;
            int height = src.Rows;
            // 显示图像分割后的结果，一维转多维
            Mat result = Mat.Zeros(src.Size(), src.Type());
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    int index = row * width + col;
                    var clusterIdx = bestLabels.At<int>(index, 0);
                    Vec3b color = new Vec3b
                    {
                        Item0 = (byte)colors[clusterIdx][0],
                        Item1 = (byte)colors[clusterIdx][1],
                        Item2 = (byte)colors[clusterIdx][2]
                    };
                    result.Set<Vec3b>(row, col, color);
                }
            }
            

            //确保黑底白字
            double numBlack = 0, numWhite = 0;
            for (int i = 0; i < (int)result.Height; i++)
            {
                for (int j = 0; j < result.Width; j++)
                {

                    if (result.At<Vec3b>(i, j)[0] == 255)
                    {
                        numWhite++;
                    }
                    else if (result.At<Vec3b>(i, j)[0] == 0)
                    {
                        numBlack++;
                    }
                }
            }

            if (numWhite > numBlack)
            {
                for (int i = 0; i < (int)result.Height; i++)
                {
                    for (int j = 0; j < result.Width; j++)
                    {
                        //读取源图的像素
                        int b = result.At<Vec3b>(i, j)[0];
                        int g = result.At<Vec3b>(i, j)[1];
                        int r = result.At<Vec3b>(i, j)[2];

                        Vec3b color = new Vec3b
                        {
                            Item0 = (byte)(255 - b), //反转像素   (byte)( Math.Max(r, Math.Max(b, g)));
                            Item1 = (byte)(255 - g), //                 (byte)(Math.Max(r, Math.Max(b, g)));
                            Item2 = (byte)(255 - r) //                   (byte)(Math.Max(r, Math.Max(b, g)));
                        };
                        *//*  
                         Vec3b color = new Vec3b //反转像素
                        {
                           Item0 = (byte)Math.Abs(src.Get<Vec3b>(row, col).Item0 - 255),
                           Item1 = (byte)Math.Abs(src.Get<Vec3b>(row, col).Item1 - 255),
                           Item2 = (byte)Math.Abs(src.Get<Vec3b>(row, col).Item2 - 255)
                        };
                        *//*
                        //Math.Max(r, Math.Max(b, g));取灰度，min也可以，但是亮度不同

                        //赋值
                        result.Set<Vec3b>(i, j, color);

                    }
                }
            }

            Cv2.ImShow("clustersCount", result);


            // Cv2.ImShow("clustersCount", clusteredImage);
            //Cv2.ImShow(string.Format("Clustered Image [k:{0}]", clustersCount), clusteredImage);
            Cv2.WaitKey(1); // do events






            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }

*/










































        /* private void example02()
         {
             var src = new Mat("C:/Users/Administrator/Desktop/1.png", ImreadModes.AnyDepth | ImreadModes.AnyColor);
             Cv2.ImShow("Source", src);
             Cv2.WaitKey(1); // do events

             //Cv2.Blur(src, src, new Size(9,9));D:\0Aproject\C#\knean_20210627\kmeans_1\kmeans_1.sln
             //Cv2.ImShow("Blurred Image", src);D:\0Aproject\C#\elevatorAlarm20210402\elevatorAlarm\
             Cv2.WaitKey(1); // do eventsDllNotFoundException

             // Converts the MxNx3 image into a Kx3 matrix where K=MxN and
             // each row is now a vector in the 3-D space of RGB.
             // change to a Mx3 column vector (M is number of pixels in image)
             var columnVector = src.Reshape(cn: 3, rows: src.Rows * src.Cols);

             // convert to floating point, it is a requirement of the k-means method of OpenCV.
             var samples = new Mat();
             columnVector.ConvertTo(samples, MatType.CV_32FC3);

             var clustersCount = 2; 
                 var bestLabels = new Mat();
                 var centers = new Mat();
                 Cv2.Kmeans(
                     data: samples,
                     k: clustersCount,
                     bestLabels: bestLabels,
                     criteria:
                         new TermCriteria(type:CriteriaType.Eps|CriteriaType.MaxIter , maxCount: 10, epsilon:0.1),
                     attempts: 3,
                     flags: KMeansFlags.PpCenters,
                     centers: centers);


                 var clusteredImage = new Mat(src.Rows, src.Cols, src.Type());
                 for (var size = 0; size < src.Cols * src.Rows; size++)
                 {
                     var clusterIndex = bestLabels.At<int>(0, size);
                     var newPixel = new Vec3b
                     {
                         Item0 = (byte)(centers.At<float>(clusterIndex, 0)), 
                         Item1 = (byte)(centers.At<float>(clusterIndex, 1)), 
                         Item2 = (byte)(centers.At<float>(clusterIndex, 2)) 
                     };
                     clusteredImage.Set(size / src.Cols, size % src.Cols, newPixel);
                 }


                 Scalar[] colors =
             {
                 new Scalar(0, 0, 255),
                 new Scalar(0, 255, 0),
                 new Scalar(255, 100, 100),
                 new Scalar(255, 0, 255),
                 new Scalar(0, 255, 255)
             };

                 int width = src.Cols;
                 int height = src.Rows;
                 // 显示图像分割后的结果，一维转多维
                 Mat result = Mat.Zeros(src.Size(), src.Type());
                 int dims = src.Channels();
                 for (int row = 0; row < height; row++)
                 {
                     for (int col = 0; col < width; col++)
                     {
                         int index = row * width + col;
                         var clusterIdx = bestLabels.At<int>(index, 0);
                         Vec3b color = new Vec3b
                         {
                             Item0 = (byte)colors[clusterIdx][0],
                             Item1 = (byte)colors[clusterIdx][1],
                             Item2 = (byte)colors[clusterIdx][2]
                         };
                         result.Set<Vec3b>(row, col, color);
                     }
                 }
                 Cv2.ImShow("img", result);




                 Cv2.ImShow("clustersCount",clusteredImage);
                 //Cv2.ImShow(string.Format("Clustered Image [k:{0}]", clustersCount), clusteredImage);
                 Cv2.WaitKey(1); // do events






             Cv2.WaitKey(0);
             Cv2.DestroyAllWindows();
         }
 */

       /* private static void example01()
        {
            using (var window = new Window("Clusters", flags: WindowMode.AutoSize | WindowMode.FreeRatio))
            {
                const int maxClusters = 5;
                var rng = new RNG(state: (ulong)DateTime.Now.Ticks);

                for (; ; )
                {
                    var clustersCount = rng.Uniform(a: 2, b: maxClusters + 1);
                    var samplesCount = rng.Uniform(a: 1, b: 1001);

                    var points = new Mat(rows: samplesCount, cols: 1, type: MatType.CV_32FC2);
                    clustersCount = Math.Min(clustersCount, samplesCount);

                    var img = new Mat(rows: 500, cols: 500, type: MatType.CV_8UC3, s: Scalar.All(0));

                    // generate random sample from multi-gaussian distribution
                    for (var k = 0; k < clustersCount; k++)
                    {
                        var pointChunk = points.RowRange(
                                startRow: k * samplesCount / clustersCount,
                                endRow: (k == clustersCount - 1)
                                    ? samplesCount
                                    : (k + 1) * samplesCount / clustersCount);

                        var center = new Point
                        {
                            X = rng.Uniform(a: 0, b: img.Cols),
                            Y = rng.Uniform(a: 0, b: img.Rows)
                        };
                        rng.Fill(
                            mat: pointChunk,
                            distType: DistributionType.Normal,
                            a: new Scalar(center.X, center.Y),
                            b: new Scalar(img.Cols * 0.05f, img.Rows * 0.05f));
                    }

                    Cv2.RandShuffle(dst: points, iterFactor: 1, rng: rng);

                    var labels = new Mat();
                    var centers = new Mat(rows: clustersCount, cols: 1, type: points.Type());
                    Cv2.Kmeans(
                        data: points,
                        k: clustersCount,
                        bestLabels: labels,
                        criteria: new TermCriteria(CriteriaType.Eps | CriteriaType.MaxIter, 10, 1.0),
                        attempts: 3,
                        flags: KMeansFlags.PpCenters,
                        centers: centers);


                    Scalar[] colors =
                    {
                       new Scalar(0, 0, 255),
                       new Scalar(0, 255, 0),
                       new Scalar(255, 100, 100),
                       new Scalar(255, 0, 255),
                       new Scalar(0, 255, 255)
                    };

                    for (var i = 0; i < samplesCount; i++)
                    {
                        var clusterIdx = labels.At<int>(i);
                        Point ipt = (Point)points.At<Point2f>(i);

                        Cv2.Circle(
                            img: img,
                            center: ipt,
                            radius: 2,
                            color: colors[clusterIdx],
                            lineType: LineTypes.AntiAlias,
                            thickness: 1);
                    }

                    window.Image = img;

                    var key = (char)Cv2.WaitKey();
                    if (key == 27 || key == 'q' || key == 'Q') // 'ESC'
                    {
                        break;
                    }
                }
            }
        }*/
    }
}
