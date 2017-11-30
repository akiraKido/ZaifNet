using System.Security.Cryptography;
using System.Text;

namespace ZaifNet.Api
{
    /// <summary>
    /// 認証の必要な API に使う API キーです。
    /// API キーは zaif のウェブサイトから入手してください。
    /// </summary>
    public class ApiKey
    {
        public string Key { get; }
        public string Secret { get; }

        /// <summary>
        /// API キーとシークレットキーからインスタンスを構築します。
        /// </summary>
        /// <param name="key">API キー文字列</param>
        /// <param name="secret">シークレットキー文字列</param>
        public ApiKey(string key, string secret)
        {
            Key = key;
            Secret = secret;
        }

        /// <summary>
        /// メッセージを SHA512 で署名します。
        /// </summary>
        /// <param name="query">署名するメッセージ</param>
        /// <returns>署名されたメッセージ</returns>
        public string MakeSignedHex(string query)
        {
            using (var hmac = new HMACSHA512(Encoding.ASCII.GetBytes(Secret)))
            {
                var hashValue = hmac.ComputeHash(Encoding.ASCII.GetBytes(query));
                var builder = new StringBuilder(hashValue.Length * 2);
                foreach (var b in hashValue) builder.Append($"{b:x2}");
                return builder.ToString();
            }
        }
    }
}