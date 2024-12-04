using Server.Logic.Currency;
using Server.Logic.Tests.Data;

namespace Server.Logic.Tests.Currency;

[TestClass]
public class CurrencyTests
{
    [TestMethod]
    [DynamicData(nameof(CurrencyDataProvider.CurrencyConstructorExceeptionData), typeof(CurrencyDataProvider))]
    public void Currency_Constructor_Exception(string name, string code, string sign)
    {
        Assert.ThrowsException<ArgumentNullException>(() => new Logic.Currency.Currency(name, code, sign));
    }

    [TestMethod]
    public void Currency_Constructor_Void()
    {
        Assert.AreEqual(
            new Logic.Currency.Currency(
                CurrencyProvider.USD.Name, 
                CurrencyProvider.USD.Code, 
                CurrencyProvider.USD.Sign), 
            CurrencyProvider.USD);
        Assert.AreEqual(
            new Logic.Currency.Currency(
                CurrencyProvider.EUR.Name, 
                CurrencyProvider.EUR.Code, 
                CurrencyProvider.EUR.Sign), 
            CurrencyProvider.EUR);
        Assert.AreEqual(
            new Logic.Currency.Currency(CurrencyProvider.EUR), CurrencyProvider.EUR);
    }

    [TestMethod]
    [DynamicData(nameof(CurrencyDataProvider.EqualsCurrencies), typeof(CurrencyDataProvider))]
    public void Currency_Equals_ReturnsBool(Logic.Currency.Currency currency1, object currency2, bool expected)
    {
        bool result = currency1.Equals(currency2);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [DynamicData(nameof(CurrencyDataProvider.ToStringCurrencies), typeof(CurrencyDataProvider))]
    public void Currency_ToString_ReturnsString(Logic.Currency.Currency currency, string expected)
    {
        string result = currency.ToString();

        Assert.AreEqual(expected, result);
    }
}
