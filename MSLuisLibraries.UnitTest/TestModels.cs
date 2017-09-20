using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSLuisLibraries.Entities;
using MSLuisLibraries.Interface;
using System.Collections.Generic;
using System.Linq;
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
                name = "城市",
                subLists = new Sublist[] {
                   new Sublist {
                       canonicalForm = "北京",
                       list = new string[] {
                           "北京",
                           "北京市",
                           "京"
                       }
                   },
                   new Sublist {
                       canonicalForm = "上海",
                       list = new string[] {
                           "上海",
                           "上海市",
                           "沪"
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

            var entity = entities
                 .ToList()
                 .Find(it => it.name == "城市");

            var id = entity.id;

            var subEntity = entity
                .subLists
                .ToList()
                .Find(it => it.canonicalForm == "北京");
            var subId = subEntity.id;

            var list = new List<string>();
            list.AddRange(entities[0].subLists[0].list);
            list.Add("jing");
            list.Add("beijing");

            var subList = new Sublist {
                canonicalForm = "北京",
                list = list.ToArray()
            };

            await _luis.Models.UpdateClosedListSublistAsync(id, subId, subList);
        }

        [TestMethod]
        public async Task AddClosedListSublistAsync() {
            var entities = await _luis.Models.GetClosedListEntityAsync();
            var subList = new Sublist {
                canonicalForm = "郑州",
                list = new string[] {
                    "郑州",
                    "郑州市"
                }
            };

            var subId = await _luis.Models.AddClosedListSublistAsync(entities[0].id, subList);

        }
    }
}
