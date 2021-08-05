using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using MeterReadings.ApplicationLogic.Interfaces;
using Microsoft.AspNetCore.Http;

namespace MeterReadings.ApplicationLogic
{
    public class CsvFileReader : ICsvFileReader
    {
        public IEnumerable<T> ReadAndConvertFromCsv<T>(IFormFile uploadedFile)
        {
            using var streamReader = new StreamReader(uploadedFile.OpenReadStream());
            using var csvReader = new CsvReader(streamReader, new CultureInfo("en-GB"));
            while (csvReader.Read())
            {
                yield return csvReader.GetRecord<T>();
            }
        }
    }
}