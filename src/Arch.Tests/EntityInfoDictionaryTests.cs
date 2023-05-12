using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arch.Core;
using Arch.Core.Utils;

namespace Arch.Tests;

/// <summary>
///     Tests the <see cref="JaggedArray{T}"/> for its functionality.
/// </summary>
[TestFixture]
public class EntityInfoDictionaryTests
{


    /// <summary>
    ///     Checks whether the <see cref="JaggedArray{T}"/> adds <see cref="EntityInfo"/> correctly.
    /// </summary>
    [Test]
    public void Add()
    {
        var size = 1000;
        var entityInfo = new JaggedArray<EntityInfo>(size);

        // Adds
        for (var i = 0; i < size; i++)
        {
            // Set the version to the current index.
            var version = i;
            entityInfo.Add(i, new EntityInfo(default, default, version));
        }

        // Check
        for (var i = 0; i < size; i++)
        {
            // Iterate over the dictionary if the index doesn't match the version then something went wrong
            var version = entityInfo[i].Version;
            if (version != i)
            {
                Assert.Fail("Dictionary does not returns the correct object");
            }
        }
    }

    /// <summary>
    ///     Checks whether the <see cref="JaggedArray{T}"/> trims correctly.
    /// </summary>
    [Test]
    public void TrimExcess()
    {
        var size = 10000;
        var entityInfo = new JaggedArray<EntityInfo>(size);

        // Add infos
        for(var i = 0; i < size; i++)
        {
            var version = i;
            entityInfo.Add(i, new EntityInfo(default, default, version));
        }

        // Trims
        entityInfo.TrimExcess();

        for (var i = 0; i < size ; i++)
        {
            var version = entityInfo[i].Version;
            if(version != i)
            {
                Assert.Fail($"{nameof(JaggedArray<EntityInfo>.TrimExcess)} method deleted items");
            }
        }
    }
}
