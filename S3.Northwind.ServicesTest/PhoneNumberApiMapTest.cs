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
            PhoneNumberService phoneNumberAPIMap = new PhoneNumberService();
            phoneNumberAPIMap.PhoneNumber = "4588888888";

            //Act
            JsonPhonenumber actualPhoneNumber = phoneNumberAPIMap.PhoneNumberReturnedFromApiCall();

            //Assert
            Assert.IsTrue(actualPhoneNumber.Valid);
        }
    }
}
