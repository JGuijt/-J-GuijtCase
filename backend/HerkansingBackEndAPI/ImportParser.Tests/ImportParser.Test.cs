using System;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Http;
using HerkansingBackEndAPI.DataAcces;
using HerkansingBackEndAPI.Services;
using Shouldly;
using HerkansingBackEndAPI.Models;

namespace ImportParser.Tests
{
    public class ImportParserTest
    {
        [Fact]
        public void ParseFileShouldTouchParsers()
        {
            Mock<IFormFile> fileMock = new Mock<IFormFile>();
            Mock<IDatabaseAcces> dbAMock = new Mock<IDatabaseAcces>();
            ImportingParser sut = new ImportingParser(fileMock.Object, dbAMock.Object);

            sut.ParseFile();
            //incompleet
        }

        [Fact]
        public void VerzamelbakShouldReturn0IfArrayIsEmpty()
        {
            Mock<IFormFile> fileMock = new Mock<IFormFile>();
            Mock<IDatabaseAcces> dbAMock = new Mock<IDatabaseAcces>();
            ImportingParser sut = new ImportingParser(fileMock.Object, dbAMock.Object);
            string[] test = { };
            int[] expected = { 0, 0 };
            sut.VerzamelBak(test).ShouldBe<int[]>(expected);
        }

