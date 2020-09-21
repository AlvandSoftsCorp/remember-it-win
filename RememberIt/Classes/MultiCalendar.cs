using System;

namespace RememberIt
{
    /// <summary>
    /// This class implements the algorithms by which we can convert gregorian, jalali(Persian) and lunar(arabic)
    /// to each other.
    /// It has also some static member functions that makes it easy to work with.
    /// </summary>
    public class MultiCalendar
    {
        #region Contant and Global vaiables
        const int GREG_ORG_GDP = 0;          /*  The origine of Gerigorian calendar (0001/01/01 Greg)*/
        const int GREG_MAX_GDP = 3652058;    /*  Days passd from 1/1/1 AD to 9999 Dec 31 AD which is the maximum supported date in gregorian calendar (9999/12/31 Greg)*/
        const int LUN_ORG_GDP = 227013;      /*  Days passd from 1/1/1 AD to 622 Jul 18 AD which is the starting point of lunar calendar (0001/01/01 Hejri)*/
        const int LUN_MAX_GDP = 3652058;     /*  Days passd from 1/1/1 AD to 2563 Dec 16 AD which is the maximum supported date in lunar calendar (9666/04/03 Hejri) == (9999/12/31 Greg)*/
        const int JAL_ORG_GDP = 226894;      /*  Days passd from 1/1/1 AD to 622 March 3 AD which is the starting point of jalali calendar (0001/01/01 Jalali) */
        const int JAL_MAX_GDP = 3652058;     /*  Days passd from 1/1/1 AD to 9999 Dec 31 AD which is the maximum supported date in jalali calendar (9378/10/10 Jalali) */

        const int GREG_MAX_YEAR = 9999;
        const int JAL_MAX_YEAR = 9378;
        const int LUN_MAX_YEAR = 9666;

        const string GREG_MAX_DATE = "9999/12/31";  // Gregorian Date
        const string LUN_MAX_DATE = "2001/12/29";   // Lunar Date
        const string JAL_MAX_DATE = "9378/10/10";   // Jalali Date

        // The object's GDP (Gregorian Days Passed after christ)
        double m_Gdp = 0;


