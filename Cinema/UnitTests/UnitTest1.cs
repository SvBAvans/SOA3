using Cinema;
using Cinema.Domain;

namespace UnitTests
{
    public class UnitTest1
    {
        // Helper: bouw een screening op een specifieke datum/tijd met vaste stoelprijs
        private static MovieScreening ScreeningOn(DateTime dateTime, double pricePerSeat = 10.0)
        {
            var movie = new Movie("Test Movie");
            return new MovieScreening(movie, dateTime, pricePerSeat);
        }

        private static MovieTicket Ticket(MovieScreening screening, bool premium, int row, int seat)
            => new MovieTicket(screening, premium, row, seat);

        // STUDENT
        [Fact]
        public void StudentOrder_EverySecondTicketFree_IncludingPremiumSurcharge()
        {
            // Weekend of doordeweeks maakt niet uit voor studenten: elke 2e gratis
            var screening = ScreeningOn(new DateTime(2026, 2, 7, 20, 0, 0), pricePerSeat: 10.0); // zaterdag
            var order = new Order(orderNr: 1, isStudentOrder: true);

            // 2 tickets: 1 standaard, 1 premium
            order.AddSeatReservation(Ticket(screening, premium: false, row: 1, seat: 1)); // 10
            order.AddSeatReservation(Ticket(screening, premium: true, row: 1, seat: 2));  // 10+2, maar deze is 2e => gratis incl toeslag

            var total = order.CalculatePrice();

            // Alleen eerste ticket betalen: 10
            Assert.Equal(10.0, total, precision: 2);
        }

        [Fact]
        public void StudentOrder_ThreeTickets_SecondFree_ThirdPaid()
        {
            var screening = ScreeningOn(new DateTime(2026, 2, 8, 20, 0, 0), pricePerSeat: 12.0); // zondag
            var order = new Order(orderNr: 2, isStudentOrder: true);

            // 3 standaard tickets à 12
            order.AddSeatReservation(Ticket(screening, premium: false, row: 1, seat: 1)); // 12
            order.AddSeatReservation(Ticket(screening, premium: false, row: 1, seat: 2)); // gratis
            order.AddSeatReservation(Ticket(screening, premium: false, row: 1, seat: 3)); // 12

            var total = order.CalculatePrice();

            Assert.Equal(24.0, total, precision: 2);
        }

        [Fact]
        public void StudentOrder_FourTickets_TwoFree()
        {
            var screening = ScreeningOn(new DateTime(2026, 2, 7, 20, 0, 0), pricePerSeat: 10.0);
            var order = new Order(orderNr: 3, isStudentOrder: true);

            // 4 tickets: 2e en 4e gratis
            order.AddSeatReservation(Ticket(screening, false, 1, 1)); // 10
            order.AddSeatReservation(Ticket(screening, true, 1, 2)); // gratis (normaal 12)
            order.AddSeatReservation(Ticket(screening, true, 1, 3)); // 12
            order.AddSeatReservation(Ticket(screening, false, 1, 4)); // gratis (10)

            var total = order.CalculatePrice();

            // betalen: 10 + (10+2) = 22
            Assert.Equal(22.0, total, precision: 2);
        }

        // NIET STUDENT
        [Fact]
        public void NonStudent_WeekdayMonToThu_EverySecondTicketFree()
        {
            var screening = ScreeningOn(new DateTime(2026, 2, 2, 20, 0, 0), pricePerSeat: 10.0); // maandag
            var order = new Order(orderNr: 10, isStudentOrder: false);

            order.AddSeatReservation(Ticket(screening, premium: false, row: 1, seat: 1)); // 10
            order.AddSeatReservation(Ticket(screening, premium: true, row: 1, seat: 2)); // 10+3, maar 2e => gratis

            var total = order.CalculatePrice();

            Assert.Equal(10.0, total, precision: 2);
        }

        [Fact]
        public void NonStudent_Weekend_NoFreeSecondTicket_IfLessThan6Tickets()
        {
            var screening = ScreeningOn(new DateTime(2026, 2, 7, 20, 0, 0), pricePerSeat: 10.0); // zaterdag
            var order = new Order(orderNr: 11, isStudentOrder: false);

            // 2 tickets, weekend, niet-student => geen gratis 2e, geen groepskorting
            order.AddSeatReservation(Ticket(screening, premium: false, row: 1, seat: 1)); // 10
            order.AddSeatReservation(Ticket(screening, premium: true, row: 1, seat: 2)); // 13

            var total = order.CalculatePrice();

            Assert.Equal(23.0, total, precision: 2);
        }

