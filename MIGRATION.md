# Migrating from 1.5.0 to 2.0.0

2.0.0 changes public signatures and a few behaviours, so it is a major release: **recompile** every project that
references the SDK, and expect compile errors in the places listed below. Nothing here needs a converter or a new
dependency; the Polly packages are no longer referenced.

## Amounts and rates are `decimal`

32 `double` properties are now `decimal` (nullable where they were nullable), so they match the Payment DTOs and sums add up
exactly (`0.1m + 0.2m == 0.3m`). JSON is unchanged: they are still plain numbers with the same property names.

| DTO | Properties |
|---|---|
| `TransferSaveRequestDto`, `TransferSaveInternalTransferRequestDto` | `Value` |
| `TransferGetResponseDto`, `TransferSaveInternalTransferResponseDto` | `Value`, `NetValue`, `TransferFee` |
| `InvoiceSaveRequestDto` | `Value`, `Deductions` |
| `InvoiceUpdateRequestDto`, `InvoiceGetResponseDto` | `Value`, `Deductions` |
| `FinanceBalanceResponseDto` | `Balance` |
| `FinancialTransactionGetResponseDto` | `Value`, `Balance` |
| `FinanceGetPaymentStatisticsResponseDto` | `Value`, `NetValue` |
| `FinanceGetSplitStatisticsResponseDto` | `Income`, `Value` |
| `AccountSaveRequestDto`, `AccountUpdateRequestDto`, `AccountInfoSaveRequestDto`, `AccountInfoGetResponseDto` | `IncomeValue` |
| `InvoiceTaxesDto` | `Iss`, `Pis`, `Cofins`, `Csll`, `Inss`, `Ir` (percentages: `2` means 2%) |
| `MunicipalServiceDto` | `IssTax` (percentage) |

What to change: add the `m` suffix to literals (`Value = 10.5m`); a `double` variable needs a deliberate
`(decimal)` conversion (or, better, to become a `decimal`); code that reads these properties into a `double` needs
`(double)` or a type change. The SDK applies no rounding: round to cents yourself where your rules require it.

## Typed requests and responses

These services used `object` request bodies and `dynamic` results (a boxed `JsonElement`, on which `result.id` throws). They
now take and return DTOs, with enum-like values as `string`s and unset optional fields left out of the request:

| Service | Methods |
|---|---|
| `SubscriptionService` | list, get, create, update, delete, list payments |
| `InstallmentService` | list, get, create, delete, list payments |
| `PaymentLinkService` | list, get, create, update, delete, add and list images |
| `PixService` | keys (list, create, get, delete) and static QR Codes (create, delete) |
| `PixTransactionService` | get, list, cancel, decode QR Code |
| `RecurringPixService` | list, get, list items, cancel, cancel item |
| `ChargebackService` | `GetPaymentChargebackAsync`, `CreateChargebackDisputeAsync`, `ListChargebacksAsync(ListChargebacksParameters)` |

- Build the request DTO instead of an anonymous object, and read properties instead of `JsonElement` members.
- `Delete*` methods return the body the Asaas sends (`{ deleted, id }`; the Pix key for `DeleteKeyAsync`) instead of `Task`.
  Awaiting and ignoring it still compiles, but it is a binary change.
- `ListSubscriptionPaymentsAsync` gained a `status` parameter and `ListInstallmentPaymentsAsync` is now `(id, status, ct)`;
  pass the cancellation token by name.
- The methods that still return `dynamic` are documented as a boxed `JsonElement`:
  `((JsonElement)result).GetProperty("id").GetString()`.

## Routes now follow the Asaas reference

