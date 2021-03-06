﻿using System;
using System.Collections.Generic;
using System.Linq;
using CommandCreation;
using FileLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class RenameCommandTests
    {

        private const string SourceFolder = @"D:\music\";

        [TestMethod]
        public void RenameTestExecute_Successful()
        {
            // Init
            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags{Track = 1, Title = "newtitle1"}, SourceFolder + "title1.mp3"),
                new FakeMp3File(new Mp3Tags{Track = 2, Title = "newtitle2"}, SourceFolder + "title2.mp3"),
                new FakeMp3File(new Mp3Tags{Track = 3, Title = "newtitle3"}, SourceFolder + "title3.mp3"),
            };

            // Act
            var commandPool = mp3Files.Select(mp3File => new RenameCommand(mp3File, "{track}. {title}")).Cast<Command>().ToList();
            commandPool.ForEach(command => command.Execute());

            // Assert
            Assert.AreEqual(SourceFolder + "1. newtitle1.mp3", mp3Files[0].FullName);
            Assert.AreEqual(SourceFolder + "2. newtitle2.mp3", mp3Files[1].FullName);
            Assert.AreEqual(SourceFolder + "3. newtitle3.mp3", mp3Files[2].FullName);
        }

        [TestMethod]
        public void Rename_ComplexPattern_SuccessfulRename()
        {
            // Init
            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags{Title = "newtitle1", Artist = "newartist1"}, SourceFolder + "title1.mp3"),
                new FakeMp3File(new Mp3Tags{Title = "newtitle2", Artist = "newartist2"}, SourceFolder + "title2.mp3"),
                new FakeMp3File(new Mp3Tags{Title = "newtitle3", Artist = "newartist3"}, SourceFolder + "title3.mp3"),
            };

            // Act
            var commandPool = mp3Files.Select(mp3File => new RenameCommand(mp3File, "{asd{artist}}-..{title}")).Cast<Command>().ToList();
            commandPool.ForEach(command => command.Execute());

            // Assert
            Assert.AreEqual(SourceFolder + "{asdnewartist1}-..newtitle1.mp3", mp3Files[0].FullName);
            Assert.AreEqual(SourceFolder + "{asdnewartist2}-..newtitle2.mp3", mp3Files[1].FullName);
            Assert.AreEqual(SourceFolder + "{asdnewartist3}-..newtitle3.mp3", mp3Files[2].FullName);
        }

        [TestMethod]
        public void Rename_FileWithSuchNameAlreadyExists_UniquePathCreated()
        {
            // Init
            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags{Artist = "artist"}, SourceFolder + "artist.mp3"),
            };

            // Act
            var commandPool = mp3Files.Select(mp3File => new RenameCommand(mp3File, "{artist}")).Cast<Command>().ToList();
            commandPool.ForEach(command => command.Execute());

            // Assert
            Assert.AreEqual(SourceFolder + "artist (1).mp3", mp3Files[0].FullName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Rename_EmptyPattern_ArgumentException()
        {
            // Init
            var mp3Files = new List<IMp3File>
            {
                new FakeMp3File(new Mp3Tags(), SourceFolder + "title.mp3"),
            };

            // Act
            var commandPool = mp3Files.Select(mp3File => new RenameCommand(mp3File, "")).Cast<Command>().ToList();
            commandPool.ForEach(command => command.Execute());
        }
    }
}
