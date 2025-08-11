using CoreWebAPIs.Models;
using System.Globalization;

namespace CoreWebAPIs.Helpers
{
    public class HelperMethods
    {


        public static DateTime ConvertDateTimeToUTC(DateTime? dt)
        {
            DateTime date = DateTime.UtcNow;
            try
            {

                date = dt != null ? DateTime.Parse(dt?.ToString(), CultureInfo.InvariantCulture) : date;
            }
            catch (Exception)
            {

                throw new ArgumentException("Invalid date format.");
            }

            return date;
        }
        public static IQueryable<VwotpFlight> GetArrivedFlightsQueryBasedOnDates(IQueryable<VwotpFlight> query, DateTime? fromFlightDate = null, DateTime? toFlightDate = null)
        {
            if (fromFlightDate != null && toFlightDate != null)
            {
                query = query.Where(x => x.ActualOnblocks == null ||
                                         (x.ActualOnblocks.Value.Date >= fromFlightDate &&
                                          x.ActualOnblocks.Value.Date <= toFlightDate));
            }
            else if (fromFlightDate != null && toFlightDate == null)
            {
                query = query.Where(x => x.ActualOnblocks == null || x.ActualOnblocks.Value.Date == ConvertDateTimeToUTC(fromFlightDate).Date);
            }
            else if (toFlightDate != null && fromFlightDate == null)
            {
                query = query.Where(x => x.ActualOnblocks == null || x.ActualOnblocks.Value.Date == ConvertDateTimeToUTC(toFlightDate));
            }
            else
            {
                query = query.Where(x => x.ActualOnblocks == null || x.ActualOnblocks.Value.Date == DateTime.UtcNow);
            }

            return query;
        }

        public static IQueryable<VwotpFlight> GetDepartedFlightsQueryBasedOnDates(IQueryable<VwotpFlight> query, DateTime? fromFlightDate = null, DateTime? toFlightDate = null)
        {
            if (fromFlightDate != null && toFlightDate != null)
            {
                query = query.Where(x => x.ActualAirborne == null || x.ActualAirborne.Value.Date >= fromFlightDate
                && x.ActualAirborne.Value.Date <= toFlightDate);
            }
            else if (fromFlightDate != null && toFlightDate == null)
            {
                query = query.Where(x => x.ActualAirborne == null || x.ActualAirborne.Value.Date == ConvertDateTimeToUTC(fromFlightDate).Date);
            }
            else if (toFlightDate != null && fromFlightDate == null)
            {
                query = query.Where(x => x.ActualAirborne == null || x.ActualAirborne.Value.Date == ConvertDateTimeToUTC(toFlightDate));
            }
            else
            {
                query = query.Where(x => x.ActualAirborne == null || x.ActualAirborne.Value.Date == DateTime.UtcNow);
            }

            return query;
        }

        public static IQueryable<VwotpFlight> GetFlightsQueryBasedOnDates(IQueryable<VwotpFlight> query, DateTime? fromFlightDate = null, DateTime? toFlightDate = null)
        {
            if (fromFlightDate != null && toFlightDate != null)
            {
                query = query.Where(x => x.PubDepDt == null || x.PubDepDt.Value.Date >= fromFlightDate
                && x.PubDepDt.Value.Date <= toFlightDate);
            }
            else if (fromFlightDate != null && toFlightDate == null)
            {
                query = query.Where(x => x.PubDepDt == null || x.PubDepDt.Value.Date == ConvertDateTimeToUTC(fromFlightDate).Date);
            }
            else if (toFlightDate != null && fromFlightDate == null)
            {
                query = query.Where(x => x.PubDepDt == null || x.PubDepDt.Value.Date == ConvertDateTimeToUTC(toFlightDate));
            }
            else
            {
                query = query.Where(x => x.PubDepDt == null || x.PubDepDt.Value.Date == DateTime.UtcNow);
            }

            return query;
        }

