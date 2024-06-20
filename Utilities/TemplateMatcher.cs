using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class TemplateMatcher
{
    private readonly Dictionary<Regex, IEmailTemplate> _regexTemplateMap;

    public TemplateMatcher()
    {
        _regexTemplateMap = new Dictionary<Regex, IEmailTemplate>();
    }

    public void AddTemplate(Regex regex, IEmailTemplate template)
    {
        _regexTemplateMap[regex] = template;
    }

    public IEmailTemplate MatchTemplate(string subject)
    {
        foreach (var entry in _regexTemplateMap)
        {
            if (entry.Key.IsMatch(subject))
            {
                return entry.Value;
            }
        }
        return null;
    }
}