using CsvHelper;
using CsvHelper.Configuration;
using Qalam.Common.Enums;
using Qalam.Common.Helper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Qalam.BLL.Managers
{
    public class CsvManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TFile"> Data type of the rows which will read from the file </typeparam>
        /// <typeparam name="TMapper"> The Mapping Class which will use to read the file </typeparam>
        /// <param name="streamFile"></param>
        /// <exception cref="HeaderValidationException"> There is missing values in the headers </exception>
        /// <returns></returns>
        public List<TFile> ReadFromFile<TFile, TMapper>(Stream streamFile) where TMapper : ClassMap
        {
            try
            {
                using var csvReader = new CsvReader(new StreamReader(streamFile), CultureInfo.InvariantCulture);
                // Applay csv configurations
                csvReader.Configuration.RegisterClassMap<TMapper>();
                csvReader.Configuration.IgnoreBlankLines = true;

                // Read all records without the headers
                var records = csvReader.GetRecords<TFile>().ToList();

                // if read done successfully then return the records
                return records;
            }
            // if any unexcpected exception happened
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TFile"></typeparam>
        /// <param name="fileData"></param>
        /// <returns></returns>
        public MemoryStream WriteToFile<TFile>(List<TFile> fileData)
        {
            try
            {
                using var memoryStream = new MemoryStream();
                using var streamWriter = new StreamWriter(memoryStream);
                using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
                // Write all records with header
                csvWriter.WriteRecords<TFile>(fileData);

                streamWriter.Flush();

                // if writing done successfully then return the memory stream of this file
                return memoryStream;
            }
            // if any unexcpected exception happened
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
