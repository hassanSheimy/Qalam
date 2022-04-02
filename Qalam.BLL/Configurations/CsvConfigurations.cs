using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Qalam.BLL.ViewModels;
using Qalam.Common.Enums;
using Qalam.Common.Extensions;
using Qalam.Common.Helper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Qalam.BLL.Configurations
{
    /// <summary>
    /// Mapper Class to map object properties to csv columns
    /// </summary>
    public sealed class QuestionsMapper : ClassMap<QuestionViewModel>
    {
        public QuestionsMapper()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.Text).Name("Text");
            Map(m => m.DifficultyLevel).Name("Difficulty Level").TypeConverter<CsvConverter<int>>();
            
            // map all other attributes as a answers
            Map(m => m.Answers).ConvertUsing(row =>
            {
                var attributes = new List<QuestionAnswerViewModel>();
                foreach (var header in row.Context.HeaderRecord)
                {
                    if (!String.IsNullOrEmpty(row.GetField(header)) && (typeof(QuestionViewModel).GetProperty(header.RemoveAllSpaces()) == null))
                    {
                        attributes.Add(new QuestionAnswerViewModel {
                            Answer = row.GetField(header),
                            IsCorrect = header == "Correct Answer"
                        });
                    }
                }
                return attributes;
            });
        }
    }
}
