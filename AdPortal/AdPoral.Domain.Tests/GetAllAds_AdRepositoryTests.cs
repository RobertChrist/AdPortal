namespace AdPortal.Domain.Tests
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class GetAllAds_AdRespositoryTests 
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
        public void GetAllAds_Normal_ReturnsCorrectNumberOfAds()
        {
            var target = new AdRepository(_mockDataService);
            
            var result = target.GetAllAds(_startDate, _endDate, "AdId", true, 9000, 0);

            Assert.AreEqual(12, result.DisplayAds.Count());
            Assert.AreEqual(0, result.Page);
            Assert.AreEqual(1, result.TotalPageCount);
        }

        [TestMethod]
        public void GetAllAds_SortsAscending()
        {
            var target = new AdRepository(_mockDataService);

            var result = target.GetAllAds(_startDate, _endDate, "AdId", true, 9000, 0);

            int lastAdId = Int32.MinValue;
            foreach (Ad ad in result.DisplayAds)
            {
                Assert.IsTrue(ad.AdId > lastAdId);
                lastAdId = ad.AdId;
            }
        }

        [TestMethod]
        public void GetAllAds_SortsDescending()
        {
            var target = new AdRepository(_mockDataService);

            var result = target.GetAllAds(_startDate, _endDate, "AdId", false, 9000, 0);

            int lastAdId = Int32.MaxValue;
            foreach (Ad ad in result.DisplayAds)
            {
                Assert.IsTrue(ad.AdId < lastAdId);
                lastAdId = ad.AdId;
            }
        }

        [TestMethod]
        public void GetAllAds_SortsNestedAscending()
        {
            var target = new AdRepository(_mockDataService);

            var result = target.GetAllAds(_startDate, _endDate, "Brand.BrandId", true, 9000, 0);

            int lastBrandId = Int32.MinValue;
            foreach (Ad ad in result.DisplayAds)
            {
                Assert.IsTrue(ad.Brand.BrandId >= lastBrandId);
                lastBrandId = ad.Brand.BrandId;
            }
        }

        [TestMethod]
        public void GetAllAds_SortsNestedDescending()
        {
            var target = new AdRepository(_mockDataService);

            var result = target.GetAllAds(_startDate, _endDate, "Brand.BrandId", false, 9000, 0);

            int lastBrandId = Int32.MaxValue;
            foreach (Ad ad in result.DisplayAds)
            {
                Assert.IsTrue(ad.Brand.BrandId <= lastBrandId);
                lastBrandId = ad.Brand.BrandId;
            }
        }

        [TestMethod]
        public void GetAllAds_Pages()
        {
            var target = new AdRepository(_mockDataService);

            var result = target.GetAllAds(_startDate, _endDate, "AdId", true, 6, 1);

            Assert.AreEqual(6, result.DisplayAds.Count());
            Assert.AreEqual(2, result.TotalPageCount);
            Assert.AreEqual(1, result.Page);
        }

        /* We could add quite a few more tests here, but I think this is sufficient for now */
    }
}
