﻿/*
 * ******************************************************************************
 *   Copyright 2014-2018 Spectra Logic Corporation. All Rights Reserved.
 *   Licensed under the Apache License, Version 2.0 (the "License"). You may not use
 *   this file except in compliance with the License. A copy of the License is located at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 *   or in the "license" file accompanying this file.
 *   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
 *   CONDITIONS OF ANY KIND, either express or implied. See the License for the
 *   specific language governing permissions and limitations under the License.
 * ****************************************************************************
 */

using System.Net;
using Moq;
using NUnit.Framework;
using SpectraLogic.EscapePodClient.Calls;
using SpectraLogic.EscapePodClient.Model;
using SpectraLogic.EscapePodClient.Runtime;
using SpectraLogic.EscapePodClient.Test.Mock;
using SpectraLogic.EscapePodClient.Test.Utils;
using SpectraLogic.EscapePodClient.Utils;

namespace SpectraLogic.EscapePodClient.Test
{
    [TestFixture]
    internal class EscapePodClientTest
    {
        [Test]
        public void ArchiveTest()
        {
            var archiveRequest =
                HttpUtils<ArchiveRequest>.JsonToObject(
                    ResourceFilesUtils.Read("SpectraLogic.EscapePodClient.Test.TestFiles.ArchiveRequest"));
            Assert.AreEqual("api/archive\nPOST\n{\"files\":[{\"name\":\"fileName\",\"uri\":\"uri\",\"size\":1234,\"metadata\":[{\"Key\":\"key\",\"Value\":\"value\"}],\"indexMedia\":false,\"storeFileProperties\":false}]}", archiveRequest.ToString());

            var mockNetwork = new Mock<INetwork>(MockBehavior.Strict);
            mockNetwork
                .Setup(n => n.Invoke(archiveRequest))
                .Returns(new MockHttpWebResponse("SpectraLogic.EscapePodClient.Test.TestFiles.ArchiveResponse",
                    HttpStatusCode.OK, null));

            var mockBuilder = new Mock<IEscapePodClientBuilder>(MockBehavior.Strict);
            mockBuilder
                .Setup(b => b.Build())
                .Returns(new EscapePodClient(mockNetwork.Object));

            var builder = mockBuilder.Object;
            var client = builder.Build();

            var job = client.Archive(archiveRequest);
            Assert.AreEqual("123456789", job.Id);
            Assert.AreEqual(EscapePodJobStatus.IN_PROGRESS, job.Status);

            mockBuilder.VerifyAll();
            mockNetwork.VerifyAll();
        }

        [Test]
        public void RestoreTest()
        {
            var restoreRequest =
                HttpUtils<RestoreRequest>.JsonToObject(
                    ResourceFilesUtils.Read("SpectraLogic.EscapePodClient.Test.TestFiles.RestoreRequest"));
            Assert.AreEqual("api/restore\nGET\n{\"Files\":[{\"Name\":\"name\",\"Destination\":\"dest\",\"RestoreFileAttributes\":true,\"ByteRange\":null,\"TimeCodeRange\":null},{\"Name\":\"name2\",\"Destination\":\"dest2\",\"RestoreFileAttributes\":false,\"ByteRange\":{\"Start\":0,\"Stop\":10},\"TimeCodeRange\":null},{\"Name\":\"name3\",\"Destination\":\"dest3\",\"RestoreFileAttributes\":false,\"ByteRange\":null,\"TimeCodeRange\":{\"Start\":10,\"Stop\":20}}]}", restoreRequest.ToString());

            var mockNetwork = new Mock<INetwork>(MockBehavior.Strict);
            mockNetwork
                .Setup(n => n.Invoke(restoreRequest))
                .Returns(new MockHttpWebResponse("SpectraLogic.EscapePodClient.Test.TestFiles.RestoreResponse",
                    HttpStatusCode.OK, null));

            var mockBuilder = new Mock<IEscapePodClientBuilder>(MockBehavior.Strict);
            mockBuilder
                .Setup(b => b.Build())
                .Returns(new EscapePodClient(mockNetwork.Object));

            var builder = mockBuilder.Object;
            var client = builder.Build();

            var job = client.Restore(restoreRequest);
            Assert.AreEqual("123456789", job.Id);

            mockBuilder.VerifyAll();
            mockNetwork.VerifyAll();
        }

        [Test]
        public void DeleteTest()
        {
            var deleteRequest =
                HttpUtils<DeleteRequest>.JsonToObject(
                    ResourceFilesUtils.Read("SpectraLogic.EscapePodClient.Test.TestFiles.DeleteRequest"));
            Assert.AreEqual("api/delete\nDELETE\n{\"Files\":[{\"Name\":\"file1\"},{\"Name\":\"file2\"}]}", deleteRequest.ToString());

            var mockNetwork = new Mock<INetwork>(MockBehavior.Strict);
            mockNetwork
                .Setup(n => n.Invoke(deleteRequest))
                .Returns(new MockHttpWebResponse("SpectraLogic.EscapePodClient.Test.TestFiles.DeleteResponse",
                    HttpStatusCode.OK, null));

            var mockBuilder = new Mock<IEscapePodClientBuilder>(MockBehavior.Strict);
            mockBuilder
                .Setup(b => b.Build())
                .Returns(new EscapePodClient(mockNetwork.Object));

            var builder = mockBuilder.Object;
            var client = builder.Build();

            var job = client.Delete(deleteRequest);
            Assert.AreEqual("123456789", job.Id);
            Assert.AreEqual(EscapePodJobStatus.DONE, job.Status);

            mockBuilder.VerifyAll();
            mockNetwork.VerifyAll();
        }

