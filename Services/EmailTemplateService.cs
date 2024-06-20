using System;

public class EmailTemplateService
{
    private readonly TemplateMatcher _templateMatcher;
    private readonly PredictionEngine<EmailSubjectData, EmailSubjectPrediction> _predictionEngine;

    public EmailTemplateService(TemplateMatcher templateMatcher, PredictionEngine<EmailSubjectData, EmailSubjectPrediction> predictionEngine)
    {
        _templateMatcher = templateMatcher;
        _predictionEngine = predictionEngine;
    }

    public string ProcessEmail(string subject, string body)
    {
        var emailSubjectData = new EmailSubjectData { Subject = subject };
        var prediction = _predictionEngine.Predict(emailSubjectData);

        var matchedTemplate = _templateMatcher.Match(prediction.PredictedLabel);
        return matchedTemplate?.Process(body) ?? "No matching template found.";
    }
}