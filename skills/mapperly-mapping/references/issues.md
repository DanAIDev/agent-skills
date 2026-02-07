# Known issues / pitfalls

MapProperty in projections issue:
- https://github.com/riok/mapperly/issues/861
- The issue reports that MapProperty on IQueryable projections can be ignored unless a normal mapping method is also declared.
- Workaround: add a normal mapping method for the same type pair, then keep projection separate.
