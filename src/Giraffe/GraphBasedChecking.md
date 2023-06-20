# Graph-based checking

## As is

> dotnet "C:\Users\nojaf\Projects\fsharp\artifacts\bin\fsc\Debug\net7.0\fsc.dll" "@Giraffe.rsp" --times --test:GraphBasedChecking --nowarn:75

```
--------------------------------------------------------------------------------------------------------
|Phase name                          |Elapsed |Duration| WS(MB)|  GC0  |  GC1  |  GC2  |Handles|Threads|
|------------------------------------|--------|--------|-------|-------|-------|-------|-------|-------|
|Import mscorlib+FSharp.Core         |  0.3855|  0.3757|    172|      0|      0|      0|    270|     28|
|Parse inputs                        |  0.5658|  0.1738|    215|      0|      0|      0|    349|     49|
|Import non-system references        |  0.6296|  0.0604|    252|      0|      0|      0|    349|     49|
|Typecheck                           |  2.0297|  1.3971|    715|      2|      1|      1|    410|     69|
|Typechecked                         |  2.0357|  0.0022|    715|      0|      0|      0|    410|     69|
|Write Interface File                |  2.0391|  0.0001|    715|      0|      0|      0|    410|     69|
|Write XML doc signatures            |  2.0523|  0.0104|    718|      0|      0|      0|    410|     69|
|Write XML docs                      |  2.0614|  0.0061|    719|      0|      0|      0|    410|     69|
|Encode Interface Data               |  2.1530|  0.0884|    746|      0|      0|      0|    411|     69|
|Optimizations                       |  2.4398|  0.2834|    848|      0|      0|      0|    456|     69|
|Ending Optimizations                |  2.4432|  0.0000|    848|      0|      0|      0|    456|     69|
|Encoding OptData                    |  2.4601|  0.0139|    850|      0|      0|      0|    456|     69|
|TAST -> IL                          |  2.8814|  0.4183|    920|      0|      0|      0|    458|     69|
|>Write Started                      |  2.9030|  0.0098|    922|      0|      0|      0|    461|     69|
|>Module Generation Preparation      |  2.9079|  0.0011|    922|      0|      0|      0|    461|     69|
|>Module Generation Pass 1           |  2.9239|  0.0125|    923|      0|      0|      0|    461|     69|
|>Module Generation Pass 2           |  3.0169|  0.0899|    947|      0|      0|      0|    461|     69|
|>Module Generation Pass 3           |  3.0246|  0.0042|    947|      0|      0|      0|    461|     69|
|>Module Generation Pass 4           |  3.0297|  0.0022|    948|      0|      0|      0|    461|     69|
|>Finalize Module Generation Results |  3.0330|  0.0003|    948|      0|      0|      0|    461|     69|
|>Generated Tables and Code          |  3.0387|  0.0028|    948|      0|      0|      0|    461|     69|
|>Layout Header of Tables            |  3.0423|  0.0003|    948|      0|      0|      0|    461|     69|
|>Build String/Blob Address Tables   |  3.0476|  0.0015|    948|      0|      0|      0|    461|     69|
|>Sort Tables                        |  3.0508|  0.0003|    948|      0|      0|      0|    461|     69|
|>Write Header of tablebuf           |  3.0552|  0.0016|    948|      0|      0|      0|    461|     69|
|>Write Tables to tablebuf           |  3.0580|  0.0000|    949|      0|      0|      0|    461|     69|
|>Layout Metadata                    |  3.0614|  0.0000|    949|      0|      0|      0|    461|     69|
|>Write Metadata Header              |  3.0643|  0.0000|    949|      0|      0|      0|    461|     69|
|>Write Metadata Tables              |  3.0674|  0.0000|    949|      0|      0|      0|    461|     69|
|>Write Metadata Strings             |  3.0703|  0.0003|    949|      0|      0|      0|    461|     69|
|>Write Metadata User Strings        |  3.0730|  0.0003|    949|      0|      0|      0|    461|     69|
|>Write Blob Stream                  |  3.0762|  0.0004|    949|      0|      0|      0|    461|     69|
|>Fixup Metadata                     |  3.0791|  0.0003|    949|      0|      0|      0|    461|     69|
|>Generated IL and metadata          |  3.0990|  0.0170|    951|      0|      0|      0|    461|     69|
|>PDB: Defined 22 documents          |  3.1031|  0.0008|    951|      0|      0|      0|    461|     69|
|>PDB: Sorted 1311 methods           |  3.1230|  0.0160|    953|      0|      0|      0|    461|     69|
|>PDB: Created                       |  3.1303|  0.0041|    953|      0|      0|      0|    461|     69|
|>Layout image                       |  3.1352|  0.0019|    953|      0|      0|      0|    461|     69|
|>Writing Image                      |  3.1409|  0.0025|    953|      0|      0|      0|    461|     69|
|>Finalize PDB                       |  3.1444|  0.0005|    953|      0|      0|      0|    461|     69|
|>Signing Image                      |  3.1477|  0.0001|    953|      0|      0|      0|    460|     69|
|>Generate PDB Info                  |  3.1507|  0.0000|    953|      0|      0|      0|    460|     69|
|Write .NET Binary                   |  3.1540|  0.2691|    953|      0|      0|      0|    460|     69|
--------------------------------------------------------------------------------------------------------
```

