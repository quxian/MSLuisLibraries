using MSLuisLibraries.Interface;
using System;
using System.Linq;
using System.Net.Http;

namespace MSLuisLibraries
{
    public class Luis : ILuis {
        private HttpClient _client;
        private const string _key = "Ocp-Apim-Subscription-Key";

        private string _subscriptionKey;
        public string SubscriptionKey {
            get => _subscriptionKey;
            set {
                _subscriptionKey = value;
                if (null == _client) {
                    _client = new HttpClient();
                    _client.DefaultRequestHeaders.Add(_key, _subscriptionKey);
                } else if (_client.DefaultRequestHeaders.Any(it => it.Key == _key)) {
                    _client.DefaultRequestHeaders.Remove(_key);
                    _client.DefaultRequestHeaders.Add(_key, _subscriptionKey);
                }
            }
        }

        private string _baseAddress;
        public string BaseAddress {
            get => _baseAddress;
            set {
                _baseAddress = value;
                CombindeUri();
            }
        }

        private string _appId;
        public string AppId {
            get => _appId;
            set {
                _appId = value;
                CombindeUri();
            }
        }
        private string _versionId;
        public string VersionId {
            get => _versionId;
            set {
                _versionId = value;
                CombindeUri();
            }
        }

        private void CombindeUri() {
            if (!string.IsNullOrEmpty(_baseAddress) && !string.IsNullOrEmpty(_appId) && !string.IsNullOrEmpty(_versionId))
                Uri = new Uri($"{_baseAddress}/apps/{_appId}/versions/{_versionId}/");
        }

        private Uri _uri;
        public Uri Uri {
            get {
                if (null == _uri) {
                    throw new NullReferenceException("URI不能为空！");
                }
                return _uri;
            }
            set => _uri = value;
        }

        public Luis(string subscriptionKey, string baseAddress, string appId, string versionId) {
            SubscriptionKey = subscriptionKey;
            BaseAddress = baseAddress;
            AppId = appId;
            VersionId = versionId;
        }

        private IModels _models;
        public IModels Models {
            get {
                if (null == _models) {
                    _models = new Models(_client, Uri);
                }
                return _models;
            }
        }
    }
}
