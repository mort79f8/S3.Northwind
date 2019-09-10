using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using S3.Northwind.Services;

namespace S3.Northwind.ServicesTest
{
    [TestClass]
    public class PhoneNumberApiMapTest
    {
        [TestMethod]
        public void PhoneNumberReturnedFromApiCall_isValid()     
        {
            // Arrange
            PhoneNumberAPIMap phoneNumberAPIMap = new PhoneNumberAPIMap();
            PhoneNumberAPI phoneNumberAPI = new PhoneNumberAPI();
            phoneNumberAPIMap.PhoneNumber = "4588888888";

            //Act
            PhoneNumberAPI actualPhoneNumber = phoneNumberAPIMap.PhoneNumberReturnedFromApiCall();

            //Assert
            Assert.IsTrue(actualPhoneNumber.Valid);
        }
    }
}
