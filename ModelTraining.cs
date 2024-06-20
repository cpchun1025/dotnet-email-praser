using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.IO;

public class EmailSubjectData
{
    [LoadColumn(0)]
    public string Subject { get; set; }

    [LoadColumn(1)]
    public string Label { get; set; }
}

public class EmailSubjectPrediction
{
    [ColumnName("PredictedLabel")]
    public string PredictedLabel { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        var mlContext = new MLContext();

        // Load data
        var dataPath = "path/to/your/trainingData.csv"; // Path to your training data file
        var dataView = mlContext.Data.LoadFromTextFile<EmailSubjectData>(dataPath, separatorChar: ',', hasHeader: true);

        // Define data preparation and training pipeline
        var dataProcessPipeline = mlContext.Transforms.Conversion.MapValueToKey("Label")
            .Append(mlContext.Transforms.Text.FeaturizeText("Subject", "Features"))
            .Append(mlContext.Transforms.Concatenate("Features"))
            .AppendCacheCheckpoint(mlContext);

        var trainer = mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features")
            .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

        var trainingPipeline = dataProcessPipeline.Append(trainer);

        // Train the model
        var trainedModel = trainingPipeline.Fit(dataView);

        // Save the model
        var modelPath = Path.Combine(Environment.CurrentDirectory, "model.zip");
        mlContext.Model.Save(trainedModel, dataView.Schema, modelPath);

        Console.WriteLine($"Model saved to: {modelPath}");
    }
}