        [Test]
        public void CancelStringTest()
        {
            var cancelRequest = new CancelRequest("123456789");
            Assert.AreEqual("api/cancel?id=123456789\nPUT", cancelRequest.ToString());

            var mockNetwork = new Mock<INetwork>(MockBehavior.Strict);
            mockNetwork
                .Setup(n => n.Invoke(cancelRequest))
                .Returns(new MockHttpWebResponse("SpectraLogic.EscapePodClient.Test.TestFiles.CancelResponse",
                    HttpStatusCode.OK, null));

            var mockBuilder = new Mock<IEscapePodClientBuilder>(MockBehavior.Strict);
            mockBuilder
                .Setup(b => b.Build())
                .Returns(new EscapePodClient(mockNetwork.Object));

            var builder = mockBuilder.Object;
            var client = builder.Build();

            var job = client.Cancel(cancelRequest);
            Assert.AreEqual("123456789", job.Id);
            Assert.AreEqual(EscapePodJobStatus.CANCELED, job.Status);

            mockBuilder.VerifyAll();
            mockNetwork.VerifyAll();
        }

        [Test]
        public void CancelLongTest()
        {
            var cancelRequest = new CancelRequest(123456789);
            Assert.AreEqual("api/cancel?id=123456789\nPUT", cancelRequest.ToString());

            var mockNetwork = new Mock<INetwork>(MockBehavior.Strict);
            mockNetwork
                .Setup(n => n.Invoke(cancelRequest))
                .Returns(new MockHttpWebResponse("SpectraLogic.EscapePodClient.Test.TestFiles.CancelResponse",
                    HttpStatusCode.OK, null));

            var mockBuilder = new Mock<IEscapePodClientBuilder>(MockBehavior.Strict);
            mockBuilder
                .Setup(b => b.Build())
                .Returns(new EscapePodClient(mockNetwork.Object));

            var builder = mockBuilder.Object;
            var client = builder.Build();

            var job = client.Cancel(cancelRequest);
            Assert.AreEqual("123456789", job.Id);
            Assert.AreEqual(EscapePodJobStatus.CANCELED, job.Status);

            mockBuilder.VerifyAll();
            mockNetwork.VerifyAll();
        }

        [Test]
        public void GetArchiveTest()
        {
            var getArchiveRequest = new GetArchiveRequest("archive");
            Assert.AreEqual("api/getarchive?name=archive\nGET", getArchiveRequest.ToString());

            var mockNetwork = new Mock<INetwork>(MockBehavior.Strict);
            mockNetwork
                .Setup(n => n.Invoke(getArchiveRequest))
                .Returns(new MockHttpWebResponse("SpectraLogic.EscapePodClient.Test.TestFiles.GetArchiveResponse",
                    HttpStatusCode.OK, null));

            var mockBuilder = new Mock<IEscapePodClientBuilder>(MockBehavior.Strict);
            mockBuilder
                .Setup(b => b.Build())
                .Returns(new EscapePodClient(mockNetwork.Object));

            var builder = mockBuilder.Object;
            var client = builder.Build();

            var archive = client.GetArchive(getArchiveRequest);
            Assert.AreEqual("archive", archive.Name);

            mockBuilder.VerifyAll();
            mockNetwork.VerifyAll();
        }

        [Test]
        public void GetEscapePodJobWithStatusStringTest()
        {
            var id = "123456789";
            var getEscapePodJobWithStatusRequest = new GetEscapePodJob("archiveName", id);
            Assert.AreEqual("api/archive/archiveName/jobs/123456789\nGET", getEscapePodJobWithStatusRequest.ToString());

            var mockNetwork = new Mock<INetwork>(MockBehavior.Strict);
            mockNetwork
                .SetupSequence(n => n.Invoke(getEscapePodJobWithStatusRequest))
                .Returns(new MockHttpWebResponse(
                    "SpectraLogic.EscapePodClient.Test.TestFiles.GetEscapePodJobWithStatusInProgressResponse",
                    HttpStatusCode.OK, null))
                .Returns(new MockHttpWebResponse(
                    "SpectraLogic.EscapePodClient.Test.TestFiles.GetEscapePodJobWithStatusDoneResponse",
                    HttpStatusCode.OK, null))
                .Returns(new MockHttpWebResponse(
                    "SpectraLogic.EscapePodClient.Test.TestFiles.GetEscapePodJobWithStatusCanceledResponse",
                    HttpStatusCode.OK, null))
                .Returns(new MockHttpWebResponse(
                    "SpectraLogic.EscapePodClient.Test.TestFiles.GetEscapePodJobWithStatusUnknownResponse",
                    HttpStatusCode.OK, null));

            var mockBuilder = new Mock<IEscapePodClientBuilder>(MockBehavior.Strict);
            mockBuilder
                .Setup(b => b.Build())
                .Returns(new EscapePodClient(mockNetwork.Object));

            var builder = mockBuilder.Object;
            var client = builder.Build();

            var job = client.GetJob(getEscapePodJobWithStatusRequest);
            Assert.AreEqual(id, job.Id);
            Assert.AreEqual(EscapePodJobStatus.IN_PROGRESS, job.Status);

            job = client.GetJob(getEscapePodJobWithStatusRequest);
            Assert.AreEqual(id, job.Id);
            Assert.AreEqual(EscapePodJobStatus.DONE, job.Status);

            job = client.GetJob(getEscapePodJobWithStatusRequest);
            Assert.AreEqual(id, job.Id);
            Assert.AreEqual(EscapePodJobStatus.CANCELED, job.Status);

            job = client.GetJob(getEscapePodJobWithStatusRequest);
            Assert.AreEqual(id, job.Id);
            Assert.AreEqual(EscapePodJobStatus.UNKNOWN, job.Status);

            mockBuilder.VerifyAll();
            mockNetwork.VerifyAll();
        }