| Before | Now |
|---|---|
| `POST v3/pix/qrCodes`, `DELETE v3/pix/qrCodes/{id}` | `POST v3/pix/qrCodes/static`, `DELETE v3/pix/qrCodes/static/{id}` |
| `v3/pix/transactions/recurring/...` | `v3/pix/transactions/recurrings/...` |
| `POST v3/pix/transactions` (`CreateTransactionAsync`) | `PayQrCodeAsync`: `POST v3/pix/qrCodes/pay` |
| `GET v3/paymentLinks/{id}/files`, `POST .../files` | `ListPaymentLinkImagesAsync` and `AddPaymentLinkImageAsync`: `.../images` |
| `GET v3/chargebacks/{id}` | `GetPaymentChargebackAsync`: `GET v3/payments/{paymentOrInstallmentId}/chargeback` |
| `POST v3/chargebacks/{id}/contest` (JSON) | `CreateChargebackDisputeAsync`: `POST v3/chargebacks/{id}/dispute` (multipart) |
| `POST v3/fiscalInfo/` as JSON | the same route as multipart/form-data, with a binary `certificateFile` |
| `POST v3/payments/{id}/documents` as JSON | multipart with `file`, `availableAfterPayment` and `type` |

The old methods that called undocumented routes are kept, marked `[Obsolete]`, and point at their replacement:
`GetChargebackAsync`, `RequestChargebackContestAsync`, `CreateTransactionAsync`, `GetQrCodeAsync`,
`GetRecurringTransactionItemAsync`, `ListPaymentLinkFilesAsync`, `UploadPaymentLinkFileAsync`, and the `object` overload of
`UploadPaymentDocumentAsync`. `ListChargebackDocumentsAsync`, `UploadChargebackDocumentAsync`, `UpdateFiscalInfoAsync`,
`GetMunicipalityAsync` are unchanged: their routes still have to be confirmed with the Asaas.

Uploads take a `Stream`, a file name and a content type. The SDK reads the stream once and disposes it with the request
(also when the request fails or is cancelled); if an argument is rejected first, the stream is left untouched.
`FiscalInfoSaveRequestDto.CertificateFile` (`byte[]`) is still accepted; there is also an overload that takes a `Stream`.

## Identifiers are validated and encoded

Pass identifiers exactly as the Asaas returned them. Each is percent-encoded as one path segment, so `/`, `?`, `#`, `%` and
spaces no longer change the route, and do not encode them yourself (that would encode `%` twice). A null, blank, `.` or
`..` identifier is rejected before any request is sent: `ArgumentException`, or the `ValidationException` that Customer,
Payment, Transfer and Webhook already threw for a missing id.

## Errors

`ApiException.Message` is now the Asaas `errors[].description` values joined with `; ` (it used to be the HTTP reason phrase,
such as `Bad Request`, for an `errors[]` body). New `ApiException.Errors` lists each `Code` and `Description`; branch on the
code instead of matching the message. `StatusCode` and `ResponseBody` are unchanged, and so are the existing constructors.
Code that compared `Message` with a reason phrase must read `Errors` or `StatusCode`.

## Retries and timeouts

- `RetryConfig` now works. **Only `GET` requests are retried** (408, 429, 500, 502, 503, 504, connection errors and timed-out
  attempts); a `POST`, `PUT`, `DELETE` or upload is sent once, because repeating a payment or transfer after an ambiguous
  failure could duplicate it. `Retry-After` and `RateLimit-Reset` are honoured, and a wait above `MaxDelayMs` (such as the
  12-hour quota) is not retried. Retries are on by default (`MaxRetries = 3`); set `MaxRetries = 0` to turn them off.
- `AsaasSdkConfig.Timeout` now applies to **each attempt**, not to the whole call, so a retried `GET` can take up to
  `(MaxRetries + 1)` timeouts plus the waits.
- Removed `BaseService.GetRetryPolicy` (it returned a Polly type and nothing called it) and the Polly package references.
- A service created over your own `HttpClient` retries only if that client's handlers include a `RetryHandler`.

## Smaller changes

- `AsaasSdkConfig.UserAgent` defaults to `asaas-sdk-dotnet/2.0.0` (it still said `1.1.5`).
- A top-level `"message": ""` in an error body no longer becomes the exception message.
