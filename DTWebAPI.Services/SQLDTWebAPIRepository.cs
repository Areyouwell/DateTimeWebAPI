using DTWebAPI.Models;
using DTWebAPI.Services.AddClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NReco.Csv;
using System.Globalization;


namespace DTWebAPI.Services
{
    public class SQLDTWebAPIRepository : IDateInfRepository
    {
        private readonly AppDbContext _context;

        public SQLDTWebAPIRepository(AppDbContext context)
        {
            _context = context;
        }

        public int GetFileAndParse(IFormFile? file, string path)
        {
            IntermediateResult interRes = new IntermediateResult();
            DataValidation dv = new DataValidation();
            List<Value> valueList = new List<Value>();
            FileInf fileInf = GetOrAddFile(file.FileName);

            CopyFile(path + file.FileName, file);

            using (var streamRdr = new StreamReader(file.FileName))
            {
                var csvReader = new CsvReader(streamRdr, ";");
                while (csvReader.Read())
                {
                    DateTime val1 = DateTime.ParseExact(csvReader[0], "yyyy-MM-dd_hh-mm-ss", CultureInfo.InvariantCulture);
                    int val2 = int.Parse(csvReader[1]);
                    double val3 = double.Parse(csvReader[2]);

                    if (dv.IsFullDateIncorrect(val1) || dv.IsTimeInSecLessThanZero(val2) || dv.IsIndexLessThanZero(val3)) return -1;

                    valueList.Add(new Value { FullDate = val1, TimeInSec = val2, Index = val3, FileId = fileInf.Id});

                    RecordIntermediateResult(interRes, val1, val2, val3);

                    if (dv.IsCountStringIncorrect(interRes.CountString)) return -1;
                }
            }
            DeleteFile(file.FileName);

            fileInf.Values = valueList;

            Result res = CalculateValuesForResult(interRes);
            res.FileId = fileInf.Id;

            fileInf.Result = res;
            _context.SaveChanges();

            return 0;
        }

        public void CopyFile(string path, IFormFile? file)
        {
            using (var fs = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fs);
            }
        }

        public void DeleteFile(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            if (fileInfo.Exists)
                fileInfo.Delete();
        }

        public FileInf GetFiles(string fileName)
        {
            if (!fileName.Contains(".csv"))
                return _context.FileInfs.FirstOrDefault(x => x.Name == fileName+".csv");
            return _context.FileInfs.FirstOrDefault(x => x.Name == fileName);
        }

        public void RecordIntermediateResult(IntermediateResult res, DateTime val1, int val2, double val3)
        {
            if (val2 < res.MinTime) res.MinTime = val2;
            if (val2 > res.MaxTime) res.MaxTime = val2;
            if (val1 < res.MinDateTime) res.MinDateTime = val1;
            if (val3 < res.MinIndex) res.MinIndex = val3;
            if (val3 > res.MaxIndex) res.MaxIndex = val3;
            res.AllTime += val2;
            res.AllIndex += val3;
            res.ListForMedian.Add(val3);
            res.CountString++;
        }

        public FileInf GetOrAddFile(string fileName)
        {
            FileInf fileInf = new FileInf();
            if (!fileName.Contains(".csv")) fileName = fileName + ".csv";

            if (GetFiles(fileName) == null)
            {
                fileInf.Name = fileName;
                _context.FileInfs.Add(fileInf);
                _context.SaveChanges();
                fileInf = GetFiles(fileName);
            }
            else
                fileInf = _context.FileInfs.Include(c => c.Values).Include(x => x.Result).FirstOrDefault(x => x.Name == fileName);

            return fileInf;
        }

        public Result CalculateValuesForResult(IntermediateResult res)
        {
            int averTime = (int)res.AllTime / res.CountString;
            double averIndex = res.AllIndex / res.CountString;
            res.ListForMedian.OrderBy(x => x).ToList();

            if (res.ListForMedian.Count % 2 == 0)
                res.MedianIndex = (res.ListForMedian[res.ListForMedian.Count / 2] + res.ListForMedian[(res.ListForMedian.Count / 2) - 1]) / 2;
            else
                res.MedianIndex = (res.ListForMedian[res.ListForMedian.Count / 2]);

            return new Result() { AllTime = res.MaxTime - res.MinTime, MinDateTime = res.MinDateTime, AverTime = averTime,
                                        AverIndex = averIndex, MedianIndex = res.MedianIndex, MaxIndex = res.MaxIndex, 
                                        MinIndex = res.MinIndex, CountString = res.CountString};
        }

        public ActionResult<IEnumerable<ResultSimply>> GetResults(Filters filters)
        {
            List<ResultSimply> resultSimplies = _context.Results.Include(x => x.FileInf).Select(x => new ResultSimply(x)).ToList();

            FileInf fileInf = GetFiles(filters.FileName);

            if (fileInf != null)
            {
                resultSimplies = _context.Results.Include(x => x.FileInf).Where(x => x.FileId == fileInf.Id).Select(x => new ResultSimply(x)).ToList();
            }
            if (filters.FromMinDateTime < filters.ToMinDateTime)
            {
                resultSimplies = resultSimplies.Where(x => x.MinDateTime.TimeOfDay >= filters.FromMinDateTime.TimeOfDay && x.MinDateTime.TimeOfDay <= filters.ToMinDateTime.TimeOfDay).ToList();
            }
            if (filters.MinAverIndex < filters.MaxAverIndex)
            {
                resultSimplies = resultSimplies.Where(x => x.AverIndex >= filters.MinAverIndex && x.AverIndex <= filters.MaxAverIndex).ToList();
            }
            if (filters.MinAverTime < filters.MaxAverTime)
            {
                resultSimplies = resultSimplies.Where(x => x.AverTime >= filters.MinAverTime && x.AverTime <= filters.MaxAverTime).ToList();
            }

            return resultSimplies;
        }

        public ActionResult<IEnumerable<ValueSimply>> GetValues(int fileId)
        {
            return _context.Values.Where(x => x.FileId == fileId).Select(x => new ValueSimply(x)).ToList();
        }
    }
}
