# Telegram Bot for Currency Exchange Rate

This project is a **Telegram bot** designed to display the exchange rate of foreign currencies to UAH. It uses the **PrivatBank Public API** to fetch exchange rates based on user input, such as the currency code (e.g., USD) and the date. The bot also provides error handling and notifications for invalid inputs or issues with fetching data.

## Features

- **Currency Exchange Rate**: 
  - User inputs a **currency code** (e.g., USD) and a **date**, and the bot fetches the exchange rate from the selected currency to UAH on the specified date.
  
- **Error Notifications**:
  - The bot will notify users if:
    - The currency code is invalid.
    - The date format is incorrect.
    - No data is available for the specified currency and date.

- **Custom Settings**:
  - Users can set preferences for viewing the currency rate. These settings are cached for future use, allowing users to quickly check the exchange rate without re-entering the currency code and date.

## API Used

- **PrivatBank API**: The bot uses the PrivatBank public API to get exchange rate data. [API Documentation](https://api.privatbank.ua/#p24/exchangeArchive)