        [Fact]
        public void VerzamelbakShouldCallMethodsIfArrayNotNull()
        {
            Mock<IFormFile> fileMock = new Mock<IFormFile>();
            Mock<IDatabaseAcces> dbAMock = new Mock<IDatabaseAcces>();
            ImportingParser sut = new ImportingParser(fileMock.Object, dbAMock.Object);
            string[] test = { "test" };

            sut.VerzamelBak(test);

            dbAMock.Verify(x => x.CheckCursusAanwezig(It.IsAny<string>()), Times.Once);
            dbAMock.Verify(x => x.ZoekCursusId(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void VerzamelbakShouldNotCallMethodsIfArrayNull()
        {
            Mock<IFormFile> fileMock = new Mock<IFormFile>();
            Mock<IDatabaseAcces> dbAMock = new Mock<IDatabaseAcces>();
            ImportingParser sut = new ImportingParser(fileMock.Object, dbAMock.Object);
            string[] test = { };

            sut.VerzamelBak(test);

            dbAMock.Verify(x => x.CheckCursusAanwezig(It.IsAny<string>()), Times.Never);
            dbAMock.Verify(x => x.ZoekCursusId(It.IsAny<string>()), Times.Never);
        }


        [Fact]
        public void SplitRegelsShouldSplitStringAtNewLine()
        {            
            Mock<IFormFile> fileMock = new Mock<IFormFile>();
            Mock<IDatabaseAcces> dbAMock = new Mock<IDatabaseAcces>();
            string test = "test1\ntest2";
            string[] expected = { "test1", "test2" };
            ImportingParser sut = new ImportingParser(fileMock.Object, dbAMock.Object);            

            sut.SplitRegels(test).ShouldBe(expected);
        }

        [Fact]
        public void SendCursusToDbShouldCallCursusAanwezig()
        {
            Mock<IFormFile> fileMock = new Mock<IFormFile>();
            Mock<IDatabaseAcces> dbAMock = new Mock<IDatabaseAcces>();
            ImportingParser sut = new ImportingParser(fileMock.Object, dbAMock.Object);

            sut.SendCursusToDb(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>());

            dbAMock.Verify(x => x.CheckCursusAanwezig(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void SendCursusToDbShouldCallInsertInDbIfCursusIsNotPresent()
        {
            Mock<IFormFile> fileMock = new Mock<IFormFile>();
            Mock<IDatabaseAcces> dbAMock = new Mock<IDatabaseAcces>();
            Mock<Cursus> cursusMock = new Mock<Cursus>();
            ImportingParser sut = new ImportingParser(fileMock.Object, dbAMock.Object);
            dbAMock.Setup(f => f.CheckCursusAanwezig(It.IsAny<string>())).Returns(false);

            sut.SendCursusToDb(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>());

            dbAMock.Verify(x => x.InsertInDb(It.IsAny<Cursus>()), Times.Once);
        }

        [Fact]
        public void SendCursusToDbShouldNotCallInsertInDbIfCursusPresent()
        {
            Mock<IFormFile> fileMock = new Mock<IFormFile>();
            Mock<IDatabaseAcces> dbAMock = new Mock<IDatabaseAcces>();
            Mock<Cursus> cursusMock = new Mock<Cursus>();
            ImportingParser sut = new ImportingParser(fileMock.Object, dbAMock.Object);
            dbAMock.Setup(f => f.CheckCursusAanwezig(It.IsAny<string>())).Returns(true);

            sut.SendCursusToDb(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>());

            dbAMock.Verify(x => x.InsertInDb(It.IsAny<Cursus>()), Times.Never);
        }

        [Fact]
        public void SendInstantieToDbShouldCallZoekCursisIdAndInsertInDb()
        {
            Mock<IFormFile> fileMock = new Mock<IFormFile>();
            Mock<IDatabaseAcces> dbAMock = new Mock<IDatabaseAcces>();
            ImportingParser sut = new ImportingParser(fileMock.Object, dbAMock.Object);

            sut.SendInstantieToDb(It.IsAny<DateTime>(), It.IsAny<string>());

            dbAMock.Verify(x => x.InsertInDb(It.IsAny<CursusInstantie>()), Times.Once);
        }

        [Fact]
        public void ParseTitelShouldRemoveTitel()
        {
            Mock<IFormFile> fileMock = new Mock<IFormFile>();
            Mock<IDatabaseAcces> dbAMock = new Mock<IDatabaseAcces>();
            string[] testArray = { "Titel: test" };
            ImportingParser sut = new ImportingParser(fileMock.Object, dbAMock.Object);

            sut.ParseTitel(testArray).ShouldBe("test");            
        }

        [Fact]
        public void ParseTitelShouldReturnSingleString()
        {
            Mock<IFormFile> fileMock = new Mock<IFormFile>();
            Mock<IDatabaseAcces> dbAMock = new Mock<IDatabaseAcces>();
            string[] testArray = { "Titel: test", "Duur: 5 dagen" };
            ImportingParser sut = new ImportingParser(fileMock.Object, dbAMock.Object);

            sut.ParseTitel(testArray).ShouldBeOfType<string>();
        }

        [Fact]
        public void ParseDuurShouldRemoveDuurEnDagen()
        {
            Mock<IFormFile> fileMock = new Mock<IFormFile>();
            Mock<IDatabaseAcces> dbAMock = new Mock<IDatabaseAcces>();
            string[] testArray = { "Duur: 5 dagen" };
            ImportingParser sut = new ImportingParser(fileMock.Object, dbAMock.Object);

            sut.ParseDuur(testArray).ShouldBe(5);
        }

        [Fact]
        public void ParseDuurShouldReturnSingleInt()
        {
            Mock<IFormFile> fileMock = new Mock<IFormFile>();
            Mock<IDatabaseAcces> dbAMock = new Mock<IDatabaseAcces>();
            string[] testArray = { "Titel: test", "Duur: 5 dagen" };
            ImportingParser sut = new ImportingParser(fileMock.Object, dbAMock.Object);

            sut.ParseDuur(testArray).ShouldBeOfType<int>();
        }

        [Fact]
        public void ParseDatumShouldRemoveStartDatum()
        {
            Mock<IFormFile> fileMock = new Mock<IFormFile>();
            Mock<IDatabaseAcces> dbAMock = new Mock<IDatabaseAcces>();
            string[] testArray = { "Startdatum: 15/10/2018" };
            ImportingParser sut = new ImportingParser(fileMock.Object, dbAMock.Object);

            sut.ParseDatum(testArray).ShouldBe(new DateTime(2018, 10, 15));
        }

        [Fact]
        public void ParseDuurShouldReturnSingleDateTime()
        {
            Mock<IFormFile> fileMock = new Mock<IFormFile>();
            Mock<IDatabaseAcces> dbAMock = new Mock<IDatabaseAcces>();
            string[] testArray = { "Titel: test", "Startdatum: 15/10/2018" };
            ImportingParser sut = new ImportingParser(fileMock.Object, dbAMock.Object);

            sut.ParseDatum(testArray).ShouldBeOfType<DateTime>();
        }



    }
}
