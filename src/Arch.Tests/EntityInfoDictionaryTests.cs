using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arch.Core.Utils;

namespace Arch.Tests;

[TestFixture]
public class EntityInfoDictionaryTests
{
    [Test]
    public void Add()
    {
        var size = 1000;
        var entityInfo = new EntityInfoDictionary(size);

        
        for (int i = 0; i < size; i++)
        {
            // Set the version to the current index.
            var version = i;

            entityInfo.Add(i, new Core.EntityInfo(default, default, version));
        }

        for (int i = 0; i < size; i++)
        {
            // Iterate over the dictionary if the index doesn't match the version then something went wrong
            var version = entityInfo[i].Version;

            if (version != i)
            {
                Assert.Fail("Dictionary does not returns the correct object");
            }
        }
    }

    [Test]
    public void TrimExcess()
    {
        var size = 10000;
        var entityInfo = new EntityInfoDictionary(size);

        for(int i = 0; i < size; i++)
        {
            var version = i;

            entityInfo.Add(i, new Core.EntityInfo(default, default, version));
        }

        entityInfo.TrimExcess();

        for (int i = 0; i < size ; i++)
        {
            var version = entityInfo[i].Version;

            if(version != i)
            {
                Assert.Fail($"{nameof(EntityInfoDictionary.TrimExcess)} method deleted items");
            }
        }
    }
}
