# ZaifNet

.NET 用の ZaifAPI ラッパーです。PublicAPI用のラッパーがあります。対応する通貨ペアは BTC-JPY/MONA-JPY/BTC-MONA/XEM-JPYです。

[JZaif](https://github.com/nyatla/JZaif) のクローンです。

# セットアップ

いつか nuget に。

それまではダウンロードまたは Clone して dll にするなりプロジェクトをインポートするなりでお願いします。

C# 7.0、.NET Standard で書いているため、rider 2017.2以降 または Visual Studio 2017 でご利用いただけます。

# 使い方の例

## Public API

### 現在の終値を得る

```csharp
private static async Task Main()
{
    using(var api = new PublicApi(CurrencyPair.BtcJpy))
    {
        var lastPrice = await publicApi.QueryLastPriceAsync();
        Console.WriteLine(lastPrice);
    }
}
```

# ZaifNetSample.csproj を開く際の注意

シークレットキーなどを保持するため `private.json` というファイルを設置しています。

Clone するとエラーが出る可能性があるので、`ZaifNetSmaple` プロジェクト配下に `private.json`

を作成して、`Key` と `Secret` をキーにした json を作成してください。

# ライセンス

JZaif に見習い、2 条項 BSD とします。
