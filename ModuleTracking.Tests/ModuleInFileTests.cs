namespace ModuleTracking.Tests
{
    public class ModuleInFileTests
    {
        [Test]
        public void WhenAllPercentsAreInRangeAndNoExtreme_ShouldReturnSuccess()
        {
            var module = new ModuleInFile("Czujnik", "P350009-010", true);

            module.AddPercent(1.3);
            module.AddPercent(1);
            module.AddPercent(45);
            module.AddPercent("56");
            module.AddPercent(23.5f);

            var stats = module.GetStatistics();

            Assert.That(stats.Max, Is.EqualTo(56));
        }

        [Test]
        public void WhenAllPercentsAreInRangeWithOneMaxAsInteger_ShouldReturnSuccess()
        {
            var module = new ModuleInFile("Czujnik", "P350009-010", true);

            module.AddPercent(71.3);
            module.AddPercent(39);
            module.AddPercent(100);
            module.AddPercent("99.99");
            module.AddPercent(73.9f);

            var stats = module.GetStatistics();

            Assert.That(stats.Max, Is.EqualTo(100));
        }

        [Test]
        public void WhenAllPercentsAreInRangeWithOneMaxAsString_ShouldReturnSuccess()
        {
            var module = new ModuleInFile("Czujnik", "P350009-010", true);

            module.AddPercent(1.3);
            module.AddPercent(1);
            module.AddPercent(99.999);
            module.AddPercent("100.0000");
            module.AddPercent(23.5f);

            var stats = module.GetStatistics();

            Assert.That(stats.Max, Is.EqualTo(100));
        }

        [Test]
        public void WhenAllPercentsAreInRangeWithOneMinAsInteger_ShouldReturnSuccess()
        {
            var module = new ModuleInFile("Czujnik", "P350009-010", true);

            module.AddPercent(18.9);
            module.AddPercent(1);
            module.AddPercent(0);
            module.AddPercent("99.99");
            module.AddPercent(23.5f);

            var stats = module.GetStatistics();

            Assert.That(stats.Min, Is.EqualTo(0));
        }

        [Test]
        public void WhenAllPercentsAreInRangeWithOneMinAsString_ShouldReturnSuccess()
        {
            var module = new ModuleInFile("Czujnik", "P350009-010", true);

            module.AddPercent(76.2);
            module.AddPercent(56);
            module.AddPercent(0.0001);
            module.AddPercent("0");
            module.AddPercent(23.5f);

            var stats = module.GetStatistics();

            Assert.That(stats.Min, Is.EqualTo(0));
        }

        [Test]
        public void WhenAllPercentsAreInRange_ShouldReturnCorrectAverage()
        {
            var module = new ModuleInList("Czujnik", "P350009-010");

            module.AddPercent(10);
            module.AddPercent(90);
            module.AddPercent(80);
            module.AddPercent("70");
            module.AddPercent(20f);

            var stats = module.GetStatistics();

            Assert.That(stats.Average, Is.EqualTo(54));
        }
    }
}
