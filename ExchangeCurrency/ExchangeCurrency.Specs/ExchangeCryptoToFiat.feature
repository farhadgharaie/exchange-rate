Feature: ExchangeCryptoToFiat
	end user get cryptocurrency conversion to fiat currencies

@mytag
Scenario: Show Cryptocurrency exchangerate
	Given the user entered BTC cryptocurrency symbol
	When the user press submit
	Then the result should be <Currency> in screen
	Examples: 
	 | Currency |
	 | USD    |
	 | EUR    |
	 | BRL    |
	 | GBP    |
	 | AUD    |