        [Test]
        public void GetEscapePodJobWithStatusLongTest()
        {
            var id = "123456789";
            var getEscapePodJobWithStatusRequest = new GetEscapePodJob("archiveName", long.Parse(id));
            Assert.AreEqual("api/archive/archiveName/jobs/123456789\nGET", getEscapePodJobWithStatusRequest.ToString());

            var mockNetwork = new Mock<INetwork>(MockBehavior.Strict);
            mockNetwork
                .SetupSequence(n => n.Invoke(getEscapePodJobWithStatusRequest))
                .Returns(new MockHttpWebResponse(
                    "SpectraLogic.EscapePodClient.Test.TestFiles.GetEscapePodJobWithStatusInProgressResponse",
                    HttpStatusCode.OK, null))
                .Returns(new MockHttpWebResponse(
                    "SpectraLogic.EscapePodClient.Test.TestFiles.GetEscapePodJobWithStatusDoneResponse",
                    HttpStatusCode.OK, null))
                .Returns(new MockHttpWebResponse(
                    "SpectraLogic.EscapePodClient.Test.TestFiles.GetEscapePodJobWithStatusCanceledResponse",
                    HttpStatusCode.OK, null))
                .Returns(new MockHttpWebResponse(
                    "SpectraLogic.EscapePodClient.Test.TestFiles.GetEscapePodJobWithStatusUnknownResponse",
                    HttpStatusCode.OK, null));

            var mockBuilder = new Mock<IEscapePodClientBuilder>(MockBehavior.Strict);
            mockBuilder
                .Setup(b => b.Build())
                .Returns(new EscapePodClient(mockNetwork.Object));

            var builder = mockBuilder.Object;
            var client = builder.Build();

            var job = client.GetJob(getEscapePodJobWithStatusRequest);
            Assert.AreEqual(id, job.Id);
            Assert.AreEqual(EscapePodJobStatus.IN_PROGRESS, job.Status);

            job = client.GetJob(getEscapePodJobWithStatusRequest);
            Assert.AreEqual(id, job.Id);
            Assert.AreEqual(EscapePodJobStatus.DONE, job.Status);

            job = client.GetJob(getEscapePodJobWithStatusRequest);
            Assert.AreEqual(id, job.Id);
            Assert.AreEqual(EscapePodJobStatus.CANCELED, job.Status);

            job = client.GetJob(getEscapePodJobWithStatusRequest);
            Assert.AreEqual(id, job.Id);
            Assert.AreEqual(EscapePodJobStatus.UNKNOWN, job.Status);

            mockBuilder.VerifyAll();
            mockNetwork.VerifyAll();
        }

        [Test]
        public void CreateArchiveTest()
        {
            var createArchiveRequest =
                HttpUtils<CreateArchiveRequest>.JsonToObject(
                    ResourceFilesUtils.Read("SpectraLogic.EscapePodClient.Test.TestFiles.CreateArchiveRequest"));
            Assert.AreEqual("api/createarchive\nPOST\n{\"Name\":\"archive\"}", createArchiveRequest.ToString());

            var mockNetwork = new Mock<INetwork>(MockBehavior.Strict);
            mockNetwork
                .Setup(n => n.Invoke(createArchiveRequest))
                .Returns(new MockHttpWebResponse("SpectraLogic.EscapePodClient.Test.TestFiles.CreateArchiveResponse",
                    HttpStatusCode.OK, null));

            var mockBuilder = new Mock<IEscapePodClientBuilder>(MockBehavior.Strict);
            mockBuilder
                .Setup(b => b.Build())
                .Returns(new EscapePodClient(mockNetwork.Object));

            var builder = mockBuilder.Object;
            var client = builder.Build();

            var archive = client.CreateArchive(createArchiveRequest);
            Assert.AreEqual("archive", archive.Name);

            mockBuilder.VerifyAll();
            mockNetwork.VerifyAll();
        }
    }
}
