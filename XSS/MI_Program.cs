//using System;
//using System.Data;
//using Microsoft.ML;
//using Microsoft.ML.Data;
//namespace XSS
//{
//    public class ApiRequest
//    {
//        [LoadColumn(0)]
//        public float RequestCount { get; set; }
//        [LoadColumn(1)]
//        public string IpAddress { get; set; } // Not used in modeling, but loaded for completeness.
//        [LoadColumn(2)]
//        public float TimeBetweenRequests { get; set; }
//    }

//    public class ApiRequestPrediction
//    {
//        //[VectorType]
//        [ColumnName("Score")]
//        public float Score { get; set; }
//        public bool PredictedLabel { get; set; }
//    }

//    public class mlProgram
//    {
//        public static void Main(string[] args)
//        {
//            var context = new MLContext();
//            var apiRequests = new List<ApiRequest>
//        {
//            new ApiRequest { RequestCount = 500, IpAddress = "192.168.1.1", TimeBetweenRequests = 1.2f },
//            new ApiRequest { RequestCount = 800, IpAddress = "192.168.1.2", TimeBetweenRequests = 1.5f },
//            new ApiRequest { RequestCount = 300, IpAddress = "192.168.1.3", TimeBetweenRequests = 2f },
//            new ApiRequest { RequestCount = 1000, IpAddress = "192.168.1.1", TimeBetweenRequests = 0.5f },
//            new ApiRequest { RequestCount = 400, IpAddress = "192.168.1.4", TimeBetweenRequests = 1.8f },
//            // ... continue in this pattern for all your data points ...
//            new ApiRequest { RequestCount = 540, IpAddress = "192.168.1.26", TimeBetweenRequests = 1.9f },
//            new ApiRequest { RequestCount = 790, IpAddress = "192.168.1.27", TimeBetweenRequests = 1.7f },
//            new ApiRequest { RequestCount = 550, IpAddress = "192.168.1.28", TimeBetweenRequests = 1.6f },
//            new ApiRequest { RequestCount = 800, IpAddress = "192.168.1.29", TimeBetweenRequests = 1.8f }
//        };

//            IDataView data = context.Data.LoadFromEnumerable(apiRequests);
//            var preview = data.Preview();
//            Console.WriteLine($"Loaded {preview.RowView.Length} rows.");
//            foreach (var row in preview.RowView)
//            {
//                foreach (var column in row.Values)
//                {
//                    Console.Write($"{column.Value} \t");
//                }
//                Console.WriteLine();
//            }


//            // Split the data into training and testing sets
//            var trainTestSplit = context.Data.TrainTestSplit(data);

//            // Define the pipeline
//            var pipeline = context.Transforms.Concatenate("Features", "RequestCount", "TimeBetweenRequests")
//                .Append(context.Transforms.NormalizeMinMax("Features"))
//                .Append(context.AnomalyDetection.Trainers.RandomizedPca(rank: 1, ensureZeroMean: false));

//            // Train the model
//            var model = pipeline.Fit(trainTestSplit.TrainSet);


//            // Test a prediction
//            var predictionEngine = context.Model.CreatePredictionEngine<ApiRequest, ApiRequestPrediction>(model);


//            //predication
//            var prediction = predictionEngine.Predict(

//                new ApiRequest { RequestCount = 550, TimeBetweenRequests = 1.6f, IpAddress = "192.168.1.32" }
//                //new ApiRequest { RequestCount = 5000, TimeBetweenRequests = 0.1f, IpAddress = "192.168.1.30" }


//            );

//            Console.WriteLine($"Is anomaly: {prediction.PredictedLabel}  score {prediction.Score}");
//        }
//    }
//}