        public static IQueryable<RvChatbotM> GetReportsQueryBasedOnDates(IQueryable<RvChatbotM> query, DateTime? fromFlightDate = null, DateTime? toFlightDate = null)
        {
            if (fromFlightDate != null && toFlightDate != null)
            {
                query = query.Where(x => x.Localfltdate == null || x.Localfltdate.Value.Date >= fromFlightDate
                && x.Localfltdate.Value.Date <= toFlightDate);
            }
            else if (fromFlightDate != null && toFlightDate == null)
            {
                query = query.Where(x => x.Localfltdate == null || x.Localfltdate.Value.Date == ConvertDateTimeToUTC(fromFlightDate).Date);
            }
            else if (toFlightDate != null && fromFlightDate == null)
            {
                query = query.Where(x => x.Localfltdate == null || x.Localfltdate.Value.Date == ConvertDateTimeToUTC(toFlightDate));
            }
            else
            {
                query = query.Where(x => x.Localfltdate == null || x.Localfltdate.Value.Date == DateTime.UtcNow);
            }

            return query;
        }


        public static IQueryable<RvChatbotM> GetReportsQueryBasedOnMonth(IQueryable<RvChatbotM> query, DateTime? fromMonth = null, DateTime? toMonth = null)
        {
            if (fromMonth != null && toMonth != null)
            {
                query = query.Where(x => x.Localfltdate == null || x.Localfltdate.Value.Month >= fromMonth.Value.Month
                && x.Localfltdate.Value.Month <= toMonth.Value.Month);
            }
            else if (fromMonth != null && toMonth == null)
            {
                query = query.Where(x => x.Localfltdate == null || x.Localfltdate.Value.Month == fromMonth.Value.Month);
            }
            else if (toMonth != null && fromMonth == null)
            {
                query = query.Where(x => x.Localfltdate == null || x.Localfltdate.Value.Month == toMonth.Value.Month);
            }
            else
            {
                query = query.Where(x => x.Localfltdate == null || x.Localfltdate.Value.Month == DateTime.UtcNow.Month);
            }

            return query;
        }



        public static IQueryable<RvChatbotM> GetReportsQueryBasedOnYear(IQueryable<RvChatbotM> query, DateTime? fromYear = null, DateTime? toYear = null)
        {
            if (fromYear != null && toYear != null)
            {
                query = query.Where(x => x.Localfltdate == null || x.Localfltdate.Value.Year >= fromYear.Value.Year
                && x.Localfltdate.Value.Year <= toYear.Value.Year);
            }
            else if (fromYear != null && toYear == null)
            {
                query = query.Where(x => x.Localfltdate == null || x.Localfltdate.Value.Year == fromYear.Value.Year);
            }
            else if (toYear != null && fromYear == null)
            {
                query = query.Where(x => x.Localfltdate == null || x.Localfltdate.Value.Year == toYear.Value.Year);
            }
            else
            {
                query = query.Where(x => x.Localfltdate == null || x.Localfltdate.Value.Year == DateTime.UtcNow.Year);
            }

            return query;
        }

        public static DateTime ValidateMonthYear(string? monthYear = "")
        {
            DateTime date = DateTime.UtcNow;
            try
            {
                if (!string.IsNullOrEmpty(monthYear))
                {
                    if (!IsValidMonthYear(monthYear))
                    {
                        throw new ArgumentException("Invalid date format. Please use MM-yyyy.");

                    }
                    else
                    {
                        date = DateTime.ParseExact(monthYear, "MMM-yyyy", CultureInfo.InvariantCulture);
                    }
                }
            }
            catch (Exception)
            {

                throw new ArgumentException("Invalid date format. Please use MM-yyyy.");
            }

            return date;
        }

        public static DateTime ValidateYear(string? year = "")
        {
            DateTime date = DateTime.UtcNow;
            try
            {
                if (!string.IsNullOrEmpty(year))
                {
                    if (!IsValidYear(year))
                    {
                        throw new ArgumentException("Invalid date format. Please use yyyy.");

                    }
                    else
                    {
                        date = DateTime.ParseExact(year, "yyyy", CultureInfo.InvariantCulture);
                    }
                }
            }
            catch (Exception)
            {

                throw new ArgumentException("Invalid date format. Please use yyyy.");
            }

            return date;
        }

        public static bool IsValidMonthYear(string monthYear)
        {
            return !string.IsNullOrWhiteSpace(monthYear) &&
                   monthYear.Length == 8 &&
                   monthYear[3] == '-' &&
                   DateTime.TryParseExact(monthYear, "MMM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }

        public static bool IsValidYear(string year)
        {
            return !string.IsNullOrWhiteSpace(year) &&
                   year.Length == 4 &&
                   DateTime.TryParseExact(year, "yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }

    }
}
