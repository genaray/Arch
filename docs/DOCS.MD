| Case (ns/op) | 2.2.0 | repo | Change |
|---|---|---|---|
| `Add<Health>` | 43.5 | 35.2 | **−19 %** |
| `Remove<Health>` | 47.8 | 35.8 | **−25 %** |
| Add+Remove pair | 86 | 68 | **−21 %** |
| `Add<Tag>` | 68 | 52 | **−23 %** |
| `Add<Health,Mana>` + `Remove<Health,Mana>` | 308 | 125 | **−60 % to −70 %** |
| CommandBuffer record `Add<Health>`, 100k | 26,000–46,000 | 250 | **~100–180×** |
| CommandBuffer record/playback `Set` | 248 / 29 | 239 / 28 | same |
| `Get` / `Set` / `Has` | 6.1 / 6.3 / 5.7 | 6.2 / 6.0 / 5.8 | same |
| Iteration (inline, delegate, chunk loop) | 1.5–1.9 | 1.6–1.9 | same |
| `Destroy` (creation order, random order, by query) | 25 / 380 / 9.3 | 26 / 374 / 9.2 | same |
| `Create<Pos,Vel>`, cold / pre-sized | 77 / 46 | 78 / 45 | same |
| Bulk `Create<T>(1M)`, pre-sized or recycled ids | 12 / 11 | 7 / 7 | −40 % in isolation, but not stable (see below) |
| Bulk `Create<T>(1M)`, cold | 28–30 | 30–44 | 0 to -10 %, no consistent win |
