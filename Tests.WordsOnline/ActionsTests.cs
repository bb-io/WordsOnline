using Apps.WordsOnline.Actions;
using Apps.WordsOnline.Models.Requests;
using WordsOnline.Base;

namespace Tests.WordsOnline
{
    [TestClass]
    public class ActionsTests : TestBase
    {
        [TestMethod]
        public async Task CreateRequest_ValidFile_ReturnsResponse()
        {
            var client = new Actions(InvocationContext,FileManager);

            var input = new CreateRequestRequest
            {
                RequestName = "NameTest",
                SourceLanguage = "EN-US",
                TargetLanguages = ["FR-FR"],
                ContentType = "Web content",
                ServiceLevel = "Enterprise"
            };

            var response = await client.CreateRequest(input);


            Assert.IsNotNull(response, "Response should not be null.");
            Assert.IsFalse(string.IsNullOrEmpty(response.RequestGuid), "Request GUID should not be null or empty.");
            Assert.AreEqual(input.RequestName, response.RequestName, "Request name should match the input.");
            Assert.AreEqual(input.ContentType, response.ContentType, "Content type should match the input.");
            Assert.AreEqual(input.ServiceLevel, response.ServiceLevel, "Service level should match the input.");
        }


    }
}
