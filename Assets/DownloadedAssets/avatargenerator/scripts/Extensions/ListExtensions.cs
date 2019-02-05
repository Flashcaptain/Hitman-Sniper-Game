﻿﻿﻿﻿﻿﻿﻿using System.Collections.Generic;

public static class ListExtensions
{
    public static void Swap(this List<int> list, int indexA, int indexB)
    {
        var tmp = list[indexA];
        list[indexA] = list[indexB];
        list[indexB] = tmp;
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   