        [Fact]
        public void NonStudent_Weekend_GroupDiscount10Percent_From6Tickets_IncludingPremiumSurcharge()
        {
            var screening = ScreeningOn(new DateTime(2026, 2, 7, 20, 0, 0), pricePerSeat: 10.0); // zaterdag
            var order = new Order(orderNr: 12, isStudentOrder: false);

            // 6 tickets: 3 standaard, 3 premium
            // Subtotaal = 3*10 + 3*(10+3) = 30 + 39 = 69
            // 10% korting => 62.10
            order.AddSeatReservation(Ticket(screening, false, 1, 1));
            order.AddSeatReservation(Ticket(screening, false, 1, 2));
            order.AddSeatReservation(Ticket(screening, false, 1, 3));
            order.AddSeatReservation(Ticket(screening, true, 2, 1));
            order.AddSeatReservation(Ticket(screening, true, 2, 2));
            order.AddSeatReservation(Ticket(screening, true, 2, 3));

            var total = order.CalculatePrice();

            Assert.Equal(62.10, total, precision: 2);
        }

        [Fact]
        public void NonStudent_Friday_NoFreeSecondTicket_AndNoGroupDiscountIfLessThan6()
        {
            var screening = ScreeningOn(new DateTime(2026, 2, 6, 20, 0, 0), pricePerSeat: 10.0); // vrijdag
            var order = new Order(orderNr: 13, isStudentOrder: false);

            // Vrijdag valt niet onder ma-di-wo-do regel
            order.AddSeatReservation(Ticket(screening, false, 1, 1)); // 10
            order.AddSeatReservation(Ticket(screening, false, 1, 2)); // 10

            var total = order.CalculatePrice();

            Assert.Equal(20.0, total, precision: 2);
        }

        // EDGE CASES
        [Fact]
        public void EmptyOrder_CostIsZero()
        {
            var order = new Order(orderNr: 99, isStudentOrder: false);
            var total = order.CalculatePrice();
            Assert.Equal(0.0, total, precision: 2);
        }

        [Fact]
        public void PremiumSurcharge_Is2ForStudents_3ForNonStudents()
        {
            var screening = ScreeningOn(new DateTime(2026, 2, 5, 20, 0, 0), pricePerSeat: 10.0); // donderdag
            var studentOrder = new Order(orderNr: 20, isStudentOrder: true);
            studentOrder.AddSeatReservation(Ticket(screening, premium: true, row: 1, seat: 1)); // 12
                                                                                                // (maar let op: als je hier 2 tickets zou doen, zou 2e gratis zijn)

            var nonStudentOrder = new Order(orderNr: 21, isStudentOrder: false);
            nonStudentOrder.AddSeatReservation(Ticket(screening, premium: true, row: 1, seat: 1)); // 13

            Assert.Equal(12.0, studentOrder.CalculatePrice(), precision: 2);
            Assert.Equal(13.0, nonStudentOrder.CalculatePrice(), precision: 2);
        }

        // EXPORT
        [Fact]
        public void Export_PlainText_CreatesATextFile_WithOrderNumberInContent()
        {
            var tempDir = CreateIsolatedTempDir();
            var oldDir = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(tempDir);

            try
            {
                var screening = ScreeningOn(new DateTime(2026, 2, 2, 20, 0, 0), 10.0);
                var order = new Order(orderNr: 500, isStudentOrder: true);
                order.AddSeatReservation(Ticket(screening, false, 1, 1));

                order.Export(TicketExportFormat.PLAINTEXT);

                var file = FindNewestFile(tempDir, "*.txt");
                Assert.NotNull(file);

                var content = File.ReadAllText(file!);
                Assert.Contains("500", content); // orderNr ergens in output
            }
            finally
            {
                Directory.SetCurrentDirectory(oldDir);
                TryDeleteDir(tempDir);
            }
        }

        [Fact]
        public void Export_Json_CreatesAJsonFile_ThatLooksLikeJson()
        {
            var tempDir = CreateIsolatedTempDir();
            var oldDir = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(tempDir);

            try
            {
                var screening = ScreeningOn(new DateTime(2026, 2, 2, 20, 0, 0), 10.0);
                var order = new Order(orderNr: 501, isStudentOrder: false);
                order.AddSeatReservation(Ticket(screening, true, 1, 1));

                order.Export(TicketExportFormat.JSON);

                var file = FindNewestFile(tempDir, "*.json");
                Assert.NotNull(file);

                var content = File.ReadAllText(file!);
                // simpele "looks like JSON" checks
                Assert.True(content.TrimStart().StartsWith("{") || content.TrimStart().StartsWith("["));
            }
            finally
            {
                Directory.SetCurrentDirectory(oldDir);
                TryDeleteDir(tempDir);
            }
        }

        // Helpers voor export tests
        private static string CreateIsolatedTempDir()
        {
            var dir = Path.Combine(Path.GetTempPath(), "bioscoop_tests_" + Guid.NewGuid());
            Directory.CreateDirectory(dir);
            return dir;
        }

        private static void TryDeleteDir(string dir)
        {
            try { Directory.Delete(dir, recursive: true); } catch { /* ignore */ }
        }

        private static string? FindNewestFile(string dir, string pattern)
        {
            var files = Directory.GetFiles(dir, pattern, SearchOption.TopDirectoryOnly);
            return files
                .Select(f => new FileInfo(f))
                .OrderByDescending(fi => fi.LastWriteTimeUtc)
                .FirstOrDefault()
                ?.FullName;
        }
    }
}