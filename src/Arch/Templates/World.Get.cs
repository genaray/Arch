

using System;
using System.Diagnostics.Contracts;
using Arch.Core;
using Arch.Core.Utils;

namespace Arch.Core
{
    public partial class World
    {
        
        [Pure]
        public Components<T0, T1> Get<T0, T1>(Entity entity)
        {
            ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
            var slot = entitySlot.Slot;
            var archetype = entitySlot.Archetype;
            return archetype.Get<T0, T1>(ref slot);
        }
        
        [Pure]
        public Components<T0, T1, T2> Get<T0, T1, T2>(Entity entity)
        {
            ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
            var slot = entitySlot.Slot;
            var archetype = entitySlot.Archetype;
            return archetype.Get<T0, T1, T2>(ref slot);
        }
        
        [Pure]
        public Components<T0, T1, T2, T3> Get<T0, T1, T2, T3>(Entity entity)
        {
            ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
            var slot = entitySlot.Slot;
            var archetype = entitySlot.Archetype;
            return archetype.Get<T0, T1, T2, T3>(ref slot);
        }
        
        [Pure]
        public Components<T0, T1, T2, T3, T4> Get<T0, T1, T2, T3, T4>(Entity entity)
        {
            ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
            var slot = entitySlot.Slot;
            var archetype = entitySlot.Archetype;
            return archetype.Get<T0, T1, T2, T3, T4>(ref slot);
        }
        
        [Pure]
        public Components<T0, T1, T2, T3, T4, T5> Get<T0, T1, T2, T3, T4, T5>(Entity entity)
        {
            ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
            var slot = entitySlot.Slot;
            var archetype = entitySlot.Archetype;
            return archetype.Get<T0, T1, T2, T3, T4, T5>(ref slot);
        }
        
        [Pure]
        public Components<T0, T1, T2, T3, T4, T5, T6> Get<T0, T1, T2, T3, T4, T5, T6>(Entity entity)
        {
            ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
            var slot = entitySlot.Slot;
            var archetype = entitySlot.Archetype;
            return archetype.Get<T0, T1, T2, T3, T4, T5, T6>(ref slot);
        }
        
        [Pure]
        public Components<T0, T1, T2, T3, T4, T5, T6, T7> Get<T0, T1, T2, T3, T4, T5, T6, T7>(Entity entity)
        {
            ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
            var slot = entitySlot.Slot;
            var archetype = entitySlot.Archetype;
            return archetype.Get<T0, T1, T2, T3, T4, T5, T6, T7>(ref slot);
        }
        
        [Pure]
        public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8>(Entity entity)
        {
            ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
            var slot = entitySlot.Slot;
            var archetype = entitySlot.Archetype;
            return archetype.Get<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ref slot);
        }
        
        [Pure]
        public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(Entity entity)
        {
            ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
            var slot = entitySlot.Slot;
            var archetype = entitySlot.Archetype;
            return archetype.Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ref slot);
        }
        
        [Pure]
        public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Entity entity)
        {
            ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
            var slot = entitySlot.Slot;
            var archetype = entitySlot.Archetype;
            return archetype.Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(ref slot);
        }
        
        [Pure]
        public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Entity entity)
        {
            ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
            var slot = entitySlot.Slot;
            var archetype = entitySlot.Archetype;
            return archetype.Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(ref slot);
        }
        
        [Pure]
        public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Entity entity)
        {
            ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
            var slot = entitySlot.Slot;
            var archetype = entitySlot.Archetype;
            return archetype.Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(ref slot);
        }
        
        [Pure]
        public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Entity entity)
        {
            ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
            var slot = entitySlot.Slot;
            var archetype = entitySlot.Archetype;
            return archetype.Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(ref slot);
        }
        
        [Pure]
        public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Entity entity)
        {
            ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
            var slot = entitySlot.Slot;
            var archetype = entitySlot.Archetype;
            return archetype.Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(ref slot);
        }
        
        [Pure]
        public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Entity entity)
        {
            ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
            var slot = entitySlot.Slot;
            var archetype = entitySlot.Archetype;
            return archetype.Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(ref slot);
        }
        
        [Pure]
        public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Entity entity)
        {
            ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
            var slot = entitySlot.Slot;
            var archetype = entitySlot.Archetype;
            return archetype.Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(ref slot);
        }
        
        [Pure]
        public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(Entity entity)
        {
            ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
            var slot = entitySlot.Slot;
            var archetype = entitySlot.Archetype;
            return archetype.Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(ref slot);
        }
        
        [Pure]
        public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(Entity entity)
        {
            ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
            var slot = entitySlot.Slot;
            var archetype = entitySlot.Archetype;
            return archetype.Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(ref slot);
        }
        
        [Pure]
        public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(Entity entity)
        {
            ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
            var slot = entitySlot.Slot;
            var archetype = entitySlot.Archetype;
            return archetype.Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(ref slot);
        }
        
        [Pure]
        public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(Entity entity)
        {
            ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
            var slot = entitySlot.Slot;
            var archetype = entitySlot.Archetype;
            return archetype.Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(ref slot);
        }
        
        [Pure]
        public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(Entity entity)
        {
            ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
            var slot = entitySlot.Slot;
            var archetype = entitySlot.Archetype;
            return archetype.Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(ref slot);
        }
        
        [Pure]
        public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(Entity entity)
        {
            ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
            var slot = entitySlot.Slot;
            var archetype = entitySlot.Archetype;
            return archetype.Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22>(ref slot);
        }
        
        [Pure]
        public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(Entity entity)
        {
            ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
            var slot = entitySlot.Slot;
            var archetype = entitySlot.Archetype;
            return archetype.Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23>(ref slot);
        }
        
        [Pure]
        public Components<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24> Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(Entity entity)
        {
            ref var entitySlot = ref EntityInfo.GetEntityData(entity.Id);
            var slot = entitySlot.Slot;
            var archetype = entitySlot.Archetype;
            return archetype.Get<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21, T22, T23, T24>(ref slot);
        }
            }
}