        int[] GregMonthDaysSoundYear = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        int[] GregMonthDaysLeapYear = { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        int[] JalMonthDaysSoundYear = { 31, 31, 31, 31, 31, 31, 30, 30, 30, 30, 30, 29 };
        int[] JalMonthDaysLeapYear = { 31, 31, 31, 31, 31, 31, 30, 30, 30, 30, 30, 30 };
        int[] LunMonthDaysSoundYear = { 30, 29, 30, 29, 30, 29, 30, 29, 30, 29, 30, 29 };
        int[] LunMonthDaysLeapYear = { 30, 29, 30, 29, 30, 29, 30, 29, 30, 29, 30, 30 };

        string[] GREG_WEEK_DAYS = { "Saturday", "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
        string[] GREG_WEEK_DAYS_ABBR = { "Sat", "Sun", "Mon", "Tue", "Wed", "Thr", "Fri" };

        string[] JAL_WEEK_DAYS = { "شنبه", "یکشنبه", "دوشنبه", "سه شنبه", "چهار شنبه", "پنج شنبه", "جمعه" };
        string[] JAL_WEEK_DAYS_ABBR = { "ش", "ی", "د", "س", "چ", "پ", "ج" };

        string[] LUN_WEEK_DAYS = { "شنبه", "یکشنبه", "دوشنبه", "سه شنبه", "چهار شنبه", "پنج شنبه", "جمعه" };
        string[] LUN_WEEK_DAYS_ABBR = { "ش", "ی", "د", "س", "چ", "پ", "ج" };


        string[] GREG_MONTHS = { "January", "February", "March", "April", "May", "June", "July", "August", "Septamber", "October", "November", "December" };
        string[] GREG_MONTHS_ABBR = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

        string[] JAL_MONTHS = { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };
        string[] JAL_MONTHS_ABBR = { "فرو", "ارد", "خرد", "تیر", "مرد", "شهر", "مهر", "آبا", "آذر", "دی", "بهم", "اسف" };

        string[] LUN_MONTHS = { "محرم", "صفر", "ربیع الاول", "ربیع الثانی", "جمادی الاول", "جمادی الثانی", "رجب", "شعبان", "رمضان", "شوال", "ذی القعده", "ذی الحجه" };
        string[] LUN_MONTHS_ABBR = { "محرم", "صفر", "ربیع الاول", "ربیع الثانی", "جمادی الاول", "جمادی الثانی", "رجب", "شعبان", "رمضان", "شوال", "ذی القعدا", "ذی الحجه" };



        const int LUNAR_LEAP_YEARS_COUNT = 3544;
        int[] LunarLeapYears = { 
		2,5,7,10,13,16,18,21,24,26,29,32,35,37,40,43,46,48,51,54,
		56,59,62,65,67,70,73,76,78,81,84,86,89,92,95,97,100,103,106,108,
		111,114,116,119,122,125,127,130,133,136,138,141,144,146,149,152,155,157,160,163,
		166,168,171,174,176,179,182,185,187,190,193,196,198,201,204,206,209,212,215,217,
		220,223,226,228,231,234,236,239,242,245,247,250,253,256,258,261,264,266,269,272,
		275,277,280,283,286,288,291,294,296,299,302,305,307,310,313,316,318,321,324,326,
		329,332,335,337,340,343,346,348,351,354,356,359,362,365,367,370,373,376,378,381,
		384,386,389,392,395,397,400,403,406,408,411,414,416,419,422,425,427,430,433,436,
		438,441,444,446,449,452,455,457,460,463,466,468,471,474,476,479,482,485,487,490,
		493,496,498,501,504,506,509,512,515,517,520,523,526,528,531,534,536,539,542,545,
		547,550,553,556,558,561,564,566,569,572,575,577,580,583,586,588,591,594,596,599,
		602,605,607,610,613,616,618,621,624,626,629,632,635,637,640,643,646,648,651,654,
		656,659,662,665,667,670,673,676,678,681,684,686,689,692,695,697,700,703,706,708,
		711,714,716,719,722,725,727,730,733,736,738,741,744,746,749,752,755,757,760,763,
		766,768,771,774,776,779,782,785,787,790,793,796,798,801,804,806,809,812,815,817,
		820,823,826,828,831,834,836,839,842,845,847,850,853,856,858,861,864,866,869,872,
		875,877,880,883,886,888,891,894,896,899,902,905,907,910,913,916,918,921,924,926,
		929,932,935,937,940,943,946,948,951,954,956,959,962,965,967,970,973,976,978,981,
		984,986,989,992,995,997,1000,1003,1006,1008,1011,1014,1016,1019,1022,1025,1027,1030,1033,1036,
		1038,1041,1044,1046,1049,1052,1055,1057,1060,1063,1066,1068,1071,1074,1076,1079,1082,1085,1087,1090,
		1093,1096,1098,1101,1104,1106,1109,1112,1115,1117,1120,1123,1126,1128,1131,1134,1136,1139,1142,1145,
		1147,1150,1153,1156,1158,1161,1164,1166,1169,1172,1175,1177,1180,1183,1186,1188,1191,1194,1196,1199,
		1202,1205,1207,1210,1213,1216,1218,1221,1224,1226,1229,1232,1235,1237,1240,1243,1246,1248,1251,1254,
		1256,1259,1262,1265,1267,1270,1273,1276,1278,1281,1284,1286,1289,1292,1295,1297,1300,1303,1306,1308,
		1311,1314,1316,1319,1322,1325,1327,1330,1333,1336,1338,1341,1344,1346,1349,1352,1355,1357,1360,1363,
		1366,1368,1371,1374,1376,1379,1382,1385,1387,1390,1393,1396,1398,1401,1404,1406,1409,1412,1415,1417,
		1420,1423,1426,1428,1431,1434,1436,1439,1442,1445,1447,1450,1453,1456,1458,1461,1464,1466,1469,1472,
		1475,1477,1480,1483,1486,1488,1491,1494,1496,1499,1502,1505,1507,1510,1513,1516,1518,1521,1524,1526,
		1529,1532,1535,1537,1540,1543,1546,1548,1551,1554,1556,1559,1562,1565,1567,1570,1573,1576,1578,1581,
		1584,1586,1589,1592,1595,1597,1600,1603,1606,1608,1611,1614,1616,1619,1622,1625,1627,1630,1633,1636,
		1638,1641,1644,1646,1649,1652,1655,1657,1660,1663,1666,1668,1671,1674,1676,1679,1682,1685,1687,1690,
		1693,1696,1698,1701,1704,1706,1709,1712,1715,1717,1720,1723,1726,1728,1731,1734,1736,1739,1742,1745,
		1747,1750,1753,1756,1758,1761,1764,1766,1769,1772,1775,1777,1780,1783,1786,1788,1791,1794,1796,1799,
		1802,1805,1807,1810,1813,1816,1818,1821,1824,1826,1829,1832,1835,1837,1840,1843,1846,1848,1851,1854,
		1856,1859,1862,1865,1867,1870,1873,1876,1878,1881,1884,1886,1889,1892,1895,1897,1900,1903,1906,1908,
		1911,1914,1916,1919,1922,1925,1927,1930,1933,1936,1938,1941,1944,1946,1949,1952,1955,1957,1960,1963,
		1966,1968,1971,1974,1976,1979,1982,1985,1987,1990,1993,1996,1998,2001,2004,2006,2009,2012,2015,2017,
		2020,2023,2026,2028,2031,2034,2036,2039,2042,2045,2047,2050,2053,2056,2058,2061,2064,2066,2069,2072,
		2075,2077,2080,2083,2086,2088,2091,2094,2096,2099,2102,2105,2107,2110,2113,2116,2118,2121,2124,2126,
		2129,2132,2135,2137,2140,2143,2146,2148,2151,2154,2156,2159,2162,2165,2167,2170,2173,2176,2178,2181,
		2184,2186,2189,2192,2195,2197,2200,2203,2206,2208,2211,2214,2216,2219,2222,2225,2227,2230,2233,2236,
		2238,2241,2244,2246,2249,2252,2255,2257,2260,2263,2266,2268,2271,2274,2276,2279,2282,2285,2287,2290,
		2293,2296,2298,2301,2304,2306,2309,2312,2315,2317,2320,2323,2326,2328,2331,2334,2336,2339,2342,2345,
		2347,2350,2353,2356,2358,2361,2364,2366,2369,2372,2375,2377,2380,2383,2386,2388,2391,2394,2396,2399,
		2402,2405,2407,2410,2413,2416,2418,2421,2424,2426,2429,2432,2435,2437,2440,2443,2446,2448,2451,2454,
		2456,2459,2462,2465,2467,2470,2473,2476,2478,2481,2484,2486,2489,2492,2495,2497,2500,2503,2506,2508,
		2511,2514,2516,2519,2522,2525,2527,2530,2533,2536,2538,2541,2544,2546,2549,2552,2555,2557,2560,2563,
		2566,2568,2571,2574,2576,2579,2582,2585,2587,2590,2593,2596,2598,2601,2604,2606,2609,2612,2615,2617,
		2620,2623,2626,2628,2631,2634,2636,2639,2642,2645,2647,2650,2653,2656,2658,2661,2664,2666,2669,2672,
		2675,2677,2680,2683,2686,2688,2691,2694,2696,2699,2702,2705,2707,2710,2713,2716,2718,2721,2724,2726,
		2729,2732,2735,2737,2740,2743,2746,2748,2751,2754,2756,2759,2762,2765,2767,2770,2773,2776,2778,2781,
		2784,2786,2789,2792,2795,2797,2800,2803,2806,2808,2811,2814,2816,2819,2822,2825,2827,2830,2833,2836,
		2838,2841,2844,2846,2849,2852,2855,2857,2860,2863,2866,2868,2871,2874,2876,2879,2882,2885,2887,2890,
		2893,2896,2898,2901,2904,2906,2909,2912,2915,2917,2920,2923,2926,2928,2931,2934,2936,2939,2942,2945,
		2947,2950,2953,2956,2958,2961,2964,2966,2969,2972,2975,2977,2980,2983,2986,2988,2991,2994,2996,2999,
		3002,3005,3007,3010,3013,3016,3018,3021,3024,3026,3029,3032,3035,3037,3040,3043,3046,3048,3051,3054,
		3056,3059,3062,3065,3067,3070,3073,3076,3078,3081,3084,3086,3089,3092,3095,3097,3100,3103,3106,3108,
		3111,3114,3116,3119,3122,3125,3127,3130,3133,3136,3138,3141,3144,3146,3149,3152,3155,3157,3160,3163,
		3166,3168,3171,3174,3176,3179,3182,3185,3187,3190,3193,3196,3198,3201,3204,3206,3209,3212,3215,3217,
		3220,3223,3226,3228,3231,3234,3236,3239,3242,3245,3247,3250,3253,3256,3258,3261,3264,3266,3269,3272,
		3275,3277,3280,3283,3286,3288,3291,3294,3296,3299,3302,3305,3307,3310,3313,3316,3318,3321,3324,3326,
		3329,3332,3335,3337,3340,3343,3346,3348,3351,3354,3356,3359,3362,3365,3367,3370,3373,3376,3378,3381,
		3384,3386,3389,3392,3395,3397,3400,3403,3406,3408,3411,3414,3416,3419,3422,3425,3427,3430,3433,3436,
		3438,3441,3444,3446,3449,3452,3455,3457,3460,3463,3466,3468,3471,3474,3476,3479,3482,3485,3487,3490,
		3493,3496,3498,3501,3504,3506,3509,3512,3515,3517,3520,3523,3526,3528,3531,3534,3536,3539,3542,3545,
		3547,3550,3553,3556,3558,3561,3564,3566,3569,3572,3575,3577,3580,3583,3586,3588,3591,3594,3596,3599,
		3602,3605,3607,3610,3613,3616,3618,3621,3624,3626,3629,3632,3635,3637,3640,3643,3646,3648,3651,3654,
		3656,3659,3662,3665,3667,3670,3673,3676,3678,3681,3684,3686,3689,3692,3695,3697,3700,3703,3706,3708,
		3711,3714,3716,3719,3722,3725,3727,3730,3733,3736,3738,3741,3744,3746,3749,3752,3755,3757,3760,3763,
		3766,3768,3771,3774,3776,3779,3782,3785,3787,3790,3793,3796,3798,3801,3804,3806,3809,3812,3815,3817,
		3820,3823,3826,3828,3831,3834,3836,3839,3842,3845,3847,3850,3853,3856,3858,3861,3864,3866,3869,3872,
		3875,3877,3880,3883,3886,3888,3891,3894,3896,3899,3902,3905,3907,3910,3913,3916,3918,3921,3924,3926,
		3929,3932,3935,3937,3940,3943,3946,3948,3951,3954,3956,3959,3962,3965,3967,3970,3973,3976,3978,3981,
		3984,3986,3989,3992,3995,3997,4000,4003,4006,4008,4011,4014,4016,4019,4022,4025,4027,4030,4033,4036,
		4038,4041,4044,4046,4049,4052,4055,4057,4060,4063,4066,4068,4071,4074,4076,4079,4082,4085,4087,4090,
		4093,4096,4098,4101,4104,4106,4109,4112,4115,4117,4120,4123,4126,4128,4131,4134,4136,4139,4142,4145,
		4147,4150,4153,4156,4158,4161,4164,4166,4169,4172,4175,4177,4180,4183,4186,4188,4191,4194,4196,4199,
		4202,4205,4207,4210,4213,4216,4218,4221,4224,4226,4229,4232,4235,4237,4240,4243,4246,4248,4251,4254,
		4256,4259,4262,4265,4267,4270,4273,4276,4278,4281,4284,4286,4289,4292,4295,4297,4300,4303,4306,4308,
		4311,4314,4316,4319,4322,4325,4327,4330,4333,4336,4338,4341,4344,4346,4349,4352,4355,4357,4360,4363,
		4366,4368,4371,4374,4376,4379,4382,4385,4387,4390,4393,4396,4398,4401,4404,4406,4409,4412,4415,4417,
		4420,4423,4426,4428,4431,4434,4436,4439,4442,4445,4447,4450,4453,4456,4458,4461,4464,4466,4469,4472,
		4475,4477,4480,4483,4486,4488,4491,4494,4496,4499,4502,4505,4507,4510,4513,4516,4518,4521,4524,4526,
		4529,4532,4535,4537,4540,4543,4546,4548,4551,4554,4556,4559,4562,4565,4567,4570,4573,4576,4578,4581,
		4584,4586,4589,4592,4595,4597,4600,4603,4606,4608,4611,4614,4616,4619,4622,4625,4627,4630,4633,4636,
		4638,4641,4644,4646,4649,4652,4655,4657,4660,4663,4666,4668,4671,4674,4676,4679,4682,4685,4687,4690,
		4693,4696,4698,4701,4704,4706,4709,4712,4715,4717,4720,4723,4726,4728,4731,4734,4736,4739,4742,4745,
		4747,4750,4753,4756,4758,4761,4764,4766,4769,4772,4775,4777,4780,4783,4786,4788,4791,4794,4796,4799,
		4802,4805,4807,4810,4813,4816,4818,4821,4824,4826,4829,4832,4835,4837,4840,4843,4846,4848,4851,4854,
		4856,4859,4862,4865,4867,4870,4873,4876,4878,4881,4884,4886,4889,4892,4895,4897,4900,4903,4906,4908,
		4911,4914,4916,4919,4922,4925,4927,4930,4933,4936,4938,4941,4944,4946,4949,4952,4955,4957,4960,4963,
		4966,4968,4971,4974,4976,4979,4982,4985,4987,4990,4993,4996,4998,5001,5004,5006,5009,5012,5015,5017,
		5020,5023,5026,5028,5031,5034,5036,5039,5042,5045,5047,5050,5053,5056,5058,5061,5064,5066,5069,5072,
		5075,5077,5080,5083,5086,5088,5091,5094,5096,5099,5102,5105,5107,5110,5113,5116,5118,5121,5124,5126,
		5129,5132,5135,5137,5140,5143,5146,5148,5151,5154,5156,5159,5162,5165,5167,5170,5173,5176,5178,5181,
		5184,5186,5189,5192,5195,5197,5200,5203,5206,5208,5211,5214,5216,5219,5222,5225,5227,5230,5233,5236,
		5238,5241,5244,5246,5249,5252,5255,5257,5260,5263,5266,5268,5271,5274,5276,5279,5282,5285,5287,5290,
		5293,5296,5298,5301,5304,5306,5309,5312,5315,5317,5320,5323,5326,5328,5331,5334,5336,5339,5342,5345,
		5347,5350,5353,5356,5358,5361,5364,5366,5369,5372,5375,5377,5380,5383,5386,5388,5391,5394,5396,5399,
		5402,5405,5407,5410,5413,5416,5418,5421,5424,5426,5429,5432,5435,5437,5440,5443,5446,5448,5451,5454,
		5456,5459,5462,5465,5467,5470,5473,5476,5478,5481,5484,5486,5489,5492,5495,5497,5500,5503,5506,5508,
		5511,5514,5516,5519,5522,5525,5527,5530,5533,5536,5538,5541,5544,5546,5549,5552,5555,5557,5560,5563,
		5566,5568,5571,5574,5576,5579,5582,5585,5587,5590,5593,5596,5598,5601,5604,5606,5609,5612,5615,5617,
		5620,5623,5626,5628,5631,5634,5636,5639,5642,5645,5647,5650,5653,5656,5658,5661,5664,5666,5669,5672,
		5675,5677,5680,5683,5686,5688,5691,5694,5696,5699,5702,5705,5707,5710,5713,5716,5718,5721,5724,5726,
		5729,5732,5735,5737,5740,5743,5746,5748,5751,5754,5756,5759,5762,5765,5767,5770,5773,5776,5778,5781,
		5784,5786,5789,5792,5795,5797,5800,5803,5806,5808,5811,5814,5816,5819,5822,5825,5827,5830,5833,5836,
		5838,5841,5844,5846,5849,5852,5855,5857,5860,5863,5866,5868,5871,5874,5876,5879,5882,5885,5887,5890,
		5893,5896,5898,5901,5904,5906,5909,5912,5915,5917,5920,5923,5926,5928,5931,5934,5936,5939,5942,5945,
		5947,5950,5953,5956,5958,5961,5964,5966,5969,5972,5975,5977,5980,5983,5986,5988,5991,5994,5996,5999,
		6002,6005,6007,6010,6013,6016,6018,6021,6024,6026,6029,6032,6035,6037,6040,6043,6046,6048,6051,6054,
		6056,6059,6062,6065,6067,6070,6073,6076,6078,6081,6084,6086,6089,6092,6095,6097,6100,6103,6106,6108,
		6111,6114,6116,6119,6122,6125,6127,6130,6133,6136,6138,6141,6144,6146,6149,6152,6155,6157,6160,6163,
		6166,6168,6171,6174,6176,6179,6182,6185,6187,6190,6193,6196,6198,6201,6204,6206,6209,6212,6215,6217,
		6220,6223,6226,6228,6231,6234,6236,6239,6242,6245,6247,6250,6253,6256,6258,6261,6264,6266,6269,6272,
		6275,6277,6280,6283,6286,6288,6291,6294,6296,6299,6302,6305,6307,6310,6313,6316,6318,6321,6324,6326,
		6329,6332,6335,6337,6340,6343,6346,6348,6351,6354,6356,6359,6362,6365,6367,6370,6373,6376,6378,6381,
		6384,6386,6389,6392,6395,6397,6400,6403,6406,6408,6411,6414,6416,6419,6422,6425,6427,6430,6433,6436,
		6438,6441,6444,6446,6449,6452,6455,6457,6460,6463,6466,6468,6471,6474,6476,6479,6482,6485,6487,6490,
		6493,6496,6498,6501,6504,6506,6509,6512,6515,6517,6520,6523,6526,6528,6531,6534,6536,6539,6542,6545,
		6547,6550,6553,6556,6558,6561,6564,6566,6569,6572,6575,6577,6580,6583,6586,6588,6591,6594,6596,6599,
		6602,6605,6607,6610,6613,6616,6618,6621,6624,6626,6629,6632,6635,6637,6640,6643,6646,6648,6651,6654,
		6656,6659,6662,6665,6667,6670,6673,6676,6678,6681,6684,6686,6689,6692,6695,6697,6700,6703,6706,6708,
		6711,6714,6716,6719,6722,6725,6727,6730,6733,6736,6738,6741,6744,6746,6749,6752,6755,6757,6760,6763,
		6766,6768,6771,6774,6776,6779,6782,6785,6787,6790,6793,6796,6798,6801,6804,6806,6809,6812,6815,6817,
		6820,6823,6826,6828,6831,6834,6836,6839,6842,6845,6847,6850,6853,6856,6858,6861,6864,6866,6869,6872,
		6875,6877,6880,6883,6886,6888,6891,6894,6896,6899,6902,6905,6907,6910,6913,6916,6918,6921,6924,6926,
		6929,6932,6935,6937,6940,6943,6946,6948,6951,6954,6956,6959,6962,6965,6967,6970,6973,6976,6978,6981,
		6984,6986,6989,6992,6995,6997,7000,7003,7006,7008,7011,7014,7016,7019,7022,7025,7027,7030,7033,7036,
		7038,7041,7044,7046,7049,7052,7055,7057,7060,7063,7066,7068,7071,7074,7076,7079,7082,7085,7087,7090,
		7093,7096,7098,7101,7104,7106,7109,7112,7115,7117,7120,7123,7126,7128,7131,7134,7136,7139,7142,7145,
		7147,7150,7153,7156,7158,7161,7164,7166,7169,7172,7175,7177,7180,7183,7186,7188,7191,7194,7196,7199,
		7202,7205,7207,7210,7213,7216,7218,7221,7224,7226,7229,7232,7235,7237,7240,7243,7246,7248,7251,7254,
		7256,7259,7262,7265,7267,7270,7273,7276,7278,7281,7284,7286,7289,7292,7295,7297,7300,7303,7306,7308,
		7311,7314,7316,7319,7322,7325,7327,7330,7333,7336,7338,7341,7344,7346,7349,7352,7355,7357,7360,7363,
		7366,7368,7371,7374,7376,7379,7382,7385,7387,7390,7393,7396,7398,7401,7404,7406,7409,7412,7415,7417,
		7420,7423,7426,7428,7431,7434,7436,7439,7442,7445,7447,7450,7453,7456,7458,7461,7464,7466,7469,7472,
		7475,7477,7480,7483,7486,7488,7491,7494,7496,7499,7502,7505,7507,7510,7513,7516,7518,7521,7524,7526,
		7529,7532,7535,7537,7540,7543,7546,7548,7551,7554,7556,7559,7562,7565,7567,7570,7573,7576,7578,7581,
		7584,7586,7589,7592,7595,7597,7600,7603,7606,7608,7611,7614,7616,7619,7622,7625,7627,7630,7633,7636,
		7638,7641,7644,7646,7649,7652,7655,7657,7660,7663,7666,7668,7671,7674,7676,7679,7682,7685,7687,7690,
		7693,7696,7698,7701,7704,7706,7709,7712,7715,7717,7720,7723,7726,7728,7731,7734,7736,7739,7742,7745,
		7747,7750,7753,7756,7758,7761,7764,7766,7769,7772,7775,7777,7780,7783,7786,7788,7791,7794,7796,7799,
		7802,7805,7807,7810,7813,7816,7818,7821,7824,7826,7829,7832,7835,7837,7840,7843,7846,7848,7851,7854,
		7856,7859,7862,7865,7867,7870,7873,7876,7878,7881,7884,7886,7889,7892,7895,7897,7900,7903,7906,7908,
		7911,7914,7916,7919,7922,7925,7927,7930,7933,7936,7938,7941,7944,7946,7949,7952,7955,7957,7960,7963,
		7966,7968,7971,7974,7976,7979,7982,7985,7987,7990,7993,7996,7998,8001,8004,8006,8009,8012,8015,8017,
		8020,8023,8026,8028,8031,8034,8036,8039,8042,8045,8047,8050,8053,8056,8058,8061,8064,8066,8069,8072,
		8075,8077,8080,8083,8086,8088,8091,8094,8096,8099,8102,8105,8107,8110,8113,8116,8118,8121,8124,8126,
		8129,8132,8135,8137,8140,8143,8146,8148,8151,8154,8156,8159,8162,8165,8167,8170,8173,8176,8178,8181,
		8184,8186,8189,8192,8195,8197,8200,8203,8206,8208,8211,8214,8216,8219,8222,8225,8227,8230,8233,8236,
		8238,8241,8244,8246,8249,8252,8255,8257,8260,8263,8266,8268,8271,8274,8276,8279,8282,8285,8287,8290,
		8293,8296,8298,8301,8304,8306,8309,8312,8315,8317,8320,8323,8326,8328,8331,8334,8336,8339,8342,8345,
		8347,8350,8353,8356,8358,8361,8364,8366,8369,8372,8375,8377,8380,8383,8386,8388,8391,8394,8396,8399,
		8402,8405,8407,8410,8413,8416,8418,8421,8424,8426,8429,8432,8435,8437,8440,8443,8446,8448,8451,8454,
		8456,8459,8462,8465,8467,8470,8473,8476,8478,8481,8484,8486,8489,8492,8495,8497,8500,8503,8506,8508,
		8511,8514,8516,8519,8522,8525,8527,8530,8533,8536,8538,8541,8544,8546,8549,8552,8555,8557,8560,8563,
		8566,8568,8571,8574,8576,8579,8582,8585,8587,8590,8593,8596,8598,8601,8604,8606,8609,8612,8615,8617,
		8620,8623,8626,8628,8631,8634,8636,8639,8642,8645,8647,8650,8653,8656,8658,8661,8664,8666,8669,8672,
		8675,8677,8680,8683,8686,8688,8691,8694,8696,8699,8702,8705,8707,8710,8713,8716,8718,8721,8724,8726,
		8729,8732,8735,8737,8740,8743,8746,8748,8751,8754,8756,8759,8762,8765,8767,8770,8773,8776,8778,8781,
		8784,8786,8789,8792,8795,8797,8800,8803,8806,8808,8811,8814,8816,8819,8822,8825,8827,8830,8833,8836,
		8838,8841,8844,8846,8849,8852,8855,8857,8860,8863,8866,8868,8871,8874,8876,8879,8882,8885,8887,8890,
		8893,8896,8898,8901,8904,8906,8909,8912,8915,8917,8920,8923,8926,8928,8931,8934,8936,8939,8942,8945,
		8947,8950,8953,8956,8958,8961,8964,8966,8969,8972,8975,8977,8980,8983,8986,8988,8991,8994,8996,8999,
		9002,9005,9007,9010,9013,9016,9018,9021,9024,9026,9029,9032,9035,9037,9040,9043,9046,9048,9051,9054,
		9056,9059,9062,9065,9067,9070,9073,9076,9078,9081,9084,9086,9089,9092,9095,9097,9100,9103,9106,9108,
		9111,9114,9116,9119,9122,9125,9127,9130,9133,9136,9138,9141,9144,9146,9149,9152,9155,9157,9160,9163,
		9166,9168,9171,9174,9176,9179,9182,9185,9187,9190,9193,9196,9198,9201,9204,9206,9209,9212,9215,9217,
		9220,9223,9226,9228,9231,9234,9236,9239,9242,9245,9247,9250,9253,9256,9258,9261,9264,9266,9269,9272,
		9275,9277,9280,9283,9286,9288,9291,9294,9296,9299,9302,9305,9307,9310,9313,9316,9318,9321,9324,9326,
		9329,9332,9335,9337,9340,9343,9346,9348,9351,9354,9356,9359,9362,9365,9367,9370,9373,9376,9378,9381,
		9384,9386,9389,9392,9395,9397,9400,9403,9406,9408,9411,9414,9416,9419,9422,9425,9427,9430,9433,9436,
		9438,9441,9444,9446,9449,9452,9455,9457,9460,9463,9466,9468,9471,9474,9476,9479,9482,9485,9487,9490,
		9493,9496,9498,9501,9504,9506,9509,9512,9515,9517,9520,9523,9526,9528,9531,9534,9536,9539,9542,9545,
		9547,9550,9553,9556,9558,9561,9564,9566,9569,9572,9575,9577,9580,9583,9586,9588,9591,9594,9596,9599,
		9602,9605,9607,9610,9613,9616,9618,9621,9624,9626,9629,9632,9635,9637,9640,9643,9646,9648,9651,9654};
        #endregion Contant and Global vaiables


        public static MultiCalendar FromDateTime(DateTime ADateTime)
        {
            MultiCalendar mc = new MultiCalendar();
            mc.SetGregDate(ADateTime.Year, ADateTime.Month, ADateTime.Day);
            mc.SetTime(ADateTime.Hour, ADateTime.Minute, ADateTime.Second);
            return mc;
        }

        /// <summary>
        /// Gets the integer part of given double number 
        /// Example: Int(12.2536) -> 12
        /// </summary>
        /// <param name="ADouble">An input double</param>
        /// <returns>The integer part of the given input double value</returns>
        int Int(double ADouble)
        {
            return (int)ADouble;
        }

         
         


        /// <summary>
        /// Gets the fraction part of a double number.
        /// Example: Frac(12.2536) -> 0.2536
        /// </summary>
        /// <param name="ADouble">An input double</param>
        /// <returns>The fraction part of the given input double value</returns>
        double Frac(double ADouble)
        {
            return ADouble - Int(ADouble);
        }



        /// <summary>
        /// Returns true if the given gregorian year is a leap year.
        /// </summary>
        /// <param name="AYear">A Gregorian Year</param>
        /// <returns>
        /// True: if the given gregorian year is a leap year.
        /// False: if the given gregorian year is a sound year.
        /// </returns>
        public bool IsGregLeapYear(int AYear)
        {
            if (AYear <= 0) return false;
            if (AYear % 4 != 0) return false;
            if (AYear % 100 == 0)
            {
                if (AYear % 400 == 0) return true;
                else return false;
            }
            return true;
        }




        /// <summary>
        /// Calculates the number of gregorian century out of given inputs.
        /// </summary>
        /// <param name="GregDaysPassed">An input GDP(Gregorian Days Passed After Christ)</param>
        /// <param name="RemainingDays">An output parameter which indicates a year in century</param>
        /// <returns>The number of a gregorian century</returns>
        int GetGregCentury(int GregDaysPassed, ref int RemainingDays)
        {
            int d = 0;
            int c = 0;
            int c_days = 0;
            while (true)
            {
                c++;
                if (c % 4 == 0) c_days = 36525;
                else c_days = 36524;

                if (d + c_days > GregDaysPassed) break;
                d += c_days;
            }
            RemainingDays = GregDaysPassed - d;
            return c;
        }



        /// <summary>
        /// Calculates the number of gregorian year out of given inputs.
        /// </summary>
        /// <param name="GregDaysPassed">An input GDP(Gregorian Days Passed After Christ)</param>
        /// <param name="RemainingDays">An output parameter which indicates a month in year</param>
        /// <returns>An integer which represents the year compliment of a gregorian date</returns>
        int GetGregYear(int GregDaysPassed, ref int RemainingDays)
        {
            RemainingDays = 0;
            if (GregDaysPassed < GREG_ORG_GDP) return -1;
            if (GregDaysPassed > GREG_MAX_GDP) return -1;


            int rem = 0;
            int c = this.GetGregCentury(GregDaysPassed, ref rem);

            int d = 0;
            int y = 0;
            int y_days = 0;
            while (true)
            {
                y++;
                if (this.IsGregLeapYear(y + (c - 1) * 100)) y_days = 366;
                else y_days = 365;

                if (d + y_days > rem) break;
                d += y_days;
            }
            RemainingDays = rem - d;
            return y + (c - 1) * 100;
        }



        /// <summary>
        /// Calculates the number of gregorian month out of given inputs.
        /// </summary>
        /// <param name="IsLeapYear">If the year is a leap year this input paramete must be set to true</param>
        /// <param name="YearDaysPassed">The number of days passed from the begining of the given year</param>
        /// <param name="RemainingDays">An output parameter which indicates a day in month</param>
        /// <returns>An integer which represents the month compliment of a gregorian date</returns>
        int GetGregMonth(bool IsLeapYear, int YearDaysPassed, ref int RemainingDays)
        {
            RemainingDays = 0;
            if (YearDaysPassed > 366) return -1;
            if (YearDaysPassed < 0) return -1;

            int[] M = null;
            if (IsLeapYear) M = GregMonthDaysLeapYear;
            else M = GregMonthDaysSoundYear;

            int d = 0;
            int m = 1;
            while (d + M[m - 1] <= YearDaysPassed)
            {
                d += M[m - 1];
                m++;
            }
            RemainingDays = YearDaysPassed - d;
            return m;
        }


        /// <summary>
        /// Converts a gregorian date to GDP (Gregorian Days Passed after christ)
        /// </summary>
        /// <param name="Year">Gregorian year</param>
        /// <param name="Month">Gregorian month</param>
        /// <param name="DayOfMonth">Gregorian day of month</param>
        /// <returns>A GDP (Gregorian Days Passed after christ)</returns>
        public int GregDateToGregDaysPassed(int Year, int Month, int DayOfMonth)
        {
            int c = 0;
            int y = 1;
            int d = 0;
            int[] M = null;
            while ((c + 1) * 100 < Year)
            {
                c++;
                if (c % 4 == 0) d += 36525;
                else d += 36524;
            }

            while (c * 100 + y < Year)
            {
                if (this.IsGregLeapYear(c * 100 + y)) d += 366;
                else d += 365;
                y++;
            }
            if (this.IsGregLeapYear(Year)) M = GregMonthDaysLeapYear;
            else M = GregMonthDaysSoundYear;

            for (int m = 1; m < Month; m++)
            {
                d += M[m - 1];
            }
            d += DayOfMonth;
            return d - 1;
        }


        /// <summary>
        /// Returns true if the given jalali year is a leap year.
        /// </summary>
        /// <param name="AJalaliYear">An input jalai(Persian) year</param>
        /// <returns>
        /// True: if the given jalali year is a leap year.
        /// False: if the given jalai year is a sound year.
        /// </returns>
        public bool IsJalLeapYear(int AJalaliYear)
        {
            for (int i = 0; i < 7; i++) if ((AJalaliYear - (i * 4 - 7)) % 33 == 0) return true;
            if ((AJalaliYear - (7 * 4 - 6)) % 33 == 0) return true;
            return false;
        }


        
        /// <summary>
        /// Gets the index of jalali leap year sequence.Refere to documentations for more information
        /// </summary>
        /// <param name="GregDaysPassed">An input GDP</param>
        /// <param name="RemainingDays">An output parameter which indicates a year in jalali leap-year sequence</param>
        /// <returns>
        /// The index of jalali leap year sequence
        /// </returns>
        int GetJalLeapSequence(int GregDaysPassed, ref int RemainingDays)
        {
            int JalDaysPassed = GregDaysPassed - JAL_ORG_GDP;
            int n = (JalDaysPassed + 2922) / 12053;

            if (n == 0)
            {
                RemainingDays = JalDaysPassed;
                return n;
            }

            if (n == 1)
            {
                RemainingDays = JalDaysPassed - 9131;
                return n;
            }

            RemainingDays = JalDaysPassed - (9131 + (n - 1) * 12053);
            return n;
        }


        /// <summary>
        /// Gets an integer which represents the year compliment of a jalali date
        /// </summary>
        /// <param name="GregDaysPassed">An input GDP</param>
        /// <param name="RemainingDays">An output parameter which indicates a month in year</param>
        /// <returns>An integer which represents the year compliment of a jalali date</returns>
        int GetJalYear(int GregDaysPassed, ref int RemainingDays)
        {
            RemainingDays = 0;
            if (GregDaysPassed < JAL_ORG_GDP) return -1;
            if (GregDaysPassed > JAL_MAX_GDP) return -1;

            int d = 0;
            int y = 0;

            int Rem = 0;
            int n = this.GetJalLeapSequence(GregDaysPassed, ref Rem);
            if (n == 0) y = 0;
            else y = n * 33 - 8;

            int y_days = 0;
            while (true)
            {
                y++;
                if (this.IsJalLeapYear(y)) y_days = 366;
                else y_days = 365;

                if (d + y_days > Rem) break;
                d += y_days;
            }
            RemainingDays = Rem - d;
            return y;
        }


        /// <summary>
        /// Gets an integer which represents the month compliment of a jalali date
        /// </summary>
        /// <param name="IsLeapYear">If the year is a leap year this input paramete must be set to true</param>
        /// <param name="YearDaysPassed">The number of days passed from the begining of the given year</param>
        /// <param name="RemainingDays">An output parameter which indicates a day in month</param>
        /// <returns>An integer which represents the month compliment of a jalali date</returns>
        int GetJalMonth(bool IsLeapYear, int YearDaysPassed, ref int RemainingDays)
        {
            RemainingDays = 0;
            if (YearDaysPassed > 366) return -1;
            if (YearDaysPassed < 0) return -1;

            int[] M = null;
            if (IsLeapYear) M = JalMonthDaysLeapYear;
            else M = JalMonthDaysSoundYear;

            int d = 0;
            int m = 1;
            while (d + M[m - 1] <= YearDaysPassed)
            {
                d += M[m - 1];
                m++;
            }
            RemainingDays = YearDaysPassed - d;
            return m;
        }


        /// <summary>
        /// Converts a jalai date to GDP (Gregorian Days Passed after christ)
        /// </summary>
        /// <param name="Year">Jalai year</param>
        /// <param name="Month">Jalai month</param>
        /// <param name="DayOfMonth">Jalai day of month</param>
        /// <returns>A GDP (Gregorian Days Passed after christ)</returns>
        public int JalDateToGregDaysPassed(int Year, int Month, int DayOfMonth)
        {
            int n = (Year + 7) / 33;
            int y = 0;
            int d = 0;
            int m = 1;
            int[] M = null;

            if (n == 0)
            {
                d = 0;
                y = 1;
            }
            else
            {
                d = 9131 + (n - 1) * 12053;
                y = n * 33 - 7;
            }

            while (y < Year)
            {
                if (this.IsJalLeapYear(y)) d += 366;
                else d += 365;
                y++;
            }

            if (this.IsJalLeapYear(y)) M = JalMonthDaysLeapYear;
            else M = JalMonthDaysSoundYear;

            while (m < Month)
            {
                d += M[m - 1];
                m++;
            }
            d += DayOfMonth;
            d += JAL_ORG_GDP;

            return d - 1;
        }



        /// <summary>
        /// Returns true if the given lunar year is a leap year
        /// </summary>
        /// <param name="AYear">An input lunar year</param>
        /// <returns>
        /// True: if the given lunar year is a leap year
        /// False: if the given lunar year is a sound year
        /// </returns>
        public bool IsLunLeapYear(int AYear)
        {
            //for (int i = 0; i < LUNAR_LEAP_YEARS_COUNT; i++)
            //{
            //	if (AYear == LunarLeapYears[i]) return true;
            //}

            int mid = 0;
            int h = 0;
            int t = LUNAR_LEAP_YEARS_COUNT - 1;

            while (h <= t)
            {
                mid = (t + h) / 2;
                if (AYear == LunarLeapYears[mid]) return true;
                else if (AYear < LunarLeapYears[mid]) t = mid - 1;
                else h = mid + 1;
            }
            return false;
        }



        

        /// <summary>
        /// Gets the lunar (Arabic-Hejri) year
        /// </summary>
        /// <param name="GregDaysPassed">An input GDP</param>
        /// <param name="RemainingDays">An output parameter which indicates a month in year</param>
        /// <returns>The lunar (Arabic-Hejri) year</returns>
        int GetLunYear(int GregDaysPassed, ref int RemainingDays)
        {
            // Output parameter initialization.
            RemainingDays = 0;
            if (GregDaysPassed < LUN_ORG_GDP) return -1;
            if (GregDaysPassed > LUN_MAX_GDP) return -1;

            int Rem = GregDaysPassed - LUN_ORG_GDP;
            int d = 0;
            int y = 0;
            int y_days = 0;

            while (true)
            {
                y++;
                if (this.IsLunLeapYear(y)) y_days = 355;
                else y_days = 354;

                if (d + y_days > Rem) break;
                d += y_days;
            }
            RemainingDays = Rem - d;
            return y;
        }



        
        /// <summary>
        /// Gets an integer which represents the month compliment of a lunar (Arabic-Hejri) date
        /// </summary>
        /// <param name="IsLeapYear">If the year is a leap year this input paramete must be set to true</param>
        /// <param name="YearDaysPassed">The number of days passed from the begining of the given year</param>
        /// <param name="RemainingDays">An output parameter which indicates a day in month</param>
        /// <returns>An integer which represents the month compliment of a lunar (Arabic-Hejri) date</returns>
        int GetLunMonth(bool IsLeapYear, int YearDaysPassed, ref int RemainingDays)
        {
            RemainingDays = 0;
            if (YearDaysPassed > 355) return -1;
            if (YearDaysPassed < 0) return -1;

            int[] M = null;
            if (IsLeapYear) M = LunMonthDaysLeapYear;
            else M = LunMonthDaysSoundYear;

            int d = 0;
            int m = 1;
            while (d + M[m - 1] <= YearDaysPassed)
            {
                d += M[m - 1];
                m++;
            }
            RemainingDays = YearDaysPassed - d;
            return m;
        }


        /// <summary>
        /// Converts a lunar (Arabic-Hejri) date to GDP (Gregorian Days Passed after christ).
        /// </summary>
        /// <param name="Year">A lunar year</param>
        /// <param name="Month">A lunar month</param>
        /// <param name="DayOfMonth">A lunar day of month</param>
        /// <returns>GDP (Gregorian Days Passed after christ)</returns>
        public int LunDateToGregDaysPassed(int Year, int Month, int DayOfMonth)
        {
            int y = 1;
            int d = 0;
            int m = 1;
            int[] M = null;

            while (y < Year)
            {
                if (this.IsLunLeapYear(y)) d += 355;
                else d += 354;
                y++;
            }

            if (this.IsLunLeapYear(y)) M = LunMonthDaysLeapYear;
            else M = LunMonthDaysSoundYear;

            while (m < Month)
            {
                d += M[m - 1];
                m++;
            }
            d += DayOfMonth;
            d += LUN_ORG_GDP;

            return d - 1;
        }



        /// <summary>
        /// Gets a 10 character lunar (Arabic-Hejri) date.
        /// </summary>
        /// <param name="GregDaysPassedGdp">An input GDP</param>
        /// <returns>A 10 character lunar (Arabic-Hejri) date.</returns>
        public string GetLunDate(double GregDaysPassedGdp)
        {
            int Rem1 = 0, Rem2 = 0;
            int y = this.GetLunYear((int)GregDaysPassedGdp, ref Rem1);
            int m = this.GetLunMonth(this.IsLunLeapYear(y), Rem1, ref Rem2);
            int d = Rem2 + 1;
            string Res = string.Format("{0}/{1}/{2}", AddZeroToleft(y, 4), AddZeroToleft(m, 2), AddZeroToleft(d, 2));
            return Res;
        }



        /// <summary>
        /// Converts a given number to string and Adds a number of zero before it.
        /// Example: AddZeroToleft(12, 4) -> "0012"
        /// </summary>
        /// <param name="Number">The given number. Ex: 12</param>
        /// <param name="DigitsCount">The number of padding zeroes</param>
        /// <returns>A zero padded integer string</returns>
        string AddZeroToleft(int Number, int DigitsCount)
        {
            string res = Number.ToString().PadLeft(DigitsCount, '0');
            return res;
            //string res = Number.ToString();
            //int n = res.Length;
            //string S0 = "0";

            //for (int i = n; i < DigitsCount; i++)
            //{
            //    res = S0 + res;
            //}
            //return res;
        }


        /// <summary>
        /// Gets an integer number between 0 to 23 which indicates the houre
        /// </summary>
        /// <returns>An integer number between 0 to 23 which indicates the houre</returns>
        public int GetHoure24()
        {
            double h = this.Frac(this.m_Gdp) * 24;
            return (this.Int(h));
        }


        /// <summary>
        /// Gets an integer number between 0 to 59 which indicates the minute
        /// </summary>
        /// <returns>An integer number between 0 to 59 which indicates the minute</returns>
        public int GetMinute()
        {
            double h = this.Frac(this.m_Gdp) * 24;
            double m = this.Frac(h) * 60;
            return (this.Int(m));
        }


        /// <summary>
        /// Gets an integer number between 0 to 59 which indicates the second
        /// </summary>
        /// <returns>An integer number between 0 to 59 which indicates the second</returns>
        public int GetSecond()
        {
            double h = this.Frac(this.m_Gdp) * 24;
            double m = this.Frac(h) * 60;
            double s = this.Frac(m) * 60;
            return (this.Int(s));
        }


        /// <summary>
        /// Gets an integer number between 0 to 999 which indicates the milliseconds
        /// </summary>
        /// <returns>An integer number between 0 to 999 which indicates the milliseconds</returns>
        public int GetMilliSecond()
        {
            double h = this.Frac(this.m_Gdp) * 24;
            double m = this.Frac(h) * 60;
            double s = this.Frac(m) * 60;
            double ms = this.Frac(s) * 1000;
            return (this.Int(ms));
        }
        
        
        /// <summary>
        /// Gets a double number indecating the micro second section of time
        /// </summary>
        /// <returns>A double number indecating the micro second section of time</returns>
        public double GetMicroSecond()
        {
            double h = this.Frac(this.m_Gdp) * 24;
            double m = this.Frac(h) * 60;
            double s = this.Frac(m) * 60;
            double ms = this.Frac(s) * 1000;
            double us = this.Frac(ms) * 1000;
            return us;
        }


        /// <summary>
        /// Sets the time
        /// </summary>
        /// <param name="Houre24">Houre 0 to 23</param>
        /// <param name="Minute">Minute</param>
        /// <param name="Second">Second</param>
        /// <param name="MilliSecond">Milli Second</param>
        /// <param name="MicroSecond">Micro Second</param>
        /// <returns>
        /// Success: 0
        /// Failure: -1
        /// </returns>
        public int SetTime(int Houre24, int Minute, int Second, int MilliSecond, double MicroSecond)
        {
            if (Houre24 >= 24) return -1;
            if (Houre24 < 0) return -1;
            if (Minute >= 60) return -1;
            if (Minute < 0) return -1;
            if (Second >= 60) return -1;
            if (Second < 0) return -1;
            if (MilliSecond >= 1000) return -1;
            if (MilliSecond < 0) return -1;
            if (MicroSecond >= 1000) return -1;
            if (MicroSecond < 0) return -1;


            double d = 0;
            d += (double)Houre24 / (double)24;
            d += (double)Minute / (double)1440;					// 24 * 60 = 1440
            d += (double)Second / (double)86400;				// 24 * 60 * 60 = 86400
            d += (double)MilliSecond / (double)86400000;		// 24 * 60 * 60 * 1000 = 86400000
            d += MicroSecond / (double)86400000000;		        // 24 * 60 * 60 * 1000 * 1000 = 86400000000

            this.m_Gdp = this.Int(this.m_Gdp) + d;

            return 0;
        }



        /// <summary>
        /// Sets the time
        /// </summary>
        /// <param name="Houre24">Houre 0 to 23</param>
        /// <param name="Minute">Minute</param>
        /// <param name="Second">Second</param>
        /// <param name="MilliSecond">Milli Second</param>
        /// <returns>
        /// Success: 0
        /// Failure: -1
        /// </returns>
        public int SetTime(int Houre24, int Minute, int Second, int MilliSecond)
        {
            return this.SetTime(Houre24, Minute, Second, MilliSecond, 0);
        }



        /// <summary>
        /// Sets the time
        /// </summary>
        /// <param name="Houre24">Houre 0 to 23</param>
        /// <param name="Minute">Minute</param>
        /// <param name="Second">Second</param>
        /// <returns>
        /// Success: 0
        /// Failure: -1
        /// </returns>
        public int SetTime(int Houre24, int Minute, int Second)
        {
            return this.SetTime(Houre24, Minute, Second, 0, 0);
        }



        /// <summary>
        /// Sets the time
        /// </summary>
        /// <param name="Houre24">Houre 0 to 23</param>
        /// <param name="Minute">Minute</param>
        /// <returns>
        /// Success: 0
        /// Failure: -1
        /// </returns>
        public int SetTime(int Houre24, int Minute)
        {
            return this.SetTime(Houre24, Minute, 0, 0, 0);
        }

        
        
        /// <summary>
        /// Gets an integer number between 0 to 6 indicating the day of week.
        /// </summary>
        /// <returns>
        /// An integer number between 0 to 6 indicating the day of week.
        /// </returns>
        public int GetDayOfWeekNumber()
        {
            return this.Int(this.m_Gdp) % 7;
        }



        /// <summary>
        /// Gets the gregorian name of week day as a string.
        /// </summary>
        /// <returns>
        /// The gregorian name of week day as a string.
        /// </returns>
        public string GetDayOfWeekTitleGreg()
        {
            return GREG_WEEK_DAYS[this.GetDayOfWeekNumber()];
        }


        /// <summary>
        /// Gets the jalali (Persian) name of week day as a string
        /// </summary>
        /// <returns>
        /// The jalali (Persian) name of week day as a string.
        /// </returns>
        public string GetDayOfWeekTitleJal()
        {
            return JAL_WEEK_DAYS[this.GetDayOfWeekNumber()];
        }



        /// <summary>
        /// Gets the lunar (Arabic) name of week day as a string
        /// </summary>
        /// <returns>
        /// The lunar (Arabic) name of week day as a string
        /// </returns>
        public string GetDayOfWeekTitleLun()
        {
            return LUN_WEEK_DAYS[this.GetDayOfWeekNumber()];
        }


        /// <summary>
        /// Gets an integer which represents the year compliment of a gregorian date
        /// </summary>
        /// <returns>
        /// An integer which represents the year compliment of a gregorian date
        /// </returns>
        public int GetGregYear()
        {
            int Rem = 0;
            return this.GetGregYear(this.m_Gdp, ref Rem);
        }


        /// <summary>
        /// Gets an integer which represents the year compliment of a gregorian date
        /// </summary>
        /// <param name="Gdp">An input GDP</param>
        /// <param name="Rem">An output remainder</param>
        /// <returns>
        /// An integer which represents the year compliment of a gregorian date
        /// </returns>
        private int GetGregYear(double Gdp, ref int Rem)
        {
            return GetGregYear((int)Gdp, ref Rem);
        }


        /// <summary>
        /// Gets an integer which represents the month compliment of a gregorian date
        /// </summary>
        /// <returns>
        /// An integer which represents the month compliment of a gregorian date
        /// </returns>
        public int GetGregMonth()
        {
            int Rem = 0;
            int Y = this.GetGregYear(this.m_Gdp, ref Rem);
            int M = this.GetGregMonth(this.IsGregLeapYear(Y), Rem, ref Rem);
            return M;
        }



        /// <summary>
        /// Gets a string which represents the month title of a gregorian date
        /// </summary>
        /// <returns>
        /// A string which represents the month title of a gregorian date
        /// </returns>
        public string GetGregMonthTitle()
        {
            return GREG_MONTHS[this.GetGregMonth() - 1];
        }

        
        /// <summary>
        /// Gets a string which represents the abbreviated month title of a gregorian date
        /// </summary>
        /// <returns>
        /// A string which represents the abbreviated month title of a gregorian date
        /// </returns>
        public string GetGregMonthTitleAbbr()
        {
            return GREG_MONTHS_ABBR[this.GetGregMonth() - 1];
        }


        /// <summary>
        /// Gets an integer which represents the month day compliment of a gregorian date.
        /// </summary>
        /// <returns>
        /// An integer which represents the month day compliment of a gregorian date.
        /// </returns>
        public int GetGregDayOfMonth()
        {
            int Rem = 0;
            int Y = this.GetGregYear(this.m_Gdp, ref Rem);
            int M = this.GetGregMonth(this.IsGregLeapYear(Y), Rem, ref Rem);
            int D = Rem + 1;
            return D;
        }



        /// <summary>
        /// Gets a 10 character gregorian date
        /// </summary>
        /// <returns>
        /// A 10 character gregorian date
        /// </returns>
        public string GetGregDate()
        {
            int y = this.GetGregYear();
            int m = this.GetGregMonth();
            int d = this.GetGregDayOfMonth();
            string Res = string.Format("{0}/{1}/{2}", AddZeroToleft(y, 4), AddZeroToleft(m, 2), AddZeroToleft(d, 2));
            return Res;
        }

        public string GetGregDate(string DateSeparator)
        {
            int y = this.GetGregYear();
            int m = this.GetGregMonth();
            int d = this.GetGregDayOfMonth();
            string Res = string.Format("{0}/{1}/{2}", AddZeroToleft(y, 4), AddZeroToleft(m, 2), AddZeroToleft(d, 2));
            Res = Res.Replace("/", DateSeparator);
            return Res;
        }


        /// <summary>
        /// Sets a given gregorian date between (0001/01/01 Gregorian) and (9999/12/31 Gregorian)
        /// </summary>
        /// <param name="GregYear">Year</param>
        /// <param name="GregMonth">Month</param>
        /// <param name="GregDayOfMonth">Day</param>
        /// <returns>
        /// Success: 0
        /// Failure: -1
        /// </returns>
        public int SetGregDate(int GregYear, int GregMonth, int GregDayOfMonth)
        {
            if (!this.IsValidGregDate(GregYear, GregMonth, GregDayOfMonth)) return -1;

            this.m_Gdp = this.GregDateToGregDaysPassed(GregYear, GregMonth, GregDayOfMonth);
            return 0;
        }


        // Checks if the given date is a valid gregorian date and returns

        /// <summary>
        /// Checks if the given date is a valid gregorian date and returns true if valid.
        /// </summary>
        /// <param name="GregYear">Year</param>
        /// <param name="GregMonth">Month</param>
        /// <param name="GregDayOfMonth">Day</param>
        /// <returns>
        /// True: Valid gregorian date
        /// False: Invalid Gregorian date
        /// </returns>
        public bool IsValidGregDate(int GregYear, int GregMonth, int GregDayOfMonth)
        {
            // This routine is not compeletely tested.
            if (GregYear < 0) return false;
            if (GregYear > 9999) return false;
            if (GregMonth < 0) return false;
            if (GregMonth > 12) return false;
            if (GregDayOfMonth < 0) return false;
            if (GregDayOfMonth > 31) return false;

            if (GregDayOfMonth == 1) return true;
            MultiCalendar mc = new MultiCalendar();
            mc.SetGregDate(GregYear, GregMonth, 1);
            double gdp = mc.GetGdp();

            for (int i = 1; i < GregDayOfMonth; i++)
            {
                mc.SetGdp(gdp + i);
                if (mc.GetGregDayOfMonth() == GregDayOfMonth) return true;
            }
            return false;
        }



        /// <summary>
        /// Sets a given gregorian date between (0001/01/01 Gregorian) and (9999/12/31 Gregorian)
        /// </summary>
        /// <param name="AGregDate">An input date string</param>
        /// <returns>
        /// Success: 0
        /// Failure: -1
        /// </returns>
        public int SetGregDate(string AGregDate)
        {
            int n = AGregDate.Length;
            if (n < 10) return -1;
            try
            {
                int y = Convert.ToInt32(AGregDate.Substring(0, 4));
                int m = Convert.ToInt32(AGregDate.Substring(5, 2));
                int d = Convert.ToInt32(AGregDate.Substring(8, 2));
                return this.SetGregDate(y, m, d);
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        
       
        /// <summary>
        /// Gets the number of days in a given gregorian month
        /// </summary>
        /// <param name="GregYear">Year</param>
        /// <param name="GregMonth">Month</param>
        /// <returns>
        /// The number of days in a given gregorian month
        /// </returns>
        public int GregDaysIn(int GregYear, int GregMonth)
        {
            if (GregYear < 0) return -1;
            if (GregYear > GREG_MAX_YEAR) return -1;
            if (GregMonth < 1) return -1;
            if (GregMonth > 12) return -1;

            if (IsGregLeapYear(GregYear)) return GregMonthDaysLeapYear[GregMonth - 1];
            return GregMonthDaysSoundYear[GregMonth - 1];
        }


        /// <summary>
        /// Gets the number of days in a given gregorian year
        /// </summary>
        /// <param name="GregYear">Year</param>
        /// <returns>
        /// The number of days in a given gregorian year
        /// </returns>
        public int GregDaysIn(int GregYear)
        {
            if (GregYear < 0) return -1;
            if (GregYear > GREG_MAX_GDP) return -1;
            if (IsGregLeapYear(GregYear)) return 366;
            return 365;
        }



        /// <summary>
        /// Gets an integer which represents the year compliment of a jalali date
        /// </summary>
        /// <returns>
        /// An integer which represents the year compliment of a jalali date
        /// </returns>
        public int GetJalYear()
        {
            int Rem = 0;
            return this.GetJalYear(this.m_Gdp, ref Rem);
        }



        /// <summary>
        /// Gets an integer which represents the year compliment of a jalali date
        /// </summary>
        /// <param name="Gdp">An input GDP</param>
        /// <param name="Rem">An output remainder</param>
        /// <returns>
        /// An integer which represents the year compliment of a jalali date
        /// </returns>
        private int GetJalYear(double Gdp, ref int Rem)
        {
            return GetJalYear((int)Gdp, ref Rem);
        }



        /// <summary>
        /// Gets an integer which represents the month compliment of a jalali date
        /// </summary>
        /// <returns>
        /// An integer which represents the month compliment of a jalali date
        /// </returns>
        public int GetJalMonth()
        {
            int Rem = 0;
            int Y = this.GetJalYear(this.m_Gdp, ref Rem);
            int M = this.GetJalMonth(this.IsJalLeapYear(Y), Rem, ref Rem);
            return M;
        }



        /// <summary>
        /// Gets a string which represents the month title of a jalali date
        /// </summary>
        /// <returns>
        /// A string which represents the month title of a jalali date
        /// </returns>
        public string GetJalMonthTitle()
        {
            return JAL_MONTHS[this.GetJalMonth() - 1];
        }



        /// <summary>
        /// Gets a string which represents the abbreviated month title of a jalali date
        /// </summary>
        /// <returns>
        /// A string which represents the abbreviated month title of a jalali date
        /// </returns>
        public string GetJalMonthTitleAbbr()
        {
            return JAL_MONTHS_ABBR[this.GetJalMonth() - 1];
        }



        /// <summary>
        /// Gets an integer which represents the month day compliment of a jalali date
        /// </summary>
        /// <returns>
        /// An integer which represents the month day compliment of a jalali date
        /// </returns>
        public int GetJalDayOfMonth()
        {
            int Rem = 0;
            int Y = this.GetJalYear(this.m_Gdp, ref Rem);
            int M = this.GetJalMonth(this.IsJalLeapYear(Y), Rem, ref Rem);
            int D = Rem + 1;
            return D;
        }



        /// <summary>
        /// Gets a 10 character jalali date
        /// </summary>
        /// <returns>
        /// A 10 character jalali date
        /// </returns>
        public string GetJalDate()
        {
            int y = this.GetJalYear();
            int m = this.GetJalMonth();
            int d = this.GetJalDayOfMonth();
            string Res = string.Format("{0}/{1}/{2}", AddZeroToleft(y, 4), AddZeroToleft(m, 2), AddZeroToleft(d, 2));
            return Res;
        }

        
        
        public string GetJalDate(string DateSeparator)
        {
            int y = this.GetJalYear();
            int m = this.GetJalMonth();
            int d = this.GetJalDayOfMonth();
            string Res = string.Format("{0}/{1}/{2}", AddZeroToleft(y, 4), AddZeroToleft(m, 2), AddZeroToleft(d, 2));
            Res = Res.Replace("/", DateSeparator);
            return Res;
        }


        /// <summary>
        /// Checks if the given date is a valid jalali date and returns true if valid.
        /// </summary>
        /// <param name="JalYear">Year</param>
        /// <param name="JalMonth">Month</param>
        /// <param name="JalDayOfMonth">Day</param>
        /// <returns>
        /// True: Valid jalali date
        /// False: Invalid jalali date
        /// </returns>
        public bool IsValidJalDate(int JalYear, int JalMonth, int JalDayOfMonth)
        {
            // This routine is not compeletely tested.

            if (JalYear < 0) return false;
            if (JalYear > 9999) return false;
            if (JalMonth < 0) return false;
            if (JalMonth > 12) return false;
            if (JalDayOfMonth < 0) return false;
            if (JalDayOfMonth > 31) return false;
            if (JalDayOfMonth == 1) return true;

            MultiCalendar mc = new MultiCalendar();
            mc.SetJalDate(JalYear, JalMonth, 1);
            double gdp = mc.GetGdp();

            for (int i = 1; i < JalDayOfMonth; i++)
            {
                mc.SetGdp(gdp + i);
                if (mc.GetJalDayOfMonth() == JalDayOfMonth) return true;
            }
            return false;

        }



        /// <summary>
        /// Sets a given jalali date between (0001/01/01 Jalali) and (9378/10/10 Jalali)
        /// </summary>
        /// <param name="JalYear">Year</param>
        /// <param name="JalMonth">Month</param>
        /// <param name="JalDayOfMonth">Day</param>
        /// <returns>
        /// Success: 0
        /// Failure: -1
        /// </returns>
        public int SetJalDate(int JalYear, int JalMonth, int JalDayOfMonth)
        {
            if (!this.IsValidJalDate(JalYear, JalMonth, JalDayOfMonth)) return -1;

            this.m_Gdp = this.JalDateToGregDaysPassed(JalYear, JalMonth, JalDayOfMonth);
            return 0;
        }

        
        
        /// <summary>
        /// Sets a given jalali date between (0001/01/01 Jalali) and (9378/10/10 Jalali)
        /// </summary>
        /// <param name="AJalDate">An input date string between (0001/01/01 Jalali) and (9378/10/10 Jalali)</param>
        /// <returns>
        /// Success: 0
        /// Failure: -1
        /// </returns>
        public int SetJalDate(string AJalDate)
        {
            int n = AJalDate.Length;
            if (n < 10) return -1;
            try
            {
                int y = Convert.ToInt32(AJalDate.Substring(0, 4));
                int m = Convert.ToInt32(AJalDate.Substring(5, 2));
                int d = Convert.ToInt32(AJalDate.Substring(8, 2));
                return this.SetJalDate(y, m, d);
            }
            catch (Exception e)
            {
                return -1;
            }
        }


        /// <summary>
        /// Gets the number of days in a given jalali month
        /// </summary>
        /// <param name="JalYear">Year</param>
        /// <param name="JalMonth">Month</param>
        /// <returns>
        /// The number of days in a given jalali month
        /// </returns>
        public int JalDaysIn(int JalYear, int JalMonth)
        {
            if (JalYear < 0) return -1;
            if (JalYear > JAL_MAX_YEAR) return -1;
            if (JalMonth < 1) return -1;
            if (JalMonth > 12) return -1;

            if (IsJalLeapYear(JalYear)) return JalMonthDaysLeapYear[JalMonth - 1];
            return JalMonthDaysSoundYear[JalMonth - 1];
        }


        /// <summary>
        /// Gets the number of days in a given jalali year
        /// </summary>
        /// <param name="JalYear">Year</param>
        /// <returns>
        /// The number of days in a given jalali year
        /// </returns>
        public int JalDaysIn(int JalYear)
        {
            if (JalYear < 0) return -1;
            if (JalYear > JAL_MAX_GDP) return -1;
            if (IsJalLeapYear(JalYear)) return 366;
            return 365;
        }



        /// <summary>
        /// Gets an integer which represents the year compliment of a lunar (Arabic-Hejri) date
        /// </summary>
        /// <returns>
        /// An integer which represents the year compliment of a lunar (Arabic-Hejri) date
        /// </returns>
        public int GetLunYear()
        {
            int Rem = 0;
            return this.GetLunYear(this.m_Gdp, ref Rem);
        }


        /// <summary>
        /// Gets an integer which represents the year compliment of a lunar (Arabic-Hejri) date
        /// </summary>
        /// <param name="Gdp">An input GDP</param>
        /// <param name="Rem">An output remainder</param>
        /// <returns>
        /// An integer which represents the year compliment of a lunar (Arabic-Hejri) date
        /// </returns>
        private int GetLunYear(double Gdp, ref int Rem)
        {
            return GetLunYear((int)Gdp, ref Rem);
        }



        /// <summary>
        /// Gets an integer which represents the month compliment of a lunar (Arabic-Hejri) date
        /// </summary>
        /// <returns>
        /// An integer which represents the month compliment of a lunar (Arabic-Hejri) date
        /// </returns>
        public int GetLunMonth()
        {
            int Rem = 0;
            int Y = this.GetLunYear(this.m_Gdp, ref Rem);
            int M = this.GetLunMonth(this.IsLunLeapYear(Y), Rem, ref Rem);
            return M;
        }


        /// <summary>
        /// Gets a string which represents the month title of a lunar (Arabic-Hejri) date
        /// </summary>
        /// <returns>
        /// A string which represents the month title of a lunar (Arabic-Hejri) date
        /// </returns>
        public string GetLunMonthTitle()
        {
            return LUN_MONTHS[this.GetLunMonth() - 1];
        }



        /// <summary>
        /// Gets a string which represents the abbreviated month title of a lunar (Arabic-Hejri) date
        /// </summary>
        /// <returns>
        /// A string which represents the abbreviated month title of a lunar (Arabic-Hejri) date
        /// </returns>
        public string GetLunMonthTitleAbbr()
        {
            return LUN_MONTHS_ABBR[this.GetLunMonth() - 1];
        }


        /// <summary>
        /// Gets an integer which represents the month day compliment of a lunar (Arabic-Hejri) date
        /// </summary>
        /// <returns>
        /// An integer which represents the month day compliment of a lunar (Arabic-Hejri) date
        /// </returns>
        public int GetLunDayOfMonth()
        {
            int Rem = 0;
            int Y = this.GetLunYear(this.m_Gdp, ref Rem);
            int M = this.GetLunMonth(this.IsLunLeapYear(Y), Rem, ref Rem);
            int D = Rem + 1;
            return D;
        }



        /// <summary>
        /// Gets a 10 character lunar (Arabic-Hejri) date
        /// </summary>
        /// <returns>
        /// A 10 character lunar (Arabic-Hejri) date
        /// </returns>
        public string GetLunDate()
        {
            int y = this.GetLunYear();
            int m = this.GetLunMonth();
            int d = this.GetLunDayOfMonth();
            string Res = string.Format("{0}/{1}/{2}", AddZeroToleft(y, 4), AddZeroToleft(m, 2), AddZeroToleft(d, 2));
            return Res;
        }




        /// <summary>
        /// Checks if the given date is a valid lunar (Arabic-Hejri) date and returns true if valid.
        /// </summary>
        /// <param name="LunYear">Year</param>
        /// <param name="LunMonth">Month</param>
        /// <param name="LunDayOfMonth">Day</param>
        /// <returns>
        /// True: Valid gregorian date
        /// False: Invalid Gregorian date
        /// </returns>
        public bool IsValidLunDate(int LunYear, int LunMonth, int LunDayOfMonth)
        {
            // This routine is not compeletely tested.
            if (LunYear < 0) return false;
            if (LunYear > 9999) return false;
            if (LunMonth < 0) return false;
            if (LunMonth > 12) return false;
            if (LunDayOfMonth < 0) return false;
            if (LunDayOfMonth > 31) return false;
            if (LunDayOfMonth == 1) return true;

            MultiCalendar mc = new MultiCalendar();
            mc.SetLunDate(LunYear, LunMonth, 1);
            double gdp = mc.GetGdp();

            for (int i = 1; i < LunDayOfMonth; i++)
            {
                mc.SetGdp(gdp + i);
                if (mc.GetLunDayOfMonth() == LunDayOfMonth) return true;
            }
            return false;
        }



        /// <summary>
        /// Sets a given lunar (Arabic-Hejri) date between (0001/01/01 lunar (Arabic-Hejri)) and (9666/04/03 lunar (Arabic-Hejri))
        /// </summary>
        /// <param name="LunYear">Year</param>
        /// <param name="LunMonth">Month</param>
        /// <param name="LunDayOfMonth">Day</param>
        /// <returns>
        /// Success: 0
        /// Failure: -1
        /// </returns>
        public int SetLunDate(int LunYear, int LunMonth, int LunDayOfMonth)
        {
            if (!this.IsValidLunDate(LunYear, LunMonth, LunDayOfMonth)) return -1;

            this.m_Gdp = this.LunDateToGregDaysPassed(LunYear, LunMonth, LunDayOfMonth);
            return 0;
        }

        
        
        
        /// <summary>
        /// Sets a given lunar (Arabic-Hejri) date between (0001/01/01 lunar (Arabic-Hejri)) and (9666/04/03 lunar (Arabic-Hejri))
        /// </summary>
        /// <param name="ALunDate">An input date string between (0001/01/01 lunar (Arabic-Hejri)) and (9666/04/03 lunar (Arabic-Hejri))</param>
        /// <returns>
        /// Success: 0
        /// Failure: -1
        /// </returns>
        public int SetLunDate(string ALunDate)
        {
            int n = ALunDate.Length;
            if (n < 10) return -1;
            try
            {
                int y = Convert.ToInt32(ALunDate.Substring(0, 4));
                int m = Convert.ToInt32(ALunDate.Substring(5, 2));
                int d = Convert.ToInt32(ALunDate.Substring(8, 2));
                return this.SetLunDate(y, m, d);
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        
        
        /// <summary>
        /// Gets the number of days in a given lunar (Arabic-Hejri) month
        /// </summary>
        /// <param name="LunYear">Year</param>
        /// <param name="LunMonth">Month</param>
        /// <returns>
        /// The number of days in a given lunar (Arabic-Hejri) month
        /// </returns>
        public int LunDaysIn(int LunYear, int LunMonth)
        {
            if (LunYear < 0) return -1;
            if (LunYear > LUN_MAX_YEAR) return -1;
            if (LunMonth < 1) return -1;
            if (LunMonth > 12) return -1;

            if (IsLunLeapYear(LunYear)) return LunMonthDaysLeapYear[LunMonth - 1];
            return LunMonthDaysSoundYear[LunMonth - 1];
        }



        /// <summary>
        /// Gets the number of days in a given lunar (Arabic-Hejri) year
        /// </summary>
        /// <param name="LunYear">Year</param>
        /// <returns>
        /// The number of days in a given lunar (Arabic-Hejri) year
        /// </returns>
        public int LunDaysIn(int LunYear)
        {
            if (LunYear < 0) return -1;
            if (LunYear > LUN_MAX_GDP) return -1;
            if (IsLunLeapYear(LunYear)) return 366;
            return 365;
        }



        /// <summary>
        /// Gets the object's GDP (Gregorian Days Passed after christ)
        /// </summary>
        /// <returns>
        /// The object's GDP (Gregorian Days Passed after christ)
        /// </returns>
        public double GetGdp()
        {
            return this.m_Gdp;
        }


        /// <summary>
        /// Sets the object's GDP (Gregorian Days Passed after christ)
        /// </summary>
        /// <param name="Gdp">
        /// GDP (Gregorian Days Passed after christ)
        /// </param>
        /// <returns>
        /// Success: 0
        /// Failure: -1
        /// </returns>
        public int SetGdp(double Gdp)
        {
            if (Gdp < 0) return -1;
            if (Gdp > GREG_MAX_GDP) return -1;

            this.m_Gdp = Gdp;
            return 0;
        }


        #region Static Members
        
        /// <summary>
        /// A static method which directly converts a gregorian date string to its jalali equivalent
        /// </summary>
        /// <param name="AGregDate">An input date string</param>
        /// <returns>
        /// Success: Equivalent Jalali date string
        /// Failure: Empty string.
        /// </returns>
        public static string GregToJal(string AGregDate)
        {
            MultiCalendar mc = new MultiCalendar();
            if (mc.SetGregDate(AGregDate) == -1) return "";
            return mc.GetJalDate();
        }



        /// <summary>
        /// A static method which directly converts a gregorian date string to its lunar(arabic) equivalent
        /// </summary>
        /// <param name="AGregDate">An input date string</param>
        /// <returns>
        /// Success: Equivalent Lunar date string
        /// Failure: Empty string.
        /// </returns>
        public static string GregToLun(string AGregDate)
        {
            MultiCalendar mc = new MultiCalendar();
            if (mc.SetGregDate(AGregDate) == -1) return "";
            return mc.GetLunDate();
        }

        
        /// <summary>
        /// A static method which directly converts a jalali date string to its gregorian equivalent
        /// </summary>
        /// <param name="AJalDate">An input date string</param>
        /// <returns>
        /// Success: Equivalent Gregorian date string
        /// Failure: Empty string.
        /// </returns>
        public static string JalToGreg(string AJalDate)
        {
            MultiCalendar mc = new MultiCalendar();
            if (mc.SetJalDate(AJalDate) == -1) return "";
            return mc.GetGregDate();
        }



        /// <summary>
        /// A static method which directly converts a jalali date string to its lunar(arabic) equivalent
        /// </summary>
        /// <param name="AJalDate">An input date string</param>
        /// <returns>
        /// Success: Equivalent Lunar(Arabi) date string
        /// Failure: Empty string.
        /// </returns>
        public static string JalToLun(string AJalDate)
        {
            MultiCalendar mc = new MultiCalendar();
            if (mc.SetJalDate(AJalDate) == -1) return "";
            return mc.GetLunDate();
        }



        /// <summary>
        /// A static method which directly converts a lunar date string to its gregorian equivalent
        /// </summary>
        /// <param name="ALunDate">An input date string</param>
        /// <returns>
        /// Success: Equivalent Gregorian date string
        /// Failure: Empty string.
        /// </returns>
        public static string LunToGreg(string ALunDate)
        {
            MultiCalendar mc = new MultiCalendar();
            if (mc.SetLunDate(ALunDate) == -1) return "";
            return mc.GetGregDate();
        }



        /// <summary>
        /// A static method which directly converts a lunar date string to its jalali equivalent
        /// </summary>
        /// <param name="ALunDate">An input date string</param>
        /// <returns>
        /// Success: Equivalent Jalali date string
        /// Failure: Empty string.
        /// </returns>
        public static string LunToJal(string ALunDate)
        {
            MultiCalendar mc = new MultiCalendar();
            if (mc.SetLunDate(ALunDate) == -1) return "";
            return mc.GetJalDate();
        }



        /// <summary>
        /// Converts the GDP (Gregorian Days Passed after christ) to its gregorian equivalent date string
        /// </summary>
        /// <param name="AGdp">An input GDP</param>
        /// <returns>
        /// Success: Equivalent Gregorian date string
        /// Failure: Empty string
        /// </returns>
        public static string GdpToGreg(double AGdp)
        {
            MultiCalendar mc = new MultiCalendar();
            if (mc.SetGdp(AGdp) == -1) return "";
            return mc.GetGregDate();
        }



        /// <summary>
        /// Converts the GDP (Gregorian Days Passed after christ) to its jalali equivalent date string
        /// </summary>
        /// <param name="AGdp">An input GDP</param>
        /// <returns>
        /// Success: Equivalent Jalali date string
        /// Failure: Empty string
        /// </returns>
        public static string GdpToJal(double AGdp)
        {
            if (AGdp < JAL_ORG_GDP) return "";
            MultiCalendar mc = new MultiCalendar();
            if (mc.SetGdp(AGdp) == -1) return "";
            return mc.GetJalDate();
        }



        /// <summary>
        /// Converts the GDP (Gregorian Days Passed after christ) to its lunar(arabic) equivalent date string
        /// </summary>
        /// <param name="AGdp">An input GDP</param>
        /// <returns>
        /// Success: Equivalent Lunar(Arabic) date string
        /// Failure: Empty string
        /// </returns>
        public static string GdpToLun(double AGdp)
        {
            if (AGdp < LUN_ORG_GDP) return "";
            MultiCalendar mc = new MultiCalendar();
            if (mc.SetGdp(AGdp) == -1) return "";
            return mc.GetLunDate();
        }


        /// <summary>
        /// Converts a gregorian date string to its GDP (Gregorian Days Passed after christ) equivalent
        /// </summary>
        /// <param name="AGregDate">An input date string</param>
        /// <returns>
        /// Success: Equivalent GDP
        /// Failure: -1
        /// </returns>
        public static double GregToGdp(string AGregDate)
        {
            MultiCalendar mc = new MultiCalendar();
            if (mc.SetGregDate(AGregDate) == -1) return -1;
            return mc.GetGdp();
        }



        /// <summary>
        /// Converts a jalali date string to its GDP (Gregorian Days Passed after christ) equivalent
        /// </summary>
        /// <param name="AJalDate">An input date string</param>
        /// <returns>
        /// Success: Equivalent GDP
        /// Failure: -1
        /// </returns>
        public static double JalToGdp(string AJalDate)
        {
            MultiCalendar mc = new MultiCalendar();
            if (mc.SetJalDate(AJalDate) == -1) return -1;
            return mc.GetGdp();
        }




        /// <summary>
        /// Converts a lunar date string to its GDP (Gregorian Days Passed after christ) equivalent
        /// </summary>
        /// <param name="AJalDate">An input date string</param>
        /// <returns>
        /// Success: Equivalent GDP
        /// Failure: -1
        /// </returns>
        public static double LunToGdp(string ALunDate)
        {
            MultiCalendar mc = new MultiCalendar();
            if (mc.SetLunDate(ALunDate) == -1) return -1;
            return mc.GetGdp();
        }
        #endregion Static Members




        /// <summary>
        /// Adds days to the existing date
        /// </summary>
        /// <param name="Days">Days: Number of days to be added</param>
        public void AddDays(int Days)
        {
            this.SetGdp(this.GetGdp() + Days);
        }

        internal string GetTime(string TimeSeparator)
        {
            string time_str = "";
            time_str += this.GetHoure24().ToString().PadLeft(2, '0') + TimeSeparator;
            time_str += this.GetMinute().ToString().PadLeft(2, '0')+ TimeSeparator;
            time_str += this.GetSecond().ToString().PadLeft(2, '0');
            return time_str;
        }
    }
}