## With signatures

> dotnet "C:\Users\nojaf\Projects\fsharp\artifacts\bin\fsc\Debug\net7.0\fsc.dll" @Extra.rsp --times --test:GraphBasedChecking --nowarn:75

```
--------------------------------------------------------------------------------------------------------
|Phase name                          |Elapsed |Duration| WS(MB)|  GC0  |  GC1  |  GC2  |Handles|Threads|
|------------------------------------|--------|--------|-------|-------|-------|-------|-------|-------|
|Import mscorlib+FSharp.Core         |  0.3893|  0.3792|    173|      0|      0|      0|    270|     28|
|Parse inputs                        |  0.5754|  0.1793|    227|      0|      0|      0|    350|     49|
|Import non-system references        |  0.6405|  0.0618|    264|      0|      0|      0|    350|     49|
|Typecheck                           |  1.2601|  0.6167|    774|      2|      1|      1|    412|     69|
|Typechecked                         |  1.2655|  0.0023|    774|      0|      0|      0|    412|     69|
|Write Interface File                |  1.2688|  0.0001|    774|      0|      0|      0|    412|     69|
|Write XML doc signatures            |  1.2829|  0.0114|    777|      0|      0|      0|    412|     69|
|Write XML docs                      |  1.2922|  0.0065|    777|      0|      0|      0|    412|     69|
|Encode Interface Data               |  1.3869|  0.0919|    804|      0|      0|      0|    413|     69|
|Optimizations                       |  1.6871|  0.2971|    899|      0|      0|      0|    458|     69|
|Ending Optimizations                |  1.6900|  0.0000|    899|      0|      0|      0|    458|     69|
|Encoding OptData                    |  1.7068|  0.0139|    901|      0|      0|      0|    458|     69|
|TAST -> IL                          |  2.1322|  0.4225|    977|      0|      0|      0|    460|     69|
|>Write Started                      |  2.1544|  0.0105|    978|      0|      0|      0|    463|     69|
|>Module Generation Preparation      |  2.1583|  0.0010|    978|      0|      0|      0|    463|     69|
|>Module Generation Pass 1           |  2.1740|  0.0125|    980|      0|      0|      0|    463|     69|
|>Module Generation Pass 2           |  2.2689|  0.0919|   1008|      0|      0|      0|    463|     69|
|>Module Generation Pass 3           |  2.2764|  0.0044|   1009|      0|      0|      0|    463|     69|
|>Module Generation Pass 4           |  2.2820|  0.0023|   1009|      0|      0|      0|    463|     69|
|>Finalize Module Generation Results |  2.2850|  0.0002|   1009|      0|      0|      0|    463|     69|
|>Generated Tables and Code          |  2.2906|  0.0030|   1009|      0|      0|      0|    463|     69|
|>Layout Header of Tables            |  2.2938|  0.0007|   1009|      0|      0|      0|    463|     69|
|>Build String/Blob Address Tables   |  2.2993|  0.0014|   1010|      0|      0|      0|    463|     69|
|>Sort Tables                        |  2.3030|  0.0005|   1010|      0|      0|      0|    463|     69|
|>Write Header of tablebuf           |  2.3072|  0.0017|   1010|      0|      0|      0|    463|     69|
|>Write Tables to tablebuf           |  2.3114|  0.0000|   1010|      0|      0|      0|    463|     69|
|>Layout Metadata                    |  2.3141|  0.0000|   1010|      0|      0|      0|    463|     69|
|>Write Metadata Header              |  2.3176|  0.0001|   1010|      0|      0|      0|    463|     69|
|>Write Metadata Tables              |  2.3203|  0.0001|   1010|      0|      0|      0|    463|     69|
|>Write Metadata Strings             |  2.3232|  0.0004|   1010|      0|      0|      0|    463|     69|
|>Write Metadata User Strings        |  2.3279|  0.0003|   1010|      0|      0|      0|    463|     69|
|>Write Blob Stream                  |  2.3308|  0.0003|   1010|      0|      0|      0|    463|     69|
|>Fixup Metadata                     |  2.3340|  0.0002|   1010|      0|      0|      0|    463|     69|
|>Generated IL and metadata          |  2.3553|  0.0188|   1012|      0|      0|      0|    463|     69|
|>PDB: Defined 42 documents          |  2.3593|  0.0007|   1012|      0|      0|      0|    463|     69|
|>PDB: Sorted 1311 methods           |  2.3800|  0.0175|   1015|      0|      0|      0|    463|     69|
|>PDB: Created                       |  2.3879|  0.0050|   1015|      0|      0|      0|    463|     69|
|>Layout image                       |  2.3930|  0.0023|   1015|      0|      0|      0|    463|     69|
|>Writing Image                      |  2.3982|  0.0026|   1015|      0|      0|      0|    463|     69|
|>Finalize PDB                       |  2.4018|  0.0005|   1015|      0|      0|      0|    463|     69|
|>Signing Image                      |  2.4049|  0.0001|   1015|      0|      0|      0|    462|     69|
|>Generate PDB Info                  |  2.4080|  0.0000|   1015|      0|      0|      0|    462|     69|
|Write .NET Binary                   |  2.4107|  0.2755|   1015|      0|      0|      0|    462|     69|
--------------------------------------------------------------------------------------------------------
```

**1.3971 -> 0.6167**