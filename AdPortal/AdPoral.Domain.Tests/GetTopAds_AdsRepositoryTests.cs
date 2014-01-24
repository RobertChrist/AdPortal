namespace AdPortal.Domain.Tests
{
    using System;
    using System.Linq;

    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class GetTopAds_AdsRepositoryTests
    {
        readonly DateTime _startDate = new DateTime(2011, 1, 1);
        readonly DateTime _endDate = new DateTime(2011, 4, 1);

        static IDataService _mockDataService;

        static readonly Ad[] _defaultDataSource = new Ad[12]
            {
                new Ad { AdId = 1, NumPages = 2, Position = "Page", Brand = new Brand {BrandId = 1, BrandName = "A"  } },
                new Ad { AdId = 2, NumPages = 1, Position = "cover", Brand = new Brand {BrandId = 2, BrandName = "B" } },
                new Ad { AdId = 3, NumPages = 1, Position = "Page", Brand = new Brand {BrandId = 3, BrandName = "C" } },
                new Ad { AdId = 4, NumPages = 1, Position = null, Brand = new Brand {BrandId = 2, BrandName = "B" } },
                new Ad { AdId = 5, NumPages = 1, Position = "Cover", Brand = new Brand {BrandId = 4, BrandName = "D" } },
                new Ad { AdId = 6, NumPages = 2, Position = "Page", Brand = new Brand {BrandId = 3, BrandName = "C" } },
                new Ad { AdId = 7, NumPages = 2, Position = "Cover", Brand = new Brand {BrandId = 5, BrandName = "E"  } },
                new Ad { AdId = 8, NumPages = .5m, Position = "Page", Brand = new Brand {BrandId = 2, BrandName = "B" } },
                new Ad { AdId = 9, NumPages = 3, Position = "Page", Brand = new Brand {BrandId = 7, BrandName = null } },
                new Ad { AdId = 10, NumPages = .3m, Position = "Page", Brand = new Brand {BrandId = 2, BrandName = "B" } },
                new Ad { AdId = 11, NumPages = 1, Position = "cover", Brand = new Brand {BrandId = 6, BrandName = "F" } },
                new Ad { AdId = 12, NumPages = 0, Position = "Page", Brand = new Brand {BrandId = 3, BrandName = "C" } }

            };

        [ClassInitialize]
        public static void SetupDefaultDataService(TestContext context)
        {
            var dataService = new Mock<IDataService>();
            dataService.Setup(ds => ds.GetAdDataByDateRange(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(_defaultDataSource);
            _mockDataService = dataService.Object;
        }

        [TestMethod]
        public void GetTopAds_Normal_AreFiveLargest()
        {
            const int maxLength = 5;

            var target = new AdRepository(_mockDataService);

            var result = target.GetTopAds(_startDate, _endDate, maxLength);

            foreach (Ad ad in result.DisplayAds)
            {
                // This number was determined by looking at our sample set.  This should probably be determined programmatically.
                Assert.IsTrue(ad.NumPages >= 1);    
            }
        }

        [TestMethod]
        public void GetTopAds_Normal_ReturnsCorrectLength()
        {
            const int maxLength = 5;

            var target = new AdRepository(_mockDataService);

            var result = target.GetTopAds(_startDate, _endDate, maxLength);

            Assert.AreEqual(maxLength, result.DisplayAds.Count());
        }

        [TestMethod]
        public void GetTopAds_Normal_DistinctByBrand()
        {
            const int maxLength = 5;

            var target = new AdRepository(_mockDataService);

            var result = target.GetTopAds(_startDate, _endDate, maxLength);

            HashSet<int> brands = new HashSet<int>();
            foreach (Ad ad in result.DisplayAds)
            {
                Assert.IsTrue(brands.Add(ad.Brand.BrandId));
            }
        }

        [TestMethod]
        public void GetTopAds_Normal_SortedDescendingByPageCoverageAmount()
        {
            var target = new AdRepository(_mockDataService);

            var result = target.GetTopAds(_startDate, _endDate, 5);

            decimal lastPageCoverageAmount = decimal.MaxValue;
            foreach (Ad ad in result.DisplayAds)
            {
                Assert.IsTrue(ad.NumPages <= lastPageCoverageAmount);
                lastPageCoverageAmount = ad.NumPages;
            }
        }

        [TestMethod]
        public void GetTopAds_Normal_SecondarySortBrandName()
        {
            var target = new AdRepository(_mockDataService);

            var result = target.GetTopAds(_startDate, _endDate, 5);

            decimal lastPageCoverageAmount = decimal.MaxValue;
            string lastBrandName = null;
            foreach (Ad ad in result.DisplayAds)
            {
                if (ad.NumPages == lastPageCoverageAmount)
                {
                    Assert.IsTrue(string.Compare(ad.Brand.BrandName, lastBrandName) > 0);
                }

                lastPageCoverageAmount = ad.NumPages;
                lastBrandName = ad.Brand.BrandName;
            }
        }
    }
}
