using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class TemplateMatcher
{
    private readonly Dictionary<Regex, EmailTemplate> _templates = new Dictionary<Regex, EmailTemplate>();

    public void AddTemplate(Regex regex, EmailTemplate template)
    {
        _templates[regex] = template;
    }

    public EmailTemplate Match(string subject)
    {
        foreach (var kvp in _templates)
        {
            if (kvp.Key.IsMatch(subject))
            {
                return kvp.Value;
            }
        }

        return null; // Handle no match scenario as needed
    }
}