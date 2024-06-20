public class EmailSubjectData
{
    public string Subject { get; set; }
    public string Template { get; set; }
}

public class EmailSubjectPrediction
{
    [ColumnName("PredictedLabel")]
    public string PredictedTemplate { get; set; }
}