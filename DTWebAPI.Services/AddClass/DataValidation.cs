namespace DTWebAPI.Services.AddClass
{
    public class DataValidation
    {
        public Predicate<DateTime> IsFullDateIncorrect = (DateTime date) => date < new DateTime(2000, 01, 01) && date > DateTime.Now;

        public Predicate<int> IsTimeInSecLessThanZero = (int num) => num < 0;

        public Predicate<double> IsIndexLessThanZero = (double num) => num < 0;

        public Predicate<int> IsCountStringIncorrect = (int num) => num < 1 || num > 10000; 
    }
}
