using Dragon.CpuInfo;
using Newtonsoft.Json.Linq;



Console.WriteLine();
Console.WriteLine(JObject.FromObject( BindingBridge.Parse()).ToString());