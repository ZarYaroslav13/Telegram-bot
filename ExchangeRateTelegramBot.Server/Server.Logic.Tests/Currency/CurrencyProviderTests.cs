using Server.Logic.Currency;
using Server.Logic.Tests.Data;

namespace Server.Logic.Tests.Currency;

[TestClass]
public class CurrencyProviderTests
{
    [TestMethod]
    [DynamicData(nameof(CurrencyDataProvider.CurrencyCodeData), typeof(CurrencyDataProvider))]
    public void CurrencyProvider_IsRegisteredCurrencyCode_ReturnsBool(string code, bool expected)
    {
        bool result = CurrencyProvider.IsRegisteredCurrencyCode(code);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [DynamicData(nameof(CurrencyDataProvider.CurrencySignData), typeof(CurrencyDataProvider))]
    public void CurrencyProvider_IsRegisteredCurrencySign_ReturnsBool(string sign, bool expected)
    {
        bool result = CurrencyProvider.IsRegisteredCurrencySign(sign);

        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    [DynamicData(nameof(CurrencyDataProvider.RegisteredCurrencies), typeof(CurrencyDataProvider))]
    public void CurrencyProvider_IsRegisteredCurrency_ReturnsBool(Logic.Currency.Currency currency, bool expected)
    {
        bool result = CurrencyProvider.IsRegisteredCurrency(currency);

        Assert.AreEqual(expected, result);
    }
}
