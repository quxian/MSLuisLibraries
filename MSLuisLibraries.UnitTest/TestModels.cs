using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSLuisLibraries.Entities;
using MSLuisLibraries.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSLuisLibraries.UnitTest {
    [TestClass]
    public class TestModels {
        private const string _subscriptionKey = "b79f63ed94014a1595098db0e596870d";
        //https://westus.api.cognitive.microsoft.com/luis/api/v2.0/
        private const string _baseAddress = "https://westus.api.cognitive.microsoft.com/luis/api/v2.0";
        private const string _appId = "4891b116-dfef-45ed-8a09-3bae780ace73";
        private const string _versionId = "0.1";

        private ILuis _luis = new Luis(_subscriptionKey, _baseAddress, _appId, _versionId);

        [TestMethod]
        public async Task GetClosedListEntitiesAsync() {
            var result = await _luis
                 .Models
                 .GetClosedListEntityAsync();
        }

        [TestMethod]
        public async Task GetClosedListEntityByIdAsync() {
            var result = await _luis.Models.GetClosedListEntityAsync();
            if (result.Length > 0) {
                var only = await _luis.Models.GetClosedListEntityByIdAsync(result[0].id);
            }
        }

        [TestMethod]
        public async Task CreateClosedListEntityAsync() {
            var closedListEntity = new ClosedListEntity {
                name = "测试",
                subLists = new Sublist[] {
                   new Sublist {
                       canonicalForm = "城市",
                       list = new string[] {
                           "北京",
                           "上海"
                       }
                   },
                   new Sublist {
                       canonicalForm = "景区",
                       list = new string[] {
                           "故宫",
                           "长城"
                       }
                   }
               }
            };

            var result = await _luis.Models.CreateClosedListEntityAsync(closedListEntity);
        }

        [TestMethod]
        public async Task DeleteClosedListEntityByIdAsync() {
            var entities = await _luis.Models.GetClosedListEntityAsync();
            foreach (var entity in entities) {
                await _luis.Models.DeleteClosedListEntityByIdAsync(entity.id);
            }
        }


        [TestMethod]
        public async Task UpdateClosedListSublistAsync() {
            var entities = await _luis.Models.GetClosedListEntityAsync();

            var list = new List<string>();
            list.AddRange(entities[0].subLists[0].list);
            list.Add("新的");

            var subList = new Sublist {
                canonicalForm = entities[0].subLists[0].canonicalForm,
                list = list.ToArray()
            };

            await _luis.Models.UpdateClosedListSublistAsync(entities[0].id, entities[0].subLists[0].id, subList);
        }

        [TestMethod]
        public async Task AddClosedListSublistAsync() {
            var entities = await _luis.Models.GetClosedListEntityAsync();
            var subList = new Sublist {
                canonicalForm = "test",
                list = new string[] {
                    "t1",
                    "t2"
                }
            };

            var subId = await _luis.Models.AddClosedListSublistAsync(entities[0].id, subList);

        }
    }
}
