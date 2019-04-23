using System;
using System.Collections.Generic;
using Autofac.Extras.Moq;
using Data.Contexts;
using Data.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;

namespace UnitTests
{
    [TestClass]
    public class CategoryTests
    {
        [TestMethod]
        public void GetCategories_IsValid()
        {
            using (var mock = AutoMock.GetLoose())
            {
                mock.Mock<ICategoryContext>()
                    .Setup(x => x.GetAllCategories())
                    .Returns(GetSampleCategory());

                var cls = mock.Create<CategoryContextSQL>();
                var expected = GetSampleCategory();

                var actual = cls.GetAllCategories();

                Assert.IsTrue(actual != null);
                Assert.AreEqual(expected.Count, actual.Count);

            }
        }

        private List<Category> GetSampleCategory()
        {
            List<Category> output = new List<Category>
            {
                new Category
                {
                    Name = "Medisch"
                },
                new Category
                {
                    Name = "Huishoudelijk"
                }
            };

            return output;
        }
    }
}
